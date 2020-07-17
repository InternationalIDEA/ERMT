using System;
using System.Collections.Generic;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Server
{
    [ServiceContract]
    public interface IPhaseService
    {
        [OperationContract]
        List<Phase> GetAll();

        [OperationContract]
        Phase Save(Phase phase);

        [OperationContract]
        void GenerateAllFiles(Phase phase, String culture);

        [OperationContract]
        Phase Get(int idPhase);

    }
}
