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
    [KnownType(typeof(ModelRiskAlertPhase))]
    [KnownType(typeof(PhaseBullet))]
    public partial class Phase
    {
        public Phase()
        {
            this.ModelRiskAlertPhases = new HashSet<ModelRiskAlertPhase>();
            this.PhaseBullets = new HashSet<PhaseBullet>();
    		OnCreated();
        }
    
    	partial void OnCreated();
    
    
    	[DataMember]
        public int IDPhase { get; set; }
    
    	[DataMember]
        public string Title { get; set; }
    
    	[DataMember]
        public string Column1Text { get; set; }
    
    	[DataMember]
        public string Column2Text { get; set; }
    
    	[DataMember]
        public string Column3Text { get; set; }
    
    	[DataMember]
        public string PractitionersTips { get; set; }
    
    	[DataMember]
        public System.DateTime DateCreated { get; set; }
    
    	[DataMember]
        public System.DateTime DateModified { get; set; }
    
    
    	[DataMember]
        public virtual ICollection<ModelRiskAlertPhase> ModelRiskAlertPhases { get; set; }
    
    	[DataMember]
        public virtual ICollection<PhaseBullet> PhaseBullets { get; set; }
    }
}
