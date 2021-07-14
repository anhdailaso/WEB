using Biz.Lib.Helpers;
using Model.Data;
using Model.Interface.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model.Repository.Repository
{
    public class CheckInventory : IMyEcomainRepository, IDisposable
    {
        Model.Data.Model1 db;
        //public CheckInventoryRepository() => db = new Model1();
        public CheckInventory(Model1 context) => db = context;
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

        public IEnumerable<PhieuBaoTriViewModel> GetPhieuBaoTris(string username, int languages, string ms_nx, int ms_ht, string ms_loaimay, string ms_may)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GiamSatTinhTrangViewModel> GetGSTRs(string username, int languages, string tngay, string dngay, string ms_nx, int ms_ht, string ms_loaimay, string ms_may, int loaiCV)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MyEcomainViewModel> GetMyEcomain(string username, int languages, string tngay, string dngay, string ms_nx, string may, int giaidoan)
        {
            throw new NotImplementedException();
        }
    }
}