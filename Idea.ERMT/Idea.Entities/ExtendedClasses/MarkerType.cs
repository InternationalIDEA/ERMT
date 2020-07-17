using System;

namespace Idea.Entities
{
    public partial class MarkerType
    {
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
           }
    }
}
