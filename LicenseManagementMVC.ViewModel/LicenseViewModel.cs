using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LicenseManagementMVC.ViewModel
{
    public class LicenseViewModel
    {
        public List<SelectListItem> Product = new List<SelectListItem>();
        public List<SelectListItem> SoftwareType = new List<SelectListItem>();
        public List<SelectListItem> Software = new List<SelectListItem>();
        public int ProductId { get; set; }
        public int SoftwareTypeId { get; set; }
        public int SoftwareId { get; set; }
        public int LicenseCount { get; set; }
        public string Key { get; set; }

    }
}
