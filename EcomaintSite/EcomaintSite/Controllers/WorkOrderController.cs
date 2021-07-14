using Model.Combobox;
using Model.Data;
using Model.Interface;
using Model.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace EcomaintSite.Controllers
{
    public class WorkOrderController : Controller
    {
        IStaffRepository staffRepository;
        IDeviceRepository deviceRepository;
        IPriorityRepository priorityRepository;
        IGeneralRepository generalRepository;
        IWorkOrderRepository workOrderRepository;
        ITypeOfMaintenanceRepository typeOfMaintenanceRepository;
        IComponentRepository componentRepository;
        IWorkOrderOfSparePartRepository workOrderOfSparePartRepository;
        IWorkComponentRepository workComponentRepository;
        IWorkRepository workRepository;
        IWorkOrderOfWorkRepository workOrderOfWorkRepository;
        ISparePartRepository sparePartRepository;
        IWorkOrderUnitOfWork workOrderUnitOfWork;

        private ICombobox _Combobox;
        private ICombobox Combobox()
        {
            return _Combobox ?? (_Combobox = new Combobox());
        }

        public WorkOrderController(IStaffRepository _staffRepository, IDeviceRepository _deviceRepository, IPriorityRepository _priorityRepository, IGeneralRepository _generalRepository, IWorkOrderRepository _workOrderRepository, ITypeOfMaintenanceRepository _typeOfMaintenanceRepository, IComponentRepository _componentRepository, IWorkOrderOfSparePartRepository _workOrderOfSparePartRepository, IWorkComponentRepository _workComponentRepository, IWorkRepository _workRepository, IWorkOrderOfWorkRepository _workOrderOfWorkRepository, ISparePartRepository _sparePartRepository, IWorkOrderUnitOfWork _workOrderUnitOfWork)
        {
            staffRepository = _staffRepository;
            deviceRepository = _deviceRepository;
            priorityRepository = _priorityRepository;
            generalRepository = _generalRepository;
            workOrderRepository = _workOrderRepository;
            typeOfMaintenanceRepository = _typeOfMaintenanceRepository;
            componentRepository = _componentRepository;
            workOrderOfSparePartRepository = _workOrderOfSparePartRepository;
            workComponentRepository = _workComponentRepository;
            workRepository = _workRepository;
            workOrderOfWorkRepository = _workOrderOfWorkRepository;
            sparePartRepository = _sparePartRepository;
            workOrderUnitOfWork = _workOrderUnitOfWork;
        }
        
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.link = 0;
            ViewBag.cboCreatedBy = new SelectList(staffRepository.GetStaffRole(2, User.Identity.Name).Select(x =>
            new
            {
                ID = x.ID,
                Name = x.Name
            }).ToList(), "ID", "Name");

            ViewBag.cboDevice = new SelectList(deviceRepository.ListAll().Select(x =>
            new Model.Data.DeviceObjForDropdown
            {
                ID = x.ID,
                Name = x.ID + " - " + (SessionVariable.TypeLanguage == 0 ? x.Name : x.NameOfEng)
            }).ToList(), "ID", "Name");

            ViewBag.cboPriority = new SelectList(priorityRepository.ListAll().Select(x =>
            new
            {
                ID = x.ID,
                Name = (SessionVariable.TypeLanguage == 0 ? x.Name : x.NameOfEng)
            }), "ID", "Name");
            ViewBag.IsAuto = generalRepository.GetGeneralInfo().IsAuto;
            ViewBag.NhaXuong = Combobox().GetCbbDiaDiem(User.Identity.Name, SessionVariable.TypeLanguage,1);
            return View();
        }

        [Authorize]
        public ActionResult Show(string msnx, string msmay,string todate,string fromdate)
        {
            ViewBag.link = 1;
            ViewBag.nx = msnx;
            ViewBag.may = msmay;
            ViewBag.tngay = Convert.ToDateTime(todate, new CultureInfo("vi-vn")).ToString("dd/MM/yyyy");
            ViewBag.dngay = Convert.ToDateTime(fromdate, new CultureInfo("vi-vn")).ToString("dd/MM/yyyy");
            ViewBag.cboCreatedBy = new SelectList(staffRepository.GetStaffRole(2, User.Identity.Name).Select(x =>
                        new
                        {
                            ID = x.ID,
                            Name = x.Name
                        }).ToList(), "ID", "Name");

            ViewBag.cboDevice = new SelectList(deviceRepository.ListAll().Select(x =>
            new Model.Data.DeviceObjForDropdown
            {
                ID = x.ID,
                Name = x.ID + " - " + (SessionVariable.TypeLanguage == 0 ? x.Name : x.NameOfEng)
            }).ToList(), "ID", "Name");

            ViewBag.cboPriority = new SelectList(priorityRepository.ListAll().Select(x =>
            new
            {
                ID = x.ID,
                Name = (SessionVariable.TypeLanguage == 0 ? x.Name : x.NameOfEng)
            }), "ID", "Name");
            ViewBag.IsAuto = generalRepository.GetGeneralInfo().IsAuto;
            ViewBag.NhaXuong = Combobox().GetCbbDiaDiem(User.Identity.Name, SessionVariable.TypeLanguage, 1);
            return View("~/Views/WorkOrder/Index.cshtml", deviceRepository.ListAll().Select(x => new DeviceObjForDropdown
            {
                ID = x.ID,
                Name = x.Name
            }).ToList());
        }

        [Authorize]
        public ActionResult GetWorkOrderNumber() =>
            Json(workOrderRepository.GenerateWorkOrderID(User.Identity.Name, DateTime.Now.Year, DateTime.Now.Month), JsonRequestBehavior.AllowGet);

        [Authorize]
        public ActionResult GetTypeOfMaintenanceByDevice(string deviceID) =>
             Json(typeOfMaintenanceRepository.GetTypeOfMaintenanceByDevice(deviceID).Select(x => new
             {
                 TypeOfMaintenanceID = x.ID,
                 TypeOfMaintenanceName = x.Name
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
        public JsonResult GetSparePart() => Json(sparePartRepository.GetSparePart(User.Identity.Name).Select(x =>
            new
            {
                SparePartID = x.ID,
                SparePartName = SessionVariable.TypeLanguage == 0 ? x.Name : x.NameOfEng,
                Specification = x.Specification,
                UnitOfCalculationID = x.UnitOfCalculationID
            }).ToList(), JsonRequestBehavior.AllowGet);
        [Authorize]
        public JsonResult FilterData(string  msnx,string msmay,string fromDate, string toDate, int type)
        {
            try
            {
                if (string.IsNullOrEmpty(msmay)) msmay = "-1";
                var model = workOrderRepository.GetWorkOrder(msnx,msmay,User.Identity.Name, Convert.ToDateTime(fromDate, new System.Globalization.CultureInfo("vi-vn")), Convert.ToDateTime(toDate, new System.Globalization.CultureInfo("vi-vn")), type, SessionVariable.TypeLanguage).Select(x => new
                {
                    Document = x.Document,
                    ID = x.WorkOrderID,
                    DeviceID = x.DeviceID,
                    DeviceName = x.DeviceName,
                    TheStatusOfWorkOrderID = x.TheStatusOfWorkOrderID,
                    PriorityName = x.PriorityName,
                    TheStatusDetailsOfWorkOrderName = x.TheStatusDetailsOfWorkOrderName,
                    TheStatusOfWorkOrderName = x.TheStatusOfWorkOrderName,
                    Reason = x.Reason,
                    DateCreated = x.DateCreated.HasValue ? x.DateCreated.Value.ToString("dd/MM/yyyy") : "",
                    WorkSiteName = x.WorkSiteName,
                    TypeOfMaintenanceID = x.TypeOfMaintenanceID,
                    TimeCreated = x.TimeCreated.HasValue ? x.TimeCreated.Value.ToString("HH:mm") : "",
                    TypeOfMaintenanceName = x.TypeOfMaintenanceName,
                    CreatedBy = x.CreatedBy,
                    UserCreated = x.UserCreated,
                    StaffName = x.StaffName,
                    MonitoredBy = x.MonitoredBy,
                    StartDatePlan = x.StartDatePlan.HasValue ? x.StartDatePlan.Value.ToString("dd/MM/yyyy") : "",
                    EndDatePlan = x.EndDatePlan.HasValue ? x.EndDatePlan.Value.ToString("dd/MM/yyyy") : "",
                    BreakdownStartTime = x.BreakdownStartTime.HasValue ? x.BreakdownStartTime.Value.ToString("HH:mm") : "",
                    BreakdownStartDate = x.BreakdownStartDate.HasValue ? x.BreakdownStartDate.Value.ToString("dd/MM/yyyy") : "",
                    BreakdownEndTime = x.BreakdownEndTime.HasValue ? x.BreakdownEndTime.Value.ToString("HH:mm") : "",
                    BreakdownEndDate = x.BreakdownEndDate.HasValue ? x.BreakdownEndDate.Value.ToString("dd/MM/yyyy") : "",
                    IssuedID = x.IssuedID,
                    LastDateModified = x.LastDateModified.HasValue ? x.LastDateModified.Value.ToString("dd/MM/yyyy") : ""
                });
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public JsonResult GetWorkOrderDetail(string workOrderID)
        {
            var model = workOrderOfSparePartRepository.GetWorkOrderOfSparePart(workOrderID).Select(x => new
            {
                DeviceID = new WorkOrderRepository().GetWorkOrderByID(workOrderID).DeviceID,
                WorkID = x.WorkID,
                ComponentID = x.ComponentID,
                Description = SessionVariable.TypeLanguage == 0 ? workRepository.GetWorkByID(x.WorkID).Description : workRepository.GetWorkByID(x.WorkID).DescriptionOfEnglish,
                SparePartID = x.SparePartID,
                PlanQuantity = x.PlanQuantity,
                ActualQuantity = x.ActualQuantity
            });

            if (model.Count() == 0)
            {
                return Json(new
                {
                    times = 2,
                    model = workOrderOfWorkRepository.GetWorkOrderOfWork(workOrderID).Select(x => new
                    {
                        DeviceID = workOrderRepository.GetWorkOrderByID(workOrderID).DeviceID,
                        WorkID = x.WorkID,
                        ComponentID = x.ComponentID,
                        Description = SessionVariable.TypeLanguage == 0 ? workRepository.GetWorkByID(x.WorkID).Description : workRepository.GetWorkByID(x.WorkID).DescriptionOfEnglish,
                        SparePartID = "",
                        PlanQuantity = Convert.ToDouble(0),
                        ActualQuantity = Convert.ToDouble(0)
                    })
                }
                , JsonRequestBehavior.AllowGet);

            }
            return Json(new { times = 1, model = model }, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public JsonResult GetWorkDetailByWorkOrder(string workOrderID)
        {
            var model = workOrderOfWorkRepository.GetWorkOrderOfWork(workOrderID).Select(x => new
            {
                WorkOrderID = x.WorkOrderID,
                WorkID = x.WorkID,
                ActualTime = x.ActualTime.HasValue ? x.ActualTime : 0,
                ComponentID = x.ComponentID,
                Description = SessionVariable.TypeLanguage == 0 ? workRepository.GetWorkByID(x.WorkID).Description : workRepository.GetWorkByID(x.WorkID).DescriptionOfEnglish
            });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult GetSparePartByWorkOrder(string workOrderID, string componentID, int workID)
        {
            var model = workOrderOfSparePartRepository.GetWorkOrderOfSparePartByWork(workOrderID, componentID, workID).Select(x => new
            {
                SparePartID = x.SparePartID,
                Name = SessionVariable.TypeLanguage == 0 ? sparePartRepository.GetSparePartByID(x.SparePartID).Name : sparePartRepository.GetSparePartByID(x.SparePartID).NameOfEng,
                PlanQuantity = x.PlanQuantity.HasValue ? x.PlanQuantity : 0,
                ActualQuantity = x.ActualQuantity.HasValue ? x.ActualQuantity : 0,
            });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult CreateWO(string data, string data2, string data3)
        {
            List<WorkOrder> lstWO = JsonConvert.DeserializeObject<List<WorkOrder>>(data);
            List<WorkOrderOfWork> lstWorkOfWO = JsonConvert.DeserializeObject<List<WorkOrderOfWork>>(data2);
            List<WorkOrderOfWorkIncludeSparePart> lstSparePartOfWO = JsonConvert.DeserializeObject<List<WorkOrderOfWorkIncludeSparePart>>(data3);
            List<WorkOrderOfWorkIncludeDetailSparePart> lstSparePartDetailsOfWO = new List<WorkOrderOfWorkIncludeDetailSparePart>();
            List<WorkOrderDetailsByTheState> lstWOStatus = new List<WorkOrderDetailsByTheState>();
            List<ComponentOfSparePart> lstSparePartOfComponent = new List<ComponentOfSparePart>();
            try
            {
                bool isAuto = workOrderUnitOfWork.GeneralRepository.GetGeneralInfo().IsAuto.HasValue ? true : false;
                if (isAuto == true)
                {
                    lstWorkOfWO = new List<WorkOrderOfWork>();
                    lstSparePartOfWO = new List<WorkOrderOfWorkIncludeSparePart>();
                }

                string workOrderID = workOrderUnitOfWork.WorkOrderRepository.GenerateWorkOrderID(User.Identity.Name, DateTime.Now.Year, DateTime.Now.Month);
                foreach (var item in lstWO)
                {
                    item.ID = item.ID == "" ? workOrderID : item.ID;
                    item.UserCreated = User.Identity.Name;
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

                    if (isAuto == true)
                    {
                        if (workOrderUnitOfWork.TheFormalityOfMaintenanceRepository.IsPeriodicMaintenance(workOrderUnitOfWork.TypeOfMaintenanceRepository.GetTypeOfMaintenance(item.TypeOfMaintenanceID.Value).MaintenanceFormID))
                        {
                            var lstPreventiveMaintenanceOfWork = workOrderUnitOfWork.PreventiveMaintenanceOfWorkRepository.GetPreventiveMaintenanceOfWork(item.DeviceID, item.TypeOfMaintenanceID.Value);
                            foreach (var preventiveMaintenanceOfWork in lstPreventiveMaintenanceOfWork)
                            {
                                Work congViec = workOrderUnitOfWork.WorkRepository.GetWorkByID(preventiveMaintenanceOfWork.WorkID);
                                ComponentOfWork workComponent = workOrderUnitOfWork.WorkComponentRepository.GetWorkComponentByID(preventiveMaintenanceOfWork.DeviceID, preventiveMaintenanceOfWork.ComponentID, preventiveMaintenanceOfWork.WorkID);
                                lstWorkOfWO.Add(new Model.Data.WorkOrderOfWork
                                {
                                    WorkOrderID = workOrderID,
                                    WorkID = preventiveMaintenanceOfWork.WorkID,
                                    ComponentID = preventiveMaintenanceOfWork.ComponentID,
                                    Tool = workComponent.Tool,
                                    InquiryHR = workComponent.InquiryHR,
                                    HourPlan = workComponent.TimePlan,
                                    ActualTime = workComponent.TimePlan,
                                    Action = workComponent.Action,
                                    QualityCheck = workComponent.QualityCheck,
                                    AmountOfPeople = congViec.AmountOfPeople,
                                    Path = workComponent.Path,
                                    WorkStartDate = item.StartDatePlan,
                                    WorkEndDate = item.StartDatePlan
                                });
                                /**************************************************************************************************/
                                if (workOrderUnitOfWork.GeneralRepository.GetGeneralInfo().THEM_CV_PT.HasValue && workOrderUnitOfWork.GeneralRepository.GetGeneralInfo().THEM_CV_PT.Value != 0) continue;
                                /**************************get thông tin cần thiết - thêm công việc pt pbt*************************/
                                var lstSparePart = workOrderUnitOfWork.PreventiveMaintenanceOfSparePartRepository.GetPreventiveMaintenanceOfSparePart(item.DeviceID, item.TypeOfMaintenanceID.Value, preventiveMaintenanceOfWork.WorkID);
                                foreach (var sparePart in lstSparePart)
                                {
                                    if (sparePart.Amount <= 0) continue;
                                    if (workOrderUnitOfWork.TypeOfMaterialRepository.GetTypeOfMaterial(workOrderUnitOfWork.SparePartRepository.GetSparePartByID(sparePart.SparePartID).ID).IsMaterial == false && !workOrderUnitOfWork.SparePartComponentRepository.CheckExists(item.DeviceID, sparePart.ComponentID, sparePart.SparePartID))
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
                                    Model.Data.WorkOrderOfWorkIncludeSparePart sparePartOfWO = new Model.Data.WorkOrderOfWorkIncludeSparePart
                                    {
                                        WorkOrderID = workOrderID,
                                        WorkID = sparePart.WorkID,
                                        ComponentID = sparePart.ComponentID,
                                        SparePartID = sparePart.SparePartID,
                                        PlanQuantity = sparePart.Amount
                                    };
                                    var lst = workOrderUnitOfWork.SparePartComponentRepository.GetSparePartComponentByID(item.DeviceID, sparePart.ComponentID, sparePart.SparePartID);
                                    double sum = sparePart.Amount;
                                    foreach (var component in lst)
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
                                    sparePartOfWO.PlanQuantity = lstSparePartDetailsOfWO.Where(x => x.WorkID == sparePartOfWO.WorkID && x.SparePartID == sparePartOfWO.SparePartID && x.ComponentID == sparePartOfWO.ComponentID && x.PlanQuantity > 0).Sum(x => x.PlanQuantity);
                                    lstSparePartOfWO.Add(sparePartOfWO);
                                }
                            }
                        }
                        continue;
                    }
                    else
                    {
                        foreach (var workOfWO in lstWorkOfWO)
                        {
                            ComponentOfWork workComponent = workOrderUnitOfWork.WorkComponentRepository.GetWorkComponentByID(item.DeviceID, workOfWO.ComponentID, workOfWO.WorkID);
                            workOfWO.WorkOrderID = workOrderID;
                            workOfWO.Tool = workComponent.Tool;
                            workOfWO.InquiryHR = workComponent.InquiryHR;
                            workOfWO.HourPlan = workComponent.TimePlan;
                            workOfWO.ActualTime = workComponent.TimePlan;
                            workOfWO.Action = workComponent.Action;
                            workOfWO.QualityCheck = workComponent.QualityCheck;
                            workOfWO.Path = workComponent.Path;
                            workOfWO.WorkStartDate = item.StartDatePlan;
                            workOfWO.WorkEndDate = item.StartDatePlan;
                        }
                        foreach (var sparePartOfWO in lstSparePartOfWO)
                        {
                            if (workOrderUnitOfWork.TypeOfMaterialRepository.GetTypeOfMaterial(workOrderUnitOfWork.SparePartRepository.GetSparePartByID(sparePartOfWO.SparePartID).TypeOfMaterialID).IsMaterial == false && !workOrderUnitOfWork.SparePartComponentRepository.CheckExists(item.DeviceID, sparePartOfWO.ComponentID, sparePartOfWO.SparePartID) && !lstSparePartOfComponent.Any(x => x.DeviceID == item.DeviceID && x.ComponentID == sparePartOfWO.ComponentID && x.SparePartID == sparePartOfWO.SparePartID))
                            {
                                lstSparePartOfComponent.Add(new Model.Data.ComponentOfSparePart
                                {
                                    Active = true,
                                    Amount = sparePartOfWO.PlanQuantity,
                                    DeviceID = item.DeviceID,
                                    ComponentID = sparePartOfWO.ComponentID,
                                    SparePartID = sparePartOfWO.SparePartID,
                                    LocationID = "A"
                                });
                            }
                            sparePartOfWO.WorkOrderID = workOrderID;
                            Model.Data.WorkOrderOfWorkIncludeDetailSparePart obj = new Model.Data.WorkOrderOfWorkIncludeDetailSparePart
                            {
                                WorkOrderID = workOrderID,
                                WorkID = sparePartOfWO.WorkID,
                                ComponentID = sparePartOfWO.ComponentID,
                                SparePartID = sparePartOfWO.SparePartID,
                                LocationID = "A",
                                PlanQuantity = sparePartOfWO.PlanQuantity
                            };
                            lstSparePartDetailsOfWO.Add(obj);
                        }
                    }
                    //WO-2017010001 
                }
                //string phieuBTNEW = workOrderUnitOfWork.WorkOrderRepository.GenerateWorkOrderID(User.Identity.Name, DateTime.Now.Year, DateTime.Now.Month);
                //if (lstWO.OrderBy(x => x.WorkOrderID).Take(1).SingleOrDefault().WorkOrderID != phieuBTNEW)
                //{
                //    return Json("failed");
                //}

                workOrderUnitOfWork.SparePartComponentRepository.AddRange(lstSparePartOfComponent);
                workOrderUnitOfWork.WorkOrderRepository.AddRange(lstWO);
                workOrderUnitOfWork.WorkOrderDetailByTheStateRepository.AddRange(lstWOStatus);
                workOrderUnitOfWork.WorkOrderOfWorkRepository.AddRange(lstWorkOfWO);
                workOrderUnitOfWork.WorkOrderOfSparePartRepository.AddRange(lstSparePartOfWO);
                workOrderUnitOfWork.WorkOrderIncludeDetailSparePartRepository.AddRange(lstSparePartDetailsOfWO);
                workOrderUnitOfWork.Save();
                return Json(new { Message = "success" });
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
                List<WorkOrder> lstWO = JsonConvert.DeserializeObject<List<WorkOrder>>(data);
                List<WorkOrderOfWork> lstWorkOfWO = JsonConvert.DeserializeObject<List<WorkOrderOfWork>>(data2);
                List<WorkOrderOfWorkIncludeSparePart> lstSparePartOfWO = JsonConvert.DeserializeObject<List<WorkOrderOfWorkIncludeSparePart>>(data3);
                List<ComponentOfSparePart> lstSparePartOfComponent = new List<ComponentOfSparePart>();
                List<WorkOrderOfWorkIncludeDetailSparePart> lstSparePartDetailsOfWO = new List<WorkOrderOfWorkIncludeDetailSparePart>();

                workOrderUnitOfWork.WorkOrderRepository.Edit(lstWO[0]);

                workOrderUnitOfWork.WorkOrderIncludeDetailSparePartRepository.Delete(lstWO[0].ID);
                workOrderUnitOfWork.WorkOrderOfSparePartRepository.Delete(lstWO[0].ID);
                workOrderUnitOfWork.WorkOrderOfWorkRepository.Delete(lstWO[0].ID);

                //workOrderUnitOfWork.Save();

                lstWorkOfWO.ForEach(x => x.WorkOrderID = lstWO[0].ID);
                lstSparePartOfWO.ForEach(x => x.WorkOrderID = lstWO[0].ID);

                foreach (var workOfWO in lstWorkOfWO)
                {
                    ComponentOfWork workComponent = workOrderUnitOfWork.WorkComponentRepository.GetWorkComponentByID(lstWO[0].DeviceID, workOfWO.ComponentID, workOfWO.WorkID);
                    workOfWO.WorkOrderID = lstWO[0].ID;
                    workOfWO.Tool = workComponent.Tool;
                    workOfWO.InquiryHR = workComponent.InquiryHR;
                    workOfWO.HourPlan = workComponent.TimePlan;
                    workOfWO.ActualTime = workComponent.TimePlan;
                    workOfWO.Action = workComponent.Action;
                    workOfWO.QualityCheck = workComponent.QualityCheck;
                    workOfWO.Path = workComponent.Path;
                    workOfWO.WorkStartDate = lstWO[0].StartDatePlan;
                    workOfWO.WorkEndDate = lstWO[0].StartDatePlan;
                }
                foreach (var sparePartOfWO in lstSparePartOfWO)
                {
                    if (workOrderUnitOfWork.TypeOfMaterialRepository.GetTypeOfMaterial(workOrderUnitOfWork.SparePartRepository.GetSparePartByID(sparePartOfWO.SparePartID).TypeOfMaterialID).IsMaterial == false && !workOrderUnitOfWork.SparePartComponentRepository.CheckExists(lstWO[0].DeviceID, sparePartOfWO.ComponentID, sparePartOfWO.SparePartID) && !lstSparePartOfComponent.Any(x => x.DeviceID == lstWO[0].DeviceID && x.ComponentID == sparePartOfWO.ComponentID && x.SparePartID == sparePartOfWO.SparePartID))
                    {
                        lstSparePartOfComponent.Add(new Model.Data.ComponentOfSparePart
                        {
                            Active = true,
                            Amount = sparePartOfWO.PlanQuantity,
                            DeviceID = lstWO[0].DeviceID,
                            ComponentID = sparePartOfWO.ComponentID,
                            SparePartID = sparePartOfWO.SparePartID,
                            LocationID = "A"
                        });
                    }
                    sparePartOfWO.WorkOrderID = lstWO[0].ID;
                    Model.Data.WorkOrderOfWorkIncludeDetailSparePart obj = new Model.Data.WorkOrderOfWorkIncludeDetailSparePart
                    {
                        WorkOrderID = lstWO[0].ID,
                        WorkID = sparePartOfWO.WorkID,
                        ComponentID = sparePartOfWO.ComponentID,
                        SparePartID = sparePartOfWO.SparePartID,
                        LocationID = "A",
                        PlanQuantity = sparePartOfWO.PlanQuantity
                    };
                    lstSparePartDetailsOfWO.Add(obj);
                }
                workOrderUnitOfWork.SparePartComponentRepository.AddRange(lstSparePartOfComponent);
                workOrderUnitOfWork.WorkOrderOfWorkRepository.AddRange(lstWorkOfWO);
                workOrderUnitOfWork.WorkOrderOfSparePartRepository.AddRange(lstSparePartOfWO);
                workOrderUnitOfWork.WorkOrderIncludeDetailSparePartRepository.AddRange(lstSparePartDetailsOfWO);
                workOrderUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                return Json("failed: " + ex.Message);
            }
            return Json("success");
        }
        public JsonResult Remove(string workOrderID)
        {
            if (workOrderUnitOfWork.WorkOrderRepository.XoaPhieuBaoTri(workOrderID) == 1)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("failure", JsonRequestBehavior.AllowGet);
            }
        }
    }
}