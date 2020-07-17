using System;

namespace Idea.Entities
{
    public partial class Phase
    {
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}
