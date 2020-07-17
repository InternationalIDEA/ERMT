using System.Collections.Generic;
using System.ServiceModel;
using Idea.Entities;


namespace Idea.Server
{
    [ServiceContract]
    public interface IMarkerTypeService
    {
        [OperationContract]
        List<MarkerType> GetAll();

        [OperationContract]
        MarkerType Get(int idMarkerType);

        [OperationContract]
        MarkerType Save(MarkerType markerType);

        [OperationContract]
        void Delete(MarkerType markerType, bool deleteMarkerImage);

        [OperationContract]
        List<MarkerType> GetByName(string name);
    }
}
