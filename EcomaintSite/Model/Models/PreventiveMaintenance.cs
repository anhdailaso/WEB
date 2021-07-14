namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MAY_LOAI_BTPN")]
    public partial class PreventiveMaintenance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PreventiveMaintenance()
        {
            PreventiveMaintenanceCycle = new HashSet<PreventiveMaintenanceCycle>();
            PreventiveMaintenanceOfWork = new HashSet<PreventiveMaintenanceOfWork>();
        }

        [Key]
        [Column("MS_MAY", Order = 0)]
        [StringLength(30)]
        public string DeviceID { get; set; }

        [Key]
        [Column("MS_LOAI_BT", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeOfMaintenanceID { get; set; }

        [Column("NGAY_CUOI")]
        public DateTime? LastDate { get; set; }

        [Column("SO_NGAY")]
        public int? NumberOfDate { get; set; }

        [StringLength(255)]
        [Column("THUC_HIEN_BOI")]
        public string ExecutedBy { get; set; }

        [StringLength(255)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        public TypeOfMaintenance TypeOfMaintenance { get; set; }

        public Device Device { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<PreventiveMaintenanceCycle> PreventiveMaintenanceCycle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<PreventiveMaintenanceOfWork> PreventiveMaintenanceOfWork { get; set; }
    }
}
