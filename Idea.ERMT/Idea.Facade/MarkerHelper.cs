using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Facade
{
    public class MarkerHelper
    {
        private static MarkerService.IMarkerService _service;
        private static MarkerService.IMarkerService GetService()
        {
            if (_service == null)
            {
                _service = new MarkerService.MarkerServiceClient();
                Uri uri = new Uri(((ClientBase<MarkerService.IMarkerService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                ((ClientBase<MarkerService.IMarkerService>)_service).Endpoint.Address = new EndpointAddress(uri);
            }
            else
            {
                try
                {
                    ((ClientBase<MarkerService.IMarkerService>)(_service)).Close();
                }
                catch
                { }
                finally
                {
                    _service = new MarkerService.MarkerServiceClient();
                    Uri uri = new Uri(((ClientBase<MarkerService.IMarkerService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                    ((ClientBase<MarkerService.IMarkerService>)_service).Endpoint.Address = new EndpointAddress(uri);
                }

            }
            switch (((ClientBase<MarkerService.IMarkerService>)(_service)).State)
            {
                case CommunicationState.Faulted:
                    ((ClientBase<MarkerService.IMarkerService>)(_service)).Abort();
                    _service = new MarkerService.MarkerServiceClient();
                    break;
                case CommunicationState.Closed:
                case CommunicationState.Closing:
                    _service = new MarkerService.MarkerServiceClient();
                    break;
            }
            return _service;
        }

        /// <summary>
        /// Returns a new Marker.
        /// </summary>
        /// <returns></returns>
        public static Marker GetNew()
        {
            return new Marker();
        }

        /// <summary>
        /// Returns the Marker by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<Marker> GetByName(string name)
        {
            return GetService().GetByName(name).ToList();
        }

        /// <summary>
        ///  Returns the Marker with IDMarker = idMarker.
        /// </summary>
        /// <param name="idMarker"></param>
        /// <returns></returns>
        public static Marker Get(int idMarker)
        {
            return GetService().Get(idMarker);
        }

        /// <summary>
        /// Saves the Marker.
        /// </summary>
        /// <param name="marker"></param>
        /// <returns></returns>
        public static Marker Save(Marker marker)
        {
            return GetService().Save(marker);
        }

        /// <summary>
        /// Returns list the all Markers with IDModel = idModel.
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static List<Marker> GetByModelId(int idModel)
        {
            return GetService().GetByModelId(idModel).ToList();
        }

        /// <summary>
        /// Returns list the all Markers with IDMarkerType = idMarkerType.
        /// </summary>
        /// <param name="idMarkerType"></param>
        /// <returns></returns>
        public static List<Marker> GetByMarkerTypeId(int idMarkerType)
        {
            return GetService().GetByMarkerTypeId(idMarkerType).ToList();
        }

        /// <summary>
        /// Returns all Markers.
        /// </summary>
        /// <returns></returns>
        public static List<Marker> GetAll()
        {
            return GetService().GetAll().ToList();
        }

        /// <summary>
        /// Returns list the Markers by IdModel, idMarkerType and DateTime.
        /// </summary>
        /// <param name="idModel"></param>
        /// <param name="idMarkerType"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static List<Marker> GetByModelIdAndMarkerTypeIdAndFromAndTo(int idModel, int? idMarkerType, DateTime from, DateTime to)
        {
            return GetService().GetByModelIdAndMarkerTypeIdAndFromAndTo(idModel, idMarkerType, from, to).ToList();
        }

        /// <summary>
        /// Deletes a Marker.
        /// </summary>
        /// <param name="marker"></param>
        public static void Delete(Marker marker)
        {
            GetService().Delete(marker);
        }

        /// <summary>
        /// Returns minimum date time with idModel.
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static DateTime? GetMinDateByModelID(int idModel)
        {
            return GetService().GetMinDateByModelID(idModel);
        }

        /// <summary>
        /// Returns maximum date time with idModel.
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static DateTime? GetMaxDateByModelID(int idModel)
        {
            return GetService().GetMaxDateByModelID(idModel);
        }
    }
}
