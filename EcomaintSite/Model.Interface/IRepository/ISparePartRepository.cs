using Model.Data;
using System;
using System.Collections.Generic;
namespace Model.Interface
{
    public interface ISparePartRepository
    { 
        IEnumerable<Sparepart> ListAll();
        IEnumerable<Sparepart> GetSparePart(string username);
        Sparepart GetSparePartByID(string ID);
    }
}
