using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Data
{
    public class PhuTungViewModel
    {
        public int STT { get; set; }
        public string MS_MAY { get; set; }

        public int STT_VAN_DE { get; set; }
        public List<BoPhan> ListBoPhan { get; set; }
        public List<PhuTung> ListPhuTung { get; set; }
        public class BoPhan
        {
            public bool Choose { get; set; }
            public string MS_MAY { get; set; }
            public string MS_BO_PHAN { get; set; }
            public string MS_BO_PHAN_CHA { get; set; }
            public string TEN_BO_PHAN { get; set; }
        }
        public class PhuTung
        {
            public bool Choose { get; set; }
            public string MS_MAY { get; set; }
            public string MS_BO_PHAN { get; set; }
            public string TEN_BO_PHAN { get; set; }
            public string MS_PT { get; set; }
            public string MS_VI_TRI_PT { get; set; }
            public string TEN_PT { get; set; }
            public int STT_VAN_DE { get; set; }
        }
    }
}
