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
    [KnownType(typeof(Role))]
    public partial class User
    {
        public User()
        {
    		OnCreated();
        }
    
    	partial void OnCreated();
    
    
    	[DataMember]
        public int IDUser { get; set; }
    
    	[DataMember]
        public int IDRole { get; set; }
    
    	[DataMember]
        public string Name { get; set; }
    
    	[DataMember]
        public string Lastname { get; set; }
    
    	[DataMember]
        public string Username { get; set; }
    
    	[DataMember]
        public string Password { get; set; }
    
    	[DataMember]
        public string Email { get; set; }
    
    	[DataMember]
        public System.DateTime DateCreated { get; set; }
    
    	[DataMember]
        public System.DateTime DateModified { get; set; }
    
    
    	[DataMember]
        public virtual Role Role { get; set; }
    }
}
