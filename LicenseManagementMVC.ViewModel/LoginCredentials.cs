// /*****************************************************************************************************************
//  * Project Name  :  LicenseManagementMVC.Web
//  * File Name  :   LoginCredentials.cs   
//  * Description :  View model of user credentials.
//  * Created By :   Arpit Mishra
//  * Created Date :  12-april-2017
//  * Modified By : 
//  * Last Modified Date :  
//  ****************************************************************************************************************/


//using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LicenseManagementMVC.ViewModel
{
    /// <summary>
    /// Login Credential view model
    /// </summary>
   public class LoginCredentials
    {
        [Required(ErrorMessage = "  User Name Required")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "  Password Required")]
        [StringLength(500,MinimumLength = 6, ErrorMessage = " ")] 
        public string Password { get; set; }
    }
}
