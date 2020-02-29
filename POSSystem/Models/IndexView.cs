using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class IndexView
    {
        public ICollection<ProductView> ProductsList { get; set; }
        public SaleView Sale { get; set; }
    }
}