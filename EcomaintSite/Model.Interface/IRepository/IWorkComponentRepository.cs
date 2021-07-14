using System;
using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IWorkComponentRepository : IDisposable
    {
        IEnumerable<ComponentOfWork> ListAll();
        IEnumerable<GetWorkByComponentObj> GetWorkByComponent(string deviceID, string componentID, int lang);
        ComponentOfWork GetWorkComponentByID(string deviceID, string componentID, int workID);
        void SaveChanges();
    }
}
