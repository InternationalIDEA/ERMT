using System.Collections.Generic;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Server
{
    [ServiceContract]
    public interface IRoleService
    {
        [OperationContract]
        List<Role> GetAll();
    }
}
