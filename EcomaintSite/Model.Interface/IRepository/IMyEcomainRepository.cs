using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model.Interface.IRepository
{
    public interface IMyEcomainRepository
    {
        IEnumerable<PhieuBaoTriViewModel> GetPhieuBaoTris(string username, int languages,string ms_nx, int ms_ht,string ms_loaimay,string ms_may);

        IEnumerable<GiamSatTinhTrangViewModel> GetGSTRs(string username, int languages,string tngay,string dngay, string ms_nx, int ms_ht, string ms_loaimay, string ms_may,int loaiCV);

        IEnumerable<MyEcomainViewModel> GetMyEcomain(string username, int languages, string tngay, string dngay, string ms_nx,string may, int giaidoan);

    }
}
