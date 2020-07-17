using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Facade
{
    public class ModelFactorHelper
    {
        private static ModelFactorService.IModelFactorService _service;
        private static ModelFactorService.IModelFactorService GetService()
        {
            if (_service == null)
            {
                _service = new ModelFactorService.ModelFactorServiceClient();
                Uri uri = new Uri(((ClientBase<ModelFactorService.IModelFactorService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                ((ClientBase<ModelFactorService.IModelFactorService>)_service).Endpoint.Address = new EndpointAddress(uri);
            }
            else
            {
                try
                {
                    ((ClientBase<ModelFactorService.IModelFactorService>)(_service)).Close();
                }
                catch
                { }
                finally
                {
                    _service = new ModelFactorService.ModelFactorServiceClient();
                    Uri uri = new Uri(((ClientBase<ModelFactorService.IModelFactorService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                    ((ClientBase<ModelFactorService.IModelFactorService>)_service).Endpoint.Address = new EndpointAddress(uri);
                }

            }
            switch (((ClientBase<ModelFactorService.IModelFactorService>)(_service)).State)
            {
                case CommunicationState.Faulted:
                    ((ClientBase<ModelFactorService.IModelFactorService>)(_service)).Abort();
                    _service = new ModelFactorService.ModelFactorServiceClient();
                    break;
                case CommunicationState.Closed:
                case CommunicationState.Closing:
                    _service = new ModelFactorService.ModelFactorServiceClient();
                    break;
            }
            return _service;
        }

        /// <summary>
        /// Returns a new ModelFactor (service).
        /// </summary>
        /// <returns></returns>
        public static ModelFactor GetNew()
        {
            return new ModelFactor();
        }

        /// <summary>
        /// Returns the list of all ModelFactor (service).
        /// </summary>
        /// <returns></returns>
        public static List<ModelFactor> GetAll()
        {
            return (GetService().GetAll()).ToList();
        }

        /// <summary>
        /// Make a validation for Modelfactor (service).
        /// </summary>
        /// <param name="modelFactor"></param>
        /// <returns></returns>
        public static bool Validate(ModelFactor modelFactor)
        {
            return GetService().Validate((modelFactor));
        }

        /// <summary>
        /// Saves the ModelFactor (service).
        /// </summary>
        /// <param name="modelFactor"></param>
        public static void Save(ModelFactor modelFactor)
        {
            GetService().Save((modelFactor));
        }

        /// <summary>
        /// Returns the list of Modelfactor by model (service).
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<ModelFactor> GetByModel(Model model)
        {
            return (GetService().GetByModel(model.IDModel)).ToList();
        }

        /// <summary>
        /// Returns the ModelFactor by idModelFactor (service).
        /// </summary>
        /// <param name="idModelFactor"></param>
        /// <returns></returns>
        public static ModelFactor Get(int idModelFactor)
        {
            return (GetService().Get(idModelFactor));

        }

        /// <summary>
        /// Returns the list of ModelFactor by idModel and idRegion (service).
        /// </summary>
        /// <param name="idModel"></param>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<ModelFactor> GetModelFactorWithDataAvailable(int idModel, int idRegion)
        {
            return (GetService().GetModelFactorWithDataAvailable(idModel, idRegion)).ToList();
        }

        /// <summary>
        /// Returns the list of ModelFactor by idModel and idFactor  (service).
        /// </summary>
        /// <param name="idModel"></param>
        /// <param name="idFactor"></param>
        /// <returns></returns>
        public static List<ModelFactor> GetByModelAndFactorId(int idModel, int idFactor)
        {
            return (GetService().GetByModelAndFactorId(idModel, idFactor)).ToList();
        }

        /// <summary>
        /// Deletes the ModelFactor
        /// </summary>
        /// <param name="modelFactor"></param>
        public static void Delete(ModelFactor modelFactor)
        {
            GetService().Delete(modelFactor);
        }
    }
}
