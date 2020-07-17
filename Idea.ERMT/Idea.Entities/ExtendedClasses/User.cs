using System;

namespace Idea.Entities
{
    public partial class User
    {
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
         }
    }
}
