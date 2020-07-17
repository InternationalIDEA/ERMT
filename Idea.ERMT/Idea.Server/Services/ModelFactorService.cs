using System;
using System.Collections.Generic;
using System.Linq;
using Idea.Business;
using Idea.Entities;

namespace Idea.Server
{
    public class ModelFactorService : IModelFactorService
    {
        public bool Validate(ModelFactor modelFactor)
        {
            return ModelFactorManager.ValidateRequiredFields((modelFactor));
        }

        public void Save(ModelFactor modelFactor)
        {
            ModelFactorManager.Save((modelFactor));
        }

        public List<ModelFactor> GetAll()
        {
            return (ModelFactorManager.GetAll().ToList());
        }


        public List<ModelFactor> GetByModel(int idModel)
        {
            return (ModelFactorManager.GetByModel(idModel).ToList());
        }

        public ModelFactor Get(int idModelFactor)
        {
            return (ModelFactorManager.Get(idModelFactor));
        }

        public List<ModelFactor> GetModelFactorWithDataAvailable(int idModel, int idRegion)
        {
            return (ModelFactorManager.GetModelFactorWithDataAvailable(idModel, idRegion).ToList());
        }

        public List<ModelFactor> GetByModelAndFactorId(int idModel, int idFactor)
        {
            return (ModelFactorManager.GetByModelAndFactorId(idModel, idFactor).ToList());
        }

        public void Delete(ModelFactor modelFactor)
        {
            ModelFactorManager.Delete(modelFactor);
        }
    }
}
