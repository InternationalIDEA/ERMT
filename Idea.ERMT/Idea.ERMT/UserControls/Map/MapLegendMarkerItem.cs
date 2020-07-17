using System;
using System.Drawing;
using System.Windows.Forms;
using Idea.Entities;

namespace Idea.ERMT.UserControls
{
    public partial class MapLegendMarkerItem : ERMTUserControl
    {
        public event EventHandler OnMouseDown;

        public Image MarkerTypeImage
        {
            set { pbMarkerTypeImage.Image = value; }
        }

        public String LegendItemText
        {
            set
            {
                lblLegendItemValue.Text = value;
            }
        }

        public MapLegendMarkerItem()
        {
            InitializeComponent();
            MouseDown += MapLegendItem_MouseDown;
            lblLegendItemValue.MouseDown += MapLegendItem_MouseDown;
            pbMarkerTypeImage.MouseDown += MapLegendItem_MouseDown;
        }

        void MapLegendItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (OnMouseDown != null)
            {
                OnMouseDown(sender,new EventArgs());
            }
        }
    }
}
