namespace Idea.ERMT.UserControls
{
    partial class DataBackupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataBackupForm));
            this.dataBackup1 = new Idea.ERMT.UserControls.DataBackup();
            this.SuspendLayout();
            // 
            // dataBackup1
            // 
            resources.ApplyResources(this.dataBackup1, "dataBackup1");
            this.dataBackup1.Name = "dataBackup1";
            // 
            // DataBackupForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataBackup1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataBackupForm";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private DataBackup dataBackup1;
    }
}