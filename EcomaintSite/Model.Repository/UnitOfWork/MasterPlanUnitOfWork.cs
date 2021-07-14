using Model.Interface;

using Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.UnitOfWork
{
    public class MasterPlanUnitOfWork : IMasterPlanUnitOfWork, IDisposable
    {
        private Model.Data.Model1 db;
        public MasterPlanUnitOfWork() => db = new Model.Data.Model1();        

        private IMasterPlanRepository _masterPlanRepository = null;
        private IMasterPlansOfWorkRepository _masterPlansOfWorkRepository = null;
        private IMasterPlansOfSparePartRepository _masterPlansOfSparePartRepository = null;
        private IWorkRepository _workRepository = null;
        private IPreventiveMaintenanceOfWorkRepository _preventiveMaintenanceOfWorkRepository = null;
        private IPreventiveMaintenanceOfSparePartRepository _preventiveMaintenanceOfSparePartRepository = null;
        private IComponentRepository _componentRepository = null;
        private IWorkComponentRepository _workComponentRepository = null;    

        public IMasterPlanRepository MasterPlanRepository
        {
            get => this._masterPlanRepository ?? new MasterPlanRepository(db);            
        }

        public IMasterPlansOfWorkRepository MasterPlansOfWorkRepository
        {
            get => this._masterPlansOfWorkRepository ?? new MasterPlansOfWorkRepository(db);
        }
        public IMasterPlansOfSparePartRepository MasterPlansOfSparePartRepository
        {
            get => this._masterPlansOfSparePartRepository ?? new MasterPlansOfSparePartRepository(db);
        }
        public IWorkRepository WorkRepository
        {
            get => this._workRepository ?? new WorkRepository(db);
        }

        public IPreventiveMaintenanceOfSparePartRepository PreventiveMaintenanceOfSparePartRepository
        {
            get => this._preventiveMaintenanceOfSparePartRepository ?? new PreventiveMaintenanceOfSparePartRepository(db);
        }

        public IPreventiveMaintenanceOfWorkRepository PreventiveMaintenanceOfWorkRepository
        {
            get => this._preventiveMaintenanceOfWorkRepository ?? new PreventiveMaintenanceOfWorkRepository(db);
        }

        public IComponentRepository ComponentRepository
        {
            get => this._componentRepository ?? new ComponentRepository(db);
        }
        public IWorkComponentRepository WorkComponentRepository
        {
            get => this._workComponentRepository ?? new WorkComponentRepository(db);
        }

        public void Save() => db.SaveChanges();

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
