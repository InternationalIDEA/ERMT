using System;

namespace Idea.Entities
{
    public partial class Factor
    {
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}
