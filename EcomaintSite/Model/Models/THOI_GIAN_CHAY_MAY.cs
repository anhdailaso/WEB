namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("THOI_GIAN_CHAY_MAY")]
    public partial class THOI_GIAN_CHAY_MAY
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string MS_MAY { get; set; }

        [Key]
        [Column(Order = 1)]
        //[DisplayFormat(DataFormatString = "{0:yyyyMMdd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime NGAY { get; set; }

        public double? CHI_SO_DONG_HO { get; set; }

        [StringLength(20)]
        public string MS_PBT { get; set; }

        public int? SO_MOVEMENT { get; set; }

        public double? SO_GIO_LUY_KE { get; set; }

        [StringLength(50)]
        public string USERNAME { get; set; }
    }
}
