using Model.Data;
using System;
using System.Collections.Generic;
namespace Model.Interface
{
    public interface IReasonForRepairRepository
    {
        IEnumerable<ReasonForRepair> ListAll();
    }
}
