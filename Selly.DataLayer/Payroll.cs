//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Selly.DataLayer
{
    using System;
    using System.Collections.Generic;
    using Selly.DataLayer.Interfaces;
    
    public partial class Payroll : ISinglePkDataAccessObject
    {
        public System.Guid Id { get; set; }
        public System.Guid ClientId { get; set; }
        public System.Guid OrderId { get; set; }
        public System.DateTime Date { get; set; }
        public double Value { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Client Client { get; set; }
    }
}
