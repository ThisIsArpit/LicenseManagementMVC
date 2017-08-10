using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LicenseManagementMVC.ViewModel
{
   public class ProductVm
    {
       [Required(ErrorMessage = "*Required")]
       [Display(Name = "Product")]
       public int ProductId { get; set; }

       [Required(ErrorMessage = "*Required")]
       [Display(Name = "Product")]
       public string ProductName { get; set; }

    }
}
