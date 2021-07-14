namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("THONG_TIN_CHUNG")]
    public partial class General
    {
        [Key]
        [Column("STT")]
        public int ID { get; set; }

        [StringLength(150)]
        [Column("TEN_CTY_TIENG_VIET")]
        public string CompanyName { get; set; }

        [StringLength(150)]
        [Column("TEN_CTY_TIENG_ANH")]
        public string CompanyNameOfEng { get; set; }

        [StringLength(50)]
        [Column("TEN_NGAN_TIENG_VIET")]
        public string CompanyShortName { get; set; }

        [StringLength(50)]
        [Column("TEN_NGAN_TIENG_ANH")]
        public string CompanyShortNameOfEng { get; set; }

        [Column("LOGO", TypeName = "image")]
        public byte[] Logo { get; set; }

        [StringLength(100)]
        [Column("LOGO_PATH")]
        public string PathLogo { get; set; }

        [StringLength(100)]
        [Column("DIA_CHI_VIET")]
        public string Address { get; set; }

        [StringLength(100)]
        [Column("DIA_CHI_ANH")]
        public string AddressOfEng { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(15)]
        public string Fax { get; set; }

        [Column("WIDTH")]
        public int? Width { get; set; }

        [Column("HEIGHT")]
        public int? Height { get; set; }

        [Column("TI_LE_PHAN_TRAM")]
        public int? Percentage { get; set; }

        [Column("STRETCH")]
        public bool? IsStretch { get; set; }

        [Column("LE_PHAI_LOGO")]
        public int? MarginRight { get; set; }

        [Column("LE_TREN_LOGO")]
        public int? MarginTop { get; set; }

        [Column("LOGO_TEN_CTY")]
        //BetWeenLogo&Name
        public int? Distance { get; set; }

        [StringLength(20)]
        
        [Column("LIM_NUMBER")]
        public string QuantityLimit { get; set; }

        [StringLength(20)]
        [Column("USE_NUMBER")]
        public string LicenseNumber { get; set; }

        [Column("NGAY_TAO")]
        public DateTime? DateCreated { get; set; }

        [Column("VI_TRI_KHO")]
        public bool? IsWarehouseLocation { get; set; }

        [StringLength(255)]
        [Column("DUONG_DAN_TL")]
        public string PathOfDocument { get; set; }

        [Column("LENGTH")]
        public int? Length { get; set; }

        [Column("LIM_DATE")]
        public DateTime? DateLimitted { get; set; }

        [Column("USED_DATE")]
        public DateTime? DateOfUsed { get; set; }

        [Column("AUTO_TGNM")]
        public bool? IsRuntime { get; set; }

        [StringLength(255)]
        [Column("PRIVATE")]
        public string Private { get; set; }

        [Column("PBT_KHO")]
        public bool? PBT_KHO { get; set; }

        public int? SO_PHUT_HIEN_MSG { get; set; }

        public bool DDH_DXMH { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        public bool? CPNC_DINHMUC { get; set; }

        [StringLength(255)]
        public string PHIEU_XUAT_VTPT { get; set; }

        public bool? HCG { get; set; }

        [Column("ISAUTO")]
        public bool? IsAuto { get; set; }

        [StringLength(250)]

        [Column("TAI_KHOAN")]
        public string AccountName { get; set; }

        [StringLength(100)]
        [Column("TAI_KHOAN_ANH")]
        public string AccountNameOfEng { get; set; }

        [StringLength(100)]
        [Column("MS_THUE")]
        public string TaxID { get; set; }

        [Column("CAP_NHAP")]
        public bool? IsUpdate { get; set; }

        [StringLength(100)]
        [Column("VER")]
        public string VersionNumber { get; set; }

        [Column("GSTT")]
        public bool? IsMonitoring { get; set; }

        public bool? TT_NX { get; set; }

        [Column("KHO_MOI")]
        public bool? NewWarehouse { get; set; }

        [StringLength(250)]
        [Column("MAIL_FROM")]
        public string FromMail { get; set; }

        [StringLength(250)]
        [Column("PASS_MAIL")]
        public string PassOfMail { get; set; }

        [StringLength(250)]
        [Column("SMTP_MAIL")]
        public string Protocol { get; set; }

        [StringLength(250)]
        [Column("PORT_MAIL")]
        public string Port { get; set; }

        public bool? SENT_MAIL { get; set; }

        public bool? KIEM_PCCV { get; set; }

        public int? HIEN_THI_YCBT { get; set; }

        [Column("LOAI_GOI_MAIL")]
        public int? TypeOfSendMail { get; set; }

        [StringLength(2000)]
        [Column("DUONG_DAN_MAIL")]
        public string PartOfMail { get; set; }

        [Column("VAT_TU_PT")]
        public int? IsMaterial { get; set; }

        public bool? YEU_CAU_BT { get; set; }

        public bool? HIEN_TK_CT { get; set; }

        public int? MREPORT { get; set; }

        public int? MDEFAULT { get; set; }

        public int? TTC_CAO { get; set; }

        public int? TTC_RONG { get; set; }

        [Column("SO_LE_SL")]
        public int? SO_LE_SL { get; set; }

        public int? SO_LE_DG { get; set; }

        public int? SO_LE_TT { get; set; }

        public int? TRANSFER_DATA { get; set; }

        public int? PBT_THEO_GIO { get; set; }

        public int? DN_PBT { get; set; }

        [Column("NGUOI_GIAM_SAT")]
        public bool? IsMonitoredBy { get; set; }

        [Column("VER_HT")]
        public double? DatabaseVersionNO { get; set; }

        [Column("VER_KH")]
        public double? PrivateVersionNO { get; set; }

        public bool? TTBT { get; set; }

        [Column("SSL_MAIL")]
        public bool? IsUsingSSLProtocol { get; set; }

        [Column("VER_TB")]
        public double? TableVersionNO { get; set; }

        [Column("AUTO_CN")]
        public int? InsertStaffIntoWO { get; set; }

        [Column("DOI_FONT")]
        public bool? IsChangeFont { get; set; }

        [StringLength(255)]
        [Column("FONT_REPORT")]
        public string FontReport { get; set; }

        public int? THEM_CV_PT { get; set; }

        public int? PHUT_GIO_PBT { get; set; }

        [StringLength(250)]
        [Column("FONT_FORM")]
        public string FontStyle { get; set; }

        [Column("FONT_SIZE")]
        public double? FontSize { get; set; }

        [Column("VER_LAN")]
        public double? LanguageVersionNo { get; set; }

        [StringLength(250)]
        [Column("NGAY_LIC")]
        public string LicensingDate { get; set; }

        [Column("LOAI_CN")]
        public int? TypeOfUpdate { get; set; }

        [Column("LINK1")]
        public string URLUpdate { get; set; }

        [Column("LINK2")]   
        public string URLVersion { get; set; }

        [Column("LINK3")]
        public string URLLan { get; set; }

        [Column("MUC_UU_TIEN")]
        public bool? Priority { get; set; }

        [Column("TTCT")]
        public bool? IsUsingTheStateDetails { get; set; }

        [Column("DUYET_YC")]
        public bool? IsApproveRequest { get; set; }
    }
}
