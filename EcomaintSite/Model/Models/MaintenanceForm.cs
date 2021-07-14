namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HINH_THUC_BAO_TRI")]
    public partial class MaintenanceForm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MaintenanceForm()
        {
            TypeOfMaintenance = new HashSet<TypeOfMaintenance>();
        }

        [Key]
        [Column("MS_HT_BT")]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Column("TEN_HT_BT")]
        public string Name { get; set; }

        [Column("PHONG_NGUA")]
        public bool Periodic { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TypeOfMaintenance> TypeOfMaintenance { get; set; }
    }
}
