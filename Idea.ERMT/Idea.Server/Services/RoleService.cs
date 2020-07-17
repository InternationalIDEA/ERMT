using System.Collections.Generic;
using Idea.Business;
using Idea.Entities;

namespace Idea.Server
{
    public class RoleService : IRoleService
    {
        public List<Role> GetAll()
        {
            return (RoleManager.GetAll());
        }
    }
}
