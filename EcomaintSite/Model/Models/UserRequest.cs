namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YEU_CAU_NSD")]
    public partial class UserRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserRequest()
        {
            UserRequestDetail = new HashSet<UserRequestDetail>();
        }

        [Key]
        [Column("STT")]
        public int ID { get; set; }

        [Column("NGAY")]
        public DateTime DateCreated { get; set; }

        [Column("GIO_YEU_CAU")]
        public DateTime HourCreated { get; set; }

        [Required]
        [StringLength(50)]
        [Column("NGUOI_YEU_CAU")]
        public string RequestedBy { get; set; }

        [Column("USER_COMMENT")]
        public string UserComment { get; set; }

        [Column("NGAY_HOAN_THANH")]
        public DateTime? DateCompleted { get; set; }

        [Column("DA_KIEM_TRA")]
        public bool? IsChecked { get; set; }

        [StringLength(50)]
        [Column("USERNAME")]
        public string Username { get; set; }

        [StringLength(255)]
        [Column("REVIEWER_COMMENT")]
        public string ReviewComment { get; set; }

        [StringLength(30)]
        [Column("USER_LAP")]
        public string CreatedBy { get; set; }

        [StringLength(500)]
        [Column("EMAIL_NSD")]
        public string Email { get; set; }

        [StringLength(250)]
        [Column("SO_YEU_CAU")]
        public string RequestNO { get; set; }

        [StringLength(50)]
        [Column("MS_YEU_CAU")]
        public string UserRequestID { get; set; }

        [StringLength(50)]
        [Column("MS_N_XUONG")]
        public string WorkSiteID { get; set; }

        [StringLength(300)]
        [Column("REF_NO")]
        public string REFNo { get; set; }

        [Column("REF_ID")]
        public int? REFID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<UserRequestDetail> UserRequestDetail { get; set; }
    }
}
