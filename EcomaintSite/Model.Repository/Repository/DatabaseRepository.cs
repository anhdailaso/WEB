using Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Model.Data;

namespace Model.Repository
{
   public class DatabaseRepository : IDatabaseRepository, IDisposable
    {
        Model.Data.Model1 db;
        public DatabaseRepository() => db = new Model1();
        
        public IEnumerable<DataBase> GetDatabase() =>
            db.Database.SqlQuery<DataBase>("select name DatabaseName from master.sys.databases WHERE left(name, 4) = 'CMMS'").ToList();

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
