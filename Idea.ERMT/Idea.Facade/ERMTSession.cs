using Idea.Entities;

namespace Idea.Facade
{
    public sealed class ERMTSession
    {
        ERMTSession()
        {
        }

        public static ERMTSession Instance
        {
            get
            {
                return Nested.instance;
            }
        }
        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly ERMTSession instance = new ERMTSession();
        }

        private User _currentUser;

        private Region _currentModelMainRegion;

        private string _serverAddress;

        public string ServerAddress
        {
            set { _serverAddress = value; }
            get { return string.IsNullOrEmpty(_serverAddress) ? "localhost" : _serverAddress; }
        }

        private Model _currentModel;

        public Model CurrentModel {
            get
            {
                return _currentModel;
            }
            set
            {
                _currentModel = value;
                _currentModelMainRegion = null;
            }
        }

        public Report CurrentReport { get; set; }

        public Region CurrentModelMainRegion
        {
            get
            {
                if (_currentModelMainRegion == null && CurrentModel != null)
                {
                    _currentModelMainRegion = RegionHelper.Get(CurrentModel.IDRegion);
                }
                return _currentModelMainRegion;
            }
        }

        public User CurrentUser
        {
            get { return _currentUser; }
        }

        public void LoginUser(User user)
        {
            _currentUser = user;
        }

        public void LogoutUser()
        {
            _currentUser = null;
        }
    }
}
