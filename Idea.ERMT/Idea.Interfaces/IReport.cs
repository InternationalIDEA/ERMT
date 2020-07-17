using System;

namespace Idea.Interfaces
{
    public interface IReport
    {
        int IDReport { get; set; }
        int IDModel { get; set; }
        string Name { get; set; }
        string Parameters { get; set; }
        string Markers { get; set; }
        int Type { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        IModel Model { get; set; }
    }
}
