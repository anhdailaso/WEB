namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IC_PHU_TUNG")]
    public partial class Sparepart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sparepart()
        {
            //CONG_VIEC_HANG_NGAY_VT = new HashSet<CONG_VIEC_HANG_NGAY_VT>();
            //DE_XUAT_MUA_HANG_CHI_TIET = new HashSet<DE_XUAT_MUA_HANG_CHI_TIET>();
            //DI_CHUYEN_VI_TRI_TRONG_KHO = new HashSet<DI_CHUYEN_VI_TRI_TRONG_KHO>();
            //DON_DAT_HANG_CHI_TIET = new HashSet<DON_DAT_HANG_CHI_TIET>();
            //IC_DON_HANG_NHAP_VAT_TU = new HashSet<IC_DON_HANG_NHAP_VAT_TU>();
            //IC_DON_HANG_NHAP_X_VAT_TU = new HashSet<IC_DON_HANG_NHAP_X_VAT_TU>();
            //IC_DON_HANG_XUAT_VAT_TU = new HashSet<IC_DON_HANG_XUAT_VAT_TU>();
            //IC_DON_HANG_XUAT_X_VAT_TU = new HashSet<IC_DON_HANG_XUAT_X_VAT_TU>();
            //SparePartWarehouse = new HashSet<SparePartWarehouse>();
            //IC_PHU_TUNG_THAY_THE = new HashSet<IC_PHU_TUNG_THAY_THE>();
            //KIEM_KE_VAT_TU_CHI_TIET = new HashSet<KIEM_KE_VAT_TU_CHI_TIET>();
            //PICK_LIST_CHI_TIET = new HashSet<PICK_LIST_CHI_TIET>();
            //LOAI_MAY = new HashSet<LOAI_MAY>();
            //LOAI_PHU_TUNG = new HashSet<LOAI_PHU_TUNG>();
            WorkOrderOfWorkIncludeDetailSparePartAtWarehouse = new HashSet<WorkOrderOfWorkIncludeDetailSparePartAtWarehouse>();
            WorkOrderOfWorkIncludeSupportingSparePart = new HashSet<WorkOrderOfWorkIncludeSupportingSparePart>();
           
        }

        [Key]
        [StringLength(25)]
        [Column("MS_PT")]
        public string ID { get; set; }

        [StringLength(25)]
        [Column("MS_PT_NCC")]
        public string PartNo { get; set; }

        [StringLength(25)]
        [Column("MS_PT_CTY")]
        public string ItemCode { get; set; }

        [Required]
        [StringLength(100)]
        [Column("TEN_PT")]
        public string Name { get; set; }

        [StringLength(255)]
        [Column("QUY_CACH")]
        public string Specification { get; set; }

        [StringLength(255)]
        [Column("ANH_PT")]
        public string Image { get; set; }

        [Required]
        [StringLength(10)]
        [Column("MS_LOAI_VT")]
        public string TypeOfMaterialID { get; set; }

        [StringLength(10)]
        [Column("DVT")]
        public string UnitOfCalculationID { get; set; }

        [Column("DUNG_CU_DO")]
        public bool IsMeasurementInstrument { get; set; }

        [Column("MS_CACH_DAT_HANG")]
        public int? PurchaseMethodID { get; set; }

        [Column("SO_NGAY_DAT_MUA_HANG")]
        public int? Leadtime { get; set; }

        [Column("TON_TOI_THIEU")]
        public double? MinInventory { get; set; }

        [StringLength(20)]
        [Column("MS_KH")]
        public string CustomerID { get; set; }

        [Column("SL_DA_DAT_CHUA_MUA")]
        public double? QuantityOrdered { get; set; }

        [Column("MS_HSX")]
        public int? ProducerID { get; set; }

        [Column("MS_VI_TRI")]
        public int? LocationID { get; set; }

        [Column("MS_CLASS")]
        public int? ClassID { get; set; }

        [Column("TON_KHO_MAX")]
        public double? MaxInventory { get; set; }

        [Column("HANG_NGOAI")]
        public bool? IsForeign { get; set; }

        //[StringLength(250)]
        //public string TEN_PT_VIET { get; set; }

        [Column("SERVICE_ID")]
        public int? ServiceID { get; set; }

        [Column("THEO_KHO")]
        public bool? IsMultipleWarehouse { get; set; }

        [Column("ACTIVE_PT")]
        public bool? Active { get; set; }

        [StringLength(50)]
        [Column("USER_INSERT_PT")]
        public string CreatedBy { get; set; }

        [Column("NGAY_INSERT_PT")]
        public DateTime? DateCreated { get; set; }

        [StringLength(50)]
        [Column("USER_UPDATE_PT")]
        public string ModifiedBy { get; set; }

        [Column("NGAY_UPDATE_PT")]
        public DateTime? DateModified { get; set; }

        [Column("KY_PB")]
        public int? Periodic { get; set; }

        [StringLength(100)]
        [Column("TEN_PT_OLD")]
        public string OldName { get; set; }

        [StringLength(250)]
        [Column("TEN_PT_ANH")]
        public string NameOfEng { get; set; }

        [StringLength(250)]
        [Column("TEN_PT_HOA")]
        public string NameOfChina { get; set; }

        [Column("VAT_TU_PT")]
        public bool? IsMaterial { get; set; }
        [Column("MS_KHO")]
        public int? WarehouseID { get; set; }

        //public virtual CACH_DAT_HANG CACH_DAT_HANG { get; set; }

        //public virtual CLASS_VT CLASS_VT { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CONG_VIEC_HANG_NGAY_VT> CONG_VIEC_HANG_NGAY_VT { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DE_XUAT_MUA_HANG_CHI_TIET> DE_XUAT_MUA_HANG_CHI_TIET { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DI_CHUYEN_VI_TRI_TRONG_KHO> DI_CHUYEN_VI_TRI_TRONG_KHO { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DON_DAT_HANG_CHI_TIET> DON_DAT_HANG_CHI_TIET { get; set; }

        //public virtual UnitOfCalculation UnitOfCalculation { get; set; }

        //public virtual HANG_SAN_XUAT HANG_SAN_XUAT { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<IC_DON_HANG_NHAP_VAT_TU> IC_DON_HANG_NHAP_VAT_TU { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<IC_DON_HANG_NHAP_X_VAT_TU> IC_DON_HANG_NHAP_X_VAT_TU { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<IC_DON_HANG_XUAT_VAT_TU> IC_DON_HANG_XUAT_VAT_TU { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<IC_DON_HANG_XUAT_X_VAT_TU> IC_DON_HANG_XUAT_X_VAT_TU { get; set; }

        //public virtual KHACH_HANG KHACH_HANG { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SparePartWarehouse> SparePartWarehouse { get; set; }

        public TypeOfMaterial TypeOfMaterial { get; set; }

        //public virtual SERVICE_LEVEL SERVICE_LEVEL { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<IC_PHU_TUNG_THAY_THE> IC_PHU_TUNG_THAY_THE { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<KIEM_KE_VAT_TU_CHI_TIET> KIEM_KE_VAT_TU_CHI_TIET { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrderOfWorkIncludeDetailSparePartAtWarehouse> WorkOrderOfWorkIncludeDetailSparePartAtWarehouse { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrderOfWorkIncludeSupportingSparePart> WorkOrderOfWorkIncludeSupportingSparePart { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<PICK_LIST_CHI_TIET> PICK_LIST_CHI_TIET { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LOAI_MAY> LOAI_MAY { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LOAI_PHU_TUNG> LOAI_PHU_TUNG { get; set; }
    }
}
