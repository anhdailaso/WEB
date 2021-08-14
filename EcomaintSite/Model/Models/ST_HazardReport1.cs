namespace  Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ST_HazardReport1
    {
        public int ID { get; set; }

        public int? HazardReportID { get; set; }

        [StringLength(250)]
        public string PreventiveAction { get; set; }

        [StringLength(250)]
        public string PersonIncharge { get; set; }

        public DateTime? Deadline { get; set; }

        public DateTime? CompletedDate { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        [StringLength(100)]
        public string PersonReport { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public int? MUCUUTIEN { get; set; }

        [StringLength(300)]
        public string THIETBI { get; set; }

        public int? LOAIYEUCAU { get; set; }

        public int? NGUYENNHAN { get; set; }
    }
}
