using System.Collections.Generic;
using System.Linq;
using Idea.Business;
using Idea.Entities;

namespace Idea.Server
{
    public class UserService : IUserService
    {
        public User Login(string username, string password)
        {
            return UserManager.Login(username, password);
        }

        public void Save(User user)
        {
            UserManager.Save((user));
        }

        public bool Validate(User user)
        {
            return UserManager.ValidateRequiredFields((user));
        }

        public List<User> GetAll()
        {
            return (UserManager.GetAll()).ToList();
        }

        public void Delete(User user)
        {
            UserManager.Delete((user));
        }

        public User GetByIDUser(int idUser)
        {
            return UserManager.GetById(idUser);
        }
    }
}
