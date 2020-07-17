using System;
using System.Drawing;
using System.Windows.Forms;
using Idea.Entities;

namespace Idea.ERMT.UserControls
{
    public partial class MapLegendItem : UserControl
    {
        public event EventHandler OnMouseDown;

        public LegendItemType LegendItemType;

        public Color LegendItemBackColor
        {
            set
            {
                pnlLegendItemColor.BackColor = value;
            }
        }

        public Color LegendItemCumulativeTextColor
        {
            set { lblCumulativeTextColor.ForeColor = value; }
        }

        public String LegendItemValue
        {
            set
            {
                lblLegendItemValue.Text = value;
            }
        }

        public MapLegendItem(LegendItemType legendItemType)
        {
            InitializeComponent();
            MouseDown += MapLegendItem_MouseDown;
            pnlLegendItemColor.MouseDown += MapLegendItem_MouseDown;
            lblLegendItemValue.MouseDown += MapLegendItem_MouseDown;
            LegendItemType = legendItemType;

            if (legendItemType != LegendItemType.Cumulative)
            {
                lblCumulativeTextColor.Visible = false;
            }
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
