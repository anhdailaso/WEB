using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interface.IRepository
{
    public interface IPurchaseRequestRepository
    {
        IEnumerable<PhieuBaoTriViewModel> GetPhieuBaoTris(string username, int languages, string ms_nx, int ms_ht, string ms_loaimay, string ms_may);
    }
}
