using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace LicenseManagementMVC.ViewModel
{
    public class SoftwareVm
    {
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        public int SoftwareId { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Software")]
        public string SoftwareName { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Type")]
        public int SoftwareTypeId { get; set; }

        [Required(ErrorMessage = "*Required")]
        public List<SelectListItem> SoftwareTypeList = new List<SelectListItem>();

        [Required(ErrorMessage = "*Required")]
        public List<SelectListItem> ProductList = new List<SelectListItem>();


    }
}
