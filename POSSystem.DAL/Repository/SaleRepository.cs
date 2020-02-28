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
                    var result = db.Sales.Add(sale);
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
