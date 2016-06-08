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
    
    public partial class Order : Interfaces.ISinglePkModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.Payrolls = new HashSet<Payroll>();
            this.OrderItems = new HashSet<OrderItem>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid ClientId { get; set; }
        public System.Guid CurrencyId { get; set; }
        public System.DateTime Date { get; set; }
        public int SaleType { get; set; }
        public int Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payroll> Payrolls { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Client Client { get; set; }
    }
}
