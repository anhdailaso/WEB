

--exec ApprovedRequest 'admin' ,0,0,NULL,'06/27/2020','07/27/2020','-1','-1'
ALTER proc [dbo].[ApprovedRequest]
	@username nvarchar(50),
	@option int,
	@lang int,
	@image Image,
	@fromDate DATETIME,
	@toDate DATETIME,
	@workSiteID nvarchar(50),
	@typeOfDeviceID nvarchar(50)
as
begin

SELECT A.* INTO #MAY FROM dbo.MGetMayUserNgay(@toDate,@username,@workSiteID,-1,-1,@typeOfDeviceID,'-1','-1', @lang) A 	
SELECT CONVERT(BIT, 0) AS CHON ,
	CONVERT(Image,CASE (SELECT COUNT(*) FROM YEU_CAU_NSD_CHI_TIET_HINH WHERE STT =B.STT AND STT_VAN_DE =B.STT_VAN_DE) 
	WHEN 0 THEN NULL ELSE @image END) AS TAI_LIEU, 
	B.STT, B.MS_MAY, B.STT_VAN_DE, A.TEN_MAY, B.MO_TA_TINH_TRANG, C.NGAY,C.MS_YEU_CAU, C.SO_YEU_CAU,   A.Ten_N_XUONG, 
	C.NGUOI_YEU_CAU, B.YEU_CAU, B.MS_UU_TIEN, B.USERNAME_DSX,B.THOI_GIAN_DSX, B.Y_KIEN_DSX, B.THUC_HIEN_DSX, B.EMAIL_DSX, 
	B.MS_CACH_TH, dbo.MUC_UU_TIEN.TEN_UU_TIEN,TEN_LOAI_YEU_CAU_BT, (SELECT CONVERT(BIT,CASE COUNT(*) WHEN 0 THEN 0 ELSE 1 END) 
	FROM USERS A INNER JOIN (SELECT MS_TO FROM YEU_CAU_NSD T1 INNER JOIN USERS T2 ON T1.USER_LAP = T2.USERNAME WHERE T1.STT = C.STT ) B ON A.MS_TO = B.MS_TO 
	WHERE USERNAME = @username) AS QUYEN_DUYET INTO #tableDS
FROM #MAY A INNER JOIN dbo.YEU_CAU_NSD_CHI_TIET B ON A.MS_MAY = B.MS_MAY INNER JOIN
	dbo.YEU_CAU_NSD C ON B.STT = C.STT LEFT OUTER JOIN
	dbo.MUC_UU_TIEN ON B.MS_UU_TIEN = dbo.MUC_UU_TIEN.MS_UU_TIEN INNER JOIN 
	LOAI_YEU_CAU_BAO_TRI T1 ON B.MS_LOAI_YEU_CAU_BT = T1.MS_LOAI_YEU_CAU_BT
WHERE C.NGAY BETWEEN @fromDate AND @toDate

if @option =0
	  SELECT CHON [Choose],TAI_LIEU Document,STT ID,MS_MAY DeviceID,STT_VAN_DE UserRequestDetailID,TEN_MAY DeviceName,MO_TA_TINH_TRANG [Description],
			  YEU_CAU Request,TEN_LOAI_YEU_CAU_BT TypeOfMaintenanceName,NGAY DateCreated,MS_YEU_CAU UserRequestID,MS_UU_TIEN PriorityID, SO_YEU_CAU RequestNO, Ten_N_XUONG WorkSiteName,NGUOI_YEU_CAU RequestedBy,TEN_UU_TIEN PriorityName,USERNAME_DSX ApprovedRequestBy,THOI_GIAN_DSX DateApproveRequested,	Y_KIEN_DSX ApprovalNote,THUC_HIEN_DSX IsExecuteApproveRequested,EMAIL_DSX EmailByApproveRequested,MS_CACH_TH ExecuteID,QUYEN_DUYET IsApprovedRequest
	   FROM #tableDS
	   WHERE THUC_HIEN_DSX IS NULL AND QUYEN_DUYET =1  order by MS_UU_TIEN 
if @option =1
		  SELECT CHON [Choose],TAI_LIEU Document,STT ID,MS_MAY DeviceID,STT_VAN_DE UserRequestDetailID,TEN_MAY DeviceName,MO_TA_TINH_TRANG [Description],
			  YEU_CAU Request,TEN_LOAI_YEU_CAU_BT TypeOfMaintenanceName,NGAY DateCreated,MS_YEU_CAU UserRequestID,MS_UU_TIEN PriorityID, SO_YEU_CAU RequestNO, Ten_N_XUONG WorkSiteName,NGUOI_YEU_CAU  RequestedBy,TEN_UU_TIEN PriorityName,USERNAME_DSX ApprovedRequestBy,THOI_GIAN_DSX DateApproveRequested,	Y_KIEN_DSX ApprovalNote,THUC_HIEN_DSX IsExecuteApproveRequested,EMAIL_DSX EmailByApproveRequested,MS_CACH_TH ExecuteID,QUYEN_DUYET IsApprovedRequest
		   FROM #tableDS
		   WHERE THUC_HIEN_DSX =1 AND MS_CACH_TH is null  AND QUYEN_DUYET =1 order by MS_UU_TIEN		   
if @option =2
	   SELECT CHON [Choose],TAI_LIEU Document,STT ID,MS_MAY DeviceID,STT_VAN_DE UserRequestDetailID,TEN_MAY DeviceName, MO_TA_TINH_TRANG [Description],
			  YEU_CAU Request,TEN_LOAI_YEU_CAU_BT TypeOfMaintenanceName,NGAY DateCreated,MS_YEU_CAU UserRequestID,MS_UU_TIEN PriorityID, SO_YEU_CAU RequestNO, Ten_N_XUONG WorkSiteName,NGUOI_YEU_CAU RequestedBy,TEN_UU_TIEN PriorityName,USERNAME_DSX ApprovedRequestBy,THOI_GIAN_DSX DateApproveRequested,	Y_KIEN_DSX ApprovalNote,THUC_HIEN_DSX IsExecuteApproveRequested,EMAIL_DSX EmailByApproveRequested,MS_CACH_TH ExecuteID,QUYEN_DUYET IsApprovedRequest
	   FROM #tableDS
	   WHERE THUC_HIEN_DSX =0 AND QUYEN_DUYET =1  order by MS_UU_TIEN	   
end	   
   