using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using POSSystem.DAL.Repository;
using POSSystem.Models;
using static POSSystem.DAL.Repository.ProductRepository;

namespace POSSystem.Controllers
{
    public class ProductController : Controller
    {
        public ProductRepository productRepository = new ProductRepository();
        // GET: Product
        [Authorize]
        public async Task<ActionResult> Index()
        {
            IndexView indexView = new IndexView()
            {
                ProductsList = await GetConsolidatedProductList(),
                Sale = new SaleView()
            };
            return View(indexView);
        }
        public ActionResult AddProductToSale(int id)
        {
            List<SalesProductView> salesProducts = Session["ProductSaleList"] as List<SalesProductView>;
            var _saleProduct = salesProducts.Where(s => s.ID == id).FirstOrDefault();
            _saleProduct.AvailableQuantity -= 1;
            _saleProduct.Quantity += 1;
            _saleProduct.Total = _saleProduct.Quantity * _saleProduct.Price;
            Session["ProductSaleList"] = salesProducts;
            IndexView viewModel = new IndexView()
            {
                ProductsList = salesProducts,
                Sale = new SaleView()
            };
            return PartialView("_Page", viewModel);
        }
        public ActionResult DecrementQuantity(int id)
        {
            List<SalesProductView> salesProducts = Session["ProductSaleList"] as List<SalesProductView>;
            var _saleProduct = salesProducts.Where(s => s.ID == id).FirstOrDefault();
            _saleProduct.Quantity -= 1;
            _saleProduct.AvailableQuantity += 1;
            if (_saleProduct.Quantity == 0)
            {
                salesProducts.Remove(_saleProduct);
            }
            else
            {
                _saleProduct.Total = _saleProduct.Quantity * _saleProduct.Price;
            }
            Session["ProductSaleList"] = salesProducts;
            IndexView viewModel = new IndexView()
            {
                ProductsList = salesProducts,
                Sale = new SaleView()
            };
            return PartialView("_Page", viewModel);
        }
        public async Task<ActionResult> CancelSale()
        {
            IndexView viewModel = new IndexView()
            {
                ProductsList = await GetConsolidatedProductList(),
                Sale = new SaleView()
            };
            return PartialView("_Page", viewModel);
        }

        [NonAction]
        public async Task<List<SalesProductView>> GetConsolidatedProductList()
        {
            var productList = await productRepository.GetProductList();
            var products = new List<ProductView>();
            Session["ProductSaleList"] = new List<SalesProductView>();

            foreach (var product in productList)
            {
                (Session["ProductSaleList"] as List<SalesProductView>).Add(new SalesProductView()
                {
                    ID = product.ID,
                    AvailableQuantity = product.AvailableQuantity,
                    Name = product.Name,
                    Price = product.UnitPrice,
                    Quantity = 0,
                    Total = 0,
                    Image = product.Image,
                    Category = product.Category
                });
            }
            return Session["ProductSaleList"] as List<SalesProductView>;
        }
    }
}