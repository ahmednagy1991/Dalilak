//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DalilakProject.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hospital
    {
        public long Id { get; set; }
        public string HospitalName { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Nullable<long> FK_Category { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public Nullable<long> FK_AreaId { get; set; }
        public string Website { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual Category Category { get; set; }
    }
}
