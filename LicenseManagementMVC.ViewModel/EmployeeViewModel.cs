// /*****************************************************************************************************************
//  * Project Name  :  LicenseManagementMVC.ViewModel
//  * File Name  :  EmployeeViewModel.cs   
//  * Description :  View available for user in other pages(like home page).
//  * Created By :   Jeevan Chhetri
//  * Created Date :  14-april-2017
//  * Modified By : 
//  * Last Modified Date :  
//  ****************************************************************************************************************/
using System;
using System.ComponentModel.DataAnnotations;

namespace LicenseManagementMVC.ViewModel
{
    public class EmployeeViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string LocationName { get; set; }
        public string RoleName { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public bool IsReleased { get; set; }
    }
}
