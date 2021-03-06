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
    [KnownType(typeof(MarkerType))]
    [KnownType(typeof(Model))]
    public partial class Marker
    {
        public Marker()
        {
    		OnCreated();
        }
    
    	partial void OnCreated();
    
    
    	[DataMember]
        public int IDMarker { get; set; }
    
    	[DataMember]
        public int IDMarkerType { get; set; }
    
    	[DataMember]
        public int IDModel { get; set; }
    
    	[DataMember]
        public string Name { get; set; }
    
    	[DataMember]
        public decimal Latitude { get; set; }
    
    	[DataMember]
        public decimal Longitude { get; set; }
    
    	[DataMember]
        public System.DateTime DateFrom { get; set; }
    
    	[DataMember]
        public System.DateTime DateTo { get; set; }
    
    	[DataMember]
        public string Color { get; set; }
    
    	[DataMember]
        public string Description { get; set; }
    
    	[DataMember]
        public System.DateTime DateCreated { get; set; }
    
    	[DataMember]
        public System.DateTime DateModified { get; set; }
    
    
    	[DataMember]
        public virtual MarkerType MarkerType { get; set; }
    
    	[DataMember]
        public virtual Model Model { get; set; }
    }
}
