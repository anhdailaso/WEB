using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Repository
{
    public class PriorityRepository : IPriorityRepository, IDisposable
    {
        Model.Data.Model1 db;
        public PriorityRepository() => db = new Model1(); 

        public PriorityRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<Priority> ListAll() => db.Priority.ToList();
    
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
