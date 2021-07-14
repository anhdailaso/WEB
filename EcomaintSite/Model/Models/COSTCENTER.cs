namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COSTCENTER")]
    public partial class CostCenter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CostCenter()
        {
            //BO_PHAN_CHIU_PHI_RULE = new HashSet<BO_PHAN_CHIU_PHI_RULE>();
            Budget = new HashSet<Budget>();
        }

        [Key]
        [StringLength(50)]
        [Column("MS_COSTCENTER")]
        public string ID { get; set; }

        [Required]
        [StringLength(255)]
        [Column("TEN_COSTCENTER")]
        public string Name { get; set; }

        [Column("LOAI_CHI_PHI")]
        public short? TypeOfCost { get; set; }

        [Required]
        [StringLength(6)]
        [Column("MSDV")]
        public string OrgUnitID { get; set; }

        [StringLength(255)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<BO_PHAN_CHIU_PHI_RULE> BO_PHAN_CHIU_PHI_RULE { get; set; }

        public OrgUnit OrgUnit { get; set; }

        //public TypeOfCost LOAI_CHI_PHI1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Budget> Budget { get; set; }
    }

}