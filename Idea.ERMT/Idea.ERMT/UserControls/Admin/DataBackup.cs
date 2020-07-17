using System;
using System.IO;
using System.Windows.Forms;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class DataBackup : ERMTUserControl
    {
        public event EventHandler OnBackupCompleted;

        public DataBackup()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool backupDatabase = chkBackupDataBase.Checked;
            bool backupFiles = chkBackupFiles.Checked;
            bool backupShapefiles = chkBackupShapefiles.Checked;

            if(backupDatabase == false && backupFiles == false && backupShapefiles == false)
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("MustSelectAtLeastOneOption"));
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog { Filter = "Gzipped backup files(*gz)|*.gz", Title = "Save backup files" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                btnOK.Enabled = false;

                try
                {
                    LoadingForm.ShowLoading();
                    string filename = sfd.FileName;
                    string content = DocumentHelper.Backup(backupDatabase, backupFiles, backupShapefiles);
                    File.WriteAllBytes(filename, Convert.FromBase64String(content));
                    //SetBackupVersion(filename);
                    
                    
                    if (OnBackupCompleted != null)
                    {
                        OnBackupCompleted(new object(), new EventArgs());
                    }
                }
                catch (Exception ex)
                {
                    CustomMessageBox.ShowMessage(ex.Message, CustomMessageBoxMessageType.Error, CustomMessageBoxButtonType.OKOnly);
                    LoadingForm.Fadeout();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ((DataBackupForm)this.Parent).Close();
        }
    }
}
