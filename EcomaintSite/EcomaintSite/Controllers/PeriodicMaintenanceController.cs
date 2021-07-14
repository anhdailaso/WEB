using Model.Data;
using Model.Interface;

using Model.Repository;
using Model.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomaintSite.Controllers
{
    public class PeriodicMaintenanceController : Controller
    {
        IPeriodicMaintenanceRepository periodicMaintenanceRepository;
        IStaffRepository staffRepository;
        IMasterPlanUnitOfWork masterPlanUnitOfWork;
        IWorkOrderUnitOfWork workOrderUnitOfWork;

        public PeriodicMaintenanceController(IStaffRepository _staffRepository, IPeriodicMaintenanceRepository _periodicMaintenanceRepository, IMasterPlanUnitOfWork _masterPlanUnitOfWork, IWorkOrderUnitOfWork _workOrderUnitOfWork)
        {
            periodicMaintenanceRepository = _periodicMaintenanceRepository;           
            staffRepository = _staffRepository;
            masterPlanUnitOfWork = _masterPlanUnitOfWork;
            workOrderUnitOfWork = _workOrderUnitOfWork;
        }

        // GET:  PeriodicMaintenance
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.cboCreatedBy = new SelectList(staffRepository.GetStaffRole(2, User.Identity.Name), "ID", "Name");
            ViewBag.cboMonitoredBy = new SelectList(staffRepository.GetStaffRole(3, User.Identity.Name), "ID", "Name");
            return View();
        }

        [Authorize]
        public JsonResult FilterData(string fromDate, string toDate, bool timeOut) =>
            Json(periodicMaintenanceRepository.GetPeriodicMaintenance(Convert.ToDateTime(fromDate, new CultureInfo("vi-vn")), Convert.ToDateTime(toDate, new CultureInfo("vi-vn")), "-1", "-1", "-1", "-1", "-1", User.Identity.Name, timeOut).Select(x => new
            {
                Choose = "",
                DeviceID = x.DeviceID,
                TypeOfMaintenanceName = x.TypeOfMaintenanceName,
                TypeOfMaintenanceID = x.TypeOfMaintenanceID,
                DeviceName = x.DeviceName,
                LastDate = x.LastDate.ToString("dd/MM/yyyy"),
                NextDate = x.NextDate.ToString("dd/MM/yyyy"),
                RuntimeCycle = x.RuntimeCycle,
                CurrentRunTime = x.CurrentRunTime,
                DeviceGroupName = x.DeviceGroupName,
                PriorityID = x.PriorityID
            }), JsonRequestBehavior.AllowGet);

        [Authorize]
        public JsonResult SavePlan(string data)
        {
            List<Model.Data.MasterPlan> lstPlans = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.Data.MasterPlan>>(data);
            try
            {
                foreach (var item in lstPlans)
                {
                    item.Username = User.Identity.Name;
                    masterPlanUnitOfWork.MasterPlanRepository.Add(item);
                    var lstPreventiveMaintenanceOfWork = masterPlanUnitOfWork.PreventiveMaintenanceOfWorkRepository.GetPreventiveMaintenanceOfWork(item.DeviceID, item.TypeOfMaintenanceID.Value);
                    foreach (var preventiveMaintenanceOfWork in lstPreventiveMaintenanceOfWork)
                    {
                        Work work = masterPlanUnitOfWork.WorkRepository.GetWorkByID(preventiveMaintenanceOfWork.WorkID);
                        ComponentOfWork workComponent = masterPlanUnitOfWork.WorkComponentRepository.GetWorkComponentByID(preventiveMaintenanceOfWork.DeviceID, preventiveMaintenanceOfWork.ComponentID, preventiveMaintenanceOfWork.WorkID);
                        masterPlanUnitOfWork.MasterPlansOfWorkRepository.Add(new Model.Data.MasterPlansOfWork
                        {
                            DeviceID = preventiveMaintenanceOfWork.DeviceID,
                            PlanID = item.ID,
                            WorkID = preventiveMaintenanceOfWork.WorkID,
                            ComponentID = preventiveMaintenanceOfWork.ComponentID,
                            TimePlan = work.TimePlan.HasValue ? work.TimePlan : workComponent.TimePlan,
                            AmountOfPeople = work.AmountOfPeople,
                            InquiryHR = workComponent.InquiryHR,
                            Tool = workComponent.Tool
                        });

                        var lstPreventiveMaintenanceOfSparePart = masterPlanUnitOfWork.PreventiveMaintenanceOfSparePartRepository.GetPreventiveMaintenanceOfSparePart(item.DeviceID, item.TypeOfMaintenanceID.Value, preventiveMaintenanceOfWork.WorkID);
                        foreach (var preventiveMaintenanceOfSparePart in lstPreventiveMaintenanceOfSparePart)
                        {
                            masterPlanUnitOfWork.MasterPlansOfSparePartRepository.Add(new Model.Data.MasterPlansOfSparePart
                            {
                                DeviceID = preventiveMaintenanceOfSparePart.DeviceID,
                                PlanID = item.ID,
                                WorkID = preventiveMaintenanceOfSparePart.WorkID,
                                ComponentID = preventiveMaintenanceOfSparePart.ComponentID,
                                SparePartID = preventiveMaintenanceOfSparePart.SparePartID,
                                Amount = preventiveMaintenanceOfSparePart.Amount
                            });
                        }
                    }
                    masterPlanUnitOfWork.Save();
                }
                return Json("success");
            }
            catch (Exception ex)
            {
                return Json("failed: " + ex.InnerException.Message);
            }
        }

        [Authorize]
        public JsonResult SaveWO(string data)
        {
            List<Model.Data.WorkOrder> lstWO = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.Data.WorkOrder>>(data);
            try
            {
                List<string> lstWONumber = new List<string>();
                List<WorkOrderOfWork> lstWork = new List<WorkOrderOfWork>();
                List<WorkOrderOfWorkIncludeSparePart> lstSparePart = new List<WorkOrderOfWorkIncludeSparePart>();
                List<WorkOrderOfWorkIncludeDetailSparePart> lstDetailSparePart = new List<WorkOrderOfWorkIncludeDetailSparePart>();
                List<WorkOrderDetailsByTheState> lstWOStatus = new List<WorkOrderDetailsByTheState>();
                List<ComponentOfSparePart> lstSparePartComponent = new List<ComponentOfSparePart>();

                string workOrderID = workOrderUnitOfWork.WorkOrderRepository.GenerateWorkOrderID(User.Identity.Name, DateTime.Now.Year, DateTime.Now.Month);
                foreach (var item in lstWO)
                {
                    item.ID = workOrderID;
                    item.DateCreated = DateTime.Now;
                    item.TimeCreated = DateTime.Now;
                    item.UserCreated = User.Identity.Name;
                    item.PriorityID = item.PriorityID == null ? workOrderUnitOfWork.PriorityRepository.ListAll().Max(x => x.ID) : item.PriorityID;
                    item.TheStatusOfWorkOrderID = workOrderUnitOfWork.TheStatusDetailsOfWorkOrderRepository.GetTheStatusDetailsOfWorkOrderByID(item.TheStatusOfWorkOrderID ?? 1).TheStatusOfWorkOrderID;
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
                    var lstPreventiveMaintenanceOfWork = workOrderUnitOfWork.PreventiveMaintenanceOfWorkRepository.GetPreventiveMaintenanceOfWork(item.DeviceID, item.TypeOfMaintenanceID.Value);
                    foreach (var preventiveMaintenanceOfWork in lstPreventiveMaintenanceOfWork)
                    {
                        Work work = workOrderUnitOfWork.WorkRepository.GetWorkByID(preventiveMaintenanceOfWork.WorkID);
                        ComponentOfWork workComponent = workOrderUnitOfWork.WorkComponentRepository.GetWorkComponentByID(preventiveMaintenanceOfWork.DeviceID, preventiveMaintenanceOfWork.ComponentID, preventiveMaintenanceOfWork.WorkID);
                        lstWork.Add(new Model.Data.WorkOrderOfWork
                        {
                            WorkOrderID = workOrderID,
                            WorkID = preventiveMaintenanceOfWork.WorkID,
                            ComponentID = preventiveMaintenanceOfWork.ComponentID,
                            Tool = workComponent.Tool,
                            InquiryHR = workComponent.InquiryHR,
                            HourPlan = workComponent.TimePlan,
                            ActualTime  = workComponent.TimePlan,
                            Action = workComponent.Action,
                            QualityCheck = workComponent.QualityCheck,
                            AmountOfPeople = work.AmountOfPeople,
                            Path = workComponent.Path,
                            WorkStartDate = item.StartDatePlan,
                            WorkEndDate = item.StartDatePlan
                        });

                        if (workOrderUnitOfWork.GeneralRepository.GetGeneralInfo().THEM_CV_PT.HasValue && workOrderUnitOfWork.GeneralRepository.GetGeneralInfo().THEM_CV_PT.Value != 0) continue;

                        var lstPreventiveMaintenanceOfSparePart = workOrderUnitOfWork.PreventiveMaintenanceOfSparePartRepository.GetPreventiveMaintenanceOfSparePart(item.DeviceID, item.TypeOfMaintenanceID.Value, preventiveMaintenanceOfWork.WorkID);
                        foreach (var preventiveMaintenanceOfSparePart in lstPreventiveMaintenanceOfSparePart)
                        {
                            Model.Data.WorkOrderOfWorkIncludeSparePart WOObject = new Model.Data.WorkOrderOfWorkIncludeSparePart
                            {
                                WorkOrderID = workOrderID,
                                WorkID = preventiveMaintenanceOfSparePart.WorkID,
                                ComponentID = preventiveMaintenanceOfSparePart.ComponentID,
                                SparePartID = preventiveMaintenanceOfSparePart.SparePartID,
                                PlanQuantity = preventiveMaintenanceOfSparePart.Amount
                            };
                            var lst = workOrderUnitOfWork.SparePartComponentRepository.GetSparePartComponentByID(item.DeviceID, preventiveMaintenanceOfSparePart.ComponentID, preventiveMaintenanceOfSparePart.SparePartID);
                            double sum = preventiveMaintenanceOfSparePart.Amount;
                            foreach (var component in lst)
                            {
                                double quality = sum - component.Amount.Value;
                                Model.Data.WorkOrderOfWorkIncludeDetailSparePart obj = new Model.Data.WorkOrderOfWorkIncludeDetailSparePart
                                {
                                    WorkOrderID = workOrderID,
                                    WorkID = preventiveMaintenanceOfSparePart.WorkID,
                                    ComponentID = preventiveMaintenanceOfSparePart.ComponentID,
                                    SparePartID = preventiveMaintenanceOfSparePart.SparePartID,
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
                                    lstDetailSparePart.Add(obj);
                            }
                            WOObject.PlanQuantity = lstDetailSparePart.Where(x => x.WorkID == WOObject.WorkID && x.SparePartID == WOObject.SparePartID && x.ComponentID == WOObject.ComponentID && x.PlanQuantity > 0).Sum(x => x.PlanQuantity);
                            lstSparePart.Add(WOObject);
                        }
                    }
                    lstWONumber.Add(workOrderID);
                    workOrderID = workOrderID.Split('-')[0] + "-" + (Convert.ToDouble(workOrderID.Split('-')[1]) + 1).ToString();
                }
                workOrderUnitOfWork.WorkOrderRepository.AddRange(lstWO);
                workOrderUnitOfWork.WorkOrderDetailByTheStateRepository.AddRange(lstWOStatus);
                workOrderUnitOfWork.WorkOrderOfWorkRepository.AddRange(lstWork);
                workOrderUnitOfWork.WorkOrderOfSparePartRepository.AddRange(lstSparePart);
                workOrderUnitOfWork.SparePartComponentRepository.AddRange(lstSparePartComponent);
                workOrderUnitOfWork.Save();
                try
                {
                    lstWONumber.ForEach(x => workOrderUnitOfWork.WorkOrderRepository.AutoAddNewStaffIntoWorkOrder(x.ToString()));
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