using System;

namespace Idea.Interfaces
{
    public interface IModelRiskAlertAttachment
    {
        int IDModelRiskAlertAttachment { get; set; }
        int IDModelRiskAlert { get; set; }
        string AttachmentFile { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        IModelRiskAlert ModelRiskAlert { get; set; }
    }
}
