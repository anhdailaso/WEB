//using Dynamsoft.Barcode;
using Microsoft.AspNet.Identity;
using Model.Combobox;
using Model.Interface;
using Model.Interface.IRepository;
using Model.Repository.Repository;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using ZXing;
using System.Text;
using QRCodeDecoderLibrary;
using ZXing;
using System.Drawing;
using System.ComponentModel;
//using IronBarCode;

namespace EcomaintSite.Controllers
{
    public class MyEcomainController : Controller
    {

        private ICombobox _Combobox;
        private IMyEcomainRepository _MyEcomainRepository;
        private ICombobox Combobox()
        {
            return _Combobox ?? (_Combobox = new Combobox());
        }

        private IMyEcomainRepository MyEcomain()
        {
            return _MyEcomainRepository ?? (_MyEcomainRepository = new MyEcomainRepository());
        }
        // GET: MyEcomain
        [Authorize]
        public ActionResult Index()
        {
            var us = User.Identity.GetUserName();
            Loadcombo(us);
            return View();
        }
        private void Loadcombo(string user)
        {
            ViewBag.NhaXuong = Combobox().GetCbbDiaDiem(user, SessionVariable.TypeLanguage, 1);
            //ViewBag.HeThong = Combobox().GetCbbHeThong(user, SessionVariable.TypeLanguage, 1);
            //ViewBag.LoaiMay = Combobox().GetCbbLoaiMay(user, SessionVariable.TypeLanguage, 1);
            //ViewBag.May = Combobox().GetCbbMay(user, SessionVariable.TypeLanguage, 1);
            //ViewBag.LoaiCV = Combobox().GetCbbLoaiCV(user, SessionVariable.TypeLanguage, 1);
        }
        [Authorize]
        public JsonResult GetPhieuBaoTri(string ms_nx, string ms_ht, string ms_loaimay, string ms_may)
        {
            var lst = MyEcomain().GetPhieuBaoTris(User.Identity.GetUserName(), SessionVariable.TypeLanguage, ms_nx, Convert.ToInt32(ms_ht), ms_loaimay, ms_may).Select(x => new
            {
                MS_PHIEU_BAO_TRI = x.MS_PHIEU_BAO_TRI,
                MS_MAY = x.MS_MAY,
                TEN_MAY = x.TEN_MAY,
                TEN_LOAI_BT = x.TEN_LOAI_BT,
                TEN_TINH_TRANG = x.TEN_TINH_TRANG,
                NGAY_BD_KH = x.NGAY_BD_KH.ToString("dd/MM/yyyy"),
                NGAY_KT_KH = x.NGAY_KT_KH.ToString("dd/MM/yyyy"),
                Ten_N_XUONG = x.Ten_N_XUONG
            });
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGiamSatTinhTrang(string fromDate, string toDate, string ms_nx, string ms_ht, string ms_loaimay, string ms_may, string ms_loaicv)
        {
            var lst = MyEcomain().GetGSTRs(User.Identity.GetUserName(), SessionVariable.TypeLanguage, Convert.ToDateTime(fromDate, new CultureInfo("vi-vn")).ToString("yyyy/MM/dd"), Convert.ToDateTime(toDate, new CultureInfo("vi-vn")).ToString("yyyy/MM/dd"), ms_nx, Convert.ToInt32(ms_ht), ms_loaimay, ms_may, Convert.ToInt32(ms_loaicv)).Select(x => new
            {
                MS_MAY = x.MS_MAY,
                TEN_MAY = x.TEN_MAY,
                TEN_BO_PHAN = x.TEN_BO_PHAN,
                TEN_TS_GSTT = x.TEN_TS_GSTT,
                CHU_KY_DO = x.CHU_KY_DO,
                NGAY_KT_CUOI = x.NGAY_KT_CUOI.ToString("dd/MM/yyyy"),
                TEN_GIA_TRI = x.TEN_GIA_TRI,
                NGAY_KT_KE = x.NGAY_KT_KE.ToString("dd/MM/yyyy"),
                CAC_THUC_HIEN = x.CAC_THUC_HIEN,
                THOI_GIAN = x.THOI_GIAN
            });
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GeMyecomain(string fromDate, string toDate, string ms_nx, string may, string giadoan)
        {
            try
            {
                var lst = MyEcomain().GetMyEcomain(User.Identity.GetUserName(), SessionVariable.TypeLanguage, Convert.ToDateTime(fromDate, new CultureInfo("vi-vn")).ToString("yyyy/MM/dd"), Convert.ToDateTime(toDate, new CultureInfo("vi-vn")).ToString("yyyy/MM/dd"), ms_nx, may, Convert.ToInt32(giadoan)).Select(x => new
                {
                    MS_MAY = x.MS_MAY,
                    TEN_MAY = x.TEN_MAY,
                    PBT = x.PBT,
                    GSTT = x.GSTT
                });
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ShowPBT(string id)
        {
            string nx = Request["nhaxuong"];
            string tngay = Request["fromday"];
            string dengnay = Request["today"];
            return RedirectToAction("show", "WorkOrder", new { msnx = nx, msmay = id, todate = tngay, fromdate = dengnay });
        }
        public ActionResult ShowGSTT(string id)
        {
            string nx = Request["nhaxuong"];
            string tungay = Request["fromday"];
            string dengnay = Request["today"];
            return RedirectToAction("Show", "Monitoring", new { msnx = nx, msmay = id, tngay = tungay, dngay = dengnay });
        }

        private static string QRCodeResult
        (
        byte[][] DataByteArray
        )
        {
            // no QR code
            if (DataByteArray == null) return string.Empty;

            // image has one QR code
            if (DataByteArray.Length == 1) return ForDisplay(QRDecoder.ByteArrayToStr(DataByteArray[0]));

            // image has more than one QR code
            StringBuilder Str = new StringBuilder();
            for (int Index = 0; Index < DataByteArray.Length; Index++)
            {
                if (Index != 0) Str.Append("\r\n");
                Str.AppendFormat("QR Code {0}\r\n", Index + 1);
                Str.Append(ForDisplay(QRDecoder.ByteArrayToStr(DataByteArray[Index])));
            }
            return Str.ToString();
        }
        private static string ForDisplay
        (
        string Result
        )
        {
            int Index;
            for (Index = 0; Index < Result.Length && (Result[Index] >= ' ' && Result[Index] <= '~' || Result[Index] >= 160); Index++) ;
            if (Index == Result.Length) return Result;

            StringBuilder Display = new StringBuilder(Result.Substring(0, Index));
            for (; Index < Result.Length; Index++)
            {
                char OneChar = Result[Index];
                if (OneChar >= ' ' && OneChar <= '~' || OneChar >= 160)
                {
                    Display.Append(OneChar);
                    continue;
                }

                if (OneChar == '\r')
                {
                    Display.Append("\r\n");
                    if (Index + 1 < Result.Length && Result[Index + 1] == '\n') Index++;
                    continue;
                }

                if (OneChar == '\n')
                {
                    Display.Append("\r\n");
                    continue;
                }

                Display.Append('¿');
            }
            return Display.ToString();
        }

        [HttpPost]
        public JsonResult ProcessRequest()
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            HttpPostedFile imgBinary = request.Files["image"];
            try
            {
                Stream iStream = imgBinary.InputStream;
                byte[] bytes = new byte[iStream.Length];
                iStream.Seek(0, SeekOrigin.Begin);
                iStream.Read(bytes, 0, bytes.Length);
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                Bitmap bitmap = (Bitmap)tc.ConvertFrom(bytes);
                BarcodeReader reder = new BarcodeReader();

                ////////////////MessagingToolkit.QRCode.Codec.QRCodeDecoder decoder = new MessagingToolkit.QRCode.Codec.QRCodeDecoder();
                ////////////////string resulst = decoder.Decode(new QRCodeBitmapImage(bitmap));
                Result resulst = reder.Decode(bitmap);
                if (resulst != null)
                {
                    string s = resulst.Text;
                }
                return Json(resulst.Text.ToString(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }

            //HttpRequest request = System.Web.HttpContext.Current.Request;
            //bool isSuccess = false;
            //Exception readBarcodeException = null;
            //TextResult[] results = null;
            //TimeSpan tsDecode = TimeSpan.Zero;
            //try
            //{
            //    // 1. Get Base64 Stream
            //    Request.InputStream.Position = 0;
            //    string jsonString;
            //    using (StreamReader streamReader = new StreamReader(request.InputStream))
            //    {
            //        jsonString = streamReader.ReadToEnd();
            //    }

            //    HttpPostedFile imgBinary = request.Files["image"];
            //    int barcodeFormat = int.Parse(request["barcodeFormat"]);
            //    if (imgBinary == null)
            //    {
            //        throw new Exception("Post data is null.");
            //    }

            //    // 2. load settings
            //    BarcodeReader barcodeReader = new BarcodeReader();
            //    barcodeReader.ProductKeys = System.Web.Configuration.WebConfigurationManager.AppSettings["barcodeReaderKey"];
            //    //Best coverage setting
            //    //barcodeReader.InitRuntimeSettingsWithString("{\"ImageParameter\":{\"Name\":\"BestCoverage\",\"DeblurLevel\":9,\"ExpectedBarcodesCount\":512,\"ScaleDownThreshold\":100000,\"LocalizationModes\":[{\"Mode\":\"LM_CONNECTED_BLOCKS\"},{\"Mode\":\"LM_SCAN_DIRECTLY\"},{\"Mode\":\"LM_STATISTICS\"},{\"Mode\":\"LM_LINES\"},{\"Mode\":\"LM_STATISTICS_MARKS\"}],\"GrayscaleTransformationModes\":[{\"Mode\":\"GTM_ORIGINAL\"},{\"Mode\":\"GTM_INVERTED\"}]}}", EnumConflictMode.CM_OVERWRITE, out errorMessage);
            //    //Best speed setting
            //    //barcodeReader.InitRuntimeSettingsWithString("{\"ImageParameter\":{\"Name\":\"BestSpeed\",\"DeblurLevel\":3,\"ExpectedBarcodesCount\":512,\"LocalizationModes\":[{\"Mode\":\"LM_SCAN_DIRECTLY\"}],\"TextFilterModes\":[{\"MinImageDimension\":262144,\"Mode\":\"TFM_GENERAL_CONTOUR\"}]}}", EnumConflictMode.CM_OVERWRITE, out errorMessage);
            //    ////Balance setting
            //    //barcodeReader.InitRuntimeSettingsWithString("{\"ImageParameter\":{\"Name\":\"Balance\",\"DeblurLevel\":5,\"ExpectedBarcodesCount\":512,\"LocalizationModes\":[{\"Mode\":\"LM_CONNECTED_BLOCKS\"},{\"Mode\":\"LM_STATISTICS\"}]}}", EnumConflictMode.CM_OVERWRITE, out errorMessage);

            //    PublicRuntimeSettings settings = barcodeReader.GetRuntimeSettings();
            //    settings.BarcodeFormatIds = barcodeFormat;
            //    barcodeReader.UpdateRuntimeSettings(settings);
            //    // 3. read and get result
            //    Stream iStream = imgBinary.InputStream;
            //    byte[] bytes = new byte[iStream.Length];
            //    iStream.Seek(0, SeekOrigin.Begin);
            //    iStream.Read(bytes, 0, bytes.Length);
            //    results = barcodeReader.DecodeFileInMemory(bytes, "");
            //    string[] s = results[0].BarcodeText.ToString().Split('*');
            //    return Json(s[0], JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            //}
        }
    }
}