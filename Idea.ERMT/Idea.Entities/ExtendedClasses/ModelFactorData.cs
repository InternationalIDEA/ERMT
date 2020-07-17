using System;

namespace Idea.Entities
{
    public partial class ModelFactorData
    {
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}
