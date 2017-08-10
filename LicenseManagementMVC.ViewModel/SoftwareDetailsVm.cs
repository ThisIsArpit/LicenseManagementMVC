using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LicenseManagementMVC.ViewModel
{
   public  class SoftwareDetailsVm
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string LastAllocation { get; set; }
        public int SystemInstallationId { get; set; }
       [Display(Name = "Product")]
       public int ProductId { get; set; }
       public int SoftwareId { get; set; }
       public string ProductName { get; set; }
       public string SoftwareName { get; set; }
       public int TotalCount { get; set; }
       public int AllocatedCount { get; set; }
       public string SoftwareType { get; set; }
       public int SoftwareTypeId { get; set; }
       public string key { get; set; }
       [Required(ErrorMessage = "*Required")]
       public List<SelectListItem> ProductList = new List<SelectListItem>();


       public DateTime AllotedDate { get; set; }
       public DateTime? ReleaseDate { get; set; }
       public int SystemAllocationId { get; set; }

   }
}
