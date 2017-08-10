using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementMVC.BusinessEntities
{
    public class License
    {
        public int LicenseId { get; set; }
        public int SoftwareId { get; set; }
        public string SoftwareName { get; set; }
        public int LicenseCount { get; set; }
        public string LicenseKey { get; set; }
    }
}
