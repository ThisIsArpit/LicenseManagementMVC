// /*****************************************************************************************************************
//  * Project Name  :  LicenseManagementMVC.Web
//  * File Name  :   CreateEmployeeViewModel.cs   
//  * Description :  View available for user in registration page.
//  * Created By :   Jeevan Chhetri
//  * Created Date :  14-april-2017
//  * Modified By : 
//  * Last Modified Date :  
//  ****************************************************************************************************************/
using System;
namespace LicenseManagementMVC.ViewModel
{
   public class CreateEmployeeViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LocationName { get; set; }
        public string RoleName { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool IsReleased { get; set; }
    }
}
