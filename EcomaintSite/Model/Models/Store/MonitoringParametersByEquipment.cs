using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Data
{
    public class MonitoringParametersByDevice
    {
        public string DeviceID { get; set; }
        public string MonitoringParamsName { get; set; }
        public string MonitoringParamsID { get; set; }
        public string ComponentID { get; set; }
        public string ComponentName { get; set; }
        public string ValueParamName { get; set; }
        public string Note { get; set; }
        public string MeasurementUnitName { get; set; }
        public bool TypeOfParam { get; set; }
        public double? Measurement { get; set; }
        public int Pass { get; set; }
        public int ValueParamID { get; set; }
        public int ID { get; set; }
        public string Path { get; set; }
        public byte[] ImageGS { get; set; }
    }
    public class MonitoringByDevice
    {
        //với cái này
        public int STT;
        //public int STT
        //{
        //    get { return stt; }
        //    set { stt = value +1; }
        //}
        public string DeviceID { get; set; }
        public string MonitoringParamsName { get; set; }
        public string MonitoringParamsID { get; set; }
        public string ComponentID { get; set; }
        public string ComponentName { get; set; }
        public string MeasurementUnitName { get; set; }
        public bool TypeOfParam { get; set; }
        public string Speak { get; set; }
        public string Message { get; set; }
        public string TEN_GT { get; set; }
    }
}
