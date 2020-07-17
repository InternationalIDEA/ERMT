using System.Collections.Generic;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Server
{
    [ServiceContract]
    public interface IReportService
    {
        [OperationContract]
        List<Report> GetAll();

        [OperationContract]
        Report Get(int idReport);

        [OperationContract]
        List<Report> GetByModel(int idModel);

        [OperationContract]
        Report Save(Report report);

        [OperationContract]
        void Delete(Report report);

        [OperationContract]
        List<Report> GetByName(string name);

        [OperationContract]
        List<Report> GetByNameAndType(string name, int type);
    }
}
