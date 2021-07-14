namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("MAY_LOAI_BTPN_CONG_VIEC_PHU_TUNG")]
    public partial class PreventiveMaintenanceOfSparePart
    {
        [Key]
        [Column("MS_MAY", Order = 0)]
        [StringLength(30)]
        public string DeviceID { get; set; }

        [Key]
        [Column("MS_LOAI_BT", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeOfMaintenanceID { get; set; }

        [Key]
        [Column("MS_CV", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkID { get; set; }
        
        [Key]
        [Column("MS_BO_PHAN", Order = 3)]
        [StringLength(50)]
        public string ComponentID { get; set; }

        [Key]
        [Column("MS_PT", Order = 4)]
        [StringLength(25)]
        public string SparePartID { get; set; }

        [Column("SO_LUONG")]
        public double Amount { get; set; }

        [StringLength(255)]
        [Column("QUI_CACH")]
        public string Specification { get; set; }

        [StringLength(250)]
        [Column("GHI_CHU_BTPN")]
        public string Note { get; set; }

        public PreventiveMaintenanceOfWork PreventiveMaintenanceOfWork { get; set; }
    }
}
