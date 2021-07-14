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
    public class WorkOrderDetailByTheStateRepository : IWorkOrderDetailByTheStateRepository, IDisposable
    {
        Model.Data.Model1 db;
        public WorkOrderDetailByTheStateRepository() => db = new Model.Data.Model1();

        public WorkOrderDetailByTheStateRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<WorkOrderDetailsByTheState> ListAll()
        {
            return db.WorkOrderDetailsByTheState.ToList();
        }

        public void AddRange(IEnumerable<WorkOrderDetailsByTheState> lst) => db.WorkOrderDetailsByTheState.AddRange(lst);

        public void Add(WorkOrderDetailsByTheState obj) => db.WorkOrderDetailsByTheState.Add(obj);

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
