using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class SparePartRepository : ISparePartRepository, IDisposable
    {
        Model.Data.Model1 db;
        public SparePartRepository() => db = new Model1();
        
        public SparePartRepository(Model.Data.Model1 context) => db = context;
        
        public Sparepart GetSparePartByID(string ID) => db.Sparepart.SingleOrDefault(x=>x.ID == ID);        

        public IEnumerable<Sparepart> ListAll() => db.Sparepart.ToList();
        
        public IEnumerable<Sparepart> GetSparePart(string username) =>  
            db.Database.SqlQuery<Sparepart>("GetSparePart @username", new object[]{
                new SqlParameter("@username", username)
            }).ToList();
        

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
