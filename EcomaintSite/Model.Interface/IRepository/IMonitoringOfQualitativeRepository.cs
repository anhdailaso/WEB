using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IMonitoringOfQualitativeRepository
    {
        void Add(MonitoringOfQualitative obj);
        void AddRange(IEnumerable<MonitoringOfQualitative> lst);
        void Edit(MonitoringOfQualitative obj);
        void Delete(int ID);
        void SaveChanges();
        IEnumerable<MonitoringOfQualitative> ListAll();
    }
}
