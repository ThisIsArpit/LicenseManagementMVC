using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementMVC.BusinessEntities
{
    public class Software
    {
        public int SoftwareId { get; set; }
        public string SoftwareName { get; set; }
        public int SoftwareTypeId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
