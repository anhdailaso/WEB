using Model.Interface;
using Model.Repository;
using Model.UnitOfWork;
using Ninject;
using System;
using System.Collections.Generic;
namespace EcomaintSite
{
    public class NinjectResolver: System.Web.Mvc.IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
         }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IOrgUnitRepository>().To<OrgUnitRepository>();
            _kernel.Bind<IUserRepository>().To<UserRepository>();
            _kernel.Bind<IDatabaseRepository>().To<DatabaseRepository>();
            _kernel.Bind<IWebUserLoginRepository>().To<WebUserLoginRepository>();
            _kernel.Bind<IUserRequestDetailRepository>().To<UserRequestDetailRepository>();
            _kernel.Bind<IPriorityRepository>().To<PriorityRepository>();
            _kernel.Bind<IChartRepository>().To<ChartRepository>();
            _kernel.Bind<ILanguagesRepository>().To<LanguagesRepository>();
            _kernel.Bind<IMasterPlanUnitOfWork>().To<MasterPlanUnitOfWork>();
            _kernel.Bind<IMonitoringUnitOfWork>().To<MonitoringUnitOfWork>();
            _kernel.Bind<IUserRequestUnitOfWork>().To<UserRequestUnitOfWork>();
            _kernel.Bind<IWorkOrderUnitOfWork>().To<WorkOrderUnitOfWork>();
            _kernel.Bind<IStaffRepository>().To<StaffRepository>();
            _kernel.Bind<IDeviceRepository>().To<DeviceRepository>();
            _kernel.Bind<IReasonForRepairRepository>().To<ReasonForRepairRepository>();
            _kernel.Bind<IMasterPlanRepository>().To<MasterPlanRepository>();
            _kernel.Bind<ITypeOfMaintenanceRepository>().To<TypeOfMaintenanceRepository>();
            _kernel.Bind<IWorkComponentRepository>().To<WorkComponentRepository>();
            _kernel.Bind<IComponentRepository>().To<ComponentRepository>();
            _kernel.Bind<ISparePartRepository>().To<SparePartRepository>();
            _kernel.Bind<IMasterPlansOfSparePartRepository>().To<MasterPlansOfSparePartRepository>();
            _kernel.Bind<IMasterPlansOfWorkRepository>().To<MasterPlansOfWorkRepository>();
            _kernel.Bind<IMenuRepository>().To<MenuRepository>();
            _kernel.Bind<IMonitoringRepository>().To<MonitoringRepository>();
            _kernel.Bind<IPeriodicMaintenanceRepository>().To<PeriodicMaintenanceRepository>();
            _kernel.Bind<IWorkSiteRepository>().To<WorkSiteRepository>();
            _kernel.Bind<IUserRequestRepository>().To<UserRequestRepository>();
            _kernel.Bind<IUserRequestComponentRepository>().To<UserRequestComponentRepository>();        
            _kernel.Bind<IUserRequestDocumentRepository>().To<UserRequestDocumentRepository>();
            _kernel.Bind<IGeneralRepository>().To<GeneralRepository>();
            _kernel.Bind<IThoiGianChayMayRepository>().To<ThoiGianChayMayRepository>();
            _kernel.Bind<IWorkOrderRepository>().To<WorkOrderRepository>();
            _kernel.Bind<IWorkOrderOfSparePartRepository>().To<WorkOrderOfSparePartRepository>();
            _kernel.Bind<IWorkRepository>().To<WorkRepository>();
            _kernel.Bind<IWorkOrderOfWorkRepository>().To<WorkOrderOfWorkRepository>();
        }
    }    
}