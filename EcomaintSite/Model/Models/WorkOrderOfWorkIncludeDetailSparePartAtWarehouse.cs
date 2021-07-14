namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEU_BAO_TRI_CONG_VIEC_PHU_TUNG_CHI_TIET_KHO")]
    public partial class WorkOrderOfWorkIncludeDetailSparePartAtWarehouse
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DetailSparePartID { get; set; }

        [Key]
        [Column("MS_DH_XUAT_PT", Order = 5)]
        [StringLength(14)]
        public string IssuedID { get; set; }

        [Key]
        [Column("MS_DH_NHAP_PT", Order = 6)]
        [StringLength(14)]
        public string ReceiptID { get; set; }

        [Key]
        [Column("MS_PTTT", Order = 7)]
        [StringLength(25)]
        public string AlternativeSparePartID { get; set; }

        [Key]
        [Column(Order = 8)]
        public double ID { get; set; }

        [Column("SL_TT")]
        public double? ActualQuantity { get; set; }

        [Column("DON_GIA")]
        public double? Price { get; set; }

        [StringLength(6)]
        [Column("NGOAI_TE")]
        public string Currency { get; set; }

        [Column("ExchangeRate")]
        public double? TI_GIA { get; set; }

        [Column("TI_GIA_USD")]
        public double? USDExchangeRate { get; set; }

        [StringLength(512)]
        [Column("GHI_CHU")]
        public string Note { get; set; }
        
        public Sparepart Sparepart { get; set; }
    }
}
