using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Facade
{
    public static class RegionHelper
    {
        private static RegionService.IRegionService _service;

        private static RegionService.IRegionService GetService()
        {
            if (_service == null)
            {
                _service = new RegionService.RegionServiceClient();
                Uri uri = new Uri(((ClientBase<RegionService.IRegionService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                ((ClientBase<RegionService.IRegionService>)_service).Endpoint.Address = new EndpointAddress(uri);
            }
            else
            {
                try
                {
                    ((ClientBase<RegionService.IRegionService>)(_service)).Close();
                }
                catch
                { }
                finally
                {
                    _service = new RegionService.RegionServiceClient();
                    Uri uri = new Uri(((ClientBase<RegionService.IRegionService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                    ((ClientBase<RegionService.IRegionService>)_service).Endpoint.Address = new EndpointAddress(uri);
                }

            }
            switch (((ClientBase<RegionService.IRegionService>)(_service)).State)
            {
                case CommunicationState.Faulted:
                    ((ClientBase<RegionService.IRegionService>)(_service)).Abort();
                    _service = new RegionService.RegionServiceClient();
                    break;
                case CommunicationState.Closed:
                case CommunicationState.Closing:
                    _service = new RegionService.RegionServiceClient();
                    break;
            }
            return _service;
        }

        /// <summary>
        /// Returns a new Region
        /// </summary>
        /// <returns></returns>
        public static Region GetNew()
        {
            return new Region();
        }

        /// <summary>
        /// Returns the list of all Regions (service).
        /// </summary>
        /// <returns></returns>
        public static List<Region> GetAll()
        {
            return (GetService().GetAll());
        }

        /// <summary>
        /// Returns the childs by idRegion (service).
        /// </summary>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<Region> GetChilds(int idRegion)
        {
            return (GetService().GetChilds(idRegion)).ToList();
        }

        /// <summary>
        /// Returns a String in the format "1|2|3" containing the features ids to exlude from a shapefile.
        /// </summary>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static string GetFeatureIDsToExclude(int idRegion)
        {
            return GetService().GetFeatureIDsToExclude(idRegion);
        }

        /// <summary>
        /// Returns the Region by idRegion (service).
        /// </summary>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static Region Get(int idRegion)
        {
            return (GetService().Get(idRegion));
        }

        /// <summary>
        /// Saves a Region (service).  
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static Region Save(Region region)
        {
            return (GetService().Save ((region)));
        }

        /// <summary>
        /// Deletes a Region by region name (service).
        /// </summary>
        /// <param name="region"></param>
        public static void Delete(Region region)
        {
            GetService().Delete((region));
        }

        /// <summary>
        /// Returns the list of childs by idRegion (service).
        /// </summary>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<Region> GetAllChilds(int idRegion)
        {
            return (GetService().GetAllChilds(idRegion)).ToList();
        }

        /// <summary>
        /// Returns the world region.
        /// </summary>
        /// <returns></returns>
        public static Region GetWorld()
        {
            return GetService().GetWorld();
        }

        /// <summary>
        ///  Returns the list of childs by idRegion and level (service).
        /// </summary>
        /// <param name="idRegion"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static List<Region> GetChildsAtLevel(int idRegion, int level)
        {
            return (GetService().GetChildsAtLevel(idRegion, level)).ToList();
        }

        public static Region GetRegionByShapeFileAndIndex(FileInfo shapeFileInfo, int index)
        {
            return (GetService().GetRegionByShapeFileAndIndex(shapeFileInfo, index));
        }

        /// <summary>
        /// Returns the level of Region by idRegion (service).
        /// </summary>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static int GetLevel(int idRegion)
        {
            return GetService().GetRegionLevel(idRegion);
        }

        public static List<Region> GetAllRelated(int idRegion)
        {
            return GetService().GetAllRelated(idRegion);
        }

        /// <summary>
        /// Returns the type of the region: World, Continent, Country, Province or Administrative.
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static RegionType GetRegionType(Region region)
        {
            int regionLevel = GetLevel(region.IDRegion);

            switch (regionLevel)
            {
                case 0:
                {
                    return RegionType.World;
                }
                case 1:
                {
                    return RegionType.Continent;
                }
                case 2:
                {
                    return RegionType.Country;
                }
                case 3:
                {
                    return RegionType.Province;
                }
                default:
                {
                    return RegionType.Administrative;
                }
            }
        }

        /// <summary>
        /// Returns the dictionary by attributes (service).
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetAttributes(string attributes)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string[] a = attributes.Split('&');
            string[] b;
            foreach (string s in a)
            {
                b = s.Split('=');
                //TODO:Descifrar
                dict.Add(b[0], ReplaceHexValues(b[1]));
            }
            return dict;
        }

        /// <summary>
        /// Returns the string of hexadecimal value (service).
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private static string ReplaceHexValues(string a)
        {
            Regex n = new Regex(@"_x(\d\d\d\d)_");
            foreach (Match c in n.Matches(a))
            {
                a = a.Replace(c.Value, ConvertHexToString(c.Groups[1].Value));
            }
            return a;
        }

        /// <summary>
        /// Convert the string of hexadecimal value (service).
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private static string ConvertHexToString(string hex)
        {
            int d = Int32.Parse(hex, NumberStyles.HexNumber);
            return ((char)d).ToString();
        }

        /// <summary>
        /// Returns the a RegionLevel corresponding to the integer.
        /// </summary>
        /// <param name="regionLevelInt"></param>
        /// <returns></returns>
        public static RegionLevel GetRegionLevelFromNumber(int regionLevelInt)
        {
            RegionLevel regionLevel = new RegionLevel();

            switch (regionLevelInt)
            {
                case 0:
                    {
                        regionLevel = RegionLevel.World;
                        break;
                    }
                case 1:
                    {
                        regionLevel = RegionLevel.Continent;
                        break;
                    }
                case 2:
                    {
                        regionLevel = RegionLevel.Country;
                        break;
                    }
                case 3:
                    {
                        regionLevel = RegionLevel.Province;
                        break;
                    }
                case 4:
                    {
                        regionLevel = RegionLevel.FirstAdmin;
                        break;
                    }
                case 5:
                    {
                        regionLevel = RegionLevel.SecondAdmin;
                        break;
                    }
                case 6:
                    {
                        regionLevel = RegionLevel.ThirdAdmin;
                        break;
                    }
                case 7:
                    {
                        regionLevel = RegionLevel.FourthAdmin;
                        break;
                    }
            }
            return regionLevel;
        }
    }
}
