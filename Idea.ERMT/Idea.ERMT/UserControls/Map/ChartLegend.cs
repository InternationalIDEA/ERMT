using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Idea.ERMT.UserControls
{
    public partial class ChartLegend : Form
    {
        public ChartLegend()
        {
            InitializeComponent();
            MouseDown += Legends_MouseDown;
            chart1.MouseDown += Legends_MouseDown;
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
    }
}
