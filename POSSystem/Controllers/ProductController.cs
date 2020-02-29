using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using POSSystem.DAL.Repository;
using POSSystem.Models;

namespace POSSystem.Controllers
{
    public class ProductController : Controller
    {
        public ProductRepository productRepository = new ProductRepository();
        // GET: Product
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var productList = await productRepository.GetProductList();
            IndexView indexView = new IndexView()
            {
                ProductsList = await GetConsolidatedProductList(),
                Sale = new SaleView()
            };
            return View(indexView);
        }
        public async Task<ActionResult> AddProductToSale(int id)
        {
            if(await productRepository.EditProduct(id, ProductRepository.Command.IncQuantity))
            {
                var product = await productRepository.GetProduct(id);
                ViewBag["Product"] = product;
                return PartialView("");
            }
            return Json(false);
        }
        public async Task<JsonResult> DecrementQuantity(int id)
        {
            if (await productRepository.EditProduct(id, ProductRepository.Command.DecQuantity))
            {
                return Json(true);
            }
            return Json(false);
        }

        public async Task<JsonResult> IncrementQuantity(int id)
        {
            if (await productRepository.EditProduct(id, ProductRepository.Command.IncQuantity))
            {
                return Json(true);
            }
            return Json(false);
        }

        public async Task<ActionResult> CancelSale(List<CancelSaleView> canceledProducts)
        {
            foreach(var canceledProduct in canceledProducts)
            {
                var result = await productRepository.CancelSaleForProductId(canceledProduct.ProductId, canceledProduct.Quantity);
            }
            return PartialView("", await GetConsolidatedProductList());
        }

        [NonAction]
        public async Task<List<ProductView>> GetConsolidatedProductList()
        {
            var productList = await productRepository.GetProductList();
            var products = new List<ProductView>();
            foreach (var product in productList)
            {
                products.Add(new ProductView() { ID = product.ID, Name = product.Name, Image = product.Image });
            }
            return products;
        }
    }
}