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
    public class WorkOrderIncludeDetailSparePartRepository : IWorkOrderIncludeDetailSparePartRepository, IDisposable
    {
        Model.Data.Model1 db;
        public WorkOrderIncludeDetailSparePartRepository() => db = new Model.Data.Model1();

        public WorkOrderIncludeDetailSparePartRepository(Model.Data.Model1 context) => db = context;
        
        public void Add(WorkOrderOfWorkIncludeDetailSparePart obj) => db.WorkOrderOfWorkIncludeDetailSparePart.Add(obj);

        public void AddRange(IEnumerable<WorkOrderOfWorkIncludeDetailSparePart> lst) => db.WorkOrderOfWorkIncludeDetailSparePart.AddRange(lst); 

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

        public void Delete(string ID) => db.WorkOrderOfWorkIncludeDetailSparePart.RemoveRange(db.WorkOrderOfWorkIncludeDetailSparePart.Where(x => x.WorkOrderID == ID).ToList());
    }
}
