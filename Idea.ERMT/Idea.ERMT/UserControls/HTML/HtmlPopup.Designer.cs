namespace Idea.ERMT.UserControls
{
    partial class HtmlPopup
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
        private void InitializeComponent()
        {
            this.indexUserControl1 = new Idea.ERMT.UserControls.IndexUserControl();
            this.SuspendLayout();
            // 
            // indexUserControl1
            // 
            this.indexUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.indexUserControl1.Location = new System.Drawing.Point(0, 0);
            this.indexUserControl1.Name = "indexUserControl1";
            this.indexUserControl1.Size = new System.Drawing.Size(960, 470);
            this.indexUserControl1.TabIndex = 0;
            // 
            // HtmlPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 470);
            this.Controls.Add(this.indexUserControl1);
            this.Name = "HtmlPopup";
            this.Text = "HtmlPopup";
            this.Load += new System.EventHandler(this.HtmlPopup_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Idea.ERMT.UserControls.IndexUserControl indexUserControl1;

    }
}