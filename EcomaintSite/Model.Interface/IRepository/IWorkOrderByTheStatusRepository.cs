using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IWorkOrderByTheStatusRepository : IDisposable
    {
        IEnumerable<TheStatusOfWorkOrder> ListAll();
        TheStatusOfWorkOrder GetTheStatusOfWorkOrderByID(int ID);
        void SaveChanges();     
    }
}
