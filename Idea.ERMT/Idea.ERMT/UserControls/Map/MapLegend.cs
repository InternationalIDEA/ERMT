using Idea.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Idea.Facade;
using ThinkGeo.MapSuite.Core;

namespace Idea.ERMT.UserControls
{
    public partial class MapLegend : Form
    {
        public MapLegend()
        {
            InitializeComponent();
            MouseDown += Legends_MouseDown;
            tlpLegends.MouseDown += Legends_MouseDown;
            lblFactorInfo.MouseDown += Legends_MouseDown;

        }

        public String Title
        {
            set
            {
                lblFactorInfo.Text = value;
                //Size size = TextRenderer.MeasureText(lblFactorInfo.Text, Font);
                //tlpLegends.RowStyles[0].Height = size.Height;
            }
            get { return lblFactorInfo.Text; }
        }

      
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Legends_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        internal void SetColorScheme(ClassBreakStyle modelClassBreakStyle)
        {
            Width = 0;
            flpLegendItems.Controls.Clear();
            foreach (ClassBreak classBreak in modelClassBreakStyle.ClassBreaks)
            {
                GeoColor gc = classBreak.DefaultAreaStyle.FillSolidBrush.Color;

                Color color = Color.FromArgb(gc.AlphaComponent, gc.RedComponent, gc.GreenComponent, gc.BlueComponent);
                if (gc != GeoColor.SimpleColors.Transparent)
                {
                    MapLegendItem mapLegendItem = new MapLegendItem(LegendItemType.Scale)
                    {
                        LegendItemBackColor = color,
                        LegendItemValue = Convert.ToInt32(classBreak.Value).ToString(),
                        Padding = new Padding(0,0,0,0), 
                        Margin = new Padding(0,0,0,0)
                    };
                    mapLegendItem.MouseDown += Legends_MouseDown;
                    mapLegendItem.OnMouseDown += mapLegendItem_OnMouseDown;
                    Width += mapLegendItem.Width + 3;
                    flpLegendItems.Controls.Add(mapLegendItem);
                }
            }
        }

        void mapLegendItem_OnMouseDown(object sender, EventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        internal void ShowCumulativeFactorsLegends(List<Factor> cumulativeFactors)
        {
            Width = 0;
            flpLegendItems.Controls.Clear();
            tlpLegends.RowStyles[0].Height = 20;
            flpLegendItems.FlowDirection = FlowDirection.TopDown;
            foreach (Factor cumulativeFactor in cumulativeFactors)
            {
                MapLegendItem li = new MapLegendItem(LegendItemType.Cumulative);
                Color backColor = SystemColors.ControlText;
                Color fontColor = SystemColors.ControlText;

                if (cumulativeFactor.Color.Split(',').Length > 1)
                {
                    backColor = ColorTranslator.FromHtml(cumulativeFactor.Color.Split(',')[0]);
                    fontColor = ColorTranslator.FromHtml(cumulativeFactor.Color.Split(',')[1]);
                }
                Size size = TextRenderer.MeasureText(cumulativeFactor.Name, li.Font);
                li.Width = size.Width + 20;
                Width = Math.Max(li.Width, Width);
                li.LegendItemBackColor = backColor;
                li.LegendItemCumulativeTextColor = fontColor;
                li.LegendItemValue = cumulativeFactor.Name;
                flpLegendItems.Controls.Add(li);
            }
            this.Height = 25 + (20*cumulativeFactors.Count);
        }

        internal void ShowMarkersLegends(List<int> markerTypeIDs)
        {
            Width = 0;
            Height = 25 + (38 * markerTypeIDs.Count);
            flpLegendItems.Controls.Clear();
            tlpLegends.RowStyles[0].Height = 20;
            flpLegendItems.FlowDirection = FlowDirection.TopDown;
            foreach (int idMarkerType in markerTypeIDs)
            {
                MarkerType mt = MarkerTypeHelper.Get(idMarkerType);
                MapLegendMarkerItem li = new MapLegendMarkerItem();
                Size size = TextRenderer.MeasureText(mt.Name, li.Font);
                li.Width = size.Width + 38;
                li.MarkerTypeImage = ImageHelper.GetMarkerImage(mt, new Size(34, 34));
                Width = Math.Max(li.Width, Width);
                li.LegendItemText = mt.Name;
                flpLegendItems.Controls.Add(li);
            }
            
        }

        public void Clear()
        {
            Title = string.Empty;
            flpLegendItems.Controls.Clear();
        }
    }
}
