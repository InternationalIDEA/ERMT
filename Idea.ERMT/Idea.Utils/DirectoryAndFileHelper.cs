using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Idea.Utils
{
    public static class DirectoryAndFileHelper
    {
        public static string ClientAppDataFolder
        {
            get
            {
#if DEBUG
                return Application.StartupPath + "\\Idea.ERMT.Client\\";
#else
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Idea.ERMT.Client\\";
#endif
            }

        }

        public static string ServerAppDataFolder
        {
            get
            {
#if DEBUG
                return @"c:\_code\IDEA\Idea.ERMT.Server\";
#else

                return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Idea.ERMT.Server\\";
#endif
            }

        }

        /// <summary>
        /// Returns the client folder containing the shapefiles.
        /// </summary>
        /// <returns></returns>
        public static String ClientShapefilesFolder
        {
            get
            {
                return ClientAppDataFolder + ConfigurationManager.AppSettings["ShapefilesFolder"] + "\\";
            }

        }

        public static String ServerShapefilesFolder
        {
            get
            {
                return ServerAppDataFolder + ConfigurationManager.AppSettings["ShapefilesFolder"] + "\\";
            }

        }

        public static DirectoryInfo ClientCumulativeFactorImageFolder
        {
            get
            {
                DirectoryInfo aux = new DirectoryInfo(ClientAppDataFolder + "CumulativeFactorsImages\\");
                if (!aux.Exists)
                {
                    Directory.CreateDirectory(aux.FullName);
                }

                return aux;
            }

        }

        public static string IndexTemplate
        {
            get
            {
                return ServerAppDataFolder + ConfigurationManager.AppSettings["IndexTemplate"];
                //switch (Thread.CurrentThread.CurrentCulture.Name.ToLower())
                //{
                //    case "es-es":
                //        {
                //            return ServerAppDataFolder + ConfigurationManager.AppSettings["IndexTemplate"].ToLower().Replace(".htm", "-es.htm");
                //        }
                //    case "fr":
                //        {
                //            return ServerAppDataFolder + ConfigurationManager.AppSettings["IndexTemplate"].ToLower().Replace(".htm", "-fr.htm");
                //        }
                //    default:
                //        {
                //            return ServerAppDataFolder + ConfigurationManager.AppSettings["IndexTemplate"];
                //        }
                //}
            }
        }

        //public static string IndexTemplateSpanish
        //{
        //    get
        //    {
        //        return ServerAppDataFolder + ConfigurationManager.AppSettings["IndexTemplate"].ToLower().Replace(".htm", "-es.htm");
        //    }
        //}

        //public static string IndexTemplateFrench
        //{
        //    get
        //    {
        //        return ServerAppDataFolder + ConfigurationManager.AppSettings["IndexTemplate"].ToLower().Replace(".htm", "-fr.htm");
        //    }
        //}

        public static string Index
        {
            get
            {
                return ServerAppDataFolder + ConfigurationManager.AppSettings["Index"];
            }
        }

        public static string ModelViewConfigurationFile
        {
            get
            {
                return ClientAppDataFolder + "Last.ini";
            }

        }

        public static string LanguageConfigurationFile
        {
            get
            {
                return ClientAppDataFolder + "Language.ini";
            }

        }

        public static string ConfigFilePath
        {
            get
            {
                return ClientAppDataFolder + "endpoint.ini";
            }

        }

        public static string SignedInUserFile
        {
            get { return ClientAppDataFolder + "signedinuser.ini"; }
        }

        #region RAR

        public static string RARDocumentsFolder
        {
            get
            {
                return ClientAppDataFolder + ConfigurationManager.AppSettings["RARDocumentsFolder"];
            }

        }

        #endregion

        #region Marker & MarkerType
        public static string GetMarkerTypeImagePath(string markerTypeSymbol)
        {
            return ClientAppDataFolder + "\\icons" + "\\" + markerTypeSymbol;
        }

        #endregion

        public static string HelpFilePath
        {
            get
            {
                return Application.StartupPath + "\\Help.chm";
            }
        }

        public static String ClientIconsFolder
        {
            get
            {
                return ClientAppDataFolder + "\\icons\\";
            }
        }

        public static String ServerIconsFolder
        {
            get { return ServerAppDataFolder + "\\icons\\"; }
        }

        public static String ClientHTMLFolder
        {
            get
            {
                return ClientAppDataFolder + ConfigurationManager.AppSettings["HTMLFolder"];
            }
        }

        public static String ServerHTMLFolder
        {
            get
            {
                return ServerAppDataFolder + ConfigurationManager.AppSettings["HTMLFolder"]; ;
            }
        }

        public static String ClientDocumentsFolder
        {
            get
            {
                return ClientAppDataFolder + ConfigurationManager.AppSettings["DocumentFolder"];
            }
        }

        public static String ServerDocumentsFolder
        {
            get
            {
                return ServerAppDataFolder + ConfigurationManager.AppSettings["DocumentFolder"];
            }
        }
    }
}
