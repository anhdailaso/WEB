using Model.Data;
using System.Collections.Generic;
namespace Model.Interface
{
    public interface IDeviceRepository
    {
        IEnumerable<DeviceObjForDropdown> GetDeviceByRequest(string username, string workSiteID, string requestDetailID);
        IEnumerable<Device> ListAll();
    }
}
