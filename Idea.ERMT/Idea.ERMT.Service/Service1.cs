using System.ServiceProcess;
using System.ServiceModel;
using Idea.Server;

namespace Idea.ERMT.Service
{
    public partial class Service1 : ServiceBase
    {
        ServiceHost _modelHost, _modelFactorHost, _factorHost, _documentHost, _modelFactorDataHost, _regionHost, _roleHost, _userHost, 
            _reportServiceHost, _markerServiceHost, _markerTypeServiceHost, _phaseServiceHost, _phaseBulletServiceHost, _modelRiskAlertServiceHost;


        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Start();

        }

        public void Start()
        {
            _modelHost = new ServiceHost(typeof(ModelService));
            _modelHost.Open();

            _modelFactorHost = new ServiceHost(typeof(ModelFactorService));
            _modelFactorHost.Open();

            _factorHost = new ServiceHost(typeof(FactorService));
            _factorHost.Open();

            _documentHost = new ServiceHost(typeof(DocumentService));
            _documentHost.Open();

            _modelFactorDataHost = new ServiceHost(typeof(ModelFactorDataService));
            _modelFactorDataHost.Open();

            _regionHost = new ServiceHost(typeof(RegionService));
            _regionHost.Open();

            _roleHost = new ServiceHost(typeof(RoleService));
            _roleHost.Open();

            _userHost = new ServiceHost(typeof(UserService));
            _userHost.Open();

            _reportServiceHost = new ServiceHost(typeof(ReportService));
            _reportServiceHost.Open();

            _markerServiceHost= new ServiceHost(typeof(MarkerService));
            _markerServiceHost.Open();
            
            _markerTypeServiceHost = new ServiceHost(typeof(MarkerTypeService));
            _markerTypeServiceHost.Open();

            _phaseServiceHost = new ServiceHost(typeof(PhaseService));
            _phaseServiceHost.Open();

            _phaseBulletServiceHost = new ServiceHost(typeof(PhaseBulletService));
            _phaseBulletServiceHost.Open();

            _modelRiskAlertServiceHost = new ServiceHost(typeof(ModelRiskAlertService));
            _modelRiskAlertServiceHost.Open();

        }

        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        protected override void OnStop()
        {
            if (_modelHost != null)
                _modelHost.Close();

            if (_modelFactorHost != null)
                _modelFactorHost.Close();

            if (_factorHost != null)
                _factorHost.Close();

            if (_documentHost != null)
                _documentHost.Close();

            if (_modelFactorDataHost != null)
                _modelFactorDataHost.Close();

            if (_regionHost != null)
                _regionHost.Close();

            if (_roleHost != null)
                _roleHost.Close();

            if (_userHost != null)
                _userHost.Close();

            if (_reportServiceHost!= null)
                _reportServiceHost.Close();

            if (_markerServiceHost != null)
                _markerServiceHost.Close();

            if (_markerTypeServiceHost != null)
                _markerTypeServiceHost.Close();

            if (_phaseServiceHost != null)
                _phaseServiceHost.Close();

            if (_phaseBulletServiceHost != null)
                _phaseBulletServiceHost.Close();

            if (_modelRiskAlertServiceHost != null)
                _modelRiskAlertServiceHost.Close();
        }
    }
}
