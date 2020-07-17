using System;
using System.Collections.Generic;

namespace Idea.Interfaces
{
    public interface IRole
    {
        int IDRole { get; set; }
        string Description { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        ICollection<IUser> Users { get; set; }
    }
}
