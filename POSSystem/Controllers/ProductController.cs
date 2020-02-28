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
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> AddProductToSale(int id)
        {
            if(await productRepository.EditProduct(id, ProductRepository.Command.IncQuantity))
            {
                return Json(true);
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

        public async Task<ActionResult> CancelSale(List<CancelSaleView> canceledProducts)
        {
            foreach(var canceledProduct in canceledProducts)
            {
                var result = await productRepository.CancelSaleForProductId(canceledProduct.ProductId, canceledProduct.Quantity);
            }
            return PartialView("");
        }
    }
}