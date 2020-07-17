using System;

namespace Idea.Entities
{
    public partial class Region
    {
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}


