using Model.Data;
using Model.Interface;
using Model.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Collections;

namespace EcomaintSite.Controllers
{
    public class MasterPlansController : Controller
    {
        IStaffRepository staff;
        IDeviceRepository device;
        IReasonForRepairRepository reason;
        IPriorityRepository priority;
        IMasterPlanRepository masterPlans;
        ITypeOfMaintenanceRepository typeOfMaintenance;
        IWorkComponentRepository workComponentRepository;
        IComponentRepository componentRepository;
        ISparePartRepository sparePartRepository;
        IMasterPlansOfSparePartRepository masterPlansOfSparePartRepository;
        IMasterPlansOfWorkRepository masterPlansOfWorkRepository;
        IWorkRepository workRepository;
        IMasterPlanUnitOfWork masterPlanUnitOfWork;
        IWorkOrderUnitOfWork workOrderUnitOfWork;

        public MasterPlansController(IStaffRepository _staff, IDeviceRepository _device, IReasonForRepairRepository _reason, IPriorityRepository _priority, IMasterPlanRepository _masterPlans, ITypeOfMaintenanceRepository _typeOfMaintenance, IComponentRepository _componentRepository, IWorkComponentRepository _workComponentRepository, ISparePartRepository _sparePartRepository, IMasterPlansOfSparePartRepository _masterPlansOfSparePartRepository, IMasterPlansOfWorkRepository _masterPlansOfWorkRepository, IWorkRepository _workRepository, IMasterPlanUnitOfWork _masterPlanUnitOfWork, IWorkOrderUnitOfWork _workOrderUnitOfWork)
        {
            staff = _staff;
            device = _device;
            reason = _reason;
            priority = _priority;
            masterPlans = _masterPlans;
            typeOfMaintenance = _typeOfMaintenance;
            workComponentRepository = _workComponentRepository;
            componentRepository = _componentRepository;
            sparePartRepository = _sparePartRepository;
            masterPlansOfSparePartRepository = _masterPlansOfSparePartRepository;
            masterPlansOfWorkRepository = _masterPlansOfWorkRepository;
            workRepository = _workRepository;
            masterPlanUnitOfWork = _masterPlanUnitOfWork;
            workOrderUnitOfWork = _workOrderUnitOfWork;
        }

        // GET: MasterPlans
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.cboMonitoredBy = new SelectList(staff.GetStaffRole(3, User.Identity.Name), "ID", "Name");
            ViewBag.cboCreatedBy = new SelectList(staff.GetStaffRole(2, User.Identity.Name), "ID", "Name");
         
            ViewBag.cboDevice = new SelectList(device.ListAll().Select(x => new DeviceObjForDropdown
            {
                ID = x.ID,
                Name = x.ID + " - " + (SessionVariable.TypeLanguage == 0 ? x.Name : x.NameOfEng)
            }).ToList(), "ID", "Name");

            ViewBag.cboReason = new SelectList(reason.ListAll().Select(x => new
            {
                ID = x.ID,
                Name = (SessionVariable.TypeLanguage == 0 ? x.Name : x.NameOfEng)
            }), "ID", "Name");
            
            ViewBag.cboPriority = new SelectList(priority.ListAll().Select(x => new
            {
                ID = x.ID,
                Name = (SessionVariable.TypeLanguage == 0 ? x.Name : x.NameOfEng)
            }), "ID", "Name");

            return View();
        }

        [Authorize]
        public JsonResult FilterData(string fromDate, string toDate, int option) => Json(masterPlans.GetMasterPlans(User.Identity.Name, Convert.ToDateTime(fromDate, new CultureInfo("vi-vn")), Convert.ToDateTime(toDate, new CultureInfo("vi-vn")), option, SessionVariable.TypeLanguage).Select(x =>
            new {
                Choose = "",
                PlanName = x.PlanName,
                DeviceID = x.DeviceID,
                DeviceName = x.DeviceName,
                TypeOfMaintenanceID = x.TypeOfMaintenanceID,
                ID = x.ID,
                DateCreated = x.DateCreated.ToString("dd/MM/yyyy"),
                DateCompleted = x.DateCompleted.ToString("dd/MM/yyyy"),
                TotalHours = x.TotalHours,
                ReasonForRepairID = x.ReasonForRepairID,
                Note = x.Note,
                PriorityID = x.PriorityID,
                PriorityName = x.PriorityName,
                TypeOfMaintenanceName = x.TypeOfMaintenanceName,
                ConfirmedBy = x.ConfirmedBy,
                StaffID = x.StaffID
            }), JsonRequestBehavior.AllowGet);

        [Authorize]
        public ActionResult GetTypeOfMaintenanceByDevice(string deviceID) =>
            Json(typeOfMaintenance.GetTypeOfMaintenanceByDevice(deviceID).Select(x => new
            {
                TypeOfMaintenanceID = x.ID,
                Name = x.Name
            }), JsonRequestBehavior.AllowGet);

        [Authorize]
        public JsonResult GetComponentByDevice(string deviceID) =>
            Json(componentRepository.GetComponentByDevice(deviceID, SessionVariable.TypeLanguage).Select(x => new
            {
                id = x.ID,
                text = x.Name,
                parentid = x.RootID
            }), JsonRequestBehavior.AllowGet);

        [Authorize]
        public JsonResult GetWorkByComponent(string deviceID, string componentID) =>
            Json(workComponentRepository.GetWorkByComponent(deviceID, componentID, SessionVariable.TypeLanguage), JsonRequestBehavior.AllowGet);

        [Authorize]
        public JsonResult GetSparePart() =>
            Json(sparePartRepository.GetSparePart(User.Identity.Name).Select(x => new
            {
                SparePartID = x.ID,
                SparePartName = SessionVariable.TypeLanguage == 0 ? x.Name : x.NameOfEng,
                Specification = x.Specification,
                UnitOfCalculationID = x.UnitOfCalculationID
            }).ToList(), JsonRequestBehavior.AllowGet);


        [Authorize]
        public JsonResult CreatePlans(string data, string data2, string data3)
        {
            try
            {
                List<MasterPlan> lstPlans = JsonConvert.DeserializeObject<List<MasterPlan>>(data);
                List<MasterPlansOfWork> lstMasterPlansOfWork = JsonConvert.DeserializeObject<List<MasterPlansOfWork>>(data2);
                List<MasterPlansOfSparePart> lstSparePartByPlan = JsonConvert.DeserializeObject<List<MasterPlansOfSparePart>>(data3);
                masterPlanUnitOfWork.MasterPlanRepository.Add(lstPlans[0]);
                lstMasterPlansOfWork.ForEach(x => x.PlanID = lstPlans[0].ID);
                lstSparePartByPlan.ForEach(x => x.PlanID = lstPlans[0].ID);
                masterPlanUnitOfWork.MasterPlansOfWorkRepository.AddRange(lstMasterPlansOfWork);
                masterPlanUnitOfWork.MasterPlansOfSparePartRepository.AddRange(lstSparePartByPlan);
                masterPlanUnitOfWork.Save();
                return Json(new { Message = "success", planID = lstPlans[0].ID });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "failed: " + ex.Message });
            }         
        }

        [Authorize]
        public JsonResult Edit(string data, string data2, string data3)
        {
            try
            {
                List<MasterPlan> lstPlans = JsonConvert.DeserializeObject<List<MasterPlan>>(data);
                List<MasterPlansOfWork> lstMasterPlansOfWork = JsonConvert.DeserializeObject<List<MasterPlansOfWork>>(data2);
                List<MasterPlansOfSparePart> lstSparePartByPlan = JsonConvert.DeserializeObject<List<MasterPlansOfSparePart>>(data3);
                masterPlanUnitOfWork.MasterPlanRepository.Edit(lstPlans[0]);
                masterPlanUnitOfWork.MasterPlansOfSparePartRepository.Delete(lstPlans[0].ID);
                masterPlanUnitOfWork.MasterPlansOfWorkRepository.Delete(lstPlans[0].ID);
                masterPlanUnitOfWork.Save();
                lstMasterPlansOfWork.ForEach(x => x.PlanID = lstPlans[0].ID);
                lstSparePartByPlan.ForEach(x => x.PlanID = lstPlans[0].ID);
                masterPlanUnitOfWork.MasterPlansOfWorkRepository.AddRange(lstMasterPlansOfWork);
                masterPlanUnitOfWork.MasterPlansOfSparePartRepository.AddRange(lstSparePartByPlan);
                masterPlanUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                return Json("failed: " + ex.Message);
            }
            return Json("success");
        }

        [Authorize]
        public JsonResult GetPlanDetails(int planID)
        {
            try
            {
                var model = masterPlansOfSparePartRepository.GetSparePartByPlanID(planID).Select(x => new
                {
                    DeviceID = x.DeviceID,
                    WorkID = x.WorkID,
                    ComponentID = x.ComponentID,
                    Description = SessionVariable.TypeLanguage == 0 ? workRepository.GetWorkByID(x.WorkID).Description : workRepository.GetWorkByID(x.WorkID).DescriptionOfEnglish,
                    SparePartID = x.SparePartID
                });

                if (model.Count() == 0)
                {
                    return Json(new
                    {
                        times = 2,
                        model = masterPlansOfWorkRepository.GetMasterPlansOfWorkByPlanID(planID).Select(x => new
                        {
                            DeviceID = x.DeviceID,
                            WorkID = x.WorkID,
                            ComponentID = x.ComponentID,
                            Description = SessionVariable.TypeLanguage == 0 ? workRepository.GetWorkByID(x.WorkID).Description : workRepository.GetWorkByID(x.WorkID).DescriptionOfEnglish,
                            SparePartID = ""
                        })
                    });
                }
                return Json(new { times = 1, model = model }, JsonRequestBehavior.AllowGet);
            }
            catch { return Json(new { times = 1, model = new List<MasterPlansOfSparePart>() }, JsonRequestBehavior.AllowGet); }
        }

        [Authorize]
        public JsonResult GetWorkPlan(int planID) => 
            Json(masterPlansOfWorkRepository.GetMasterPlansOfWorkByPlanID(planID).Select(x => new
            {
                PlanID = x.PlanID,
                WorkID = x.WorkID,
                TimePlan = x.TimePlan.HasValue ? x.TimePlan : 0,
                ComponentID = x.ComponentID,
                ComponentName = SessionVariable.TypeLanguage == 0 ? componentRepository.GetComponentByID(x.DeviceID, x.ComponentID).Name : componentRepository.GetComponentByID(x.DeviceID, x.ComponentID).NameOfEng,
                Description = SessionVariable.TypeLanguage == 0 ? workRepository.GetWorkByID(x.WorkID).Description : workRepository.GetWorkByID(x.WorkID).DescriptionOfEnglish
            }), JsonRequestBehavior.AllowGet);
        

        [Authorize]
        public JsonResult GetSparePartForPlan(int planID, int workID, string componentID) => 
            Json(masterPlansOfSparePartRepository.GetSparePartByWorkPlan(planID, workID, componentID).Select(x => new
            {
                SparePartID = x.SparePartID,
                Name = SessionVariable.TypeLanguage == 0 ? sparePartRepository.GetSparePartByID(x.SparePartID).Name : sparePartRepository.GetSparePartByID(x.SparePartID).NameOfEng,
                Amount = x.Amount.HasValue ? x.Amount : 0,
            }), JsonRequestBehavior.AllowGet);

        [Authorize]
        public JsonResult SaveWO(string data, string data2)
        {
            List<WorkOrder> lstWO = JsonConvert.DeserializeObject<List<WorkOrder>>(data);
            List<WorkOrderOfWork> lstWorkOfWO = new List<WorkOrderOfWork>();
            List<WorkOrderOfWorkIncludeSparePart> lstSparePartOfWO = new List<WorkOrderOfWorkIncludeSparePart>();
            List<WorkOrderOfWorkIncludeDetailSparePart> lstSparePartDetailsOfWO = new List<WorkOrderOfWorkIncludeDetailSparePart>();
            List<WorkOrderDetailsByTheState> lstWOStatus = new List<WorkOrderDetailsByTheState>();
            List<ComponentOfSparePart> lstSparePartOfComponent = new List<ComponentOfSparePart>();

            var lstPlanID = (JArray)JsonConvert.DeserializeObject(data2);
            try
            {
                string workOrderID = workOrderUnitOfWork.WorkOrderRepository.GenerateWorkOrderID(User.Identity.Name, DateTime.Now.Year, DateTime.Now.Month);
                int planID = 0;
                List<string> lstWorkOrderID = new List<string>();
                foreach (var item in lstWO)
                {
                    planID = Convert.ToInt32(lstPlanID[lstWO.IndexOf(item)]["PlanID"].Value<string>());
                    item.ID = workOrderID;
                    item.DateCreated = DateTime.Now;
                    item.TimeCreated = DateTime.Now;
                    item.UserCreated = User.Identity.Name;
                    item.PriorityID = item.PriorityID == null ? workOrderUnitOfWork.PriorityRepository.ListAll().Max(x => x.ID) : item.PriorityID;
                    item.TheStatusDetailsOfWorkOrderID = workOrderUnitOfWork.TheStatusDetailsOfWorkOrderRepository.GetTheStatusDetailsOfWorkOrderByID(item.TheStatusOfWorkOrderID ?? 1).ID;
                    item.LastDateModified = item.StartDatePlan;
                    item.OriginalStartDate = item.StartDatePlan;
                    if (workOrderUnitOfWork.GeneralRepository.GetGeneralInfo().MDEFAULT.HasValue && workOrderUnitOfWork.GeneralRepository.GetGeneralInfo().MDEFAULT == 1)
                        item.WorkOrderNo = workOrderID;
                    lstWOStatus.Add(new WorkOrderDetailsByTheState
                    {
                        WorkOrderID = workOrderID,
                        TheStatusOfWorkOrderID = item.TheStatusOfWorkOrderID,
                        ID = 1,
                        Username = item.UserCreated,
                        DateCreated = item.DateCreated
                    });

                    var lstMasterPlansOfWork = workOrderUnitOfWork.MasterPlansOfWorkRepository.GetMasterPlansOfWorkByPlanID(planID);
                    foreach (var masterPlansOfWork in lstMasterPlansOfWork)
                    {
                        Work work = workOrderUnitOfWork.WorkRepository.GetWorkByID(masterPlansOfWork.WorkID);
                        ComponentOfWork workComponent = workOrderUnitOfWork.WorkComponentRepository.GetWorkComponentByID(masterPlansOfWork.DeviceID, masterPlansOfWork.ComponentID, masterPlansOfWork.WorkID);
                        lstWorkOfWO.Add(new Model.Data.WorkOrderOfWork
                        {
                            WorkOrderID = workOrderID,
                            WorkID = masterPlansOfWork.WorkID,
                            ComponentID = masterPlansOfWork.ComponentID,
                            Tool = workComponent.Tool,
                            InquiryHR = workComponent.InquiryHR,
                            HourPlan = workComponent.TimePlan,
                            ActualTime = workComponent.TimePlan,
                            Action = workComponent.Action,
                            QualityCheck = workComponent.QualityCheck,
                            AmountOfPeople = work.AmountOfPeople,
                            Path = workComponent.Path,
                            WorkStartDate = item.StartDatePlan,
                            WorkEndDate = item.StartDatePlan
                        });
                        /**************************************************************************************************/
                        if (workOrderUnitOfWork.GeneralRepository.GetGeneralInfo().THEM_CV_PT.HasValue && workOrderUnitOfWork.GeneralRepository.GetGeneralInfo().THEM_CV_PT.Value != 0) continue;

                        var lstSparePartByPlan = workOrderUnitOfWork.MasterPlansOfSparePartRepository.GetSparePartByWorkPlan(planID, masterPlansOfWork.WorkID, masterPlansOfWork.ComponentID);
                        foreach (var sparePart in lstSparePartByPlan)
                        {
                            if (sparePart.Amount.HasValue == false || sparePart.Amount <= 0) continue;

                            if (workOrderUnitOfWork.TypeOfMaterialRepository.GetTypeOfMaterial(workOrderUnitOfWork.SparePartRepository.GetSparePartByID(sparePart.SparePartID).TypeOfMaterialID).IsMaterial == false && !workOrderUnitOfWork.SparePartComponentRepository.CheckExists(item.DeviceID, sparePart.ComponentID, sparePart.SparePartID))
                            {
                                lstSparePartOfComponent.Add(new Model.Data.ComponentOfSparePart
                                {
                                    Active = true,
                                    Amount = sparePart.Amount,
                                    DeviceID = item.DeviceID,
                                    ComponentID = sparePart.ComponentID,
                                    SparePartID = sparePart.SparePartID,
                                    LocationID = "A"
                                });
                            }
                            //
                            Model.Data.WorkOrderOfWorkIncludeSparePart WOobj = new Model.Data.WorkOrderOfWorkIncludeSparePart
                            {
                                WorkOrderID = workOrderID,
                                WorkID = sparePart.WorkID,
                                ComponentID = sparePart.ComponentID,
                                SparePartID = sparePart.SparePartID,
                                PlanQuantity = sparePart.Amount
                            };
                            var lstComponent = workOrderUnitOfWork.SparePartComponentRepository.GetSparePartComponentByID(item.DeviceID, sparePart.ComponentID, sparePart.SparePartID);
                            double sum = sparePart.Amount.Value;
                            foreach (var component in lstComponent)
                            {
                                double quality = sum - component.Amount.Value;
                                Model.Data.WorkOrderOfWorkIncludeDetailSparePart obj = new Model.Data.WorkOrderOfWorkIncludeDetailSparePart
                                {
                                    WorkOrderID = workOrderID,
                                    WorkID = sparePart.WorkID,
                                    ComponentID = sparePart.ComponentID,
                                    SparePartID = sparePart.SparePartID,
                                    LocationID = component.LocationID,
                                    PlanQuantity = 0
                                };
                                if (quality <= 0)
                                {
                                    obj.PlanQuantity = sum;
                                    quality = sum;
                                }
                                else if (quality > sum)
                                    quality = component.Amount.Value;
                                else
                                    quality = component.Amount.Value;
                                sum -= component.Amount.Value;
                                if (obj.PlanQuantity > 0)
                                    lstSparePartDetailsOfWO.Add(obj);
                            }
                            WOobj.PlanQuantity = lstSparePartDetailsOfWO.Where(x => x.WorkID == WOobj.WorkID && x.SparePartID == WOobj.SparePartID && x.ComponentID == WOobj.ComponentID && x.PlanQuantity > 0).Sum(x => x.PlanQuantity);
                            lstSparePartOfWO.Add(WOobj);
                        }
                    }
                    var lst = workOrderUnitOfWork.UserRequestDetailRepository.GetRequestDetailsByPlan(planID);
                    if (lst != null)
                    {
                        lst.Where(x => x.ExecuteID == "01" && string.IsNullOrEmpty(x.WorkOrderID)).ToList().ForEach(x => x.WorkOrderID = workOrderID);
                        var listGiamSat = workOrderUnitOfWork.MonitoringOfQuantitativeRepository.ListAll().Where(x => !lst.Where(y => y.ExecuteID != "03" && string.IsNullOrEmpty(y.WorkOrderID)).Any(y => y.ID == x.UserRequestDetailID));
                        listGiamSat.ToList().ForEach(x => x.WorkOrderID = workOrderID);
                    }
                    var lst1 = workOrderUnitOfWork.MasterPlansOfWorkRepository.GetMasterPlansOfWorkByPlanID(planID).ToList();
                    lst1.ForEach(x => x.WorkOrderID = workOrderID);

                    lstWorkOrderID.Add(workOrderID);
                    workOrderID = workOrderID.Split('-')[0] + "-" + (Convert.ToDouble(workOrderID.Split('-')[1]) + 1).ToString();
                }
                //string phieuBTNEW = db.WorkOrderRepository.GenerateWorkOrderID(User.Identity.Name, DateTime.Now.Year, DateTime.Now.Month);
                //if (lstWO.OrderBy(x => x.WorkOrderID).Take(1).SingleOrDefault().WorkOrderID != phieuBTNEW)
                //{
                //    return Json("failed");
                //}

                workOrderUnitOfWork.SparePartComponentRepository.AddRange(lstSparePartOfComponent);
                workOrderUnitOfWork.WorkOrderRepository.AddRange(lstWO);
                workOrderUnitOfWork.WorkOrderDetailByTheStateRepository.AddRange(lstWOStatus);
                workOrderUnitOfWork.WorkOrderOfWorkRepository.AddRange(lstWorkOfWO);
                workOrderUnitOfWork.WorkOrderOfSparePartRepository.AddRange(lstSparePartOfWO);
                workOrderUnitOfWork.Save();
                try
                {
                    lstWorkOrderID.ForEach(x => workOrderUnitOfWork.SparepartAndTypeOfSparepartRepository.IsCheckAutomaticInsertSparepartAndTypeOfSparepart(x.ToString()));
                    lstWorkOrderID.ForEach(x => workOrderUnitOfWork.WorkOrderRepository.AutoAddNewStaffIntoWorkOrder(x.ToString()));
                }
                catch { }
                return Json("success");
            }
            catch (Exception ex)
            {
                return Json("failed: " + ex.Message);
            }
        }

    }
}