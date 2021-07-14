using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class WorkOrderByTheStatusRepository : IWorkOrderByTheStatusRepository, IDisposable
    {
        Model.Data.Model1 db;
        public WorkOrderByTheStatusRepository() => db = new Model.Data.Model1();

        public WorkOrderByTheStatusRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<TheStatusOfWorkOrder> ListAll() => db.TheStatusOfWorkOrder.ToList();

        public TheStatusOfWorkOrder GetTheStatusOfWorkOrderByID(int ID) => db.TheStatusOfWorkOrder.SingleOrDefault(x => x.ID == ID);

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
