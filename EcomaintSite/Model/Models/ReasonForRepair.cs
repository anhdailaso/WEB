namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LY_DO_SUA_CHUA")]
    public partial class ReasonForRepair
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReasonForRepair()
        {
            MasterPlan = new HashSet<MasterPlan>();
        }

        [Key]
        [Column("MS_LY_DO")]
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        [Column("TEN_LY_DO_VIET")]
        public string Name { get; set; }

        [StringLength(250)]
        [Column("TEN_LY_DO_ANH")]
        public string NameOfEng { get; set; }

        [StringLength(250)]
        [Column("TEN_LY_DO_HOA")]
        public string NameOfChina { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<MasterPlan> MasterPlan { get; set; }

    }
}
