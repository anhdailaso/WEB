using System.Collections.Generic;
using Model.Data;
namespace Model.Interface
{
    public interface IComponentRepository
    {
        IEnumerable<Component> ListAll();
        Component GetComponentByID(string deviceID, string componentID);
        IEnumerable<Component> GetComponentByDevice(string deviceID, int lang);
        void SaveChanges();
    }
}
