using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.ServiceProcess;


namespace Idea.ERMT.Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        public override void Commit(IDictionary savedState)
        {
            Trace.WriteLine("ERMT: Service Commit");
            base.Commit(savedState);
        }

        public override void Install(IDictionary stateSaver)
        {
            Trace.WriteLine("ERMT: Service Install");
            ServiceController sc;
            try
            {
                sc = new ServiceController("IDEA Geo Host Service");
                if (sc.Status == ServiceControllerStatus.Running)
                {
                    sc.Stop();
                    //base.Uninstall(stateSaver);
                }
            }
            catch (InstallException ex)
            {
            }
            catch (Exception ex)
            {
            }

            base.Install(stateSaver);
        }

        public override void Uninstall(IDictionary savedState)
        {
            Trace.WriteLine("ERMT: Service Uninstall");
            base.Uninstall(savedState);
        }

        public override void Rollback(IDictionary savedState)
        {
            Trace.WriteLine("ERMT: Service Rollback");
            base.Rollback(savedState);
        }
    }
}
