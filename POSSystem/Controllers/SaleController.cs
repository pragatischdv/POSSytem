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

            Sale sale = new Sale()
            {
                CreatedOn = DateTime.Now,
                EmployeeUsername = User.Identity.Name,
                TotalPrice = saleView.Total,
                VAT = saleView.VAT,
                Products = new List<Product>()
            };
            List<SalesProductView> salesProducts = Session["ProductSaleList"] as List<SalesProductView>;
            salesProducts = salesProducts.Where(x => x.Quantity != 0).ToList();
            foreach (var product in salesProducts)
            {
                Product _product = new Product()
                {
                    ID = product.ID,
                    Name = product.Name,
                    UnitPrice = product.Price,
                    Category = product.Category,
                    AvailableQuantity = product.AvailableQuantity,
                    Image = product.Image
                };
                await productRepository.UpdateSaleForProduct(_product);
                sale.Products.Add(_product);
            }
            if (ModelState.IsValid)
            {
                var result = await saleRepository.AddSale(sale);
                return RedirectToAction("GetReciept", result);
            }
            else
            {
                return Json(false);
            }
        }
        public ActionResult GetReciept(Sale sale)
        {
            SaleView saleView = new SaleView()
            {
                CreatedOn = sale.CreatedOn,
                InvoiceNumber = sale.InvoiceNumber,
                Total = sale.TotalPrice,
                VAT = sale.VAT
            };
            return PartialView("_ProcessSale", saleView);
        }
    }
}