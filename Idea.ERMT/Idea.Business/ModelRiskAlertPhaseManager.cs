using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Idea.DAL;
using Idea.Entities;

namespace Idea.Business
{
    public class ModelRiskAlertPhaseManager
    {
        /// <summary>
        /// Returns a new ModelRiskAlertPhase.
        /// </summary>
        /// <returns></returns>
        public static ModelRiskAlertPhase GetNew()
        {
            return new ModelRiskAlertPhase();
        }

        /// <summary>
        /// Saves the ModelRiskAlertPhase.
        /// </summary>
        /// <param name="modelRiskAlertPhase"></param>
        /// <returns></returns>
        public static ModelRiskAlertPhase Save(ModelRiskAlertPhase modelRiskAlertPhase)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                context.ModelRiskAlertPhases.AddOrUpdate(modelRiskAlertPhase);
                context.SaveChanges();
                return modelRiskAlertPhase;
            }
        }


        /// <summary>
        /// Returns the list of ModelRiskAlert by idModelRiskAlert.
        /// </summary>
        /// <param name="idModelRiskAlert"></param>
        /// <returns></returns>
        public static List<ModelRiskAlertPhase> GetByModelRiskAlertID(int idModelRiskAlert)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.ModelRiskAlertPhases.Where(mraph => mraph.IDModelRiskAlert == idModelRiskAlert).ToList();
            }
        }

        /// <summary>
        /// Delete the ModelRiskAlertPhase.
        /// </summary>
        /// <param name="modelRiskAlertPhase"></param>
        public static void Delete(ModelRiskAlertPhase modelRiskAlertPhase)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                ModelRiskAlertPhase mrap =
                    context.ModelRiskAlertPhases.FirstOrDefault(
                        mrap2 => mrap2.IDModelRiskAlertPhase == modelRiskAlertPhase.IDModelRiskAlertPhase);
                context.ModelRiskAlertPhases.Remove(mrap);
                context.SaveChanges();
            }
        }
    }
}
