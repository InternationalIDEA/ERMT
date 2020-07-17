using System.Collections.Generic;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Server
{
    [ServiceContract]
    public interface IModelFactorService
    {
        [OperationContract]
        bool Validate(ModelFactor modelFactor);

        [OperationContract]
        void Save(ModelFactor modelFactor);

        [OperationContract]
        List<ModelFactor> GetAll();

        [OperationContract]
        List<ModelFactor> GetByModel(int idModel);

        [OperationContract]
        ModelFactor Get(int idModelFactor);

        [OperationContract]
        List<ModelFactor> GetModelFactorWithDataAvailable(int idModel, int idRegion);

        [OperationContract]
        List<ModelFactor> GetByModelAndFactorId(int idModel, int idFactor);

        [OperationContract]
        void Delete(ModelFactor modelFactor);
    }
}
