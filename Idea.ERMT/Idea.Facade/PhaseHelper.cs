using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Idea.Entities;
using System.Threading;

namespace Idea.Facade
{
    public static class PhaseHelper
    {
        private static PhaseService.IPhaseService _service;
        private static PhaseService.IPhaseService GetService()
        {
            if (_service == null)
            {
                _service = new PhaseService.PhaseServiceClient();
                Uri uri = new Uri(((ClientBase<PhaseService.IPhaseService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                ((ClientBase<PhaseService.IPhaseService>)_service).Endpoint.Address = new EndpointAddress(uri);
            }
            else
            {
                try
                {
                    ((ClientBase<PhaseService.IPhaseService>)(_service)).Close();
                }
                catch
                { }
                finally
                {
                    _service = new PhaseService.PhaseServiceClient();
                    Uri uri = new Uri(((ClientBase<PhaseService.IPhaseService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                    ((ClientBase<PhaseService.IPhaseService>)_service).Endpoint.Address = new EndpointAddress(uri);
                }

            }
            switch (((ClientBase<PhaseService.IPhaseService>)(_service)).State)
            {
                case CommunicationState.Faulted:
                    ((ClientBase<PhaseService.IPhaseService>)(_service)).Abort();
                    _service = new PhaseService.PhaseServiceClient();
                    break;
                case CommunicationState.Closed:
                case CommunicationState.Closing:
                    _service = new PhaseService.PhaseServiceClient();
                    break;
            }
            return _service;
        }

        /// <summary>
        /// Returns the list of all Phases.
        /// </summary>
        /// <returns></returns>
        public static List<Phase> GetAll()
        {
            return (GetService().GetAll()).ToList();
        }


        public static Phase Get(int idPhase)
        {
            return GetService().Get(idPhase);
        }

        /// <summary>
        /// Make a validation for Phase (service).
        /// </summary>
        /// <param name="fase"></param>
        /// <returns></returns>
        public static bool Validate(Phase fase)
        {
            if (string.IsNullOrEmpty(fase.Title))
                throw new ArgumentException("PhaseCannotEmpty");

            return true;
        }

        /// <summary>
        /// Saves the Phase (service).
        /// </summary>
        /// <param name="phase"></param>
        /// <returns></returns>
        public static Phase Save(Phase phase)
        {
            return GetService().Save((phase));
        }

        public static void GenerateAllFiles(Phase phase)
        {
            GetService().GenerateAllFiles(phase, Thread.CurrentThread.CurrentUICulture.Name);
        }

    }
}
