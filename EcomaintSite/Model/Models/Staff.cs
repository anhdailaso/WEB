namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CONG_NHAN")]
    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            //CHUC_VU = new HashSet<CHUC_VU>();
            //MAY_CONG_NHAN = new HashSet<MAY_CONG_NHAN>();
            //MAY_CONG_NHAN1 = new HashSet<MAY_CONG_NHAN>();
            //CONG_NHAN_CHUYEN_MON = new HashSet<CONG_NHAN_CHUYEN_MON>();
            //CONG_VIEC_HANG_NGAY_NV = new HashSet<CONG_VIEC_HANG_NGAY_NV>();
            //Monitoring = new HashSet<Monitoring>();
            //IC_KHO = new HashSet<IC_KHO>();
            //KE_HOACH_THUC_HIEN = new HashSet<KE_HOACH_THUC_HIEN>();
            //LUONG_CO_BAN = new HashSet<LUONG_CO_BAN>();
            WorkOrder = new HashSet<WorkOrder>();
            //PHIEU_BAO_TRI_GIAM_SAT_NGHIEM_THU = new HashSet<PHIEU_BAO_TRI_GIAM_SAT_NGHIEM_THU>();
            //TAI_NAN_LD = new HashSet<TAI_NAN_LD>();
            //TRUC_CA = new HashSet<TRUC_CA>();
            //UserRequestDetail = new HashSet<UserRequestDetail>();
            //VAI_TRO = new HashSet<VAI_TRO>();
            //IC_QUOC_GIA = new HashSet<IC_QUOC_GIA>();
        }

        [Key]
        [StringLength(9)]
        [Column("MS_CONG_NHAN")]
        public string ID { get; set; }

        [StringLength(50)]
        [Column("HO")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Column("TEN")]
        public string LastName { get; set; }

        [Column("NGAY_SINH")]
        public DateTime? Birthday { get; set; }

        [StringLength(255)]
        [Column("NOI_SINH")]
        public string Born { get; set; }

        [Column("PHAI")]
        public bool Gender { get; set; }

        [StringLength(100)]
        [Column("DIA_CHI_THUONG_TRU")]
        public string Address { get; set; }

        [StringLength(10)]
        [Column("SO_CMND")]
        public string IdentityCard { get; set; }

        [Column("NGAY_CAP")]
        public DateTime? DateReleased { get; set; }

        [StringLength(50)]
        [Column("NOI_CAP")]
        public string PlaceReleased { get; set; }

        [Column("MS_TO")]
        public int DepartmentID { get; set; }

        [Column("NGAY_VAO_LAM")]
        public DateTime? DateWorked { get; set; }

        [Column("BO_VIEC")]
        public bool Out { get; set; }

        [Column("NGAY_NGHI_VIEC")]
        public DateTime? DateQuitted { get; set; }

        [StringLength(255)]
        [Column("LY_DO_NGHI")]
        public string ResignationReason { get; set; }

        [Column("MS_TRINH_DO")]
        public short? EducationalLevel { get; set; }

        [StringLength(20)]
        [Column("NGOAI_NGU")]
        public string LanguageLevel { get; set; }

        [StringLength(255)]
        [Column("HINH_CN")]
        public string Photo { get; set; }

        [StringLength(255)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [StringLength(9)]
        [Column("MS_THE_CC")]
        public string EmployeeCard { get; set; }

        [StringLength(15)]
        [Column("SO_DT_NHA_RIENG")]
        public string HomePhone { get; set; }

        [StringLength(15)]
        [Column("SO_DTDD")]
        public string Mobile { get; set; }

        [StringLength(50)]
        [Column("TEN_NGUOI_THAN")]
        public string RelativesName { get; set; }

        [StringLength(50)]
        [Column("QUAN_HE")]
        public string Relationships { get; set; }

        [StringLength(255)]
        [Column("BANG_CAP")]
        public string Certificate { get; set; }

        [StringLength(150)]
        [Column("USER_MAIL")]
        public string Email { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<CHUC_VU> CHUC_VU { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<MAY_CONG_NHAN> MAY_CONG_NHAN { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<MAY_CONG_NHAN> MAY_CONG_NHAN1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<CONG_NHAN_CHUYEN_MON> CONG_NHAN_CHUYEN_MON { get; set; }

        public Team Team { get; set; }

        //public  TRINH_DO_VAN_HOA TRINH_DO_VAN_HOA { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<CONG_VIEC_HANG_NGAY_NV> CONG_VIEC_HANG_NGAY_NV { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<Monitoring> Monitoring { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<IC_KHO> IC_KHO { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<KE_HOACH_THUC_HIEN> KE_HOACH_THUC_HIEN { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<LUONG_CO_BAN> LUONG_CO_BAN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<WorkOrder> WorkOrder { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<PHIEU_BAO_TRI_GIAM_SAT_NGHIEM_THU> PHIEU_BAO_TRI_GIAM_SAT_NGHIEM_THU { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<TAI_NAN_LD> TAI_NAN_LD { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<TRUC_CA> TRUC_CA { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<UserRequestDetail> UserRequestDetail { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<VAI_TRO> VAI_TRO { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<IC_QUOC_GIA> IC_QUOC_GIA { get; set; }
    }
}
