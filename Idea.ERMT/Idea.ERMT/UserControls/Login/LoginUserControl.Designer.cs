namespace Idea.ERMT.UserControls
{
    partial class LoginUserControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        protected override void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginUserControl));
            this.tlpLogin = new System.Windows.Forms.TableLayoutPanel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblPassword = new System.Windows.Forms.Label();
            this.chkStaySignedIn = new System.Windows.Forms.CheckBox();
            this.tlpLogin.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpLogin
            // 
            resources.ApplyResources(this.tlpLogin, "tlpLogin");
            this.tlpLogin.BackColor = System.Drawing.SystemColors.Control;
            this.tlpLogin.Controls.Add(this.btnLogin, 2, 5);
            this.tlpLogin.Controls.Add(this.txtUserName, 2, 2);
            this.tlpLogin.Controls.Add(this.txtPassword, 2, 3);
            this.tlpLogin.Controls.Add(this.panel1, 0, 2);
            this.tlpLogin.Controls.Add(this.panel2, 0, 3);
            this.tlpLogin.Controls.Add(this.chkStaySignedIn, 2, 4);
            this.tlpLogin.Name = "tlpLogin";
            // 
            // btnLogin
            // 
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtUserName
            // 
            resources.ApplyResources(this.txtUserName, "txtUserName");
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyUp);
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.tlpLogin.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.lblUserName);
            this.panel1.Name = "panel1";
            // 
            // lblUserName
            // 
            resources.ApplyResources(this.lblUserName, "lblUserName");
            this.lblUserName.Name = "lblUserName";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.tlpLogin.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.lblPassword);
            this.panel2.Name = "panel2";
            // 
            // lblPassword
            // 
            resources.ApplyResources(this.lblPassword, "lblPassword");
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Name = "lblPassword";
            // 
            // chkStaySignedIn
            // 
            resources.ApplyResources(this.chkStaySignedIn, "chkStaySignedIn");
            this.chkStaySignedIn.Name = "chkStaySignedIn";
            this.chkStaySignedIn.UseVisualStyleBackColor = true;
            // 
            // LoginUserControl
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tlpLogin);
            this.Name = "LoginUserControl";
            this.Load += new System.EventHandler(this.LoginUserControl_Load);
            this.tlpLogin.ResumeLayout(false);
            this.tlpLogin.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TableLayoutPanel tlpLogin;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkStaySignedIn;
    }
}
