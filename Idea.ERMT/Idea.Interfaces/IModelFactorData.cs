using System;

namespace Idea.Interfaces
{
    public interface IModelFactorData
    {
        int IDModelFactorData { get; set; }
        int IDModelFactor { get; set; }
        int IDRegion { get; set; }
        decimal Data { get; set; }
        DateTime Date { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        IModelFactor ModelFactor { get; set; }
        IRegion Region { get; set; }
    }
}
