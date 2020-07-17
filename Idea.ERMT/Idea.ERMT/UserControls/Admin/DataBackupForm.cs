using System.Windows.Forms;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class DataBackupForm : Form
    {
        public DataBackupForm()
        {
            InitializeComponent();
            dataBackup1.OnBackupCompleted += dataBackup1_OnBackupCompleted;
        }

        void dataBackup1_OnBackupCompleted(object sender, System.EventArgs e)
        {
            LoadingForm.Fadeout();
            if (CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("BackupSaved")) ==
                CustomMessageBoxReturnValue.Ok)
            {
                Close();
            }
        }
    }
}
