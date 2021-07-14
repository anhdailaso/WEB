namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TINH_TRANG_PBT_CT")]
    public partial class TheStatusDetailsOfWorkOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TheStatusDetailsOfWorkOrder()
        {
            WorkOrderDetailsByTheState = new HashSet<WorkOrderDetailsByTheState>();
        }

        [Key]
        [Column("MS_TT_CT")]
        public int ID { get; set; }

        [Column("STT")]
        public int TheStatusOfWorkOrderID { get; set; }

        [Required]
        [StringLength(50)]
        [Column("MA_SO")]
        public string Code { get; set; }

        [StringLength(500)]
        [Column("TEN_TIENG_VIET")]
        public string Name { get; set; }

        [StringLength(500)]
        [Column("TEN_TIENG_ANH")]
        public string NameOfEng { get; set; }

        [StringLength(500)]
        [Column("TEN_TIENG_HOA")]
        public string NameOfChina { get; set; }

        [StringLength(1000)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [Column("MAC_DINH")]
        public bool? Default { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<WorkOrderDetailsByTheState> WorkOrderDetailsByTheState { get; set; }

        public TheStatusOfWorkOrder TheStatusOfWorkOrder { get; set; }
    }
}
