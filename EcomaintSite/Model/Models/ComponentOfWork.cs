namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CAU_TRUC_THIET_BI_CONG_VIEC")]
    public partial class ComponentOfWork
    {
        [Key]
        [Column("MS_MAY", Order = 0)]
        [StringLength(30)]
        public string DeviceID { get; set; }

        [Key]
        [Column("MS_BO_PHAN", Order = 1)]
        [StringLength(50)]
        public string ID { get; set; }

        [Key]
        [Column("MS_CV", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkID { get; set; }

        [StringLength(250)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [Column("ACTIVE")]
        public bool? Active { get; set; }

        [Column("TG_KH")]
        public double? TimePlan { get; set; }

        [StringLength(2000)]
        [Column("THAO_TAC")]
        public string Action { get; set; }

        [StringLength(2000)]
        [Column("TIEU_CHUAN_KT")]
        public string QualityCheck { get; set; }

        [StringLength(1000)]
        [Column("YEU_CAU_NS")]
        public string InquiryHR { get; set; }

        [StringLength(1000)]
        [Column("YEU_CAU_DUNG_CU")]
        public string Tool { get; set; }

        [StringLength(500)]
        [Column("PATH_HD")]
        public string Path { get; set; }

        public Component Component { get; set; }

        public Work Work { get; set; }
    }
}
