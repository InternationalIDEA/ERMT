﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Idea.Facade.PhaseBulletService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PhaseBulletService.IPhaseBulletService")]
    public interface IPhaseBulletService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPhaseBulletService/GetAll", ReplyAction="http://tempuri.org/IPhaseBulletService/GetAllResponse")]
        System.Collections.Generic.List<Idea.Entities.PhaseBullet> GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPhaseBulletService/GetAll", ReplyAction="http://tempuri.org/IPhaseBulletService/GetAllResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.PhaseBullet>> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPhaseBulletService/GetByPhaseAndColumn", ReplyAction="http://tempuri.org/IPhaseBulletService/GetByPhaseAndColumnResponse")]
        System.Collections.Generic.List<Idea.Entities.PhaseBullet> GetByPhaseAndColumn(int idPhase, int columnNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPhaseBulletService/GetByPhaseAndColumn", ReplyAction="http://tempuri.org/IPhaseBulletService/GetByPhaseAndColumnResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.PhaseBullet>> GetByPhaseAndColumnAsync(int idPhase, int columnNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPhaseBulletService/Delete", ReplyAction="http://tempuri.org/IPhaseBulletService/DeleteResponse")]
        void Delete(Idea.Entities.PhaseBullet phaseBullet);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPhaseBulletService/Delete", ReplyAction="http://tempuri.org/IPhaseBulletService/DeleteResponse")]
        System.Threading.Tasks.Task DeleteAsync(Idea.Entities.PhaseBullet phaseBullet);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPhaseBulletService/DeleteByID", ReplyAction="http://tempuri.org/IPhaseBulletService/DeleteByIDResponse")]
        void DeleteByID(int idPhaseBullet);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPhaseBulletService/DeleteByID", ReplyAction="http://tempuri.org/IPhaseBulletService/DeleteByIDResponse")]
        System.Threading.Tasks.Task DeleteByIDAsync(int idPhaseBullet);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPhaseBulletService/SaveColumnBullets", ReplyAction="http://tempuri.org/IPhaseBulletService/SaveColumnBulletsResponse")]
        void SaveColumnBullets(System.Collections.Generic.List<Idea.Entities.PhaseBullet> bullets);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPhaseBulletService/SaveColumnBullets", ReplyAction="http://tempuri.org/IPhaseBulletService/SaveColumnBulletsResponse")]
        System.Threading.Tasks.Task SaveColumnBulletsAsync(System.Collections.Generic.List<Idea.Entities.PhaseBullet> bullets);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPhaseBulletServiceChannel : Idea.Facade.PhaseBulletService.IPhaseBulletService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PhaseBulletServiceClient : System.ServiceModel.ClientBase<Idea.Facade.PhaseBulletService.IPhaseBulletService>, Idea.Facade.PhaseBulletService.IPhaseBulletService {
        
        public PhaseBulletServiceClient() {
        }
        
        public PhaseBulletServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PhaseBulletServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PhaseBulletServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PhaseBulletServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<Idea.Entities.PhaseBullet> GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.PhaseBullet>> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public System.Collections.Generic.List<Idea.Entities.PhaseBullet> GetByPhaseAndColumn(int idPhase, int columnNumber) {
            return base.Channel.GetByPhaseAndColumn(idPhase, columnNumber);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.PhaseBullet>> GetByPhaseAndColumnAsync(int idPhase, int columnNumber) {
            return base.Channel.GetByPhaseAndColumnAsync(idPhase, columnNumber);
        }
        
        public void Delete(Idea.Entities.PhaseBullet phaseBullet) {
            base.Channel.Delete(phaseBullet);
        }
        
        public System.Threading.Tasks.Task DeleteAsync(Idea.Entities.PhaseBullet phaseBullet) {
            return base.Channel.DeleteAsync(phaseBullet);
        }
        
        public void DeleteByID(int idPhaseBullet) {
            base.Channel.DeleteByID(idPhaseBullet);
        }
        
        public System.Threading.Tasks.Task DeleteByIDAsync(int idPhaseBullet) {
            return base.Channel.DeleteByIDAsync(idPhaseBullet);
        }
        
        public void SaveColumnBullets(System.Collections.Generic.List<Idea.Entities.PhaseBullet> bullets) {
            base.Channel.SaveColumnBullets(bullets);
        }
        
        public System.Threading.Tasks.Task SaveColumnBulletsAsync(System.Collections.Generic.List<Idea.Entities.PhaseBullet> bullets) {
            return base.Channel.SaveColumnBulletsAsync(bullets);
        }
    }
}