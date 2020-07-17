using System;
using System.Collections.Generic;
using System.Linq;
using Idea.Entities;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class UserModify : ERMTUserControl
    {
        public User User
        {
            set
            {
                userCRUD.User = value;
            }
        }

        public UserModify()
        {
            LoadCombos();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateScreen()) return;

            try
            {
                User user = userCRUD.User;
                UserHelper.Validate(user);
                if ((from u in UserHelper.GetAll() where (user.Username.ToLower() == u.Username.ToLower() && user.IDUser != u.IDUser) select u).Any())
                {
                    CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("UserNameAlreadyExists"),
                        CustomMessageBoxMessageType.Information, CustomMessageBoxButtonType.OKOnly);
                    return;
                }
                UserHelper.Save(user);
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("UserSavedOk"),
                    CustomMessageBoxMessageType.Information, CustomMessageBoxButtonType.OKOnly);
                Clear();
                LoadUserCombo();
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowError(ex.Message);
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
            cbUsers.SelectedIndex = -1;
            userCRUD.User = null;
        }

        public override void ShowTitle()
        {
            ViewManager.ShowTitle(ResourceHelper.GetResourceText("UserModifyTitle"));
        }

        public void LoadCombos()
        {
            LoadUserCombo();
            LoadRoleCombo();
        }

        public void LoadUserCombo()
        {
            cbUsers.DisplayMember = "Username";
            cbUsers.ValueMember = "IDUser";
            List<User> users = UserHelper.GetAll();
            if (ERMTSession.Instance.CurrentUser.IDRole == 2)
            {
                users = (from a in users where a.IDRole > 1 select a).ToList();
            }

            cbUsers.DataSource = users;
        }

        private void LoadRoleCombo()
        {
            List<Role> roles = RoleHelper.GetAll();
            if (ERMTSession.Instance.CurrentUser.IDRole == 2)
            {
                roles = (from a in roles where a.IDRole != 1 select a).ToList();
            }

            userCRUD.LoadCombo(roles);
        }

        private void cbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbUsers.SelectedIndex >= 0)
            {
                userCRUD.User = (User)cbUsers.SelectedItem;
            }
            else
            {
                userCRUD.User = null;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (
                    CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ConfirmDeleteUser"),
                        CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.YesNo) ==
                    CustomMessageBoxReturnValue.Cancel)
                {
                    return;
                }

                UserHelper.Delete((User)cbUsers.SelectedItem);
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("UserDeletedOk"));
                ViewManager.ShowStart();
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowError(ex.Message);
            }
        }
    }
}
