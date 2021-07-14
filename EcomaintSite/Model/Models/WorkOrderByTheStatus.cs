namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEU_BAO_TRI_TINH_TRANG")]
    public partial class WorkOrderByTheStatus
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Key]
        [Column("MS_PHIEU_BAO_TRI", Order = 1)]
        [StringLength(20)]
        public string WorkOrderID { get; set; }

        [StringLength(500)]
        [Column("THONG_SO_DO")]
        public string MeasumentParam { get; set; }

        [StringLength(500)]
        [Column("THIET_BI_DO")]
        public string MeasumentDevice { get; set; }

        [StringLength(500)]
        [Column("TT_TRUOC_BT")]
        public string StateBeforeMaintenance { get; set; }

        [StringLength(500)]
        [Column("TT_SAU_BT")]
        public string StateAfterMaintenance { get; set; }

        [Column("MS_DV_DO")]
        public short? MeasurementUnitID { get; set; }

        [Column("XAC_NHAN")]
        public bool? IsConfirm { get; set; }

        [StringLength(500)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [StringLength(50)]
        [Column("USER_XN")]
        public string Username { get; set; }

        //public DON_VI_DO DON_VI_DO { get; set; }

        public WorkOrder WorkOrder { get; set; }
    }
}
