using System.Collections.Generic;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Server
{
    [ServiceContract]
    public interface IModelRiskAlertService
    {
        [OperationContract]
        List<ModelRiskAlert> GetAll();

        [OperationContract]
        List<ModelRiskAlert> GetWithFilter(int? idModel, List<int> regions, bool? isActive);

        [OperationContract]
        ModelRiskAlert Save(ModelRiskAlert modelRiskAlert);

        [OperationContract]
        void Delete(ModelRiskAlert modelRiskAlert);

        [OperationContract]
        ModelRiskAlertAttachment GetNewModelRiskAlertAttachment();

        [OperationContract]
        ModelRiskAlertPhase GetNewModelRiskAlertPhase();

        [OperationContract]
        void SavePhase(ModelRiskAlertPhase modelRiskAlertPhase);

        [OperationContract]
        void SaveRegion(ModelRiskAlertRegion modelRiskAlertRegion);

        [OperationContract]
        void SaveAttachment(ModelRiskAlertAttachment modelRiskAlertAttachment);

        [OperationContract]
        ModelRiskAlertRegion GetNewModelRiskAlertRegion();

        [OperationContract]
        List<Region> GetAllRegionsWithAlerts();

        [OperationContract]
        ModelRiskAlert Get(int idModelRiskAlert);

        [OperationContract]
        List<Region> GetRegions(ModelRiskAlert modelRiskAlert);

        [OperationContract]
        List<ModelRiskAlertPhase> GetPhases(ModelRiskAlert modelRiskAlert);

        [OperationContract]
        List<ModelRiskAlertRegion> GetModelRiskAlertRegions(ModelRiskAlert modelRiskAlert);

        [OperationContract]
        List<ModelRiskAlertAttachment> GetModelRiskAlertAttachments(ModelRiskAlert modelRiskAlert);

        [OperationContract]
        void DeletePhase(ModelRiskAlertPhase modelRiskAlertPhase);

        [OperationContract]
        void DeleteRegion(ModelRiskAlertRegion modelRiskAlertRegion);

        [OperationContract]
        void DeleteAttachment(ModelRiskAlertAttachment modelRiskAlertAttachment);
    }
}
