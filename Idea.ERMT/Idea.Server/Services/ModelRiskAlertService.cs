using System.Collections.Generic;
using Idea.Business;
using Idea.Entities;


namespace Idea.Server
{
    public class ModelRiskAlertService : IModelRiskAlertService
    {
        public  List<ModelRiskAlert> GetAll()
        {
            return ModelRiskAlertManager.GetAll();
        }

        public List<ModelRiskAlert> GetWithFilter(int? idModel, List<int> regions, bool? isActive)
        {
            return ModelRiskAlertManager.GetWithFilter(idModel, regions, isActive);
        }

        public ModelRiskAlert Save(ModelRiskAlert modelRiskAlert)
        {
            return ModelRiskAlertManager.Save(modelRiskAlert);
        }

        public void Delete(ModelRiskAlert modelRiskAlert)
        {
            ModelRiskAlertManager.Delete(modelRiskAlert);
        }

        public ModelRiskAlertAttachment GetNewModelRiskAlertAttachment()
        {
            return ModelRiskAlertAttachmentManager.GetNew();
        }

        public ModelRiskAlertPhase GetNewModelRiskAlertPhase()
        {
            return ModelRiskAlertPhaseManager.GetNew();
        }

        public void SavePhase(ModelRiskAlertPhase modelRiskAlertPhase)
        {
            ModelRiskAlertPhaseManager.Save(modelRiskAlertPhase);
        }

        public void SaveRegion(ModelRiskAlertRegion modelRiskAlertRegion)
        {
            ModelRiskAlertRegionManager.Save(modelRiskAlertRegion);
        }

        public void SaveAttachment(ModelRiskAlertAttachment modelRiskAlertAttachment)
        {
            ModelRiskAlertAttachmentManager.Save(modelRiskAlertAttachment);
        }

        public ModelRiskAlertRegion GetNewModelRiskAlertRegion()
        {
            return ModelRiskAlertRegionManager.GetNew();
        }

        public List<Region> GetAllRegionsWithAlerts()
        {
            return ModelRiskAlertManager.GetAllRegionsWithAlerts();
        }

        public ModelRiskAlert Get(int idModelRiskAlert)
        {
            return ModelRiskAlertManager.Get(idModelRiskAlert);
        }

        public List<Region> GetRegions(ModelRiskAlert modelRiskAlert)
        {
            return ModelRiskAlertManager.GetRegions(modelRiskAlert);
        }

        public List<ModelRiskAlertPhase> GetPhases(ModelRiskAlert modelRiskAlert)
        {
            return ModelRiskAlertManager.GetPhases(modelRiskAlert);
        }

        public List<ModelRiskAlertRegion> GetModelRiskAlertRegions(ModelRiskAlert modelRiskAlert)
        {
            return ModelRiskAlertRegionManager.GetByModelRiskAlertID(modelRiskAlert.IDModelRiskAlert);
        }

        public List<ModelRiskAlertAttachment> GetModelRiskAlertAttachments(ModelRiskAlert modelRiskAlert)
        {
            return ModelRiskAlertAttachmentManager.GetByModelRiskAlertID(modelRiskAlert.IDModelRiskAlert);
        }

        public void DeletePhase(ModelRiskAlertPhase modelRiskAlertPhase)
        {
            ModelRiskAlertPhaseManager.Delete(modelRiskAlertPhase);
        }

        public void DeleteRegion(ModelRiskAlertRegion modelRiskAlertRegion)
        {
            ModelRiskAlertRegionManager.Delete(modelRiskAlertRegion);
        }

        public void DeleteAttachment(ModelRiskAlertAttachment modelRiskAlertAttachment)
        {
            ModelRiskAlertAttachmentManager.Delete(modelRiskAlertAttachment);
        }
    }
}
