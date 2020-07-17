using System.Collections.Generic;
using System.Linq;
using Idea.Entities;
using System;
using System.ServiceModel;

namespace Idea.Facade
{
    public class RoleHelper
    {
        private static RoleService.IRoleService _service;
        private static RoleService.IRoleService GetService()
        {
            if (_service == null)
            {
                _service = new RoleService.RoleServiceClient();
                Uri uri = new Uri(((ClientBase<RoleService.IRoleService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                ((ClientBase<RoleService.IRoleService>)_service).Endpoint.Address = new EndpointAddress(uri);
            }
            else
            {
                try
                {
                    ((System.ServiceModel.ClientBase<Idea.Facade.RoleService.IRoleService>)(_service)).Close();
                }
                catch
                { }
                finally
                {
                    _service = new RoleService.RoleServiceClient();
                    Uri uri = new Uri(((ClientBase<RoleService.IRoleService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                    ((ClientBase<RoleService.IRoleService>)_service).Endpoint.Address = new EndpointAddress(uri);
                }

            }
            switch (((System.ServiceModel.ClientBase<Idea.Facade.RoleService.IRoleService>)(_service)).State)
            {
                case System.ServiceModel.CommunicationState.Faulted:
                    ((System.ServiceModel.ClientBase<Idea.Facade.RoleService.IRoleService>)(_service)).Abort();
                    _service = new RoleService.RoleServiceClient();
                    break;
                case System.ServiceModel.CommunicationState.Closed:
                case System.ServiceModel.CommunicationState.Closing:
                    _service = new RoleService.RoleServiceClient();
                    break;
            }
            return _service;
        }

        /// <summary>
        /// Returns the list of all Roles (service).
        /// </summary>
        /// <returns></returns>
        public static List<Role> GetAll()
        {
            return (GetService().GetAll()).ToList();
        }
    }
}
