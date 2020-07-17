using System.Windows.Forms;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class FactorPreview : Form
    {
        public FactorPreview()
        {
            InitializeComponent();
            Text = ResourceHelper.GetResourceText("PreviewTitle");

        }

        public string BodyHtml
        {
            get { return htmlEditorControl1.BodyHtml; }
            set { htmlEditorControl1.BodyHtml = value; }
        }

        public void LinkStyleSheet(string stylesheet)
        {
            htmlEditorControl1.LinkStyleSheet(stylesheet);
        }
    }
}
