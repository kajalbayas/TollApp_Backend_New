//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TollApp_Backend.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.PaymentHistories = new HashSet<PaymentHistory>();
            this.UserVehicles = new HashSet<UserVehicle>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public Nullable<int> TollId { get; set; }
        public Nullable<int> RouteId { get; set; }
        public Nullable<decimal> Balance_Amount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentHistory> PaymentHistories { get; set; }
        public virtual Route Route { get; set; }
        public virtual Toll Toll { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserVehicle> UserVehicles { get; set; }
    }
}
