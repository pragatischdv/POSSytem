using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSSystem.DAL.Models;
using POSSystem.DAL.Identity;
using System.Data.Entity;

namespace POSSystem.DAL.Repository
{
    public class ProductRepository
    {
        public enum Command {IncQuantity, DecQuantity};
        public async Task<ICollection<Product>> GetProductList(string catagory="")
        {
            using(var db = new ApplicationDbContext())
            {
                var productList = db.Products.Where(x => x.AvailableQuantity > 0);
                if (string.IsNullOrEmpty(catagory))
                {
                    return await productList.ToListAsync();
                }
                return await productList.Where(x => x.Category == catagory).ToListAsync();
            }
        }
        public async Task<bool> AddProduct(Product product)
        {
            using (var db = new ApplicationDbContext())
            {
                try
                {
                    db.Products.Add(product);
                    await db.SaveChangesAsync();
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
            }
        }
        public async Task<Product> EditProduct(int id, Command command)
        {
            using(var db = new ApplicationDbContext())
            {
                try
                {
                    var product = await GetProduct(id);
                    if (command.Equals(Command.IncQuantity))
                    {
                        if (product.AvailableQuantity == 0) throw new Exception();
                        product.AvailableQuantity = product.AvailableQuantity - 1;
                    }
                    else if (command.Equals(Command.DecQuantity))
                    {
                        product.AvailableQuantity = product.AvailableQuantity + 1;
                    }
                    await db.SaveChangesAsync();
                    return product;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            using(var db = new ApplicationDbContext())
            {
                try
                {
                    var product = await GetProduct(id);
                    db.Products.Remove(product);
                    await db.SaveChangesAsync();
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
            }
        }

        public async Task<bool> UpdateSaleForProduct(Product product)
        {
            using(var db = new ApplicationDbContext())
            {
                try
                {
                    db.Entry(product).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
            }
        }

        public async Task<Product> GetProduct(int id)
        {
            using(var db = new ApplicationDbContext())
            {
                return await db.Products.FindAsync(id);
            }
        }
        
    }
}
