using System;

namespace Idea.Entities
{
    public partial class ModelRiskAlertRegion
    {
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        public bool Deleted { get; set; }
    }
}
