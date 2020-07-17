using System;
using System.Windows.Forms;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class ServerSettings : Form
    {
        public ServerSettings()
        {
            InitializeComponent();
            Text = ResourceHelper.GetResourceText("ServerTitle");
        }

        private void ServerSettings_Load(object sender, EventArgs e)
        {
            txtEndPointAddress.Text = ConfigurationSettingsHelper.GetEndpointAddress();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Save settings;
            ConfigurationSettingsHelper.SaveEndpointAddress(txtEndPointAddress.Text);
            DialogResult = DialogResult.OK;

            CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("IPChanged"));
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
