using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementMVC.BusinessEntities
{
    public enum ResultStatus
    {
        Success,
        ConnectionError,
        AlreadyExist,
        QueryNotExecuted
    }
}
