using System;
using System.Reflection;

namespace Idea.Utils
{
    public class AppVersion
    {
        private static readonly Version _version;
        static AppVersion()
        {
            _version = Assembly.GetExecutingAssembly().GetName().Version; 
        }
        public static Version Version
        {
            get
            {
                return _version;
            }
        }
    }
}
