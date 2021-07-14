namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEU_BAO_TRI_TINH_TRANG_CHI_TIET")]
    public partial class WorkOrderDetailsByTheState
    {
        [Key]
        [Column("MS_PHIEU_BAO_TRI", Order = 0)]
        [StringLength(20)]
        public string WorkOrderID { get; set; }

        [Key]
        [Column("STT", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Column("MS_TT_CT")]
        public int? TheStatusOfWorkOrderID { get; set; }

        [StringLength(500)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [StringLength(50)]
        [Column("USERNAME")]
        public string Username { get; set; }

        [Column("THOI_GIAN")]
        public DateTime? DateCreated { get; set; }

        public WorkOrder WorkOrder { get; set; }

        public TheStatusDetailsOfWorkOrder TheStatusDetailsOfWorkOrder { get; set; }
    }
}
