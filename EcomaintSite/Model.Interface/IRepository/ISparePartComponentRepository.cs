using Model.Data;
using System;
using System.Collections.Generic;
namespace Model.Interface
{
    public interface ISparePartComponentRepository : IDisposable
    {
        bool CheckExists(string deviceID, string componentID, string sparePartID);
        IEnumerable<ComponentOfSparePart> ListAll();
        void Add(ComponentOfSparePart sparePart);
        void AddRange(IEnumerable<ComponentOfSparePart> lstSparePartComponent);
        IEnumerable<ComponentOfSparePart> GetSparePartComponentByID(string deviceID, string componentID, string sparePartID);
        void SaveChanges();
    }
}