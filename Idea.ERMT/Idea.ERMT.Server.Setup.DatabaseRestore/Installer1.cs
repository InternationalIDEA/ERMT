using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data.SqlClient;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceProcess;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace Idea.DatabaseRestore
{
    [RunInstaller(true)]
    public partial class Installer1 : Installer
    {
        public Installer1()
        {
            InitializeComponent();
        }

        public void RestoreDatabase(String databaseName, String filePath, String serverName, String userName, String password, String dataFilePath, String logFilePath)
        {
            CheckServiceStarted();
            Restore sqlRestore = new Restore();

            BackupDeviceItem deviceItem = new BackupDeviceItem
                    (filePath, DeviceType.File);
            sqlRestore.Devices.Add(deviceItem);
            sqlRestore.Database = databaseName;

            ServerConnection connection;
            // for Windows Authentication
            if (userName == "")
            {
                SqlConnection sqlCon = new SqlConnection
            (@"Data Source=" + serverName + @"; Integrated Security=True;");
                connection = new ServerConnection(sqlCon);
            }
            // for Server Authentication
            else
                connection = new ServerConnection(serverName, userName, password);

            Server sqlServer = new Server(connection);

            Database db = sqlServer.Databases[databaseName];
            sqlRestore.Action = RestoreActionType.Database;
            String dataFileLocation = dataFilePath + databaseName + ".mdf";
            String logFileLocation = logFilePath + databaseName + "_Log.ldf";
            db = sqlServer.Databases[databaseName];

            sqlRestore.RelocateFiles.Add(new RelocateFile
                    (databaseName, dataFileLocation));
            sqlRestore.RelocateFiles.Add(new RelocateFile
                (databaseName + "_log", logFileLocation));
            sqlRestore.ReplaceDatabase = true;
            sqlRestore.Complete += sqlRestore_Complete;
            sqlRestore.PercentCompleteNotification = 10;
            sqlRestore.PercentComplete += sqlRestore_PercentComplete;

            try
            {
                sqlRestore.SqlRestore(sqlServer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }


            db = sqlServer.Databases[databaseName];

            db.SetOnline();

            sqlServer.Refresh();
        }

        private void CheckServiceStarted()
        {
            try
            {
                ServiceController sc = new ServiceController("MSSQL$IDEAERMT");
                if (sc.Status != ServiceControllerStatus.Running)
                {
                    sc.Start();
                }
            }
            catch (Exception ex)
            {
                Context.LogMessage("The service couldn't be started. Please start it manually from the services console.");
            }
        }

        public void DetachDatabase(String databaseName, String serverName, String userName, String password)
        {
            ServerConnection connection;
            // for Windows Authentication
            if (userName == "")
            {
                SqlConnection sqlCon = new SqlConnection
            (@"Data Source=" + serverName + @"; Integrated Security=True;");
                connection = new ServerConnection(sqlCon);
            }
            // for Server Authentication
            else
                connection = new ServerConnection(serverName, userName, password);

            Server sqlServer = new Server(connection);
            try
            {
                sqlServer.DetachDatabase(databaseName, false);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("The database couldn't be detached. If you want to install the product again, please detach it manually.");
            }
        }

        void sqlRestore_PercentComplete(object sender, PercentCompleteEventArgs e)
        {

        }

        void sqlRestore_Complete(object sender, ServerMessageEventArgs e)
        {

        }
        public override void Commit(IDictionary savedState)
        {
            // The code below changes the TARGETDIR permission 
            // for a Windows Services running under the 
            // NT AUTHORITY\NETWORK SERVICE account.

            try
            {
                DirectorySecurity dirSec = Directory.GetAccessControl(Context.Parameters["TargetDir"]);
                FileSystemAccessRule fsar = new FileSystemAccessRule(GetAccountName(), FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
                dirSec.AddAccessRule(fsar);
                Directory.SetAccessControl(Context.Parameters["TargetDir"], dirSec);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            RestoreDatabase(Context.Parameters["databaseName"], Context.Parameters["filePath"], Context.Parameters["serverName"], Context.Parameters["userName"], Context.Parameters["password"], Context.Parameters["dataFilePath"], Context.Parameters["logFilePath"]);

            base.Commit(savedState);
        }

        private string GetAccountName()
        {
            try
            {
                return new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null).Translate(typeof(NTAccount)).Value;
            }
            catch (Exception)
            {
                return @"NT AUTHORITY\NETWORK SERVICE";
            }
            
            //switch (CultureInfo.CurrentCulture.Name.Split('-')[0])
            //{
            //    case "de":
            //        return @"NT-AUTORITÄT\NETZWERKDIENST";
            //    case "es":
            //    case "en":
            //    default:
            //        return @"NT AUTHORITY\NETWORK SERVICE";

            //}

        }

        public override void Uninstall(IDictionary savedState)
        {
            try
            {
                DetachDatabase(Context.Parameters["databaseName"], Context.Parameters["serverName"], Context.Parameters["userName"], Context.Parameters["password"]);
            }
            catch { }

            base.Uninstall(savedState);
        }
    }
}