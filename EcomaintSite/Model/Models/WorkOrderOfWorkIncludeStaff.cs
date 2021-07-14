namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEU_BAO_TRI_CONG_VIEC_NHAN_SU")]
    public partial class WorkOrderOfWorkIncludeStaff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkOrderOfWorkIncludeStaff()
        {
            WorkOrderOfWorkIncludeDetailStaff = new HashSet<WorkOrderOfWorkIncludeDetailStaff>();
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
        [Column("MS_CONG_NHAN", Order = 3)]
        [StringLength(9)]
        public string StaffID { get; set; }

        public WorkOrderOfWork WorkOrderOfWork { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<WorkOrderOfWorkIncludeDetailStaff> WorkOrderOfWorkIncludeDetailStaff { get; set; }
    }
}
