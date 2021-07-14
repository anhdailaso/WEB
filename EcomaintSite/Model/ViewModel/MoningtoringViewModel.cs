using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Data
{
    public class MoningtoringViewModel
    {
        public int STT { get; set; }
        public string SO_PHIEU { get; set; }
        public DateTime NGAY_KT { get; set; }
        public DateTime GIO_KT { get; set; }
        public string HO_TEN { get; set; }
        public string MS_CONG_NHAN { get; set; }
        public string MS_MAY { get; set; }
        public string TEN_MAY { get; set; }
        public string NHAN_XET { get; set; }
        public List<ThongSoGiamSat> DSThongSoGiamSat { get; set; }

        public class ThongSoGiamSat
        {
            public int STT { get; set; }
            public string MS_MAY { get; set; }
            public string TEN_MAY { get; set; }
            public string MS_BO_PHAN { get; set; }
            public string TEN_BO_PHAN { get; set; }
            public string MS_TS_GSTT { get; set; }
            public string TEN_TS_GSTT { get; set; }
            public string TG_TT { get; set; }
            public string THOI_GIAN { get; set; }
            public double GIA_TRI_DO { get; set; }
            public string TEN_GT { get; set; }
            public string TEN_DV_DO { get; set; }
            public int MS_TT { get; set; }
            public string CT_CVIEC { get; set; }
            public List<GiaTri> DSGiaTri { get; set; }

        public class GiaTri
            {
                public int STT { get; set; }
                public string MS_MAY { get; set; }
                public string MS_BO_PHAN { get; set; }
                public string MS_TS_GSTT { get; set; }
                public int STT_GT { get; set; }
                public string TEN_GIA_TRI { get; set; }
                public string GHI_CHU { get; set; }
            }
            public class GiaTriDL
            {
                public string TEN_GT { get; set; }
                public double GIA_TRI_DUOI { get; set; }
                public double GIA_TRI_TREN { get; set; }
            }
        }
    }
}
