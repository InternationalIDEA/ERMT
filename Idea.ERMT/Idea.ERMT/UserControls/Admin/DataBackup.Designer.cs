namespace Idea.ERMT.UserControls
{
    partial class DataBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataBackup));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblBackup = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkBackupDataBase = new System.Windows.Forms.CheckBox();
            this.chkBackupFiles = new System.Windows.Forms.CheckBox();
            this.chkBackupShapefiles = new System.Windows.Forms.CheckBox();
            this.lblBackupInfo = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lblBackup, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.chkBackupDataBase, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.chkBackupFiles, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.chkBackupShapefiles, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblBackupInfo, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lblBackup
            // 
            resources.ApplyResources(this.lblBackup, "lblBackup");
            this.lblBackup.Name = "lblBackup";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnOK, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkBackupDataBase
            // 
            resources.ApplyResources(this.chkBackupDataBase, "chkBackupDataBase");
            this.chkBackupDataBase.Name = "chkBackupDataBase";
            this.chkBackupDataBase.UseVisualStyleBackColor = true;
            // 
            // chkBackupFiles
            // 
            resources.ApplyResources(this.chkBackupFiles, "chkBackupFiles");
            this.chkBackupFiles.Name = "chkBackupFiles";
            this.chkBackupFiles.UseVisualStyleBackColor = true;
            // 
            // chkBackupShapefiles
            // 
            resources.ApplyResources(this.chkBackupShapefiles, "chkBackupShapefiles");
            this.chkBackupShapefiles.Name = "chkBackupShapefiles";
            this.chkBackupShapefiles.UseVisualStyleBackColor = true;
            // 
            // lblBackupInfo
            // 
            resources.ApplyResources(this.lblBackupInfo, "lblBackupInfo");
            this.lblBackupInfo.Name = "lblBackupInfo";
            // 
            // DataBackup
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DataBackup";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblBackup;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkBackupDataBase;
        private System.Windows.Forms.CheckBox chkBackupFiles;
        private System.Windows.Forms.CheckBox chkBackupShapefiles;
        private System.Windows.Forms.Label lblBackupInfo;
    }
}
