
namespace Model.Interface
{
    public interface IMasterPlanUnitOfWork 
    {
        IMasterPlanRepository MasterPlanRepository { get; }
        IMasterPlansOfWorkRepository MasterPlansOfWorkRepository { get; }
        IMasterPlansOfSparePartRepository MasterPlansOfSparePartRepository { get; }
        IWorkRepository WorkRepository { get; }
        IPreventiveMaintenanceOfWorkRepository PreventiveMaintenanceOfWorkRepository { get; }
        IPreventiveMaintenanceOfSparePartRepository PreventiveMaintenanceOfSparePartRepository { get; }
        IComponentRepository ComponentRepository { get; }
        IWorkComponentRepository WorkComponentRepository { get; }
        void Save();
    }
}
