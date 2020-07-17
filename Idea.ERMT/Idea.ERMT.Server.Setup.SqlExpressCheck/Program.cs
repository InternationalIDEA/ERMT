using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;

namespace CustomCheck.SqlExpressCheck
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>        
        static void Main(string[] args)
        {
            //Agregamos algo que MATE el servicio de IDEA - prerequisito para hacer un upgrade
            StopService("IDEA Geo Host Service");
            //Process p = Process.Start(@"C:\Program Files\Microsoft SDKs\Windows\v6.0A\Bootstrapper\Packages\SqlExpress\SqlExpresschk.exe");
            //p.Start();
            //p.WaitForExit();
            //int x = p.ExitCode;
            //InstanceName
            try
            {
                string instanceName = args[0].ToUpper();
                bool found = false;
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\" + instanceName);
                if (rk != null)
                    found = true;
                RegistryKey rk2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL");

                foreach (string name in rk2.GetValueNames())
                {
                    if (name.ToUpper() == instanceName.ToUpper())
                    {
                        found = true;
                        break;
                    }
                }
#if DEBUG
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\CustomCheck.SQLExpressCheck.log", "Instance " + instanceName + " found:" + found.ToString());

#endif

                if (found)
                    Environment.ExitCode = 0;
                else
                    Environment.ExitCode = 1;
            }
            catch { Environment.ExitCode = 1; }
        }

        public static void StopService(String serviceName)
        {
            ServiceController sc;
            try
            {
                sc = new ServiceController(serviceName);
                if (sc.Status != ServiceControllerStatus.Running)
                    return;
            }
            catch (System.InvalidOperationException)
            { //service doesn't exist 
                return;
            }
            try
            {

                if (sc.Status == ServiceControllerStatus.Running)
                    sc.Stop();
            }
            catch (Exception ex)
            {
                
                //throw new Exception("The service couldn't be stopped. Please stop it manually and run the uninstall program again.");

            }
        }    
    }
}
