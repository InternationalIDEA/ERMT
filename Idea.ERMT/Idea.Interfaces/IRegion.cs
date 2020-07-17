using System;
using System.Collections.Generic;

namespace Idea.Interfaces
{
    public interface IRegion
    {
        int IDRegion { get; set; }
        string RegionName { get; set; }
        string RegionDescription { get; set; }
        int? IDParent { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        ICollection<IModel> Models { get; set; }
        ICollection<IModelFactorData> ModelFactorDatas { get; set; }
        ICollection<IModelRiskAlertRegion> ModelRiskAlertRegions { get; set; }
    }
}
