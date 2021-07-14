using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IMonitoringOfQuantitativeRepository
    {
        IEnumerable<MonitoringOfQuantitative> ListAll();
        void AddRange(IEnumerable<MonitoringOfQuantitative> lst);
        void Add(MonitoringOfQuantitative obj);
        bool CheckExist(MonitoringOfQuantitative obj);
        void Edit(MonitoringOfQuantitative obj);
        void Delete(int monitoringID);
        void SaveChanges();
    }
}
