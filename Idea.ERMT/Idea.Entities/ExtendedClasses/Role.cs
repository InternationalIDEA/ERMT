using System;

namespace Idea.Entities
{
    public partial class Role
    {
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
         }
    }
}
