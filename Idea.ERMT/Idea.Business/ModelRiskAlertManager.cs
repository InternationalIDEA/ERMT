using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using Idea.DAL;
using Idea.Entities;

namespace Idea.Business
{
    public class ModelRiskAlertManager
    {
        /// <summary>
        /// Returns a new ModelRiskAlert.
        /// </summary>
        /// <returns></returns>
        public static ModelRiskAlert GetNew()
        {
            return new ModelRiskAlert();
        }

        public static ModelRiskAlert Get(int idModelRiskAlert)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.ModelRiskAlerts.FirstOrDefault(mra => mra.IDModelRiskAlert == idModelRiskAlert);
            }
        }

        /// <summary>
        /// Returns all the ModelRiskAlert.
        /// </summary>
        /// <returns></returns>
        public static List<ModelRiskAlert> GetAll()
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.ModelRiskAlerts.ToList();
            }
        }

        public static List<Region> GetAllRegionsWithAlerts()
        {
            List<Region> aux = new List<Region>();
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                IEnumerable<ModelRiskAlertRegion> modelRiskAlertRegions = context.ModelRiskAlertRegions;

                foreach (ModelRiskAlertRegion modelRiskAlertRegion in modelRiskAlertRegions)
                {
                    if (!aux.Any(r => r.IDRegion == modelRiskAlertRegion.IDRegion))
                    {
                        aux.Add(RegionManager.Get(modelRiskAlertRegion.IDRegion));
                    }
                }

                aux = aux.OrderBy(r => r.RegionLevel).ToList();
            }
            return aux;
        }

        /// <summary>
        /// Returns the ModelRiskAlert by idModel and regions  
        /// </summary>
        /// <param name="idModel"></param>
        /// <param name="regions"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public static List<ModelRiskAlert> GetWithFilter(int? idModel, List<int> regions, bool? isActive)
        {
            //TODO: this method should return the CHILDS from the regions list also. Compare to [dbo].[spModelRiskAlert_GetWithFilter]
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                if (regions.Count > 0)
                {
                    IQueryable<ModelRiskAlert> modelRiskAlerts = context.ModelRiskAlerts;
                    //List<Region> childs = RegionManager.GetChilds()
                    IQueryable<ModelRiskAlertRegion> modelRiskAlertsRegions =
                        context.ModelRiskAlertRegions.Where(mrar => regions.Contains(mrar.IDRegion));
                    IQueryable<Model> models = context.Models;
                    return (from mra in modelRiskAlerts
                        join mrar in modelRiskAlertsRegions
                            on mra.IDModelRiskAlert equals mrar.IDModelRiskAlert
                        join m in models
                            on mra.IDModel equals m.IDModel
                        where (isActive == null || mra.Active == isActive)
                              && (idModel == null || mra.IDModel == idModel)
                        select mra).ToList();
                }
                else
                {
                    IQueryable<ModelRiskAlert> modelRiskAlerts = context.ModelRiskAlerts;
                    IQueryable<Model> models = context.Models;
                    return (from mra in modelRiskAlerts
                        join m in models
                            on mra.IDModel equals m.IDModel
                        where (isActive == null || mra.Active == isActive)
                              && (idModel == null || mra.IDModel == idModel)
                        select mra).ToList();
                }

            }
        }

        /// <summary>
        /// Save the ModelRiskAlert.
        /// </summary>
        /// <param name="modelRiskAlert"></param>
        public static ModelRiskAlert Save(ModelRiskAlert modelRiskAlert)
        {
            if (modelRiskAlert.Code == string.Empty)
            {
                modelRiskAlert.Code = SystemParameterManager.GetNextCode();
                SystemParameterManager.SaveLastCode(Convert.ToInt32(modelRiskAlert.Code));
            }

            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                context.ModelRiskAlerts.AddOrUpdate(modelRiskAlert);
                context.SaveChanges();
                return modelRiskAlert;
            }
        }

        /// <summary>
        /// Delete the ModelRiskAlert.
        /// </summary>
        /// <param name="modelRiskAlert"></param>
        public static void Delete(ModelRiskAlert modelRiskAlert)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                ModelRiskAlert mra =
                    context.ModelRiskAlerts.FirstOrDefault(
                        mra2 => mra2.IDModelRiskAlert == modelRiskAlert.IDModelRiskAlert);
                context.ModelRiskAlerts.Remove(mra);
                context.SaveChanges();
            }
        }

        public static List<Region> GetRegions(ModelRiskAlert modelRiskAlert)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return
                    context.Regions.Where(
                        r =>
                            context.ModelRiskAlertRegions.Where(
                                mrar => mrar.IDModelRiskAlert == modelRiskAlert.IDModelRiskAlert)
                                .Select(mrar2 => mrar2.IDRegion)
                                .Contains(r.IDRegion)).ToList();
            }
        }

        public static List<ModelRiskAlertPhase> GetPhases(ModelRiskAlert modelRiskAlert)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return
                    context.ModelRiskAlertPhases.Where(mrap => mrap.IDModelRiskAlert == modelRiskAlert.IDModelRiskAlert).ToList();
            }
        }

        /// <summary>
        /// Returns the list of ModelRiskAlert by idModel.
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static List<ModelRiskAlert> GetByModelID(int idModel)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.ModelRiskAlerts.Where(mra => mra.IDModel == idModel).ToList();
            }
        }
    }
}
