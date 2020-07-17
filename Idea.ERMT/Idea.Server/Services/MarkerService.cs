using System;
using System.Collections.Generic;
using System.Linq;
using Idea.Business;
using Idea.Entities;

namespace Idea.Server
{
    public class MarkerService : IMarkerService
    {
        public List<Marker> GetAll()
        {
            return MarkerManager.GetAll().ToList();
        }

        public Marker Get(int idMarker)
        {
            return MarkerManager.Get(idMarker);
        }

        public Marker Save(Marker marker)
        {
            return MarkerManager.Save(marker);
        }

        public void Delete(Marker marker)
        {
            MarkerManager.Delete(marker);
        }

        public List<Marker> GetByName(string name)
        {
            return MarkerManager.GetByName(name);
        }

        public List<Marker> GetByModelId(int idModel)
        {
            return MarkerManager.GetByModelId(idModel);
        }

        public List<Marker> GetByMarkerTypeId(int idMarkerType)
        {
            return MarkerManager.GetByMarkerTypeId(idMarkerType);
        }

        public List<Marker> GetByModelIdAndMarkerTypeIdAndFromAndTo(int idModel, int? idMarkerType, DateTime from, DateTime to)
        {
            return MarkerManager.GetByModelIdAndMarkerTypeIdAndFromAndTo(idModel, idMarkerType, from, to);
        }

        public DateTime? GetMinDateByModelID(int idModel)
        {
            return MarkerManager.GetMinDateByModelID(idModel);
        }

        public DateTime? GetMaxDateByModelID(int idModel)
        {
            return MarkerManager.GetMaxDateByModelID(idModel);
        }
    }
}
