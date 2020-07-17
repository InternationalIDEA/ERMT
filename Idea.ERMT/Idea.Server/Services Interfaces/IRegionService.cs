using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Server
{
    [ServiceContract]
    public interface IRegionService
    {
        [OperationContract]
        List<Region> GetAll();

        [OperationContract]
        List<Region> GetChilds(int idRegion);

        [OperationContract]
        Region Get(int idRegion);

        [OperationContract]
        Region Save(Region region);

        [OperationContract]
        void Delete(Region region);

        [OperationContract]
        List<Region> GetAllChilds(int idRegion);

        [OperationContract]
        List<Region> GetChildsAtLevel(int idRegion, int level);

        [OperationContract]
        int GetRegionLevel(int idRegion);

        [OperationContract]
        Region GetWorld();

        [OperationContract]
        Region GetRegionByShapeFileAndIndex(FileInfo shapeFileInfo, int index);

        [OperationContract]
        List<Region> GetAllRelated(int idRegion);
        
        [OperationContract]
        string GetFeatureIDsToExclude(int idRegion);
    }
}
