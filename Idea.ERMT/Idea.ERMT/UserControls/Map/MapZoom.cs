using System;

namespace Idea.ERMT.UserControls
{
    public partial class MapZoom : ERMTUserControl
    {
        public event EventHandler OnZoomIn;
        public event EventHandler OnZoomOut; 

        public MapZoom()
        {
            InitializeComponent();
        }

        private void pbZoomOut_Click(object sender, EventArgs e)
        {
            if (OnZoomOut != null)
            {
                OnZoomOut(new object(), new EventArgs());
            }
        }

        private void pbZoomIn_Click(object sender, EventArgs e)
        {
            if (OnZoomIn != null)
            {
                OnZoomIn(new object(),new EventArgs());
            }
        }
    }
}
