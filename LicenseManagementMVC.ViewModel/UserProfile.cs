//  *****************************************************************************************************************
//  * Project Name  :  LicenseManagementMVC
//  * File Name  :   ProfileUpdateController.cs   
//  * Description :  This class hold property for employee.
//  * Created By :   Tipu Ali Khan
//  * Created Date :  20-Apr-2017
//  * Modified By :  
//  * Last Modified Date : 
//  ****************************************************************************************************************
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace LicenseManagementMVC.ViewModel
{
    public class UserProfile
    {
        public int EmployeeId { get; set; }
        [Display(Name = "First Name :")]
        [Required(ErrorMessage="First name is required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name :")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Display(Name = "Email :")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage="Invalid email")]
        public string Email { get; set; }
        [Display(Name = "Locatoin :")]
        [Required(ErrorMessage = "Location is required")]
        public int LocationId { get; set; }
        [Display(Name = "Role :")]
        [Required(ErrorMessage = "Role is required")]
        public int RoleId { get; set; }
        [Display(Name = "Joining Date :")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? JoiningDate { get; set; }
        [Display(Name = "Release Date :")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }
        [Display(Name = "Released \t")]
        public bool IsReleased { get; set; }
        public List<SelectListItem> Location = new List<SelectListItem>();
        public List<SelectListItem> Role = new List<SelectListItem>();

    }
}
