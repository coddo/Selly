//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Selly.Models
{
    using System;
    using System.Collections.Generic;
    using Selly.Models.Interfaces;
    
    public partial class ValueAddedTax : ISinglePkModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ValueAddedTax()
        {
            this.Products = new HashSet<Product>();
        }
    
        public System.Guid Id { get; set; }
        public double Value { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
