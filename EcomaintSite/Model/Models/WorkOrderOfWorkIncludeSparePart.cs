namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("PHIEU_BAO_TRI_CONG_VIEC_PHU_TUNG")]
    public partial class WorkOrderOfWorkIncludeSparePart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkOrderOfWorkIncludeSparePart()
        {
            WorkOrderOfWorkIncludeDetailSparePart = new HashSet<WorkOrderOfWorkIncludeDetailSparePart>();
        }

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

        [Column("SL_KH")]
        public double? PlanQuantity { get; set; }

        [Column("SL_TT")]
        public double? ActualQuantity { get; set; }

        [Column("STT")]
        public int? Index { get; set; }

        [StringLength(255)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [Column("DON_GIA")]
        public double? Price { get; set; }

        [StringLength(50)]
        [Column("NGOAI_TE")]
        public string Currency { get; set; }

        [Column("TY_GIA")]
        public double? ExchangeRate { get; set; }

        [Column("TY_GIA_USD")]
        public double? USDExchangeRate { get; set; }

        [StringLength(500)]
        [Column("MS_PT_TT")]
        public string AlternativeSparePartID { get; set; }

        public WorkOrderOfWork WorkOrderOfWork { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<WorkOrderOfWorkIncludeDetailSparePart> WorkOrderOfWorkIncludeDetailSparePart { get; set; }
    }
}
