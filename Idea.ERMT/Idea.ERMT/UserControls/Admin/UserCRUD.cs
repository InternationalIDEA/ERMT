using System.Collections.Generic;
using System.Windows.Forms;
using Idea.Entities;

namespace Idea.ERMT.UserControls
{
    public partial class UserCRUD : ERMTUserControl
    {
        private User _user;

        public UserCRUD()
        {
            InitializeComponent();
        }

        public bool PasswordsMatch
        {
            get { return txtConfirmPassword.Text == txtPassword.Text; }
        }

        public User User
        {
            get
            {
                if (_user == null)
                {
                    return null;
                }
                _user.Name = txtName.Text;
                _user.Lastname = txtLastName.Text;
                _user.Username = txtUsername.Text;
                _user.Password = txtPassword.Text;
                _user.Email = txtEmail.Text;
                _user.IDRole = cbRole.SelectedIndex < 0 ? 0 : int.Parse(cbRole.SelectedValue.ToString());
                return _user;
            }
            set
            {
                _user = value;
                if (value != null)
                {
                    txtName.Text = _user.Name;
                    txtLastName.Text = _user.Lastname;
                    txtUsername.Text = _user.Username;
                    txtPassword.Text = _user.Password;
                    txtConfirmPassword.Text = _user.Password;
                    txtEmail.Text = _user.Email;
                    cbRole.SelectedValue = _user.IDRole;
                }
                else
                {
                    txtName.Text = string.Empty;
                    txtLastName.Text = string.Empty;
                    txtUsername.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtConfirmPassword.Text = string.Empty;
                    cbRole.SelectedIndex = -1;
                }
            }
        }

        public void LoadCombo(List<Role> roles)
        {           

            cbRole.DisplayMember = "Description";
            cbRole.ValueMember = "IDRole";
            cbRole.DataSource = roles;            
        }
    }
}
