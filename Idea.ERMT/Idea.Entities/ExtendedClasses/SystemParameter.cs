using System;

namespace Idea.Entities
{
    public partial class SystemParameter 
    {
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
         }
    }
}
