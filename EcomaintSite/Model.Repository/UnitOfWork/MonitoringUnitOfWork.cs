using Model.Interface;
using Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.UnitOfWork
{
    
    public class MonitoringUnitOfWork : IMonitoringUnitOfWork, IDisposable
    {
        private Model.Data.Model1 db;
        public MonitoringUnitOfWork() => db = new Model.Data.Model1();
        
        private IMonitoringRepository _monitoringRepository = null;
        private IMonitoringOfQuantitativeRepository _monitoringOfQuantitativeRepository = null;
        private IMonitoringOfQualitativeRepository _monitoringOfQualitativeRepository = null;

        public IMonitoringRepository MonitoringRepository
        {
            get => this._monitoringRepository ?? new MonitoringRepository(db);
        }

        public IMonitoringOfQuantitativeRepository MonitoringOfQuantitativeRepository
        {
            get => this._monitoringOfQuantitativeRepository ?? new MonitoringOfQuantitativeRepository(db);
        }
        public IMonitoringOfQualitativeRepository MonitoringOfQualitativeRepository
        {
            get => this._monitoringOfQualitativeRepository ?? new MonitoringOfQualitativeRepository(db);
        }

        public void Save() => db.SaveChanges();


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
