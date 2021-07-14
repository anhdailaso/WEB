
namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("DON_VI")]
    public partial class OrgUnit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrgUnit()
        {
            Budget = new HashSet<Budget>();
            CostCenters = new HashSet<CostCenter>();
            //DON_VI_TO = new HashSet<DON_VI_TO>();
            Department = new HashSet<Department>();
        }

        [Key]
        [StringLength(6)]
        [Column("MS_DON_VI")]
        public string ID { get; set; }

        [Required]
        [StringLength(255)]
        [Column("TEN_DON_VI")]
        public string Name { get; set; }

        [StringLength(20)]
        [Column("TEN_NGAN")]
        public string ShortName { get; set; }

        [StringLength(100)]
        [Column("DIA_CHI")]
        public string Address { get; set; }

        [Column("THUE_NGOAI")]
        public bool IsOutsource { get; set; }

        [Column("MAC_DINH")]
        public bool Default { get; set; }

        [StringLength(15)]
        [Column("DIEN_THOAI")]
        public string Phone { get; set; }

        [StringLength(15)]
        [Column("FAX")]
        public string Fax { get; set; }

        [StringLength(255)]
        [Column("TEN_DON_VI_ANH")]
        public string NameOfEng { get; set; }

        [StringLength(255)]
        [Column("TEN_DON_VI_HOA")]
        public string NameOfChina { get; set; }

        [StringLength(10)]
        [Column("TEN_RUT_GON")]
        public string CompactName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Budget> Budget { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<CostCenter> CostCenters { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public  ICollection<DON_VI_TO> DON_VI_TO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Department { get; set; }
    }
}
