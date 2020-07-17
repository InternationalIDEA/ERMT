using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace Idea.Utils
{
    public static class WindowsServiceHelper
    {
        public static void StartService(String serviceName)
        {
            Trace.WriteLine("ERMT: StartService. ServiceName: " + serviceName);
            try
            {
                ServiceController sc = new ServiceController(serviceName);
                sc.Start();
            }
            catch (InvalidOperationException)
            { //service doesn't exist 
                throw new Exception("Service doesn't exists");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void StopService(String serviceName)
        {
            Trace.WriteLine("ERMT: StopService. ServiceName: " + serviceName);
            ServiceController sc;
            try
            {
                sc = new ServiceController(serviceName);
                if (sc.Status != ServiceControllerStatus.Running)
                    return;
            }
            catch (InvalidOperationException)
            { //service doesn't exist 
                throw new Exception("Service doesn't exists");
            }
            try
            {

                if (sc.Status == ServiceControllerStatus.Running)
                {
                    sc.Stop();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }     
    }
}
