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
    public partial class Report
    {
        public Report()
        {
    		OnCreated();
        }
    
    	partial void OnCreated();
    
    
    	[DataMember]
        public int IDReport { get; set; }
    
    	[DataMember]
        public int IDModel { get; set; }
    
    	[DataMember]
        public string Name { get; set; }
    
    	[DataMember]
        public string Parameters { get; set; }
    
    	[DataMember]
        public string Markers { get; set; }
    
    	[DataMember]
        public int Type { get; set; }
    
    	[DataMember]
        public System.DateTime DateCreated { get; set; }
    
    	[DataMember]
        public System.DateTime DateModified { get; set; }
    
    
    	[DataMember]
        public virtual Model Model { get; set; }
    }
}
