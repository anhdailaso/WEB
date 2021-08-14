namespace  Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ST_HazardReport
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string DocNum { get; set; }

        public DateTime? DocDate { get; set; }

        public DateTime? DocTime { get; set; }

        public DateTime? Createdtime { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string Department { get; set; }

        public bool? UnsafeCondition { get; set; }

        public bool? BelongContractor { get; set; }

        public bool? Operation { get; set; }

        public bool? HR { get; set; }

        public bool? UnsafeBehevior { get; set; }

        public bool? BelongToBSPort { get; set; }

        public bool? TechHSE { get; set; }

        public bool? Acount { get; set; }

        public bool? SatetySuggestion { get; set; }

        public bool? Tally { get; set; }

        public bool? Procu { get; set; }

        public bool? Other { get; set; }

        public bool? Stopwork { get; set; }

        public bool? Commercial { get; set; }

        public bool? HPES1 { get; set; }

        public bool? HPES2 { get; set; }

        public bool? HPES3 { get; set; }

        public bool? HPES4 { get; set; }

        public bool? HPES5 { get; set; }

        public bool? HPES6 { get; set; }

        [StringLength(250)]
        public string Location { get; set; }

        public string Description { get; set; }

        public bool? External { get; set; }

        public bool? NearMiss { get; set; }

        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        [StringLength(250)]
        public string REPORT_PARENT { get; set; }

        [StringLength(250)]
        public string APPROVAL_USER { get; set; }

        public bool? IS_APPROVED { get; set; }

        public string IMG_PATCH1 { get; set; }

        public string IMG_PATCH2 { get; set; }

        public bool? Environment { get; set; }

        public bool? TT { get; set; }

        public bool? IS_DELETE { get; set; }

        public string COMMENT { get; set; }

        [StringLength(300)]
        public string NGUOILIENQUAN1 { get; set; }

        [StringLength(300)]
        public string NGUOILIENQUAN2 { get; set; }

        [StringLength(300)]
        public string RANDOM { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image_1 { get; set; }
    }
}
