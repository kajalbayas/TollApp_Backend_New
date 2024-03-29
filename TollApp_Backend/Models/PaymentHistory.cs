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
    
    public partial class PaymentHistory
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> ExitLocId { get; set; }
        public Nullable<int> VehicleTypeId { get; set; }
        public string VehicleNumber { get; set; }
        public Nullable<int> RouteId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string TranscationId { get; set; }
        public Nullable<decimal> Amount { get; set; }
    
        public virtual TollPlaza TollPlaza { get; set; }
        public virtual Route Route { get; set; }
        public virtual User User { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
