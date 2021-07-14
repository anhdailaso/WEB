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
    public class TheStatusDetailsOfWorkOrderRepository : ITheStatusDetailsOfWorkOrderRepository, IDisposable
    {
        Model.Data.Model1 db;
        public TheStatusDetailsOfWorkOrderRepository() => db = new Model.Data.Model1();

        public TheStatusDetailsOfWorkOrderRepository(Model.Data.Model1 context) => db = context;
        

        public IEnumerable<TheStatusDetailsOfWorkOrder> ListAll() => db.TheStatusDetailsOfWorkOrder.ToList();

        public TheStatusDetailsOfWorkOrder GetTheStatusDetailsOfWorkOrderByID(int theStatusOfWorkOrder) => 
            db.TheStatusDetailsOfWorkOrder.Where(x => x.TheStatusOfWorkOrderID == theStatusOfWorkOrder && x.Default == true).Count() > 0 ? db.TheStatusDetailsOfWorkOrder.SingleOrDefault(x => x.TheStatusOfWorkOrderID == theStatusOfWorkOrder && x.Default == true) : db.TheStatusDetailsOfWorkOrder.Where(x => x.TheStatusOfWorkOrderID == theStatusOfWorkOrder).Take(1).SingleOrDefault();

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
