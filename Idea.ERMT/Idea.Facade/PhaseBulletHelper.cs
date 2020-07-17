using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Facade
{
    public class PhaseBulletHelper
    {
        private static PhaseBulletService.IPhaseBulletService _service;
        private static PhaseBulletService.IPhaseBulletService GetService()
        {
            if (_service == null)
            {
                _service = new PhaseBulletService.PhaseBulletServiceClient();
                Uri uri = new Uri(((ClientBase<PhaseBulletService.IPhaseBulletService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                ((ClientBase<PhaseBulletService.IPhaseBulletService>)_service).Endpoint.Address = new EndpointAddress(uri);
            }
            else
            {
                try
                {
                    ((ClientBase<PhaseBulletService.IPhaseBulletService>)(_service)).Close();
                }
                catch
                { }
                finally
                {
                    _service = new PhaseBulletService.PhaseBulletServiceClient();
                    Uri uri = new Uri(((ClientBase<PhaseBulletService.IPhaseBulletService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                    ((ClientBase<PhaseBulletService.IPhaseBulletService>)_service).Endpoint.Address = new EndpointAddress(uri);
                }

            }
            switch (((ClientBase<PhaseBulletService.IPhaseBulletService>)(_service)).State)
            {
                case CommunicationState.Faulted:
                    ((ClientBase<PhaseBulletService.IPhaseBulletService>)(_service)).Abort();
                    _service = new PhaseBulletService.PhaseBulletServiceClient();
                    break;
                case CommunicationState.Closed:
                case CommunicationState.Closing:
                    _service = new PhaseBulletService.PhaseBulletServiceClient();
                    break;
            }
            return _service;
        }

        /// <summary>
        /// Returns a new PhaseBullet (service).
        /// </summary>
        /// <returns></returns>
        public static PhaseBullet GetNew()
        {
            return new PhaseBullet();
        }

        /// <summary>
        /// Returns the list of all PhaseBullet (service).
        /// </summary>
        /// <returns></returns>
        public static List<PhaseBullet> GetAll()
        {
            return (GetService().GetAll()).ToList();
        }

        /// <summary>
        /// Returns the list of PhaseBullet by idPhase and columnNumber (service).
        /// </summary>
        /// <param name="idPhase"></param>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        public static List<PhaseBullet> GetByPhaseAndColumn(int idPhase, int columnNumber)
        {
            return (GetService().GetByPhaseAndColumn(idPhase, columnNumber)).ToList();
        }

        /// <summary>
        /// Deletes a PhaseBullet (service).
        /// </summary>
        /// <param name="phaseBullet"></param>
        public static void Delete(PhaseBullet phaseBullet)
        {
            GetService().Delete(phaseBullet);
        }

        /// <summary>
        /// Deletes a PhaseBullet (service).
        /// </summary>
        /// <param name="idPhaseBullet"></param>
        public static void Delete(int idPhaseBullet)
        {
            GetService().DeleteByID(idPhaseBullet);
        }


        /// <summary>
        /// Saves the list of phase bullets
        /// </summary>
        /// <param name="phaseBullets"></param>
        public static void SaveColumnBullets(List<PhaseBullet> phaseBullets)
        {
            GetService().SaveColumnBullets(phaseBullets);
        }

    }
}
