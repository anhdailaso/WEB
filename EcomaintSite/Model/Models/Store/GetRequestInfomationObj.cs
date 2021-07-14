namespace Model.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class GetRequestInfomationObj
    {
        public string DeviceID { get; set; }
        public string DeviceName { get; set; }
        public Nullable<int> TypeOfMaintenanceRequestID { get; set; }
        public string Description { get; set; }
        public Nullable<int> CauseID { get; set; }


        public string Request { get; set; }
        public Nullable<System.DateTime> DateOccurred { get; set; }
        public Nullable<System.DateTime> HourOccurred { get; set; }
        public Nullable<int> PriorityID { get; set; }
        public int ID { get; set; }
        public int UserRequestDetailID { get; set; }
        public string OldDeviceID { get; set; }
        public int OldUserRequestDetailID { get; set; }
        public Nullable<bool> IsExecuteApproveRequested { get; set; }

        public string TEN_NGUYEN_NHAN { get; set; }
        public string TEN_UU_TIEN { get; set; }
        public string TEN_LOAI_YEU_CAU_BT { get; set; }
    }
}