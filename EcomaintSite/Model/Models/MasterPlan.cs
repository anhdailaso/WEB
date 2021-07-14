
namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KE_HOACH_TONG_THE")]
    public partial class MasterPlan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MasterPlan()
        {
            MasterPlansOfWork = new HashSet<MasterPlansOfWork>();
        }

        [Required]
        [StringLength(30)]
        [Column("MS_MAY")]
        public string DeviceID { get; set; }

        [Key]
        [Column("HANG_MUC_ID")]
        public int ID { get; set; }

        [StringLength(250)]
        [Column("TEN_HANG_MUC")]
        public string Name { get; set; }

        [Column("NGAY")]
        public DateTime? DateCreated { get; set; }

        [Column("NGAY_DK_HT")]
        public DateTime? DateCompleted { get; set; }

        [StringLength(255)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [Column("MS_LOAI_BT")]
        public int? TypeOfMaintenanceID { get; set; }

        [Column("NGAY_BTPN")]
        public DateTime? DueDate { get; set; }

        [Column("LY_DO_SC")]
        public int? ReasonForRepairID { get; set; }

        [StringLength(15)]
        [Column("MS_CACH_TH")]
        public string ExecuteID { get; set; }

        [StringLength(50)]
        [Column("USERNAME")]
        public string Username { get; set; }

        [Column("PT_HTHANH")] 
        public double? PercentOfCompleting { get; set; }

        [Column("MS_UU_TIEN")]
        public int? PriorityID { get; set; }

        [StringLength(9)]
        [Column("MS_CN_PT")]
        public string ExecutedBy { get; set; }

        [StringLength(9)]
        [Column("MS_CN_GS")]
        public string MonitoredBy { get; set; }

        [Column("CP_VT_NN_NGOAI_DM")]
        public long? ImportCost { get; set; }

        [Column("CP_VT_NGOAI_DM")]
        public long? ItemCost { get; set; }

        [Column("CP_THUE_NGOAI")]
        public long? OutsourcingCost { get; set; }

        [Column("NGAY_BTDK_GOC")]
        public DateTime? OriginalStartDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<MasterPlansOfWork> MasterPlansOfWork { get; set; }

        public  TypeOfMaintenance TypeOfMaintenance { get; set; }

        public  ReasonForRepair ReasonForRepair { get; set; }

        public  Device Device { get; set; }
    }
}
