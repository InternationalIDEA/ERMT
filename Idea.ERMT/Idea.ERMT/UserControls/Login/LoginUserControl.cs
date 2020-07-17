using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Idea.Entities;
using Idea.Facade;
using Idea.Utils;

namespace Idea.ERMT.UserControls
{
    public partial class LoginUserControl : ERMTUserControl
    {
        private void btnLogin_Click(object sender, EventArgs e)
        {
#if DEBUG
            if (txtUserName.Text == string.Empty)
            {
                txtUserName.Text = "admin";
                txtPassword.Text = "123456";
                LoginUser();
            }
            else
            {
                LoginUser();
            }
#else
            LoginUser();
#endif


        }

        private void LoginUser()
        {
            User user;
            UserHelper.Login(txtUserName.Text, txtPassword.Text, out user);

            if (user != null)
            {
                ERMTSession.Instance.LoginUser(user);
                ViewManager.LoadApplicationInitialState();
                ViewManager.ShowStart();
                if (chkStaySignedIn.Checked)
                {
                    CacheCurrentUser();
                }
            }
            else
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("LoginFailed"),
                    CustomMessageBoxMessageType.Error,CustomMessageBoxButtonType.OKOnly);
            }
        }

        private void CacheCurrentUser()
        {
            String userInfoFileName = DirectoryAndFileHelper.SignedInUserFile;
            XmlDocument doc = new XmlDocument();
            RC4Engine encryptionEngine = new RC4Engine();
            encryptionEngine.EncryptionKey = "1deageo!";
            encryptionEngine.InClearText = ERMTSession.Instance.CurrentUser.IDUser.ToString();
            if (!encryptionEngine.Encrypt())
            {
                return;
            }

            if (File.Exists(userInfoFileName))
            {
                doc.Load(userInfoFileName);
                doc.DocumentElement.Attributes["user"].Value = encryptionEngine.CryptedText;
            }
            else
            {
                doc.LoadXml("<Language user=\"" + encryptionEngine.CryptedText + "\" />");
            }

            doc.Save(userInfoFileName);
        }

        public override void Clear()
        {
            base.Clear();
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }

        private void txtUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPassword.Focus();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                LoginUser();
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  
                return cp;
            }
        }

        private void LoginUserControl_Load(object sender, EventArgs e)
        {
            User loggedInUser = UserHelper.GetLoggedUser();
            if (loggedInUser != null)
            {
                ERMTSession.Instance.LoginUser(loggedInUser);
                ViewManager.ShowStart();
                ViewManager.LoadApplicationInitialState();
            }
        } 
    }
}
