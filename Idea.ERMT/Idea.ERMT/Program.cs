using System;
using System.Diagnostics;
using System.Runtime.Remoting.Contexts;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using Idea.Entities;
using Idea.ERMT.UserControls;
using Idea.Facade;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Xml;

namespace Idea.ERMT
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Trace.WriteLine("Program.cs");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if !DEBUG
            Splash.ShowSplash(500);
            Thread.Sleep(4000);
            Splash.Fadeout();
#endif
            //LogHelper.ConfigureLog();
            
            ConfigurationSettingsHelper.SetInstanceEndpointAddress();

            Boolean serverAvailable = ConfigurationSettingsHelper.TestServer();
            if (!serverAvailable)
            {
                CustomMessageBox.ShowError(ResourceHelper.GetResourceText("ServerConnectionError"));
                ServerSettings s = new ServerSettings();
                s.ShowDialog();
                Application.Exit();
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                String configFileName = Utils.DirectoryAndFileHelper.LanguageConfigurationFile;
                if (File.Exists(configFileName))
                {
                    doc.Load(configFileName);

                    try
                    {
                        CultureInfo uiCulture = new CultureInfo(doc.DocumentElement.Attributes["culture"].Value);
                        CultureInfo culture = new CultureInfo("en-GB");
                        //Thread.CurrentThread.CurrentCulture = culture;
                        Thread.CurrentThread.CurrentCulture = culture;
                        Thread.CurrentThread.CurrentUICulture = uiCulture;
                    }
                    catch (System.Globalization.CultureNotFoundException)
                    {
                    }
                }

                PrincipalForm principalForm = ViewManager.CreatePrincipalForm();
                ViewManager.SetMainControl(ERMTControl.Login);
                Application.Run(principalForm);    
            }
        }
    }
}

