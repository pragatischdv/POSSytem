using POSSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POSSystem.DAL.Repository;
using POSSystem.DAL.Models;
using System.Threading.Tasks;

namespace POSSystem.Controllers
{
    public class SaleController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> AddSale(SaleView saleView)
        {
            SaleRepository saleRepository = new SaleRepository();
            ProductRepository productRepository = new ProductRepository();
            
            Sale sale = new Sale() {
                CreatedOn = DateTime.Now,
                EmployeeUsername = User.Identity.Name,
                TotalPrice = saleView.Total,
                VAT = saleView.VAT
            };
            foreach (var id in saleView.ProductIDs)
            {
                sale.Products.Add(await productRepository.GetProduct(id));
            }
            var result = await saleRepository.AddSale(sale);
            Session.Remove("ProductSaleList");
            return PartialView("", result);
        }
    }
}