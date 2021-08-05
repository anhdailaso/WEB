
using Microsoft.AspNet.Identity;
using Model.Combobox;
using Model.Data;
using Model.Interface;
using Model.Repository;
using Model.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace EcomaintSite.Controllers
{
    public class MonitoringController : Controller
    {
        IMonitoringUnitOfWork monitoringUnitOfWork;
        IDeviceRepository deviceRepository;
        IMonitoringRepository monitoringRepository;
        IUserRepository userRepository;
        IGeneralRepository generalRepository;
        IThoiGianChayMayRepository thoigianchaymayRepository;
        private ICombobox _Combobox;
        public static List<MonitoringParametersByDevice> listthongso = null;
        public static List<MonitoringByDevice> listgiamsat = null;
        string sKeyV = @"http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q=AAAXXX&tl=vi";

        private ICombobox Combobox()
        {
            return _Combobox ?? (_Combobox = new Combobox());
        }
        public MonitoringController(IMonitoringUnitOfWork _monitoringUnitOfWork, IDeviceRepository _deviceRepository, IMonitoringRepository _monitoringRepository, IUserRepository _userRepository, IGeneralRepository _generalRepository, IThoiGianChayMayRepository _thoigianchaymayRepository)
        {
            deviceRepository = _deviceRepository;
            monitoringRepository = _monitoringRepository;
            monitoringUnitOfWork = _monitoringUnitOfWork;
            userRepository = _userRepository;
            generalRepository = _generalRepository;
            thoigianchaymayRepository = _thoigianchaymayRepository;

        }
        //
        // GET: Monitoring
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.link = 0;
            ViewBag.NhaXuong = Combobox().GetCbbDiaDiem(User.Identity.GetUserName(), SessionVariable.TypeLanguage, 1);
            ViewBag.LoaiCV = Combobox().GetCbbLoaiCV(User.Identity.GetUserName(), SessionVariable.TypeLanguage, 1);
            ViewBag.NhanVien = Combobox().NhanVienKT(User.Identity.GetUserName());
            return View(deviceRepository.ListAll().Select(x => new DeviceObjForDropdown
            {
                ID = x.ID,
                Name = x.Name
            }).ToList());
        }

        // GET: Monitoring
        [Authorize]
        public ActionResult ShowMornitoringDevice()
        {
            ViewBag.link = 0;
            ViewBag.NhaXuong = Combobox().GetCbbDiaDiem(User.Identity.GetUserName(), SessionVariable.TypeLanguage, 1);
            return View("~/Views/Monitoring/_GiamSatTinhTrangMay.cshtml");
        }

        [Authorize]
        public ActionResult Show(string msnx, string msmay, string tngay, string dngay)
        {
            ViewBag.link = 1;
            ViewBag.nx = msnx;
            ViewBag.may = msmay;
            ViewBag.tngay = Convert.ToDateTime(tngay, new CultureInfo("vi-vn")).ToString("dd/MM/yyyy");
            ViewBag.dngay = Convert.ToDateTime(dngay, new CultureInfo("vi-vn")).ToString("dd/MM/yyyy");
            ViewBag.STT = "-1";
            ViewBag.NhaXuong = Combobox().GetCbbDiaDiem(User.Identity.GetUserName(), SessionVariable.TypeLanguage, 1);
            ViewBag.LoaiCV = Combobox().GetCbbLoaiCV(User.Identity.GetUserName(), SessionVariable.TypeLanguage, 1);
            ViewBag.NhanVien = Combobox().NhanVienKT(User.Identity.GetUserName());
            return View("~/Views/Monitoring/_ThemGiamSatTinhTrang.cshtml", deviceRepository.ListAll().Select(x => new DeviceObjForDropdown
            {
                ID = x.ID,
                Name = x.Name
            }).ToList());
        }

        public ActionResult InitMonitoring(string stt, string msmay, string msphieu)
        {
            if (!string.IsNullOrEmpty(stt))
            {
                //sửa
                List<string> lstmay = msmay.Split(',').ToList();
                List<SelectListItem> resulst = Combobox().GetCbbMay(User.Identity.Name, 0, 0).ToList();
                var kq = resulst.Where(p => lstmay.Where(y => y.Trim().Equals(p.Value)).Any()).ToList();
                ViewBag.May = new SelectList(kq, "Value", "Text", null);
                ViewBag.MSPhieu = msphieu;
            }
            else
            {
                //thêm
                stt = "-1";
                ViewBag.May = null;
                //ViewBag.msphieu = monitoringRepository.CreateSoPhieu(Convert.ToDateTime(denngay, new System.Globalization.CultureInfo("vi-vn")));
            }
            ViewBag.STT = stt;
            ViewBag.NhaXuong = Combobox().GetCbbDiaDiem(User.Identity.GetUserName(), SessionVariable.TypeLanguage, 1);
            ViewBag.LoaiCV = Combobox().GetCbbLoaiCV(User.Identity.GetUserName(), SessionVariable.TypeLanguage, 1);
            ViewBag.NhanVien = Combobox().NhanVienKT(User.Identity.Name);
            return View("~/Views/Monitoring/_ThemGiamSatTinhTrang.cshtml", deviceRepository.ListAll().Select(x => new DeviceObjForDropdown
            {
                ID = x.ID,
                Name = x.Name
            }).ToList());
        }
        public ActionResult InitMonitoringEdit(int stt, string msmay)
        {
            ViewBag.NhaXuong = Combobox().GetCbbDiaDiem(User.Identity.GetUserName(), SessionVariable.TypeLanguage, 1);
            ViewBag.LoaiCV = Combobox().GetCbbLoaiCV(User.Identity.GetUserName(), SessionVariable.TypeLanguage, 1);
            ViewBag.NhanVien = Combobox().NhanVienKT(User.Identity.Name);
            return View("~/Views/Monitoring/_ThemGiamSatTinhTrang.cshtml", deviceRepository.ListAll().Select(x => new DeviceObjForDropdown
            {
                ID = x.ID,
                Name = x.Name
            }).ToList());
        }

        [Authorize]
        public JsonResult FilterData(string fromDate, string toDate, string msmay, string mscn)
        {
            try
            {
                var model = monitoringRepository.GetGiamSatTinhTrang(Convert.ToDateTime(fromDate, new System.Globalization.CultureInfo("vi-vn")), Convert.ToDateTime(toDate, new System.Globalization.CultureInfo("vi-vn")), mscn, msmay).Select(x => new
                {
                    STT = x.STT,
                    SO_PHIEU = x.SO_PHIEU,
                    NGAY_KT = x.NGAY_KT.ToString("dd/MM/yyyy"),
                    GIO_KT = x.GIO_KT.ToString("HH:mm"),
                    HO_TEN = x.HO_TEN,
                    MS_CONG_NHAN = x.MS_CONG_NHAN,
                    MS_MAY = x.MS_MAY,
                    TEN_MAY = x.TEN_MAY,
                    NHAN_XET = x.NHAN_XET
                });
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        //IEnumerable<MoningtoringViewModel.ThongSoGiamSat> GetThongSoGSTT(int stt, int dat, string msmay, int loaits);
        [Authorize]
        public JsonResult GetThongSo(int stt, int dat, string msmay, int loaits)
        {
            try
            {
                var model = monitoringRepository.GetThongSoGSTT(stt, dat, msmay, loaits).ToList();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        public JsonResult GetGiaTri(int stt, string msmay, string msbp, string msts, int loai)
        {
            try
            {
                var model = monitoringRepository.GetGiaTriDT(stt, msmay, msbp, msts, loai).ToList();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        public JsonResult GetGiaTriDL(int stt, string msmay, string msbp, string msts)
        {
            try
            {
                var model = monitoringRepository.GetGiaTriDL(stt, msmay, msbp, msts, User.Identity.GetUserName(), SessionVariable.TypeLanguage).ToList();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }


        [Authorize]
        public JsonResult CheckTheParametersDue(string msnx)
        {
            try
            {
                List<DeviceObjForDropdown> lst = new List<DeviceObjForDropdown>();
                return Json(deviceRepository.GetDeviceByRequest(User.Identity.GetUserName(), msnx, "-1").ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public JsonResult GetRuntimeInYear(string msmay)
        {
            try
            {
                var list = thoigianchaymayRepository.GetThoiGianChayMayInfo(msmay).Select(x=> new { NGAY = x.NGAY.ToString("dd/MM/yyyy"),x.CHI_SO_DONG_HO,x.SO_GIO_LUY_KE }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult RetrieveImage(string moningtoringID, string compunetID)
        {
            byte[] cover = (byte[])listthongso.Where(x => x.MonitoringParamsID == moningtoringID && x.ComponentID == compunetID && x.ImageGS != null).Select(x => x.ImageGS).FirstOrDefault();
            return cover != null
                ? File(cover, "image/png")
                : null;
        }
        [HttpPost]
        public JsonResult SaveImage()
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            string mor = request["mor"].ToString();
            string com = request["com"].ToString();
            try
            {
                var webImage = new System.Web.Helpers.WebImage(Request.Files["image"].InputStream);
                byte[] bytes = webImage.GetBytes();
                var a = listthongso.Where(x => x.ComponentID == com && x.MonitoringParamsID == mor).FirstOrDefault();
                a.ImageGS = imgToByteConverter(bytes);
                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
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


        [Authorize]
        public JsonResult GetMonitoringParameters(string id, string due, string todate, string mslcv, string stt)
        {
            try
            {
                listthongso = monitoringRepository.GetMonitoringParametersByDevice(id, Convert.ToInt32(due), Convert.ToDateTime(todate, new CultureInfo("vi-vn")).ToString("yyyy/MM/dd"), Convert.ToInt32(mslcv), Convert.ToInt32(stt)).ToList();
                //var gs = listthongso.Select(x => new
                //{
                //    DeviceID = x.DeviceID,
                //    MonitoringParamsName = x.MonitoringParamsName,
                //    MonitoringParamsID = x.MonitoringParamsID,
                //    ComponentID = x.ComponentID,
                //    ComponentName = x.ComponentName,
                //    TypeOfParam = x.TypeOfParam,
                //    MeasurementUnitName = x.MeasurementUnitName,
                //}).Distinct().ToList();

                //listgiamsat = gs.Select((x, i) => new MonitoringByDevice()
                //{
                //    STT = i + 1,
                //    DeviceID = x.DeviceID,
                //    MonitoringParamsName = x.MonitoringParamsName,
                //    MonitoringParamsID = x.MonitoringParamsID,
                //    ComponentID = x.ComponentID,
                //    ComponentName = x.ComponentName,
                //    TypeOfParam = x.TypeOfParam,
                //    MeasurementUnitName = x.MeasurementUnitName,
                //    TEN_GT = GetGiaTriTS(x.ComponentID, x.MonitoringParamsID, x.TypeOfParam),
                //    //Value = GetGiaTriTS(x.ComponentID, x.MonitoringParamsID, x.TypeOfParam),
                //    Speak = "thông số kiểm tra thứ " + (i + 1) + " " + x.MonitoringParamsName + " ở " + x.ComponentName + " gồm các giá trị sau: " + GetGiaTriTS(x.ComponentID, x.MonitoringParamsID, x.TypeOfParam),
                //    Message = "thông số kiểm tra thứ " + (i + 1) + " " + x.MonitoringParamsName + " ở " + x.ComponentName + " gồm các giá trị sau: " + GetGiaTriTS(x.ComponentID, x.MonitoringParamsID, x.TypeOfParam),
                //}).ToList();
                return Json(listthongso, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public JsonResult GetMonitoringParametersTMP()
        {
            try
            {
                return Json(listthongso, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public string GetGiaTriTS(string msbp, string msts, bool loaits)
        {
            string resulst = "";
            if (loaits == true)
            {
                var list = listthongso.Where(x => x.ComponentID.Equals(msbp) && x.MonitoringParamsID.Equals(msts)).ToList();
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        resulst += item.ValueParamName + ";";
                    }
                }
            }
            return resulst;
        }
        [Authorize]
        public JsonResult GetParameterInfo(int id)
        {
            try
            {
                return Json(monitoringRepository.GetParameterInfo(id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        public JsonResult Save(string data, string mscn, string stt, string ngaykt, string giokt,string chisotruoc,string luyke)
        {
            try
            {
                List<MonitoringParametersByDevice> lstParameter = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MonitoringParametersByDevice>>(data);
                List<MonitoringOfQuantitative> lstQuantitative = new List<MonitoringOfQuantitative>();
                List<MonitoringOfQualitative> lstQualitative = new List<MonitoringOfQualitative>();
                //cap nhat bytr imageGS
                var lists = listthongso.Where(x => x.ImageGS != null).ToList();
                for (int i = 0; i < lists.Count; i++)
                {
                    if (lists[i].ImageGS.Count() > 0)
                    {
                        var parameter = lstParameter.Where(x => x.DeviceID == lists[i].DeviceID && x.ComponentID == lists[i].ComponentID && x.MonitoringParamsID == lists[i].MonitoringParamsID).FirstOrDefault();
                        parameter.ImageGS = lists[i].ImageGS;
                    }
                }
                Monitoring obj = new Monitoring
                {
                    ID = Convert.ToInt32(stt),
                    Comment = "",
                    Staff = null,
                    ToHour = DateTime.Now,
                    //DateChecked = DateTime.Now.Date,
                    DateChecked = Convert.ToDateTime(ngaykt, new CultureInfo("vi-vn")),
                    HourOfRuntime = null,
                    //FromHour = DateTime.Now,
                    FromHour = Convert.ToDateTime(giokt, new CultureInfo("vi-vn")),
                    Username = User.Identity.GetUserName(),
                    StaffID = mscn,
                    MonitoringOfQuantitative = null,
                    votes = monitoringRepository.CreateSoPhieu(Convert.ToDateTime(ngaykt, new System.Globalization.CultureInfo("vi-vn")))
                };
                if (luyke != "")
                {
                    THOI_GIAN_CHAY_MAY tgcm = new THOI_GIAN_CHAY_MAY
                    {
                        MS_MAY = lstParameter[0].DeviceID,
                        NGAY = DateTime.Now,
                        CHI_SO_DONG_HO = (Convert.ToDouble(luyke) == 0 ? 0 : Convert.ToDouble(luyke) - Convert.ToDouble(chisotruoc)),
                        MS_PBT = obj.votes,
                        SO_MOVEMENT = 0,
                        SO_GIO_LUY_KE = Convert.ToDouble(luyke),
                        USERNAME = User.Identity.GetUserName()
                    };
                    thoigianchaymayRepository.Add(tgcm);
                }

                var lst = lstParameter.GroupBy(x => new { x.ComponentID, x.DeviceID, x.MonitoringParamsID }).Select(x => new MonitoringParametersByDevice
                {
                    MonitoringParamsID = x.First().MonitoringParamsID,
                    ComponentID = x.First().ComponentID,
                    DeviceID = x.First().DeviceID,
                    Pass = x.First().Pass,
                    Note = x.First().Note,
                    ValueParamID = x.First().ValueParamID,
                    Measurement = x.First().Measurement,
                    TypeOfParam = x.First().TypeOfParam,
                    ImageGS = x.First().ImageGS,
                    ID = x.First().ID
                });
                lst.ToList().ForEach(x =>
                {
                    lstQuantitative.Add(new MonitoringOfQuantitative
                    {
                        MonitoringID = obj.ID,
                        MonitoringParamsID = x.MonitoringParamsID,
                        DeviceID = x.DeviceID,
                        ComponentID = x.ComponentID,
                        Measurement = x.Measurement,
                        ID = x.ID,
                        Note = x.Note,
                        ImageGS = x.ImageGS
                    });
                });
                lstParameter.ForEach(x =>
                {
                    if (x.TypeOfParam == true)
                    {
                        lstQualitative.Add(new MonitoringOfQualitative
                        {
                            MonitoringID = obj.ID,
                            MonitoringParamsID = x.MonitoringParamsID,
                            DeviceID = x.DeviceID,
                            ComponentID = x.ComponentID,
                            ValueParamID = x.ValueParamID,
                            ID = x.ID,
                            Note = x.Note,
                        });
                    }
                });
                if (stt == "-1")
                {//thêm
                    monitoringUnitOfWork.MonitoringRepository.Add(obj);//add giám sát tình trạng

                }
                else
                {//sữa
                    monitoringUnitOfWork.MonitoringRepository.Edit(obj);
                    monitoringUnitOfWork.MonitoringOfQualitativeRepository.Delete(Convert.ToInt32(stt));
                    monitoringUnitOfWork.MonitoringOfQuantitativeRepository.Delete(Convert.ToInt32(stt));
                    monitoringUnitOfWork.Save();
                }
                monitoringUnitOfWork.MonitoringOfQuantitativeRepository.AddRange(lstQuantitative);
                monitoringUnitOfWork.MonitoringOfQualitativeRepository.AddRange(lstQualitative);
     
                monitoringUnitOfWork.Save();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error: " + ex.InnerException.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult XoaGiamSatTinhTrang(int id)
        {
            if (monitoringRepository.XoaGiamSatTinhTrang(id) == 1)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("failure", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult StarVoice(Int64 stt, string msmay)
        {
            string name = "", mess = "";
            name = GetMp3("Bắc đầu kiểm tra thông số của máy " + msmay + "");
            mess = "<b>Bắc đầu kiểm tra thông số của máy " + msmay + "!</b>";
            stt = 0;
            return Json(new { name = name, tengt = mess, stt = stt }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SpeakVoice(string a, Int64 stt, string msmay)
        {
            string name = "", mess = "";
            MonitoringByDevice recoure = listgiamsat.Where(x => x.STT == stt).FirstOrDefault();
            MonitoringByDevice check = listgiamsat.Where(x => x.STT == stt - 1).FirstOrDefault();
            if (CheckData(check, a) == false)
            {
                name = GetMp3("Giá trị bạn chọn không đúng xin vui lòng chọn lại");
                mess = "Giá trị bạn chọn không đúng xin vui lòng chọn lại!";
                stt = 0;
            }
            else
            {
                if (recoure == null)
                {
                    name = GetMp3("Bạn đã kiểm tra hết thông số");
                    mess = "Bạn đã kiểm tra hết thông số!";
                    stt = 0;
                }
                else
                {
                    name = GetMp3(recoure.Speak);
                    mess = recoure.Message;
                    //update giá trị vào list gía trị
                    try
                    {
                        if (a.ToLower() == "ok")
                        {
                            var list = listthongso.Where(x => x.ComponentID.Equals(check.ComponentID) && x.MonitoringParamsID.Equals(check.MonitoringParamsID) && x.Pass == 1).FirstOrDefault();
                            list.Measurement = 1;
                        }
                        else
                        {
                            var list = listthongso.Where(x => x.ComponentID.Equals(check.ComponentID) && x.MonitoringParamsID.Equals(check.MonitoringParamsID) && x.ValueParamName.ToLower().Trim().Equals(a.ToLower().Trim())).FirstOrDefault();
                            list.Measurement = 1;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return Json(new { name = name, tengt = mess, stt = stt }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetThoiGianChayMay(string msmay)
        {
            try
            {
                double? sglk = thoigianchaymayRepository.SoGioLuyKeHienTai(msmay);
                var tgcm = thoigianchaymayRepository.ThoiGianLuyKeTruoc(msmay);
                string chuoi = "";
                if (SessionVariable.TypeLanguage == 0)
                    chuoi = "Vào ngày " + tgcm.NGAY.ToString("dd/MM/yyyy") + ": " + tgcm.SO_GIO_LUY_KE.ToString();
                else
                    chuoi = "Date " + tgcm.NGAY.ToString("dd/MM/yyyy") + ": " + tgcm.SO_GIO_LUY_KE.ToString();
                return Json(new { chuoi = chuoi, sglk = (sglk == 0 ? null : sglk) , sgs = tgcm.SO_GIO_LUY_KE }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { chuoi = (SessionVariable.TypeLanguage == 0 ? "Chưa có thời gian chạy máy" : "no time to run"), sglk = "", sgs = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public bool CheckData(MonitoringByDevice row, string input)
        {
            bool resulst = false;
            if (row is null)
            {
                resulst = true;
            }
            else
            {
                List<string> array = new List<string> { "ok", "đạt" };
                if (row.TEN_GT.ToString() != "")
                {
                    string[] mang = row.TEN_GT.ToString().Split(';');
                    array.AddRange(mang);
                    foreach (var item in array)
                    {
                        if (item.ToString().ToLower().Trim() == input.ToLower().Trim())
                        {
                            resulst = true; break;
                        }
                    }
                }
                else
                {
                    try
                    {
                        Int64 so = Convert.ToInt64(input.Trim());
                        resulst = true;
                    }
                    catch (Exception)
                    {
                        resulst = false;
                    }
                }
            }
            return resulst;
        }
        public string GetMp3(string a)
        {
            DirectoryInfo di = new DirectoryInfo(HttpContext.Server.MapPath("~/Resource"));
            foreach (FileInfo Files in di.GetFiles())
            {
                Files.Delete();
            }
            string sText = "";
            sText = a;
            sText = sKeyV.Replace("AAAXXX", sText);
            //var url = "http://translate.google.com/translate_tts?tl=pt&q=";
            //url += HttpUtility.UrlEncode(text, Encoding.GetEncoding("utf-8"));
            var url = sText;
            url = HttpUtility.UrlEncode(sText, Encoding.GetEncoding("utf-8"));
            string name = DateTime.Now.TimeOfDay.Seconds.ToString();
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:7.0.1) Gecko/20100101 Firefox/7.0.1";
                client.DownloadFile(sText, HttpContext.Server.MapPath("~/Resource/" + name + ".mp3"));
            }
            return name;
        }

    }
}