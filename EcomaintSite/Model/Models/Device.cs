namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MAY")]
    public partial class Device
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Device()
        {
            Component = new HashSet<Component>();
            CalibrationCycle = new HashSet<CalibrationCycle>();         
            MasterPlan = new HashSet<MasterPlan>();          
            PreventiveMaintenance = new HashSet<PreventiveMaintenance>();          
            UserRequestDetail = new HashSet<UserRequestDetail>();
        }

        [Key]
        [StringLength(30)]
        [Column("MS_MAY")]
        public string ID { get; set; }

        [StringLength(20)]
        [Column("MS_NHOM_MAY")]
        public string EquipmentGroupID { get; set; }

        [Column("MS_HSX")]
        public int? ProducerID { get; set; }

        [Column("MS_HIEN_TRANG")]
        public int? ConditionID { get; set; }

        [StringLength(20)]
        [Column("MS_KH")]
        public string CustomerID { get; set; }

        [Column("SO_NAM_KHAU_HAO")]
        public int? YearsOfDepreciation { get; set; }

        [StringLength(255)]
        [Column("MO_TA")]
        public string Description { get; set; }

        [StringLength(150)]
        [Column("NHIEM_VU_THIET_BI")]
        public string WhatToDo { get; set; }

        [StringLength(50)]
        [Column("MODEL")]
        public string Model { get; set; }

        [StringLength(50)]
        [Column("SERIAL_NUMBER")]
        public string SerialNumber { get; set; }

        [Column("NGAY_SX")]
        public DateTime? DateOfManufacture { get; set; }

        [StringLength(30)]
        [Column("NUOC_SX")]
        public string Origin { get; set; }

        [Column("NGAY_MUA")]
        public DateTime? DatePurchased { get; set; }

        [Column("NGAY_BD_BAO_HANH")]
        public DateTime? WarrantyFrom { get; set; }

        [Column("NGAY_DUA_VAO_SD")]
        public DateTime? DateUsed { get; set; }

        [StringLength(50)]
        [Column("SO_THE")]
        public string CardNumber { get; set; }

        [Column("SO_NGAY_LV_TRONG_TUAN")]
        public double? WorkingDaysPerWeek { get; set; }

        [Column("SO_GIO_LV_TRONG_NGAY")]
        public double? WorkingHoursPerWeek { get; set; }


        [Column("MS_DVT_RT")]
        public int? UnitOfRuntimeID { get; set; }

        [Column("TY_LE_CON_LAI")]
        public int? RatioRemains { get; set; }

        [Column("MUC_UU_TIEN")]
        public short? PriorityID { get; set; }

        [Column("SO_THANG_BH")]
        public int? WarrantyPeriod { get; set; }

        [Column("GIA_MUA")]
        public double? Price { get; set; }

        [StringLength(6)]
        [Column("NGOAI_TE")]
        public string Currency { get; set; }

        [StringLength(1000)]
        [Column("LUU_Y_SU_DUNG")]
        public string Note { get; set; }

        [StringLength(255)]
        [Column("Anh_TB")]
        public string Image { get; set; }

        [Column("CHU_KY_HC_TB")]
        public int? CalibrationCycleNo { get; set; }

        [StringLength(255)]
        [Column("TEN_MAY")]
        public string Name { get; set; }

        [Column("AN_TOAN")]
        public bool? Safety { get; set; }

        [StringLength(50)]
        [Column("MS_HO_SO")]
        public string DocumentID { get; set; }

        [StringLength(150)]
        [Column("TEN_HO_SO")]
        public string DocumentName { get; set; }

        [Column("NGAY_HIEU_LUC_HO_SO")]
        public DateTime? DateDocumentEffective { get; set; }

        [StringLength(30)]
        [Column("SO_CT")]
        public string InvoiceNumber { get; set; }

        [Column("TI_GIA_VND")]
        public double? VNDExchangeRate { get; set; }

        [Column("TI_GIA_USD")]
        public double? USDExchangeRate { get; set; }

        [Column("CHU_KY_HIEU_CHUAN_TB_NGOAI")]
        public int? ExternalCycle { get; set; }

        [Column("CHU_KY_KD_TB")]
        public int? VerifiableCycle { get; set; }

        [Column("DON_VI_THOI_GIAN")]
        public int? UnitTime { get; set; }

        [StringLength(50)]
        [Column("USER_INSERT")]
        public string CreatedBy { get; set; }

        [Column("NGAY_INSERT")]
        public DateTime? DateCreated { get; set; }

        [StringLength(50)]
        [Column("USER_UPDATE")]
        public string ModifiedBy { get; set; }

        [Column("NGAY_UPDATE")]
        public DateTime? DateModified { get; set; }

        [Column("DINH_MUC_SL")]
        public double? Quota { get; set; }

        [StringLength(50)]
        [Column("DVT_SL")]
        public string UnitQuantity { get; set; }

        [StringLength(250)]
        [Column("TEN_MAY_ANH")]
        public string NameOfEng { get; set; }

        [StringLength(250)]
        [Column("TEN_MAY_HOA")]
        public string NameOfChina { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Component> Component { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<CalibrationCycle> CalibrationCycle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<MasterPlan> MasterPlan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<PreventiveMaintenance> PreventiveMaintenance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<UserRequestDetail> UserRequestDetail { get; set; }
    }
}
