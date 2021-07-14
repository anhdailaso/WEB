using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IWorkRepository : IDisposable
    {
        IEnumerable<Work> ListAll();
        Work GetWorkByID(int workID);
        void SaveChanges();
    } 
}
