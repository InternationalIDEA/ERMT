using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Idea.DAL;
using Idea.Entities;

namespace Idea.Business
{
    public class MarkerTypeManager
    {
        /// <summary>
        /// Returns a new MarkerType.
        /// </summary>
        /// <returns></returns>
        public static MarkerType GetNew()
        {
            return new MarkerType();
        }

        /// <summary>
        /// Returns all the MarkerTypes.
        /// </summary>
        /// <returns></returns>
        public static List<MarkerType> GetAll()
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (context.MarkerTypes).ToList();
            }
        }

        /// <summary>
        /// Get and Returns the MarkerType with IDMarkerType = idMarkerType.
        /// </summary>
        /// <param name="idMarkerType"></param>
        /// <returns></returns>
        public static MarkerType Get(int idMarkerType)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (context.MarkerTypes.Where(m => m.IDMarkerType == idMarkerType)).FirstOrDefault();
            }
        }

        /// <summary>
        /// Saves the MarkerType.
        /// </summary>
        /// <param name="markerType"></param>
        /// <returns></returns>
        public static MarkerType Save(MarkerType markerType)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                context.MarkerTypes.AddOrUpdate(markerType);
                context.SaveChanges();
                return markerType;
            }
        }

        /// <summary>
        /// Deletes the MarkerType.
        /// </summary>
        /// <param name="markerType"></param>
        public static void Delete(MarkerType markerType)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                MarkerType mt = context.MarkerTypes.FirstOrDefault(mt2 => mt2.IDMarkerType == markerType.IDMarkerType);
                context.MarkerTypes.Remove(mt);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Get and Returns the MarkerType by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<MarkerType> GetByName(string name)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.MarkerTypes.Where(mt => mt.Name.ToLower().Equals(name.ToLower())).ToList();
            }
        }
    }
}
