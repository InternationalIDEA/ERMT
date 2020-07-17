using System;
using Idea.ERMT.UserControls;

namespace Idea.ERMT
{
    public static class ControlCache
    {
        private static Start _start;
        private static RiskMapping _riskMapping;


        public static Start StartInstance
        {
            get { return _start ?? (_start = new Start()); }
        }

        public static RiskMapping RiskMappingInstance
        {
            get { return _riskMapping ?? (_riskMapping = new RiskMapping()) ; }
        }

        public static Boolean RiskMappingInstanceCreated 
        {
            get { return _riskMapping != null; }
        }
    }
}
