namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CONG_VIEC")]
    public partial class Work
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Work()
        {
            MasterPlansOfWork = new HashSet<MasterPlansOfWork>();
            PreventiveMaintenanceOfWork = new HashSet<PreventiveMaintenanceOfWork>();
            //WorkOrderOfWork = new HashSet<WorkOrderOfWork>();
            ComponentOfWork = new HashSet<ComponentOfWork>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MS_CV")]
        public int ID { get; set; }

        [StringLength(100)]
        [Column("MA_CV")]
        public string ID2 { get; set; }

        [StringLength(100)]
        [Column("KY_HIEU_CV")]
        public string WorkSymbol { get; set; }

        [Column("MS_LOAI_CV")]
        public short? TypeOfWorkID { get; set; }

        [StringLength(255)]
        [Column("MO_TA_CV")]
        public string Description { get; set; }

        [StringLength(255)]
        [Column("MO_TA_CV_ANH")]
        public string DescriptionOfEnglish { get; set; }

        [StringLength(255)]
        [Column("MO_TA_CV_HOA")]
        public string DescriptionOfChina { get; set; }

        [StringLength(250)]
        [Column("PATH_HD")]
        public string Path { get; set; }

        [StringLength(20)]
        [Column("MS_LOAI_MAY")]
        public string TypeOfDeviceID { get; set; }

        [Column("THOI_GIAN_DU_KIEN")]
        public float? TimePlan { get; set; }

        [StringLength(2000)]
        [Column("THAO_TAC")]
        public string Action { get; set; }

        [StringLength(2000)]
        [Column("TIEU_CHUAN_KT")]
        public string QualityCheck { get; set; }

        [StringLength(2000)]
        [Column("GHI_CHU")]
        public string Note { get; set; }

        [Column("MS_CHUYEN_MON")]
        public int? SpecialtyID { get; set; }

        [Column("MS_BAC_THO")]
        public int? SkillGradeID { get; set; }

        [Column("AN_TOAN")]
        public bool? Safety { get; set; }

        [Column("DINH_MUC")]
        public double? Assess { get; set; }

        [StringLength(6)]
        [Column("NGOAI_TE")]
        public string Currency { get; set; }

        [Column("SO_NGUOI")]
        public int? AmountOfPeople { get; set; }

        [StringLength(1000)]
        [Column("YEU_CAU_NS")]
        public string InquiryHR { get; set; }

        [StringLength(1000)]
        [Column("YEU_CAU_DUNG_CU")]
        public string Tool { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ComponentOfWork> ComponentOfWork { get; set; }

        //public  LOAI_CONG_VIEC LOAI_CONG_VIEC { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public  ICollection<EOR_CONG_VIEC> EOR_CONG_VIEC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<MasterPlansOfWork> MasterPlansOfWork { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<PreventiveMaintenanceOfWork> PreventiveMaintenanceOfWork { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public  ICollection<WorkOrderOfWork> WorkOrderOfWork { get; set; }
    }
}
