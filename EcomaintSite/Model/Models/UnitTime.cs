namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DON_VI_THOI_GIAN")]
    public partial class UnitTime
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UnitTime()
        {
            CalibrationCycle = new HashSet<CalibrationCycle>();
            PreventiveMaintenanceCycle = new HashSet<PreventiveMaintenanceCycle>();
        }

        [Key]
        [Column("MS_DV_TG")]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        [Column("TEN_DV_TG")]
        public string Name { get; set; }

        [StringLength(20)]
        [Column("TEN_DV_TG_ANH")]
        public string NameOfEng { get; set; }

        [StringLength(20)]
        [Column("TEN_DV_TG_HOA")]
        public string NameOfChina { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<CalibrationCycle> CalibrationCycle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<PreventiveMaintenanceCycle> PreventiveMaintenanceCycle { get; set; }
    }
}
