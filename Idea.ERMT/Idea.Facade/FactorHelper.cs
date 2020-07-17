using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Facade
{
    public class FactorHelper
    {
        private static FactorService.IFactorService _service;
        private static FactorService.IFactorService GetService()
        {
            if (_service == null)
            {
                _service = new FactorService.FactorServiceClient();
                Uri uri = new Uri(((ClientBase<FactorService.IFactorService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                ((ClientBase<FactorService.IFactorService>)_service).Endpoint.Address = new EndpointAddress(uri);
            }
            else
            {
                try
                {
                    ((ClientBase<FactorService.IFactorService>)(_service)).Close();
                }
                catch
                { }
                finally
                {
                    _service = new FactorService.FactorServiceClient();
                    Uri uri = new Uri(((ClientBase<FactorService.IFactorService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                    ((ClientBase<FactorService.IFactorService>)_service).Endpoint.Address = new EndpointAddress(uri);
                }

            }
            switch (((ClientBase<FactorService.IFactorService>)(_service)).State)
            {
                case CommunicationState.Faulted:
                    ((ClientBase<FactorService.IFactorService>)(_service)).Abort();
                    _service = new FactorService.FactorServiceClient();
                    break;
                case CommunicationState.Closed:
                case CommunicationState.Closing:
                    _service = new FactorService.FactorServiceClient();
                    break;
            }

            return _service;
        }

        /// <summary>
        /// Returns list the Factors (services).
        /// </summary>
        /// <returns></returns>
        public static List<Factor> GetAll()
        {
            return GetService().GetAll().ToList();
        }

        /// <summary>
        /// Validate the Factor by factor.
        /// </summary>
        /// <param name="factor"></param>
        public static void Validate(Factor factor)
        {
            if (string.IsNullOrEmpty(factor.Name))
            {
                throw new ArgumentException(("FactorNameRequired") );
            }

            if (GetAll().Any(f => f.Name.ToLower() == factor.Name.ToLower() && f.IdFactor != factor.IdFactor))
            {
                throw new ArgumentException("FactorNameAlreadyExists");
            }

            if (!factor.CumulativeFactor)
            {
                if (factor.ScaleMin < 0)
                {
                    throw new ArgumentException("FactorScaleMin");
                }
                if (factor.ScaleMax <= factor.ScaleMin)
                {
                    throw new ArgumentException("FactorScaleMax");
                }
                if (factor.Interval <= 0)
                {
                    throw new ArgumentException("FactorInterval");
                }
            }
        }

        /// <summary>
        /// Saves a Factor (Service).
        /// </summary>
        /// <param name="factor"></param>
        public static void Save(Factor factor)
        {
            GetService().Save(factor);
        }

        /// <summary>
        /// Returns a Factor (Service)
        /// </summary>
        /// <param name="idFactor"></param>
        /// <returns></returns>
        public static Factor Get(int idFactor)
        {
            return GetService().Get(idFactor);
        }

        /// <summary>
        /// Deletes a Factor (Service).
        /// </summary>
        /// <param name="factor"></param>
        public static void Delete(Factor factor)
        {
            GetService().Delete(factor);
        }
    }
}
