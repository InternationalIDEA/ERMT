using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Idea.DAL;
using Idea.Entities;

namespace Idea.Business
{
    public class ReportManager
    {
        /// <summary>
        ///  Returns a new Report.
        /// </summary>
        /// <returns></returns>
        public static Report GetNew()
        {
            return new Report();
        }

        /// <summary>
        ///  Returns the list of all Reports.
        /// </summary>
        /// <returns></returns>
        public static List<Report> GetAll()
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Reports.ToList();
            }
        }

        /// <summary>
        /// Returns a Report by idReport.
        /// </summary>
        /// <param name="idReport"></param>
        /// <returns></returns>
        public static Report Get(int idReport)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Reports.FirstOrDefault(r => r.IDReport == idReport);
            }
        }

        /// <summary>
        /// Saves a Report.
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public static Report Save(Report report)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                try
                {
                    context.Reports.AddOrUpdate(report);
                    context.SaveChanges();
                    return report;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException exDB)
                {
                    return new Report();
                }
                catch (Exception ex)
                {
                    return new Report();
                }

            }
        }

        /// <summary>
        /// Deletes a Report by report name.
        /// </summary>
        /// <param name="report"></param>
        public static void Delete(Report report)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                Report r = context.Reports.FirstOrDefault(r2 => r2.IDReport == report.IDReport);
                context.Reports.Remove(r);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns the list of Reports by idModel.
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static List<Report> GetByModel(int idModel)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Reports.Where(r => r.IDModel == idModel).ToList();
            }
        }

        /// <summary>
        /// Returns the list of Reports by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<Report> GetByName(string name)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Reports.Where(r => r.Name.ToLower() == name.ToLower()).ToList();
            }
        }

        public static List<Report> GetByNameAndType(string name, int type)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Reports.Where(r => r.Type == type && r.Name.ToLower() == name.ToLower()).ToList();
            }
        }
    }
}
