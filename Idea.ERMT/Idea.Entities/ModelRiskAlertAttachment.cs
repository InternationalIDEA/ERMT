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
    [KnownType(typeof(ModelRiskAlert))]
    public partial class ModelRiskAlertAttachment
    {
        public ModelRiskAlertAttachment()
        {
    		OnCreated();
        }
    
    	partial void OnCreated();
    
    
    	[DataMember]
        public int IDModelRiskAlertAttachment { get; set; }
    
    	[DataMember]
        public int IDModelRiskAlert { get; set; }
    
    	[DataMember]
        public string AttachmentFile { get; set; }
    
    	[DataMember]
        public System.DateTime DateCreated { get; set; }
    
    	[DataMember]
        public System.DateTime DateModified { get; set; }
    
    
    	[DataMember]
        public virtual ModelRiskAlert ModelRiskAlert { get; set; }
    }
}
