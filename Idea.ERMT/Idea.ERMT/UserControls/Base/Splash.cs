using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Idea.ERMT.UserControls
{
    /// <summary>
    /// Description of Splash.
    /// </summary>
    public partial class Splash : Form
    {
        private readonly Timer _fadeTimer;
    
        public Splash()
        {
            InitializeComponent();

            Size = BackgroundImage.Size;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;

            TopMost = true;
            ShowInTaskbar = false;

            BackColor = Color.Fuchsia;
            TransparencyKey = Color.Fuchsia;

            if (components == null)
            {
                components = new Container();
            }
            _fadeTimer = new Timer(this.components);

        }
    
    

        internal static Splash Instance = null;
        internal static Thread splashThread = null;

        public static void ShowSplash()
        {
            //	Show Splash with no fading
            ShowSplash(0);
        }

        public static void ShowSplash(int fadeinTime)
        {
            //	Only show if not showing already
            if (Instance == null)
            {
                Instance = new Splash();

                //	Hide initially so as to avoid a nasty pre paint flicker
                Instance.Opacity = 0;
                Instance.Show();

                //	Process the initial paint events
                Application.DoEvents();

                // Perform the fade in
                if (fadeinTime > 0)
                {
                    //	Set the timer interval so that we fade out at the same speed.
                    int fadeStep = (int)Math.Round((double)fadeinTime / 20);
                    Instance._fadeTimer.Interval = fadeStep;

                    for (int i = 0; i <= fadeinTime; i += fadeStep)
                    {
                        Thread.Sleep(fadeStep);
                        Instance.Opacity += 0.05;
                    }
                }
                else
                {
                    //	Set the timer interval so that we fade out instantly.
                    Instance._fadeTimer.Interval = 1;
                }
                Instance.Opacity = 1;
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
        }


        #region Close Splash Methods

        protected override void OnClick(EventArgs e)
        {
            //	If we are displaying as a about dialog we need to provide a way out.
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            //	Close immediatly is the timer interval is set to 1 indicating no fade.
            if (this._fadeTimer.Interval == 1)
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
                    this._fadeTimer.Tick -= new EventHandler(this.FadeoutTick);
                    this._fadeTimer.Tick += new EventHandler(this.FadeoutTick);
                    this._fadeTimer.Start();
                }
                else
                {
                    e.Cancel = false;
                    this._fadeTimer.Stop();

                    //	Clear the instance variable so we can reshow the splash, and ensure that we don't try to close it twice
                    Instance = null;
                }
            }
            else
            {
                if (this.Opacity > 0)
                {
                    //	Sleep on this thread to slow down the fade as there is no message pump running
                    Thread.Sleep(this._fadeTimer.Interval);
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

        void FadeoutTick(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
