using System.Diagnostics;
using System.Windows.Forms;
using Idea.Facade;
using Idea.Utils;

namespace Idea.ERMT.UserControls
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            lblApplicationVersion.Text = ResourceHelper.GetResourceText("Version") + AppVersion.Version.ToString(3);
            Text = ResourceHelper.GetResourceText("About");
        }

        private void lblIDEAWebsiteLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(lblIDEAWebsiteLink.Text);
        }
    }
}
