using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IWorkOrderIncludeDetailSparePartRepository : IDisposable
    {
        void Add(WorkOrderOfWorkIncludeDetailSparePart obj);
        void AddRange(IEnumerable<WorkOrderOfWorkIncludeDetailSparePart> lst);
        void SaveChanges();
        void Delete(string ID);
    }
}
