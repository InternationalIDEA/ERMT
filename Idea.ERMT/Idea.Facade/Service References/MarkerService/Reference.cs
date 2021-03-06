﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Idea.Facade.MarkerService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MarkerService.IMarkerService")]
    public interface IMarkerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/GetAll", ReplyAction="http://tempuri.org/IMarkerService/GetAllResponse")]
        System.Collections.Generic.List<Idea.Entities.Marker> GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/GetAll", ReplyAction="http://tempuri.org/IMarkerService/GetAllResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.Marker>> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/Get", ReplyAction="http://tempuri.org/IMarkerService/GetResponse")]
        Idea.Entities.Marker Get(int idMarker);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/Get", ReplyAction="http://tempuri.org/IMarkerService/GetResponse")]
        System.Threading.Tasks.Task<Idea.Entities.Marker> GetAsync(int idMarker);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/Save", ReplyAction="http://tempuri.org/IMarkerService/SaveResponse")]
        Idea.Entities.Marker Save(Idea.Entities.Marker marker);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/Save", ReplyAction="http://tempuri.org/IMarkerService/SaveResponse")]
        System.Threading.Tasks.Task<Idea.Entities.Marker> SaveAsync(Idea.Entities.Marker marker);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/Delete", ReplyAction="http://tempuri.org/IMarkerService/DeleteResponse")]
        void Delete(Idea.Entities.Marker marker);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/Delete", ReplyAction="http://tempuri.org/IMarkerService/DeleteResponse")]
        System.Threading.Tasks.Task DeleteAsync(Idea.Entities.Marker marker);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/GetByName", ReplyAction="http://tempuri.org/IMarkerService/GetByNameResponse")]
        System.Collections.Generic.List<Idea.Entities.Marker> GetByName(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/GetByName", ReplyAction="http://tempuri.org/IMarkerService/GetByNameResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.Marker>> GetByNameAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/GetByModelId", ReplyAction="http://tempuri.org/IMarkerService/GetByModelIdResponse")]
        System.Collections.Generic.List<Idea.Entities.Marker> GetByModelId(int idModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/GetByModelId", ReplyAction="http://tempuri.org/IMarkerService/GetByModelIdResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.Marker>> GetByModelIdAsync(int idModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/GetByMarkerTypeId", ReplyAction="http://tempuri.org/IMarkerService/GetByMarkerTypeIdResponse")]
        System.Collections.Generic.List<Idea.Entities.Marker> GetByMarkerTypeId(int idMarkerType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/GetByMarkerTypeId", ReplyAction="http://tempuri.org/IMarkerService/GetByMarkerTypeIdResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.Marker>> GetByMarkerTypeIdAsync(int idMarkerType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/GetByModelIdAndMarkerTypeIdAndFromAndTo", ReplyAction="http://tempuri.org/IMarkerService/GetByModelIdAndMarkerTypeIdAndFromAndToResponse" +
            "")]
        System.Collections.Generic.List<Idea.Entities.Marker> GetByModelIdAndMarkerTypeIdAndFromAndTo(int idModel, System.Nullable<int> idMarkerType, System.DateTime from, System.DateTime to);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/GetByModelIdAndMarkerTypeIdAndFromAndTo", ReplyAction="http://tempuri.org/IMarkerService/GetByModelIdAndMarkerTypeIdAndFromAndToResponse" +
            "")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.Marker>> GetByModelIdAndMarkerTypeIdAndFromAndToAsync(int idModel, System.Nullable<int> idMarkerType, System.DateTime from, System.DateTime to);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/GetMinDateByModelID", ReplyAction="http://tempuri.org/IMarkerService/GetMinDateByModelIDResponse")]
        System.Nullable<System.DateTime> GetMinDateByModelID(int idModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/GetMinDateByModelID", ReplyAction="http://tempuri.org/IMarkerService/GetMinDateByModelIDResponse")]
        System.Threading.Tasks.Task<System.Nullable<System.DateTime>> GetMinDateByModelIDAsync(int idModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/GetMaxDateByModelID", ReplyAction="http://tempuri.org/IMarkerService/GetMaxDateByModelIDResponse")]
        System.Nullable<System.DateTime> GetMaxDateByModelID(int idModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMarkerService/GetMaxDateByModelID", ReplyAction="http://tempuri.org/IMarkerService/GetMaxDateByModelIDResponse")]
        System.Threading.Tasks.Task<System.Nullable<System.DateTime>> GetMaxDateByModelIDAsync(int idModel);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMarkerServiceChannel : Idea.Facade.MarkerService.IMarkerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MarkerServiceClient : System.ServiceModel.ClientBase<Idea.Facade.MarkerService.IMarkerService>, Idea.Facade.MarkerService.IMarkerService {
        
        public MarkerServiceClient() {
        }
        
        public MarkerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MarkerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MarkerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MarkerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<Idea.Entities.Marker> GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.Marker>> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public Idea.Entities.Marker Get(int idMarker) {
            return base.Channel.Get(idMarker);
        }
        
        public System.Threading.Tasks.Task<Idea.Entities.Marker> GetAsync(int idMarker) {
            return base.Channel.GetAsync(idMarker);
        }
        
        public Idea.Entities.Marker Save(Idea.Entities.Marker marker) {
            return base.Channel.Save(marker);
        }
        
        public System.Threading.Tasks.Task<Idea.Entities.Marker> SaveAsync(Idea.Entities.Marker marker) {
            return base.Channel.SaveAsync(marker);
        }
        
        public void Delete(Idea.Entities.Marker marker) {
            base.Channel.Delete(marker);
        }
        
        public System.Threading.Tasks.Task DeleteAsync(Idea.Entities.Marker marker) {
            return base.Channel.DeleteAsync(marker);
        }
        
        public System.Collections.Generic.List<Idea.Entities.Marker> GetByName(string name) {
            return base.Channel.GetByName(name);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.Marker>> GetByNameAsync(string name) {
            return base.Channel.GetByNameAsync(name);
        }
        
        public System.Collections.Generic.List<Idea.Entities.Marker> GetByModelId(int idModel) {
            return base.Channel.GetByModelId(idModel);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.Marker>> GetByModelIdAsync(int idModel) {
            return base.Channel.GetByModelIdAsync(idModel);
        }
        
        public System.Collections.Generic.List<Idea.Entities.Marker> GetByMarkerTypeId(int idMarkerType) {
            return base.Channel.GetByMarkerTypeId(idMarkerType);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.Marker>> GetByMarkerTypeIdAsync(int idMarkerType) {
            return base.Channel.GetByMarkerTypeIdAsync(idMarkerType);
        }
        
        public System.Collections.Generic.List<Idea.Entities.Marker> GetByModelIdAndMarkerTypeIdAndFromAndTo(int idModel, System.Nullable<int> idMarkerType, System.DateTime from, System.DateTime to) {
            return base.Channel.GetByModelIdAndMarkerTypeIdAndFromAndTo(idModel, idMarkerType, from, to);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Idea.Entities.Marker>> GetByModelIdAndMarkerTypeIdAndFromAndToAsync(int idModel, System.Nullable<int> idMarkerType, System.DateTime from, System.DateTime to) {
            return base.Channel.GetByModelIdAndMarkerTypeIdAndFromAndToAsync(idModel, idMarkerType, from, to);
        }
        
        public System.Nullable<System.DateTime> GetMinDateByModelID(int idModel) {
            return base.Channel.GetMinDateByModelID(idModel);
        }
        
        public System.Threading.Tasks.Task<System.Nullable<System.DateTime>> GetMinDateByModelIDAsync(int idModel) {
            return base.Channel.GetMinDateByModelIDAsync(idModel);
        }
        
        public System.Nullable<System.DateTime> GetMaxDateByModelID(int idModel) {
            return base.Channel.GetMaxDateByModelID(idModel);
        }
        
        public System.Threading.Tasks.Task<System.Nullable<System.DateTime>> GetMaxDateByModelIDAsync(int idModel) {
            return base.Channel.GetMaxDateByModelIDAsync(idModel);
        }
    }
}
