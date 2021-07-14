using Model.Data;
using Model.Interface;
using System;
using System.Linq;

namespace Model.Repository
{
    public class GeneralRepository: IGeneralRepository, IDisposable
    {
        Model.Data.Model1 db;
        public GeneralRepository() => db = new Model.Data.Model1();
        public GeneralRepository(Model.Data.Model1 context) => db = context;
        public General GetGeneralInfo() => db.General.Take(1).SingleOrDefault();

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
