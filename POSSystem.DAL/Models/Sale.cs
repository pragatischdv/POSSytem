using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSSystem.DAL.Models
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InvoiceNumber { get; set; }
        public string EmployeeUsername { get; set; }
        public DateTime CreatedOn { get; set; }
        public Double VAT { get; set; }
        public Double TotalPrice { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
