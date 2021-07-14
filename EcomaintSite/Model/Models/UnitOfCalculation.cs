namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DON_VI_TINH")]
    public partial class UnitOfCalculation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UnitOfCalculation()
        {
            Sparepart = new HashSet<Sparepart>();
        }

        [Key]
        [StringLength(10)]
        [Column("DVT")]
        public string ID { get; set; }

        [Required]
        [StringLength(20)]
        [Column("TEN_1")]
        public string Name { get; set; }

        [StringLength(20)]
        [Column("TEN_2")]
        public string NameOfEng { get; set; }

        [StringLength(20)]
        [Column("TEN_3")]
        public string NameOfChina { get; set; }

        [StringLength(255)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Sparepart> Sparepart { get; set; }
    }
}
