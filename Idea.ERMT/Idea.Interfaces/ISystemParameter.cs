using System;

namespace Idea.Interfaces
{
    public interface ISystemParameter
    {
        int IDSystemParameter { get; set; }
        int LastRiskCode { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
    }
}
