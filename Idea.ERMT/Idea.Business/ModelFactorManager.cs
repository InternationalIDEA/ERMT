using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Idea.DAL;
using Idea.Entities;

namespace Idea.Business
{
    public static class ModelFactorManager
    {
        /// <summary>
        /// Validates fields with parameter factor. 
        /// </summary>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static bool ValidateRequiredFields(ModelFactor factor)
        {
            if (factor.ScaleMin < 0)
                throw new ArgumentException("FactorScaleMin");
            if (factor.ScaleMax <= factor.ScaleMin)
                throw new ArgumentException("FactorScaleMax");
            if (factor.Interval <= 0)
                throw new ArgumentException("FactorInterval");
            return true;
        }

        /// <summary>
        /// Returns a new ModelFactor.
        /// </summary>
        /// <returns></returns>
        public static ModelFactor GetNew()
        {
            return new ModelFactor();
        }

        /// <summary>
        /// Save the ModelFactor.
        /// </summary>
        /// <param name="modelFactor"></param>
        public static void Save(ModelFactor modelFactor)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                context.ModelFactors.AddOrUpdate(modelFactor);
                context.SaveChanges();
            }
        }

        /// <summary>
        ///  Returns all the ModelFactors.
        /// </summary>
        /// <returns></returns>
        public static List<ModelFactor> GetAll()
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (from mf in context.ModelFactors select mf).ToList<ModelFactor>();
            }
        }

        /// <summary>
        /// Returns the list of ModelFactor by idModel.
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static List<ModelFactor> GetByModel(int idModel)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (from mf in context.ModelFactors where mf.IDModel == idModel select mf).ToList<ModelFactor>();
            }
        }

        /// <summary>
        /// Get and Returns the ModelFactor with IDModelFactor = idModelFactor.
        /// </summary>
        /// <param name="idModelFactor"></param>
        /// <returns></returns>
        public static ModelFactor Get(int idModelFactor)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return
                    (from mf in context.ModelFactors where mf.IDModelFactor == idModelFactor select mf)
                        .FirstOrDefault();
            }
        }

        /// <summary>
        /// Returns the list of ModelFactor with the parameters idModel and idRegion.
        /// </summary>
        /// <param name="idModel"></param>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<ModelFactor> GetModelFactorWithDataAvailable(int idModel, int idRegion)
        {
            //TODO: este método tiene que devolver lo mismo que el SP [dbo].[spModelFactor_GetModelFactorWithDataAvailable]
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {

            }
            //return ModelFactor.GetModelFactorWithDataAvailable(idModel, idRegion);
            //TODO: this is wrong.
            return GetByModel(idModel);
        }

        /// <summary>
        /// Returns the ModelFactor with the parameters idModel and idFactor.
        /// </summary>
        /// <param name="idModel"></param>
        /// <param name="idFactor"></param>
        /// <returns></returns>
        public static List<ModelFactor> GetByModelAndFactorId(int idModel, int idFactor)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return
                    (from mf in context.ModelFactors
                        where mf.IDModel == idModel && mf.IDFactor == idFactor
                        select mf).ToList<ModelFactor>();
            }
        }

        /// <summary> 
        /// Deletes a ModelFactor
        /// </summary>
        /// <param name="modelFactor"></param>
        public static void Delete(ModelFactor modelFactor)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                ModelFactor f = (from o in context.ModelFactors where o.IDModelFactor == modelFactor.IDModelFactor select o).FirstOrDefault();
                context.ModelFactors.Remove(f);
                context.SaveChanges();
            }
        }
    }
}
