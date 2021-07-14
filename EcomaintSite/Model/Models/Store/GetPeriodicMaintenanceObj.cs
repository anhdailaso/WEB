using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class GetPeriodicMaintenanceObj
    {
        public string DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string TypeOfMaintenanceName { get; set; }
        public DateTime LastDate { get; set; }
        public DateTime NextDate { get; set; }
        public string RuntimeCycle { get; set; }
        public string CurrentRunTime { get; set; }
        public int TypeOfMaintenanceID { get; set; }
        public int Index { get; set; }
        public string DeviceGroupName { get; set; }
        public double DateProcessed { get; set; }
        public int Runtime { get; set; }
        public short? PriorityID { get; set; }
    }
}
