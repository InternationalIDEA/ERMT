using System.Collections.Generic;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Server
{
    [ServiceContract]
    public interface IUserService 
    {
        [OperationContract]
        User Login(string username, string password);

        [OperationContract]
        void Save(User user);

        [OperationContract]
        bool Validate(User user);

        [OperationContract]
        List<User> GetAll();
        
        [OperationContract]
        void Delete(User user);

        [OperationContract]
        User GetByIDUser(int idUser);
    }

    
}
