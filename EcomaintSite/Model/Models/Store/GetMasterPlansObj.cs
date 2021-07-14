using System;

namespace Model.Data
{
    public class GetMasterPlansObj
    {
        public string DeviceID { get; set; }
        public string DeviceName { get; set; }
        public int ID { get; set; }
        public string PlanName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateCompleted { get; set; }
        public string Note { get; set; }
        public int? TypeOfMaintenanceID { get; set; }
        public string Username { get; set; }
        public int PriorityID { get; set; }
        public string StaffID { get; set; }
        public string ConfirmedBy { get; set; }
        public double TotalHours { get; set; }
        public int ReasonForRepairID { get; set; }
        public string ReasonForRepairName { get; set; }
        public string PriorityName { get; set; }
        public string TypeOfMaintenanceName { get; set; }
    }
}
