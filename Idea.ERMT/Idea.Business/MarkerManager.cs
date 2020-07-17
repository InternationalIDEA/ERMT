using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Idea.DAL;
using Idea.Entities;

namespace Idea.Business
{
    public class MarkerManager
    {
        /// <summary>
        /// Returns a new Marker.
        /// </summary>
        /// <returns></returns>
        public static Marker GetNew()
        {
            return new Marker();
        }

        /// <summary>
        /// Returns all Markers.
        /// </summary>
        /// <returns></returns>
        public static IQueryable<Marker> GetAll()
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Markers;
            }
        }

        /// <summary>
        /// Returns the Marker with IDMarker = idMarker.
        /// </summary>
        /// <param name="idMarker"></param>
        /// <returns></returns>
        public static Marker Get(int idMarker)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (from m in context.Markers where m.IDMarker == idMarker select m).FirstOrDefault();
            }
        }

        /// <summary>
        /// Saves the Marker.
        /// </summary>
        /// <param name="marker"></param>
        /// <returns></returns>
        public static Marker Save(Marker marker)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                context.Markers.AddOrUpdate(marker);
                context.SaveChanges();
                return marker;
            }
        }

        /// <summary>
        /// Deletes the Marker.
        /// </summary>
        /// <param name="marker"></param>
        public static void Delete(Marker marker)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                Marker m = context.Markers.FirstOrDefault(m2 => m2.IDMarker == marker.IDMarker);
                context.Markers.Remove(m);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns the Marker by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<Marker> GetByName(string name)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Markers.Where(m => m.Name.ToLower().Equals(name.ToLower())).ToList();
            }
        }

        /// <summary>
        /// Returns list the all Markers with IDModel = idModel.
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static List<Marker> GetByModelId(int idModel)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Markers.Where(m => m.IDModel == idModel).ToList();
            }
        }

        /// <summary>
        /// Returns list the Markers by type with IDMarkerType = idMarkerType.
        /// </summary>
        /// <param name="idMarkerType"></param>
        /// <returns></returns>
        public static List<Marker> GetByMarkerTypeId(int idMarkerType)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Markers.Where(m => m.IDMarkerType == idMarkerType).ToList();
            }
        }

        /// <summary>
        /// Returns list the markers by model, marker type and date time.
        /// </summary>
        /// <param name="idModel"></param>
        /// <param name="idMarkerType"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static List<Marker> GetByModelIdAndMarkerTypeIdAndFromAndTo(int idModel, int? idMarkerType, DateTime from, DateTime to)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Markers.Where(m =>
                m.IDModel == idModel && m.IDMarkerType == idMarkerType
                && m.DateFrom >= from
                && m.DateTo <= to
                ).ToList();
            }
        }

        /// <summary>
        /// Returns minimum date time with idModel.
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static DateTime? GetMinDateByModelID(int idModel)
        {
            List<Marker> allMarkers = GetByModelId(idModel);
            allMarkers.Sort((p1, p2) => p1.DateFrom.CompareTo(p2.DateFrom));

            if (allMarkers.Count > 0)
            {
                return allMarkers[0].DateFrom;
            }

            return null;
        }

        /// <summary>
        /// Returns maximum date time with idModel.
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static DateTime? GetMaxDateByModelID(int idModel)
        {
            List<Marker> allMarkers = GetByModelId(idModel);
            allMarkers.Sort((p1, p2) => p2.DateFrom.CompareTo(p1.DateFrom));

            if (allMarkers.Count > 0)
            {
                return allMarkers[0].DateFrom;
            }

            return null;
        }
    }
}
