using System;

namespace Model.Data
{
    public partial class ApprovedRequestObj
    {
        public Nullable<bool> Choose { get; set; }
        public byte[] Document { get; set; }
        public int ID { get; set; }
        public string DeviceID { get; set; }
        public int UserRequestDetailID { get; set; }
        public string DeviceName { get; set; }
        public string Description { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string UserRequestID { get; set; }
        public string RequestNO { get; set; }
        public string WorkSiteName { get; set; }
        public string RequestedBy { get; set; }
        public string Request { get; set; }
        public Nullable<int> PriorityID { get; set; }
        public string ApprovedRequestBy { get; set; }
        public Nullable<System.DateTime> DateApproveRequested { get; set; }
        public string ApprovalNote { get; set; }
        public Nullable<bool> IsExecuteApproveRequested { get; set; }
        public string EmailByApproveRequested { get; set; }
        public string ExecuteID { get; set; }
        public string PriorityName { get; set; }
        public string TypeOfMaintenanceName { get; set; }
        public Nullable<bool> IsApprovedRequest { get; set; }
    }
}