namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserRequestComponent
    {
        [Key]
        [Column("STT", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserRequestID { get; set; }

        [Key]
        [Column("MS_MAY", Order = 1)]
        [StringLength(30)]
        public string DeviceID { get; set; }

        [Key]
        [Column("STT_VAN_DE", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserRequestDetailID { get; set; }

        [Key]
        [Column("STT_BO_PHAN", Order = 3)]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        [Column("MS_BO_PHAN")]
        public string ComponentID { get; set; }

        [Required]
        [StringLength(30)]
        [Column("MS_PT")]
        public string SparePartID { get; set; }

        [StringLength(30)]
        [Column("MS_VI_TRI_PT")]
        public string LocationID { get; set; }

        public  UserRequestDetail UserRequestDetail { get; set; }
    }
}
