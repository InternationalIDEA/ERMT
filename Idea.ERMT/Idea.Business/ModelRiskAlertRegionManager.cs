using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Idea.DAL;

using Idea.Entities;

namespace Idea.Business
{
    public class ModelRiskAlertRegionManager
    {
        /// <summary>
        /// Returns a new ModelRiskAlertRegion.
        /// </summary>
        /// <returns></returns>
        public static ModelRiskAlertRegion GetNew()
        {
            return new ModelRiskAlertRegion();
        }

        /// <summary>
        ///  Returns the ModelRiskAlert by idModelRiskAlert.
        /// </summary>
        /// <param name="idModelRiskAlert"></param>
        /// <returns></returns>
        public static List<ModelRiskAlertRegion> GetByModelRiskAlertID(int idModelRiskAlert)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.ModelRiskAlertRegions.Where(mrar => mrar.IDModelRiskAlert == idModelRiskAlert).ToList();
            }
        }

        /// <summary>
        /// Delete the ModelRiskAlertRegion.
        /// </summary>
        /// <param name="modelRiskAlertRegion"></param>
        public static void Delete(ModelRiskAlertRegion modelRiskAlertRegion)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                ModelRiskAlertRegion mrar =
                    context.ModelRiskAlertRegions.FirstOrDefault(
                        mrar2 => mrar2.IDModelRiskAlertRegion == modelRiskAlertRegion.IDModelRiskAlertRegion);
                context.ModelRiskAlertRegions.Remove(mrar);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Saves the ModelRiskAlertRegion.
        /// </summary>
        /// <param name="modelRiskAlertRegion"></param>
        /// <returns></returns>
        public static ModelRiskAlertRegion Save(ModelRiskAlertRegion modelRiskAlertRegion)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                context.ModelRiskAlertRegions.AddOrUpdate(modelRiskAlertRegion);
                context.SaveChanges();
                return modelRiskAlertRegion;
            }
        }

        public static List<ModelRiskAlertRegion> GetByRegion(Region region)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.ModelRiskAlertRegions.Where(mrar => mrar.IDRegion == region.IDRegion).ToList();
            }
        }
    }
}
