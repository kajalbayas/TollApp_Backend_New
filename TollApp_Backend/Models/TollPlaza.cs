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
    
    public partial class TollPlaza
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TollPlaza()
        {
            this.Tolls = new HashSet<Toll>();
        }
    
        public int Id { get; set; }
        public string Location { get; set; }
        public Nullable<int> RouteId { get; set; }
    
        public virtual Route Route { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Toll> Tolls { get; set; }
    }
}