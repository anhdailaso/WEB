using Model.Data;
using System;
using System.Collections.Generic;
namespace Model.Interface
{
    public interface ISparepartAndTypeOfSparepartRepository
    {
        IEnumerable<SparepartAndTypeOfSparepart> ListAll();
        void IsCheckAutomaticInsertSparepartAndTypeOfSparepart(string workOrderID);
    }
}
