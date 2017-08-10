using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementMVC.BusinessEntities
{
    public class SystemAllocation
    {
        public int SystemAllocationId { get; set; }
        public int SystemDetailsId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? AllotedDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public bool IsReleased { get; set; }
        public string Remarks { get; set; }
    }
}
