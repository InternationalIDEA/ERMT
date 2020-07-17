using System;
using System.Collections.Generic;

namespace Idea.Interfaces
{
    public interface IModelFactor
    {
        int IDModelFactor { get; set; }
        int IDModel { get; set; }
        int IDFactor { get; set; }
        int ScaleMin { get; set; }
        int ScaleMax { get; set; }
        decimal Interval { get; set; }
        int Weight { get; set; }
        int? SortOrder { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        IFactor Factor { get; set; }
        IModel Model { get; set; }
        ICollection<IModelFactorData> ModelFactorDatas { get; set; }
    }
}
