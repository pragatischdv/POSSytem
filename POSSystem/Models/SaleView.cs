using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class SaleView
    {
        public decimal VAT { get; set; }
        public decimal Discount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public List<int> ProductIDs { get; set; }
        
    }
}