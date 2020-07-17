using System.Collections.Generic;
using Idea.Business;
using Idea.Entities;

namespace Idea.Server
{

    public class ReportService : IReportService
    {
        public List<Report> GetAll()
        {
            return (ReportManager.GetAll());
        }

        public Report Get(int idReport)
        {
            return (ReportManager.Get(idReport));
        }

        public List<Report> GetByModel(int modelId)
        {
            return (ReportManager.GetByModel(modelId));
        }


        public Report Save(Report report)
        {
            return (ReportManager.Save((report)));
        }

        public void Delete(Report report)
        {
            ReportManager.Delete((report));
        }

        public List<Report> GetByName(string name)
        {
            return (ReportManager.GetByName(name));
        }

        public List<Report> GetByNameAndType(string name, int type)
        {
            return (ReportManager.GetByNameAndType(name, type));
        }
    }
}
