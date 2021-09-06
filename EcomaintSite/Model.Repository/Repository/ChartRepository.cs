using Microsoft.ApplicationBlocks.Data;
using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class ChartRepository : IChartRepository, IDisposable
    {
        Model.Data.Model1 db;
        public ChartRepository() => db = new Model1();

        public IEnumerable<GetSituationWOObj> GetSituationWO(Nullable<int> lang) =>
            db.Database.SqlQuery<GetSituationWOObj>("spGetSituationWO @lang", new SqlParameter("@lang", lang)).ToList();

        public IEnumerable<GetSituationWOColumnObj> GetSituationWOColumn(Nullable<int> lang) =>
            db.Database.SqlQuery<GetSituationWOColumnObj>("spGetSituationWOColumn @lang", new SqlParameter("@lang", lang)).ToList();

        public IEnumerable<GetDeviceInfoObj> GetDeviceInfo(Nullable<int> lang)
        =>db.Database.SqlQuery<GetDeviceInfoObj>("spGetDeviceInfo @lang ,@username", new object[] {
                new SqlParameter("@lang", lang),
                new SqlParameter("@username", "Admin")
            }).ToList();
        public IEnumerable<GetDeviceStatusObj> GetDeviceStatus(Nullable<System.DateTime> toDate, string Username, Nullable<int> lang)
        =>db.Database.SqlQuery<GetDeviceStatusObj>("spGetDeviceStatus @toDate, @username, @lang", new object[] {
                new SqlParameter("@toDate", toDate),
                new SqlParameter("@username", Username),
                new SqlParameter("@lang", lang)
            }).ToList();

        public int GetSoMay()
        {
            int resulst = Convert.ToInt32(SqlHelper.ExecuteScalar(db.Database.Connection.ConnectionString, CommandType.Text, "SELECT COUNT(*) FROM (SELECT MS_HE_THONG AS MS_HT, MS_MAY, MS_HIEN_TRANG FROM dbo.MGetMayUserNgay(GETDATE(), 'admin', '-1', -1, -1, '-1', '-1', '-1', 0) union SELECT DISTINCT NULL AS MS_HT, MS_MAY, MS_HIEN_TRANG FROM dbo.MAY A WHERE A.MS_MAY NOT IN(SELECT MS_MAY FROM MAY_BO_PHAN_CHIU_PHI) OR A.MS_MAY NOT IN(SELECT MS_MAY FROM MAY_HE_THONG) OR A.MS_MAY NOT IN(SELECT MS_MAY FROM MAY_NHA_XUONG) OR A.MS_NHOM_MAY IS NULL OR A.MS_HIEN_TRANG IS NULL OR A.MS_MAY  IN(SELECT MS_MAY FROM MAY_BO_PHAN_CHIU_PHI WHERE MS_BP_CHIU_PHI IS NULL) OR A.MS_MAY  IN(SELECT MS_MAY FROM MAY_HE_THONG  WHERE MS_HE_THONG IS NULL) OR A.MS_MAY  IN(SELECT MS_MAY FROM MAY_NHA_XUONG  WHERE MS_N_XUONG IS NULL))A"));
            return resulst;
        }

        private bool disposed = false;
        protected  void Dispose(bool disposing)
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
