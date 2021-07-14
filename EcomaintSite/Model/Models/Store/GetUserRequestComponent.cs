namespace Model.Data
{
    using System;

    public partial class GetUserRequestComponent
    {
        public int UserRequestID { get; set; }
        public string DeviceID { get; set; }
        public int UserRequestDetailID { get; set; }
        public int ID { get; set; }
        public string ComponentID { get; set; }
        public string ComponentName { get; set; }
        public string SparePartID { get; set; }
        public string SparePartName { get; set; }
        public string LocationID { get; set; }
        public Nullable<int> Del { get; set; }
    }
}