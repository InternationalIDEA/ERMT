using System.Collections.Generic;
using System.IO;
using Idea.Business;
using Idea.Entities;

namespace Idea.Server
{
    public class RegionService : IRegionService
    {
        public List<Region> GetAll()
        {
            return (RegionManager.GetAll());
        }

        public Region GetWorld()
        {
            return RegionManager.GetWorld();
        }

        public Region GetRegionByShapeFileAndIndex(FileInfo shapeFileInfo, int index)
        {
            return RegionManager.GetRegionByShapeFileAndIndex(shapeFileInfo, index);
        }

        public List<Region> GetAllRelated(int idRegion)
        {
            return RegionManager.GetAllRelated(idRegion);
        }

        /// <summary>
        /// Returns a String in the format "1|2|3" containing the features ids to exlude from a shapefile.
        /// </summary>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public string GetFeatureIDsToExclude(int idRegion)
        {
            return RegionManager.GetFeatureIDsToExclude(idRegion);
        }

        public List<Region> GetChilds(int idRegion)
        {
            return (RegionManager.GetChilds(idRegion));
        }

        public Region Get(int idRegion)
        {
            return (RegionManager.Get(idRegion));
        }

        public Region Save(Region region)
        {
            return (RegionManager.Save((region)));
        }

        public void Delete(Region region)
        {
            RegionManager.Delete((region));
        }

        public List<Region> GetAllChilds(int region)
        {
            return (RegionManager.GetAllChilds(region));
        }

        public List<Region> GetChildsAtLevel(int idRegion, int level)
        {
            return (RegionManager.GetChildsAtLevel(idRegion, level));
        }

        public int GetRegionLevel(int idRegion)
        {
            return RegionManager.GetRegionLevel(idRegion);
        }
    }
}
