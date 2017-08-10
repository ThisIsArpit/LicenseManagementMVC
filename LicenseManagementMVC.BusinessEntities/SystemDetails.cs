using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementMVC.BusinessEntities
{
    public class SystemDetails
    {
        public int SystemDetailsId { get; set; }
        public int BrandId { get; set; }
        public int SystemTypeId { get; set; }
        public string Series { get; set; }
        public string Processor { get; set; }
        public string HardDiskSpace { get; set; }
        public int Count { get; set; }
    }
}
