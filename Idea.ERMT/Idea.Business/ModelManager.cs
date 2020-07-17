using System;
using System.Collections.Generic;
using System.Linq;
using Idea.DAL;
using Idea.Entities;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.IO;

namespace Idea.Business
{
    public class ModelManager
    {
        /// <summary>
        /// Returns a new Model.
        /// </summary>
        /// <returns></returns>
        public static Model GetNew()
        {
            return new Model();
        }

        /// <summary>
        /// Returns all the Models.
        /// </summary>
        /// <returns></returns>
        public static List<Model> GetAll()
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Models.ToList();
            }
        }

        /// <summary>
        /// Returns the Model with IDModel = idModel
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static Model Get(int idModel)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Models.FirstOrDefault(m => m.IDModel == idModel);
            }
        }

        private static void ClearRelatedEntities(Model model)
        {
            model.Markers = new List<Marker>();
            model.ModelFactors = new List<ModelFactor>();
            model.ModelRiskAlerts = new List<ModelRiskAlert>();
            model.Reports = new List<Report>();
        }

        /// <summary>
        /// Save the Model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Model Save(Model model)
        {
            using (IdeaContext context = new IdeaContext())
            {
                ClearRelatedEntities(model);
                context.Models.AddOrUpdate(model);
                context.SaveChanges();
                return model;
            }
        }

        /// <summary>
        /// Delete the Model.
        /// </summary>
        /// <param name="model"></param>
        public static void Delete(Model model)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                Model m = context.Models.FirstOrDefault(m2 => m2.IDModel == model.IDModel);
                context.Models.Remove(m);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes every model in the list
        /// </summary>
        /// <param name="models"></param>
        public static void Delete(List<Model> models)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                foreach (Model model in models)
                {
                    Model m = context.Models.FirstOrDefault(m2 => m2.IDModel == model.IDModel);
                    context.Models.Remove(m);    
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns the Model by idRegion.
        /// </summary>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<Model> GetByRegion(int idRegion)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Models.Where(m => m.IDRegion == idRegion).ToList();
            }
        }

        /// <summary>
        /// Retuns a list of models for the provided list of regions.
        /// </summary>
        /// <param name="regions"></param>
        /// <returns></returns>
        public static List<Model> GetByRegions(List<Region> regions)
        {
            List<int> regionIDs = regions.Select(r => r.IDRegion).ToList();
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Models.Where(m => regionIDs.Contains(m.IDRegion)).ToList();
            }
        }

        /// <summary>
        /// Returns the Model by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<Model> GetByName(string name)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Models.Where(m => m.Name == name).ToList();
            }
            
        }
    }
}
