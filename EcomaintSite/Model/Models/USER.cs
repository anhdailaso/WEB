namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USERS")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            UserRequestDetail = new HashSet<UserRequestDetail>();
        }

        [Key]
        [StringLength(50)]
        [Column("USERNAME")]
        public string Username { get; set; }

        [Column("GROUP_ID")]
        public int GroupID { get; set; }

        [StringLength(50)]
        [Column("FULL_NAME")]
        public string FullName { get; set; }

        [Required]
        [StringLength(50)]
        [Column("PASS")]
        public string Password { get; set; }

        [StringLength(100)]
        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Column("MS_TO")]
        public int? DepartmentID { get; set; }

        [StringLength(150)]
        [Column("USER_MAIL")]
        public string Email { get; set; }

        [StringLength(9)]
        [Column("MS_CONG_NHAN")]
        public string StaffID { get; set; }

        [Column("Active")]
        public bool? Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<UserRequestDetail> UserRequestDetail { get; set; }
    }
}
