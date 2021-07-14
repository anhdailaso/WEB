using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface ITheStatusDetailsOfWorkOrderRepository
    {
        IEnumerable<TheStatusDetailsOfWorkOrder> ListAll();
        TheStatusDetailsOfWorkOrder GetTheStatusDetailsOfWorkOrderByID(int theStatusOfWorkOrder);
        void SaveChanges();
    }
}
