using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using Idea.DAL;
using Idea.Entities;

namespace Idea.Business
{
    public class RegionManager
    {
        /// <summary>
        /// Returns a new Region.
        /// </summary>
        /// <returns></returns>
        public static Region GetNew()
        {
            return new Region();
        }

        /// <summary>
        /// Returns all Regions.
        /// </summary>
        /// <returns></returns>
        public static List<Region> GetAll()
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Regions.OrderBy(r => r.RegionName).ToList();
            }
        }

        /// <summary>
        /// Returns all Regions.
        /// </summary>
        /// <returns></returns>
        public static List<Region> GetAllByRegionLevel(int regionLevel)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Regions.Where(r=>r.RegionLevel == regionLevel).OrderBy(r => r.RegionName).ToList();
            }
        }

        /// <summary>
        /// Returns a String in the format "1|2|3" containing the features ids to exlude from a shapefile.
        /// </summary>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static String GetFeatureIDsToExclude(int idRegion)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.spGetFeatureIDsToExclude(idRegion).First();
            }
        }

        /// <summary>
        /// Returns the world.
        /// </summary>
        /// <returns></returns>
        public static Region GetWorld()
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Regions.FirstOrDefault(r => r.IDRegion == 1);
            }
        }

        /// <summary>
        /// If idRegion = 0, get those regions with no parent
        /// if idRegion != 0, get the childs of that region.
        /// </summary>
        /// <param name="idRegion">The ID of the region.</param>
        /// <returns></returns>
        public static List<Region> GetChilds(int idRegion)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                if (idRegion == 0)
                {
                    return (from r in context.Regions where r.RegionName == null orderby r.RegionName select r).ToList();
                }

                return (from r in context.Regions where r.IDParent == idRegion orderby r.RegionName select r).ToList();
            }
        }

        /// <summary>
        /// Returns the region by shape file and index.
        /// </summary>
        public static Region GetRegionByShapeFileAndIndex(FileInfo shapeFileInfo, int index)
        {
            String databaseShapeFileName =
                shapeFileInfo.Directory.ToString().Substring(shapeFileInfo.Directory.ToString().Length - 1, 1) + "\\" +
                shapeFileInfo.Name;

            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (context.Regions.FirstOrDefault(r => r.ShapeFileName.ToLower().Equals(databaseShapeFileName.ToLower()) &&
                            r.ShapeFileIndex == index));
            }
        }

        /// <summary>
        /// Returns the region by shape file name and index.
        /// </summary>
        public static Region GetRegionByShapeFileAndIndex(String shapeFileName, int? index)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (context.Regions.FirstOrDefault(r => r.ShapeFileName.ToLower().Equals(shapeFileName.ToLower()) &&
                            r.ShapeFileIndex == index));
            }
        }

        //returns the region by shape file name and index.
        public static Region GetRegionByNameAndLevel(FileInfo shapeFileInfo, int index)
        {
            String databaseShapeFileName =
                shapeFileInfo.Directory.ToString().Substring(shapeFileInfo.Directory.ToString().Length - 1, 1) + "\\" +
                shapeFileInfo.Name;

            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (context.Regions.FirstOrDefault(r => r.ShapeFileName.ToLower().Equals(databaseShapeFileName.ToLower()) &&
                            r.ShapeFileIndex == index));
            }
        }

        /// <summary>
        /// Returns the region with IDRegion = idRegion
        /// </summary>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static Region Get(int idRegion)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                Region aux = context.Regions.FirstOrDefault(r => r.IDRegion == idRegion);
                return aux;
            }
        }

        /// <summary>
        /// Saves a Region.
        /// </summary>
        /// <param name="region">The region to save.</param>
        /// <returns></returns>
        public static Region Save(Region region)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                context.Regions.AddOrUpdate(region);
                context.SaveChanges();
                return region;
            }
        }

        /// <summary>
        /// Deletes the Region.
        /// </summary>
        /// <param name="region"></param>
        public static void Delete(Region region)
        {
            List<Model> regionModels = ModelManager.GetByRegion(region.IDRegion);

            ModelManager.Delete(regionModels);

            List<ModelFactorData> regionData = ModelFactorDataManager.GetByRegion(region.IDRegion);
            foreach (ModelFactorData modelFactorData in regionData)
            {
                ModelFactorDataManager.Delete(modelFactorData);
            }

            List<ModelRiskAlertRegion> alertData = ModelRiskAlertRegionManager.GetByRegion(region);
            foreach (ModelRiskAlertRegion modelRiskAlertRegion in alertData)
            {
                ModelRiskAlertRegionManager.Delete(modelRiskAlertRegion);
            }

            List<Region> childRegions = GetChilds(region.IDRegion);

            foreach (Region childRegion in childRegions)
            {
                Delete(childRegion);
            }

            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                Region r = (from o in context.Regions where o.IDRegion == region.IDRegion select o).FirstOrDefault();
                if (r != null)
                {
                    context.Regions.Remove(r);
                    context.SaveChanges();
                }
                
            }
        }

        /// <summary>
        /// Returns all the childs of the Region.
        /// </summary>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<Region> GetAllChilds(int idRegion)
        {
            List<Region> childsList = GetChilds(idRegion);

            List<Region> returnList = new List<Region>();

            foreach (Region region in childsList)
            {
                returnList.Add(region);
                returnList.AddRange(GetAllChilds(region.IDRegion));
            }

            return returnList.OrderBy(r => r.RegionName).ToList();
        }

        /// <summary>
        /// Returns all the childs at the specified level.
        /// </summary>
        /// <param name="idRegion"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static List<Region> GetChildsAtLevel(int idRegion, int level)
        {
            //return GetAllChilds(idRegion).Where(r => GetRegionLevel(r.IDRegion) == level).ToList();
            List<Region> childsList = GetChilds(idRegion);

            List<Region> returnList = new List<Region>();

            foreach (Region region in childsList)
            {
                returnList.Add(region);
                returnList.AddRange(GetAllChilds(region.IDRegion));
            }

            return returnList.Where(r => r.RegionLevel == level).OrderBy(r => r.RegionName).ToList();
        }

        /// <summary>
        /// returns the first region with the specified parent and name 
        /// </summary>
        /// <param name="idParent"></param>
        /// <param name="regionName"></param>
        /// <returns></returns>
        public static Region GetRegionByIDParentAndName(int idParent, String regionName)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                Region aux = context.Regions.FirstOrDefault(r => r.IDParent == idParent && r.RegionName.ToLower() == regionName.ToLower());
                return aux;
            }
        }

        /// <summary>
        /// Returns the level of the region
        /// </summary>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static int GetRegionLevel(int? idRegion)
        {
            if (idRegion == null)
            {
                return 0;
            }

            Region r = Get(idRegion ?? 1);
            return r.RegionLevel;
        }

        /// <summary>
        /// Returns the parents of the region, including the parameter region.
        /// </summary>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<Region> GetParents(int idRegion)
        {
            List<Region> returnList = new List<Region>();
            Region region = Get(idRegion);

            returnList.Add(region);

            if (region.IDParent == null)
            {
                //base case.
                return returnList;
            }

            returnList.AddRange(GetParents(Convert.ToInt32(region.IDParent)));
            return returnList;
        }

        /// <summary>
        /// Returns all the regions related to the region with IDRegion = idRegion.
        /// </summary>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<Region> GetAllRelated(int idRegion)
        {
            List<Region> aux = GetAllChilds(idRegion);
            aux.AddRange(GetParents(idRegion));

            return aux.OrderBy(r => r.RegionLevel).ToList();
        }
    }
}
