namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("MAY_LOAI_BTPN_CHU_KY")]
    public partial class PreventiveMaintenanceCycle
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
        [Column("NGAY_AD", Order = 2)]
        public DateTime StartDate { get; set; }

        [Column("CHU_KY")]
        public int? Cycle { get; set; }

        [Column("MS_DV_TG")]
        public int? TimeUnitID { get; set; }

        [Column("RUN_TIME")]
        public int? Runtime { get; set; }

        [Column("MS_DVT_RT")]
        public int? UnitOfRuntimeID { get; set; }

        [StringLength(20)]
        [Column("MOVEMENT")]
        public string Movement { get; set; }

        public UnitTime UnitTime { get; set; }

        public UnitOfRuntime UnitOfRuntime { get; set; }

        public PreventiveMaintenance PreventiveMaintenance { get; set; }
    }
}
