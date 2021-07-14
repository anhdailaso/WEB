using Model.Data;
using System.Collections.Generic;
namespace Model.Interface
{
    public interface IWorkSiteRepository
    {
        IEnumerable<WorkSite> GetWorkSiteByID(string username, string lang);
    }
}
