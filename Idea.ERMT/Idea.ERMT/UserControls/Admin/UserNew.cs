using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Idea.Entities;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class UserNew : ERMTUserControl
    {
        public User User
        {
            set
            {
                userCRUD.User = value;
            }
        }

        public UserNew()
        {
            userCRUD.User = new User();
            LoadCombos();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateScreen())
            {
                User user = userCRUD.User;
                try
                {
                    UserHelper.Validate(user);
                    if ((from u in UserHelper.GetAll() where user.Username == u.Username select u).Any())
                    {
                        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("UserNameAlreadyExists"));
                        return;
                    }
                    UserHelper.Save(user);
                    CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("UserSavedOk"));
                    ViewManager.ShowStart();
                }
                catch (Exception ex)
                {
                    CustomMessageBox.ShowError(ex.Message);
                }
            }            

        }

        private bool ValidateScreen()
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(userCRUD.User.Name))
            {
                message += ResourceHelper.GetResourceText("UserValidateFirstName") + "\r\n";
            }
            if (string.IsNullOrEmpty(userCRUD.User.Lastname))
            {
                message += ResourceHelper.GetResourceText("UserValidateLastName") + "\r\n";
            }
            if (string.IsNullOrEmpty(userCRUD.User.Password))
            {
                message += ResourceHelper.GetResourceText("UserValidatePass") + "\r\n";
            }
            if (string.IsNullOrEmpty(userCRUD.User.Username))
            {
                message += ResourceHelper.GetResourceText("UserValidateUsername") + "\r\n";           
            }
            if (string.IsNullOrEmpty(userCRUD.User.Email))
            {
                message += ResourceHelper.GetResourceText("UserValidateEmail") + "\r\n";
            }
            if (userCRUD.User.IDRole == 0)
            {
                message += ResourceHelper.GetResourceText("UserValidateRole") + "\r\n";
            }
            if (!userCRUD.PasswordsMatch)
            {
                message += ResourceHelper.GetResourceText("UserValidatePassMatch") + "\r\n";
            }
            
            bool valid = (message.Length == 0);
            if (!valid)
            {
                CustomMessageBox.ShowMessage(message);
            }

            return valid;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ViewManager.ShowStart();
        }

        public override void Clear()
        {
            userCRUD.User = null;
        }

        public override void ShowTitle()
        {
            ViewManager.ShowTitle(ResourceHelper.GetResourceText("AddNewUser"));
        }

        public void LoadCombos()
        {
            List<Role> roles = RoleHelper.GetAll();
            if (ERMTSession.Instance.CurrentUser.IDRole == 2)
            {
                roles = (from a in roles where a.IDRole != 1 select a).ToList();
            }
            
            userCRUD.LoadCombo(roles);
        }
    }
}
