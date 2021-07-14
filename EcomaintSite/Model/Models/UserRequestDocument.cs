namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserRequestDocument
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
        [Column("STT_HINH", Order = 3)]
        public int ID { get; set; }

        [StringLength(255)]
        [Column("DUONG_DAN")]
        public string Path { get; set; }

        [StringLength(100)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        public  UserRequestDetail UserRequestDetail { get; set; }
    }
}
