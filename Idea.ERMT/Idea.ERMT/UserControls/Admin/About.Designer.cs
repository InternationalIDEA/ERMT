namespace Idea.ERMT.UserControls
{
    partial class About
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblApplicationName = new System.Windows.Forms.Label();
            this.lblApplicationVersion = new System.Windows.Forms.Label();
            this.lblIDEAWebsiteLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblApplicationName
            // 
            this.lblApplicationName.AutoSize = true;
            this.lblApplicationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lblApplicationName.Location = new System.Drawing.Point(11, 20);
            this.lblApplicationName.Name = "lblApplicationName";
            this.lblApplicationName.Size = new System.Drawing.Size(287, 24);
            this.lblApplicationName.TabIndex = 0;
            this.lblApplicationName.Text = "Electoral Risk Management Tool ";
            // 
            // lblApplicationVersion
            // 
            this.lblApplicationVersion.AutoSize = true;
            this.lblApplicationVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblApplicationVersion.Location = new System.Drawing.Point(103, 66);
            this.lblApplicationVersion.Name = "lblApplicationVersion";
            this.lblApplicationVersion.Size = new System.Drawing.Size(102, 20);
            this.lblApplicationVersion.TabIndex = 1;
            this.lblApplicationVersion.Text = "Version 7.0.0";
            // 
            // lblIDEAWebsiteLink
            // 
            this.lblIDEAWebsiteLink.AutoSize = true;
            this.lblIDEAWebsiteLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblIDEAWebsiteLink.Location = new System.Drawing.Point(109, 106);
            this.lblIDEAWebsiteLink.Name = "lblIDEAWebsiteLink";
            this.lblIDEAWebsiteLink.Size = new System.Drawing.Size(91, 18);
            this.lblIDEAWebsiteLink.TabIndex = 2;
            this.lblIDEAWebsiteLink.TabStop = true;
            this.lblIDEAWebsiteLink.Text = "www.idea.int";
            this.lblIDEAWebsiteLink.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lblIDEAWebsiteLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblIDEAWebsiteLink_LinkClicked);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 157);
            this.Controls.Add(this.lblIDEAWebsiteLink);
            this.Controls.Add(this.lblApplicationVersion);
            this.Controls.Add(this.lblApplicationName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblApplicationName;
        private System.Windows.Forms.Label lblApplicationVersion;
        private System.Windows.Forms.LinkLabel lblIDEAWebsiteLink;
    }
}
