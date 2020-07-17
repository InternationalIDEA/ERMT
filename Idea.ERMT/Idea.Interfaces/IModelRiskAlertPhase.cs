using System;

namespace Idea.Interfaces
{
    public interface IModelRiskAlertPhase
    {
        int IDModelRiskAlertPhase { get; set; }
        int IDModelRiskAlert { get; set; }
        int IDPhase { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        IModelRiskAlert ModelRiskAlert { get; set; }
        IPhase Phase { get; set; }
    }
}
