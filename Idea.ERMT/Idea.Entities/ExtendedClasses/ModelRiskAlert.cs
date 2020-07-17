using System;

namespace Idea.Entities
{
    public partial class ModelRiskAlert
    {
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
         }

        public string Deleted { get; set; }
    }
}
