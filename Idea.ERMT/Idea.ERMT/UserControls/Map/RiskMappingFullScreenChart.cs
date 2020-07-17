using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;
using ThinkGeoMarker = ThinkGeo.MapSuite.DesktopEdition.Marker;

namespace Idea.ERMT.UserControls
{
    public partial class RiskMappingFullScreenChart : Form
    {
        public event EventHandler OnClose;

        public Chart ChartControl
        {
            get { return chart1; }
            set { chart1 = value; }
        }

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        public RiskMappingFullScreenChart()
        {
            InitializeComponent();
        }

        private void chart_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                case Keys.F11:
                {
                    int hWnd = FindWindow("Shell_TrayWnd", "");
                    ShowWindow(hWnd, SW_SHOW);
                    if (OnClose != null)
                    {
                        OnClose(sender, EventArgs.Empty);
                    }
                    
                    Close();
                    break;
                }
            }
        }
    }
}
