using Microsoft.AspNet.Identity;
using Model.Combobox;
using Model.Data;
using Model.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace EcomaintSite.Controllers
{
    public class ApproveRequestsController : Controller
    {
        IUserRequestDetailRepository request;
        IPriorityRepository priority;

        private ICombobox _Combobox;
        private ICombobox Combobox()
        {
            return _Combobox ?? (_Combobox = new Combobox());
        }
        public ApproveRequestsController(IUserRequestDetailRepository _request, IPriorityRepository _priority)
        {
            request = _request;
            priority = _priority;
        }

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Username = User.Identity.Name;
            ViewBag.cboPriority = new SelectList(priority.ListAll(), "ID", "Name", "-1");
            return View();
        }
        public JsonResult Approve(string data,string data1)
        {
            List<UserRequestDetail> lstRequest = JsonConvert.DeserializeObject<List<UserRequestDetail>>(data);
            try
            {
                request.ApproveRequests(lstRequest);
                request.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json("failure: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
            // sử lý lọc ra các yêu cầu
            var x = lstRequest.Select(a => a.UserRequestID).Distinct().ToArray();
            // for dùng duyệt qua các yêu cầu rồi gửi mail cho từng cái mỗi yêu cầu có 1 danh sách máy gửi kèm
            foreach (var ite in x)
            {
                //lấy tất cả các dòng có UserRequestID = id thì gửi mail
                List<UserRequestDetail> lstRequest1 = lstRequest.Where(a => a.UserRequestID.Equals(ite)).ToList();
                string row = "";
                foreach (var model in lstRequest1)
                {
                    //item.ID;
                    var item = request.GetRequestInfomationbySTTVD(model.ID, User.Identity.GetUserName()).FirstOrDefault();
                    row += string.Format(EcomaintSite.Resulst.Emailtemplete.ROW_YEU_CAU_NSD, item.DeviceID, item.DeviceName, item.Description, item.Request, item.TEN_NGUYEN_NHAN, item.TEN_UU_TIEN, item.TEN_LOAI_YEU_CAU_BT, item.DateOccurred == null ? "" : Convert.ToDateTime(item.DateOccurred).ToString("dd/MM/yyyy"), item.HourOccurred == null ? "" : Convert.ToDateTime(item.HourOccurred).ToString("HH:mm tt"));
                }
                string result = string.Format(EcomaintSite.Resulst.Emailtemplete.DUYET_YCNSD, request.SoYeuCau(ite), lstRequest[0].ApprovedRequestBy, request.GetFullNAmeByUserName(lstRequest[0].ApprovedRequestBy), Convert.ToDateTime(lstRequest[0].DateApproveRequested).ToString("dd/MM/yyyy"), DateTime.Now.ToString("HH:mm tt"), lstRequest[0].ApprovalNote, row);
                Combobox().SendEmail(Combobox().GetEmailByNhaXuong(lstRequest[0].UserRequestID, "", "2", User.Identity.GetUserName(), lstRequest[0].EmailByApproveRequested), "Duyệt yêu cầu bảo trì " + request.SoYeuCau(lstRequest[0].UserRequestID), result, Resulst.Emailtemplete.LINK_DUYET_YEU_CAU);
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult FilterData(string fromDate, string toDate, int option)
        {
            var model = request.ApprovedRequest(User.Identity.GetUserName(), option, SessionVariable.TypeLanguage, null, Convert.ToDateTime(fromDate, new CultureInfo("vi-vn")), Convert.ToDateTime(toDate, new CultureInfo("vi-vn")), "-1", "-1").Select(x => new
            {
                Choose = x.Choose,
                UserRequestDetailID = x.UserRequestDetailID,
                ID = x.ID,
                DeviceID = x.DeviceID,
                DateCreated = x.DateCreated.ToString("dd/MM/yyyy"),
                Description = x.Description,
                Request = x.Request,
                RequestNO = x.RequestNO,
                RequestedBy = x.RequestedBy,
                TypeOfMaintenanceName = x.TypeOfMaintenanceName,
                Document = x.Document,
                PriorityName = x.PriorityName,
                IsApprovedRequest = x.IsApprovedRequest
            });
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}