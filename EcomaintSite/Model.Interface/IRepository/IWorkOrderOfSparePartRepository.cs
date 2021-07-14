using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IWorkOrderOfSparePartRepository : IDisposable
    {
        void AddRange(IEnumerable<WorkOrderOfWorkIncludeSparePart> lst);
        IEnumerable<WorkOrderOfWorkIncludeSparePart> ListAll();
        WorkOrderOfWorkIncludeSparePart GetWorkOrderOfSparePartByID(string workOrderID, string componentID, int workID, string sparePartID);
        IEnumerable<WorkOrderOfWorkIncludeSparePart> GetWorkOrderOfSparePart(string workOrderID);
        IEnumerable<WorkOrderOfWorkIncludeSparePart> GetWorkOrderOfSparePartByWork(string workOrderID, string componentID, int workID);
        void SaveChanges();
        void Add(WorkOrderOfWorkIncludeSparePart obj);
        void Edit(WorkOrderOfWorkIncludeSparePart obj);
        bool CheckExists(WorkOrderOfWorkIncludeSparePart obj);
        void Delete(string ID);
    }
}
