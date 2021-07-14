
namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KE_HOACH_TONG_CONG_VIEC_PHU_TUNG")]
    public partial class MasterPlansOfSparePart
    {
        [Key]
        [Column("MS_MAY", Order = 0)]
        [StringLength(30)]
        public string DeviceID { get; set; }

        [Key]
        [Column("HANG_MUC_ID", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlanID { get; set; }

        [Key]
        [Column("MS_CV", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkID { get; set; }

        [Key]
        [Column("MS_BO_PHAN", Order = 3)]
        [StringLength(50)]
        public string ComponentID { get; set; }

        [Key]
        [Column("MS_PT", Order = 4)]
        [StringLength(25)]
        public string SparePartID { get; set; }

        [Column("SO_LUONG")]
        public double? Amount { get; set; }

        [Column("GHI_CHU")]
        [StringLength(255)]
        public string Note { get; set; }

        [Column("DON_GIA_KH", TypeName = "numeric")]
        public decimal? EstimatePrice { get; set; }

        [Column("TIEN_TE_KH", TypeName = "numeric")]
        public decimal? Currency { get; set; }

        [Column("TY_GIA_KH", TypeName = "numeric")]
        public decimal? ExchangeRate { get; set; }

        [Column("TY_GIA_USD_KH", TypeName = "numeric")]
        public decimal? USDExchangeRate { get; set; }

        [Column("DON_GIA_CUOI", TypeName = "numeric")]
        public decimal? LastPrice { get; set; }

        [Column("NGAY_LAY_DG_KH")]
        public DateTime? InputDate { get; set; }

        public  MasterPlansOfWork MasterPlansOfWork { get; set; }
    }
}
