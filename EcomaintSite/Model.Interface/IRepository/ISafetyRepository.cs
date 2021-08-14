using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model.Interface.IRepository
{
    public interface ISafetyRepository
    {

        IEnumerable<ChooseListHazard> ChooseListHazard(DateTime tngay, DateTime dngay);
        HazardReportViewModel GetListByID(string ID);
        string GetSoPhieuHazard();
    }
}
