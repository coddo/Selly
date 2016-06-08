using System;

namespace Selly.Website.Models
{
    public class MakePaymentModel
    {
        public Guid OrderId { get; set; }

        public Guid ClientId { get; set; }
    }
}