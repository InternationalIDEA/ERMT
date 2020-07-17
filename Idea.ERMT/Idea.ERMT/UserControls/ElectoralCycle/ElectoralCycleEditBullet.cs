using System;
using System.Windows.Forms;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class ElectoralCycleEditBullet : Form
    {
        // property for the text to display
        public string BulletText
        {
            get
            {
                return txtBullet.Text;
            }
            set
            {
                txtBullet.Text = value;
            }

        } //ImageText


        public ElectoralCycleEditBullet()
        {
            InitializeComponent();
            txtBullet.Focus();
        }

        private void EditBullet_Load(object sender, EventArgs e)
        {
            txtBullet.Focus();
        }

        private void EditBullet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (txtBullet.Text.Trim() == string.Empty)
                {
                    // Avoid close:
                    MessageBox.Show(ResourceHelper.GetResourceText("ElectoralCycleBulletText"), ResourceHelper.GetResourceText("PhaseBullet"));
                    txtBullet.Focus();
                    e.Cancel = true;
                }
                else
                {
                    // Closes dialog with OK message
                }
            }
            else if (DialogResult == DialogResult.Cancel)
            {
                // Closes dialog with CANCEL message
            }
        }


    }
}
