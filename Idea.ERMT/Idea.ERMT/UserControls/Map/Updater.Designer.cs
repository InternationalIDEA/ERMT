namespace Idea.ERMT
{
    partial class Updater
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lbDownload = new System.Windows.Forms.LinkLabel();
            this.btUpload = new System.Windows.Forms.Button();
            this.pnlDownload = new System.Windows.Forms.TableLayoutPanel();
            this.lbDelete = new System.Windows.Forms.LinkLabel();
            this.pnlDownload.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lbDownload
            // 
            this.lbDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDownload.Location = new System.Drawing.Point(3, 0);
            this.lbDownload.Name = "lbDownload";
            this.lbDownload.Size = new System.Drawing.Size(441, 260);
            this.lbDownload.TabIndex = 0;
            this.lbDownload.TabStop = true;
            this.lbDownload.Text = "document1.jpg";
            this.lbDownload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbDownload_LinkClicked);
            // 
            // btUpload
            // 
            this.btUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btUpload.Location = new System.Drawing.Point(0, 0);
            this.btUpload.Name = "btUpload";
            this.btUpload.Size = new System.Drawing.Size(467, 260);
            this.btUpload.TabIndex = 1;
            this.btUpload.Text = "Upload File";
            this.btUpload.UseVisualStyleBackColor = true;
            this.btUpload.Click += new System.EventHandler(this.btUpload_Click);
            // 
            // pnlDownload
            // 
            this.pnlDownload.ColumnCount = 2;
            this.pnlDownload.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlDownload.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlDownload.Controls.Add(this.lbDelete, 1, 0);
            this.pnlDownload.Controls.Add(this.lbDownload, 0, 0);
            this.pnlDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDownload.Location = new System.Drawing.Point(0, 0);
            this.pnlDownload.Name = "pnlDownload";
            this.pnlDownload.RowCount = 1;
            this.pnlDownload.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlDownload.Size = new System.Drawing.Size(467, 260);
            this.pnlDownload.TabIndex = 2;
            // 
            // lbDelete
            // 
            this.lbDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDelete.Location = new System.Drawing.Point(450, 0);
            this.lbDelete.Name = "lbDelete";
            this.lbDelete.Size = new System.Drawing.Size(14, 260);
            this.lbDelete.TabIndex = 0;
            this.lbDelete.TabStop = true;
            this.lbDelete.Text = "X";
            this.lbDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbDelete.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbDelete_LinkClicked);
            // 
            // Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btUpload);
            this.Controls.Add(this.pnlDownload);
            this.Name = "Updater";
            this.Size = new System.Drawing.Size(467, 260);
            this.Load += new System.EventHandler(this.Updater_Load);
            this.pnlDownload.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.LinkLabel lbDownload;
        private System.Windows.Forms.Button btUpload;
        private System.Windows.Forms.TableLayoutPanel pnlDownload;
        private System.Windows.Forms.LinkLabel lbDelete;
    }
}
