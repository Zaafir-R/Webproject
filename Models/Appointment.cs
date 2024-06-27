//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public System.DateTime DateandTime { get; set; }
        public string AdminComment { get; set; }
        public string UserComment { get; set; }
        public System.DateTime LastModified { get; set; }
        public int AppointmentStatusId { get; set; }
        public int UserId { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public Nullable<bool> isDeleted { get; set; }
    
        public virtual AppointmentStatu AppointmentStatu { get; set; }
        public virtual Service Service { get; set; }
        public virtual User User { get; set; }
    }
}
