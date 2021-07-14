using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class SparepartAndTypeOfSparepartRepository : ISparepartAndTypeOfSparepartRepository, IDisposable
    {
        Model.Data.Model1 db;
      
        public SparepartAndTypeOfSparepartRepository() => db = new Model1();

        public SparepartAndTypeOfSparepartRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<SparepartAndTypeOfSparepart> ListAll() => db.SparepartAndTypeOfSparepart.ToList();

        public void IsCheckAutomaticInsertSparepartAndTypeOfSparepart(string workOrderID) => 
            db.Database.ExecuteSqlCommand("IsCheckAutomaticInsertSparepartAndTypeOfSparepart @workOrderID ", new object[]
            {
                new SqlParameter("@workOrderID", workOrderID)
            });
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
