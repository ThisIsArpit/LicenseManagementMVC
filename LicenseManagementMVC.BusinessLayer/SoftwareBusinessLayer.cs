// /*****************************************************************************************************************
//  * Project Name  :  LicenseManagementMVC.BusinessLayer
//  * File Name  :   SoftwareBusinessLayer.cs   
//  * Description :  Accepts request from controller and then manupulate Software data accordingly
//  * Created By :   Arpit Mishra
//  * Created Date :  10-april-2017
//  * Modified By :  
//  * Last Modified Date : 
//  ****************************************************************************************************************/

using System.Linq;
using LicenseManagementMVC.Data.Model;
using LicenseManagementMVC.BusinessEntities;
using System;
using LicenseManagementMVC.ViewModel;
using System.Collections.Generic;
using System.Collections;

namespace LicenseManagementMVC.BusinessLayer
{
    /// <summary>
    /// This class is will accept the Product details and manupulate it into the database.
    /// </summary>
    public class SoftwareBusinessLayer
    {

        /// <summary>
        /// This method will check whether software exists in the database or not
        /// </summary>
        /// <param name="objSoftware">object holding software details</param>
        /// <returns>It returns a boolean value</returns>
        public bool IsProductExist(ProductVm objProduct)
        {
            using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
            {

                int isPresent = (from p in DbContext.Products
                                 where ( p.ProductName.ToLower() == objProduct.ProductName.ToLower())
                                 select p).Count();
                if (isPresent != 0)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// This method will accept the Product details and save it into the database.
        /// </summary>
        /// <param name="objProduct">Object containing the details of Product</param>
        /// <returns>Returns the same Object</returns>
        public ResultStatus SaveProduct(ProductVm objProduct)
        {
           if(!IsProductExist(objProduct))
           {
               try
               {
                   using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
                   {
                       try
                       {
                           Data.Model.Product objProductData = new Data.Model.Product();
                           objProductData.ProductName = objProduct.ProductName;
                           DbContext.Products.Add(objProductData);
                           DbContext.SaveChanges();
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
           }
           else
           {
               return ResultStatus.AlreadyExist;
           }

            return ResultStatus.Success;
        }

        /// <summary>
        /// This method will return the list of all the product in the database
        /// </summary>
        /// <returns>Returns the list of all the product</returns>
        public List<BusinessEntities.Product> ShowProduct()
        {
            using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
            {
                    List<BusinessEntities.Product> productList = new List<BusinessEntities.Product>();
                    List<Data.Model.Product> productListData = new List<Data.Model.Product>();
                    productListData = DbContext.Products.ToList();
                    BusinessEntities.Product objProduct = new BusinessEntities.Product();

                foreach(var x in productListData)
                {
                    objProduct = new BusinessEntities.Product();
                    objProduct.ProductName = x.ProductName;
                    objProduct.ProductId = x.ProductId;
                    productList.Add(objProduct);
                }
                    return productList;
            }
        }
        

        /// <summary>
        /// This method will populate product details in a DropDown list
        /// </summary>
        /// <returns>Returns the list of Product details</returns>
        /// List<BusinessEntities.Product>
        public List<BusinessEntities.Product> PopulateProduct()
        
        {
            using(Data.Model.LicenseManagementMVCEntities DbContext=new LicenseManagementMVCEntities())
            {
                List<BusinessEntities.Product> productList = new List<BusinessEntities.Product>();
                List<Data.Model.Product> productListData = new List<Data.Model.Product>();
                productListData = DbContext.Products.ToList();

                BusinessEntities.Product objProduct = new BusinessEntities.Product();
                foreach (var p in productListData)
                {
                    objProduct = new BusinessEntities.Product();
                    objProduct.ProductId = p.ProductId;
                    objProduct.ProductName = p.ProductName;
                    productList.Add(objProduct);
                }
               return productList;
            }
        }


        /// <summary>
        /// This method will populate Software Type details in a DropDown list
        /// </summary>
        /// <returns>Returns the list of Product details</returns>
        /// List<BusinessEntities.Product>
        public List<BusinessEntities.SoftwareType> PopulateSoftwareType()
        {
            using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
            {
                List<BusinessEntities.SoftwareType> typeList = new List<BusinessEntities.SoftwareType>();
                List<Data.Model.SoftwareType> typeListData = new List<Data.Model.SoftwareType>();
                typeListData = DbContext.SoftwareTypes.ToList();

                BusinessEntities.SoftwareType objType = new BusinessEntities.SoftwareType();
                foreach (var p in typeListData)
                {
                    objType = new BusinessEntities.SoftwareType();
                    objType.SoftwareTypeId = p.SoftwareTypeId;
                    objType.Type = p.SoftwareTypeName;
                    typeList.Add(objType);
                }
                return typeList;
            }
        }

        /// <summary>
        /// This method will check whether software exists in the database or not
        /// </summary>
        /// <param name="objSoftware">object holding software details</param>
        /// <returns>It returns a boolean value</returns>
        public bool IsSoftwareExist(BusinessEntities.Software objSoftware)
        {
            using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
            {

               int isPresent =( from s in DbContext.Softwares
                               where (s.ProductId == objSoftware.ProductId && s.SoftwareName.ToLower() == objSoftware.SoftwareName.ToLower() && s.SoftwareTypeId == objSoftware.SoftwareTypeId)
                               select s).Count();
               if (isPresent != 0)
                   return true;
            }
            return false;
        }

        /// <summary>
        /// This method will accept the software details in an object from controller and save it into the database 
        /// </summary>
        /// <param name="objSoftware">Object holding software details</param>
        /// <returns></returns>
        public ResultStatus AddSoftware(BusinessEntities.Software objSoftware)
        {
            if (!IsSoftwareExist(objSoftware))
            {
               try
               {
                   using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
                   {
                       try
                       {
                           Data.Model.Software objSoftwareData = new Data.Model.Software();
                           objSoftwareData.SoftwareName = objSoftware.SoftwareName;
                           objSoftwareData.SoftwareTypeId = objSoftware.SoftwareTypeId;
                           objSoftwareData.ProductId = objSoftware.ProductId;
                           DbContext.Softwares.Add(objSoftwareData);
                           DbContext.SaveChanges();
                       }
                       catch(Exception)
                       {
                           return ResultStatus.QueryNotExecuted;
                       }
                   }
               }
               catch(Exception)
               {
                   return ResultStatus.ConnectionError;
               }
            }
            else
            {
                return ResultStatus.AlreadyExist;
            }
            return ResultStatus.Success;
        }

        /// <summary>
        /// This method will update software details in database
        /// </summary>
        /// <param name="objSoftware">Object holding software details to be updated</param>
        /// <returns>Returns the status of operation being performed by the method</returns>
        public ResultStatus UpdateSoftware(BusinessEntities.Software objSoftware)
        {
            try
            {
                using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
                {

                    try
                    {
                        var software = DbContext.Softwares.FirstOrDefault(x => x.SoftwareId == objSoftware.SoftwareId);
                     
                        if (software != null)
                        {
                            software.SoftwareName = objSoftware.SoftwareName;
                            software.SoftwareTypeId = objSoftware.SoftwareTypeId;
                            software.ProductId = objSoftware.ProductId;
                        }
                        else
                        {
                            return ResultStatus.QueryNotExecuted;
                        }

                        try
                        {
                            DbContext.SaveChanges();
                        }
                        catch(Exception)
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
            catch(Exception)
            {
                return ResultStatus.ConnectionError;
            }
            return ResultStatus.Success;
        }


        /// <summary>
        /// This method will fill all the Softwares in an autocomplete textbox
        /// </summary>
        /// <returns>Returns the list of Software</returns>
        public List<BusinessEntities.Software> PopulateSoftware(string prefix)
        {
            using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
            {
                List<BusinessEntities.Software> softwareList = new List<BusinessEntities.Software>();
                List<Data.Model.Software> softwareListData = new List<Data.Model.Software>();
                //   softwareListData = DbContext.Softwares.ToList();
                if(prefix == "all")
                {
                    softwareListData = (from s in DbContext.Softwares
                                        select s).ToList();
                }
                else
                {
                    softwareListData = (from s in DbContext.Softwares
                                        where s.SoftwareName.StartsWith(prefix)
                                        select s).ToList();
                }
                
                BusinessEntities.Software objSoft = new BusinessEntities.Software();
                foreach (var s in softwareListData)
                {
                    objSoft = new BusinessEntities.Software()
                    {
                        SoftwareId = s.SoftwareId,
                        SoftwareName = s.SoftwareName,
                        ProductId = s.ProductId,
                        SoftwareTypeId=s.SoftwareTypeId
                    };
                    softwareList.Add(objSoft);
                }
                return softwareList;
            }
        }

        /// <summary>
        /// This method will populate software details on basis of their type
        /// </summary>
        /// <param name="objSoftware">Object holding SoftwareTypeId</param>
        /// <returns>Returns a list of software</returns>
        public List<BusinessEntities.Software> PopulateSoftwareByType(BusinessEntities.Software objSoftware)
        {
            using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
            {
                List<BusinessEntities.Software> softwareList = new List<BusinessEntities.Software>();
                List<Data.Model.Software> softwareListData = new List<Data.Model.Software>();
              
                if (objSoftware.SoftwareTypeId == -1)
                {
                    softwareListData = (from s in DbContext.Softwares
                                        select s).ToList();
                }
                else
                {
                    softwareListData = (from s in DbContext.Softwares
                                        where s.SoftwareTypeId == objSoftware.SoftwareTypeId
                                        select s).ToList();
                }

                BusinessEntities.Software objSoft = new BusinessEntities.Software();
                foreach (var s in softwareListData)
                {
                    objSoft = new BusinessEntities.Software()
                    {
                        SoftwareId = s.SoftwareId,
                        SoftwareName = s.SoftwareName,
                        ProductId = s.ProductId
                    };
                    softwareList.Add(objSoft);
                }
                return softwareList;
            }
        }

        
        /// <summary>
        /// This method checks whether software already allocated to the user.
        /// </summary>
        /// <param name="systemAllocationId">SystemAllocationId of Employee</param>
        /// <param name="softwareId">SoftwareId of a software to be added</param>
        /// <returns>Returns true if present otherwise false</returns>
       public bool checkSoftwareAlreadyAllocated(int systemAllocationId, int softwareId)
       {
           using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
           {
               var result = (from sa in DbContext.SystemAllocations
                             join si in DbContext.SystemInstallations
                             on sa.SystemAllocationId equals si.SystemAllocationId
                             join l in DbContext.Licenses
                             on si.LicenseId equals l.LicenseId
                             join s in DbContext.Softwares
                             on l.SoftwareId equals s.SoftwareId
                             where s.SoftwareId == softwareId && sa.SystemAllocationId == systemAllocationId
                             select si).FirstOrDefault();
               if (result == null)
                   return false;
               return true;
           }
       }

        /// <summary>
        /// This method will Allocate Software to user
        /// </summary>
        /// <param name="objSoftware">Object holding information neccessory to allocate software</param>
        /// <returns>Returns a ResultStatus, which reflects the status of operation performed</returns>
        public ResultStatus SoftwareAllocate(SoftwareAllocationVm objSoftware)
        {
            try
            {
                using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
                {
                    try
                    {
                        Data.Model.SystemInstallation objSoftwareData = new Data.Model.SystemInstallation();
                        var licenseList = DbContext.Licenses.Where(l => l.SoftwareId == objSoftware.SoftwareId).FirstOrDefault();
                       if(licenseList !=null)
                       {
                           objSoftwareData.LicenseId = licenseList.LicenseId;
                           objSoftwareData.SystemAllocationId = objSoftware.SystemAllocationId;
                           objSoftwareData.InstallationDate = objSoftware.AllotedDate;
                           objSoftwareData.ReleaseDate = objSoftware.ReleaseDate;
                           objSoftwareData.IsReleased = false;
                           DbContext.SystemInstallations.Add(objSoftwareData);
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
        /// This method will return list of software allocated to selected system
        /// </summary>
        /// <param name="objSoftware">Object containing SystemAllocationId</param>
        /// <returns>Returns a list of software details</returns>

        public List<SoftwareDetailsVm> ShowEmployeeSoftwareDetails(SoftwareDetailsVm objSoftware)
        {
            using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
            {

               var results = from s in DbContext.Softwares
                             join 
                             st in DbContext.SoftwareTypes on s.SoftwareTypeId equals st.SoftwareTypeId
                             join 
                             p in DbContext.Products on s.ProductId equals p.ProductId
                             join 
                             l in DbContext.Licenses on s.SoftwareId equals l.SoftwareId
                             join 
                             si in DbContext.SystemInstallations on l.LicenseId equals si.LicenseId
                             join 
                             sa in DbContext.SystemAllocations on si.SystemAllocationId equals sa.SystemAllocationId
                             where sa.SystemAllocationId == objSoftware.SystemAllocationId
                             select new
                             {
                               product=p.ProductName,
                               software=s.SoftwareName,
                               type=st.SoftwareTypeName,
                               allotedDate=si.InstallationDate,
                               releaseDate=si.ReleaseDate,
                               softwareId=s.SoftwareId,
                               systemInstallationId=si.SystemInstallationId
                             };

                SoftwareDetailsVm objSoftwareDetails = new SoftwareDetailsVm();
                List<SoftwareDetailsVm> softwareList = new List<SoftwareDetailsVm>();
                foreach (var s in results)
                {
                    objSoftwareDetails = new SoftwareDetailsVm()
                    {
                        ProductName=s.product,
                        SoftwareName=s.software,
                        AllotedDate=s.allotedDate,
                        SoftwareType=s.type,
                        ReleaseDate=s.releaseDate,
                        SoftwareId=s.softwareId,
                        SystemInstallationId=s.systemInstallationId
                    };
                    softwareList.Add(objSoftwareDetails);
                }
                return softwareList;
            }
        }

        /// <summary>
        /// This method will return Software details for updation
        /// </summary>
        /// <param name="objSoftware">Object holding SoftwareId</param>
        /// <returns>Object holding software details for updation</returns>
       public SoftwareVm GetSoftwareForUpdate(BusinessEntities.Software objSoftware)
       {
           using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
           {

               var result = (from s in DbContext.Softwares
                             join p in DbContext.Products
                             on s.ProductId equals p.ProductId
                             where s.SoftwareId == objSoftware.SoftwareId
                             select new
                             {
                                 softwareName = s.SoftwareName,
                                 softwareTypeId = s.SoftwareTypeId,
                                 productId = p.ProductId
                             }).FirstOrDefault();


               SoftwareVm objSoft = new SoftwareVm();
               objSoft = new SoftwareVm()
               {
                  SoftwareName=result.softwareName,
                  ProductId=result.productId,
                  SoftwareTypeId=result.softwareTypeId
               };
               return objSoft;
           }
       }


        /// <summary>
        /// This method saves new license details.
        /// </summary>
        /// <param name="license">Details of the license.</param>
        /// <returns></returns>
        public ResultStatus SaveLicense(BusinessEntities.License license){
            try
            {
                using(var lic=new LicenseManagementMVCEntities())
                {
                   var putlicense=new Data.Model.License()
                   {
                           SoftwareId=license.SoftwareId,
                           LicenseCount=license.LicenseCount,
                           LicenseKey=license.LicenseKey
                   };

                    try
                    {
                        var existingLicense = lic.Licenses.Where(o => o.SoftwareId == license.SoftwareId).Select(o => o.LicenseId).FirstOrDefault();
                        lic.Licenses.Add(putlicense);
                        lic.SaveChanges();
                    }
                    catch(Exception)
                    {
                        return ResultStatus.QueryNotExecuted;
                    }

                }
            }
            catch(Exception)
            {
                return ResultStatus.ConnectionError;
            }
            return ResultStatus.Success;
        }
        /// <summary>
        /// Method to return Software list for corresponding productid and softwaretype
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="softwareTypeId"></param>
        /// <returns></returns>
        public List<BusinessEntities.Software> GetSoftware(int productId,int softwareTypeId) {
            try
            {
                var softwares=new LicenseManagementMVCEntities().Softwares.Where(s => s.ProductId == productId && s.SoftwareTypeId == softwareTypeId).ToList();
                List<BusinessEntities.Software> sList = new List<BusinessEntities.Software>();
                sList.AddRange(softwares.Select(so => new BusinessEntities.Software() {
                  SoftwareId=so.SoftwareId,
                  SoftwareName=so.SoftwareName,
                  SoftwareTypeId=so.SoftwareTypeId,
                  Product = new BusinessEntities.Product() {
                  ProductId=so.ProductId
                  }
                }));
                return sList;
            }
            catch (Exception exp)
            {
                return null;
            }
        }


        /// <summary>
        /// This class will accepts the object holding ProductId and displays all the software of that product in box format.
        /// </summary>
        /// <param name="objProductRequest">Object holding ProductId of selected product</param>
        /// <returns>Software object having all the software of selected product with their specific details</returns>
        public List<BusinessEntities.Software> ShowSoftware(BusinessEntities.Product objProductRequest)
        {
            using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
            {
                Data.Model.Product objProductDataRequest = new Data.Model.Product();
                objProductDataRequest.ProductId = objProductRequest.ProductId;
                List<BusinessEntities.Software> softwareList = new List<BusinessEntities.Software>();
                List<Data.Model.Software> softwareListData = new List<Data.Model.Software>();
                softwareListData = DbContext.Softwares.ToList();

                var result = from s in DbContext.Softwares
                             join
                             p in DbContext.Products
                             on s.ProductId equals p.ProductId
                             select new
                             {
                                 ProductName = p.ProductName,
                                 SoftwareName = s.SoftwareName,
                                 SoftwareId = s.SoftwareId,
                                 ProductId = p.ProductId,
                                 SoftwareTypeId = s.SoftwareTypeId
                             };

                BusinessEntities.Software objSoftware = new BusinessEntities.Software();
                BusinessEntities.Product objProduct = new BusinessEntities.Product();

                if (objProductRequest.ProductId != -1)
                {
                    result = result.Where(s => s.ProductId == objProductRequest.ProductId);
                }
               
                    foreach (var s in result)
                    {
                            objSoftware = new BusinessEntities.Software();
                            objSoftware.SoftwareId = s.SoftwareId;
                            objSoftware.SoftwareName = s.SoftwareName;
                            objProduct = new BusinessEntities.Product();
                            objProduct.ProductId = s.ProductId;
                            objProduct.ProductName = s.ProductName;
                            objSoftware.Product = objProduct;
                            softwareList.Add(objSoftware);
                    }
              
                return softwareList;
            }
        }


        /// <summary>
        /// This method will accept the SoftwareId as paramter and displays the respective software details
        /// </summary>
        /// <param name="objSoftware">Object holding SoftwareId of selected software</param>
        /// <returns>The object holding all the details to be displayed on screen</returns>
        public SoftwareDetailsVm ShowSoftwareDetails(BusinessEntities.Software objSoftware)
        {
            using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
            {
                var result = (from s in DbContext.Softwares
                              join
                              p in DbContext.Products
                              on s.ProductId equals p.ProductId
                              join
                              st in DbContext.SoftwareTypes
                              on s.SoftwareTypeId equals st.SoftwareTypeId
                              where s.SoftwareId == objSoftware.SoftwareId
                              select new
                              {
                                  ProductName = p.ProductName,
                                  ProductId = p.ProductId,
                                  SoftwareName = s.SoftwareName,
                                  SoftwareId = s.SoftwareId,
                                  SoftwareType = st.SoftwareTypeName,
                                  SoftwareTypeId = st.SoftwareTypeId
                              }).FirstOrDefault();

                SoftwareDetailsVm objSoft = new SoftwareDetailsVm();

                if (result != null)
                {
                    objSoft.SoftwareId = result.SoftwareId;
                    objSoft.ProductName = result.ProductName;
                    objSoft.SoftwareType = result.SoftwareType;
                    objSoft.SoftwareName = result.SoftwareName;
                    objSoft.SoftwareTypeId = result.SoftwareTypeId;
                    objSoft.ProductId = result.ProductId;
                }

                var totalLicense = from l in DbContext.Licenses
                                   where l.SoftwareId == objSoftware.SoftwareId
                                   group l by l.SoftwareId into grp
                                   select new
                                   {
                                       totalCount = grp.Sum(x => x.LicenseCount)
                                   };

                foreach (var q in totalLicense)
                {
                    objSoft.TotalCount = q.totalCount;
                }


                int allocatedCount = (from l in DbContext.Licenses
                                      join si in DbContext.SystemInstallations
                                      on l.LicenseId equals si.LicenseId
                                      where l.SoftwareId == objSoftware.SoftwareId
                                      select l).Count();


                objSoft.AllocatedCount = allocatedCount;


                var lastAllocation = (from l in DbContext.Licenses
                                      join si in DbContext.SystemInstallations
                                      on l.LicenseId equals si.LicenseId
                                      join sa in DbContext.SystemAllocations
                                      on si.SystemAllocationId equals sa.SystemAllocationId
                                      join e in DbContext.Employees
                                      on sa.EmployeeId equals e.EmployeeId
                                      where l.SoftwareId == objSoftware.SoftwareId
                                      select new
                                      {
                                          firstName = e.FirstName,
                                          lastName = e.LastName,
                                          empNo = e.EmployeeId
                                      });

                foreach (var la in lastAllocation)
                {

                    objSoft.EmployeeName = la.firstName + " " + la.lastName;
                    objSoft.EmployeeId = la.empNo;
                }
                return objSoft;
            }
        }

       
        /// <summary>
        /// Give number of total and used software
        /// </summary>
        /// <returns>array containing alloted number at index[0] and total at index[1]</returns>
        public int TotalSoftware()
        {
            return new LicenseManagementMVCEntities().Softwares.Select(l => l.SoftwareId).Count();
        }
        /// <summary>
        /// Give number of total and used laptop
        /// </summary>
        /// <returns>array containing alloted number at index[0] and total at index[1]</returns>
        public int[] TotalLaptop()
        {
            int[] c = new int[2];
            LicenseManagementMVCEntities obj = new LicenseManagementMVCEntities();
            c[0] = obj.SystemDetails
                 .Join(obj.SystemAllocations,
                 sd => sd.SystemDetailsId,
                 sa => sa.SystemDetailsId,
                 (sd, sa) => new { sd, sa })
                 .Join(obj.SystemTypes,
                 st => st.sd.SystemTypeId,
                 sdi => sdi.SystemTypeId,
                 (st, sdi) => new
                 {
                     st,
                     sdi
                 }).Where(s => s.sdi.SystemTypeName == "Laptop").Count();
            c[1] = obj.SystemDetails.Where(s => s.SystemTypeId == 1).Select(s => s.Count).Sum();
            return c;
        }
        /// <summary>
        /// Give number of total and used mac
        /// </summary>
        /// <returns>array containing alloted number at index[0] and total at index[1]</returns>
        public int[] TotalMac()
        {
            int[] c = new int[2];
            LicenseManagementMVCEntities obj = new LicenseManagementMVCEntities();
            c[0] = obj.SystemDetails
                .Join(obj.SystemAllocations,
                sd => sd.SystemDetailsId,
                sa => sa.SystemDetailsId,
                (sd, sa) => new { sd, sa })
                .Join(obj.SystemTypes,
                st => st.sd.SystemTypeId,
                sdi => sdi.SystemTypeId,
                (st, sdi) => new { st, sdi }).Where(s => s.sdi.SystemTypeName == "Mac").Count();
            c[1] = obj.SystemDetails
                .Join(obj.SystemTypes,
                sd => sd.SystemTypeId,
                st => st.SystemTypeId,
                (sd, st) => new
                {
                    sd,
                    st
                }).Where(s => s.st.SystemTypeName == "Mac").Select(s => s.sd.Count).Sum();
            return c;
        }
        /// <summary>
        /// Give number of total and used monitor
        /// </summary>
        /// <returns>array containing alloted number at index[0] and total at index[1]</returns>
        public int[] TotalMonitor()
        {
            int[] c = new int[2];
            LicenseManagementMVCEntities obj = new LicenseManagementMVCEntities();
            c[0] = obj.SystemDetails
                 .Join(obj.SystemAllocations,
                 sd => sd.SystemDetailsId,
                 sa => sa.SystemDetailsId,
                 (sd, sa) => new { sd, sa })
                 .Join(obj.SystemTypes,
                 st => st.sd.SystemTypeId,
                 sdi => sdi.SystemTypeId,
                 (st, sdi) => new { st, sdi }).Where(s => s.sdi.SystemTypeName == "Monitor" || s.sdi.SystemTypeName == "Desktop").Count();
            c[1] = obj.SystemDetails
                .Join(obj.SystemTypes,
                sd => sd.SystemTypeId,
                st => st.SystemTypeId,
                (sd, st) => new { sd, st }).Where(s => s.st.SystemTypeName == "Desktop" || s.st.SystemTypeName == "Monitor").Select(s => s.sd.Count).Sum();
            return c;
        }
        /// <summary>
        /// Gets top five license software total and used nnumber
        /// </summary>
        /// <returns>Array</returns>
        public ArrayList TopFiveLicenseSoftware()
        {
            int[] c = new int[2];
            LicenseManagementMVCEntities obj = new LicenseManagementMVCEntities();
            //var x= obj.SystemInstallations.Join(obj.Licenses, sysIn => sysIn.LicenseId, l => l.LicenseId, (sysIn, l) => new {sysIn,l }).GroupBy(s=>s.l.LicenseId).Count() ;
            var result = obj.SystemInstallations
                .GroupBy(s => s.LicenseId)
                .Select(s => new
                {
                    licId = s.Key,
                    licCount = s.Select(x => x.SystemAllocationId).Count()
                }).OrderByDescending(s => s.licCount).Take(5).ToList()
                .Join(obj.Licenses,
                si => si.licId,
                li => li.LicenseId,
                (si, li) => new { si, li, li.LicenseCount })
                .Join(obj.Softwares,
                lic => lic.li.SoftwareId,
                s => s.SoftwareId,
                (lic, s) => new { lic, s }).Select(s => new
                {
                    name = s.s.SoftwareName,
                    //id = s.lic.si.licId,
                    licBought = s.lic.li.LicenseCount,
                    licInstalled = s.lic.si.licCount
                }).ToArray();
            ArrayList array = new ArrayList();
            foreach (var value in result)
            {
                array.Add(value.name);
                array.Add(value.licBought);
                array.Add(value.licInstalled);
            }
            return array;
        }
        public ArrayList EmployeeDetailAccordingToMonth()
        {
            LicenseManagementMVCEntities obj = new LicenseManagementMVCEntities();
            var result = obj.Employees
               .OrderByDescending(x => x.JoiningDate)
               .GroupBy(g => new { g.JoiningDate.Year, g.JoiningDate.Month })
               .Select(s => new
               {
                   month = s.Key.Month,
                   year = s.Key.Year,
                   employeeCount = s.Select(x => x.EmployeeId).Count()
               }).ToArray();
            ArrayList array = new ArrayList();
            foreach (var value in result)
            {
                array.Add(value.month);
                array.Add(value.year);
                array.Add(value.employeeCount);
            }
            return array;
        }
    }
}
