// /*****************************************************************************************************************
//  * Project Name  :  LicenseManagementMVC.ViewModel
//  * File Name  :  SystemDetailsViewModel.cs   
//  * Description :  View model for system details.
//  * Created By :   Jeevan Chhetri
//  * Created Date :  24-april-2017
//  * Modified By : 
//  * Last Modified Date :  
//  ****************************************************************************************************************/
using System.Web.Mvc;
using System.Collections.Generic;
namespace LicenseManagementMVC.ViewModel
{
     public class SystemDetailsViewModel
    {
        public List<SelectListItem> Brand = new List<SelectListItem>();
        public List<SelectListItem> SystemType = new List<SelectListItem>();
        public int BrandId { get; set; }
        public int SystemTypeId { get; set; }
        public string Series { get; set; }
        public string Processor { get; set; }
        public int HDDSpace { get; set; }
        public int Count { get; set; }

    }
}
