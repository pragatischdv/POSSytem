using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSSystem.DAL.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public Double UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public ICollection<Sale> Sales { get; set; }

    }
}
