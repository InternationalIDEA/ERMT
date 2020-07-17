using System.Collections.Generic;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Server
{
    [ServiceContract]
    [ServiceKnownType(typeof(Marker))]
    [ServiceKnownType(typeof(Region))]
    [ServiceKnownType(typeof(ModelFactor))]
    [ServiceKnownType(typeof(ModelRiskAlert))]
    [ServiceKnownType(typeof(Report))]
    public interface IModelService
    {
        [OperationContract]
        List<Model> GetAll();

        [OperationContract]
        Model Get(int idModel);

        [OperationContract]
        Model Save(Model model);

        [OperationContract]
        List<Model> GetByRegion(int idRegion);

        [OperationContract]
        void Delete(Model model);

        [OperationContract]
        List<Model> GetByName(string modelName);

        [OperationContract]
        List<Model> GetByRegions(List<Region> regions);
    }
}
