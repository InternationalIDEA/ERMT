using System;

namespace Idea.Interfaces
{
    public interface IModelRiskAlertRegion
    {
        int IDModelRiskAlertRegion { get; set; }
        int IDModelRiskAlert { get; set; }
        int IDRegion { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        IModelRiskAlert ModelRiskAlert { get; set; }
        IRegion Region { get; set; }
    }
}
