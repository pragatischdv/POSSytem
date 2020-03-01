using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class SaleView
    {
        public long InvoiceNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public Double VAT { get; set; }
        public Double Discount { get; set; }
        public Double SubTotal { get; set; }
        public Double Total { get; set; }
        public List<int> ProductIDs { get; set; }

    }
}