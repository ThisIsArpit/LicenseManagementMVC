using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicenseManagementMVC.BusinessEntities
{
    public class Product
    {
        //[Table("Product")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        //public int SoftwareCount { get; set; }
    }
}
