using System;
using System.Collections.Generic;
using Idea.Business;
using Idea.Entities;

namespace Idea.Server
{
    public class ModelFactorDataService : IModelFactorDataService
    {
        public ModelFactorData Save(ModelFactorData modelFactorData)
        {
            return ModelFactorDataManager.Save(modelFactorData);
        }

        /// <summary>
        /// Returns the first available date to be used when pasting data for a factor.
        /// </summary>
        /// <param name="idModelFactor"></param>
        /// <returns></returns>
        public DateTime GetAvailableDateForPastedDataByModelFactor(int idModelFactor)
        {
            return ModelFactorDataManager.GetAvailableDateForPastedDataByModelFactor(idModelFactor);
        }

        /// <summary>
        /// Returns the first available date to be used when pasting data for a region.
        /// </summary>
        /// <param name="idRegion"></param>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public DateTime GetAvailableDateForPastedDataByRegionAndModel(int idRegion, int idModel)
        {
            return ModelFactorDataManager.GetAvailableDateForPastedDataByRegionAndModel(idRegion, idModel);
        }

        public List<ModelFactorData> GetAll()
        {
            return ModelFactorDataManager.GetAll();
        }

        public ModelFactorData Get(int idModelFactorData)
        {
            return ModelFactorDataManager.GetById(idModelFactorData);
        }

        public List<ModelFactorData> GetByDateModelRegion(DateTime date, int idModel, int idRegion)
        {
            return ModelFactorDataManager.GetByDateModelRegion(date, idModel, idRegion);
        }
        public List<ModelFactorData> GetByDateModelFactorRegion(DateTime date, int idModelFactor, int idRegion)
        {
            return ModelFactorDataManager.GetByDateModelFactorRegion(date, idModelFactor, idRegion);
        }

        public List<ModelFactorData> GetByModelFactor(int idModelFactor)
        {
            return ModelFactorDataManager.GetByModelFactor(idModelFactor);
        }

        public List<ModelFactorData> GetMapData(int idModelFactor, string regionsIds, DateTime from, DateTime to)
        {
            return ModelFactorDataManager.GetMapData(idModelFactor, regionsIds, from, to);
        }

        public List<ModelFactorData> GetBetweenDatesModelFactorRegion(DateTime from, DateTime to, int idModelFactor, string regionsIds)
        {
            return ModelFactorDataManager.GetBetweenDatesModelFactorRegion(from, to, idModelFactor, regionsIds);
        }

        public DateTime? GetMinorDate(int idModel)
        {
            return ModelFactorDataManager.GetMinorDate(idModel);
        }

        public DateTime? GetMajorDate(int idModel)
        {
            return ModelFactorDataManager.GetMajorDate(idModel);
        }

        public int GetMaxLevelWithData(int idModelFactor, int idRegion, DateTime from, DateTime to)
        {
            return ModelFactorDataManager.GetMaxLevelWithData(idModelFactor, idRegion, from, to);
        }

        public ModelFactorData GetModelFactorLastDate(int idModel)
        {
            return ModelFactorDataManager.GetModelFactorLastDate(idModel);
        }

        public List<ModelFactorData> GetModelFactorWithDataAvailable(int idModel, int idRegion)
        {
            return ModelFactorDataManager.GetModelFactorWithDataAvailable(idModel, idRegion);
        }

        public List<ModelFactorData> GetByRegion(int idRegion)
        {
            return ModelFactorDataManager.GetByRegion(idRegion);
        }

        public List<ModelFactorData> GetByModelFactorIdAndDate(int idModelFactor, DateTime date)
        {
            return ModelFactorDataManager.GetByModelFactorIdAndDate(idModelFactor, date);
        }

        public void Delete(ModelFactorData modelFactorData)
        {
            ModelFactorDataManager.Delete(modelFactorData);
        }

        /// <summary>
        /// Returns a list of regions containing data for the provided model factors and dates.
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="modelFactors"></param>
        /// <returns></returns>
        public List<Region> GetRegionsWithData(DateTime dateFrom, DateTime dateTo, List<ModelFactor> modelFactors)
        {
            return ModelFactorDataManager.GetRegionsWithData(dateFrom, dateTo, modelFactors);
        }
    }
}
