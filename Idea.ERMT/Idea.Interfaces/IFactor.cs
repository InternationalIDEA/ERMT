using System;
using System.Collections.Generic;

namespace Idea.Interfaces
{
    public interface IFactor
    {
        int IdFactor { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int ScaleMin { get; set; }
        int ScaleMax { get; set; }
        decimal Interval { get; set; }
        string HtmlDocument { get; set; }
        bool InternalFactor { get; set; }
        bool CumulativeFactor { get; set; }
        string Introduction { get; set; }
        string EmpiricalCases { get; set; }
        string ObservableIndicators { get; set; }
        string DataCollection { get; set; }
        string Questionnaire { get; set; }
        int? SortOrder { get; set; }
        string Color { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        ICollection<IModelFactor> ModelFactors { get; set; }
    }
}
