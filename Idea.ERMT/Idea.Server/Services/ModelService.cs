using System.Collections.Generic;
using Idea.Business;
using Idea.Entities;

namespace Idea.Server
{
    public class ModelService : IModelService
    {
        public List<Model> GetAll()
        {
            return ModelManager.GetAll();
        }

        public Model Get(int idModel)
        {
            return (ModelManager.Get(idModel));
        }

        public Model Save(Model model)
        {
            return (ModelManager.Save((model)));
        }

        public List<Model> GetByRegion(int idRegion)
        {
            return (ModelManager.GetByRegion(idRegion));
        }

        public void Delete(Model model)
        {
            ModelManager.Delete((model));
        }

        public List<Model> GetByName(string modelName)
        {
            return (ModelManager.GetByName(modelName));
        }

        public List<Model> GetByRegions(List<Region> regions)
        {
            return ModelManager.GetByRegions(regions);
        }
    }
}
