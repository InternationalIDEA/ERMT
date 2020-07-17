using System.Windows.Forms;

namespace Idea.ERMT.UserControls
{
    partial class MessageControl
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
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.lblInformation = new System.Windows.Forms.Label();
            this.gbInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbInfo
            // 
            this.gbInfo.BackColor = System.Drawing.Color.GhostWhite;
            this.gbInfo.Controls.Add(this.lblInformation);
            this.gbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbInfo.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbInfo.Location = new System.Drawing.Point(0, 0);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(362, 257);
            this.gbInfo.TabIndex = 0;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Information";
            // 
            // lblInformation
            // 
            this.lblInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInformation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInformation.Font = new System.Drawing.Font("Arial Narrow", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformation.Location = new System.Drawing.Point(3, 28);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(356, 226);
            this.lblInformation.TabIndex = 0;
            this.lblInformation.Text = "Information";
            // 
            // MessageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbInfo);
            this.Name = "MessageControl";
            this.Size = new System.Drawing.Size(362, 257);
            this.Load += new System.EventHandler(this.MessageControl_Load);
            this.gbInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox gbInfo;
        private Label lblInformation;

    }
}
