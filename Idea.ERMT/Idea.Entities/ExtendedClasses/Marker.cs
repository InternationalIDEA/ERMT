using System;

namespace Idea.Entities
{
    public partial class Marker
    {
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
          }
    }
}
