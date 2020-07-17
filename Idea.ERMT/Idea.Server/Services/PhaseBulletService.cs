using System.Collections.Generic;
using Idea.Business;
using Idea.Entities;

namespace Idea.Server
{
    public class PhaseBulletService : IPhaseBulletService
    {
        public List<PhaseBullet> GetAll()
        {
            return (PhaseBulletManager.GetAll());
        }

        public List<PhaseBullet> GetByPhaseAndColumn(int idPhase, int columnNumber)
        {
            return (PhaseBulletManager.GetByPhaseAndColumn(idPhase, columnNumber));
        }

        public void Delete(PhaseBullet phaseBullet)
        {
            PhaseBulletManager.Delete(phaseBullet);
        }

        public void DeleteByID(int idPhaseBullet)
        {
            PhaseBulletManager.Delete(idPhaseBullet);
        }

        public void SaveColumnBullets(List<PhaseBullet> bullets)
        {
            PhaseBulletManager.SaveColumnBullets(bullets);
        }
    }
}
