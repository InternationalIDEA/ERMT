using System;
using System.Diagnostics;
using System.IO;
using Idea.Utils;

namespace Idea.Facade
{
    public static class LogHelper
    {
        public static void LogError(Exception ex)
        {
            LogError(string.Empty, ex);
        }

        public static void LogError(String customMessage, Exception ex)
        {
            string message = customMessage;
            message += " Exception: " + ex.Message;
            if (ex.InnerException != null)
            {
                message += ". Inner exception: " + ex.InnerException.Message;
            }

            LogError(message);
        }

        public static void LogError(String message)
        {
            Trace.TraceError(message);
        }

        //public static void ConfigureLog()
        //{
        //    FileStreamWithBackup fs = new FileStreamWithBackup(DirectoryAndFileHelper.ClientAppDataFolder + "\\ERMTLog.txt", 200, 10, FileMode.Append)
        //    {
        //        CanSplitData = false
        //    };
        //    TextWriterTraceListenerWithTime listener = new TextWriterTraceListenerWithTime(fs);
        //    Trace.AutoFlush = true;
        //    Trace.Listeners.Add(listener);
        //}
    }
}
