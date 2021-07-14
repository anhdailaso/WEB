using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Data
{
    public partial class GetUserRequestObj
    {
        public int ID { get; set; }
        public string UserRequestID { get; set; }
        public string RequestNO { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime HourCreated { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedName { get; set; }
        public string UserComment { get; set; }
        public Nullable<System.DateTime> DateCompleted { get; set; }
        public Nullable<bool> IsChecked { get; set; }
        public string Username { get; set; }
        public string ReviewComment { get; set; }
        public string CreatedBy { get; set; }
        public string Email { get; set; }
        public string WorkSiteID { get; set; }
        public string WorkSiteName { get; set; }
        public Nullable<bool> IsExecuteApproveRequested { get; set; }
        public string DeviceID { get; set; }
    }
}