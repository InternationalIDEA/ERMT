using System;

namespace Idea.Entities
{
    public partial class PhaseBullet
    {
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
          }
    }
}
