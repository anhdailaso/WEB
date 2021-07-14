namespace Model.Interface
{
    public interface IMonitoringUnitOfWork
    {
        IMonitoringRepository MonitoringRepository { get; }
        IMonitoringOfQuantitativeRepository MonitoringOfQuantitativeRepository { get; }
        IMonitoringOfQualitativeRepository MonitoringOfQualitativeRepository { get; }
        void Save();
    }
}
