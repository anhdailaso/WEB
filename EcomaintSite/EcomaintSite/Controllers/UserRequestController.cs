using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using Model.Data;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using Model.Interface;
using EcomaintSite.UtilityHelpers;
using System.IO;
using Model.Combobox;
using Model.Data.ViewModel;

namespace EcomaintSite.Controllers
{
    public class UserRequestController : Controller
    {
        IWorkSiteRepository workSiteRepository;
        IUserRequestRepository userRequestRepository;
        IDeviceRepository deviceRepository;
        IUserRequestDetailRepository userRequestDetailRepository;
        IUserRequestUnitOfWork userRequestUnitOfWork;
        IUserRequestComponentRepository userRequestComponentRepository;
        IUserRequestDocumentRepository userRequestDocumentRepository;


        private ICombobox _Combobox;
        private ICombobox Combobox()
        {
            return _Combobox ?? (_Combobox = new Combobox());
        }

        public UserRequestController(IWorkSiteRepository _workSiteRepository, IUserRequestRepository _userRequestRepository, IDeviceRepository _deviceRepository, IUserRequestDetailRepository _userRequestDetailRepository, IUserRequestComponentRepository _userRequestComponentRepository, IUserRequestDocumentRepository _userRequestDocumentRepository, IUserRequestUnitOfWork _userRequestUnitOfWork)
        {
            workSiteRepository = _workSiteRepository;
            userRequestRepository = _userRequestRepository;
            deviceRepository = _deviceRepository;
            userRequestDetailRepository = _userRequestDetailRepository;
            userRequestUnitOfWork = _userRequestUnitOfWork;
            userRequestComponentRepository = _userRequestComponentRepository;
            userRequestDocumentRepository = _userRequestDocumentRepository;
        }

        [Authorize]
        public ActionResult Index()
        {
            GetCreatedBy();
            return View();
        }

        [Authorize]
        public JsonResult FilterData(string fromDate, string toDate, string createdBy, string MS_N_XUONG) =>
            Json(userRequestRepository.GetUserRequest(createdBy, MS_N_XUONG, Convert.ToDateTime(fromDate, new CultureInfo("vi-vn")).ToString("yyyy/MM/dd"), Convert.ToDateTime(toDate, new CultureInfo("vi-vn")).ToString("yyyy/MM/dd"), User.Identity.GetUserName(), SessionVariable.TypeLanguage).Select(x => new
            {
                ID = x.ID,
                UserRequestID = x.UserRequestID,
                DateCreated = x.DateCreated.ToString("dd/MM/yyyy"),
                HourCreated = x.HourCreated.ToString("HH:mm"),
                RequestedBy = x.RequestedBy,
                DateCompleted = x.DateCompleted.HasValue ? x.DateCompleted.Value.ToString("dd/MM/yyyy") : "",
                Email = x.Email,
                WorkSiteID = x.WorkSiteID,
                DeviceID = x.DeviceID

            }).OrderByDescending(x => x.UserRequestID), JsonRequestBehavior.AllowGet);

        [Authorize]
        public void GetCreatedBy(string id = "-1")
        {
            ViewBag.cboCreatedBy = new SelectList(userRequestRepository.GetCreatedBy(), "RequestedBy", "RequestedName", id);
            ViewBag.cboNXuong = new SelectList(userRequestRepository.GetNhaXuong(User.Identity.Name, SessionVariable.TypeLanguage, 1), "MS_N_XUONG", "Ten_N_XUONG", id);
        }

        [Authorize]
        public void GetCombobox(string WorkSiteID)
        {
            List<Model.Data.ViewModel.DiaDiemViewModel> lst = new List<Model.Data.ViewModel.DiaDiemViewModel>();
            lst = userRequestRepository.GetNhaXuong(User.Identity.Name, SessionVariable.TypeLanguage, 0).ToList();
            //ViewBag.cboWorkSite = new SelectList(workSiteRepository.GetWorkSiteByID(User.Identity.GetUserName(), SessionVariable.TypeLanguage.ToString()), "ID", "Name", WorkSiteID);
            ViewBag.cboWorkSite = Combobox().GetCbbDiaDiem(User.Identity.Name, SessionVariable.TypeLanguage, 1);
            ViewBag.cboDevice = new SelectList(deviceRepository.GetDeviceByRequest(User.Identity.GetUserName(), WorkSiteID, "-1"), "ID", "Name", "-1");
            ViewBag.cbonguyennhan = userRequestDetailRepository.DanhSachNguyenNhan();
            ViewBag.cboyeucaubaotri = userRequestDetailRepository.DanhSachLoaiBaoTri();
            ViewBag.cbouutien = userRequestDetailRepository.DanhSachUuTien();
        }
        [HttpPost]
        public JsonResult AutocompleteMail(string keyword)
        {
            string[] arrListStr = keyword.Split(';');
            string s = arrListStr[arrListStr.Count() - 1];
            List<EmailViewModel> result = Combobox().AutoCompleteMail();
            var data = result.Where(x => x.MAILNAME.StartsWith(s)).Select(x => x.MAILNAME).Distinct().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getDevices(string WorkSiteID)
        {
            List<DeviceObjForDropdown> lst = new List<DeviceObjForDropdown>();
            lst = deviceRepository.GetDeviceByRequest(User.Identity.GetUserName(), WorkSiteID, "-1").ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
         
            //if (lst.Count == 0)
            //{
            //    return Json(deviceRepository.GetDeviceByRequest(User.Identity.GetUserName(), "-1", "-1").ToList(), JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
        }
        [Authorize]
        public JsonResult GetRequestInfomation(int id)
        {
            var lst = userRequestDetailRepository.GetRequestInfomation(id, User.Identity.GetUserName()).Select(x => new
            {
                UserRequestDetailID = x.UserRequestDetailID,
                DeviceID = x.DeviceID,
                DeviceName = x.DeviceName,
                Description = x.Description,
                DateOccurred = x.DateOccurred.HasValue ? x.DateOccurred.Value.ToString("dd/MM/yyyy") : "",
                HourOccurred = x.HourOccurred.HasValue ? x.HourOccurred.Value.ToString("HH:mm") : "",
                Request = x.Request,
                TypeOfMaintenanceRequestID = x.TypeOfMaintenanceRequestID,
                PriorityID = x.PriorityID,
                CauseID = x.CauseID,
                TEN_NGUYEN_NHAN = x.TEN_NGUYEN_NHAN,
                TEN_UU_TIEN = x.TEN_UU_TIEN,
                TEN_LOAI_YEU_CAU_BT = x.TEN_LOAI_YEU_CAU_BT
            }).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public JsonResult GetComponent(int id, int detailID, string deviceID) =>
             Json(userRequestComponentRepository.GetUserRequestComponent(id, deviceID, detailID), JsonRequestBehavior.AllowGet);
        [Authorize]
        public JsonResult GetDocument(int id, int detailID, string deviceID) =>
            Json(userRequestDocumentRepository.GetRequestDocument(id, deviceID, detailID), JsonRequestBehavior.AllowGet);

        [Authorize]
        public ActionResult InitRequest(int id)
        {
            if (id == -1)
            {
                List<GetRequestInfomationObj> lst = new List<GetRequestInfomationObj>();
                ViewBag.ID = -1;
                ViewBag.count = 0;
                ViewBag.requestNum = userRequestRepository.CreateRequestID();
                ViewBag.dateOfRequest = DateTime.Now.ToString("dd/MM/yyyy");
                ViewBag.timeOfRequest = DateTime.Now.ToString("HH:mm tt");
                ViewBag.completedDate = DateTime.Now.ToString("dd/MM/yyyy");
                ViewBag.CreatedBy = userRequestRepository.GetNguoiYC(User.Identity.GetUserName());
                ViewBag.email = "";
                //GetCreatedBy(request.RequestedBy);
                GetCombobox("-1");
                return View(lst);
            }
            else
            {
                UserRequest request = userRequestUnitOfWork.UserRequestRepository.GetUserRequest(id);
                if (request == null) RedirectToAction("Index", "UserRequest");
                ViewBag.ID = request.ID;
                List<GetRequestInfomationObj> lst = new List<GetRequestInfomationObj>();
                try
                {
                    ViewBag.requestNum = request.RequestNO;
                    ViewBag.dateOfRequest = request.DateCreated.ToString("dd/MM/yyyy");
                    ViewBag.timeOfRequest = request.HourCreated.ToString("HH:mm tt");
                    ViewBag.completedDate = request.DateCompleted.HasValue ? request.DateCompleted.Value.ToString("dd/MM/yyyy") : "";
                    ViewBag.email = request.Email;
                    ViewBag.workSiteID = request.WorkSiteID;
                    ViewBag.CreatedBy = request.RequestedBy;
                    GetCreatedBy(request.RequestedBy);
                    GetCombobox(request.WorkSiteID);
                    lst = userRequestUnitOfWork.UserRequestDetailRepository.GetRequestInfomation(id, User.Identity.Name).ToList();
                    ViewBag.count = lst.Count;
                }
                catch
                {
                }
                return View(lst);
            }
        }
        private string sp = "";
        [Authorize]
        public JsonResult SaveRequest(string request, string requestInfo, string diadiem)
        {
            //kiểm
            if (sp == request) return Json("success", JsonRequestBehavior.AllowGet);
            sp = request;
            try
            {
                List<UserRequest> lstRequest = JsonConvert.DeserializeObject<List<UserRequest>>(request);
                //cập nhật user name
                foreach (var item in lstRequest)
                {
                    item.Username = User.Identity.GetUserName();
                    item.CreatedBy = User.Identity.GetUserName();
                }
                List<UserRequestDetail> lstRequestDetails = JsonConvert.DeserializeObject<List<UserRequestDetail>>(requestInfo);
                userRequestUnitOfWork.UserRequestRepository.SaveRequest(lstRequest[0]);
                lstRequestDetails.ForEach(x => x.UserRequestID = lstRequest[0].ID);
                lstRequestDetails.ForEach(x => userRequestUnitOfWork.UserRequestDetailRepository.SaveRequestInfomation(x));
                userRequestUnitOfWork.Save();
                string row = "";
                foreach (var item in userRequestUnitOfWork.UserRequestDetailRepository.GetRequestInfomation(lstRequest[0].ID, User.Identity.Name).ToList())
                {
                    row += string.Format(EcomaintSite.Resulst.Emailtemplete.ROW_YEU_CAU_NSD, item.DeviceID, item.DeviceName, item.Description, item.Request, item.TEN_NGUYEN_NHAN, item.TEN_UU_TIEN, item.TEN_LOAI_YEU_CAU_BT, item.DateOccurred == null ? "" : Convert.ToDateTime(item.DateOccurred).ToString("dd/MM/yyyy"), item.HourOccurred == null ? "" : Convert.ToDateTime(item.HourOccurred).ToString("HH:mm tt"));
                }
                string result = string.Format(EcomaintSite.Resulst.Emailtemplete.YEU_CAU_NSD, lstRequest[0].RequestNO, lstRequest[0].Username, lstRequest[0].RequestedBy, lstRequest[0].DateCreated.ToString("dd/MM/yyyy"), lstRequest[0].HourCreated.ToString("HH:mm tt"), Convert.ToDateTime(lstRequest[0].DateCompleted).ToString("dd/MM/yyyy"), row);
                //láy danh sách
                Combobox().SendEmail(Combobox().GetEmailByNhaXuong(lstRequest[0].ID, lstRequest[0].WorkSiteID, "1", User.Identity.GetUserName(), lstRequest[0].Email), "Yêu cầu bảo trì mới số " + lstRequest[0].RequestNO.ToString(), result, Resulst.Emailtemplete.LINK_DUYET_YEU_CAU);
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("failure: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        private string GetDsMail(string msnx, string mailthem)
        {
            string resulst = "";
            return resulst;

        }

        public JsonResult EditRequest(string request, string requestInfo, string diadiem)
        {
            try
            {
                List<UserRequest> lstRequest = JsonConvert.DeserializeObject<List<UserRequest>>(request);
                List<UserRequestDetail> lstRequestDetails = JsonConvert.DeserializeObject<List<UserRequestDetail>>(requestInfo);
                lstRequestDetails.ForEach(x => x.UserRequestID = lstRequest[0].ID);
                lstRequestDetails.ForEach(x => userRequestUnitOfWork.UserRequestDetailRepository.SaveRequestInfomation(x));
                userRequestUnitOfWork.Save();
                string row = "";
                foreach (var item in userRequestUnitOfWork.UserRequestDetailRepository.GetRequestInfomation(lstRequest[0].ID, User.Identity.Name).ToList())
                {
                    row += string.Format(EcomaintSite.Resulst.Emailtemplete.ROW_YEU_CAU_NSD, item.DeviceID, item.DeviceName, item.Description, item.Request, item.TEN_NGUYEN_NHAN, item.TEN_UU_TIEN, item.TEN_LOAI_YEU_CAU_BT, item.DateOccurred, item.HourOccurred);
                }
                string result = string.Format(EcomaintSite.Resulst.Emailtemplete.YEU_CAU_NSD,
                    lstRequest[0].RequestNO,
                    lstRequest[0].Username,
                    lstRequest[0].RequestedBy,
                    lstRequest[0].DateCreated.ToString("dd/MM/yyyy"),
                    lstRequest[0].HourCreated.ToString("HH:mm tt"),
                    Convert.ToDateTime(lstRequest[0].DateCompleted).ToString("dd/MM/yyyy"),
                    row);
                //láy danh sách
                Combobox().SendEmail(Combobox().GetEmailByNhaXuong(lstRequest[0].ID, lstRequest[0].WorkSiteID, "1", User.Identity.GetUserName(), lstRequest[0].Email), "Sữa yêu cầu bảo trì số " + lstRequest[0].RequestNO.ToString(), result, Resulst.Emailtemplete.LINK_DUYET_YEU_CAU);
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("failure: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        ///them bo phan
        ///
        [HttpGet]
        public ActionResult _ThemPhuThung(int id, string may, int stt_vande)
        {
            PhuTungViewModel model = new PhuTungViewModel();
            model.MS_MAY = may;
            model.STT = id;
            model.STT_VAN_DE = stt_vande;
            model.ListPhuTung = userRequestRepository.DanhSachPhuTungTheoMay(id, may, stt_vande).ToList();
            model.ListBoPhan = userRequestRepository.DanhSachBoPhanTheoMay(id, may).ToList();
            return PartialView(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _ThemPhuThung(PhuTungViewModel model)
        {
            if (userRequestRepository.ThemChiTietBoPhan(model) == 1)
            {
                GetCreatedBy();
                //return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
                return View("~/Views/UserRequest/Index.cshtml");
            }
            else
            {
                return Json("failure", JsonRequestBehavior.AllowGet);
            }
        }
        //them hinh
        [HttpGet]
        public ActionResult _ThemHinh(int id, string may, int stt_vande)
        {
            ViewData["Address"] = new UserRequestDocument()
            {
                UserRequestID = id,
                DeviceID = may,
                ID = stt_vande
            };
            DropzoneOptions model = new DropzoneOptions();
            return PartialView(model);
        }
        private string GetNewPathForDupes(string path, ref string fn)
        {
            string directory = Path.GetDirectoryName(path);
            string filename = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path);
            int counter = 1;
            string newFullPath = path;
            string new_file_name = filename + extension;
            while (System.IO.File.Exists(newFullPath))
            {
                string newFilename = string.Format("{0}({1}){2}", filename, counter, extension);
                new_file_name = newFilename;
                newFullPath = Path.Combine(directory, newFilename);
                counter++;
            };
            fn = new_file_name;
            return newFullPath;
        }

        [HttpPost]
        public ActionResult _ThemHinhPost(HttpPostedFileBase postedFile)
        {
            int stt = Convert.ToInt32(Request.Form["STT"]);
            string msmay = Request.Form["MS_MAY"];
            int sttvande = Convert.ToInt32(Request.Form["STT_VANDE"]);
            string pathnew = "";
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                pathnew = path + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(pathnew);
            }
            if (userRequestRepository.ThemChiTietHinh(stt, msmay, sttvande, pathnew) == 1)
            {
                GetCreatedBy();
                return View("~/Views/UserRequest/Index.cshtml");
            }
            else
            {
                return Json("failure", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Removebp(int id)
        {
            if (userRequestRepository.XoaChiTietBoPhan(id) == 1)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("failure", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Removehinh(int id)
        {
            string fileName = userRequestRepository.GetPath(id);
            if (fileName != null || fileName != string.Empty)
            {
                if ((System.IO.File.Exists(fileName)))
                {
                    System.IO.File.Delete(fileName);
                }

            }
            if (userRequestRepository.XoaChiTietHinh(id) == 1)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("failure", JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult Removeyeucau(int id)
        {
            if (userRequestRepository.XoaYeuCau(id) == 1)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("failure", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult CapNhatHinhDaiDien(string maNV, HttpPostedFileBase file)
        {
            // try
            //{
            //    ResponseResult result = TaiKhoanNhanVienService.CapNhatHinhDaiDien(maNV, file);
            //    if (result != null && result.ResponseCode == 1)
            //    {
            //        var NvSession = SettingDataService.GetNhanVienSessionInfo();
            //        if (maNV == NvSession.MaNVMoi)
            //        {
            //            SettingDataService.UpdateNhanVienSessionInfo(NvSession.NhanvienID, NvSession.MaNV, maNV, NvSession.HoNV, NvSession.TenLotNV, NvSession.TenNV, NvSession.HoVaTen, result.ResponseMessage);
            //        }
            //        return Json(JsonResponseViewModel.CreateSuccess());
            //    }
            //    return Json(JsonResponseViewModel.CreateFail(NotifyMessage.THONGBAO_KHONGCAPNHAT_HINHDAIDIEN));
            //}
            //catch (Exception ex)
            //{
            return Json(JsonRequestBehavior.AllowGet);
            //}
        }

        [Authorize]
        public JsonResult ShowNguoiYeuCau()
        {
            try
            {
                return Json(userRequestRepository.GetNguoiYeuCau(User.Identity.GetUserName()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}