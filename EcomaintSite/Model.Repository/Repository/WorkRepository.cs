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
    public class WorkRepository : IWorkRepository, IDisposable
    {
        Model.Data.Model1 db;
        public WorkRepository() => db = new Model.Data.Model1();

        public WorkRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<Work> ListAll() => db.Work.ToList();

        public Work GetWorkByID(int workID) => db.Work.SingleOrDefault(x => x.ID == workID);

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
