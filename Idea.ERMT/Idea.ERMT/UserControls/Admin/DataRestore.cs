using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.ServiceProcess;
using System.Windows.Forms;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class DataRestore : ERMTUserControl
    {
        public DataRestore()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool restoreDataBase = chkRestoreDataBase.Checked;
            bool restoreFiles = chkRestoreFiles.Checked;
            bool restoreShapefiles = chkRestoreShapefiles.Checked;

            if (restoreDataBase == false && restoreFiles == false && restoreShapefiles == false)
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("MustSelectAtLeastOneOption"));
                return;
            }

            if (Environment.OSVersion.Version.Major == 6)
            {
                if (!IsAdministrator())
                {
                    CustomMessageBoxReturnValue customMessageBoxReturnValue1 = CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RestoreAsAdministratorWarning"),
                                                CustomMessageBoxMessageType.Information, CustomMessageBoxButtonType.YesNo);
                    if (customMessageBoxReturnValue1 == CustomMessageBoxReturnValue.Ok)
                    {
                        // it's windows Vista / 7. UAC requires the user to run as an ADMINISTRATOR to restart the Windows Service.
                        ProcessStartInfo info = new ProcessStartInfo();
                        info.FileName = Assembly.GetEntryAssembly().Location;
                        info.UseShellExecute = true;
                        info.Verb = "runas"; // Provides Run as Administrator

                        if (Process.Start(info) != null)
                        {
                            // The user accepted the UAC prompt.
                            Application.Exit();
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

            CustomMessageBoxReturnValue customMessageBoxReturnValue2 = CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RestoreTimeWarning"),
                                            CustomMessageBoxMessageType.Information, CustomMessageBoxButtonType.YesNo);

            if (customMessageBoxReturnValue2 == CustomMessageBoxReturnValue.Cancel)
            {
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog() { Filter = "Gzipped restore files(*.gz)|*.gz", Title = "Restore backup from..." };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Boolean backupFailed = false;
                try
                {
                    string noCorrespondingRestorations = DocumentHelper.Restore(Convert.ToBase64String(File.ReadAllBytes(ofd.FileName)), restoreDataBase, restoreFiles, restoreShapefiles);
                    RestartService("IDEA Geo Host Service");
                    if (noCorrespondingRestorations != string.Empty)
                    {
                        if (noCorrespondingRestorations.Contains("invaliddbversion"))
                        {
                            CustomMessageBox.ShowError(ResourceHelper.GetResourceText("DataRestoreInvalidDBVersion"));
                            backupFailed = true;
                        }

                        if (noCorrespondingRestorations.Contains("errorrestoringdb"))
                        {
                            CustomMessageBox.ShowError(ResourceHelper.GetResourceText("DataRestoreError"));
                            backupFailed = true;
                        }

                        noCorrespondingRestorations = noCorrespondingRestorations.Substring(0, noCorrespondingRestorations.Length - 3);
                        noCorrespondingRestorations = " " + ResourceHelper.GetResourceText("SelectedRestoreOptionsWereNotBackupFile") + ": " + noCorrespondingRestorations + ".";
                    }

                    if (!backupFailed)
                    {
                        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("DataRestored") + noCorrespondingRestorations);
                        Application.Exit();
                    }
                }
                catch (Exception ex)
                {
                    CustomMessageBox.ShowMessage(ex.Message, CustomMessageBoxMessageType.Error, CustomMessageBoxButtonType.OKOnly);
                }
            }
        }

        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            if (identity != null)
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return false;
        }

        private static void RestartService(String serviceName)
        {
            Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Method RestartService. Service name: " + serviceName));
            ServiceController sc;
            try
            {
                sc = new ServiceController(serviceName);
                if (sc.Status != ServiceControllerStatus.Running)
                    return;
            }
            catch (InvalidOperationException)
            { //service doesn't exist 
                return;
            }
            try
            {

                if (sc.Status == ServiceControllerStatus.Running)
                {
                    Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Restarting service " + serviceName));
                    sc.Stop();
                    Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Service stopped."));
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromMinutes(3));
                    sc.Start();
                    Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Service " + serviceName + " restarted."));
                }

            }
            catch (Exception ex)
            {
                //throw new Exception("The service couldn't be stopped. Please stop it manually and run the uninstall program again.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ((DataRestoreForm)this.Parent).Close();
        }
    }
}
