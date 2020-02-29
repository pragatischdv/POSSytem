using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace POSSystem.DAL.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new POSDBInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Models.Sale> Sales { get; set; }
        public DbSet<Models.Product> Products { get; set; }
    }
        public class POSDBInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                var _list = new List<Models.Product> {
                new Models.Product {Name = "Clothing", UnitPrice = 2.0, AvailableQuantity=10, Category="Clothing", Image="clothing.jpg" },
                new Models.Product {Name = "Computer Repair", UnitPrice = 3.0, AvailableQuantity=10, Category="Electronic", Image="computer_repair.jpeg" },
                new Models.Product {Name = "Computer", UnitPrice = 4.0, AvailableQuantity=10, Category="Electronic", Image="comuter.jpg" },
                new Models.Product {Name = "Git Folding", UnitPrice = 5.0, AvailableQuantity=10, Category="Stationary", Image="gift_folding.jpeg" },
                new Models.Product {Name = "Grapes", UnitPrice = 6.0, AvailableQuantity=10, Category="Fruit", Image="grapes.jpeg" },
                new Models.Product {Name = "Headphone", UnitPrice = 7.0, AvailableQuantity=10, Category="Electronic", Image="headphone.jpg" },
                new Models.Product {Name = "Jacket", UnitPrice = 2.0, AvailableQuantity=10, Category="Clothing", Image="jacket.jpeg" },
                new Models.Product {Name = "Jacket for Men", UnitPrice = 3.0, AvailableQuantity=10, Category="Clothing", Image="jacket_men.jpg" },
                new Models.Product {Name = "Keyboard", UnitPrice = 4.0, AvailableQuantity=10, Category="Electronic", Image="keyboard.jpg" },
                new Models.Product {Name = "Kiwi", UnitPrice = 5.0, AvailableQuantity=10, Category="Fruit", Image="kiwi.jpeg" },
                new Models.Product {Name = "Motherboard", UnitPrice = 6.0, AvailableQuantity=10, Category="Electronic", Image="motherboard.jpg" },
                new Models.Product {Name = "Mouse", UnitPrice = 7.0, AvailableQuantity=10, Category="Electronic", Image="mouse.jpg" },
                new Models.Product {Name = "Notebook", UnitPrice = 8.0, AvailableQuantity=10, Category="Stationary", Image="notebook.jpg" },
                new Models.Product {Name = "Strawberries", UnitPrice = 2.0, AvailableQuantity=10, Category="Fruit", Image="strawberries.jpeg" },
                new Models.Product {Name = "Tie", UnitPrice = 3.0, AvailableQuantity=10, Category="Clothing", Image="tie.jpeg" },
                };

                foreach (var item in _list)
                {
                    var _product = new Models.Product()
                    {
                        Name = item.Name,
                        UnitPrice = item.UnitPrice,
                        AvailableQuantity = item.AvailableQuantity,
                        Category = item.Category,
                        Image = item.Image
                    };
                context.Products.Add(_product);
                }
            context.SaveChanges();
            base.Seed(context);
            }
        }
}
