using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Idea.DAL;
using Idea.Entities;
using System.Linq;


namespace Idea.Business
{
    public static class UserManager
    {   
        /// <summary>
        /// Returns a new User.
        /// </summary>
        /// <returns></returns>
        public static User GetNewUser()
        {
            return new User();
        }

        /// <summary>
        /// Verify that the username and password are correct , but returns null.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static User Login(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                return null;
            }

            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            }
        }

        /// <summary>
        /// Gets and Returns User by idUserManager.
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public static User GetById(int idUser)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Users.FirstOrDefault(u => u.IDUser == idUser);
            }
         }

        /// <summary>
        /// Saves a new User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static User Save(User user)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                context.Users.AddOrUpdate(user);
                context.SaveChanges();
                return user;
            }
         }

        /// <summary>
        /// Validates the user fields.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool ValidateRequiredFields(User user)
        {
            if (string.IsNullOrEmpty(user.Name))
                throw new ArgumentException("UserNameEmpty");
            if (string.IsNullOrEmpty(user.Lastname))
                throw new ArgumentException("UserLastNameEmpty");
            if (string.IsNullOrEmpty(user.Username))
                throw new ArgumentException("UserCannotEmpty");
            if (string.IsNullOrEmpty(user.Password))
                throw new ArgumentException("PasswordCannotEmpty");
            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException("EmailCannotEmpty");
            if (user.IDRole == 0)
                throw new ArgumentException("SelectRole");
            return true;
        }

        /// <summary>
        /// Returns the list of all Users.
        /// </summary>
        /// <returns></returns>
        public static List<User> GetAll()
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Users.ToList();
            }
        }

        /// <summary>
        /// Deletes a User by user name.
        /// </summary>
        /// <param name="user"></param>
        public static void Delete(User user)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                User u = context.Users.FirstOrDefault(u2 => u2.IDUser == user.IDUser);
                context.Users.Remove(u);
                context.SaveChanges();
            }
        }
    }
}
