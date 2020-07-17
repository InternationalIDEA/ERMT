using System.Collections.Generic;
using Idea.Business;
using Idea.Entities;

namespace Idea.Server
{
    public class FactorService : IFactorService
    {
        public bool Validate(Factor factor)
        {
            return FactorManager.ValidateRequiredFields(factor);
        }

        public void Save(Factor factor)
        {
            FactorManager.Save(factor);
        }

        public List<Factor> GetAll()
        {
            List<Factor> result = FactorManager.GetAll();
            return result;
        }

        public Factor Get(int idFactor)
        {
            return FactorManager.GetById(idFactor);
        }

        public void Delete(Factor factor)
        {
            FactorManager.Delete(factor);
        }
    }
}
