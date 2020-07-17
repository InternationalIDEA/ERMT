using System;
using System.Collections.Generic;

namespace Idea.Interfaces
{
    public interface IMarkerType
    {
        int IDMarkerType { get; set; }
        string Name { get; set; }
        string Symbol { get; set; }
        string Size { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        ICollection<IMarker> Markers { get; set; }
    }
}
