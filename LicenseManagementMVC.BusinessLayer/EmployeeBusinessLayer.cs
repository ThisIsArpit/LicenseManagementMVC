// /*****************************************************************************************************************
//  * Project Name  :  LicenseManagementMVC
//  * File Name  :   EmployeeBusinessLayer.cs   
//  * Description :  Contains all business related manipulations for employee.
//  * Created By :   Jeevan Chhetri
//  * Created Date :  14-04-2017
//  * Modified By :  Tipu Ali Khan
//  * Last Modified Date :  28-04-2017
//  ****************************************************************************************************************/
using LicenseManagementMVC.Data.Model;
using Business = LicenseManagementMVC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using LicenseManagementMVC.BusinessEntities;
using LicenseManagementMVC.ViewModel;
namespace LicenseManagementMVC.BusinessLayer
{
     public class EmployeeBusinessLayer
    {   /// <summary>
        /// This method gets all locations ..
        /// </summary>
        /// <returns>It returns list of locations.</returns>
        public List<LicenseManagementMVC.BusinessEntities.Location> GetLocations()
        {
            List<BusinessEntities.Location> locations = new List<BusinessEntities.Location>();
            try
            {
                var check = new LicenseManagementMVCEntities().Locations.ToList();
                locations.AddRange(check.Select(l => new BusinessEntities.Location()
                {
                    LocationId=l.LocationId,
                    LocationName=l.LocationName
                }
                ));
            }
            catch (Exception exp)
            {
               //TODO
            }
            return locations;
        }
         /// <summary>
         /// To retrieve all roles from database.
         /// </summary>
         /// <returns>It returns List of Roles from database.</returns>
        public List<LicenseManagementMVC.BusinessEntities.Role> GetRoles()
        {
            List<BusinessEntities.Role> roles = new List<BusinessEntities.Role>();
            try
            {
                var check = new LicenseManagementMVCEntities().Roles.ToList();
                roles.AddRange(check.Select(r=>new BusinessEntities.Role(){
                    RoleId=r.RoleId,
                    RoleName=r.RoleName
                })
                );
             }
            catch (Exception exp)
            {
              //TODO
            }
            return roles;
        }
         
         /// <summary>
         /// To save new Employee
         /// </summary>
         /// <param name="employee">Employee Details of new Employee.</param>
         /// <returns>It returns true if Employee successfully added.</returns>
         public ResultStatus SaveEmployee(LicenseManagementMVC.BusinessEntities.Employee employee)
         {
              try
             {
                  using(LicenseManagementMVCEntities salesDal=new LicenseManagementMVCEntities ()){
                     Data.Model.Employee emp = new Data.Model.Employee()
                     {
                         FirstName = employee.FirstName,
                         LastName = employee.LastName,
                         Email = employee.Email,
                         Password = new LoginBusinessLayer().EncodePassword(employee.Password),
                         LocationId = employee.LocationId,
                         RoleId = employee.RoleId,
                         IsReleased = false,
                         ReleaseDate=null,
                         JoiningDate = employee.JoiningDate??DateTime.Now
                      };
                     if (new LicenseManagementMVCEntities().Employees.Any(o => o.Email == emp.Email)) return ResultStatus.AlreadyExist;
                  salesDal.Employees.Add(emp);
                  salesDal.SaveChanges();
                 return ResultStatus.Success;
             }
              }catch(Exception exp){
                 //TODO
                 return ResultStatus.ConnectionError;
             }
         }
         /// <summary>
         /// This method helps in checking if email already exists or not.
         /// </summary>
         /// <param name="email">Email provided by the user</param>
         /// <returns></returns>
         public ResultStatus CheckEmail(string email)
         {
             if (new LicenseManagementMVCEntities().Employees.Any(o => o.Email ==email)) return ResultStatus.AlreadyExist;
             return ResultStatus.Success;
         }
         /// <summary>
         /// Updates row in employee table by matching Employee Id.
         /// </summary>
         /// <param name="e"></param>
         /// <returns></returns>
                     
         public bool SaveUpdatedEmployee(UserProfile e)
         {
             if (e != null)
             {
                 using (Data.Model.LicenseManagementMVCEntities obj = new LicenseManagementMVCEntities())
                 {
                     int empId = 6;//Request.QueryString("EmployeeId");
                     var employeeList = obj.Employees.FirstOrDefault(x => x.EmployeeId == empId);
                     if (employeeList != null)
                     {
                         employeeList.FirstName = e.FirstName;
                         employeeList.LastName = e.LastName;
                         employeeList.Email = e.Email;
                         employeeList.RoleId = e.RoleId;
                         employeeList.LocationId = e.LocationId;
                     }
                     try
                     {
                         obj.SaveChanges();
                         return true;
                     }
                     catch (Exception)
                     {
                         //TODO log exception
                     }
                 }
             }
             return false;
         }
         /// <summary>
         /// Get Employee data according to employee Id.
         /// </summary>
         /// <returns>Employee properties</returns>
         public Business.Employee FetchEmployeeDetail(int id)
         {
             using (Data.Model.LicenseManagementMVCEntities obj = new LicenseManagementMVCEntities())
             {
                 var emp = from u in obj.Employees where (u.EmployeeId == id) select u;
                 var x = emp.SingleOrDefault();
                 Business.Employee empDetail = new Business.Employee()
                 {
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     Email = x.Email,
                     LocationId = x.LocationId,
                     RoleId = x.RoleId,
                     JoiningDate = x.JoiningDate,
                     ReleaseDate = x.ReleaseDate,
                     IsReleased = x.IsReleased
                 };
                 return empDetail;
             }
         }

         /// <summary>
         /// This method will populate employee records in a grid on page load
         /// </summary>
         /// <param name="objEmployee"></param>
         /// <returns>Returns a list of employees</returns>
         public List<EmployeeVm> GetEmployeeDetails(string element, string selectedColumn)
         {
             using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
             {
                 EmployeeVm objEmp = new EmployeeVm();
                 List<EmployeeVm> employeeList = new List<EmployeeVm>();

                 var employeeDataList = from e in DbContext.Employees
                                        join
                                        l in DbContext.Locations on e.LocationId equals l.LocationId
                                        join
                                        r in DbContext.Roles on e.RoleId equals r.RoleId
                                        select new
                                        {
                                            empId = e.EmployeeId,
                                            firstName = e.FirstName,
                                            lastName = e.LastName,
                                            location = l.LocationName,
                                            role = r.RoleName,
                                            joiningDate = e.JoiningDate,
                                            releaseDate = e.ReleaseDate,
                                            email = e.Email,
                                            locationId = e.LocationId,
                                            roleId = e.RoleId,
                                            isReleased = e.IsReleased

                                        };
              
                 switch (selectedColumn)
                 {
                     case "EmployeeId":
                         {
                             int empId;
                             bool check = Int32.TryParse(element, out empId);
                             employeeDataList = employeeDataList.Where(e => e.empId == empId);
                             break;
                         }
                     case "FirstName":
                         {
                             employeeDataList = employeeDataList.Where(e => e.firstName.StartsWith(element));
                             break;
                         }
                     case "LastName":
                         {
                             employeeDataList = employeeDataList.Where(e => e.lastName.StartsWith(element));
                             break;    
                         }

                     case "Email":
                         {
                             employeeDataList = employeeDataList.Where(e => e.email.StartsWith(element));
                             break;  
                         }

                     case "IsReleased":
                         {

                             employeeDataList = employeeDataList.Where(e => e.isReleased == Boolean.Parse(element));
                             break;  
                         }

                     case "LocationName":
                         {
                             int locationId;
                             bool check = Int32.TryParse(element, out locationId);
                             employeeDataList = employeeDataList.Where(e => e.locationId == locationId);
                            break;  
                         }

                     case "RoleName":
                         {
                             int roleId;
                             bool check = Int32.TryParse(element, out roleId);
                             employeeDataList = employeeDataList.Where(e => e.roleId == roleId);
                             break;  
                         }

                     case "JoiningDate":
                         {
                             employeeDataList = employeeDataList.Where(e => e.joiningDate == DateTime.Parse(element));
                             break;
                         }

                     case "ReleaseDate":
                         {
                             employeeDataList = employeeDataList.Where(e => e.releaseDate == DateTime.Parse(element));
                             break;  
                         }
                 }

                 foreach (var e in employeeDataList)
                 {
                     objEmp = new EmployeeVm()
                     {
                         EmployeeId = e.empId,
                         FirstName = e.firstName,
                         LastName = e.lastName,
                         Email = e.email,
                         JoiningDate = e.joiningDate,
                         ReleaseDate = e.releaseDate,
                         LocationName = e.location,
                         RoleName = e.role,
                         RoleId = e.roleId,
                         LocationId = e.locationId,
                         IsReleased = e.isReleased
                     };
                     employeeList.Add(objEmp);
                 }
                 return employeeList;
             }
         }

         /// <summary>
         /// This method will Load employee name on employee field 
         /// </summary>
         /// <param name="objSoftware">Object containing SystemAllocationId </param>
         /// <returns>Returns an object holding employee name</returns>
         public SoftwareAllocationVm LoadAndDisableEmployee(SoftwareAllocationVm objSoftware)
         {
             using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
             {
                 var result = (from e in DbContext.Employees
                               join
                               sa in DbContext.SystemAllocations
                               on e.EmployeeId equals sa.EmployeeId
                               where sa.SystemAllocationId == objSoftware.SystemAllocationId
                               select new
                               {
                                   EmployeeName = e.FirstName + " " + e.LastName,
                                   empId = e.EmployeeId,
                                   systemId = sa.SystemAllocationId
                               }).FirstOrDefault();

                 SoftwareAllocationVm objSoft = new SoftwareAllocationVm();
                 objSoft = new SoftwareAllocationVm()
                 {
                     EmployeeName = result.EmployeeName,
                     EmployeeId = result.empId,
                     SystemAllocationId = result.systemId
                 };
                 return objSoft;
             }
         }


         /// <summary>
         /// This method will save the changes made by admin to employee details
         /// </summary>
         /// <param name="objEmp">Object holdling EmployeeId</param>
         /// <returns>Returns ResultStatus enum which reflect the status of operation performed</returns>
         public ResultStatus SaveEditRecord(EmployeeVm objEmp)
         {
             try
             {
                 using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
                 {
                     try
                     {
                         var employee = DbContext.Employees.FirstOrDefault(x => x.EmployeeId == objEmp.EmployeeId);
                         if (employee != null)
                         {
                             employee.FirstName = objEmp.FirstName;
                             employee.LastName = objEmp.LastName;
                             employee.Email = objEmp.Email;
                             employee.RoleId = objEmp.RoleId;
                             employee.LocationId = objEmp.LocationId;
                             employee.JoiningDate = objEmp.JoiningDate;
                             employee.ReleaseDate = objEmp.ReleaseDate;
                             employee.IsReleased = objEmp.IsReleased;
                             DbContext.SaveChanges();
                         }
                         else
                         {
                             return ResultStatus.QueryNotExecuted;
                         }

                     }
                     catch (Exception)
                     {
                         return ResultStatus.QueryNotExecuted;
                     }
                 }
             }
             catch (Exception)
             {
                 return ResultStatus.ConnectionError;
             }
             return ResultStatus.Success;
         }

         /// <summary>
         /// This method will add new employee to database 
         /// </summary>
         /// <param name="objEmp">Object holding all the details of new employee</param>
         /// <returns>Returns ResultStatus enum which reflect the status of operation performed</returns>
         public ResultStatus SaveAddRecord(EmployeeVm objEmp)
         {
             try
             {
                 using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
                 {
                     try
                     {

                         Data.Model.Employee employee = new Data.Model.Employee();

                         if (objEmp != null)
                         {
                             employee.FirstName = objEmp.FirstName;
                             employee.LastName = objEmp.LastName;
                             employee.Email = objEmp.Email;
                             employee.RoleId = objEmp.RoleId;
                             employee.LocationId = objEmp.LocationId;
                             employee.JoiningDate = objEmp.JoiningDate;
                             employee.ReleaseDate = objEmp.ReleaseDate;
                             employee.IsReleased = objEmp.IsReleased;
                             BusinessLayer.LoginBusinessLayer objLogin = new LoginBusinessLayer();
                             employee.Password = objLogin.EncodePassword("mindfire");

                             DbContext.Employees.Add(employee);
                             int result = DbContext.SaveChanges();
                             if (result < 1)
                             {
                                 return ResultStatus.QueryNotExecuted;
                             }
                         }
                         else
                         {
                             return ResultStatus.QueryNotExecuted;
                         }

                     }
                     catch (Exception)
                     {
                         return ResultStatus.QueryNotExecuted;
                     }
                 }
             }
             catch (Exception)
             {
                 return ResultStatus.ConnectionError;
             }
             return ResultStatus.Success;
         }


         /// <summary>
         /// This method will delete employee details
         /// </summary>
         /// <param name="objEmp">Object holdling EmployeeId</param>
         /// <returns>Returns ResultStatus enum which reflect the status of operation performed</returns>
         public ResultStatus DeleteRecord(EmployeeVm objEmp)
         {
             try
             {
                 using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
                 {
                     try
                     {
                         var employee = DbContext.Employees.FirstOrDefault(x => x.EmployeeId == objEmp.EmployeeId);
                         if (employee != null)
                         {
                             DbContext.Employees.Remove(employee);
                             DbContext.SaveChanges();
                         }
                         else
                         {
                             return ResultStatus.QueryNotExecuted;
                         }
                     }
                     catch (Exception)
                     {
                         return ResultStatus.QueryNotExecuted;
                     }
                 }
             }
             catch (Exception)
             {
                 return ResultStatus.ConnectionError;
             }
             return ResultStatus.Success;

         }

         /// <summary>
         /// This method will fill all the Employees in an autocomplete textbox
         /// </summary>
         /// <returns>Returns a list of Employees</returns>
         public List<BusinessEntities.Employee> PopulateEmployee(string prefix)
         {
             using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
             {
                 List<BusinessEntities.Employee> employeeList = new List<BusinessEntities.Employee>();
                 List<Data.Model.Employee> employeeListData = new List<Data.Model.Employee>();
                 employeeListData = (from e in DbContext.Employees
                                     where e.FirstName.StartsWith(prefix)
                                     select e).ToList();

                 BusinessEntities.Employee objEmp = new BusinessEntities.Employee();
                 foreach (var e in employeeListData)
                 {
                     objEmp = new BusinessEntities.Employee()
                     {
                         EmployeeId = e.EmployeeId,
                         FirstName = e.FirstName,
                         LastName = e.LastName,
                         Email = e.Email
                     };
                     employeeList.Add(objEmp);
                 }
                 return employeeList;
             }
         }

         /// <summary>
         /// This method returns a list of employee having a particular Software 
         /// </summary>
         /// <param name="software">Object holding SoftwareId</param>
         /// <returns>Returns a list of employee</returns>
         public List<EmployeeVm> GetEmployee(BusinessEntities.Software software)
         {
             using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
             {
                 var results = from s in DbContext.Softwares
                               join l in DbContext.Licenses
                               on s.SoftwareId equals l.SoftwareId
                               join si in DbContext.SystemInstallations
                               on l.LicenseId equals si.LicenseId
                               join sa in DbContext.SystemAllocations
                               on si.SystemAllocationId equals sa.SystemAllocationId
                               join e in DbContext.Employees
                               on sa.EmployeeId equals e.EmployeeId
                               where s.SoftwareId == software.SoftwareId
                               select new
                               {
                                   firstName = e.FirstName,
                                   lastName = e.LastName,
                                   empId = e.EmployeeId,
                                   allotedDate=si.InstallationDate
                               };

                 List<EmployeeVm> employeeList = new List<EmployeeVm>();
                EmployeeVm employee = new EmployeeVm();

                 foreach (var emp in results)
                 {
                     employee = new EmployeeVm()
                     {
                         FirstName = emp.firstName,
                         LastName = emp.lastName,
                         EmployeeId = emp.empId,
                         SoftwareInstallationDate=emp.allotedDate
                     };
                     employeeList.Add(employee);
                 }
                 return employeeList;
             }
         }

    }
}
