using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LicenseManagementMVC.ViewModel
{
    public class SoftwareAllocationVm
    {
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Employee")]
        public string EmployeeName { get; set; }
       // public  MyProperty { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Software")]
        public string SoftwareName { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "System")]
        public int SystemAllocationId { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Software")]
        public int SoftwareId { get; set; }

        
        [Display(Name = "Alloted date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime AllotedDate { get; set; }
     
       
        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ReleaseDate { get; set; }

       
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Series")]
        public string Series { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Hard Disk Space")]
        public string HDDSpace { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Processor")]
        public string Processor { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Remaks")]
        public string Remarks { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "System Type")]
        public string SystemType { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Brand")]
        public string BrandName { get; set; }


        public Boolean IsReleased { get; set; }
    }
}
