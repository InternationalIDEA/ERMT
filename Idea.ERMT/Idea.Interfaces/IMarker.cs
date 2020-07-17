using System;

namespace Idea.Interfaces
{
    public interface IMarker
    {
        int IDMarker { get; set; }
        int IDMarkerType { get; set; }
        int IDModel { get; set; }
        string Name { get; set; }
        decimal Latitude { get; set; }
        decimal Longitude { get; set; }
        DateTime DateFrom { get; set; }
        DateTime DateTo { get; set; }
        string Color { get; set; }
        string Description { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        IMarkerType MarkerType { get; set; }
        IModel Model { get; set; }
    }
}
