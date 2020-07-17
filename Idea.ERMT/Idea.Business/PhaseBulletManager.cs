using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Idea.DAL;
using Idea.Entities;

namespace Idea.Business
{
    public static class PhaseBulletManager
    {
        /// <summary>
        /// Returns a new PhaseBullet.
        /// </summary>
        /// <returns></returns>
        public static PhaseBullet GetNew()
        {
            return new PhaseBullet();
        }

        /// <summary>
        /// Returns all the PhaseBullets.
        /// </summary>
        /// <returns></returns>
        public static List<PhaseBullet> GetAll()
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (from o in context.PhaseBullets select o).ToList();
            }

        }

        /// <summary>
        ///  Returns the PhaseBullet by idPhase and columnNumber.
        /// </summary>
        /// <param name="idPhase"></param>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        public static List<PhaseBullet> GetByPhaseAndColumn(int idPhase, int columnNumber)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (from o in context.PhaseBullets where o.IDPhase == idPhase && o.ColumnNumber == columnNumber select o).OrderBy(o => o.SortOrder).ToList();
            }

        }

        /// <summary>
        /// Save the SaveColumnBullets of PhaseBullet.
        /// </summary>
        /// <param name="bullets"></param>
        public static void SaveColumnBullets(List<PhaseBullet> bullets)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                foreach (PhaseBullet bullet in bullets)
                {
                    context.PhaseBullets.AddOrUpdate(bullet);
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Save the  PhaseBullet.
        /// </summary>
        /// <param name="phaseBullet"></param>
        /// <returns></returns>
        public static PhaseBullet Save(PhaseBullet phaseBullet)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                context.PhaseBullets.AddOrUpdate(phaseBullet);
                context.SaveChanges();
            }
            return phaseBullet;
        }

        /// <summary>
        /// Delete the PhaseBullet.
        /// </summary>
        /// <param name="phaseBullet"></param>
        public static void Delete(PhaseBullet phaseBullet)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                PhaseBullet f = (from o in context.PhaseBullets where o.IDPhaseBullet == phaseBullet.IDPhaseBullet select o).FirstOrDefault();
                context.PhaseBullets.Remove(f);
                context.SaveChanges();
            }
        }


        /// <summary>
        /// Delete the PhaseBullet.
        /// </summary>
        /// <param name="idPhaseBullet"></param>
        public static void Delete(int idPhaseBullet)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                PhaseBullet f = (from o in context.PhaseBullets where o.IDPhaseBullet == idPhaseBullet select o).FirstOrDefault();

                if (f != null)
                {
                    context.PhaseBullets.Remove(f);
                    context.SaveChanges();
                }
            }
        }
    }
}
