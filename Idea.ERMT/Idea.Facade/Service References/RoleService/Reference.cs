﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Idea.Facade.RoleService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RoleService.IRoleService")]
    public interface IRoleService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService/GetAll", ReplyAction="http://tempuri.org/IRoleService/GetAllResponse")]
        System.Collections.Generic.List<Idea.Entities.Role> GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService/GetAll", ReplyAction="http://tempuri.org/IRoleService/GetAllResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.Role>> GetAllAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRoleServiceChannel : Idea.Facade.RoleService.IRoleService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RoleServiceClient : System.ServiceModel.ClientBase<Idea.Facade.RoleService.IRoleService>, Idea.Facade.RoleService.IRoleService {
        
        public RoleServiceClient() {
        }
        
        public RoleServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RoleServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RoleServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RoleServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<Idea.Entities.Role> GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.Role>> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
    }
}