using System;

namespace Idea.Interfaces
{
    public interface IUser
    {
        int IDUser { get; set; }
        int IDRole { get; set; }
        string Name { get; set; }
        string Lastname { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        IRole Role { get; set; }
    }
}
