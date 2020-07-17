using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Windows.Forms;
using Idea.Utils;

namespace Idea.DatabaseRestore
{
    [RunInstaller(true)]
    public partial class Installer1 : Installer
    {
        public Installer1()
        {
            InitializeComponent();
        }

        public override void Commit(IDictionary savedState)
        {
            Trace.WriteLine("ERMT: ServiceStartAndStop Commit");
            try
            {
                WindowsServiceHelper.StartService(Context.Parameters["ServiceName"]);
            }
            catch (Exception ex)
            {
                Context.LogMessage("The service couldn't be started. Please start it manually from the services console. Exception message: " + ex.Message);
            }
            
            base.Commit(savedState);
        }

        public override void Install(IDictionary stateSaver)
        {
            Trace.WriteLine("ERMT: ServiceStartAndStop Install");
            try
            {
                WindowsServiceHelper.StopService(Context.Parameters["ServiceName"]);
            }
            catch (Exception ex)
            {
                Context.LogMessage("The service couldn't be stopped. Please stop it manually and run the uninstall program again. Exception message: " + ex.Message);
            }
            base.Install(stateSaver);
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            Trace.WriteLine("ERMT: ServiceStartAndStop OnBeforeInstall");
            try
            {
                WindowsServiceHelper.StopService(Context.Parameters["ServiceName"]);
            }
            catch (Exception ex)
            {
                Context.LogMessage("The service couldn't be stopped. Please stop it manually and run the uninstall program again. Exception message: " + ex.Message);
            }
            base.OnBeforeInstall(savedState);
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            Trace.WriteLine("ERMT: ServiceStartAndStop OnAfterInstall");
            try
            {
                WindowsServiceHelper.StartService(Context.Parameters["ServiceName"]);
            }
            catch (Exception ex)
            {
                Context.LogMessage("The service couldn't be started. Please start it manually from the services console. Exception message: " + ex.Message);
            }
            base.OnAfterInstall(savedState);
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            Trace.WriteLine("ERMT: ServiceStartAndStop OnBeforeUninstall");
            try
            {
                WindowsServiceHelper.StopService(Context.Parameters["ServiceName"]);
            }
            catch (Exception ex)
            {
                Context.LogMessage("The service couldn't be stopped. Please stop it manually and run the uninstall program again. Exception message: " + ex.Message);
            }
            base.OnBeforeUninstall(savedState);
        }
      
    }
}