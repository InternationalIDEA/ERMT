using System.Windows.Forms;
using Idea.Facade;
using Idea.Entities;
using System;

namespace Idea.ERMT.UserControls
{
    public partial class EditRegionNameForm : Form
    {
        public event EventHandler OnRegionNameChanged; 
        public Region RegionToChange { get; set; }
        public EditRegionNameForm()
        {
            InitializeComponent();
        }

        private void EditRegionNameForm_Load(object sender, EventArgs e)
        {
            txtOldName.Text = RegionToChange.RegionName;
            txtNewName.Text = RegionToChange.RegionName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNewName.Text.Trim() == string.Empty)
            {
                CustomMessageBox.ShowError(ResourceHelper.GetResourceText("RequiredName"));
                return;
            }

            RegionToChange.RegionName = txtNewName.Text;
            RegionHelper.Save(RegionToChange);

            if (OnRegionNameChanged != null)
            {
                OnRegionNameChanged(this,new EventArgs());
            }

            Close();
        }
    }
}
