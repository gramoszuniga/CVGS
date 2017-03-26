//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CVGS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            this.PersonEvents = new HashSet<PersonEvent>();
        }
    
        public int eventId { get; set; }
        public int personId { get; set; }
        public string title { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }
        public decimal regFee { get; set; }
        public string venue { get; set; }
        public string notes { get; set; }
    
        public virtual Person Person { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonEvent> PersonEvents { get; set; }
    }
}