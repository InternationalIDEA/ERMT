namespace Idea.ERMT.UserControls
{
    partial class DataRestoreForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataRestoreForm));
            this.dataRestore1 = new Idea.ERMT.UserControls.DataRestore();
            this.SuspendLayout();
            // 
            // dataRestore1
            // 
            this.dataRestore1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataRestore1.Location = new System.Drawing.Point(0, 0);
            this.dataRestore1.Name = "dataRestore1";
            this.dataRestore1.Size = new System.Drawing.Size(208, 224);
            this.dataRestore1.TabIndex = 0;
            // 
            // DataRestoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 224);
            this.Controls.Add(this.dataRestore1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataRestoreForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Restore";
            this.ResumeLayout(false);

        }

        #endregion

        private DataRestore dataRestore1;
    }
}