using Microsoft.AspNet.Identity;
using Model.Combobox;
using Model.Data;
using Model.Interface;
using Model.Interface.IRepository;
using Model.Repository.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace EcomaintSite.Controllers
{
    public class SafetyController : Controller
    {

        private ICombobox _Combobox;
        private ISafetyRepository _SafetyRepository;
        public static byte[] Image_1;
        private ICombobox Combobox()
        {
            return _Combobox ?? (_Combobox = new Combobox());
        }

        private ISafetyRepository Safety()
        {
            return _SafetyRepository ?? (_SafetyRepository = new SafetyRepository());
        }

        #region Hazard report
        // GET: Safety
        [Authorize]
        public ActionResult ShowHazardReport()
        {
            ViewBag.STT = "-1";
            ViewBag.UserName = User.Identity.GetUserName();

            ViewBag.ListReportParent = Combobox().GetListReportParent(User.Identity.GetUserName());
            ViewBag.ListLocation = Combobox().GetListLocation();
            ViewBag.ListDepartment = Combobox().GetListDepartment();
            ViewBag.ListPersonRef = Combobox().GetListPersonRef(User.Identity.GetUserName());

            ViewBag.LoadListInCharge = Combobox().LoadListInCharge();
            ViewBag.LoadListThietBi = Combobox().LoadListThietBi();
            ViewBag.LoadListNguyenNhan = Combobox().LoadListNguyenNhan();
            ViewBag.LoadListLoaiYC = Combobox().LoadListLoaiYC();
            ViewBag.LoadListUuTien = Combobox().LoadListUuTien(SessionVariable.TypeLanguage);

            ViewBag.IDStafety = Safety().GetIDSafery(User.Identity.GetUserName());

            ViewBag.TTPhieu = 0;
            ViewBag.UserDuyet = CheckApprovalUser("FrmHazardReport");


            HazardReportViewModel model = new HazardReportViewModel();
            model.DocNum = Safety().GetSoPhieuHazard();
            model.ID = -1;
            model.Image_1 = null;
            return View("~/Views/Safety/ShowHazardReport.cshtml", model);
        }
        private byte[] imgToByteConverter(byte[] imgConvert)
        {
            byte[] currentByteImageArray = imgConvert;
            double scale = 1f;
            MemoryStream inputMemoryStream = new MemoryStream(imgConvert);
            Image fullsizeImage = Image.FromStream(inputMemoryStream);
            while (currentByteImageArray.Length > 60000)
            {
                Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size((int)(fullsizeImage.Width * scale), (int)(fullsizeImage.Height * scale)));
                MemoryStream resultStream = new MemoryStream();
                fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);
                currentByteImageArray = resultStream.ToArray();
                resultStream.Dispose();
                resultStream.Close();
                scale -= 0.05f;
            }

            return currentByteImageArray;
        }
        [HttpPost]
        public JsonResult SaveImage()
        {
            System.Web.HttpRequest request = System.Web.HttpContext.Current.Request;
            try
            {
                var webImage = new System.Web.Helpers.WebImage(Request.Files["image"].InputStream);
                byte[] bytes = webImage.GetBytes();
                Image_1 = imgToByteConverter(bytes);
                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult EditHazardReport(string ID)
        {
            ViewBag.UserName = User.Identity.GetUserName();
            ViewBag.ListReportParent = Combobox().GetListReportParent(User.Identity.GetUserName());
            ViewBag.ListLocation = Combobox().GetListLocation();
            ViewBag.ListDepartment = Combobox().GetListDepartment();
            ViewBag.ListPersonRef = Combobox().GetListPersonRef(User.Identity.GetUserName());


            ViewBag.LoadListInCharge = Combobox().LoadListInCharge();
            ViewBag.LoadListThietBi = Combobox().LoadListThietBi();
            ViewBag.LoadListNguyenNhan = Combobox().LoadListNguyenNhan();
            ViewBag.LoadListLoaiYC = Combobox().LoadListLoaiYC();
            ViewBag.LoadListUuTien = Combobox().LoadListUuTien(SessionVariable.TypeLanguage);

            ViewBag.IDStafety = Safety().GetIDSafery(User.Identity.GetUserName());

            HazardReportViewModel model = Safety().GetListByID(ID);

            model.sDocDate = model.DocDate.HasValue ? model.DocDate.Value.ToString("dd/MM/yyyy") : "";
            model.sCreatedtime = model.Createdtime.HasValue ? model.Createdtime.Value.ToString("dd/MM/yyyy") : "";

            model.ListHarzard1 = Safety().GetListDetail(ID);

            ViewBag.TTPhieu = model.ListHarzard1.Count(x => x.Status.Trim() == "Done-Đã hoàn thành");
            ViewBag.UserDuyet = CheckApprovalUser("FrmHazardReport");

            if (model.UnsafeCondition == true) model.CauSafery = 1;
            if (model.UnsafeBehevior == true) model.CauSafery = 2;
            if (model.SatetySuggestion == true) model.CauSafery = 3;
            if (model.NearMiss == true) model.CauSafery = 4;
            if (model.Environment == true) model.CauSafery = 5;
            if (model.TT == true) model.CauSafery = 6;
            if (model.Other == true) model.CauSafery = 7;

            //CauSafery
            //1 UnsafeCondition
            //2 UnsafeBehevior
            //3 SatetySuggestion
            //4 NearMiss
            //5 Environment
            //6 TT
            //7 Other
            if (model.Operation == true) model.Relateddepartments = 2;
            if (model.TechHSE == true) model.Relateddepartments = 3;
            if (model.HR == true) model.Relateddepartments = 5;
            if (model.Acount == true) model.Relateddepartments = 6;
            //Relateddepartments
            //2  Operation
            //3 TechHSE
            //5  HR
            //6 Account

            if (model.BelongContractor == true) model.Belongto = 1;
            if (model.BelongToBSPort == true) model.Belongto = 2;

            //Belongto
            //1 BelongContractor
            //2 BelongtoBSPort
            if (model.HPES1 == true) model.HPES = 1;
            if (model.HPES2 == true) model.HPES = 2;
            if (model.HPES3 == true) model.HPES = 3;
            if (model.HPES4 == true) model.HPES = 4;
            if (model.HPES5 == true) model.HPES = 5;
            if (model.HPES6 == true) model.HPES = 6;


            return View("~/Views/Safety/ShowHazardReport.cshtml", model);
        }

        [Authorize]
        [HttpPost]
        public JsonResult GetListHazardReport(string tungay, string denngay)
        {
            try
            {
                IEnumerable<ChooseListHazard> list = Safety().ChooseListHazard(Convert.ToDateTime(tungay, new CultureInfo("vi-vn")).ToString("yyyy/MM/dd"), Convert.ToDateTime(denngay, new CultureInfo("vi-vn")).ToString("yyyy/MM/dd"));
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult SaveHazardReport(HazardReportViewModel model, string hazardetails)
        {
            int loaimail = 0;
            string mailget = "";
            string  mailBC = "";
            try
            {
                string Report = "";
                if (model.CauSafery == 1)
                {
                    model.UnsafeCondition = true;
                    Report = SessionVariable.TypeLanguage == 0 ? "Điều kiện KAT" : "Unsafe Condition";
                }
                if (model.CauSafery == 2)
                {
                    model.UnsafeBehevior = true;
                    Report = SessionVariable.TypeLanguage == 0 ? "Hành vi không an toàn" : "Unsafe Action";

                }
                if (model.CauSafery == 3)
                {
                    model.SatetySuggestion = true;
                    Report = SessionVariable.TypeLanguage == 0 ? "Đề xuất an toàn" : "Safety Suggestion";
                }
                if (model.CauSafery == 4)
                {
                    model.NearMiss = true;
                    Report = SessionVariable.TypeLanguage == 0 ? "Suýt xảy ra sự cố (loại 1)" : "Near Miss (Type 1)";
                }
                if (model.CauSafery == 5)
                {
                    model.Environment = true;
                    Report = SessionVariable.TypeLanguage == 0 ? "Sự cố môi trường (nặng)" : "Environment";
                }
                if (model.CauSafery == 6)
                {
                    model.TT = true;
                    Report = SessionVariable.TypeLanguage == 0 ? "Tình trạng xuống cấp" : "Sub-condition";
                }
                if (model.CauSafery == 7)
                {
                    model.Other = true;
                    Report = SessionVariable.TypeLanguage == 0 ? "Khác" : "Other";
                }

                //CauSafery
                //1 UnsafeCondition
                //2 UnsafeBehevior
                //3 SatetySuggestion
                //4 NearMiss
                //5 Environment
                //6 TT
                //7 Other

                if (model.Relateddepartments == 2) model.Operation = true;
                if (model.Relateddepartments == 3) model.TechHSE = true;
                if (model.Relateddepartments == 5) model.HR = true;
                if (model.Relateddepartments == 6) model.Acount = true;

                //Relateddepartments
                //2  Operation
                //3 TechHSE
                //5  HR
                //6 Account
                if (model.Belongto == 1) model.BelongContractor = true;
                if (model.Belongto == 2) model.BelongToBSPort = true;


                //Belongto
                //1 BelongContractor
                //2 BelongtoBSPort

                if (model.HPES == 1) model.HPES1 = true;
                if (model.HPES == 2) model.HPES2 = true;
                if (model.HPES == 3) model.HPES3 = true;
                if (model.HPES == 4) model.HPES4 = true;
                if (model.HPES == 5) model.HPES5 = true;
                if (model.HPES == 6) model.HPES6 = true;

                try
                {
                    model.Image_1 = Image_1;
                }
                catch
                {
                    model.Image_1 = null;
                }

                if (model.ID == -1)
                {
                    if (Safety().fnCheckAdminUser(model.CreatedBy) == true || Safety().fnCheckApprovalUser(model.CreatedBy, "FrmHazardReport") == true)
                    {
                        model.APPROVAL_USER = model.CreatedBy;
                        model.IS_APPROVED = true;
                        loaimail = 1;
                    }
                    else
                    {
                        model.APPROVAL_USER = "";
                        model.IS_APPROVED = false;
                        loaimail = 0;
                    }
                }
                if (model.IS_APPROVED == true)
                {
                    loaimail = 1;
                }
                model.ListHarzard1 = JsonConvert.DeserializeObject<List<HazardReportViewModel.HazardReport1ViewModel>>(hazardetails);
                bool resulst = false;
                if (model.ID == -1)
                {
                    int a = Safety().AddInsertHazardrepot(model);
                    if (a == -1)
                    {
                        resulst = false;
                    }
                    else
                    {
                        model.ID = a;
                        resulst = true;
                    }
                }
                else
                {
                    resulst = Safety().EditHazardrepot(model);
                }

                if (loaimail == 0)
                {
                    //gửi tới mail duyệt
                    mailget = Safety().fnGetListMailApproval(model.ID.ToString(), model.REPORT_PARENT, User.Identity.GetUserName());
                }
                else
                {
                    mailget = Safety().fnGetListMailIncharge(model.ID.ToString());
                    mailBC = Safety().fnGetListMailApprovalAndCreatedBy(model.ID.ToString());
                }
                if (resulst == true)
                    return Json(new { Message = "success", ID = model.ID, mailget = mailget, loaimail = loaimail, Report = Report,mailBC = mailBC }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { Message = "error", ID = model.ID }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = "error" + ex.InnerException.Message, ID = model.ID }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult DeleteHazardReport(int ID)
        {
            try
            {
                if (Safety().Delete(ID) == true)
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("error", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("error: " + ex.InnerException.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult GetDepartmentbyReportParent(string UserName)
        {
            try
            {
                string Depament = Safety().GetDepartmentbyReportParent(UserName);
                return Json(new { Message = "success", Depament = Depament }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = "error" + ex.InnerException.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public string CheckApprovalUser(string form)
        {
            if (Safety().fnCheckAdminUser(User.Identity.GetUserName()) == false
                && Safety().fnCheckApprovalUser(User.Identity.GetUserName(),form) == false)
            {
                return "error";
            }
            else
            {
                return "success";
            }
        }
        [HttpPost]
        public JsonResult SentMail(string ID, int sentmailType,  string CC, string Mailto, string Report)
        {

            ST_HazardReport list = Safety().HazardReport(Convert.ToInt32(ID));

            string txtDocNum = list.DocNum;
            string ReportParent = list.REPORT_PARENT.ToString();
            string Description = list.Description;
            string CreatedBy = list.CreatedBy;
            string Approval = list.APPROVAL_USER;
            string subject = "";
            string subjectEng = "";
            string body = "";
            string bodyEng = "";
            string UserName = User.Identity.GetUserName();
            switch (sentmailType)
            {
                case 0 // Gui toi nguoi duyet'
               :
                    {
                        subject = "Duyệt Báo cáo từ Ecomaint/Safety";
                        subjectEng = "Please review reports from Ecomaint/Safety";
                        string dearto = Safety().fnGET_DEAR_APPROVAL(ID, UserName, ReportParent);
                        body = "Dear <b>" + dearto + "</b> <br>" + "+ Cần duyệt báo cáo về: <b>" + Report + "</b> <br>" + "+ Người báo cáo: <b>" + CreatedBy + "</b><br>" + "+ Ngày báo cáo: <b>" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "</b><br>" + "+ Số chứng từ: <b>" + txtDocNum + "</b><br>" + "+ Nội dung/nguyên nhân: <b>" + Description + "</b><br>" + "Vui lòng đăng nhập vào Phần mềm Ecomaint/Safety để duyệt Báo cáo.<br>Thanks & Best regards.<br><br><br>";
                        bodyEng = "Dear <b>" + dearto + "</b> <br>" + "+ Need to review reports of: <b>" + Report + "</b> <br>" + "+ Reporter: <b>" + CreatedBy + "</b><br>" + "+ Report Date: <b>" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "</b><br>" + "+ Doc. No.: <b>" + txtDocNum + "</b><br>" + "+ Description/ Cause: <b>" + Description + "</b><br>" + "Please log in Ecomaint/Safety Software to review reports.<br>Thanks & Best regards.<br><br><br>";
                        break;
                    }

                case 1 // Gui toi nguoi thuc hien'
         :
                    {

                        subject = "Thực hiện Hành động khắc phục, phòng ngừa từ Ecomaint/Safety";
                        subjectEng = "Please complete preventive and corrective actions from Ecomaint/ Safety";
                        string dearto = Safety().fnGET_DEAR_INCHARGE(ID,UserName,ReportParent);

                        string nduyet = Safety().fnGetApproval(ID,Approval,ReportParent);
                        body = "Dear <b>" + dearto + "</b> <br>" + "+ Báo cáo: <b>" + Report + "</b> <br>" + "+ Người báo cáo: <b>" + CreatedBy + "</b><br>" + "+ Ngày báo cáo: <b>" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "</b><br>" + "+ Người duyệt: <b>" + (nduyet == "" ? Approval : nduyet) + "</b><br>" + "+ Số chứng từ: <b>" + txtDocNum + "</b><br>" + "+ Nội dung/nguyên nhân: <b>" + Description + "</b><br>" + "+ Hành động cần thực hiện: <br>";

                        bodyEng = "Dear <b>" + dearto + "</b> <br>" + "+ Report: <b>" + Report + "</b> <br>" + "+ Reporter: <b>" + CreatedBy + "</b><br>" + "+ Report Date: <b>" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "</b><br>" + "+ Reviewed by: <b>" + (nduyet == "" ? Approval : nduyet) + "</b><br>" + "+ Doc. No.: <b>" + txtDocNum + "</b><br>" + "+ Description/ Cause: <b>" + Description + "</b><br>" + "+ Required Actions: <br>";
                        DataTable dt = Safety().fnGetListAction(ID);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                body = body + "&nbsp;&nbsp;&nbsp;" + (i + 1).ToString() + ".&nbsp;&nbsp;<b>" + dt.Rows[i][0].ToString() + "</b><br>";
                                bodyEng = bodyEng + "&nbsp;&nbsp;&nbsp;" + (i + 1).ToString() + ".&nbsp;&nbsp;<b>" + dt.Rows[i][0].ToString() + "</b><br>";
                            }
                        }
                        body = body + "Vui lòng đăng nhập vào Phần mềm Ecomaint/Safety để thực hiện các hành động khắc phục, phòng ngừa được đề xuất theo đúng thời hạn.<br>Thanks & Best regards.<br><br><br>";
                        bodyEng = bodyEng + "Please log in Ecomaint/Safety Software to complete required actions.<br>Thanks & Best regards.<br><br><br>";
                        break;
                    }

                case 2 // Gui toi nguoi tao va nguoi duyet'
         :
                    {
                        subject = "Hoàn tất hành động khắc phục, phòng ngừa từ Ecomaint/Safety";
                        subjectEng = "Completed preventive and corrective actions from Ecomaint/Safety";
                        string dearto = Safety().GET_DEAR_APPROVAL_CREATE_BY(ID,UserName,ReportParent);
                        string nduyet = Safety().fnGetApproval(ID,Approval,ReportParent);
                        string nth = Safety().fnGetApproval(ID, UserName, ReportParent);
                        body = "Dear <b>" + dearto + "</b> <br>" + "+ Báo cáo: <b>Kết quả thực hiện hành động khắc phục, phòng ngừa từ Ecomaint/Safety</b><br>" + "+ Người báo cáo: <b>" + CreatedBy + "</b><br>" + "+ Ngày báo cáo: <b>" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "</b><br>" + "+ Người duyệt: <b>" + (nduyet == "" ? Approval : nduyet) + "</b><br>" + "+ Người thực hiện: <b>" + (nth == "" ? UserName : nth) + "</b><br>" + "+ Số chứng từ: <b>" + txtDocNum + "</b><br>" + "+ Nội dung/nguyên nhân: <b>" + Description + "</b><br>" + "+ Hành động đã hoàn tất:  <br>";
                        bodyEng = "Dear <b>" + dearto + "</b> <br>" + "+ Report: <b>Result of preventive and corrective actions</b><br>" + "+ Reporter: <b>" + Safety().fnGetReporter(ID,CreatedBy,ReportParent) + "</b><br>" + "+ Report Date: <b>" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "</b><br>" + "+ Reviewed by: <b>" + (nduyet == "" ? Approval : nduyet) + "</b><br>" + "+ Person in charge: <b>" + (nth == "" ? UserName : nth) + "</b><br>" + "+ Doc. No.: <b>" + txtDocNum + "</b><br>" + "+ Description/ Cause: <b>" + Description + "</b><br>" + "+ Action done:  <br>";
                        DataTable dt = Safety().fnGetListActionDone(ID,"");
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                body = body + "&nbsp;&nbsp;&nbsp;<b>" + (i + 1).ToString() + ".&nbsp;&nbsp;" + dt.Rows[i][0].ToString() + "</b><br>";
                                bodyEng = bodyEng + "&nbsp;&nbsp;&nbsp;<b>" + (i + 1).ToString() + ".&nbsp;&nbsp;" + dt.Rows[i][0].ToString() + "</b><br>";
                            }
                        }
                        body = body + "Vui lòng đăng nhập vào Phần mềm Ecomaint/Safety để xác nhận thông tin báo cáo.<br>Thanks & Best regards.<br><br><br>";
                        bodyEng = bodyEng + "Please log in Ecomaint/Safety Software to confirm information.<br>Thanks & Best regards.<br><br><br>";
                        break;
                    }
            }
            if (SessionVariable.TypeLanguage == 0)
            {
                Combobox().SendEmailCC(/*Mailto*/"bamboo2711@gmail.com", CC, subject, body);
            }
            else
            {
                Combobox().SendEmailCC("bamboo2711@gmail.com", CC, subjectEng, bodyEng);
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Stop card
        [Authorize]
        public ActionResult ShowStopCard()
        {
            string UserName = User.Identity.GetUserName();
            ViewBag.STT = "-1";
            ViewBag.UserName = UserName;
            
            ViewBag.ListReportParent = Combobox().GetListReportParent(UserName);
            ViewBag.ListLocation = Combobox().GetListLocation();
            ViewBag.ListDepartment = Combobox().GetListDepartment();
            ViewBag.ListPersonRef = Combobox().GetListPersonRef(UserName);

            ViewBag.LoadListInCharge = Combobox().LoadListInCharge();
            ViewBag.LoadListThietBi = Combobox().LoadListThietBi();
            ViewBag.LoadListNguyenNhan = Combobox().LoadListNguyenNhan();
            ViewBag.LoadListLoaiYC = Combobox().LoadListLoaiYC();
            ViewBag.LoadListUuTien = Combobox().LoadListUuTien(SessionVariable.TypeLanguage);

            ViewBag.IDStafety = Safety().GetIDSafery(UserName);
            ViewBag.TTPhieu = 0;
            ViewBag.UserDuyet = CheckApprovalUser("FrmStopCard");

            StopCardViewModel model = new StopCardViewModel();
            model.DocNum = Safety().GetSoPhieuStopCart();
            model.ID = -1;
            model.Image_1 = null;
            return View("~/Views/Safety/ShowStopCard.cshtml",model);

        }


        [Authorize]
        [HttpPost]
        public JsonResult GetListStopCard(string tungay, string denngay)
        {
            try
            {
                IEnumerable<ChooseListStopCard> list = Safety().ChooseListStopCard(Convert.ToDateTime(tungay, new CultureInfo("vi-vn")).ToString("yyyy/MM/dd"), Convert.ToDateTime(denngay, new CultureInfo("vi-vn")).ToString("yyyy/MM/dd"));
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditStopCard(string ID)
        {
            ViewBag.UserName = User.Identity.GetUserName();
            ViewBag.ListReportParent = Combobox().GetListReportParent(User.Identity.GetUserName());
            ViewBag.ListPersonRef = Combobox().GetListPersonRef(User.Identity.GetUserName());


            ViewBag.LoadListInCharge = Combobox().LoadListInCharge();
            ViewBag.LoadListThietBi = Combobox().LoadListThietBi();
            ViewBag.LoadListNguyenNhan = Combobox().LoadListNguyenNhan();
            ViewBag.LoadListLoaiYC = Combobox().LoadListLoaiYC();
            ViewBag.LoadListUuTien = Combobox().LoadListUuTien(SessionVariable.TypeLanguage);

            ViewBag.IDStafety = Safety().GetIDSafery(User.Identity.GetUserName());

            StopCardViewModel model = Safety().GetListStopCardByID(ID);

            model.sDocDate = model.DocDate.HasValue ? model.DocDate.Value.ToString("dd/MM/yyyy") : "";

            model.StopCard1 = Safety().GetListStopCardDetail1(ID);
            model.StopCard2 = Safety().GetListStopCardDetail2(ID);

            ViewBag.TTPhieu = model.StopCard1.Count(x => x.Status.Trim() == "Done-Đã hoàn thành") + model.StopCard2.Count(x => x.Status.Trim() == "Done-Đã hoàn thành");
            ViewBag.UserDuyet = CheckApprovalUser("FrmStopCard");

           

            //Belongto
            //1 BelongContractor
            //2 BelongtoBSPort
            if (model.HPES1 == true) model.HPES = 1;
            if (model.HPES2 == true) model.HPES = 2;
            if (model.HPES3 == true) model.HPES = 3;
            if (model.HPES4 == true) model.HPES = 4;
            if (model.HPES5 == true) model.HPES = 5;
            if (model.HPES6 == true) model.HPES = 6;


            return View("~/Views/Safety/ShowStopCard.cshtml", model);
        }


        #endregion

    }
}