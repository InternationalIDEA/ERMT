using System;

namespace Idea.Entities
{
    public partial class ModelRiskAlertPhase
    {
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        public bool Deleted { get; set; }
    }
}
