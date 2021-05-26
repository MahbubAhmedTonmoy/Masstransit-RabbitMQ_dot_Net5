using System;

namespace Shared.Models.Models
{
    public class CustomerOrder
    {
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDescription { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
