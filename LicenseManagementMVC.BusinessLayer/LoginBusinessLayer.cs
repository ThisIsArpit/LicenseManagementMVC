// /*****************************************************************************************************************
//  * Project Name  :  LicenseManagementMVC.Web
//  * File Name  :   Login.cs   
//  * Description :  Accepts request from controller and then manupulate data accordingly
//  * Created By :   Arpit Mishra
//  * Created Date :  12-april-2017
//  * Modified By :  
//  * Last Modified Date : 
//  ****************************************************************************************************************/


using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using LicenseManagementMVC.Data.Model;

namespace LicenseManagementMVC.BusinessLayer
{
    /// <summary>
    /// This class will accept the user credentials and then check the database whether user exists or not . 
    /// </summary>
    public class LoginBusinessLayer
    {
        /// <summary>
        ///  This method will accept the user password and then encrypt it.
        /// </summary>
        /// <param name="password">User Password</param>
        /// <returns>Its returns the encrypted password to the controller</returns>
        public string EncodePassword(string password)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(password);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    password = Convert.ToBase64String(ms.ToArray());
                }
            }
            return password;
        }

        /// <summary>
        /// This method will accept the UserName and password from controller and will check whether it exists in database or not.
        /// </summary>
        /// <param name="userName">UserName of User</param>
        /// <param name="password">Password of User</param>
        /// <returns>It returns object of all the basic information of user to the controller</returns>
        public BusinessEntities.Employee Authenticate(string userName,string password)
        {
            BusinessEntities.Employee objEmployee = new BusinessEntities.Employee();
            string encryptedPassword = EncodePassword(password);
                using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
                {

              //  Employee user = new Employee();
                var user = DbContext.Employees.FirstOrDefault( u => u.Email == userName && u.Password == encryptedPassword);
                    BusinessEntities.Employee emp = new BusinessEntities.Employee();
                  
                         if(user!=null)
                         {
                             emp = new BusinessEntities.Employee()
                             {
                                 EmployeeId = user.EmployeeId,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email,
                                 JoiningDate = user.JoiningDate,
                                 ReleaseDate = user.ReleaseDate,
                                 LocationId = user.LocationId,
                                 RoleId = user.RoleId,
                                 IsReleased = user.IsReleased
                             };
                         }
                 

                     return emp;
                }
           
            }
    }
}