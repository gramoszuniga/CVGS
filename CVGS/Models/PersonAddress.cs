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
    
    public partial class PersonAddress
    {
        public int personAddressId { get; set; }
        public int personId { get; set; }
        public int addressId { get; set; }
        public string type { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual Person Person { get; set; }
    }
}
