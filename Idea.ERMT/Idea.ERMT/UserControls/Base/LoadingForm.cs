using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Idea.ERMT.UserControls
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
            : base()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //	Size to the image so as to display it fully and position the form in the center screen with no border.            
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            //	Force the splash to stay on top while the mainform renders but don't show it in the taskbar.
            this.TopMost = true;
            this.ShowInTaskbar = false;

            //	Make the backcolour Fuchia and set that to be transparent
            //	so that the image can be shown with funny shapes, round corners etc.
            this.BackColor = System.Drawing.Color.Fuchsia;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;

            //	Initialise a timer to do the fade out
            if (this.components == null)
            {
                this.components = new System.ComponentModel.Container();
            }
            this.fadeTimer = new System.Windows.Forms.Timer(this.components);
        }

        private System.Windows.Forms.Timer fadeTimer;


        #region Static Methods

        internal static LoadingForm Instance = null;
        internal static System.Threading.Thread splashThread = null;
        public static bool Showing = false;

        public static void ShowMessage(string message)
        {
            ShowLoading(0,message);
        }

        public static void ShowLoading()
        {
            //	Show Splash with no fading
            ShowLoading(0, string.Empty);
        }

        public static void ShowLoading(int fadeinTime, string message)
        {
            Showing = true;
            //	Only show if not showing already
            if (Instance == null)
            {
                Instance = new LoadingForm();
               
                //	Hide initially so as to avoid a nasty pre paint flicker
                Instance.Opacity = 0;
                Instance.Show();
               
                //	Process the initial paint events
                Application.DoEvents();

                if (Instance!=null)
                {
                    Instance.Opacity = 1;
                }
            }
        }

        public static void Fadeout()
        {
            //	Only fadeout if we are currently visible.
            if (Instance != null)
            {
                Instance.BeginInvoke(new MethodInvoker(Instance.Close));

                //	Process the Close Message on the Splash Thread.
                Application.DoEvents();
            }
            Showing = false;
        }

        #endregion

        #region Close Splash Methods

        protected override void OnClick(System.EventArgs e)
        {
            //	If we are displaying as a about dialog we need to provide a way out.
            this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            //	Close immediatly is the timer interval is set to 1 indicating no fade.
            if (this.fadeTimer.Interval == 1)
            {
                e.Cancel = false;
                return;
            }

            //	Only use the timer to fade out if we have a mainform running otherwise there will be no message pump
            if (Application.OpenForms.Count > 1)
            {
                if (this.Opacity > 0)
                {
                    e.Cancel = true;
                    this.Opacity -= 0.05;

                    //	use the timer to iteratively call the close method thereby keeping the GUI thread available for other processes.
                    this.fadeTimer.Tick -= new System.EventHandler(this.FadeoutTick);
                    this.fadeTimer.Tick += new System.EventHandler(this.FadeoutTick);
                    this.fadeTimer.Start();
                }
                else
                {
                    e.Cancel = false;
                    this.fadeTimer.Stop();

                    //	Clear the instance variable so we can reshow the splash, and ensure that we don't try to close it twice
                    Instance = null;
                }
            }
            else
            {
                if (this.Opacity > 0)
                {
                    //	Sleep on this thread to slow down the fade as there is no message pump running
                    System.Threading.Thread.Sleep(this.fadeTimer.Interval);
                    Instance.Opacity -= 0.05;

                    //	iteratively call the close method
                    this.Close();
                }
                else
                {
                    e.Cancel = false;

                    //	Clear the instance variable so we can reshow the splash, and ensure that we don't try to close it twice
                    Instance = null;
                }
            }

        }

        void FadeoutTick(object sender, System.EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}

