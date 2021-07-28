namespace Model.Interface
{
    public interface IWorkOrderUnitOfWork
    {
        IWorkOrderRepository WorkOrderRepository { get; }
        IWorkOrderOfWorkRepository WorkOrderOfWorkRepository { get; }
        IWorkOrderOfSparePartRepository WorkOrderOfSparePartRepository { get; }
        IWorkOrderIncludeDetailSparePartRepository WorkOrderIncludeDetailSparePartRepository { get; }               
        IWorkOrderDetailByTheStateRepository WorkOrderDetailByTheStateRepository { get; }
        IWorkRepository WorkRepository { get; }
        IPreventiveMaintenanceOfWorkRepository PreventiveMaintenanceOfWorkRepository { get; }
        IPreventiveMaintenanceOfSparePartRepository PreventiveMaintenanceOfSparePartRepository { get; }
        IComponentRepository ComponentRepository { get; }
        IWorkComponentRepository WorkComponentRepository { get; }
        ISparePartComponentRepository SparePartComponentRepository { get; }
        IWorkOrderByTheStatusRepository WorkOrderByTheStatusRepository { get; }
        ITheStatusDetailsOfWorkOrderRepository TheStatusDetailsOfWorkOrderRepository { get; }
        IPriorityRepository PriorityRepository { get; }
        IGeneralRepository GeneralRepository { get; }
        IThoiGianChayMayRepository ThoiGianChayMayRepository { get; }
        IMasterPlanRepository MasterPlanRepository { get; }
        IMasterPlansOfWorkRepository MasterPlansOfWorkRepository { get; }
        IMasterPlansOfSparePartRepository MasterPlansOfSparePartRepository { get; }
        IMonitoringOfQuantitativeRepository MonitoringOfQuantitativeRepository { get; }
        IUserRequestDetailRepository UserRequestDetailRepository { get; }
        ITypeOfMaterialRepository TypeOfMaterialRepository { get; }
        ISparePartRepository SparePartRepository { get; }
        ISparepartAndTypeOfSparepartRepository SparepartAndTypeOfSparepartRepository { get; }
        ITypeOfMaintenanceRepository TypeOfMaintenanceRepository { get; }
        ITheFormalityOfMaintenanceRepository TheFormalityOfMaintenanceRepository { get; }
        void Save();
    }
}
