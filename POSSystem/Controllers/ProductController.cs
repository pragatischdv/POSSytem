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
            var _product = await productRepository.EditProduct(id, ProductRepository.Command.IncQuantity);
            if (_product is null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            SalesProductView _productSale = new SalesProductView()
            {
                ID = _product.ID,
                Name = _product.Name,
                Price = _product.UnitPrice,
                AvailableQuantity = _product.AvailableQuantity,
                Quantity = 1,
                Total = _product.UnitPrice
            };
            if (Session["ProductSaleList"] != null)
            {
                List<SalesProductView> salesProducts = Session["ProductSaleList"] as List<SalesProductView>;
                var _saleProduct = salesProducts.Where(s => s.ID == _product.ID).FirstOrDefault();
                if (_saleProduct != null)
                {
                    _saleProduct.Quantity += 1;
                    _saleProduct.Total = _saleProduct.Quantity * _saleProduct.Price;
                    Session["ProductSaleList"] = salesProducts;
                }
                else
                {
                    (Session["ProductSaleList"] as List<SalesProductView>).Add(_productSale);
                }
            }
            else
            {
                Session["ProductSaleList"] = new List<SalesProductView>();
                (Session["ProductSaleList"] as List<SalesProductView>).Add(_productSale);
            }
            return PartialView("_SaleProductList", Session["ProductSaleList"]);
        }
        public async Task<ActionResult> DecrementQuantity(int id)
        {
            var _product = await productRepository.EditProduct(id, ProductRepository.Command.DecQuantity);
            if (_product is null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            SalesProductView _productSale = new SalesProductView()
            {
                ID = _product.ID,
                Name = _product.Name,
                Price = _product.UnitPrice,
                AvailableQuantity = _product.AvailableQuantity
            };
            List<SalesProductView> salesProducts = Session["ProductSaleList"] as List<SalesProductView>;
            var _saleProduct = salesProducts.Where(s => s.ID == _product.ID).FirstOrDefault();
            if (_saleProduct != null)
            {
                _saleProduct.Quantity -= 1;
                if (_saleProduct.Quantity == 0)
                {
                    salesProducts.Remove(_saleProduct);
                }
                else
                {
                    _saleProduct.Total = _saleProduct.Quantity * _saleProduct.Price;
                }
                Session["ProductSaleList"] = salesProducts;
            }
            return PartialView("_SaleProductList", Session["ProductSaleList"]);
        }
        [HttpPost]
        public async Task<ActionResult> CancelSale(List<CancelSaleView> canceledProducts)
        {
            foreach(var canceledProduct in canceledProducts)
            {
                var result = await productRepository.CancelSaleForProductId(canceledProduct.ProductId, canceledProduct.Quantity);
            }
            Session.Remove("ProductSaleList");
            return PartialView("_ProductList", await GetConsolidatedProductList());
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