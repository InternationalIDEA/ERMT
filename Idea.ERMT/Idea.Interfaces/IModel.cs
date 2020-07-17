using System;
using System.Collections.Generic;

namespace Idea.Interfaces
{
    public interface IModel
    {
        int IDModel { get; set; }
        int IDRegion { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        ICollection<IMarker> Markers { get; set; }
        IRegion Region { get; set; }
        ICollection<IModelFactor> ModelFactors { get; set; }
        ICollection<IModelRiskAlert> ModelRiskAlerts { get; set; }
        ICollection<IReport> Reports { get; set; }
    }
}
