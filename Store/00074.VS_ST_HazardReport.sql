ALTER PROC  [dbo].[VS_ST_HazardReport]
(
	@ACTION nvarchar(250) =NULL,
	@ID int = NULL,
	@DocNum nvarchar(50) =NULL,
	@DocDate datetime =NULL,
	@DocTime datetime =NULL,
	@Createdtime datetime =NULL,
	@CreatedBy nvarchar(250) =NULL,
	@Department nvarchar(250) =NULL,
	@UnsafeCondition bit =NULL,
	@BelongContractor bit =NULL,
	@Operation bit =NULL,
	@NearMiss bit =NULL,
	@HR bit =NULL,
	@UnsafeBehevior bit =NULL,
	@BelongToBSPort bit =NULL,
	@TechHSE bit =NULL,
	@Acount bit =NULL,
	@SatetySuggestion bit =NULL,
	@Tally bit =NULL,
	@Procu bit =NULL,
	@Other bit =NULL,
	@Stopwork bit =NULL,
	@Commercial bit =NULL,
	@HPES1 bit =NULL,
	@HPES2 bit =NULL,
	@HPES3 bit =NULL,
	@HPES4 bit =NULL,
	@HPES5 bit =NULL,
	@HPES6 bit =NULL,
	@Location nvarchar(250) =NULL,
	@Description NVARCHAR(MAX) =NULL,
	@External bit =NULL,
	@Picture image =NULL,
	@TB [ST_HazardReport1] READONLY,
	@TUNGAY DATE =NULL,
	@DENNGAY DATE =NULL,
	@NGUOIPHUTRACH nvarchar(250) =NULL,
	@NGUOIBAOCAO nvarchar(250) =NULL,
	@TRANGTHAI nvarchar(250) =NULL,
	@PHONGBAN nvarchar(250) =NULL,
	@IS_APPROVED bit =NULL,
	@APPROVAL_USER nvarchar(250) =NULL,
	@FORMID nvarchar(250) =NULL,
	@PERSON_IN_CHARGE INT =NULL,
	@USER nvarchar(250) =NULL,
	@RID varchar(MAX) =NULL,
	@REPORT_PARENT nvarchar(250) =NULL,
	@LOAISC NVARCHAR(MAX)=NULL,
	@LOAISC2 NVARCHAR(MAX)=NULL,
	@LOAISC3 NVARCHAR(MAX)=NULL,
	@Environment bit =NULL,
	@IMG_PATCH1 NVARCHAR(MAX)=NULL,
	@IMG_PATCH2 NVARCHAR(MAX)=NULL,
	@External_N NVARCHAR(MAX)=NULL,
	@TT bit =NULL,
	@IS_DELETE BIT=NULL,
	@STOP_WORK VARCHAR(10)=NULL,
	@COMMENT NVARCHAR(MAX)=NULL,
	@NGUOILIENQUAN1 NVARCHAR(300)=NULL,
	@NGUOILIENQUAN2 NVARCHAR(300)=NULL,
	@CLICK_APPROVAL BIT=NULL,
	@RANDOM NVARCHAR(300)=NULL,
	@Image_1 Image = NULL,
	@Image_2 Image = NULL
)
AS
DECLARE @MS_YEU_CAU NVARCHAR(20)
DECLARE @MSYC_MAX INT
DECLARE @STT INT
DECLARE @DEAR NVARCHAR(MAX)
DECLARE @DEAR2 NVARCHAR(MAX)
DECLARE @DSMAIL NVARCHAR(MAX)
DECLARE @DSMAIL2 NVARCHAR(MAX)
DECLARE @DEPT NVARCHAR(MAX)
DECLARE @NGUOITH NVARCHAR(MAX)
DECLARE @DOCNO VARCHAR(20)
DECLARE @DEM INT
DECLARE @SLCT INT
BEGIN
	IF @ACTION ='GET_DOC_ID'
	BEGIN
		SELECT DISTINCT ID FROM DBO.ST_HazardReport WHERE RANDOM=@RANDOM --AND DocNum=@DocNum
	END
	IF @ACTION='IS_CREATE_APPROVAL'
	BEGIN
		SELECT 1
		FROM DBO.ST_HazardReport 
		WHERE ID=@ID AND EXISTS (SELECT * FROM DBO.ST_APPROVAL_USER WHERE FORM_ID='FrmHazardReport' AND (ISNULL(APPROVAL_LEVEL1,'')=DBO.ST_HazardReport.CreatedBy OR ISNULL(APPROVAL_LEVEL2,'')=DBO.ST_HazardReport.CreatedBy))	
	END
	IF @ACTION='GET_SO_LAN_VI_PHAM'
	BEGIN
		
		SELECT COUNT(*)SL
		FROM
		(
		SELECT ISNULL(NGUOILIENQUAN2,'')NGUOILIENQUAN FROM DBO.ST_HazardReport  WHERE ISNULL(NGUOILIENQUAN2,'') <> '' AND ISNULL(NGUOILIENQUAN2,'') LIKE N'%'+@NGUOILIENQUAN2 +'%'
		UNION ALL
		SELECT ISNULL(NGUOILIENQUAN2,'')NGUOILIENQUAN FROM DBO.ST_StopCard WHERE ISNULL(NGUOILIENQUAN2,'') <> '' AND ISNULL(NGUOILIENQUAN2,'') LIKE N'%'+@NGUOILIENQUAN2 +'%'
		UNION ALL
		SELECT ISNULL(NGUOILIENQUAN2,'')NGUOILIENQUAN FROM DBO.ST_IncidentAndAccident  WHERE ISNULL(NGUOILIENQUAN2,'') <> '' AND ISNULL(NGUOILIENQUAN2,'') LIKE N'%'+@NGUOILIENQUAN2 +'%'
		)T
		
	END
	IF @ACTION='GET_REPORTER_APPROVAL'
	BEGIN
		
		SELECT Staffname FROM [dbo].[ST_Safety] WHERE Userlogin=  @USER
		
	END
	IF @ACTION ='LIST_THIET_BI'
	BEGIN
		SELECT ''  [ID],'' [Staffname],''[Description]
		union all
		SELECT DISTINCT   MS_MAY [ID],TEN_MAY [Staffname],''[Description]  FROM MAY WHERE MS_HIEN_TRANG=2
	END
	IF @ACTION ='LIST_NGUYEN_NHAN'
	BEGIN
		SELECT ''  [ID],'' [Staffname],''[Description]
		union all
		SELECT  DISTINCT  MS_NGUYEN_NHAN [ID],TEN_NGUYEN_NHAN  [Staffname],''[Description] FROM NGUYEN_NHAN_DUNG_MAY --ORDER BY MS_NGUYEN_NHAN
	END
	IF @ACTION ='LIST_LOAI_YEU_CAU'
	BEGIN
		SELECT ''  [ID],'' [Staffname],''[Description]
		union all
		SELECT DISTINCT  MS_LOAI_YEU_CAU_BT  [ID],TEN_LOAI_YEU_CAU_BT  [Staffname],''[Description] FROM LOAI_YEU_CAU_BAO_TRI --ORDER BY MS_LOAI_YEU_CAU_BT
	END
	
	
	IF @ACTION ='CHECK_CREATE_APPROVAL'
	BEGIN
		SELECT * 
		FROM DBO.ST_APPROVAL_USER 
		WHERE FORM_ID='FrmHazardReport' AND (ISNULL(APPROVAL_LEVEL1,'')=@USER OR ISNULL(APPROVAL_LEVEL2,'')=@USER)
	END
	IF @ACTION='GET_LIST_BCT'
	BEGIN
		SET @DEPT='0'
		SET @DEPT=ISNULL((SELECT ISNULL(Department,'0')Department FROM DBO.ST_Safety WHERE Userlogin=@CreatedBy),'0')
		IF @DEPT <> '0'
		BEGIN
			SELECT '0' ID,'' [Staff name] ,''[Description]
			UNION
			SELECT DISTINCT ID, Staffname [Staff name] ,Userlogin [Description] FROM [dbo].[ST_Safety] WHERE Department=@DEPT
		END
		ELSE
		BEGIN
			SELECT '0' ID,'' [Staff name] ,''[Description]
			UNION
			SELECT DISTINCT ID, Staffname [Staff name] ,Userlogin [Description] FROM [dbo].[ST_Safety]
		END
	END
	IF @ACTION='GET_LIST_BCT1'
	BEGIN
	
		SELECT '0' ID,'' [Staff name] ,''[Description]
		UNION
		SELECT DISTINCT ID, Staffname [Staff name] ,Userlogin [Description] FROM [dbo].[ST_Safety]
	
	END
	IF @ACTION='CHECK_ADMIN_USER'
	BEGIN
		SELECT * FROM USERS WHERE  GROUP_ID=49 AND USERNAME =@USER
	END
	IF @ACTION='GET_USER_LOGIN'
	BEGIN
		SELECT Userlogin FROM DBO.ST_Safety WHERE ID=@ID
	END
	IF @ACTION='GET_DEPT'
	BEGIN
		SELECT T2.Department 
		FROM  DBO.ST_Safety T2 
		WHERE T2.Userlogin=@REPORT_PARENT 
		UNION ALL
		SELECT T2.Department 
		FROM  DBO.ST_Safety T2 
		WHERE T2.Userlogin=@CreatedBy AND @REPORT_PARENT=''
	END
	IF @ACTION='GET_REPORT_PARENT'
	BEGIN
		--SET @DEPT='0'
		--SET @DEPT=ISNULL((SELECT ISNULL(Department,'0')Department FROM DBO.ST_Safety WHERE Userlogin=@CreatedBy),'0')
		--IF @DEPT <> ''
		--BEGIN
		--	SELECT ' ' [Userlogin],'All' [NPT]
		--	UNION
		--	SELECT DISTINCT Userlogin [Userlogin], Staffname [NPT] FROM [dbo].[ST_Safety] WHERE Department=@DEPT
		--END
		--ELSE
		--BEGIN
		--	SELECT ' ' [Userlogin],'All' [NPT]
		--	UNION
		--	SELECT DISTINCT Userlogin [Userlogin], Staffname [NPT] FROM [dbo].[ST_Safety]
		--END
		SELECT ' ' [Userlogin],'All' [NPT]
		UNION
		SELECT DISTINCT Userlogin [Userlogin], Staffname [NPT] FROM [dbo].[ST_Safety]
	END
	IF @ACTION='GET_REPORT_PARENT2'
	BEGIN
		SET @DEPT='0'
		SET @DEPT=ISNULL((SELECT ISNULL(Department,'0')Department FROM DBO.ST_Safety WHERE Userlogin=@CreatedBy),'0')
		IF @DEPT <> '0'
		BEGIN
			SELECT ' ' [Userlogin],' ' [NPT]
			UNION
			SELECT DISTINCT Userlogin [Userlogin], Staffname [NPT] FROM [dbo].[ST_Safety] WHERE Department=@DEPT
		END
		ELSE
		BEGIN
			SELECT ' ' [Userlogin],' ' [NPT]
			UNION
			SELECT DISTINCT Userlogin [Userlogin], Staffname [NPT] FROM [dbo].[ST_Safety]
		END
	END
	IF @ACTION='GET_REPORT_PARENT3'
	BEGIN
		SELECT ' ' [Userlogin],' ' [NPT]
		UNION
		SELECT DISTINCT Userlogin [Userlogin], Staffname [NPT] FROM [dbo].[ST_Safety]
	END
	IF @ACTION='GET_LIST_ACTION'
	BEGIN
		SELECT  [PreventiveAction] FROM [dbo].[ST_HazardReport1] WHERE [HazardReportID] =@ID ORDER BY ID --AND ISNULL([Status],'') ='Ongoing-Đang xử lý'
	END
	IF @ACTION='GET_LIST_ACTION_DONE'
	BEGIN
		SET @RID=REPLACE(@RID,'|',',')
		DECLARE @SsQL NVARCHAR (MAX)

		--SET @SsQL = '
		--SELECT DISTINCT [PreventiveAction] FROM
		--(
		--	SELECT ROW_NUMBER() OVER(ORDER BY ID ASC) AS [RIndex], [PreventiveAction] 
		--	FROM [dbo].[ST_HazardReport1] 
		--	WHERE [HazardReportID] ='+CAST(@ID AS VARCHAR(10)) +' AND ISNULL([Status],'+CHAR(39)+''+CHAR(39)+') IN ('+CHAR(39)+'Done'+CHAR(39)+','+CHAR(39)+'Reject'+CHAR(39)+')
		--)T
		--WHERE  CAST(T.[RIndex] AS VARCHAR(10)) IN ('+@RID+')'
		--PRINT @SSQL
		--EXEC (@SsQL)
		SET @SsQL = '
		SELECT DISTINCT [PreventiveAction] FROM
		(
			SELECT ROW_NUMBER() OVER(ORDER BY ID ASC) AS [RIndex], [PreventiveAction] 
			FROM [dbo].[ST_HazardReport1] 
			WHERE [HazardReportID] ='+CAST(@ID AS VARCHAR(10)) +'
		)T
		WHERE  CAST(T.[RIndex] AS VARCHAR(10)) IN ('+@RID+')'
		PRINT @SSQL
		EXEC (@SsQL)
	END

	

	IF @ACTION='GET_REPORTER'
	BEGIN
		IF @REPORT_PARENT <> ''
			SELECT Staffname FROM [dbo].[ST_Safety] WHERE Userlogin=  @REPORT_PARENT 
		ELSE
			SELECT Staffname FROM [dbo].[ST_Safety] WHERE Userlogin=  @USER 
	END

	IF @ACTION='GET_DEAR_APPROVAL'
	BEGIN

		SET @DEPT='0'
		IF @REPORT_PARENT <> ''
			SET @DEPT=ISNULL((SELECT ISNULL(Department,'0')Department FROM DBO.ST_Safety WHERE Userlogin=@REPORT_PARENT),'0')
		ELSE
			SET @DEPT=ISNULL((SELECT ISNULL(Department,'0')Department FROM DBO.ST_Safety WHERE Userlogin=@USER),'0')

		SET @DEAR=''
		SELECT @DEAR=Staffname+','+@DEAR 
		FROM [dbo].[ST_Safety] 
		WHERE Userlogin IN (SELECT DISTINCT T.TK 
							FROM 
								(SELECT ISNULL(T1.APPROVAL_LEVEL1,'') TK 
								 FROM [dbo].[ST_APPROVAL_USER] T1 INNER JOIN DBO.ST_Safety T2 ON ISNULL(T1.APPROVAL_LEVEL1,'')=T2.Userlogin
								 WHERE FORM_ID='FrmHazardReport'  AND T2.Department=@DEPT
								 UNION 
								 SELECT ISNULL(T1.APPROVAL_LEVEL2,'') TK 
								 FROM [dbo].[ST_APPROVAL_USER] T1 INNER JOIN DBO.ST_Safety T2 ON ISNULL(T1.APPROVAL_LEVEL2,'')=T2.Userlogin
								 WHERE FORM_ID='FrmHazardReport'  AND T2.Department=@DEPT
								)T
							)
		SET @DEAR =(SELECT SUBSTRING(@DEAR,1,LEN(@DEAR)-1))
		SELECT @DEAR

	END

	IF @ACTION='GET_DEAR_INCHARGE'
	BEGIN
		SET @DEAR=''
		SELECT @DEAR=Staffname+','+@DEAR FROM [dbo].[ST_Safety] WHERE Userlogin IN (SELECT DISTINCT ISNULL(T2.Userlogin,'')Userlogin FROM [dbo].[ST_HazardReport1] T1 INNER JOIN [dbo].[ST_Safety] T2 ON T1.PersonIncharge=T2.ID WHERE [HazardReportID]=@ID)
		SET @DEAR =(SELECT SUBSTRING(@DEAR,1,LEN(@DEAR)-1))
		SELECT @DEAR
	END
	IF @ACTION='GET_DEAR_CREATE_BY'
	BEGIN
		SET @DEAR=''
	
		SET @DEAR2=''
		SELECT @DEAR2=Staffname+','+@DEAR2 FROM [dbo].[ST_Safety] WHERE Userlogin IN(SELECT ISNULL(REPORT_PARENT, ISNULL(CreatedBy,'')) FROM  [dbo].[ST_HazardReport] WHERE ID=@ID	)
		SET @DEAR2 =(SELECT SUBSTRING(@DEAR2,1,LEN(@DEAR2)-1))

		IF LEN(@DEAR)<>0 AND LEN(@DEAR2)<>0
			SELECT @DEAR +','+@DEAR2
		IF LEN(@DEAR)<>0 AND LEN(@DEAR2)=0
			SELECT @DEAR
		IF LEN(@DEAR)=0 AND LEN(@DEAR2)<>0
			SELECT @DEAR2
		IF LEN(@DEAR)=0 AND LEN(@DEAR2)=0
			SELECT ''
	END

	IF @ACTION='GET_DEAR_APPROVAL_CREATE_BY'
	BEGIN
		SET @DEAR=''
		SELECT @DEAR=Staffname+','+@DEAR FROM [dbo].[ST_Safety] WHERE Userlogin IN(SELECT ISNULL(APPROVAL_USER,'') FROM  [dbo].[ST_HazardReport] WHERE ID=@ID)
		IF LEN(@DEAR)<>0
			SET @DEAR =(SELECT SUBSTRING(@DEAR,1,LEN(@DEAR)-1))
		
		SET @DEAR2=''
		SELECT @DEAR2=Staffname+','+@DEAR2 FROM [dbo].[ST_Safety] WHERE Userlogin IN(SELECT ISNULL(REPORT_PARENT, ISNULL(CreatedBy,'')) FROM  [dbo].[ST_HazardReport] WHERE ID=@ID	)
		IF LEN(@DEAR2)<>0
			SET @DEAR2 =(SELECT SUBSTRING(@DEAR2,1,LEN(@DEAR2)-1))

		IF LEN(@DEAR)<>0 AND LEN(@DEAR2)<>0
			SELECT @DEAR +','+@DEAR2
		IF LEN(@DEAR)<>0 AND LEN(@DEAR2)=0
			SELECT @DEAR
		IF LEN(@DEAR)=0 AND LEN(@DEAR2)<>0
			SELECT @DEAR2
		IF LEN(@DEAR)=0 AND LEN(@DEAR2)=0
			SELECT ''
	END

	IF @ACTION='GET_MAIL_APPROVAL'
	BEGIN
		
		SET @DEPT='0'
		IF @REPORT_PARENT <> ''
			SET @DEPT=ISNULL((SELECT ISNULL(Department,'0')Department FROM DBO.ST_Safety WHERE Userlogin=@REPORT_PARENT),'0')
		ELSE
			SET @DEPT=ISNULL((SELECT ISNULL(Department,'0')Department FROM DBO.ST_Safety WHERE Userlogin=@USER),'0')

		SET @DSMAIL=''
		SELECT @DSMAIL=USER_MAIL+';'+@DSMAIL 
		FROM USERS  
		WHERE USERNAME IN (SELECT DISTINCT T.TK FROM ( SELECT ISNULL(T1.APPROVAL_LEVEL1,'') TK 
													   FROM [dbo].[ST_APPROVAL_USER] T1 INNER JOIN DBO.ST_Safety T2 ON ISNULL(T1.APPROVAL_LEVEL1,'')=T2.Userlogin
													   WHERE FORM_ID='FrmHazardReport'  AND T2.Department=@DEPT
													   UNION 
													   SELECT ISNULL(T1.APPROVAL_LEVEL2,'') TK 
													   FROM [dbo].[ST_APPROVAL_USER] T1 INNER JOIN DBO.ST_Safety T2 ON ISNULL(T1.APPROVAL_LEVEL2,'')=T2.Userlogin
													   WHERE FORM_ID='FrmHazardReport'  AND T2.Department=@DEPT
						)T)
				AND USER_MAIL <> ''
		SET @DSMAIL =(SELECT SUBSTRING(@DSMAIL,1,LEN(@DSMAIL)-1))
		SELECT @DSMAIL
	END
	IF @ACTION='GET_MAIL_INCHARGE'
	BEGIN
		
		SET @DSMAIL=''

		SELECT @DSMAIL=USER_MAIL+';'+@DSMAIL 
		FROM USERS 
		WHERE USERNAME IN (SELECT DISTINCT ISNULL(T2.Userlogin,'')Userlogin FROM [dbo].[ST_HazardReport1] T1 INNER JOIN [dbo].[ST_Safety] T2 ON T1.PersonIncharge=T2.ID WHERE [HazardReportID]=@ID)
			AND USER_MAIL <> ''
		SET @DSMAIL =(SELECT SUBSTRING(@DSMAIL,1,LEN(@DSMAIL)-1))
		SELECT @DSMAIL

	END

	IF @ACTION='GET_MAIL_CREATE_BY'
	BEGIN

		SET @DSMAIL2=''
		SET @DSMAIL2=ISNULL((SELECT USER_MAIL
		FROM USERS  
		WHERE USERNAME IN (SELECT ISNULL(CreatedBy,'') FROM  [dbo].[ST_HazardReport] WHERE ID=@ID)
				AND USER_MAIL <> ''),'')
		
		SELECT @DSMAIL2

	END

	IF @ACTION='GET_MAIL_APPROVAL_CREATE_BY'
	BEGIN

		SET @DSMAIL=''
		SELECT @DSMAIL=USER_MAIL+';'+@DSMAIL 
		FROM USERS  
		WHERE USERNAME IN(  SELECT ISNULL(APPROVAL_USER,'') FROM  [dbo].[ST_HazardReport] WHERE ID=@ID	)
				AND USER_MAIL <> ''
		SET @DSMAIL =(SELECT SUBSTRING(@DSMAIL,1,LEN(@DSMAIL)-1))
		

		SET @DSMAIL2=''
		SET @DSMAIL2=ISNULL((SELECT USER_MAIL
		FROM USERS  
		WHERE USERNAME IN (SELECT ISNULL(REPORT_PARENT, ISNULL(CreatedBy,'')) FROM  [dbo].[ST_HazardReport] WHERE ID=@ID)
				AND USER_MAIL <> ''),'')

		IF LEN(@DSMAIL)<>0 AND LEN(@DSMAIL2)<>0
			SELECT @DSMAIL +';'+@DSMAIL2
		IF LEN(@DSMAIL)<>0 AND LEN(@DSMAIL2)=0
			SELECT @DSMAIL
		IF LEN(@DSMAIL)=0 AND LEN(@DSMAIL2)<>0
			SELECT @DSMAIL2
		IF LEN(@DSMAIL)=0 AND LEN(@DSMAIL2)=0
			SELECT ''
	END

	IF @ACTION='CHECK_CANCEL_APPROVAL'
	BEGIN
		SELECT 1 
		FROM ST_HazardReport 
		WHERE ID=@ID AND ISNULL(IS_APPROVED,'False')='True' AND ISNULL(APPROVAL_USER,'') = @APPROVAL_USER
			AND NOT EXISTS(SELECT 1 FROM ST_HazardReport1 WHERE HazardReportID =@ID AND ISNULL([Status],'')=N'Done-Đã hoàn thành' )
	END
	IF @ACTION='CHECK_APPROVED'
	BEGIN
		
		SELECT 1 FROM ST_HazardReport WHERE ID=@ID AND ISNULL(IS_APPROVED,'False')='True' AND ISNULL(APPROVAL_USER,'') <> ''
		
	END
	IF @ACTION='CHECK_USER'
	BEGIN
		SELECT 1 'TT' FROM [dbo].[ST_APPROVAL_USER] WHERE FORM_ID='FrmHazardReport' AND (ISNULL(APPROVAL_LEVEL1,'')=@USER OR ISNULL(APPROVAL_LEVEL2,'')=@USER)
		UNION ALL
		SELECT 1 'TT' FROM [dbo].[ST_HazardReport] T1 WHERE T1.ID=@ID  AND T1.CreatedBy=@USER
		UNION ALL
		SELECT 1 'TT'
		FROM [dbo].[ST_HazardReport] T1 INNER JOIN [dbo].[ST_HazardReport1] T2 ON T1.ID=T2.HazardReportID
			INNER JOIN ST_Safety T3 ON T2.PersonIncharge=T3.ID
		WHERE T1.ID=@ID  AND T3.Userlogin=@USER
		
	END
	IF @ACTION='GET_CHARGE_USER'
	BEGIN
		SELECT [Userlogin] FROM ST_Safety WHERE ID=@PERSON_IN_CHARGE
	END
	IF @ACTION='CHECK_APPROVAL_USER'
	BEGIN
		SELECT 1 FROM ST_APPROVAL_USER WHERE FORM_ID=@FORMID AND (ISNULL(APPROVAL_LEVEL1,'')=@APPROVAL_USER OR ISNULL(APPROVAL_LEVEL2,'')=@APPROVAL_USER)
	END
	IF @ACTION='GET_DOCNUM'
	BEGIN
		
		SET @DOCNO=''
		SELECT @DEM=max(convert(int,SUBSTRING(DocNum,len(LEFT(DocNum,10))+1,LEN(DocNum)) )) FROM [dbo].[ST_HazardReport]
		IF ISNULL(@DEM,0)=0
			SET @DEM=1
		ELSE
			SET @DEM=@DEM+1
		
		SET @DOCNO='HZ' + REPLACE( CONVERT(VARCHAR(10), GETDATE(),104),'.','')+ CASE LEN(@DEM) 
			WHEN 1 THEN '000'+ CONVERT(VARCHAR(4), @DEM) 
			WHEN 2 THEN '00'+ CONVERT(VARCHAR(4), @DEM) 
			WHEN 3 THEN '0'+ CONVERT(VARCHAR(4), @DEM) 
			WHEN 4 THEN  CONVERT(VARCHAR(4), @DEM) 
			
			WHEN 5 THEN '00'+ CONVERT(VARCHAR(7), @DEM) 
			WHEN 6 THEN '0'+ CONVERT(VARCHAR(7), @DEM) 
			WHEN 7 THEN CONVERT(VARCHAR(7), @DEM) 
			END
		SELECT @DOCNO
	END
	IF @ACTION='LIST_DEPARTMENT'
	BEGIN
		SELECT [ID],[Department] FROM	[dbo].[ST_Department] ORDER BY ID
	END
	IF @ACTION='LIST_DEPARTMENT_F'
	BEGIN
		SELECT -1  [ID], 'All' [Department]
		union all
		SELECT [ID],[Department] FROM	[dbo].[ST_Department] ORDER BY ID
	END
	IF @ACTION='LIST_INCHARGE'
	BEGIN
		SELECT [ID],[Staffname] [Staff name],Userlogin [Description] FROM	[dbo].[ST_Safety] ORDER BY ID
	END
	IF @ACTION='LIST'
	BEGIN

		--UPDATE [dbo].[ST_HazardReport1]
		--SET [Status]=N'Overdue-Quá hạn'
		--WHERE ISNULL([CompletedDate],'01/01/1990') ='01/01/1990'
		--	AND DATEDIFF(day,getdate(),[Deadline])<0
		--		AND ISNULL([Status],'') <> N'Reject-Hủy bỏ'
		--UPDATE [dbo].[ST_HazardReport1]
		--SET [Status]=N'Ongoing-Đang xử lý'
		--WHERE ISNULL([CompletedDate],'01/01/1990') ='01/01/1990'
		--	AND DATEDIFF(day,getdate(),[Deadline])>=0
		--		AND ISNULL([Status],'') <> N'Reject-Hủy bỏ'
		--UPDATE [dbo].[ST_HazardReport1]
		--SET [Status]=N'Done-Đã hoàn thành'
		--WHERE ISNULL([CompletedDate],'01/01/1990') <>'01/01/1990'
		--	AND ISNULL([Status],'') <> N'Reject-Hủy bỏ'

		IF OBJECT_ID('TVT_ST_HAZARDREPORT','U') IS NOT NULL
			DROP TABLE TVT_ST_HAZARDREPORT
		IF OBJECT_ID('TVT_ST_HAZARDREPORT1','U') IS NOT NULL
			DROP TABLE TVT_ST_HAZARDREPORT1
		DELETE [dbo].[ST_EDIT_DOCUMENT] WHERE DATEDIFF(minute,[Edit_Time],getdate()) >=15

		SELECT  DISTINCT * 	INTO TVT_ST_HAZARDREPORT
		FROM
		(

		
		SELECT 
		DISTINCT
			T1.[ID],[DocNum],T1.Description,convert(varchar(10),[DocDate],103) [DocDate],convert(varchar(8),[DocTime],114)[DocTime],CASE WHEN  ISNULL(REPORT_PARENT,'')='' THEN [CreatedBy] ELSE REPORT_PARENT END [CreatedBy] ,DBO.FN_GET_NGUOITHUCHIEN(1,T1.ID) [NGUOITH],
			CASE WHEN isnull(IS_APPROVED,'False') ='True' THEN N'Đã duyệt - Approved' ELSE N'Chưa duyệt - Not Approved' END [Status],DBO.FN_VS_ST_GET_STATUS_DOC(1,T1.ID) [TTTH]
			,CASE WHEN ISNULL(T1.NGUOILIENQUAN2,'') <> '' THEN  ISNULL(T5.Staffname,'-')+','+ISNULL(T1.NGUOILIENQUAN2,'-') ELSE  ISNULL(T5.Staffname,'-') END NLQVP
		FROM [dbo].[ST_HazardReport] T1 		INNER JOIN [dbo].[ST_HazardReport1] T3 ON T1.ID=T3.HazardReportID
		LEFT JOIN [dbo].[ST_Safety] T4 ON T3.PersonIncharge=T4.ID
		LEFT JOIN [dbo].[ST_Safety] T5 ON T1.NGUOILIENQUAN1=T5.Userlogin
		WHERE CONVERT(DATE, T1.DocDate) BETWEEN @TUNGAY AND @DENNGAY
			AND ISNULL(T1.[External],'False') LIKE  CASE WHEN @External_N ='All' THEN '%' ELSE @External_N END
				AND T1.Department LIKE CASE WHEN @PHONGBAN='-1' THEN '%' ELSE @PHONGBAN END
				--AND T3.Status LIKE CASE WHEN @TRANGTHAI='All' THEN '%' ELSE @TRANGTHAI END
					AND T4.Staffname LIKE  CASE WHEN @NGUOIPHUTRACH ='All' THEN '%' ELSE @NGUOIPHUTRACH END
						AND T1.CreatedBy LIKE  CASE WHEN @NGUOIBAOCAO ='All' THEN '%' ELSE @NGUOIBAOCAO END
							AND ISNULL(T1.REPORT_PARENT,'') LIKE  CASE WHEN @REPORT_PARENT =' ' THEN '%' ELSE @REPORT_PARENT END
								AND T1.DocNum LIKE CASE WHEN @DocNum='' THEN '%' ELSE '%'+@DocNum+'%' END
									AND isnull(T3.PersonIncharge,'') <> ''
										AND ISNULL(T1.IS_DELETE,'False') =@IS_DELETE
											AND ISNULL(T1.Stopwork,'False') LIKE CASE WHEN @STOP_WORK='ALL' THEN '%' ELSE @STOP_WORK END
												AND ISNULL(T1.NGUOILIENQUAN1,'') LIKE  CASE WHEN @NGUOILIENQUAN1 =' ' THEN '%' ELSE @NGUOILIENQUAN1 END
													AND ISNULL(T1.NGUOILIENQUAN2,'') LIKE  CASE WHEN @NGUOILIENQUAN2 =' ' THEN '%' ELSE N'%'+@NGUOILIENQUAN2+'%' END
		
		UNION ALL	
		SELECT 
		DISTINCT
			T1.[ID],[DocNum],T1.Description,convert(varchar(10),[DocDate],103) [DocDate],convert(varchar(8),[DocTime],114)[DocTime],CASE WHEN  ISNULL(REPORT_PARENT,'')='' THEN [CreatedBy] ELSE REPORT_PARENT END [CreatedBy],'' [NGUOITH],
			CASE WHEN isnull(IS_APPROVED,'False') ='True' THEN N'Đã duyệt - Approved' ELSE N'Chưa duyệt - Not Approved' END [Status],'' [TTTH]
			,CASE WHEN ISNULL(T1.NGUOILIENQUAN2,'') <> '' THEN  ISNULL(T5.Staffname,'-')+','+ISNULL(T1.NGUOILIENQUAN2,'-') ELSE  ISNULL(T5.Staffname,'-') END NLQVP
		FROM [dbo].[ST_HazardReport] T1 
		LEFT JOIN [dbo].[ST_Safety] T5 ON T1.NGUOILIENQUAN1=T5.Userlogin
		WHERE CONVERT(DATE, T1.DocDate) BETWEEN @TUNGAY AND @DENNGAY
			AND ISNULL(T1.[External],'False') LIKE  CASE WHEN @External_N ='All' THEN '%' ELSE @External_N END
				AND T1.Department LIKE CASE WHEN @PHONGBAN='-1' THEN '%' ELSE @PHONGBAN END
						AND T1.CreatedBy LIKE  CASE WHEN @NGUOIBAOCAO ='All' THEN '%' ELSE @NGUOIBAOCAO END
							AND ISNULL(T1.REPORT_PARENT,'') LIKE  CASE WHEN @REPORT_PARENT =' ' THEN '%' ELSE @REPORT_PARENT END
								AND T1.DocNum LIKE CASE WHEN @DocNum='' THEN '%' ELSE '%'+@DocNum+'%' END
			AND NOT EXISTS(SELECT 1 FROM DBO.ST_HazardReport1 WHERE HazardReportID=T1.ID ) AND @TRANGTHAI='All' AND @NGUOIPHUTRACH='All'
			AND ISNULL(T1.IS_DELETE,'False') =@IS_DELETE AND ISNULL(T1.Stopwork,'False') LIKE CASE WHEN @STOP_WORK='ALL' THEN '%' ELSE @STOP_WORK END
														AND ISNULL(T1.NGUOILIENQUAN1,'') LIKE  CASE WHEN @NGUOILIENQUAN1 =' ' THEN '%' ELSE @NGUOILIENQUAN1 END
													AND ISNULL(T1.NGUOILIENQUAN2,'') LIKE  CASE WHEN @NGUOILIENQUAN2 =' ' THEN '%' ELSE N'%'+@NGUOILIENQUAN2+'%' END
		UNION ALL
		SELECT 
		DISTINCT
			T1.[ID],[DocNum],T1.Description,convert(varchar(10),[DocDate],103) [DocDate],convert(varchar(8),[DocTime],114)[DocTime],CASE WHEN  ISNULL(REPORT_PARENT,'')='' THEN [CreatedBy] ELSE REPORT_PARENT END [CreatedBy],'' [NGUOITH],
			CASE WHEN isnull(IS_APPROVED,'False') ='True' THEN N'Đã duyệt - Approved' ELSE N'Chưa duyệt - Not Approved' END [Status],'' [TTTH]
			,CASE WHEN ISNULL(T1.NGUOILIENQUAN2,'') <> '' THEN  ISNULL(T5.Staffname,'-')+','+ISNULL(T1.NGUOILIENQUAN2,'-') ELSE  ISNULL(T5.Staffname,'-') END NLQVP
		FROM [dbo].[ST_HazardReport] T1 INNER JOIN DBO.ST_HazardReport1 T2 ON T1.ID=T2.HazardReportID
		LEFT JOIN [dbo].[ST_Safety] T5 ON T1.NGUOILIENQUAN1=T5.Userlogin
		WHERE CONVERT(DATE, T1.DocDate) BETWEEN @TUNGAY AND @DENNGAY
			AND ISNULL(T1.[External],'False') LIKE  CASE WHEN @External_N ='All' THEN '%' ELSE @External_N END
				AND T1.Department LIKE CASE WHEN @PHONGBAN='-1' THEN '%' ELSE @PHONGBAN END
						AND T1.CreatedBy LIKE  CASE WHEN @NGUOIBAOCAO ='All' THEN '%' ELSE @NGUOIBAOCAO END
							AND ISNULL(T1.REPORT_PARENT,'') LIKE  CASE WHEN @REPORT_PARENT =' ' THEN '%' ELSE @REPORT_PARENT END
								AND T1.DocNum LIKE CASE WHEN @DocNum='' THEN '%' ELSE '%'+@DocNum+'%' END
			AND isnull(T2.PersonIncharge,'')='' AND @TRANGTHAI='All' AND @NGUOIPHUTRACH='All'
				AND NOT EXISTS(SELECT * FROM DBO.ST_HazardReport1 WHERE HazardReportID=T1.ID AND isnull(PersonIncharge,'')<>'')
		AND ISNULL(T1.IS_DELETE,'False') =@IS_DELETE AND ISNULL(T1.Stopwork,'False') LIKE CASE WHEN @STOP_WORK='ALL' THEN '%' ELSE @STOP_WORK END
														AND ISNULL(T1.NGUOILIENQUAN1,'') LIKE  CASE WHEN @NGUOILIENQUAN1 =' ' THEN '%' ELSE @NGUOILIENQUAN1 END
													AND ISNULL(T1.NGUOILIENQUAN2,'') LIKE  CASE WHEN @NGUOILIENQUAN2 =' ' THEN '%' ELSE N'%'+@NGUOILIENQUAN2+'%' END
		)T
		ORDER BY ID


		UPDATE TVT_ST_HAZARDREPORT SET NLQVP=REPLACE(NLQVP,'-,','')
		UPDATE TVT_ST_HAZARDREPORT SET NLQVP=REPLACE(NLQVP,',-','')
		UPDATE TVT_ST_HAZARDREPORT SET NLQVP=REPLACE(NLQVP,'-','')
		IF @TRANGTHAI <> 'All'
		BEGIN
			IF @TRANGTHAI =N'Done-Đã hoàn thành'
			BEGIN
				SELECT DISTINCT * 	INTO TVT_ST_HAZARDREPORT1 FROM TVT_ST_HAZARDREPORT T
				WHERE (T.TTTH = N'Done-Đã hoàn thành' OR T.TTTH=N'Reject-Hủy bỏ')
					AND NOT EXISTS (SELECT 1 
								FROM [dbo].[ST_HazardReport1]  
								WHERE HazardReportID =T.ID  AND ([Status] =N'Ongoing-Đang xử lý' OR  [Status] =N'Overdue-Quá hạn'))
						AND  EXISTS (SELECT 1 
								FROM [dbo].[ST_HazardReport1]  
								WHERE HazardReportID =T.ID  AND [Status] = N'Done-Đã hoàn thành')
				UPDATE TVT_ST_HAZARDREPORT1 
				SET TTTH=N'Done-Đã hoàn thành'
			END
			IF @TRANGTHAI =N'Ongoing-Đang xử lý'
			BEGIN
				SELECT DISTINCT * 	INTO TVT_ST_HAZARDREPORT1 FROM TVT_ST_HAZARDREPORT T
				WHERE T.TTTH =N'Ongoing-Đang xử lý'
					AND NOT EXISTS (SELECT 1 
								FROM [dbo].[ST_HazardReport1]  
								WHERE HazardReportID =T.ID  AND [Status] =N'Overdue-Quá hạn')
				
				UPDATE TVT_ST_HAZARDREPORT1 SET TTTH=N'Ongoing-Đang xử lý'

						
			END
			IF @TRANGTHAI =N'Overdue-Quá hạn'
			BEGIN
				SELECT DISTINCT * 	INTO TVT_ST_HAZARDREPORT1 FROM TVT_ST_HAZARDREPORT T
				WHERE T.TTTH =N'Overdue-Quá hạn'
					--AND NOT EXISTS (SELECT 1 
					--			FROM [dbo].[ST_HazardReport1]  
					--			WHERE HazardReportID =T.ID  AND [Status] =N'Ongoing-Đang xử lý')
				UPDATE TVT_ST_HAZARDREPORT1 SET TTTH=N'Overdue-Quá hạn'
			END
			IF @TRANGTHAI =N'Reject-Hủy bỏ'
			BEGIN
				SELECT DISTINCT * 	INTO TVT_ST_HAZARDREPORT1 FROM TVT_ST_HAZARDREPORT T
				WHERE T.TTTH =N'Reject-Hủy bỏ'
					AND NOT EXISTS (SELECT 1 
								FROM [dbo].[ST_HazardReport1]  
								WHERE HazardReportID =T.ID  AND [Status] <> N'Reject-Hủy bỏ')
				UPDATE TVT_ST_HAZARDREPORT1 SET TTTH=N'Reject-Hủy bỏ'
			END
		END
		ELSE
		BEGIN
			UPDATE TVT_ST_HAZARDREPORT SET TTTH='' WHERE [Status]=N'Chưa duyệt - Not Approved'
			SELECT DISTINCT * 	INTO TVT_ST_HAZARDREPORT1 FROM TVT_ST_HAZARDREPORT
		END

		UPDATE TVT_ST_HAZARDREPORT1 SET TTTH='' WHERE [Status]=N'Chưa duyệt - Not Approved'
		DECLARE @QUERY VARCHAR(8000)
		SET @QUERY =''

		

		IF LEN(@LOAISC)>0
		BEGIN
			
			SET @QUERY ='SELECT T2.* 
			FROM [dbo].[ST_HazardReport] T1 INNER JOIN TVT_ST_HAZARDREPORT1 T2 ON T1.ID=T2.ID
			WHERE '+@LOAISC +' ORDER BY T2.ID'
			PRINT @QUERY
			EXEC(@QUERY)
		END
		ELSE
		BEGIN

			SELECT * FROM TVT_ST_HAZARDREPORT1 ORDER BY ID
		END
		IF OBJECT_ID('TVT_ST_HAZARDREPORT','U') IS NOT NULL
			DROP TABLE TVT_ST_HAZARDREPORT
		IF OBJECT_ID('TVT_ST_HAZARDREPORT1','U') IS NOT NULL
			DROP TABLE TVT_ST_HAZARDREPORT1
	END
	IF @ACTION='GET_BY_ID'
	BEGIN
		SELECT 
			[ID]
			,[DocNum]
			,[DocDate]
			,[DocTime]
			,[Createdtime]
			,[CreatedBy]
			,[Department]
			,[UnsafeCondition]
			,[BelongContractor]
			,[Operation]
			,[HR]
			,[UnsafeBehevior]
			,[BelongToBSPort]
			,[TechHSE]
			,[Acount]
			,[SatetySuggestion]
			,[Tally]
			,[Procu]
			,[Other]
			,[Stopwork]
			,[Commercial]
			,[HPES1]
			,[HPES2]
			,[HPES3]
			,[HPES4]
			,[HPES5]
			,[HPES6]
			,[Location]
			,[Description]
			,isnull([External],'False')[External]
			,NearMiss
			,Picture
			,ISNULL(IS_APPROVED,'False') IS_APPROVED
			,ISNULL(APPROVAL_USER,'') APPROVAL_USER,
			ISNULL([REPORT_PARENT],'')REPORT_PARENT,
			ISNULL([REPORT_PARENT],'')REPORT_PARENT,
			ISNULL(Environment,'False')Environment,
			ISNULL(IMG_PATCH1,'')IMG_PATCH1,
			ISNULL(IMG_PATCH2,'')IMG_PATCH2
			,isnull([TT],'False')[TT]
			,isnull(IS_DELETE,'False')[IS_DELETE],
			ISNULL([COMMENT],'')COMMENT,
			ISNULL([NGUOILIENQUAN1],'')NGUOILIENQUAN1,
			ISNULL([NGUOILIENQUAN2],'')NGUOILIENQUAN2,
			ISNULL([RANDOM],'')RANDOM,
			Image_1,
			Image_2
		FROM [dbo].[ST_HazardReport]
		WHERE ID=@ID
	END
	IF @ACTION='GET_BY_DOC_NUM'
	BEGIN
		SELECT 
			[ID]
			,[DocNum]
			,[DocDate]
			,[DocTime]
			,[Createdtime]
			,[CreatedBy]
			,[Department]
			,[UnsafeCondition]
			,[BelongContractor]
			,[Operation]
			,[HR]
			,[UnsafeBehevior]
			,[BelongToBSPort]
			,[TechHSE]
			,[Acount]
			,[SatetySuggestion]
			,[Tally]
			,[Procu]
			,[Other]
			,[Stopwork]
			,[Commercial]
			,[HPES1]
			,[HPES2]
			,[HPES3]
			,[HPES4]
			,[HPES5]
			,[HPES6]
			,[Location]
			,[Description]
			,isnull([External],'False')[External]
			,NearMiss
			,Picture
			,ISNULL(IS_APPROVED,'False') IS_APPROVED
			,ISNULL(APPROVAL_USER,'') APPROVAL_USER,
			ISNULL([REPORT_PARENT],'')REPORT_PARENT,
			ISNULL(Environment,'False')Environment,
			ISNULL(IMG_PATCH1,'')IMG_PATCH1,
			ISNULL(IMG_PATCH2,'')IMG_PATCH2
			,isnull([TT],'False')[TT]
			,isnull(IS_DELETE,'False')[IS_DELETE],
			ISNULL([COMMENT],'')COMMENT,
			ISNULL([NGUOILIENQUAN1],'')NGUOILIENQUAN1,
			ISNULL([NGUOILIENQUAN2],'')NGUOILIENQUAN2,
			ISNULL([RANDOM],'')RANDOM
		FROM [dbo].[ST_HazardReport]
		WHERE DocNum=@DocNum
	END
	IF @ACTION='GET_DETAIL'
	BEGIN
		UPDATE [dbo].[ST_HazardReport1]
		SET [Status]=N'Overdue-Quá hạn'
		WHERE ISNULL([CompletedDate],'01/01/1990') ='01/01/1990'
			AND DATEDIFF(day,getdate(),[Deadline])<0
				AND [Status] <> N'Reject-Hủy bỏ'
		UPDATE [dbo].[ST_HazardReport1]
		SET [Status]=N'Ongoing-Đang xử lý'
		WHERE ISNULL([CompletedDate],'01/01/1990') ='01/01/1990'
			AND DATEDIFF(day,getdate(),[Deadline])>=0
				AND [Status] <> N'Reject-Hủy bỏ'
		UPDATE [dbo].[ST_HazardReport1]
		SET [Status]=N'Done-Đã hoàn thành'
		WHERE ISNULL([CompletedDate],'01/01/1990') <>'01/01/1990'
			AND [Status] <> N'Reject-Hủy bỏ'
		
		SELECT 
			[ID]
			,[PreventiveAction]
			,[PersonIncharge]
			,[MUCUUTIEN]
			,[Deadline]
			,[CompletedDate]
			,[Status]
			,[PersonReport]
			,[Description]
			,THIETBI
			,LOAIYEUCAU
			,NGUYENNHAN
		FROM [dbo].[ST_HazardReport1]
		WHERE [HazardReportID]=@ID
	END
	IF @ACTION='LIST_LOCATION'
	BEGIN
		SELECT [ID],[LocationName] [Name] FROM [dbo].[ST_Locations]
	END
	IF @ACTION='ADD'
	BEGIN
		IF NOT EXISTS(SELECT * FROM [dbo].[ST_HazardReport] WHERE ID=@ID)
		BEGIN
			
			SET @DOCNO=''
			SELECT @DEM=max(convert(int,SUBSTRING(DocNum,len(LEFT(DocNum,10))+1,LEN(DocNum)) )) FROM [dbo].[ST_HazardReport]
			IF ISNULL(@DEM,0)=0
				SET @DEM=1
			ELSE
				SET @DEM=@DEM+1
		
			SET @DOCNO='HZ' + REPLACE( CONVERT(VARCHAR(10), GETDATE(),104),'.','')+ 
				CASE LEN(@DEM) 
					WHEN 1 THEN '000'+ CONVERT(VARCHAR(4), @DEM) 
					WHEN 2 THEN '00'+ CONVERT(VARCHAR(4), @DEM) 
					WHEN 3 THEN '0'+ CONVERT(VARCHAR(4), @DEM) 
					WHEN 4 THEN  CONVERT(VARCHAR(4), @DEM) 
					
					WHEN 5 THEN '00'+ CONVERT(VARCHAR(7), @DEM) 
					WHEN 6 THEN '0'+ CONVERT(VARCHAR(7), @DEM) 
					WHEN 7 THEN CONVERT(VARCHAR(7), @DEM) 
				END
			
			INSERT INTO [dbo].[ST_HazardReport]
			   ([DocNum]
			   ,[DocDate]
			   ,[DocTime]
			   ,[Createdtime]
			   ,[CreatedBy]
			   ,[Department]
			   ,[UnsafeCondition]
			   ,[BelongContractor]
			   ,[Operation]
			   ,[HR]
			   ,[UnsafeBehevior]
			   ,[BelongToBSPort]
			   ,[TechHSE]
			   ,[Acount]
			   ,[SatetySuggestion]
			   ,[Tally]
			   ,[Procu]
			   ,[Other]
			   ,[Stopwork]
			   ,[Commercial]
			   ,[HPES1]
			   ,[HPES2]
			   ,[HPES3]
			   ,[HPES4]
			   ,[HPES5]
			   ,[HPES6]
			   ,[Location]
			   ,[Description],
			   [External],NearMiss,IS_APPROVED,APPROVAL_USER,[REPORT_PARENT],Environment,IMG_PATCH1,IMG_PATCH2,TT,COMMENT,NGUOILIENQUAN1,NGUOILIENQUAN2,RANDOM,Image_1,Image_2)
			VALUES
			   (@DOCNO
			   ,@DocDate
			   ,@DocTime
			   ,@Createdtime
			   ,@CreatedBy
			   ,@Department
			   ,@UnsafeCondition
			   ,@BelongContractor
			   ,@Operation
			   ,@HR
			   ,@UnsafeBehevior
			   ,@BelongToBSPort
			   ,@TechHSE
			   ,@Acount
			   ,@SatetySuggestion
			   ,@Tally
			   ,@Procu
			   ,@Other
			   ,@Stopwork
			   ,@Commercial
			   ,@HPES1
			   ,@HPES2
			   ,@HPES3
			   ,@HPES4
			   ,@HPES5
			   ,@HPES6
			   ,@Location
			   ,@Description,
			   @External,@NearMiss,@IS_APPROVED,@APPROVAL_USER,@REPORT_PARENT,@Environment,@IMG_PATCH1,@IMG_PATCH2,@TT,@COMMENT,@NGUOILIENQUAN1,@NGUOILIENQUAN2,@RANDOM,@Image_1,@Image_2)
			SET @ID=ISNULL((SELECT MAX(ID) FROM [dbo].[ST_HazardReport]),1)
			
			UPDATE DBO.ST_HazardReport SET APPROVAL_USER=CreatedBy , IS_APPROVED='True' 
			WHERE ID=@ID AND EXISTS (SELECT * FROM DBO.ST_APPROVAL_USER WHERE FORM_ID='FrmHazardReport' AND (ISNULL(APPROVAL_LEVEL1,'')=DBO.ST_HazardReport.CreatedBy OR ISNULL(APPROVAL_LEVEL2,'')=DBO.ST_HazardReport.CreatedBy))
			
			IF LEN(@NGUOILIENQUAN2)>0
			BEGIN
				IF NOT EXISTS	(SELECT * FROM ST_DT_NGOAICANG WHERE  UPPER([TENDOITUONG]) = UPPER(@NGUOILIENQUAN2) AND @NGUOILIENQUAN2 <> '')
				INSERT INTO ST_DT_NGOAICANG([TENDOITUONG],[REF_TYPE])
				SELECT @NGUOILIENQUAN2,1
			END
		END

		INSERT INTO [dbo].[ST_HazardReport1]
           ([HazardReportID]
           ,[PreventiveAction]
           ,[PersonIncharge]
           ,[Deadline]
		   ,[CompletedDate]
		   ,[Status]
		   ,[PersonReport]
		   ,[Description],MUCUUTIEN,[THIETBI],	[LOAIYEUCAU],	[NGUYENNHAN])
		SELECT @ID,[PreventiveAction],[PersonIncharge],[Deadline],[CompletedDate] ,[Status],[PersonReport],[Description],MUCUUTIEN,[THIETBI],	[LOAIYEUCAU],	[NGUYENNHAN] FROM @TB WHERE ISNULL(ID,-1) NOT IN (SELECT ID FROM [dbo].[ST_HazardReport1])
		
		---######START######TAO PHIEU YEU CAU TU DONG NEU CHUNG TU DA DUOC DUYET######START######---
		IF EXISTS(SELECT * FROM DBO.ST_HazardReport WHERE ID=@ID AND  IS_APPROVED='True')
		BEGIN
			
			SET @SLCT=0
			SELECT @SLCT=COUNT(*)
			FROM DBO.ST_HazardReport T1 INNER JOIN DBO.ST_HazardReport1 T2 ON T1.ID=T2.HazardReportID
			WHERE T1.ID=@ID AND ISNULL(T2.THIETBI,'') <> '' AND ISNULL(T2.LOAIYEUCAU,-1) <> -1 AND ISNULL(T2.NGUYENNHAN,-1) <> -1 AND ISNULL(T2.MUCUUTIEN,-1) <> -1
			
			IF @SLCT >0
			BEGIN
				SET @MS_YEU_CAU=''
				SELECT @MSYC_MAX=MAX(right(MS_YEU_CAU,6)) FROM [dbo].[YEU_CAU_NSD]
				IF ISNULL(@MSYC_MAX,0)=0
					SET @MSYC_MAX=1
				ELSE
					SET @MSYC_MAX=@MSYC_MAX+1
				SET @MS_YEU_CAU='WR-' + CAST( YEAR(GETDATE()) AS VARCHAR(4))+ CAST( REPLACE(STR(MONTH(GETDATE()), 2, 0), ' ', '0') AS VARCHAR(2)) + REPLACE(STR(@MSYC_MAX, 6, 0), ' ', '0')

				WHILE  EXISTS(SELECT 1 FROM [dbo].[YEU_CAU_NSD] WHERE MS_YEU_CAU=@MS_YEU_CAU)
				BEGIN
					IF ISNULL(@MSYC_MAX,0)=0
						SET @MSYC_MAX=1
					ELSE
						SET @MSYC_MAX=@MSYC_MAX+1
					SET @MS_YEU_CAU='WR-' + CAST( YEAR(GETDATE()) AS VARCHAR(4))+ CAST( REPLACE(STR(MONTH(GETDATE()), 2, 0), ' ', '0') AS VARCHAR(2)) + REPLACE(STR(@MSYC_MAX, 6, 0), ' ', '0')
				END
				
				INSERT INTO YEU_CAU_NSD(MS_YEU_CAU,SO_YEU_CAU,NGAY,GIO_YEU_CAU,NGUOI_YEU_CAU,MS_N_XUONG,REF_NO,REF_ID,USER_COMMENT,USER_LAP)
				
				SELECT @MS_YEU_CAU,@MS_YEU_CAU,DocDate,DocTime,ISNULL( t.REPORT_PARENT,T.CreatedBy),'BARI',T.DocNum,T.ID,t.COMMENT,ISNULL( t.REPORT_PARENT,T.CreatedBy)
				FROM DBO.ST_HazardReport T
				WHERE ID=@ID
				
				
				SET @STT=ISNULL((SELECT TOP 1 STT FROM YEU_CAU_NSD WHERE MS_YEU_CAU=@MS_YEU_CAU),0)
				
				INSERT INTO YEU_CAU_NSD_CHI_TIET(STT,MS_MAY,MO_TA_TINH_TRANG,MS_LOAI_YEU_CAU_BT,MS_NGUYEN_NHAN,REF_TYPE,REF_NO,REF_ID,DETAIL_TYPE,LINE_ID,MS_UU_TIEN,USERNAME_DSX,THOI_GIAN_DSX,Y_KIEN_DSX,THUC_HIEN_DSX,YEU_CAU)
				SELECT @STT,T2.THIETBI,T1.[Description],T2.LOAIYEUCAU,T2.NGUYENNHAN,'HZ' ,T1.DocNum,T1.ID,'PreventiveAction',T2.ID,T2.MUCUUTIEN,T1.APPROVAL_USER,GETDATE(),N'Duyệt tự động từ Safety (Chứng từ (Ecomaint AT): ' + t1.DocNum+')',1,T2.PreventiveAction
				FROM DBO.ST_HazardReport T1 INNER JOIN DBO.ST_HazardReport1 T2 ON T1.ID=T2.HazardReportID
				WHERE T1.ID=@ID AND ISNULL(T2.THIETBI,'') <> '' AND ISNULL(T2.LOAIYEUCAU,-1) <> -1 AND ISNULL(T2.NGUYENNHAN,-1) <> -1 AND ISNULL(T2.MUCUUTIEN,-1) <> -1
			END
		END
		---######END######TAO PHIEU YEU CAU TU DONG NEU CHUNG TU DA DUOC DUYET######END######---
	END
	IF @ACTION='UPDATE'
	BEGIN
		IF EXISTS(SELECT * FROM [dbo].[ST_HazardReport] WHERE ID=@ID)
		BEGIN
			UPDATE [dbo].[ST_HazardReport]
			SET [DocNum] = @DocNum
				,[DocDate] = @DocDate
				,[DocTime] = @DocTime
				,[Createdtime] = @Createdtime
				,[CreatedBy] = @CreatedBy
				,[Department] = @Department
				,[UnsafeCondition] = @UnsafeCondition
				,[BelongContractor] = @BelongContractor
				,[Operation] = @Operation
				,[HR] = @HR
				,[UnsafeBehevior] = @UnsafeBehevior
				,[BelongToBSPort] = @BelongToBSPort
				,[TechHSE] = @TechHSE
				,[Acount] = @Acount
				,[SatetySuggestion] = @SatetySuggestion
				,[Tally] = @Tally
				,[Procu] = @Procu
				,[Other] = @Other
				,[Stopwork] = @Stopwork
				,[Commercial] = @Commercial
				,[HPES1] = @HPES1
				,[HPES2] = @HPES2
				,[HPES3] = @HPES3
				,[HPES4] = @HPES4
				,[HPES5] = @HPES5
				,[HPES6] = @HPES6
				,[Location] = @Location
				,[Description] = @Description,
				[External]=@External,
				NearMiss=@NearMiss,
				IS_APPROVED=@IS_APPROVED,
				APPROVAL_USER=@APPROVAL_USER,
				[REPORT_PARENT]=@REPORT_PARENT,
				Environment=@Environment,IMG_PATCH1=@IMG_PATCH1,IMG_PATCH2=@IMG_PATCH2,TT=@TT,[COMMENT] = @COMMENT,
				NGUOILIENQUAN1=@NGUOILIENQUAN1,NGUOILIENQUAN2=@NGUOILIENQUAN2,RANDOM=@RANDOM
			WHERE ID=@ID
			
			DELETE [dbo].[ST_HazardReport1] WHERE [HazardReportID]=@ID
			INSERT INTO [dbo].[ST_HazardReport1]
				([HazardReportID]
				,[PreventiveAction]
				,[PersonIncharge]
				,[Deadline]
				,[CompletedDate]
				,[Status]
				,[PersonReport]
				,[Description],MUCUUTIEN,[THIETBI],	[LOAIYEUCAU],	[NGUYENNHAN])
			SELECT @ID,[PreventiveAction],[PersonIncharge],[Deadline],[CompletedDate] ,[Status],[PersonReport],[Description],MUCUUTIEN,[THIETBI],	[LOAIYEUCAU],	[NGUYENNHAN] FROM @TB WHERE ISNULL(ID,-1) NOT IN (SELECT ID FROM [dbo].[ST_HazardReport1])
		
			
			---######START######TAO PHIEU YEU CAU TU DONG NEU CHUNG TU DA DUOC DUYET######START######---
		IF ISNULL(@CLICK_APPROVAL,'False')='True' AND EXISTS(SELECT * FROM DBO.ST_HazardReport WHERE ID=@ID AND  IS_APPROVED='True')
		BEGIN

			SET @SLCT=0
			SELECT @SLCT=COUNT(*)
			FROM DBO.ST_HazardReport T1 INNER JOIN DBO.ST_HazardReport1 T2 ON T1.ID=T2.HazardReportID
			WHERE T1.ID=@ID AND ISNULL(T2.THIETBI,'') <> '' AND ISNULL(T2.LOAIYEUCAU,-1) <> -1 AND ISNULL(T2.NGUYENNHAN,-1) <> -1 AND ISNULL(T2.MUCUUTIEN,-1) <> -1
			IF @SLCT >0 
			BEGIN
				SET @MS_YEU_CAU=''
				SELECT @MSYC_MAX=MAX(right(MS_YEU_CAU,6)) FROM [dbo].[YEU_CAU_NSD]
				IF ISNULL(@MSYC_MAX,0)=0
					SET @MSYC_MAX=1
				ELSE
					SET @MSYC_MAX=@MSYC_MAX+1
				SET @MS_YEU_CAU='WR-' + CAST( YEAR(GETDATE()) AS VARCHAR(4))+ CAST( REPLACE(STR(MONTH(GETDATE()), 2, 0), ' ', '0') AS VARCHAR(2)) + REPLACE(STR(@MSYC_MAX, 6, 0), ' ', '0')

				WHILE  EXISTS(SELECT 1 FROM [dbo].[YEU_CAU_NSD] WHERE MS_YEU_CAU=@MS_YEU_CAU)
				BEGIN
					IF ISNULL(@MSYC_MAX,0)=0
						SET @MSYC_MAX=1
					ELSE
						SET @MSYC_MAX=@MSYC_MAX+1
					SET @MS_YEU_CAU='WR-' + CAST( YEAR(GETDATE()) AS VARCHAR(4))+ CAST( REPLACE(STR(MONTH(GETDATE()), 2, 0), ' ', '0') AS VARCHAR(2)) + REPLACE(STR(@MSYC_MAX, 6, 0), ' ', '0')
				END
				
				SELECT @DOCNO= DocNum  FROM DBO.ST_HazardReport WHERE ID=@ID 
				
				DELETE YEU_CAU_NSD_CHI_TIET WHERE REF_TYPE='HZ' AND REF_ID=@ID
				DELETE YEU_CAU_NSD WHERE REF_NO=@DOCNO AND REF_ID=@ID
				
				INSERT INTO YEU_CAU_NSD(MS_YEU_CAU,SO_YEU_CAU,NGAY,GIO_YEU_CAU,NGUOI_YEU_CAU,MS_N_XUONG,REF_NO,REF_ID,USER_COMMENT,USER_LAP)
				
				SELECT @MS_YEU_CAU,@MS_YEU_CAU,DocDate,DocTime,ISNULL( t.REPORT_PARENT,T.CreatedBy),'BARI',T.DocNum,T.ID,T.COMMENT,ISNULL( t.REPORT_PARENT,T.CreatedBy)
				FROM DBO.ST_HazardReport T
				WHERE ID=@ID
				
				
				SET @STT=ISNULL((SELECT TOP 1 STT FROM YEU_CAU_NSD WHERE MS_YEU_CAU=@MS_YEU_CAU),0)
				
				INSERT INTO YEU_CAU_NSD_CHI_TIET(STT,MS_MAY,MO_TA_TINH_TRANG,MS_LOAI_YEU_CAU_BT,MS_NGUYEN_NHAN,REF_TYPE,REF_NO,REF_ID,DETAIL_TYPE,LINE_ID,MS_UU_TIEN,USERNAME_DSX,THOI_GIAN_DSX,Y_KIEN_DSX,THUC_HIEN_DSX,YEU_CAU )
				SELECT @STT,T2.THIETBI,T1.[Description],T2.LOAIYEUCAU,T2.NGUYENNHAN,'HZ' ,T1.DocNum,T1.ID,'PreventiveAction',T2.ID,T2.MUCUUTIEN,T1.APPROVAL_USER,GETDATE(),N'Duyệt tự động từ Safety (Chứng từ (Ecomaint AT): ' + t1.DocNum+')',1,T2.PreventiveAction
				FROM DBO.ST_HazardReport T1 INNER JOIN DBO.ST_HazardReport1 T2 ON T1.ID=T2.HazardReportID
				WHERE T1.ID=@ID AND ISNULL(T2.THIETBI,'') <> '' AND ISNULL(T2.LOAIYEUCAU,-1) <> -1 AND ISNULL(T2.NGUYENNHAN,-1) <> -1 AND ISNULL(T2.MUCUUTIEN,-1) <> -1
			END
		END
		---######END######TAO PHIEU YEU CAU TU DONG NEU CHUNG TU DA DUOC DUYET######END######---
			
			
			IF LEN(@NGUOILIENQUAN2)>0
			BEGIN
				IF NOT EXISTS	(SELECT * FROM ST_DT_NGOAICANG WHERE  UPPER([TENDOITUONG]) = UPPER(@NGUOILIENQUAN2) AND @NGUOILIENQUAN2 <> '')
				INSERT INTO ST_DT_NGOAICANG([TENDOITUONG],[REF_TYPE])
				SELECT @NGUOILIENQUAN2,1
			END
		END
	END
	IF @ACTION='DELETE'
	BEGIN
		UPDATE [dbo].[ST_HazardReport] SET IS_DELETE='True' WHERE ID=@ID
		--IF EXISTS(SELECT * FROM [dbo].[ST_HazardReport] WHERE ID=@ID)
		--BEGIN
		--	DELETE [dbo].[ST_HazardReport] WHERE ID=@ID
		--END
		--IF EXISTS(SELECT * FROM [dbo].[ST_HazardReport1] WHERE [HazardReportID]=@ID)
		--BEGIN
		--	DELETE [dbo].[ST_HazardReport1] WHERE [HazardReportID]=@ID
		--END
	END
	IF @ACTION='PRINT'
	BEGIN
		SELECT 
			t1.[ID]
			,[DocNum]
			,[DocDate]
			,[DocTime]
			,[Createdtime]
			,[CreatedBy]
			,t3.Department
			,[UnsafeCondition]
			,[BelongContractor]
			,[Operation]
			,[HR]
			,[UnsafeBehevior]
			,[BelongToBSPort]
			,[TechHSE]
			,[Acount]
			,[SatetySuggestion]
			,[Tally]
			,[Procu]
			,[Other]
			,[Stopwork]
			,[Commercial]
			,[HPES1]
			,[HPES2]
			,[HPES3]
			,[HPES4]
			,[HPES5]
			,[HPES6]
			,t2.[LocationName][Location]
			,t1.[Description],
			[External],
			t1.NearMiss
		FROM [dbo].[ST_HazardReport] t1 inner join [dbo].[ST_Locations] t2 on t1.Location=t2.id
			inner join [dbo].[ST_Department] t3 on t1.Department=t3.ID
		WHERE t1.ID=@ID

		SELECT t1.ID,t1.HazardReportID,t1.PreventiveAction,t2.Staffname,t1.Deadline FROM [dbo].[ST_HazardReport1] t1 inner join [dbo].[ST_Safety] t2 on t1.PersonIncharge=t2.ID WHERE [HazardReportID]=@ID
	END 
	IF @ACTION='PRINT_NEW'
	BEGIN
		SELECT 
			t1.[ID]
			,[DocNum]
			,[DocDate]
			,[DocTime]
			,[Createdtime]
			,[CreatedBy]
			,t3.Department
			,[UnsafeCondition]
			,[BelongContractor]
			,[Operation]
			,[HR]
			,[UnsafeBehevior]
			,[BelongToBSPort]
			,[TechHSE]
			,[Acount]
			,[SatetySuggestion]
			,[Tally]
			,[Procu]
			,[Other]
			,[Stopwork]
			,[Commercial]
			,[HPES1]
			,[HPES2]
			,[HPES3]
			,[HPES4]
			,[HPES5]
			,[HPES6]
			,t2.[LocationName][Location]
			,t1.[Description],
			[External],
			t1.NearMiss
			,t1.Picture
		FROM [dbo].[ST_HazardReport] t1 inner join [dbo].[ST_Locations] t2 on t1.Location=t2.id
			inner join [dbo].[ST_Department] t3 on t1.Department=t3.ID
				
		WHERE t1.ID=@ID

		SELECT t1.ID,t1.HazardReportID,t1.PreventiveAction,t2.Staffname,t1.Deadline,CompletedDate,t1.[Status] FROM [dbo].[ST_HazardReport1] t1 inner join [dbo].[ST_Safety] t2 on t1.PersonIncharge=t2.ID WHERE [HazardReportID]=@ID
	END 
END

