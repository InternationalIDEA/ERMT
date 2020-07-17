using System;
using System.Windows.Forms;
using Idea.Entities;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class UserChangePassword : Form
    {
        public UserChangePassword()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != string.Empty && txtConfirm.Text != string.Empty)
            {
                if (txtPassword.Text == txtConfirm.Text)
                {
                    User user = ERMTSession.Instance.CurrentUser;
                    //Update password
                    user.Password = txtPassword.Text;
                    UserHelper.Save(user);
                    CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("PasswordChanged"));
                    Close();
                }
                else
                {
                    CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("PasswordNoMatch"),
                        CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.OKOnly);
                }
            }
            else
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("PasswordEmpty"),
                        CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.OKOnly);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
