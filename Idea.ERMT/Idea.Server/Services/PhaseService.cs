using System;
using System.Collections.Generic;
using Idea.Business;
using Idea.Entities;

namespace Idea.Server
{
    public class PhaseService : IPhaseService
    {
        public List<Phase> GetAll()
        {
            return (PhaseManager.GetAll());
        }

        public Phase Save(Phase fase)
        {
            return (PhaseManager.Save((fase)));
        }

        public void GenerateAllFiles(Phase phase, String culture)
        {
            PhaseManager.GenerateAllFiles(phase, culture);
        }

        public Phase Get(int idPhase)
        {
            return PhaseManager.Get(idPhase);
        }
    }
}
