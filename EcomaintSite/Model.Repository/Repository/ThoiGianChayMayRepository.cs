using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Model.Repository
{
    public class ThoiGianChayMayRepository : IThoiGianChayMayRepository, IDisposable
    {
        Model.Data.Model1 db;
        public ThoiGianChayMayRepository() => db = new Model.Data.Model1();
        public ThoiGianChayMayRepository(Model.Data.Model1 context) => db = context;
        public IEnumerable<THOI_GIAN_CHAY_MAY> GetThoiGianChayMayInfo(string MS_MAY)
        {
            List<THOI_GIAN_CHAY_MAY> list = null;
            list = db.THOI_GIAN_CHAY_MAY.Where(x=>x.MS_MAY == MS_MAY && x.NGAY.Year == DateTime.Now.Year).OrderByDescending(x=>x.NGAY).ToList();
            return list;
        }

        public double? SoGioLuyKeHienTai(string msmay)
        {
            Double? resulst = null;
            try
            {
                DateTime today = DateTime.Now;
                resulst = Convert.ToDouble(db.THOI_GIAN_CHAY_MAY.Where(x => x.NGAY.Year == today.Year && x.NGAY.Month == today.Month && x.NGAY.Day == today.Day && x.MS_MAY == msmay).Select(x => x.SO_GIO_LUY_KE).FirstOrDefault());
                return resulst;
            }
            catch
            {
                resulst = null;
            }
            return resulst;
        }
        public THOI_GIAN_CHAY_MAY ThoiGianLuyKeTruoc(string msmay)
        {
            try
            {
                return db.THOI_GIAN_CHAY_MAY.Where(x => DbFunctions.TruncateTime(x.NGAY) < DbFunctions.TruncateTime(DateTime.Now) && x.MS_MAY == msmay).OrderByDescending(x => x.NGAY).First();
            }
            catch
            {
                return null;
            }
        }
        public void Add(THOI_GIAN_CHAY_MAY obj)
        {
            try
            {
                THOI_GIAN_CHAY_MAY tgcm = db.THOI_GIAN_CHAY_MAY.Where(x => x.MS_MAY == obj.MS_MAY && DbFunctions.TruncateTime(x.NGAY) == DbFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();
                if (tgcm != null)
                {
                 db.Database.ExecuteSqlCommand("update dbo.THOI_GIAN_CHAY_MAY SET CHI_SO_DONG_HO = @CHI_SO_DONG_HO, SO_GIO_LUY_KE = @SO_GIO_LUY_KE WHERE MS_MAY = @MS_MAY AND YEAR(NGAY) = @NAM AND MONTH(NGAY) =@THANG AND DAY(NGAY) =@NGAY",
                 new object[] {
                new SqlParameter("@CHI_SO_DONG_HO", obj.CHI_SO_DONG_HO),
                new SqlParameter("@SO_GIO_LUY_KE", obj.SO_GIO_LUY_KE),
                new SqlParameter("@MS_MAY", obj.MS_MAY),
                new SqlParameter("@NAM", obj.NGAY.Year),
                new SqlParameter("@THANG", obj.NGAY.Month),
                new SqlParameter("@NGAY", obj.NGAY.Day)
            });

                }
                else
                {
                    db.THOI_GIAN_CHAY_MAY.Add(obj);
                    db.SaveChanges();
                }
            }
            catch
            {

            }
        }
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
