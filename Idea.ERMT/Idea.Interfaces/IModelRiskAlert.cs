using System;
using System.Collections.Generic;

namespace Idea.Interfaces
{
    public interface IModelRiskAlert
    {
        int IDModelRiskAlert { get; set; }
        int IDModel { get; set; }
        string Code { get; set; }
        string Title { get; set; }
        DateTime DateFrom { get; set; }
        DateTime DateTo { get; set; }
        string RiskDescription { get; set; }
        string Action { get; set; }
        string Result { get; set; }
        bool Active { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        IModel Model { get; set; }
        ICollection<IModelRiskAlertAttachment> ModelRiskAlertAttachments { get; set; }
        ICollection<IModelRiskAlertPhase> ModelRiskAlertPhases { get; set; }
        ICollection<IModelRiskAlertRegion> ModelRiskAlertRegions { get; set; }
    }
}
