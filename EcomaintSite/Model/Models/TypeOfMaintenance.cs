
namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LOAI_BAO_TRI")]
    public partial class TypeOfMaintenance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeOfMaintenance()
        {
            //Action = new HashSet<Action>();
            MasterPlan = new HashSet<MasterPlan>();
            //LOAI_BAO_TRI_QH = new HashSet<LOAI_BAO_TRI_QH>();
            PreventiveMaintenance = new HashSet<PreventiveMaintenance>();
            //WorkOrder = new HashSet<WorkOrder>();
        }

        [Key]
        [Column("MS_LOAI_BT")]
        public int ID { get; set; }

        [Required]
        [StringLength(40)]
        [Column("TEN_LOAI_BT")]
        public string Name { get; set; }

        [Column("THU_TU")]
        public int? Index { get; set; }

        [Column("MS_HT_BT")]
        public int MaintenanceFormID { get; set; }

        [StringLength(255)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [Column("HU_HONG")]
        public bool Breakdown { get; set; }

        [Column("MS_LOAI_CV")]
        public short? TypeOfWorkID { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<Action> Action { get; set; }

        public MaintenanceForm MaintenanceForm { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<MasterPlan> MasterPlan { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<LOAI_BAO_TRI_QH> LOAI_BAO_TRI_QH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<PreventiveMaintenance> PreventiveMaintenance { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<WorkOrder> WorkOrder { get; set; }
    }
}
