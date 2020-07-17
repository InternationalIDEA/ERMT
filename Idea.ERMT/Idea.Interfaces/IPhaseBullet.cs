using System;

namespace Idea.Interfaces
{
    public interface IPhaseBullet
    {
        int IDPhaseBullet { get; set; }
        int IDPhase { get; set; }
        int ColumnNumber { get; set; }
        string Text { get; set; }
        int SortOrder { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        IPhase Phase { get; set; }
    }
}
