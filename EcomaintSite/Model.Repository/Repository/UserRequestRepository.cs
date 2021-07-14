using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using Model.Interface;
using Biz.Lib.Helpers;
using Microsoft.SqlServer.Server;
using System.Data;
using Model.Data.ViewModel;
using System.Web.Mvc;

namespace Model.Repository
{
    public class UserRequestRepository : IUserRequestRepository, IDisposable
    {
        Model.Data.Model1 db;
        public UserRequestRepository() => db = new Model1();
        public UserRequestRepository(Model1 context) => db = context;

        string data = "";
        public UserRequest GetUserRequest(int ID) =>
            db.UserRequest.Where(x => x.ID == ID).Count() > 0 ? db.UserRequest.Where(x => x.ID == ID).SingleOrDefault() : null;

        public IEnumerable<GetUserRequestObj> GetUserRequest(string requestedBy, string MS_N_XUONG, string fromDate, string toDate, string username, int languages)
        {
           var lst = db.Database.SqlQuery<GetUserRequestObj>("GetUserRequest @requestedBy,@MS_N_XUONG, @fromDate, @toDate, @username, @lang", new object[]
           {
                new SqlParameter("@requestedBy", requestedBy),
                new SqlParameter("@MS_N_XUONG", MS_N_XUONG),
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate", toDate),
                new SqlParameter("@username", username),
                new SqlParameter("@lang", languages)
           }).ToList();
            return lst;
        }
        public IEnumerable<GetUserRequestObj> GetCreatedBy()
        {
            var model = db.UserRequest.Select(x => new GetUserRequestObj
            {
                RequestedBy = x.RequestedBy,
                RequestedName = x.RequestedBy
            }).Distinct().ToList();
            model.Insert(0, new GetUserRequestObj
            {
                RequestedBy = "-1",
                RequestedName = "< ALL >"
            });
            return model;
        }
        public string GetNguoiYC(string UserName)
        {
            string resulst ="";
            try
            {
                resulst = SqlHelper.ExecuteScalar(DBUtils.BizConnectionString().ToString(), CommandType.Text, "SELECT  B.HO +' '+B.TEN FROM dbo.USERS A INNER JOIN dbo.CONG_NHAN B ON B.MS_CONG_NHAN = A.MS_CONG_NHAN WHERE A.USERNAME = '" + UserName + "'").ToString();
            }
            catch (Exception ex)
            {
                resulst = "";
            }
            return resulst;
        }

      

        public IEnumerable<DiaDiemViewModel> GetNhaXuong(string UserName, int NNgu, int Coall)
        {
            List<DiaDiemViewModel> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@UserName", UserName));
                listParameter.Add(new SqlParameter("@NNgu", NNgu));
                listParameter.Add(new SqlParameter("@CoAll", Coall));
                list = DBUtils.ExecuteSPList<DiaDiemViewModel>("GetNhaXuongAll", listParameter, AppName.Model1);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public IEnumerable<PhuTungViewModel.PhuTung> DanhSachPhuTungTheoMay(int id, string may, int stt_vande)
        {
            List<PhuTungViewModel.PhuTung> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@MS_MAY", may));
                listParameter.Add(new SqlParameter("@STT", id));
                listParameter.Add(new SqlParameter("@STT_VAN_DE", stt_vande));
                list = DBUtils.ExecuteSPList<PhuTungViewModel.PhuTung>("GetPhuTungTheoMayWeb", listParameter, AppName.Model1);
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public IEnumerable<PhuTungViewModel.BoPhan> DanhSachBoPhanTheoMay(int id, string may)
        {
            List<PhuTungViewModel.BoPhan> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@MS_MAY", may));
                listParameter.Add(new SqlParameter("@STT", id));
                list = DBUtils.ExecuteSPList<PhuTungViewModel.BoPhan>("GetBoPhanTheoMay", listParameter, AppName.Model1);
            }
            catch (Exception)
            {
            }
            return list;
        }
        public int ThemChiTietHinh(int stt, string msmay, int stt_vande, string filename)
        {
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@STT", stt));
                listParameter.Add(new SqlParameter("@MS_MAY", msmay));
                listParameter.Add(new SqlParameter("@STT_VAN_DE", stt_vande));
                listParameter.Add(new SqlParameter("@DUONG_DAN", filename));
                DBUtils.ExecuteSP<int>("SP_YEU_CAU_NSD_CHI_TIET_HINH_THEM", listParameter, AppName.BizSecurity);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int ThemChiTietBoPhan(PhuTungViewModel model)
        {
            int result = 0;
            List<PhuTungViewModel.PhuTung> listpt = new List<PhuTungViewModel.PhuTung>();
            List<PhuTungViewModel.BoPhan> listbp = new List<PhuTungViewModel.BoPhan>();
            //tao bang tam phu tung
            try
            {
                listpt = model.ListPhuTung.Where(x => x.Choose.Equals(true)).ToList();
            }
            catch (Exception)
            {
            }
            listbp = model.ListBoPhan.Where(x => x.Choose.Equals(true)).ToList();
            // tao bang tam cong viec
            //listParameter.Add(new SqlParameter("@MaNV", MaNV));
            var pList = new SqlParameter("@DanhSachPhuTung", SqlDbType.Structured);
            pList.TypeName = "dbo.DanhSachPhuTung";
            pList.Value = GetDSPhuThung(listpt, listbp, model.STT, model.MS_MAY, model.STT_VAN_DE);
            List<SqlParameter> listparameter = new List<SqlParameter>();
            listparameter.Add(pList);
            try
            {
                DBUtils.ExecuteSP<int>("SP_YEU_CAU_NSD_CHI_TIET_BO_PHAN_Them", listparameter, AppName.BizSecurity);
                result = 1;
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }
        private List<SqlDataRecord> GetDSPhuThung(List<PhuTungViewModel.PhuTung> danhsachphutung, List<PhuTungViewModel.BoPhan> danhsachbophan, int stt, string msmay, int stt_vande)
        {
            List<SqlDataRecord> datatable = new List<SqlDataRecord>();
            SqlMetaData[] sqlMetaData = new SqlMetaData[6];
            sqlMetaData[0] = new SqlMetaData("STT", SqlDbType.Int);
            sqlMetaData[1] = new SqlMetaData("MS_MAY", SqlDbType.NVarChar, 30);
            sqlMetaData[2] = new SqlMetaData("STT_VAN_DE", SqlDbType.Int);
            sqlMetaData[3] = new SqlMetaData("MS_BO_PHAN", SqlDbType.NVarChar, (30));
            sqlMetaData[4] = new SqlMetaData("MS_PT", SqlDbType.NVarChar, 30);
            sqlMetaData[5] = new SqlMetaData("MS_VI_TRI_PT", SqlDbType.NVarChar, 30);
            SqlDataRecord row = new SqlDataRecord(sqlMetaData);
            try
            {
                foreach (var item in danhsachphutung)
                {
                    row = new SqlDataRecord(sqlMetaData);
                    row.SetValues(new object[] {
                    stt,
                    msmay,
                    stt_vande,
                    item.MS_BO_PHAN,
                    item.MS_PT,
                    item.MS_VI_TRI_PT
                });
                    datatable.Add(row);
                }
            }
            catch (Exception ex)
            {
            }
            try
            {
                foreach (var bp in danhsachbophan)
                {
                    row = new SqlDataRecord(sqlMetaData);
                    row.SetValues(new object[] {
                    stt,
                    msmay,
                    stt_vande,
                    bp.MS_BO_PHAN,
                    "",
                    ""
                });
                    datatable.Add(row);
                }
            }
            catch (Exception ex)
            {
            }


            return datatable;
        }

        public int XoaYeuCau(int ID)
        {
            int ResponseCode = 0;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@ID", ID));
                ResponseCode = DBUtils.ExecuteSP<int>("SP_YEU_CAU_NSD_XOA", listParameter, AppName.BizSecurity);
                ResponseCode = 1;
            }
            catch (Exception)
            {
                ResponseCode = 0;

            }
            return ResponseCode;
        }


        public int XoaChiTietBoPhan(int ID)
        {
            int ResponseCode = 0;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@ID", ID));
                ResponseCode = DBUtils.ExecuteSP<int>("SP_YEU_CAU_NSD_CHI_TIET_BO_PHAN_XOA", listParameter, AppName.BizSecurity);
                ResponseCode = 1;
            }
            catch (Exception)
            {
                ResponseCode = 0;

            }
            return ResponseCode;
        }

        public string GetPath(int ID)
        {
            string resulst = SqlHelper.ExecuteScalar(db.Database.Connection.ConnectionString, CommandType.Text, "SELECT DUONG_DAN FROM dbo.YEU_CAU_NSD_CHI_TIET_HINH WHERE STT_HINH = " + ID + "").ToString();
            return resulst;
        }
        public int XoaChiTietHinh(int ID)
        {
            int ResponseCode = 0;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@ID", ID));
                ResponseCode = DBUtils.ExecuteSP<int>("SP_YEU_CAU_NSD_CHI_TIET_HINH_XOA", listParameter, AppName.BizSecurity);
                ResponseCode = 1;
            }
            catch (Exception)
            {
                ResponseCode = 0;
            }
            return ResponseCode;
        }
        public string CreateRequestID()
        {
            DateTime date;
            string ID = "";
            int iMax = 0;
            date = Convert.ToDateTime(DateTime.Now.Month + "/" + DateTime.Now.Year);
            try
            {
                object[] parameter =
                {
                    new SqlParameter("@ID_H", "WR"),
                    new SqlParameter("@ID_D",  date.ToString("MM/dd/yyyy"))
                };
                iMax = db.Database.SqlQuery<int>("MGetIDYCSD @ID_H, @ID_D", parameter).FirstOrDefault();
                ID = "WR-" + date.ToString("yyyyMM") + iMax.ToString("000000");
            }
            catch { ID = "WR-" + date.ToString("yyyyMM") + 1.ToString("000000"); }
            if (CheckExistRequest(ID))
            {
                iMax = iMax + 1;
                ID = "WR-" + date.ToString("yyyyMM") + iMax.ToString("000000");
            }
            if (CheckExistRequest(ID))
            {
                iMax = iMax + 1;
                ID = "WR-" + date.ToString("yyyyMM") + iMax.ToString("000000");
            }
            return ID;
        }

        public SelectList GetNguoiYeuCau(string UserName)
        {
            DataTable tb = new DataTable();
            tb.Load(SqlHelper.ExecuteReader(db.Database.Connection.ConnectionString,"GetCONG_NHAN_UserAll","-1","-1",1,UserName));
            var listItem = tb.AsEnumerable().Select(
                  x => new SelectListItem
                  {
                      Text = x.Field<string>("HO_TEN"),
                      Value = x.Field<string>("MS_CONG_NHAN").ToString()
                  });
            return new SelectList(listItem, "Value", "Text", null);
        }
        public Boolean CheckExistRequest(string ID) => db.UserRequest.Any(x => x.UserRequestID == ID);
     
        public void SaveRequest(UserRequest obj) => db.UserRequest.Add(obj);

        public void SaveChanges() => db.SaveChanges();

        private bool disposed = false;
        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
