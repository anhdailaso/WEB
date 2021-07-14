namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("BO_PHAN_CHIU_PHI")]
    public partial class Budget
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Budget()
        {
            //BUDGETs = new HashSet<BUDGET>();
            //BO_PHAN_CHIU_PHI_RULE = new HashSet<BO_PHAN_CHIU_PHI_RULE>();
            //CHI_PHI_THEO_THANG = new HashSet<CHI_PHI_THEO_THANG>();
            //KINH_PHI_NAM = new HashSet<KINH_PHI_NAM>();
            //MAY_BO_PHAN_CHIU_PHI = new HashSet<MAY_BO_PHAN_CHIU_PHI>();
            CostCenters = new HashSet<CostCenter>();
            //NHOMs = new HashSet<NHOM>();
        }

        [Key]
        [Column("MS_BP_CHIU_PHI")]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        [Column("TEN_BP_CHIU_PHI")]
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

        [Column("ISIT")]
        public bool? IsIt { get; set; }

        [StringLength(10)]
        [Column("ACCOUNT")]
        public string Account { get; set; }

        [StringLength(20)]
        [Column("SUB")]
        public string Sub { get; set; }

        [StringLength(10)]
        [Column("ACCOUNT_DN")]
        public string AccountDN { get; set; }

        [StringLength(20)]
        [Column("SUB_DN")]
        public string SubDN { get; set; }

        [StringLength(10)]
        [Column("ACCOUNT_BD")]
        public string AccountBD { get; set; }

        [Column("SUB_BD")]
        public string SubBD { get; set; }

        [StringLength(10)]
        [Column("ACCOUNT_HY")]
        public string AccountHY { get; set; }

        [StringLength(20)]
        [Column("SUB_HY")]
        public string SubHY { get; set; }

        [StringLength(50)]
        [Column("MA_BP_CHIU_PHI")]
        public string ID2 { get; set; }

        [StringLength(50)]
        [Column("BPCP_MAIL")]
        public string Email { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<BUDGET> BUDGETs { get; set; }

        public OrgUnit OrgUnit { get; set; }

        //public TypeOfCost LOAI_CHI_PHI1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<BO_PHAN_CHIU_PHI_RULE> BO_PHAN_CHIU_PHI_RULE { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<CHI_PHI_THEO_THANG> CHI_PHI_THEO_THANG { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<KINH_PHI_NAM> KINH_PHI_NAM { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<MAY_BO_PHAN_CHIU_PHI> MAY_BO_PHAN_CHIU_PHI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<CostCenter> CostCenters { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<NHOM> NHOMs { get; set; }
    }

}