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

        IEnumerable<MyEcomainViewModel> GetMyEcomain(string username, int languages, string tngay, string dngay, string ms_nx,string may, int giaidoan);

    }
}
