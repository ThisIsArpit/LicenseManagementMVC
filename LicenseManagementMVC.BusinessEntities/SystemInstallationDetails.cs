using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementMVC.BusinessEntities
{
    public class SystemInstallationDetails
    {
        public int SystemInstallationID { get; set; }
        public int LicenseId { get; set; }
        public int SystemAllocationId { get; set; }
        public DateTime? InstallationDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public bool IsReleased { get; set; }
    }
}
