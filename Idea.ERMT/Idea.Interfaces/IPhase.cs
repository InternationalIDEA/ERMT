using System;
using System.Collections.Generic;

namespace Idea.Interfaces
{
    public interface IPhase
    {
        int IDPhase { get; set; }
        string Title { get; set; }
        string Column1Text { get; set; }
        string Column2Text { get; set; }
        string Column3Text { get; set; }
        string PractitionersTips { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        ICollection<IModelRiskAlertPhase> ModelRiskAlertPhases { get; set; }
        ICollection<IPhaseBullet> PhaseBullets { get; set; }

        ICollection<IPhaseBullet> Column1Bullets { get; set; }
        ICollection<IPhaseBullet> Column2Bullets { get; set; }
        ICollection<IPhaseBullet> Column3Bullets { get; set; }
    }
}
