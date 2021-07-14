using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class ReportRepository : IDisposable
    {
        Model.Model1 db;
        public ReportRepository()
        {
            db = new Model1();
        }

        public IEnumerable<spBCGioCongNhanVien_Result> spBCGioCongNhanVien(string unit, string phongBan, string Username, Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate )
        {
            object[] parameter =
             {
                new SqlParameter("@MS_DV", unit),
                new SqlParameter("@ID", phongBan),
                new SqlParameter("@username", Username),
                new SqlParameter("@TNgay", fromDate),
                new SqlParameter("@DNgay", toDate)
            };
            return db.Database.SqlQuery<spBCGioCongNhanVien_Result>("spBCGioCongNhanVien @MS_DV, @ID, @username, @TNgay, @DNgay", parameter).ToList();
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
