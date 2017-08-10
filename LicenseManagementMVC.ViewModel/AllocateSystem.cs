using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LicenseManagementMVC.ViewModel
{
    public class AllocateSystem
    {


        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Employee")]
        public string EmployeeName { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "Brand is required")]
        public int BrandId { get; set; }

        public List<SelectListItem> Brand = new List<SelectListItem>();

        public int SystemAllocationId { get; set; }

        public int SystemDetailsId { get; set; }

        public int EmployeeId { get; set; }

        [Display(Name = "Alloted Date")]
        [Required(ErrorMessage = "Date is required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? AllotedDate { get; set; }
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }
        [Display(Name = "Is Released")]
        public bool IsReleased { get; set; }
         [Display(Name = "Remark")]
        public string Remarks { get; set; }
        public List<BusinessEntities.SystemDetails> systemDetail = new List<BusinessEntities.SystemDetails>();
   }
}
