using Model.Data;
using Model.Interface;
using System;
using System.Data.Entity;
using System.Linq;

namespace Model.Repository
{
    public class ThoiGianChayMayRepository: IThoiGianChayMayRepository, IDisposable
    {
        Model.Data.Model1 db;
        public ThoiGianChayMayRepository() => db = new Model.Data.Model1();
        public ThoiGianChayMayRepository(Model.Data.Model1 context) => db = context;
        public THOI_GIAN_CHAY_MAY GetThoiGianChayMayInfo() => db.THOI_GIAN_CHAY_MAY.Take(1).SingleOrDefault();

        public double? SoGioLuyKeHienTai(string msmay)
        {
            Double? resulst = null;
            try
            {
                resulst = Convert.ToDouble(db.THOI_GIAN_CHAY_MAY.Where(x => x.NGAY.Date == DateTime.Now.Date && x.MS_MAY == msmay).Select(x => x.SO_GIO_LUY_KE));
            }
            catch (Exception)
            {
                resulst = null;
            }
            return resulst;
        }
        public THOI_GIAN_CHAY_MAY ThoiGianLuyKeTruoc(string msmay)
        {
            try
            {
                return db.THOI_GIAN_CHAY_MAY.Where(x => DbFunctions.TruncateTime(x.NGAY) < DbFunctions.TruncateTime(DateTime.Now) && x.MS_MAY == msmay).OrderByDescending(x=>x.NGAY).First();
            }
            catch 
            {
                return null;
            }
        }
        public void Add(THOI_GIAN_CHAY_MAY obj)
        {
            db.THOI_GIAN_CHAY_MAY.Remove(db.THOI_GIAN_CHAY_MAY.Where(x=>x.MS_MAY == obj.MS_MAY && DbFunctions.TruncateTime(x.NGAY) == DbFunctions.TruncateTime(DateTime.Now)).FirstOrDefault());
            db.THOI_GIAN_CHAY_MAY.Add(obj);
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
