using System.Collections.Generic;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Server
{
    [ServiceContract]
    public interface IFactorService
    {
        [OperationContract]
        bool Validate(Factor factor);

        [OperationContract]
        void Save(Factor factor);

        [OperationContract]
        List<Factor> GetAll();

        [OperationContract]
        Factor Get(int idFactor);

        [OperationContract]
        void Delete(Factor factor);
    }
}
