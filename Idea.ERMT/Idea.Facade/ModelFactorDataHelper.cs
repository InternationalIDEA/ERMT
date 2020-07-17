using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Facade
{
    public class ModelFactorDataHelper
    {
        private static ModelFactorDataService.IModelFactorDataService _service;
        private static ModelFactorDataService.IModelFactorDataService GetService()
        {
            if (_service == null)
            {
                _service = new ModelFactorDataService.ModelFactorDataServiceClient();
                Uri uri = new Uri(((ClientBase<ModelFactorDataService.IModelFactorDataService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                ((ClientBase<ModelFactorDataService.IModelFactorDataService>)_service).Endpoint.Address = new EndpointAddress(uri);            
            }
            else
            {
                try
                {
                    ((ClientBase<ModelFactorDataService.IModelFactorDataService>)(_service)).Close();
                }
                catch
                { }
                finally
                {
                    _service = new ModelFactorDataService.ModelFactorDataServiceClient();
                    Uri uri = new Uri(((ClientBase<ModelFactorDataService.IModelFactorDataService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                    ((ClientBase<ModelFactorDataService.IModelFactorDataService>)_service).Endpoint.Address = new EndpointAddress(uri);
                }

            }
            switch (((ClientBase<ModelFactorDataService.IModelFactorDataService>)(_service)).State)
            {
                case CommunicationState.Faulted:
                    ((ClientBase<ModelFactorDataService.IModelFactorDataService>)(_service)).Abort();
                    _service = new ModelFactorDataService.ModelFactorDataServiceClient();
                    break;
                case CommunicationState.Closed:
                case CommunicationState.Closing:
                    _service = new ModelFactorDataService.ModelFactorDataServiceClient();
                    break;
            }

            return _service;
        }

        /// <summary>
        /// Returns a new ModelFactorData (service).
        /// </summary>
        /// <returns></returns>
        public static ModelFactorData GetNew()
        {
            return new ModelFactorData();
        }

        /// <summary>
        /// Returns the list of all ModelFactorDatas (service).
        /// </summary>
        /// <returns></returns>
        public static List<ModelFactorData> GetAll()
        {
            return GetService().GetAll().ToList();
        }

        /// <summary>
        /// Saves the ModelFactorData (service).
        /// </summary>
        /// <param name="modelFactorData"></param>
        /// <returns></returns>
        public static ModelFactorData Save(ModelFactorData modelFactorData)
        {
            return GetService().Save(modelFactorData);
        }

        /// <summary>
        /// Returns the first available date to be used when pasting data for a factor.
        /// </summary>
        /// <param name="idModelFactor"></param>
        /// <returns></returns>
        public static DateTime GetAvailableDateForPastedDataByModelFactor(int idModelFactor)
        {
            return GetService().GetAvailableDateForPastedDataByModelFactor(idModelFactor);
        }

        /// <summary>
        /// Returns the first available date to be used when pasting data for a region.
        /// </summary>
        /// <param name="idRegion"></param>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static DateTime GetAvailableDateForPastedDataByRegionAndModel(int idRegion, int idModel)
        {
            return GetService().GetAvailableDateForPastedDataByRegionAndModel(idRegion, idModel);
        }

        /// <summary>
        /// Returns a ModelFactorData (service).
        /// </summary>
        /// <param name="modelFactorData"></param>
        /// <returns></returns>
        public static ModelFactorData Get(ModelFactorData modelFactorData)
        {
            return GetService().Get(modelFactorData.IDModelFactorData);
        }

        /// <summary>
        /// Returns the list a ModelFactorData by modelFactor (service).
        /// </summary>
        /// <param name="modelFactor"></param>
        /// <returns></returns>
        public static List<ModelFactorData> GetByModelFactor(ModelFactor modelFactor)
        {
            return GetService().GetByModelFactor(modelFactor.IDModelFactor).ToList();
        }

        /// <summary>
        /// Returns the list a ModelFactorData by date, idModel an idRegion (service).
        /// </summary>
        /// <param name="date"></param>
        /// <param name="idModel"></param>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<ModelFactorData> GetByDateModelRegion(DateTime date, int idModel, int idRegion)
        {
            return GetService().GetByDateModelRegion(date, idModel, idRegion).ToList();
        }

        /// <summary>
        /// Returns a list of regions containing data for the provided model factors and dates.
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="modelFactors"></param>
        /// <returns></returns>
        public static List<Region> GetRegionsWithData(DateTime dateFrom, DateTime dateTo, List<ModelFactor> modelFactors)
        {
            return GetService().GetRegionsWithData(dateFrom, dateTo, modelFactors);
        }

        /// <summary>
        /// Returns a map by idModelFactor, regionsIds and DateTime (service). 
        /// </summary>
        /// <param name="idModelFactor"></param>
        /// <param name="regionsIds"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static List<ModelFactorData> GetMapData(int idModelFactor, string regionsIds, DateTime from, DateTime to)
        {
            return GetService().GetMapData(idModelFactor, regionsIds, from, to).ToList();
        }

        /// <summary>
        /// Returns the list of ModelFactorData by DateTime (service).
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="idModelFactor"></param>
        /// <param name="regionsIds"></param>
        /// <returns></returns>
        public static List<ModelFactorData> GetBetweenDatesModelFactorRegion(DateTime from, DateTime to, int idModelFactor, string regionsIds)
        {
            return GetService().GetBetweenDatesModelFactorRegion(from, to, idModelFactor, regionsIds).ToList();
        }

        /// <summary>
        /// Returns the minor date time with idModel (service).
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static DateTime? GetMinorDate(int idModel)
        {
            return GetService().GetMinorDate(idModel);
        }

        /// <summary>
        /// Returns the major date time with idModel (service).
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static DateTime? GetMajorDate(int idModel)
        {
            return GetService().GetMajorDate(idModel);
        }

        /// <summary>
        /// Returns the Maximum level with idModelFactor, idParent and date time (service).
        /// </summary>
        /// <param name="idModelFactor"></param>
        /// <param name="idParent"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static int GetMaxLevelWithData(int idModelFactor, int idParent, DateTime from, DateTime to)
        {
            return GetService().GetMaxLevelWithData(idModelFactor, idParent, from, to);
        }

        /// <summary>
        /// Returns the last ModelFactorData by idModel (service).
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static ModelFactorData GetModelFactorLastDate(int idModel)
        {
            return GetService().GetModelFactorLastDate(idModel);
        }

        /// <summary>
        /// Returns the list of ModelFactorData available by idModel and idRegion (service). 
        /// </summary>
        /// <param name="idModel"></param>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<ModelFactorData> GetModelFactorWithDataAvailable(int idModel, int idRegion)
        {
            return GetService().GetModelFactorWithDataAvailable(idModel, idRegion).ToList();
        }

        /// <summary>
        /// Returns the list of ModelFactorData by idRegion (service).
        /// </summary>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<ModelFactorData> GetByRegionId(int idRegion)
        {
            return GetService().GetByRegion(idRegion).ToList();
        }

        /// <summary>
        /// Returns the list of ModelFactorData by idModelFactor and Date time (service).
        /// </summary>
        /// <param name="idModelFactor"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<ModelFactorData> GetByModelFactorIdAndDate(int idModelFactor, DateTime date)
        {
            return GetService().GetByModelFactorIdAndDate(idModelFactor, date).ToList();
        }

        /// <summary>
        /// Returns the list of ModelFactorData by Date time, idModelFactor and idRegion (service).
        /// </summary>
        /// <param name="date"></param>
        /// <param name="idModelFactor"></param>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<ModelFactorData> GetByDateModelFactorRegion(DateTime date, int idModelFactor, int idRegion)
        {
            return GetService().GetByDateModelFactorRegion(date, idModelFactor, idRegion).ToList();
        }

        /// <summary>
        /// Deletes the ModelFactorData (service).
        /// </summary>
        /// <param name="modelfactordata"></param>
        public static void Delete(ModelFactorData modelfactordata)
        {
            GetService().Delete(modelfactordata);
        }

        /// <summary>
        /// Deletes the ModelFactorData by Date time, modelId and regionID (service).
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="modelId"></param>
        /// <param name="regionID"></param>
        public static void DeleteByDateModelRegion(DateTime dt, int modelId, int regionID)
        {
            List<ModelFactorData> data = GetByDateModelRegion(dt, modelId, regionID);
            foreach (ModelFactorData m in data)
            {
                Delete(m);
            }
        }

        /// <summary>
        /// Deletes the ModelFactorData by Date time and modelfactorId (service).
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="modelfactorId"></param>
        public static void DeleteByDateModelFactor(DateTime dt, int modelfactorId)
        {
            List<ModelFactorData> data = GetByModelFactorIdAndDate(modelfactorId, dt);
            foreach (ModelFactorData m in data)
            {
                Delete(m);
            }

        }
    }
}




