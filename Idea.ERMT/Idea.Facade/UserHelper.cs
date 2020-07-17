using System.Collections.Generic;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Xml;
using Idea.Entities;
using Idea.Utils;

namespace Idea.Facade
{
    public class UserHelper
    {
        private static UserService.IUserService _service;
        private static UserService.IUserService GetService()
        {
            if (_service == null)
            {
                _service = new UserService.UserServiceClient();
                Uri uri = new Uri(((ClientBase<UserService.IUserService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                ((ClientBase<UserService.IUserService>)_service).Endpoint.Address = new EndpointAddress(uri);
            }
            else
            {
                try
                {
                    ((ClientBase<UserService.IUserService>)(_service)).Close();
                }
                catch
                { }
                finally
                {
                    _service = new UserService.UserServiceClient();
                    Uri uri = new Uri(((ClientBase<UserService.IUserService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                    ((ClientBase<UserService.IUserService>)_service).Endpoint.Address = new EndpointAddress(uri);
                }

            }
            switch (((ClientBase<UserService.IUserService>)(_service)).State)
            {
                case CommunicationState.Faulted:
                    ((ClientBase<UserService.IUserService>)(_service)).Abort();
                    _service = new UserService.UserServiceClient();
                    break;
                case CommunicationState.Closed:
                case CommunicationState.Closing:
                    _service = new UserService.UserServiceClient();
                    break;
            }
            return _service;

        }

        /// <summary>
        /// Returns a new User (service).
        /// </summary>
        /// <returns></returns>
        public static User GetNew()
        {
            return new User();
        }

        /// <summary>
        /// Check the username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool Login(string username, string password, out User user)
        {
            User obj = GetService().Login(username, password);
            user = obj;
            return (obj != null);
        }

        /// <summary>
        /// Validate the User by user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool Validate(User user)
        {
            if (string.IsNullOrEmpty(user.Name))
                throw new ArgumentException(ResourceHelper.GetResourceText("UserNameEmpty"));
            if (string.IsNullOrEmpty(user.Lastname))
                throw new ArgumentException(ResourceHelper.GetResourceText("UserLastNameEmpty"));
            if (string.IsNullOrEmpty(user.Username))
                throw new ArgumentException(ResourceHelper.GetResourceText("UserCannotEmpty"));
            if (string.IsNullOrEmpty(user.Password))
                throw new ArgumentException(ResourceHelper.GetResourceText("PasswordCannotEmpty"));
            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException(ResourceHelper.GetResourceText("EmailCannotEmpty"));
            if (user.IDRole == 0)
                throw new ArgumentException(ResourceHelper.GetResourceText("SelectRole"));
            return true;
        }

        /// <summary>
        /// Saves the User (service).
        /// </summary>
        /// <param name="user"></param>
        public static void Save(User user)
        {
            GetService().Save((user));
        }

        /// <summary>
        /// Returns the list of all Users (service).
        /// </summary>
        /// <returns></returns>
        public static List<User> GetAll()
        {
            return (GetService().GetAll()).ToList();
        }

        /// <summary>
        /// Deletes the User (service).
        /// </summary>
        /// <param name="user"></param>
        public static void Delete(User user)
        {
            GetService().Delete((user));
        }

        public static User GetByIDUser(int idUser)
        {
            return GetService().GetByIDUser(idUser);
        }

        public static User GetLoggedUser()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                String configFileName = Utils.DirectoryAndFileHelper.SignedInUserFile;
                if (File.Exists(configFileName))
                {
                    doc.Load(configFileName);
                    RC4Engine encrytionEngine = new RC4Engine();
                    encrytionEngine.EncryptionKey = "1deageo!";
                    encrytionEngine.CryptedText = doc.DocumentElement.Attributes["user"].Value;
                    if (!encrytionEngine.Decrypt())
                    {
                        return null;
                    }
                    if (encrytionEngine.InClearText == "-1")
                    {
                        return null;
                    }
                    return GetByIDUser(Convert.ToInt32(encrytionEngine.InClearText));
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void SignOutUser()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                String userInfoFileName = Utils.DirectoryAndFileHelper.SignedInUserFile;
                if (File.Exists(userInfoFileName))
                {
                    doc.Load(userInfoFileName);
                    RC4Engine encryptionEngine = new RC4Engine();
                    encryptionEngine.EncryptionKey = "1deageo!";
                    encryptionEngine.InClearText = "-1";
                    if (!encryptionEngine.Encrypt())
                    {
                        return;
                    }
                    doc.DocumentElement.Attributes["user"].Value = encryptionEngine.CryptedText;
                    doc.Save(userInfoFileName);
                }
                ERMTSession.Instance.LogoutUser();
            }
            catch (Exception)
            {
            }
        }
    }
}
