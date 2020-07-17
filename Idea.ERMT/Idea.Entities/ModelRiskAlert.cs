//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Idea.Entities
{
    using System;
    using System.Collections.Generic;
    
    using System.Runtime.Serialization;
    
    [DataContract]
    [KnownType(typeof(Model))]
    [KnownType(typeof(ModelRiskAlertAttachment))]
    [KnownType(typeof(ModelRiskAlertPhase))]
    [KnownType(typeof(ModelRiskAlertRegion))]
    public partial class ModelRiskAlert
    {
        public ModelRiskAlert()
        {
            this.ModelRiskAlertAttachments = new HashSet<ModelRiskAlertAttachment>();
            this.ModelRiskAlertPhases = new HashSet<ModelRiskAlertPhase>();
            this.ModelRiskAlertRegions = new HashSet<ModelRiskAlertRegion>();
    		OnCreated();
        }
    
    	partial void OnCreated();
    
    
    	[DataMember]
        public int IDModelRiskAlert { get; set; }
    
    	[DataMember]
        public int IDModel { get; set; }
    
    	[DataMember]
        public string Code { get; set; }
    
    	[DataMember]
        public string Title { get; set; }
    
    	[DataMember]
        public System.DateTime DateFrom { get; set; }
    
    	[DataMember]
        public System.DateTime DateTo { get; set; }
    
    	[DataMember]
        public string RiskDescription { get; set; }
    
    	[DataMember]
        public string Action { get; set; }
    
    	[DataMember]
        public string Result { get; set; }
    
    	[DataMember]
        public bool Active { get; set; }
    
    	[DataMember]
        public System.DateTime DateCreated { get; set; }
    
    	[DataMember]
        public System.DateTime DateModified { get; set; }
    
    
    	[DataMember]
        public virtual Model Model { get; set; }
    
    	[DataMember]
        public virtual ICollection<ModelRiskAlertAttachment> ModelRiskAlertAttachments { get; set; }
    
    	[DataMember]
        public virtual ICollection<ModelRiskAlertPhase> ModelRiskAlertPhases { get; set; }
    
    	[DataMember]
        public virtual ICollection<ModelRiskAlertRegion> ModelRiskAlertRegions { get; set; }
    }
}