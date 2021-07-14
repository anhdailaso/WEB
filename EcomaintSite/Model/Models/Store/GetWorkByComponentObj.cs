using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class GetWorkByComponentObj
    {
        public string DeviceID { get; set; }
        public string ComponentID { get; set; }
        public int WorkID { get; set; }
        public string Description { get; set; }
        public Nullable<double> TimePlan { get; set; }
    }
}
