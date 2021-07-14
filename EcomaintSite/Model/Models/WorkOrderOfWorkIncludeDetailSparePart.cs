namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEU_BAO_TRI_CONG_VIEC_PHU_TUNG_CHI_TIET")]
    public partial class WorkOrderOfWorkIncludeDetailSparePart
    {
        [Key]
        [Column("MS_PHIEU_BAO_TRI", Order = 0)]
        [StringLength(20)]
        public string WorkOrderID { get; set; }

        [Key]
        [Column("MS_CV", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkID { get; set; }

        [Key]
        [Column("MS_BO_PHAN", Order = 2)]
        [StringLength(50)]
        public string ComponentID { get; set; }

        [Key]
        [Column("MS_PT", Order = 3)]
        [StringLength(25)]
        public string SparePartID { get; set; }

        [Key]
        [Column("STT", Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(30)]
        [Column("MS_VI_TRI_PT")]
        public string LocationID { get; set; }

        [Column("SL_KH")]
        public double? PlanQuantity { get; set; }

        [Column("SL_TT")]
        public double? ActualQuantity { get; set; }

        [Column("VICT_NHA_THAU")]
        public bool? IsCompanyOrSupplier { get; set; }

        [StringLength(80)]
        [Column("NGUOI_THAY_THE")]
        public string ReplacedBy { get; set; }

        [Column("NGAY_THAY_THE")]
        public DateTime? DateReplaced { get; set; }

        [Column("THAY_SUA")]
        public bool? IsReplaceOrRepair { get; set; }

        [StringLength(14)]
        [Column("MS_DH_NHAP_PT")]
        public string ReceiptID { get; set; }

        [StringLength(25)]
        [Column("MS_PT1")]
        public string SparePartID1 { get; set; }

        [Column("DON_GIA")]
        public double? Price { get; set; }

        [StringLength(50)]
        [Column("NGOAI_TE")]
        public string Currency { get; set; }

        [Column("TY_GIA")]
        public double? ExchangeRate { get; set; }

        [Column("TY_GIA_USD")]
        public double? USDExchangeRate { get; set; }

        [StringLength(512)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        public WorkOrderOfWorkIncludeSparePart WorkOrderOfWorkIncludeSparePart { get; set; }
    }
}
