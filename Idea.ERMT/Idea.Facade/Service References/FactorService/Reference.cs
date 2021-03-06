﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Idea.Facade.FactorService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FactorService.IFactorService")]
    public interface IFactorService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFactorService/Validate", ReplyAction="http://tempuri.org/IFactorService/ValidateResponse")]
        bool Validate(Idea.Entities.Factor factor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFactorService/Validate", ReplyAction="http://tempuri.org/IFactorService/ValidateResponse")]
        System.Threading.Tasks.Task<bool> ValidateAsync(Idea.Entities.Factor factor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFactorService/Save", ReplyAction="http://tempuri.org/IFactorService/SaveResponse")]
        void Save(Idea.Entities.Factor factor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFactorService/Save", ReplyAction="http://tempuri.org/IFactorService/SaveResponse")]
        System.Threading.Tasks.Task SaveAsync(Idea.Entities.Factor factor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFactorService/GetAll", ReplyAction="http://tempuri.org/IFactorService/GetAllResponse")]
        System.Collections.Generic.List<Idea.Entities.Factor> GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFactorService/GetAll", ReplyAction="http://tempuri.org/IFactorService/GetAllResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.Factor>> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFactorService/Get", ReplyAction="http://tempuri.org/IFactorService/GetResponse")]
        Idea.Entities.Factor Get(int idFactor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFactorService/Get", ReplyAction="http://tempuri.org/IFactorService/GetResponse")]
        System.Threading.Tasks.Task<Idea.Entities.Factor> GetAsync(int idFactor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFactorService/Delete", ReplyAction="http://tempuri.org/IFactorService/DeleteResponse")]
        void Delete(Idea.Entities.Factor factor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFactorService/Delete", ReplyAction="http://tempuri.org/IFactorService/DeleteResponse")]
        System.Threading.Tasks.Task DeleteAsync(Idea.Entities.Factor factor);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFactorServiceChannel : Idea.Facade.FactorService.IFactorService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FactorServiceClient : System.ServiceModel.ClientBase<Idea.Facade.FactorService.IFactorService>, Idea.Facade.FactorService.IFactorService {
        
        public FactorServiceClient() {
        }
        
        public FactorServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FactorServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FactorServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FactorServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Validate(Idea.Entities.Factor factor) {
            return base.Channel.Validate(factor);
        }
        
        public System.Threading.Tasks.Task<bool> ValidateAsync(Idea.Entities.Factor factor) {
            return base.Channel.ValidateAsync(factor);
        }
        
        public void Save(Idea.Entities.Factor factor) {
            base.Channel.Save(factor);
        }
        
        public System.Threading.Tasks.Task SaveAsync(Idea.Entities.Factor factor) {
            return base.Channel.SaveAsync(factor);
        }
        
        public System.Collections.Generic.List<Idea.Entities.Factor> GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.Factor>> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public Idea.Entities.Factor Get(int idFactor) {
            return base.Channel.Get(idFactor);
        }
        
        public System.Threading.Tasks.Task<Idea.Entities.Factor> GetAsync(int idFactor) {
            return base.Channel.GetAsync(idFactor);
        }
        
        public void Delete(Idea.Entities.Factor factor) {
            base.Channel.Delete(factor);
        }
        
        public System.Threading.Tasks.Task DeleteAsync(Idea.Entities.Factor factor) {
            return base.Channel.DeleteAsync(factor);
        }
    }
}
