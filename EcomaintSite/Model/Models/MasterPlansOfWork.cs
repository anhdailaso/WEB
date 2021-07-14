
namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KE_HOACH_TONG_CONG_VIEC")]
    public partial class MasterPlansOfWork
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MasterPlansOfWork()
        {
            MasterPlansOfSparePart = new HashSet<MasterPlansOfSparePart>();
        }

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

        [Column("MS_UU_TIEN")]
        public int? PriorityID { get; set; }

        [StringLength(255)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [StringLength(20)]
        [Column("MS_PHIEU_BAO_TRI")]
        public string WorkOrderID { get; set; }

        [StringLength(20)]
        [Column("EOR_ID")]
        public string EORID { get; set; }

        [Column("KHONG_GQ")]
        public bool? IsIgnored { get; set; }

        [Column("THUE_NGOAI")]
        public bool? IsOutsource { get; set; }

        [Column("PT_HTHANH")]
        public double? PercentOfCompleting { get; set; }

        [Column("SNGUOI")]
        public int? AmountOfPeople { get; set; }

        [StringLength(1000)]
        [Column("YCAU_NS")]
        public string InquiryHR { get; set; }

        [StringLength(1000)]
        [Column("YCAU_DC")]
        public string Tool { get; set; }

        [Column("THOI_GIAN_DU_KIEN")]
        public double? TimePlan { get; set; }

        public  Work Work { get; set; }

        public  MasterPlan MasterPlan { get; set; }

        public  Priority Priority { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<MasterPlansOfSparePart> MasterPlansOfSparePart { get; set; }
    }
}
