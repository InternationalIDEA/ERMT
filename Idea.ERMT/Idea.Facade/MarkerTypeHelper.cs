using System;
using System.Collections.Generic;
//using Dundas.Maps.WinControl;
using System.ServiceModel;
using Idea.Entities;
using Idea.Server;
using System.Linq;

namespace Idea.Facade
{
    public class MarkerTypeHelper
    {
        private static MarkerTypeService.IMarkerTypeService _service;
        private static MarkerTypeService.IMarkerTypeService GetService()
        {
            if (_service == null)
            {
                _service = new MarkerTypeService.MarkerTypeServiceClient();
                Uri uri = new Uri(((ClientBase<MarkerTypeService.IMarkerTypeService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                ((ClientBase<MarkerTypeService.IMarkerTypeService>)_service).Endpoint.Address = new EndpointAddress(uri);
            }
            else
            {
                try
                {
                    ((ClientBase<MarkerTypeService.IMarkerTypeService>)(_service)).Close();
                }
                catch
                { }
                finally
                {
                    _service = new MarkerTypeService.MarkerTypeServiceClient();
                    Uri uri = new Uri(((ClientBase<MarkerTypeService.IMarkerTypeService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                    ((ClientBase<MarkerTypeService.IMarkerTypeService>)_service).Endpoint.Address = new EndpointAddress(uri);
                }

            }
            switch (((ClientBase<MarkerTypeService.IMarkerTypeService>)(_service)).State)
            {
                case CommunicationState.Faulted:
                    ((ClientBase<IMarkerTypeService>)(_service)).Abort();
                    _service = new MarkerTypeService.MarkerTypeServiceClient();
                    break;
                case CommunicationState.Closed:
                case CommunicationState.Closing:
                    _service = new MarkerTypeService.MarkerTypeServiceClient();
                    break;
            }
            return _service;
        }

        /// <summary>
        /// Returns a MarkerType.
        /// </summary>
        /// <param name="idMarkerType"></param>
        /// <returns></returns>
        public static MarkerType Get(int idMarkerType)
        {
            return GetService().Get(idMarkerType);
        }

        /// <summary>
        /// Returns a Symbol Struct.
        /// </summary>
        public static SymbolStruct[] Symbols = new SymbolStruct[35]
                                                   {
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 1, Name = "Circle", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 2, Name = "Diamond", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 3, Name = "Pentagon", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 4, Name = "Rectangle", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 5, Name = "Star", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 6, Name = "Trapezoid", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 7, Name = "Triangle", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 8, Name = "Wedge", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 9, Name = "Counting Closed", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 10, Name = "Black pin", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 11, Name = "Counting Open", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 12, Name = "Blue pin", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 13, Name = "Counting Ready", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 14, Name = "Disaster", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 15, Name = "Gray pin", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 16, Name = "Dispute", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 17, Name = "Hate Speech", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 18, Name = "Green pin", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 19, Name = "Gender Issues", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 20, Name = "Human Rights", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 21, Name = "Information Campaign", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 22, Name = "Media Access", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 23, Name = "No Media Access", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 24, Name = "Rally", Type = true},
                                                       new SymbolStruct()
                                                           {Directory = "", ID = 25, Name = "No", Type = true},
                                                           new SymbolStruct()
                                                           {Directory = "", ID = 26, Name = "Polling Station Closed", Type = true},
                                                           new SymbolStruct()
                                                           {Directory = "", ID = 27, Name = "Polling Station Open", Type = true},
                                                           new SymbolStruct()
                                                           {Directory = "", ID = 28, Name = "Red pin", Type = true},
                                                           new SymbolStruct()
                                                           {Directory = "", ID = 29, Name = "Polling Station Ready", Type = true},
                                                              new SymbolStruct()
                                                           {Directory = "", ID = 30, Name = "Provocative Use Of Media", Type = true},
                                                              new SymbolStruct()
                                                           {Directory = "", ID = 31, Name = "Voter registration open", Type = true},
                                                              new SymbolStruct()
                                                           {Directory = "", ID = 32, Name = "Voter registration closed", Type = true},
                                                              new SymbolStruct()
                                                           {Directory = "", ID = 33, Name = "Voter registration ready", Type = true},
                                                              new SymbolStruct()
                                                           {Directory = "", ID = 34, Name = "Funds", Type = true},
                                                              new SymbolStruct()
                                                           {Directory = "", ID = 35, Name = "Yellow pin", Type = true}
                                                   };


        /// <summary>
        /// Returns list the  MarkerType 's sizes.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSizes()
        {
            return new List<string> { ResourceHelper.GetResourceText("Small"), 
                ResourceHelper.GetResourceText("Medium"), 
                ResourceHelper.GetResourceText("Large") };
        }

        /// <summary>
        /// Returns all MarkerTypes.
        /// </summary>
        /// <returns></returns>
        public static List<MarkerType> GetAll()
        {
            return GetService().GetAll().ToList();
        }

        /// <summary>
        /// Returns a new MarkerType.
        /// </summary>
        /// <returns></returns>
        public static MarkerType GetNew()
        {
            return new MarkerType();
        }

        /// <summary>
        /// Saves a MarkerType.
        /// </summary>
        /// <param name="mt"></param>
        public static void Save(MarkerType mt)
        {
            GetService().Save((mt));
        }

        /// <summary>
        /// Deletes the MarkerType. 
        /// </summary>
        /// <param name="mt"></param>
        /// <param name="deleteMarkerTypeImage"></param>
        public static void Delete(MarkerType mt, bool deleteMarkerTypeImage)
        {
            GetService().Delete((mt), deleteMarkerTypeImage);
        }

        /*public static MarkerStyle GetSymbol(string symbol)
        {
            switch (symbol)
            {
                case "1":
                    return MarkerStyle.Circle;
                case "2":
                    return MarkerStyle.Diamond;
                case "3":
                    return MarkerStyle.Pentagon;
                case "4":
                    return MarkerStyle.Rectangle;
                case "5":
                    return MarkerStyle.Star;
                case "6":
                    return MarkerStyle.Trapezoid;
                case "7":
                    return MarkerStyle.Triangle;
                case "8":
                    return MarkerStyle.Wedge;
                default:
                    return MarkerStyle.None;
            }
        }*/    

        /// <summary>
        /// Make a validation for PredefinedSymbol.
        /// </summary>
        /// <param name="p"></param>   
        /// <returns></returns>
        public static bool isPredefinedSymbol(string p)
        {
            return (p == "Circle.png" || p == "Diamond.png"
                    || p == "Pentagon.png"
                   || p == "Rectangle.png"
                   || p == "Star.png"
                   || p == "Trapezoid.png"
                   || p == "Triangle.png"
                  || p == "Wedge.png");

        }
    }
}
