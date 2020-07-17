using System;
using System.Collections.Generic;
using Idea.Entities;
using Idea.DAL;
using System.Linq;


namespace Idea.Business
{
    public class RoleManager
    {
        /// <summary>
        /// Returns a new Role.
        /// </summary>
        /// <returns></returns>
        public static Role GetNew()
        {
            return new Role();
        }

        /// <summary>
        /// Returns the list of all the Roles.
        /// </summary>
        /// <returns></returns>
        public static List<Role> GetAll()
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (from o in context.Roles select o).ToList();
            }
        }
 }
}
