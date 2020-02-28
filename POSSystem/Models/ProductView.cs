﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class ProductListView
    {
        public int ID { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
    }
    public class SalesProductView
    {
        public int ID { get; set; }
        public string Name{ get; set; }
        public decimal Price { get; set; }
        public int Qauntity { get; set; }
        public decimal Total { get; set; }
    }
    public class CancelSaleView
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}