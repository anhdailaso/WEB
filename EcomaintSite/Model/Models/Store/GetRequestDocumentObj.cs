namespace Model.Data
{
    using System;

    public partial class GetRequestDocumentObj
    {
        public int UserRequestID { get; set; }
        public string DeviceID { get; set; }
        public int UserRequestDetailID { get; set; }
        public int ID { get; set; }
        public string Path { get; set; }
        public string Note { get; set; }
        public string Image { get; set; }
    }
}