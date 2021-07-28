using Biz.Lib.Helpers;
using Microsoft.ApplicationBlocks.Data;
using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class MonitoringRepository : IMonitoringRepository, IDisposable
    {
        Model.Data.Model1 db;
        public MonitoringRepository() => db = new Model.Data.Model1();

        public MonitoringRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<Monitoring> ListAll(DateTime dt) =>
            db.Monitoring.Where(x => x.DateChecked == dt.Date && db.MonitoringOfQuantitative.Where(y => y.MonitoringID == x.ID).GroupBy(z =>
            new
            {
                z.ID,
                z.DeviceID
            }).Select(z =>
            new
            {
                ID = z.FirstOrDefault().ID,
                DeviceID = z.FirstOrDefault().DeviceID
            }).Count() == 1).ToList();

        private void ChecktheParameterDue() => db.Database.SqlQuery<Monitoring>("asdfa adfs, adsfafd,asdf,asdfadf", new object[] { new SqlParameter("@adfaDSF", "Ádfadf") });
        public IEnumerable<DeviceObjForDropdown> CheckTheParametersDue() =>
            db.Database.SqlQuery<DeviceObjForDropdown>("spCheckTheParametersDue").ToList();

        public IEnumerable<Monitoring> ListAll() => db.Monitoring.ToList();

        public void Add(Monitoring monitor) => db.Monitoring.Add(monitor);

        public void Edit(Monitoring obj)
        {
            db.Monitoring.Attach(obj);
            db.Entry(obj).Property(x => x.DateChecked).IsModified = true;
            db.Entry(obj).Property(x => x.Username).IsModified = true;
            db.Entry(obj).Property(x => x.Comment).IsModified = true;
            db.Entry(obj).Property(x => x.HourOfRuntime).IsModified = true;
            db.Entry(obj).Property(x => x.StaffID).IsModified = true;
            db.Entry(obj).Property(x => x.ToHour).IsModified = true;
            db.Entry(obj).Property(x => x.FromHour).IsModified = true;
        }

        public IEnumerable<MonitoringParametersByDevice> GetParameterInfo(int ID) =>
            db.Database.SqlQuery<MonitoringParametersByDevice>("GetParameterInfo @ID", new object[]
            {
                new SqlParameter("@ID", ID)
            }).ToList();

        public void Delete(int ID) => db.Monitoring.Remove(db.Monitoring.SingleOrDefault(x => x.ID == ID));

        public IEnumerable<MonitoringParametersByDevice> GetMonitoringParametersByDevice(string deviceID, int isDue, string dngay, int msloaicv,int stt)
        {
            List<MonitoringParametersByDevice> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@deviceID", deviceID));
                listParameter.Add(new SqlParameter("@tungay", dngay));
                listParameter.Add(new SqlParameter("@ms_loaicv", msloaicv));
                listParameter.Add(new SqlParameter("@isDue", isDue));
                listParameter.Add(new SqlParameter("@stt",stt));
                list = DBUtils.ExecuteSPList<MonitoringParametersByDevice>("GetMonitoringParametersByDevice", listParameter, AppName.Model1);
                //DataTable dt = new DataTable();
                //dt.Load(SqlHelper.ExecuteReader(db.Database.Connection.ConnectionString, "GetMonitoringParametersByDevice", deviceID, "10/10/2020", msloaicv, isDue, stt));

            }
            catch (Exception ex)
            {
            }
            return list;
        }
        public IEnumerable<MoningtoringViewModel> GetGiamSatTinhTrang(DateTime tungay,DateTime denngay,string mscn,string msmay)
        {
            List<MoningtoringViewModel> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@TU_NGAY",tungay));
                listParameter.Add(new SqlParameter("@DEN_NGAY", denngay));
                listParameter.Add(new SqlParameter("@MS_CONG_NHAN", mscn));
                listParameter.Add(new SqlParameter("@MS_MAY", msmay));
                list = DBUtils.ExecuteSPList<MoningtoringViewModel>("GetGIAM_SAT_TINH_TRANG_WEB",listParameter,AppName.Model1);
            }
            catch (Exception){ }
            return list;
        }

        public IEnumerable<MoningtoringViewModel.ThongSoGiamSat> GetThongSoGSTT(int stt, int dat, string msmay, int loaits)
        {
            List<MoningtoringViewModel.ThongSoGiamSat> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@STT", stt));
                listParameter.Add(new SqlParameter("@DAT", dat));
                listParameter.Add(new SqlParameter("@MS_MAY", msmay));
                listParameter.Add(new SqlParameter("@LOAI_TS", loaits));
                list = DBUtils.ExecuteSPList<MoningtoringViewModel.ThongSoGiamSat>("GetGIAM_SAT_TINH_TRANG_TSs_DAT", listParameter, AppName.Model1);
            }
            catch (Exception) { }
            return list;
        }

        public string CreateSoPhieu(DateTime date)
        {
            string s = Convert.ToString(SqlHelper.ExecuteScalar(db.Database.Connection.ConnectionString,System.Data.CommandType.Text, "select dbo.AUTO_CREATE_SO_PHIEU_GSTT('" + date.Date.ToString("MM/dd/yyyy") + "')"));
            return s;
        }
        public int XoaGiamSatTinhTrang(int ID)
        {
            int ResponseCode = 0;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@ID", ID));
                
                SqlHelper.ExecuteNonQuery(db.Database.Connection.ConnectionString, "spXoaGiamSatTinhTrang",ID, "-1", "-1", "-1", -1, -1, 1);
                ResponseCode = 1;
            }
            catch (Exception)
            {
            }
            return ResponseCode;
        }

        public IEnumerable<MoningtoringViewModel.ThongSoGiamSat.GiaTri> GetGiaTriDT(int stt,string msmay,string msbp,string msts, int loai)
        {
            List<MoningtoringViewModel.ThongSoGiamSat.GiaTri> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@STT", stt));
                listParameter.Add(new SqlParameter("@MS_MAY", msmay));
                listParameter.Add(new SqlParameter("@MS_BO_PHAN", msbp));
                listParameter.Add(new SqlParameter("@MS_TS_GSTT", msts));
                listParameter.Add(new SqlParameter("@LOAI", loai));
                list = DBUtils.ExecuteSPList<MoningtoringViewModel.ThongSoGiamSat.GiaTri>("GetGIAM_SAT_TINH_TRANG_TS_DT", listParameter, AppName.Model1);
            }
            catch (Exception) { }
            return list;
        }

        public IEnumerable<MoningtoringViewModel.ThongSoGiamSat.GiaTriDL> GetGiaTriDL(int stt, string msmay, string msbp, string msts, string Username,int NNgu)
        {
            List<MoningtoringViewModel.ThongSoGiamSat.GiaTriDL> list = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@STT", stt));
                listParameter.Add(new SqlParameter("@MsThonsSo", msts));
                listParameter.Add(new SqlParameter("@LoaiTS", 0));
                listParameter.Add(new SqlParameter("@MsMay", msmay));
                listParameter.Add(new SqlParameter("@MsBP", msbp));
                listParameter.Add(new SqlParameter("@UserName", Username));
                listParameter.Add(new SqlParameter("@NNgu", NNgu));
                list = DBUtils.ExecuteSPList<MoningtoringViewModel.ThongSoGiamSat.GiaTriDL>("sp_GetDanhSachGiaTri", listParameter, AppName.Model1);
            }
            catch (Exception) { }
            return list;
        }


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
