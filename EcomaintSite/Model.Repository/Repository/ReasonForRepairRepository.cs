using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class ReasonForRepairRepository : IReasonForRepairRepository, IDisposable
    {
        Model.Data.Model1 db;
        public ReasonForRepairRepository() => db = new Model1();

        public ReasonForRepairRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<ReasonForRepair> ListAll() => db.ReasonForRepair.ToList();
       
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
