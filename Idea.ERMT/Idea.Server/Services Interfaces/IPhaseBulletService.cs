using System.Collections.Generic;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Server
{
    [ServiceContract]
    public interface IPhaseBulletService
    {
        [OperationContract]
        List<PhaseBullet> GetAll();

        [OperationContract]
        List<PhaseBullet> GetByPhaseAndColumn(int idPhase, int columnNumber);

        [OperationContract]
        void Delete(PhaseBullet phaseBullet);

        [OperationContract]
        void DeleteByID(int idPhaseBullet);

        [OperationContract]
        void SaveColumnBullets(List<PhaseBullet> bullets);
    }
}
