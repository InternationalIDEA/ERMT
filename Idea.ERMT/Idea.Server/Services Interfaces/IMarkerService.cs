using System;
using System.Collections.Generic;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Server
{
    [ServiceContract]
    public interface IMarkerService
    {
        [OperationContract]
        List<Marker> GetAll();

        [OperationContract]
        Marker Get(int idMarker);

        [OperationContract]
        Marker Save(Marker marker);

        [OperationContract]
        void Delete(Marker marker);

        [OperationContract]
        List<Marker> GetByName(string name);

        [OperationContract]
        List<Marker> GetByModelId(int idModel);

        [OperationContract]
        List<Marker> GetByMarkerTypeId(int idMarkerType);

        [OperationContract]
        List<Marker> GetByModelIdAndMarkerTypeIdAndFromAndTo(int idModel, int? idMarkerType, DateTime from, DateTime to);

        [OperationContract]
        DateTime? GetMinDateByModelID(int idModel);

        [OperationContract]
        DateTime? GetMaxDateByModelID(int idModel);
    }
}
