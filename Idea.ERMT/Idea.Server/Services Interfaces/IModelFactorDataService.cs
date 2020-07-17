using System;
using System.Collections.Generic;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Server
{
    [ServiceContract]
    public interface IModelFactorDataService
    {
        [OperationContract]
        ModelFactorData Save(ModelFactorData modelFactorData);

        [OperationContract]
        DateTime GetAvailableDateForPastedDataByModelFactor(int idModelFactor);

        [OperationContract]
        DateTime GetAvailableDateForPastedDataByRegionAndModel(int idRegion, int idModel);

        [OperationContract]
        List<ModelFactorData> GetAll();

        [OperationContract]
        ModelFactorData Get(int idModelFactorData);

        [OperationContract]
        List<ModelFactorData> GetByDateModelRegion(DateTime date, int idModel, int idRegion);

        [OperationContract]
        List<ModelFactorData> GetByDateModelFactorRegion(DateTime date, int idModelFactor, int idRegion);

        [OperationContract]
        List<ModelFactorData> GetByModelFactor(int idModelFactor);

        [OperationContract]
        List<ModelFactorData> GetMapData(int idModelFactor, string regionsIds, DateTime from, DateTime to);

        [OperationContract]
        List<ModelFactorData> GetBetweenDatesModelFactorRegion(DateTime from, DateTime to, int idModelFactor, string regionsIds);

        [OperationContract]
        DateTime? GetMinorDate(int idModel);

        [OperationContract]
        DateTime? GetMajorDate(int idModel);

        [OperationContract]
        int GetMaxLevelWithData(int idModelFactor, int idRegion, DateTime from, DateTime to);

        [OperationContract]
        ModelFactorData GetModelFactorLastDate(int idModel);

        [OperationContract]
        List<ModelFactorData> GetModelFactorWithDataAvailable(int idModel, int idRegion);

        [OperationContract]
        List<ModelFactorData> GetByRegion(int idRegion);

        [OperationContract]
        List<ModelFactorData> GetByModelFactorIdAndDate(int idModelFactor, DateTime date);

        [OperationContract]
        void Delete(ModelFactorData modelfactordata);

        [OperationContract]
        List<Region> GetRegionsWithData(DateTime dateFrom, DateTime dateTo, List<ModelFactor> modelFactors);
    }
}
