using System;

namespace Idea.Entities
{
    public partial class Report
    {
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
         }
    }
}
