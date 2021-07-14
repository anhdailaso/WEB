namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MAY_LOAI_BTPN_CONG_VIEC")]
    public partial class PreventiveMaintenanceOfWork
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PreventiveMaintenanceOfWork()
        {
            PreventiveMaintenanceOfSparePart = new HashSet<PreventiveMaintenanceOfSparePart>();
        }

        [Key]
        [Column("MS_MAY", Order = 0)]
        [StringLength(30)]
        public string DeviceID { get; set; }

        [Key]
        [Column("MS_LOAI_BT", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeOfMaintenanceID { get; set; }

        [Key]
        [Column("MS_CV", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkID { get; set; }

        [Key]
        [Column("MS_BO_PHAN", Order = 3)]
        [StringLength(50)]
        public string ComponentID { get; set; }

        public Work Work { get; set; }

        public PreventiveMaintenance PreventiveMaintenance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<PreventiveMaintenanceOfSparePart> PreventiveMaintenanceOfSparePart { get; set; }
    }
}
