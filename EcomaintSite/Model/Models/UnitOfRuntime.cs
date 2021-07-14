namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DON_VI_TINH_RUN_TIME")]
    public partial class UnitOfRuntime
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UnitOfRuntime()
        {
            Component = new HashSet<Component>();
            PreventiveMaintenanceCycle = new HashSet<PreventiveMaintenanceCycle>();
        }
        
        [Key]
        [Column("MS_DVT_RT")]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Column("TEN_DVT_RT")]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Component> Component { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<PreventiveMaintenanceCycle> PreventiveMaintenanceCycle { get; set; }
    }
}
