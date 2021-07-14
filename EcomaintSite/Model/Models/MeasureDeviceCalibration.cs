namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HIEU_CHUAN_DHD")]
    public partial class MeasureDeviceCalibration
    {
        [Key]
        [Column("MS_MAY", Order = 0)]
        [StringLength(30)]
        public string DeviceID { get; set; }

        [Key]
        [Column("MS_PT", Order = 1)]
        [StringLength(25)]
        public string SparePartID { get; set; }

        [Key]
        [Column("MS_VI_TRI", Order = 2)]
        [StringLength(50)]
        public string LocationID { get; set; }

        [Key]
        [Column("DateCompleted", Order = 3)]
        public DateTime DateCreated { get; set; }

        [StringLength(50)]
        [Column("GIAY_CHUNG_NHAN")]
        public string Certificate { get; set; }

        [StringLength(50)]
        [Column("CO_QUAN_HIEU_CHUAN")]
        public string Organization { get; set; }

        [StringLength(255)]
        [Column("DANH_GIA")]
        public string Evaluate { get; set; }

        [Column("NOI")]
        public bool? Internal { get; set; }

        [Column("MS_LOAI_HIEU_CHUAN")]
        public int? TypeOfCalibration { get; set; }

        [Column("NGAY_KH")]
        public DateTime? DatePlan { get; set; }

        [StringLength(250)]
        [Column("DUONG_DAN")]
        public string Path { get; set; }

        [StringLength(250)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [Column("MAC_DINH")]
        public bool? Default { get; set; }

        [StringLength(250)]
        [Column("DD_GOC")]
        public string OriginalPath { get; set; }

        public CalibrationCycle CalibrationCycle { get; set; }
    }
}
