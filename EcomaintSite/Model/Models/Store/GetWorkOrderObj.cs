using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class GetWorkOrderObj
    {
        public string Document { get; set; }
        public int? FormationID { get; set; }
        public string TypeOfDeviceID { get; set; }
        public string EquipmentGroupID { get; set; }
        public string WorkOrderID { get; set; }
        public string WorkOrderNo { get; set; }
        public string DeviceID { get; set; }
        public string DeviceName { get; set; }
        public int? TheStatusOfWorkOrderID { get; set; }
        public string TheStatusOfWorkOrderName { get; set; }
        public string TheStatusDetailsOfWorkOrderName { get; set; }
        public int? TypeOfMaintenanceID { get; set; }
        public string TypeOfMaintenanceName { get; set; }
        public string Reason { get; set; }
        public DateTime? DateCreated { get; set; }
        public string WorkSiteName { get; set; }
        public string WorkSiteID { get; set; }
        public string UserRequestID { get; set; }
        public DateTime? TimeCreated { get; set; }
        public int? PriorityID { get; set; }
        public string PriorityName { get; set; }
        public string CreatedBy { get; set; }
        public string UserCreated { get; set; }
        public string StaffName { get; set; }
        public string MonitoredBy { get; set; }
        public DateTime? StartDatePlan { get; set; }
        public DateTime? EndDatePlan { get; set; }
        public DateTime? BreakdownStartTime { get; set; }
        public DateTime? BreakdownStartDate { get; set; }
        public int? CauseID { get; set; }
        public DateTime? BreakdownEndTime { get; set; }
        public DateTime? BreakdownEndDate { get; set; }
        public string IssuedID { get; set; }
        public DateTime? LastDateModified { get; set; }
    }
}