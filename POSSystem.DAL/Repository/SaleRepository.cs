using POSSystem.DAL.Identity;
using POSSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSSystem.DAL.Repository
{
    public class SaleRepository
    {
        public async Task<Sale> AddSale(Sale sale)
        {
            using (var db = new ApplicationDbContext())
            {
                try
                {
                    Sale _sale = new Sale() {
                        CreatedOn = sale.CreatedOn,
                        EmployeeUsername = sale.EmployeeUsername,
                        TotalPrice = sale.TotalPrice,
                        VAT = sale.VAT,
                        Products = new List<Product>()
                    };
                    var products = sale.Products;
                    foreach (var product in products)
                    {
                        _sale.Products.Add(db.Products.Find(product.ID));
                    }
                    var result = db.Sales.Add(_sale);
                    await db.SaveChangesAsync();
                    return result;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
    }
}
