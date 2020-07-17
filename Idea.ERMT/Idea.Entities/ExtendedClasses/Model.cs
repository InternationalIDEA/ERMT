using System;

namespace Idea.Entities
{
    public partial class Model
    {
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
           }
    }
}
