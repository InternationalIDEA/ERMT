using System.Windows.Forms;

namespace Idea.ERMT.UserControls
{
    public class DataGridViewExtended : DataGridView
    {
        public void DamnOnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }
    }
}
