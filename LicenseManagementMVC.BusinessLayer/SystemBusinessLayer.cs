// /*****************************************************************************************************************
//  * Project Name  :  LicenseManagementMVC
//  * File Name  :   EmployeeBusinessLayer.cs   
//  * Description :  Contains all system related manipulations 
//  * Created By :   Jeevan Chhetri
//  * Created Date :  19-04-2017
//  * Modified By :  Tipu Ali Khan
//  * Last Modified Date :  26-4-2017
//  ****************************************************************************************************************/
using LicenseManagementMVC.Data.Model;
using LicenseManagementMVC.BusinessEntities;
using LicenseManagementMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace LicenseManagementMVC.BusinessLayer
{
    public class SystemBusinessLayer
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(SystemBusinessLayer));
        /// <summary>
        /// This method provides all system types to the grid
        /// </summary>
        /// <returns></returns>
        public List<BusinessEntities.SystemType> SystemtypeDetails()
        {
            LicenseManagementMVCEntities detail = new LicenseManagementMVCEntities();
            var schemaList = (from schema in detail.SystemTypes select schema).ToList();
            List<BusinessEntities.SystemType> type = new List<BusinessEntities.SystemType>();
            type.AddRange(schemaList.Select(s => new BusinessEntities.SystemType()
            {
                SystemTypeId = s.SystemTypeId,
                Type = s.SystemTypeName
            }));
            return type;
        }
        /// <summary>
        /// Fetch system detail from Db.
        /// </summary>
        /// <returns>List of type SystemDetail</returns>
        public List<BusinessEntities.SystemDetails> FetchSystemDetails(int brandId)
        {
            LicenseManagementMVCEntities detail = new LicenseManagementMVCEntities();
            var schemaList = (from schema in detail.SystemDetails where schema.BrandId == brandId select schema).ToList();
            List<BusinessEntities.SystemDetails> type = new List<BusinessEntities.SystemDetails>();
            type.AddRange(schemaList.Select(s => new BusinessEntities.SystemDetails()
            {
                SystemDetailsId = s.SystemDetailsId,
                BrandId = s.BrandId,
                SystemTypeId = s.SystemTypeId,
                Series = s.Series,
                Processor = s.Processor,
                HardDiskSpace = s.HDDSpace,
                Count = s.Count
            }));
            return type;
        }
        /// <summary>
        /// This method provides all brands to the grid
        /// </summary>
        /// <returns></returns>
        public List<BusinessEntities.Brand> ShowBrandDetails()
        {
            LicenseManagementMVCEntities detail = new LicenseManagementMVCEntities();
            var schemaList = (from schema in detail.Brands select schema).ToList();
            List<BusinessEntities.Brand> type = new List<BusinessEntities.Brand>();
            type.AddRange(schemaList.Select(s => new BusinessEntities.Brand()
            {
                BrandId = s.BrandId,
                BrandName = s.BrandName
            }));
            return type;
        }
        /// <summary>
        /// For adding new system type to database
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public ResultStatus SaveSystemType(BusinessEntities.SystemType st)
        {
            try
            {
                using (LicenseManagementMVCEntities system = new LicenseManagementMVCEntities())
                {
                    Data.Model.SystemType stData = new Data.Model.SystemType()
                    {
                        SystemTypeName = st.Type
                    };
                    if (new LicenseManagementMVCEntities().SystemTypes.Any(o => o.SystemTypeName == stData.SystemTypeName)) return ResultStatus.AlreadyExist;
                    system.SystemTypes.Add(stData);
                    system.SaveChanges();
                    return ResultStatus.Success;
                }
            }
            catch (Exception exp)
            {
                //TODO
                return ResultStatus.ConnectionError;
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public ResultStatus SaveBrand(BusinessEntities.Brand brand)
        {
            try
            {
                using (LicenseManagementMVCEntities system = new LicenseManagementMVCEntities())
                {
                    Data.Model.Brand bdData = new Data.Model.Brand()
                    {
                        BrandName = brand.BrandName
                    };
                    if (new LicenseManagementMVCEntities().Brands.Any(o => o.BrandName == bdData.BrandName)) return ResultStatus.AlreadyExist;
                    system.Brands.Add(bdData);
                    system.SaveChanges();
                    return ResultStatus.Success;
                }
            }
            catch (Exception exp)
            {
                //TODO
                return ResultStatus.ConnectionError;
            }
        }
        /// <summary>
        /// This method helps in saving system details in the database.
        /// </summary>
        /// <param name="systemDetails">Details of the new system .</param>
        /// <returns>It returns if system details are successfully added or not.</returns>
        public ResultStatus SaveSystemDetails(BusinessEntities.SystemDetails systemDetails)
        {

            try
            {
                using (var sys = new LicenseManagementMVCEntities())
                {
                    var details = new Data.Model.SystemDetail()
                    {
                        BrandId = systemDetails.BrandId,
                        SystemTypeId = systemDetails.SystemTypeId,
                        Series = systemDetails.Series,
                        HDDSpace = systemDetails.HardDiskSpace.ToString(),
                        Processor = systemDetails.Processor,
                        Count = systemDetails.Count
                    };
                    var record = sys.SystemDetails
                                           .Where(o => o.BrandId == details.BrandId
                                             && o.SystemTypeId == details.SystemTypeId
                                             && o.Series == details.Series
                                             && o.Processor == details.Processor
                                             &&o.HDDSpace == details.HDDSpace)
                                           .Select(o => o.SystemDetailsId).SingleOrDefault();
                    if (record == 0)
                    {
                        sys.SystemDetails.Add(details);
                        sys.SaveChanges();
                        return ResultStatus.Success;
                    }
                    var check = sys.SystemDetails.FirstOrDefault(o => o.SystemDetailsId == record);
                    check.Count += systemDetails.Count;
                    sys.SaveChanges();
                    return ResultStatus.Success;
                }
            }
            catch (Exception exp)
            {
                return ResultStatus.ConnectionError;
            }
        }
        /// <summary>
        /// Make entries in SystemAllocation table and reduce count by 1 in SystemDetails table
        /// </summary>
        /// <param name="detail">Acept parameter of type SystemAllocation</param>
        /// <returns>ResultStaus(enum with status value)</returns>
        public ResultStatus SaveSystemAllocationDetail(BusinessEntities.SystemAllocation detail)
        {
            try
            {
                using (LicenseManagementMVCEntities obj = new LicenseManagementMVCEntities())
                {
                    Data.Model.SystemAllocation allocate = new Data.Model.SystemAllocation()
                    {
                        SystemDetailsId = detail.SystemDetailsId,
                        EmployeeId = detail.EmployeeId,
                        AllotedDate = detail.AllotedDate ?? DateTime.Now,
                        ReleaseDate = detail.ReleaseDate,
                        Remarks = detail.Remarks,
                        IsReleased = detail.IsReleased,
                    };
                    obj.SystemAllocations.Add(allocate);
                    //decrease count value by 1 according to SystemdetailId.
                    var systemDetail = obj.SystemDetails.FirstOrDefault(x => x.SystemDetailsId == detail.SystemDetailsId);
                    if (systemDetail != null)
                    {
                        --systemDetail.Count; 
                    }
                    obj.SaveChanges();
                    return ResultStatus.Success;
                }
            }
            catch(Exception exp)
            {
                log.Error(exp);
                return ResultStatus.QueryNotExecuted;
            }
        }

       
        /// <summary>
        /// This method will Show SystemDetails in a grid on Employee selection
        /// </summary>
        /// <param name="objEmployee">Object containing EmployeeId of selected employee</param>
        /// <returns>Object holding systen details of system allocated to selected employee</returns>
        public List<SoftwareAllocationVm> ShowEmployeeSystemDetails(BusinessEntities.Employee objEmployee)
        {
            using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
            {

                var result = from s in DbContext.SystemAllocations
                             join sd in DbContext.SystemDetails
                             on s.SystemDetailsId equals sd.SystemDetailsId
                             join b in DbContext.Brands
                             on sd.BrandId equals b.BrandId
                             join st in DbContext.SystemTypes
                             on sd.SystemTypeId equals st.SystemTypeId
                             where s.EmployeeId == objEmployee.EmployeeId
                             select new
                             {
                                 brand = b.BrandName,
                                 type = st.SystemTypeName,
                                 remarks = s.Remarks,
                                 series = sd.Series,
                                 processor = sd.Processor,
                                 hddSpace = sd.HDDSpace,
                                 systemAllocationId = s.SystemAllocationId,
                                 allotedDate = s.AllotedDate,
                                 releaseDate = s.ReleaseDate,
                                 isReleased = s.IsReleased,

                             };

                SoftwareAllocationVm objSystemDetails = new SoftwareAllocationVm();
                List<SoftwareAllocationVm> systemList = new List<SoftwareAllocationVm>();
                foreach (var r in result)
                {
                    objSystemDetails = new SoftwareAllocationVm()
                    {
                        BrandName = r.brand,
                        Series = r.series,
                        Remarks = r.remarks,
                        Processor = r.processor,
                        HDDSpace = r.hddSpace,
                        SystemType = r.type,
                        SystemAllocationId = r.systemAllocationId,
                        AllotedDate = r.allotedDate,
                        ReleaseDate = r.releaseDate,
                        IsReleased = r.isReleased
                    };
                    systemList.Add(objSystemDetails);
                }
                return systemList;
            }
        }

        /// <summary>
        /// This method will display system details on system selection
        /// </summary>
        /// <param name="objSoftware">Object holding SystemAllocationID</param>
        /// <returns>Returns a list of SystemDetails </returns>
        public SoftwareAllocationVm ShowSystemDetails(SoftwareAllocationVm objSoftware)
        {
            using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
            {
                var result = (from s in DbContext.SystemAllocations
                              join sd in DbContext.SystemDetails
                              on s.SystemDetailsId equals sd.SystemDetailsId
                              join b in DbContext.Brands
                              on sd.BrandId equals b.BrandId
                              join st in DbContext.SystemTypes
                              on sd.SystemTypeId equals st.SystemTypeId
                              where s.SystemAllocationId == objSoftware.SystemAllocationId
                              select new
                              {
                                  brand = b.BrandName,
                                  type = st.SystemTypeName,
                                  remarks = s.Remarks,
                                  series = sd.Series,
                                  processor = sd.Processor,
                                  hddSpace = sd.HDDSpace,
                                  systemAllocationId = s.SystemAllocationId,
                                  allotedDate = s.AllotedDate,
                                  releaseDate = s.ReleaseDate,
                                  isReleased = s.IsReleased
                              }).FirstOrDefault();

                SoftwareAllocationVm objSystemDetails = new SoftwareAllocationVm()
                    {
                        BrandName = result.brand,
                        Series = result.series,
                        Remarks = result.remarks,
                        Processor = result.processor,
                        HDDSpace = result.hddSpace,
                        SystemType = result.type,
                        SystemAllocationId = result.systemAllocationId
                    };
                
                return objSystemDetails;
            }
        }

        /// <summary>
        /// This method will return the system list on Employee selection
        /// </summary>
        /// <param name="objSoftware">Object holding EmployeeId</param>
        /// <returns>Returns a list of System</returns>
        public List<BusinessEntities.SystemAllocation> PopulateSystem(SoftwareAllocationVm objSoftware)
        {
            using (Data.Model.LicenseManagementMVCEntities DbContext = new LicenseManagementMVCEntities())
            {
                SoftwareBusinessLayer software = new SoftwareBusinessLayer();
                List<BusinessEntities.SystemAllocation> systemList = new List<BusinessEntities.SystemAllocation>();
                List<Data.Model.SystemAllocation> systemDataList = new List<Data.Model.SystemAllocation>();

                systemDataList = (from s in DbContext.SystemAllocations
                                  where s.EmployeeId == objSoftware.EmployeeId
                                  select s).ToList();

                BusinessEntities.SystemAllocation objSystem = new BusinessEntities.SystemAllocation();
                foreach (var s in systemDataList)
                {
                    objSystem = new BusinessEntities.SystemAllocation()
                    {
                        SystemAllocationId = s.SystemAllocationId
                    };
                    if (!software.checkSoftwareAlreadyAllocated(objSystem.SystemAllocationId, objSoftware.SoftwareId))
                    {
                        systemList.Add(objSystem);
                    }
                }
                return systemList;
            }
        }
    }
}