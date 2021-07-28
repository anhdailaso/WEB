using Model.Interface;
using Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.UnitOfWork
{
    public class WorkOrderUnitOfWork : IWorkOrderUnitOfWork, IDisposable
    {
        private Model.Data.Model1 db;
        public WorkOrderUnitOfWork()
        {
            db = new Model.Data.Model1();
            db.Configuration.AutoDetectChangesEnabled = false;
        }

        private IWorkOrderRepository _workOrderRepository = null;
        private IWorkOrderOfWorkRepository _workOrderOfWorkRepository = null;
        private IWorkOrderOfSparePartRepository _workOrderOfSparePartRepository = null;
        private IWorkOrderIncludeDetailSparePartRepository _workOrderIncludeDetailSparePartRepository = null;               
        private IWorkOrderDetailByTheStateRepository _workOrderDetailByTheStateRepository = null;
        private IWorkRepository _workRepository = null;
        private IPreventiveMaintenanceOfWorkRepository _preventiveMaintenanceOfWorkRepository = null;
        private IPreventiveMaintenanceOfSparePartRepository _preventiveMaintenanceOfSparePartRepository = null;
        private IComponentRepository _componentRepository = null;
        private IWorkComponentRepository _workComponentRepository = null;
        private ISparePartComponentRepository _sparePartComponentRepository = null;
        private IWorkOrderByTheStatusRepository _workOrderByTheStatusRepository = null;
        private ITheStatusDetailsOfWorkOrderRepository _theStatusDetailsOfWorkOrderRepository = null;
        private IPriorityRepository _priorityRepository = null;
        private IGeneralRepository _generalRepository = null;
        IThoiGianChayMayRepository _thoigianchaymayRepository = null;
        private IMasterPlanRepository _masterPlanRepository = null;
        private IMasterPlansOfWorkRepository _masterPlansOfWorkRepository = null;
        private IMasterPlansOfSparePartRepository _masterPlansOfSparePartRepository = null;
        private IMonitoringOfQuantitativeRepository _monitoringOfQuantitativeRepository = null;
        private IUserRequestDetailRepository _userRequestDetailRepository = null;
        private ITypeOfMaterialRepository _typeOfMaterialRepository = null;
        private ISparePartRepository _sparePartRepository = null;
        private ISparepartAndTypeOfSparepartRepository _sparepartAndTypeOfSparepartRepository = null;
        private ITypeOfMaintenanceRepository _typeOfMaintenanceRepository = null;
        private ITheFormalityOfMaintenanceRepository _theFormalityOfMaintenanceRepository = null;

        public ITheFormalityOfMaintenanceRepository TheFormalityOfMaintenanceRepository
        {
            get => this._theFormalityOfMaintenanceRepository ?? new TheFormalityOfMaintenanceRepository(db);
        }

        public ITypeOfMaintenanceRepository TypeOfMaintenanceRepository
        {
            get => this._typeOfMaintenanceRepository ?? new TypeOfMaintenanceRepository(db);          
        }
        public ISparepartAndTypeOfSparepartRepository SparepartAndTypeOfSparepartRepository
        {
            get => this._sparepartAndTypeOfSparepartRepository ?? new SparepartAndTypeOfSparepartRepository(db);          
        }

        public ITypeOfMaterialRepository TypeOfMaterialRepository
        {
            get => this._typeOfMaterialRepository ?? new TypeOfMaterialRepository(db);           
        }

        public ISparePartRepository SparePartRepository
        {
            get => this._sparePartRepository ?? new SparePartRepository(db);           
        }

        public IUserRequestDetailRepository UserRequestDetailRepository
        {
            get => this._userRequestDetailRepository ?? new UserRequestDetailRepository(db);            
        }

        public IMonitoringOfQuantitativeRepository MonitoringOfQuantitativeRepository
        {
            get => this._monitoringOfQuantitativeRepository ?? new MonitoringOfQuantitativeRepository(db);            
        }

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

        public ISparePartComponentRepository SparePartComponentRepository
        {
            get => this._sparePartComponentRepository ?? new SparePartComponentRepository(db);            
        }

        public IWorkOrderIncludeDetailSparePartRepository WorkOrderIncludeDetailSparePartRepository
        {
            get => this._workOrderIncludeDetailSparePartRepository ?? new WorkOrderIncludeDetailSparePartRepository(db);            
        }

        public IGeneralRepository GeneralRepository
        {
            get => this._generalRepository ?? new GeneralRepository(db);            
        }
        public IThoiGianChayMayRepository ThoiGianChayMayRepository
        {
            get => this._thoigianchaymayRepository ?? new ThoiGianChayMayRepository(db);
        }

        public IWorkOrderDetailByTheStateRepository WorkOrderDetailByTheStateRepository
        {
            get => this._workOrderDetailByTheStateRepository ?? new WorkOrderDetailByTheStateRepository(db);            
        }

        public IPriorityRepository PriorityRepository
        {
            get => this._priorityRepository ?? new PriorityRepository(db);            
        }

        public IWorkOrderByTheStatusRepository WorkOrderByTheStatusRepository
        {
            get => this._workOrderByTheStatusRepository ?? new WorkOrderByTheStatusRepository(db);            
        }

        public ITheStatusDetailsOfWorkOrderRepository TheStatusDetailsOfWorkOrderRepository
        {
            get => this._theStatusDetailsOfWorkOrderRepository ?? new TheStatusDetailsOfWorkOrderRepository(db);            
        }

        public IWorkOrderRepository WorkOrderRepository
        {
            get => this._workOrderRepository ?? new WorkOrderRepository(db);            
        }

        public IWorkOrderOfWorkRepository WorkOrderOfWorkRepository
        {
            get => this._workOrderOfWorkRepository ?? new WorkOrderOfWorkRepository(db);            
        }

        public IWorkOrderOfSparePartRepository WorkOrderOfSparePartRepository
        {
            get => this._workOrderOfSparePartRepository ?? new WorkOrderOfSparePartRepository(db);            
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

        public void Save()
        {
            try
            {
                db.SaveChanges();
                db.Configuration.AutoDetectChangesEnabled = true;

            }
            catch (Exception ex)
            {

            }
        }

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
