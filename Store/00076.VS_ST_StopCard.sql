
ALTER PROC  [dbo].[VS_ST_StopCard]
(
	@ACTION nvarchar(250) =NULL,
	@ID int  =NULL,
	@CreatedBy nvarchar(250) =NULL,
	@DocNum nvarchar(50) =NULL,
	@DocDate datetime =NULL,
	@DocTime datetime =NULL,
	@AdjPerPPE bit =NULL,
	@StopJob bit =NULL,
	@ChangingPosition bit =NULL,
	@RearrangingJob bit =NULL,
	@AttachingGrouds bit =NULL,
	@PerformingLocks bit =NULL,
	@OtherReaction bit= NULL,
	@Head bit =NULL,
	@ArmsAndHands bit =NULL,
	@EyesAndFace bit =NULL,
	@Trunk bit =NULL,
	@Ears bit =NULL,
	@LegsAndFeet bit =NULL,
	@RespiratorySystem bit =NULL,
	@OtherPPE bit =NULL,
	@HPES1 bit =NULL,
	@HPES2 bit =NULL,
	@HPES3 bit =NULL,
	@HPES4 bit =NULL,
	@HPES5 bit =NULL,
	@HPES6 bit =NULL,
	@StrikingAgaintObjects bit =NULL,
	@ChokingHazardousAir bit =NULL,
	@CaughtInOn bit =NULL,
	@Inhaling bit =NULL,
	@Falling bit =NULL,
	@RepettiveMotions bit =NULL,
	@Contacting bit =NULL,
	@PositionsToWork bit =NULL,
	@ContactSharpObject bit =NULL,
	@FlyingObjects bit =NULL,
	@ContactingElectric bit =NULL,
	@EmployeeWork bit =NULL,
	@NotRight bit =NULL,
	@Unknow bit =NULL,
	@UsedIncorrectly bit =NULL,
	@NotUnderstood bit =NULL,
	@InUnsafeCondition bit =NULL,
	@NotFlowed bit =NULL,
	@NotAequate bit =NULL,
	@OtherProcedure bit =NULL,
	@OtherTools bit =NULL,
	@NotIsolate bit =NULL,
	@ToolAndEquipment bit =NULL,
	@NoCollectGarbage bit =NULL,
	@EmergencyExit bit =NULL,
	@Slip bit =NULL,
	@OtherOderLine bit =NULL,
	@External bit =NULL,

	@DUNG_CU_THIET_BI bit =NULL,
	@KHU_VUC_LAM_VIEC bit =NULL,
	@MOI_TRUONG_LAM_VIEC bit =NULL,
	@SACH_SE_NGAN_NAP bit =NULL,
	@LOAI_CONG_VIEC nvarchar(MAX) =NULL,
	@HO_TEN_CA_NHAN_TO_NHOM nvarchar(MAX) =NULL,
	@HANH_DONG_HANH_VI_AN_TOAN nvarchar(MAX) =NULL,
	@HANH_VI_KHONG_AN_TOAN nvarchar(MAX) =NULL,
	@KHAC_PHUC_TUC_THI nvarchar(MAX) =NULL,
	@TB [ST_StopCard1] READONLY,
	@TB2 [ST_StopCard2] READONLY,
	@TUNGAY DATE =NULL,
	@DENNGAY DATE =NULL,
	@NGUOIPHUTRACH nvarchar(250) =NULL,
	@NGUOIBAOCAO nvarchar(250) =NULL,
	@TRANGTHAI nvarchar(250) =NULL,
	@IS_APPROVED bit =NULL,
	@APPROVAL_USER nvarchar(250) =NULL,
	@FORMID nvarchar(250) =NULL,
	@PERSON_IN_CHARGE INT =NULL,
	@USER nvarchar(250) =NULL,
	@RID varchar(MAX) =NULL,
	@RID2 varchar(MAX) =NULL,
	@LOAISC NVARCHAR(MAX)=NULL,
    @TOtherOrderliness NVARCHAR(MAX) =NULL,
    @TOtherPPE NVARCHAR(MAX) =NULL,
    @TOtherProcedure NVARCHAR(MAX) =NULL,
    @TOtherReactions NVARCHAR(MAX) =NULL,
    @TOtherToolAndEquipment NVARCHAR(MAX) =NULL,
	@Description NVARCHAR(MAX) =NULL,
	@External_N NVARCHAR(MAX) =NULL,
	@REPORT_PARENT nvarchar(250) =NULL,
	@IMG_PATCH1 NVARCHAR(MAX)=NULL,
	@IMG_PATCH2 NVARCHAR(MAX)=NULL,
	@Image_1 IMAGE = NULL,
	@Image_2 IMAGE = NULL,
	@PHONGBAN nvarchar(250) =NULL,
	@IS_DELETE bit =NULL,
	@NGUOILIENQUAN1 NVARCHAR(300)=NULL,
	@NGUOILIENQUAN2 NVARCHAR(300)=NULL,
	@CLICK_APPROVAL BIT=NULL
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
DECLARE @DOCNO VARCHAR(20)
DECLARE @DEM INT
DECLARE @SLCT INT
BEGIN
	IF @ACTION ='GET_DOC_ID'
	BEGIN
		SELECT DISTINCT ID FROM DBO.ST_StopCard WHERE DocNum=@DocNum
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
		WHERE FORM_ID='FrmStopCard' AND (ISNULL(APPROVAL_LEVEL1,'')=@USER OR ISNULL(APPROVAL_LEVEL2,'')=@USER)
	END
	IF @ACTION='GET_LIST_BCT'
	BEGIN
		SET @DEPT='0'
		SET @DEPT=ISNULL((SELECT ISNULL(Department,'0')Department FROM DBO.ST_Safety WHERE Userlogin=@CreatedBy),'0')
		IF @DEPT <> ''
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
	IF @ACTION='GET_REPORT_PARENT3'
	BEGIN
		SELECT ' ' [Userlogin],' ' [NPT]
		UNION
		SELECT DISTINCT Userlogin [Userlogin], Staffname [NPT] FROM [dbo].[ST_Safety]
	END
	IF @ACTION='LIST_DEPARTMENT_F'
	BEGIN
		SELECT -1  [ID], 'All' [Department]
		union all
		SELECT [ID],[Department] FROM	[dbo].[ST_Department] ORDER BY ID
	END
	IF @ACTION='CHECK_ADMIN_USER'
	BEGIN
		SELECT * FROM USERS WHERE  GROUP_ID=49 AND USERNAME =@USER
	END
	IF @ACTION='GET_REPORT_PARENT'
	BEGIN
		--SET @DEPT='0'
		--SET @DEPT=ISNULL((SELECT ISNULL(Department,'0')Department FROM DBO.ST_Safety WHERE Userlogin=@CreatedBy),'0')
		--IF @DEPT <> '0'
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
	IF @ACTION='GET_LIST_BCT1'
	BEGIN
	
		SELECT '0' ID,'' [Staff name] ,''[Description]
		UNION
		SELECT DISTINCT ID, Staffname [Staff name] ,Userlogin [Description] FROM [dbo].[ST_Safety]
	
	END
	IF @ACTION='GET_INFO_USER'
	BEGIN
		SELECT ID,Staffname FROM DBO.ST_Safety WHERE Userlogin=@USER
	END
	IF @ACTION='GET_MAIL_CREATE_BY'
	BEGIN

		SET @DSMAIL2=''
		SET @DSMAIL2=ISNULL((SELECT USER_MAIL
		FROM USERS  
		WHERE USERNAME IN (SELECT ISNULL(CreatedBy,'') FROM  [dbo].[ST_StopCard] WHERE ID=@ID)
				AND USER_MAIL <> ''),'')
		
		SELECT @DSMAIL2

	END
	IF @ACTION='GET_DEAR_CREATE_BY'
	BEGIN
		SET @DEAR=''
	
		SET @DEAR2=''
		SELECT @DEAR2=Staffname+','+@DEAR2 FROM [dbo].[ST_Safety] WHERE Userlogin IN(SELECT ISNULL(REPORT_PARENT, ISNULL(CreatedBy,'')) FROM  [dbo].[ST_StopCard] WHERE ID=@ID	)
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
	IF @ACTION='CHECK_DEPT'
	BEGIN
		SET @DEPT='0'
		SET @DEPT=ISNULL((SELECT ISNULL(Department,'0')Department FROM DBO.ST_Safety WHERE Userlogin=(SELECT CreatedBy FROM DBO.ST_StopCard WHERE ID=@ID)),'0')

		SELECT * FROM DBO.ST_Safety WHERE Userlogin=@USER AND Department=@DEPT
	END
	IF @ACTION='GET_USER_LOGIN'
	BEGIN
		SELECT Userlogin FROM DBO.ST_Safety WHERE ID=@ID
	END
	IF @ACTION='GET_LIST_ACTION'
	BEGIN
		SELECT  [PreventiveAction]
		FROM
		(
		SELECT DISTINCT [PreventiveAction],ID,1 [TYPE] FROM [dbo].[ST_StopCard1] WHERE [StopCardID] =@ID --AND ISNULL([Status],'') =N'Ongoing-Đang xử lý'
		UNION ALL
		SELECT DISTINCT [PermanentAction] [PreventiveAction],ID,2 [TYPE] FROM [dbo].[ST_StopCard2] WHERE [StopCardID] =@ID --AND ISNULL([Status],'') =N'Ongoing-Đang xử lý'
		)T
		ORDER BY [TYPE],ID
	END
	IF @ACTION='GET_LIST_ACTION_DONE'
	BEGIN
		SET @RID=REPLACE(@RID,'|',',')
		SET @RID2=REPLACE(@RID2,'|',',')
		
		DECLARE @SsQL NVARCHAR (MAX)

		
		IF LEN(@RID)>0 AND LEN(@RID2)>0
		BEGIN
			SET @SsQL = '
			SELECT DISTINCT [PreventiveAction] FROM
			(
				SELECT ROW_NUMBER() OVER(ORDER BY ID ASC) AS [RIndex], [PreventiveAction] 
				FROM [dbo].[ST_StopCard1] 
				WHERE [StopCardID] ='+CAST(@ID AS VARCHAR(10)) +'
			)T
			WHERE  CAST(T.[RIndex] AS VARCHAR(10)) IN ('+@RID+')
			UNION ALL
			SELECT DISTINCT  [PreventiveAction] FROM
			(
				SELECT ROW_NUMBER() OVER(ORDER BY ID ASC) AS [RIndex],PermanentAction [PreventiveAction] 
				FROM [dbo].[ST_StopCard2] 
				WHERE [StopCardID] ='+CAST(@ID AS VARCHAR(10)) +'
			)T
			WHERE  CAST(T.[RIndex] AS VARCHAR(10)) IN ('+@RID2+')'
		END
		IF LEN(@RID)>0 AND LEN(@RID2)=0
		BEGIN
			SET @SsQL = '
			SELECT DISTINCT [PreventiveAction] FROM
			(
				SELECT ROW_NUMBER() OVER(ORDER BY ID ASC) AS [RIndex], [PreventiveAction] 
				FROM [dbo].[ST_StopCard1] 
				WHERE [StopCardID] ='+CAST(@ID AS VARCHAR(10)) +'
			)T
			WHERE  CAST(T.[RIndex] AS VARCHAR(10)) IN ('+@RID+')'
		END
		IF LEN(@RID)=0 AND LEN(@RID2)>0
		BEGIN
			SET @SsQL = '
			SELECT DISTINCT [PreventiveAction] FROM
			(
				SELECT ROW_NUMBER() OVER(ORDER BY ID ASC) AS [RIndex],PermanentAction [PreventiveAction] 
				FROM [dbo].[ST_StopCard2] 
				WHERE [StopCardID] ='+CAST(@ID AS VARCHAR(10)) +'
			)T
			WHERE  CAST(T.[RIndex] AS VARCHAR(10)) IN ('+@RID2+')'
		END
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
	IF @ACTION='GET_REPORTER_APPROVAL'
	BEGIN
		
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
							(
								SELECT ISNULL(T1.APPROVAL_LEVEL1,'') TK 
								FROM [dbo].[ST_APPROVAL_USER] T1 INNER JOIN DBO.ST_Safety T2 ON ISNULL(T1.APPROVAL_LEVEL1,'')=T2.Userlogin
								WHERE FORM_ID='FrmStopCard'  AND T2.Department=@DEPT
								UNION 
								SELECT ISNULL(T1.APPROVAL_LEVEL2,'') TK 
								FROM [dbo].[ST_APPROVAL_USER] T1 INNER JOIN DBO.ST_Safety T2 ON ISNULL(T1.APPROVAL_LEVEL2,'')=T2.Userlogin
								WHERE FORM_ID='FrmStopCard'  AND T2.Department=@DEPT								                   
						   )T)
		SET @DEAR =(SELECT SUBSTRING(@DEAR,1,LEN(@DEAR)-1))
		SELECT @DEAR

	END

	IF @ACTION='GET_DEAR_INCHARGE'
	BEGIN
		SET @DEAR=''
		SELECT @DEAR=Staffname+','+@DEAR FROM [dbo].[ST_Safety] WHERE Userlogin IN (SELECT DISTINCT ISNULL(T2.Userlogin,'')Userlogin FROM [dbo].[ST_StopCard1] T1 INNER JOIN [dbo].[ST_Safety] T2 ON T1.PersonIncharge=T2.ID WHERE [StopCardID]=@ID
		UNION ALL SELECT DISTINCT ISNULL(T2.Userlogin,'')Userlogin FROM [dbo].[ST_StopCard2] T1 INNER JOIN [dbo].[ST_Safety] T2 ON T1.PersonIncharge=T2.ID WHERE [StopCardID]=@ID)
		SET @DEAR =(SELECT SUBSTRING(@DEAR,1,LEN(@DEAR)-1))
		SELECT @DEAR
	END
	IF @ACTION='GET_DEAR_APPROVAL_CREATE_BY'
	BEGIN
		SET @DEAR=''
		SELECT @DEAR=Staffname+','+@DEAR FROM [dbo].[ST_Safety] WHERE Userlogin IN(SELECT ISNULL(APPROVAL_USER,'') FROM  [dbo].[ST_StopCard] WHERE ID=@ID)
		IF LEN(@DEAR)<>0
			SET @DEAR =(SELECT SUBSTRING(@DEAR,1,LEN(@DEAR)-1))
		
		SET @DEAR2=''
		SELECT @DEAR2=Staffname+','+@DEAR2 FROM [dbo].[ST_Safety] WHERE Userlogin IN(SELECT ISNULL(REPORT_PARENT, ISNULL(CreatedBy,'')) FROM  [dbo].[ST_StopCard] WHERE ID=@ID	)
		
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
		WHERE USERNAME IN (SELECT DISTINCT T.TK 
						   FROM ( SELECT ISNULL(T1.APPROVAL_LEVEL1,'') TK 
								FROM [dbo].[ST_APPROVAL_USER] T1 INNER JOIN DBO.ST_Safety T2 ON ISNULL(T1.APPROVAL_LEVEL1,'')=T2.Userlogin
								WHERE FORM_ID='FrmStopCard'  AND T2.Department=@DEPT
								UNION 
								SELECT ISNULL(T1.APPROVAL_LEVEL2,'') TK 
								FROM [dbo].[ST_APPROVAL_USER] T1 INNER JOIN DBO.ST_Safety T2 ON ISNULL(T1.APPROVAL_LEVEL2,'')=T2.Userlogin
								WHERE FORM_ID='FrmStopCard'  AND T2.Department=@DEPT	
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
		WHERE USERNAME IN (SELECT DISTINCT ISNULL(T2.Userlogin,'')Userlogin FROM [dbo].[ST_StopCard1] T1 INNER JOIN [dbo].[ST_Safety] T2 ON T1.PersonIncharge=T2.ID WHERE [StopCardID]=@ID
		UNION ALL SELECT DISTINCT ISNULL(T2.Userlogin,'')Userlogin FROM [dbo].[ST_StopCard2] T1 INNER JOIN [dbo].[ST_Safety] T2 ON T1.PersonIncharge=T2.ID WHERE [StopCardID]=@ID
		)
			AND USER_MAIL <> ''
		SET @DSMAIL =(SELECT SUBSTRING(@DSMAIL,1,LEN(@DSMAIL)-1))
		SELECT @DSMAIL

	END

	IF @ACTION='GET_MAIL_APPROVAL_CREATE_BY'
	BEGIN

		SET @DSMAIL=''
		SELECT @DSMAIL=USER_MAIL+';'+@DSMAIL 
		FROM USERS  
		WHERE USERNAME IN(  SELECT ISNULL(APPROVAL_USER,'') FROM  [dbo].[ST_StopCard] WHERE ID=@ID	)
				AND USER_MAIL <> ''
		SET @DSMAIL =(SELECT SUBSTRING(@DSMAIL,1,LEN(@DSMAIL)-1))
		

		SET @DSMAIL2=''
		SET @DSMAIL2=ISNULL((SELECT USER_MAIL
		FROM USERS  
		WHERE USERNAME IN (SELECT ISNULL(REPORT_PARENT, ISNULL(CreatedBy,'')) FROM  [dbo].[ST_StopCard] WHERE ID=@ID)
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
		FROM ST_StopCard 
		WHERE ID=@ID AND ISNULL(IS_APPROVED,'False')='True' AND ISNULL(APPROVAL_USER,'') = @APPROVAL_USER
			AND NOT EXISTS(SELECT 1 FROM ST_StopCard1 WHERE StopCardID =@ID AND ISNULL([Status],'')=N'Done-Đã hoàn thành' UNION ALL SELECT 1 FROM ST_StopCard2 WHERE StopCardID =@ID AND ISNULL([Status],'')=N'Done-Đã hoàn thành'  )
	END

	IF @ACTION='CHECK_APPROVED'
	BEGIN
		
		SELECT 1 FROM ST_StopCard WHERE ID=@ID AND ISNULL(IS_APPROVED,'False')='True' AND ISNULL(APPROVAL_USER,'') <> ''
		
	END
	IF @ACTION='CHECK_USER'
	BEGIN
		SELECT 1 'T'  FROM [dbo].[ST_APPROVAL_USER] WHERE FORM_ID='FrmStopCard' AND (ISNULL(APPROVAL_LEVEL1,'') =@USER OR ISNULL(APPROVAL_LEVEL2,'') =@USER)
		UNION ALL
		SELECT 1 'T'
		FROM [dbo].[ST_StopCard] T1
		WHERE T1.ID=@ID AND T1.CreatedBy=@USER
		UNION ALL
		SELECT 1 'T'
		FROM [dbo].[ST_StopCard] T1 INNER JOIN [dbo].[ST_StopCard1] T2 ON T1.ID=T2.StopCardID
			INNER JOIN ST_Safety T3 ON T2.PersonIncharge=T3.ID
		WHERE T1.ID=@ID  AND T3.Userlogin=@USER
		UNION ALL
		SELECT 1 'T'
		FROM [dbo].[ST_StopCard] T1 INNER JOIN [dbo].[ST_StopCard2] T2 ON T1.ID=T2.StopCardID
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
		SELECT @DEM=MAX(right([DocNum],4)) FROM [dbo].[ST_StopCard]
		IF ISNULL(@DEM,0)=0
			SET @DEM=1
		ELSE
			SET @DEM=@DEM+1
		
		SET @DOCNO='ST' + REPLACE( CONVERT(VARCHAR(10), GETDATE(),104),'.','')+ CASE LEN(@DEM) WHEN 1 THEN '000'+ CONVERT(VARCHAR(4), @DEM) WHEN 2 THEN '00'+ CONVERT(VARCHAR(4), @DEM) WHEN 3 THEN '0'+ CONVERT(VARCHAR(4), @DEM) WHEN 4 THEN  CONVERT(VARCHAR(4), @DEM) END
		SELECT @DOCNO
	END
	
	IF @ACTION='LIST_INCHARGE'
	BEGIN
		SELECT [ID],[Staffname] [Staff name],Userlogin [Description] FROM	[dbo].[ST_Safety] ORDER BY ID
	END
	IF @ACTION='LIST'
	BEGIN

		--UPDATE [dbo].[ST_StopCard1]
		--SET [Status]=N'Overdue-Quá hạn'
		--WHERE ISNULL([CompletedDate],'01/01/1990') ='01/01/1990'
		--	AND DATEDIFF(day,getdate(),[Deadline])<0
		--		AND ISNULL([Status],'') <> N'Reject-Hủy bỏ'
		--UPDATE [dbo].[ST_StopCard1]
		--SET [Status]=N'Ongoing-Đang xử lý'
		--WHERE ISNULL([CompletedDate],'01/01/1990') ='01/01/1990'
		--	AND DATEDIFF(day,getdate(),[Deadline])>=0
		--		AND ISNULL([Status],'') <> N'Reject-Hủy bỏ'
		--UPDATE [dbo].[ST_StopCard1]
		--SET [Status]=N'Done-Đã hoàn thành'
		--WHERE ISNULL([CompletedDate],'01/01/1990') <>'01/01/1990'
		--	AND ISNULL([Status],'') <> N'Reject-Hủy bỏ'

		--UPDATE [dbo].[ST_StopCard2]
		--SET [Status]=N'Overdue-Quá hạn'
		--WHERE ISNULL([CompletedDate],'01/01/1990') ='01/01/1990'
		--	AND DATEDIFF(day,getdate(),[Deadline])<0
		--		AND ISNULL([Status],'') <> N'Reject-Hủy bỏ'
		--UPDATE [dbo].[ST_StopCard2]
		--SET [Status]=N'Ongoing-Đang xử lý'
		--WHERE ISNULL([CompletedDate],'01/01/1990') ='01/01/1990'
		--	AND DATEDIFF(day,getdate(),[Deadline])>=0
		--		AND ISNULL([Status],'') <> N'Reject-Hủy bỏ'
		--UPDATE [dbo].[ST_StopCard2]
		--SET [Status]=N'Done-Đã hoàn thành'
		--WHERE ISNULL([CompletedDate],'01/01/1990') <>'01/01/1990'
		--	AND ISNULL([Status],'') <> N'Reject-Hủy bỏ'


		IF OBJECT_ID('TVT_ST_STOPCARD','U') IS NOT NULL
			DROP TABLE TVT_ST_STOPCARD
		IF OBJECT_ID('TVT_ST_STOPCARD1','U') IS NOT NULL
			DROP TABLE TVT_ST_STOPCARD1
		
		DELETE [dbo].[ST_EDIT_DOCUMENT] WHERE DATEDIFF(minute,[Edit_Time],getdate()) >=15
		
		SELECT DISTINCT * INTO TVT_ST_STOPCARD FROM
		(
		SELECT 
		DISTINCT
			T1.[ID],[DocNum],T1.Description [Description],convert(varchar(10),[DocDate],103) [DocDate],convert(varchar(8),[DocTime],114)[DocTime],CASE WHEN  ISNULL(REPORT_PARENT,'')='' THEN [CreatedBy] ELSE REPORT_PARENT END [CreatedBy],DBO.FN_GET_NGUOITHUCHIEN(3,T1.ID) [NGUOITH],
			CASE WHEN isnull(IS_APPROVED,'False') ='True' THEN N'Đã duyệt - Approved' ELSE N'Chưa duyệt - Not Approved' END [Status],DBO.FN_VS_ST_GET_STATUS_DOC(3,T1.ID) [TTTH]
		,CASE WHEN ISNULL(T1.NGUOILIENQUAN2,'') <> '' THEN  ISNULL(T5.Staffname,'-')+','+ISNULL(T1.NGUOILIENQUAN2,'-') ELSE  ISNULL(T5.Staffname,'-') END NLQVP
		FROM [dbo].[ST_StopCard] T1 INNER JOIN [dbo].[ST_StopCard1] T3 ON T1.ID=T3.StopCardID
		INNER JOIN [dbo].[ST_Safety] T4 ON T3.PersonIncharge=T4.ID
		LEFT JOIN [dbo].[ST_Safety] T5 ON T1.NGUOILIENQUAN1=T5.Userlogin
		WHERE CONVERT(date, T1.DocDate) BETWEEN @TUNGAY AND @DENNGAY
			AND ISNULL(T1.[External],'False') LIKE  CASE WHEN @External_N ='All' THEN '%' ELSE @External_N END
			AND T1.CreatedBy LIKE  CASE WHEN @NGUOIBAOCAO ='All' THEN '%' ELSE @NGUOIBAOCAO END
					AND ISNULL(T4.Staffname,'All') LIKE  CASE WHEN @NGUOIPHUTRACH ='All' THEN '%' ELSE @NGUOIPHUTRACH END
							--AND ISNULL(T3.[Status],'All') LIKE CASE WHEN @TRANGTHAI='All' THEN '%' ELSE @TRANGTHAI END
								AND T1.DocNum LIKE CASE WHEN @DocNum='' THEN '%' ELSE '%'+@DocNum+'%' END
									AND ISNULL(T1.REPORT_PARENT,'') LIKE  CASE WHEN @REPORT_PARENT =' ' THEN '%' ELSE @REPORT_PARENT END
										AND EXISTS (SELECT * FROM DBO.ST_Safety T100 INNER JOIN DBO.ST_Department T200 ON T100.Department=T200.ID
										     WHERE T100.Userlogin=(CASE WHEN  ISNULL(REPORT_PARENT,'')='' THEN [CreatedBy] ELSE REPORT_PARENT END)  AND T100.Department LIKE CASE WHEN @PHONGBAN='-1' THEN '%' ELSE @PHONGBAN END)
										AND ISNULL(T1.IS_DELETE,'False')=@IS_DELETE
										AND ISNULL(T1.NGUOILIENQUAN1,'') LIKE  CASE WHEN @NGUOILIENQUAN1 =' ' THEN '%' ELSE @NGUOILIENQUAN1 END
													AND ISNULL(T1.NGUOILIENQUAN2,'') LIKE  CASE WHEN @NGUOILIENQUAN2 =' ' THEN '%' ELSE N'%'+@NGUOILIENQUAN2+'%' END
		
		UNION ALL
		SELECT 
		DISTINCT
			T1.[ID],[DocNum],T1.Description [Description],convert(varchar(10),[DocDate],103) [DocDate],convert(varchar(8),[DocTime],114)[DocTime],CASE WHEN  ISNULL(REPORT_PARENT,'')='' THEN [CreatedBy] ELSE REPORT_PARENT END [CreatedBy],DBO.FN_GET_NGUOITHUCHIEN(3,T1.ID) [NGUOITH],
			CASE WHEN isnull(IS_APPROVED,'False') ='True' THEN N'Đã duyệt - Approved' ELSE N'Chưa duyệt - Not Approved' END [Status],'' [TTTH]
		,CASE WHEN ISNULL(T1.NGUOILIENQUAN2,'') <> '' THEN  ISNULL(T5.Staffname,'-')+','+ISNULL(T1.NGUOILIENQUAN2,'-') ELSE  ISNULL(T5.Staffname,'-') END NLQVP
		FROM [dbo].[ST_StopCard] T1 
		LEFT JOIN [dbo].[ST_Safety] T5 ON T1.NGUOILIENQUAN1=T5.Userlogin
		WHERE CONVERT(date, T1.DocDate) BETWEEN @TUNGAY AND @DENNGAY
			AND ISNULL(T1.[External],'False') LIKE  CASE WHEN @External_N ='All' THEN '%' ELSE @External_N END
			AND T1.CreatedBy LIKE  CASE WHEN @NGUOIBAOCAO ='All' THEN '%' ELSE @NGUOIBAOCAO END
								AND T1.DocNum LIKE CASE WHEN @DocNum='' THEN '%' ELSE '%'+@DocNum+'%' END
									AND ISNULL(T1.REPORT_PARENT,'') LIKE  CASE WHEN @REPORT_PARENT =' ' THEN '%' ELSE @REPORT_PARENT END
					AND NOT EXISTS (SELECT 1 FROM DBO.ST_StopCard1 WHERE StopCardID=@ID) AND @NGUOIPHUTRACH='All' AND @TRANGTHAI='All'
							AND EXISTS (SELECT * FROM DBO.ST_Safety T100 INNER JOIN DBO.ST_Department T200 ON T100.Department=T200.ID
										     WHERE T100.Userlogin=(CASE WHEN  ISNULL(REPORT_PARENT,'')='' THEN [CreatedBy] ELSE REPORT_PARENT END)  AND T100.Department LIKE CASE WHEN @PHONGBAN='-1' THEN '%' ELSE @PHONGBAN END)
		
										AND ISNULL(T1.IS_DELETE,'False')=@IS_DELETE
										AND ISNULL(T1.NGUOILIENQUAN1,'') LIKE  CASE WHEN @NGUOILIENQUAN1 =' ' THEN '%' ELSE @NGUOILIENQUAN1 END
													AND ISNULL(T1.NGUOILIENQUAN2,'') LIKE  CASE WHEN @NGUOILIENQUAN2 =' ' THEN '%' ELSE N'%'+@NGUOILIENQUAN2+'%' END
													
		UNION ALL
		SELECT 
		DISTINCT
			T1.[ID],[DocNum],T1.Description [Description],convert(varchar(10),[DocDate],103) [DocDate],convert(varchar(8),[DocTime],114)[DocTime],CASE WHEN  ISNULL(REPORT_PARENT,'')='' THEN [CreatedBy] ELSE REPORT_PARENT END [CreatedBy],DBO.FN_GET_NGUOITHUCHIEN(3,T1.ID) [NGUOITH],
			CASE WHEN isnull(IS_APPROVED,'False') ='True' THEN N'Đã duyệt - Approved' ELSE N'Chưa duyệt - Not Approved' END [Status],DBO.FN_VS_ST_GET_STATUS_DOC(3,T1.ID) [TTTH]
		,CASE WHEN ISNULL(T1.NGUOILIENQUAN2,'') <> '' THEN  ISNULL(T5.Staffname,'-')+','+ISNULL(T1.NGUOILIENQUAN2,'-') ELSE  ISNULL(T5.Staffname,'-') END NLQVP
		FROM [dbo].[ST_StopCard] T1 INNER JOIN [dbo].[ST_StopCard2] T3 ON T1.ID=T3.StopCardID
		INNER JOIN [dbo].[ST_Safety] T4 ON T3.PersonIncharge=T4.ID
		LEFT JOIN [dbo].[ST_Safety] T5 ON T1.NGUOILIENQUAN1=T5.Userlogin
		WHERE CONVERT(date, T1.DocDate) BETWEEN @TUNGAY AND @DENNGAY
			AND ISNULL(T1.[External],'False') LIKE  CASE WHEN @External_N ='All' THEN '%' ELSE @External_N END
			AND T1.CreatedBy LIKE  CASE WHEN @NGUOIBAOCAO ='All' THEN '%' ELSE @NGUOIBAOCAO END
					AND ISNULL(T4.Staffname,'All') LIKE  CASE WHEN @NGUOIPHUTRACH ='All' THEN '%' ELSE @NGUOIPHUTRACH END
							--AND ISNULL(T3.[Status],'All') LIKE CASE WHEN @TRANGTHAI='All' THEN '%' ELSE @TRANGTHAI END
								AND T1.DocNum LIKE CASE WHEN @DocNum='' THEN '%' ELSE '%'+@DocNum+'%' END
									AND ISNULL(T1.REPORT_PARENT,'') LIKE  CASE WHEN @REPORT_PARENT =' ' THEN '%' ELSE @REPORT_PARENT END
										AND EXISTS (SELECT * FROM DBO.ST_Safety T100 INNER JOIN DBO.ST_Department T200 ON T100.Department=T200.ID
										     WHERE T100.Userlogin=(CASE WHEN  ISNULL(REPORT_PARENT,'')='' THEN [CreatedBy] ELSE REPORT_PARENT END)  AND T100.Department LIKE CASE WHEN @PHONGBAN='-1' THEN '%' ELSE @PHONGBAN END)
												AND ISNULL(T1.IS_DELETE,'False')=@IS_DELETE
												AND ISNULL(T1.NGUOILIENQUAN1,'') LIKE  CASE WHEN @NGUOILIENQUAN1 =' ' THEN '%' ELSE @NGUOILIENQUAN1 END
													AND ISNULL(T1.NGUOILIENQUAN2,'') LIKE  CASE WHEN @NGUOILIENQUAN2 =' ' THEN '%' ELSE N'%'+@NGUOILIENQUAN2+'%' END
													
		
		UNION ALL
		SELECT 
		DISTINCT
			T1.[ID],[DocNum],T1.Description [Description],convert(varchar(10),[DocDate],103) [DocDate],convert(varchar(8),[DocTime],114)[DocTime],CASE WHEN  ISNULL(REPORT_PARENT,'')='' THEN [CreatedBy] ELSE REPORT_PARENT END [CreatedBy],DBO.FN_GET_NGUOITHUCHIEN(3,T1.ID) [NGUOITH],
			CASE WHEN isnull(IS_APPROVED,'False') ='True' THEN N'Đã duyệt - Approved' ELSE N'Chưa duyệt - Not Approved' END [Status],'' [TTTH]
		,CASE WHEN ISNULL(T1.NGUOILIENQUAN2,'') <> '' THEN  ISNULL(T5.Staffname,'-')+','+ISNULL(T1.NGUOILIENQUAN2,'-') ELSE  ISNULL(T5.Staffname,'-') END NLQVP
		FROM [dbo].[ST_StopCard] T1 
		LEFT JOIN [dbo].[ST_Safety] T5 ON T1.NGUOILIENQUAN1=T5.Userlogin
		WHERE CONVERT(date, T1.DocDate) BETWEEN @TUNGAY AND @DENNGAY
			AND ISNULL(T1.[External],'False') LIKE  CASE WHEN @External_N ='All' THEN '%' ELSE @External_N END
			AND T1.CreatedBy LIKE  CASE WHEN @NGUOIBAOCAO ='All' THEN '%' ELSE @NGUOIBAOCAO END
								AND T1.DocNum LIKE CASE WHEN @DocNum='' THEN '%' ELSE '%'+@DocNum+'%' END
									AND ISNULL(T1.REPORT_PARENT,'') LIKE  CASE WHEN @REPORT_PARENT =' ' THEN '%' ELSE @REPORT_PARENT END
					AND NOT EXISTS (SELECT 1 FROM DBO.ST_StopCard2 WHERE StopCardID=@ID) AND @NGUOIPHUTRACH='All' AND @TRANGTHAI='All'
						AND EXISTS (SELECT * FROM DBO.ST_Safety T100 INNER JOIN DBO.ST_Department T200 ON T100.Department=T200.ID
										     WHERE T100.Userlogin=(CASE WHEN  ISNULL(REPORT_PARENT,'')='' THEN [CreatedBy] ELSE REPORT_PARENT END)  AND T100.Department LIKE CASE WHEN @PHONGBAN='-1' THEN '%' ELSE @PHONGBAN END)
		
					AND ISNULL(T1.IS_DELETE,'False')=@IS_DELETE
					AND ISNULL(T1.NGUOILIENQUAN1,'') LIKE  CASE WHEN @NGUOILIENQUAN1 =' ' THEN '%' ELSE @NGUOILIENQUAN1 END
													AND ISNULL(T1.NGUOILIENQUAN2,'') LIKE  CASE WHEN @NGUOILIENQUAN2 =' ' THEN '%' ELSE N'%'+@NGUOILIENQUAN2+'%' END
		
		)T
		ORDER BY ID

		UPDATE TVT_ST_STOPCARD SET NLQVP=REPLACE(NLQVP,'-,','')
		UPDATE TVT_ST_STOPCARD SET NLQVP=REPLACE(NLQVP,',-','')
		UPDATE TVT_ST_STOPCARD SET NLQVP=REPLACE(NLQVP,'-','')
		IF @TRANGTHAI <> 'All'
		BEGIN
			IF @TRANGTHAI =N'Done-Đã hoàn thành'
			BEGIN
				SELECT DISTINCT * 	INTO TVT_ST_STOPCARD1 FROM TVT_ST_STOPCARD T
				WHERE (T.TTTH = N'Done-Đã hoàn thành' OR T.TTTH=N'Reject-Hủy bỏ')
					AND NOT EXISTS 
							(   SELECT 1 TT
							    FROM [dbo].ST_StopCard1 
								WHERE StopCardID =T.ID  AND ([Status] =N'Ongoing-Đang xử lý' OR  [Status] =N'Overdue-Quá hạn')
								UNION ALL
								SELECT 1 TT
							    FROM [dbo].ST_StopCard2  
								WHERE StopCardID =T.ID  AND ([Status] =N'Ongoing-Đang xử lý' OR  [Status] =N'Overdue-Quá hạn')
								
							)
						AND  EXISTS 
						       (SELECT 1 TT
								FROM [dbo].ST_StopCard1
								WHERE StopCardID =T.ID  AND [Status] = N'Done-Đã hoàn thành'
								UNION ALL 
								SELECT 1 TT
								FROM [dbo].ST_StopCard2
								WHERE StopCardID =T.ID  AND [Status] = N'Done-Đã hoàn thành'
								)
				UPDATE TVT_ST_STOPCARD1 
				SET TTTH=N'Done-Đã hoàn thành'
			END
			IF @TRANGTHAI =N'Ongoing-Đang xử lý'
			BEGIN
				SELECT DISTINCT * 	INTO TVT_ST_STOPCARD1 FROM TVT_ST_STOPCARD T
				WHERE T.TTTH =N'Ongoing-Đang xử lý'
					AND NOT EXISTS 
							(SELECT 1 TT
								FROM [dbo].ST_StopCard1
								WHERE StopCardID =T.ID  AND [Status] = N'Overdue-Quá hạn'
								UNION ALL 
								SELECT 1 TT
								FROM [dbo].ST_StopCard2
								WHERE StopCardID =T.ID  AND [Status] = N'Overdue-Quá hạn'
								)
				
				UPDATE TVT_ST_STOPCARD1 SET TTTH=N'Ongoing-Đang xử lý'

						
			END
			IF @TRANGTHAI =N'Overdue-Quá hạn'
			BEGIN
				SELECT DISTINCT * 	INTO TVT_ST_STOPCARD1 FROM TVT_ST_STOPCARD T
				WHERE T.TTTH =N'Overdue-Quá hạn'
					--AND NOT EXISTS (SELECT 1 TT
					--			FROM [dbo].ST_StopCard1
					--			WHERE StopCardID =T.ID  AND [Status] = N'Ongoing-Đang xử lý'
					--			UNION ALL 
					--			SELECT 1 TT
					--			FROM [dbo].ST_StopCard2
					--			WHERE StopCardID =T.ID  AND [Status] = N'Ongoing-Đang xử lý'
					--			)
				UPDATE TVT_ST_STOPCARD1 SET TTTH=N'Overdue-Quá hạn'
			END
			IF @TRANGTHAI =N'Reject-Hủy bỏ'
			BEGIN
				SELECT DISTINCT * 	INTO TVT_ST_STOPCARD1 FROM TVT_ST_STOPCARD T
				WHERE T.TTTH =N'Reject-Hủy bỏ'
					AND NOT EXISTS (SELECT 1 TT
								FROM [dbo].ST_StopCard1
								WHERE StopCardID =T.ID  AND [Status] <> N'Reject-Hủy bỏ'
								UNION ALL 
								SELECT 1 TT
								FROM [dbo].ST_StopCard2
								WHERE StopCardID =T.ID  AND [Status] <> N'Reject-Hủy bỏ'
								)
				UPDATE TVT_ST_STOPCARD1 SET TTTH=N'Reject-Hủy bỏ'
			END
		END
		ELSE
		BEGIN
			UPDATE TVT_ST_STOPCARD SET TTTH='' WHERE [Status]=N'Chưa duyệt - Not Approved'
			SELECT DISTINCT * 	INTO TVT_ST_STOPCARD1 FROM TVT_ST_STOPCARD
		END

		UPDATE TVT_ST_STOPCARD1 SET TTTH='' WHERE [Status]=N'Chưa duyệt - Not Approved'
		DECLARE @QUERY VARCHAR(8000)
		SET @QUERY =''

		IF LEN(@LOAISC)>0
		BEGIN
			SET @QUERY ='SELECT T2.[ID], T2.[DocNum],T2.Description [Description],T2.[DocDate],T2.[DocTime],T2.[CreatedBy],T2.[NGUOITH],T2.[Status],MAX(T2.[TTTH])[TTTH],NLQVP
			FROM [dbo].[ST_StopCard] T1 INNER JOIN TVT_ST_STOPCARD1 T2 ON T1.ID=T2.ID
			WHERE '+@LOAISC +' 
			GROUP BY T2.[ID], T2.[DocNum],T2.Description,T2.[DocDate],T2.[DocTime],T2.[CreatedBy],T2.[NGUOITH],T2.[Status],NLQVP
			ORDER BY T2.ID'
			PRINT @QUERY
			EXEC(@QUERY)
		END
		ELSE
		BEGIN
			SELECT T2.[ID], T2.[DocNum],T2.Description [Description],T2.[DocDate],T2.[DocTime],T2.[CreatedBy],T2.[NGUOITH],T2.[Status],MAX(T2.[TTTH])[TTTH],NLQVP
			FROM TVT_ST_STOPCARD1 T2 
			GROUP BY T2.[ID], T2.[DocNum],T2.Description,T2.[DocDate],T2.[DocTime],T2.[CreatedBy],T2.[NGUOITH],T2.[Status],NLQVP
			ORDER BY T2.ID
		END

		IF OBJECT_ID('TVT_ST_STOPCARD','U') IS NOT NULL
			DROP TABLE TVT_ST_STOPCARD
		IF OBJECT_ID('TVT_ST_STOPCARD1','U') IS NOT NULL
			DROP TABLE TVT_ST_STOPCARD1
	END
	IF @ACTION='GET_DETAIL'
	BEGIN
		UPDATE [dbo].[ST_StopCard1]
		SET [Status]=N'Overdue-Quá hạn'
		WHERE ISNULL([CompletedDate],'01/01/1990') ='01/01/1990'
			AND DATEDIFF(day,getdate(),[Deadline])<0
				AND [Status] <> N'Reject-Hủy bỏ'
		UPDATE [dbo].[ST_StopCard1]
		SET [Status]=N'Ongoing-Đang xử lý'
		WHERE ISNULL([CompletedDate],'01/01/1990') ='01/01/1990'
			AND DATEDIFF(day,getdate(),[Deadline])>=0
				AND [Status] <> N'Reject-Hủy bỏ'
		UPDATE [dbo].[ST_StopCard1]
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
		FROM [dbo].[ST_StopCard1]
		WHERE [StopCardID]=@ID
	END
	IF @ACTION='GET_DETAIL2'
	BEGIN
		UPDATE [dbo].[ST_StopCard2]
		SET [Status]=N'Overdue-Quá hạn'
		WHERE ISNULL([CompletedDate],'01/01/1990') ='01/01/1990'
			AND DATEDIFF(day,getdate(),[Deadline])<0
				AND [Status] <> N'Reject-Hủy bỏ'
		UPDATE [dbo].[ST_StopCard2]
		SET [Status]=N'Ongoing-Đang xử lý'
		WHERE ISNULL([CompletedDate],'01/01/1990') ='01/01/1990'
			AND DATEDIFF(day,getdate(),[Deadline])>=0
				AND [Status] <> N'Reject-Hủy bỏ'
		UPDATE [dbo].[ST_StopCard2]
		SET [Status]=N'Done-Đã hoàn thành'
		WHERE ISNULL([CompletedDate],'01/01/1990') <>'01/01/1990'
			AND [Status] <> N'Reject-Hủy bỏ'
		SELECT 
			[ID]
			,[PermanentAction]
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
		FROM [dbo].[ST_StopCard2]
		WHERE [StopCardID]=@ID
	END
	IF @ACTION='GET_BY_ID'
	BEGIN
		SELECT 
			 [ID]
			,CreatedBy
			,[DocNum]
			,[DocDate]
			,[DocTime]
			,[AdjPerPPE]
			,[StopJob]
			,[ChangingPosition]
			,[RearrangingJob]
			,[AttachingGrouds]
			,[PerformingLocks]
			,[OtherReaction]
			,[Head]
			,[ArmsAndHands]
			,[EyesAndFace]
			,[Trunk]
			,[Ears]
			,[LegsAndFeet]
			,[RespiratorySystem]
			,[OtherPPE]
			,[HPES1]
			,[HPES2]
			,[HPES3]
			,[HPES4]
			,[HPES5]
			,[HPES6]
			,[StrikingAgaintObjects]
			,[ChokingHazardousAir]
			,[CaughtInOn]
			,[Inhaling]
			,[Falling]
			,[RepettiveMotions]
			,[Contacting]
			,[PositionsToWork]
			,[ContactSharpObject]
			,[FlyingObjects]
			,[ContactingElectric]
			,[EmployeeWork]
			,[NotRight]
			,[Unknow]
			,[UsedIncorrectly]
			,[NotUnderstood]
			,[InUnsafeCondition]
			,[NotFlowed]
			,[NotAequate]
			,[OtherProcedure]
			,[OtherTools]
			,[NotIsolate]
			,[ToolAndEquipment]
			,[NoCollectGarbage]
			,[EmergencyExit]
			,[Slip]
			,[OtherOderLine],
			ISNULL([External],'False')[External]
			,[DUNG_CU_THIET_BI]
			,[KHU_VUC_LAM_VIEC]
			,[MOI_TRUONG_LAM_VIEC]
			,[SACH_SE_NGAN_NAP]
			,[LOAI_CONG_VIEC]
			,[HO_TEN_CA_NHAN_TO_NHOM]
			,[HANH_DONG_HANH_VI_AN_TOAN]
			,[HANH_VI_KHONG_AN_TOAN]
			,[KHAC_PHUC_TUC_THI]
			,ISNULL(IS_APPROVED,'False') IS_APPROVED
			,ISNULL(APPROVAL_USER,'') APPROVAL_USER,
			ISNULL(TOtherPPE,'')TOtherPPE,
			ISNULL(TOtherOrderliness,'')TOtherOrderliness,
			ISNULL(TOtherProcedure,'')TOtherProcedure,
			ISNULL(TOtherReactions,'')TOtherReactions,
			ISNULL(TOtherToolAndEquipment,'')TOtherToolAndEquipment,
			ISNULL(Description,'')Description,
			ISNULL(REPORT_PARENT,'')REPORT_PARENT,
			ISNULL(IMG_PATCH1,'')IMG_PATCH1,
			ISNULL(IMG_PATCH2,'')IMG_PATCH2,
			Image_1,
			Image_2,
			ISNULL(IS_DELETE,'False')IS_DELETE,
			ISNULL(NGUOILIENQUAN1,'')NGUOILIENQUAN1,
			ISNULL(NGUOILIENQUAN2,'')NGUOILIENQUAN2
		FROM [dbo].[ST_StopCard]
		WHERE ID=@ID
	END
	IF @ACTION='GET_BY_DOC_NUM'
	BEGIN
		SELECT 
			 [ID]
			,CreatedBy
			,[DocNum]
			,[DocDate]
			,[DocTime]
			,[AdjPerPPE]
			,[StopJob]
			,[ChangingPosition]
			,[RearrangingJob]
			,[AttachingGrouds]
			,[PerformingLocks]
			,[OtherReaction]
			,[Head]
			,[ArmsAndHands]
			,[EyesAndFace]
			,[Trunk]
			,[Ears]
			,[LegsAndFeet]
			,[RespiratorySystem]
			,[OtherPPE]
			,[HPES1]
			,[HPES2]
			,[HPES3]
			,[HPES4]
			,[HPES5]
			,[HPES6]
			,[StrikingAgaintObjects]
			,[ChokingHazardousAir]
			,[CaughtInOn]
			,[Inhaling]
			,[Falling]
			,[RepettiveMotions]
			,[Contacting]
			,[PositionsToWork]
			,[ContactSharpObject]
			,[FlyingObjects]
			,[ContactingElectric]
			,[EmployeeWork]
			,[NotRight]
			,[Unknow]
			,[UsedIncorrectly]
			,[NotUnderstood]
			,[InUnsafeCondition]
			,[NotFlowed]
			,[NotAequate]
			,[OtherProcedure]
			,[OtherTools]
			,[NotIsolate]
			,[ToolAndEquipment]
			,[NoCollectGarbage]
			,[EmergencyExit]
			,[Slip]
			,[OtherOderLine],
			ISNULL([External],'False')[External]
			,[DUNG_CU_THIET_BI]
			,[KHU_VUC_LAM_VIEC]
			,[MOI_TRUONG_LAM_VIEC]
			,[SACH_SE_NGAN_NAP]
			,[LOAI_CONG_VIEC]
			,[HO_TEN_CA_NHAN_TO_NHOM]
			,[HANH_DONG_HANH_VI_AN_TOAN]
			,[HANH_VI_KHONG_AN_TOAN]
			,[KHAC_PHUC_TUC_THI]
			,ISNULL(IS_APPROVED,'False') IS_APPROVED
			,ISNULL(APPROVAL_USER,'') APPROVAL_USER,
			ISNULL(TOtherPPE,'')TOtherPPE,
			ISNULL(TOtherOrderliness,'')TOtherOrderliness,
			ISNULL(TOtherProcedure,'')TOtherProcedure,
			ISNULL(TOtherReactions,'')TOtherReactions,
			ISNULL(TOtherToolAndEquipment,'')TOtherToolAndEquipment,
			ISNULL(Description,'')Description,
			ISNULL(REPORT_PARENT,'')REPORT_PARENT,
			ISNULL(IMG_PATCH1,'')IMG_PATCH1,
			ISNULL(IMG_PATCH2,'')IMG_PATCH2,
			Image_1,
			Image_2,
			ISNULL(IS_DELETE,'False')IS_DELETE,
			ISNULL(NGUOILIENQUAN1,'')NGUOILIENQUAN1,
			ISNULL(NGUOILIENQUAN2,'')NGUOILIENQUAN2
		FROM [dbo].[ST_StopCard]
		WHERE DocNum=@DocNum
	END
	IF @ACTION='ADD'
	BEGIN

		SET @DOCNO=''
		SELECT @DEM=MAX(right([DocNum],4)) FROM [dbo].[ST_StopCard]
		IF ISNULL(@DEM,0)=0
			SET @DEM=1
		ELSE
			SET @DEM=@DEM+1
		
		SET @DOCNO='ST' + REPLACE( CONVERT(VARCHAR(10), GETDATE(),104),'.','')+ CASE LEN(@DEM) WHEN 1 THEN '000'+ CONVERT(VARCHAR(4), @DEM) WHEN 2 THEN '00'+ CONVERT(VARCHAR(4), @DEM) WHEN 3 THEN '0'+ CONVERT(VARCHAR(4), @DEM) WHEN 4 THEN  CONVERT(VARCHAR(4), @DEM) END
		

		IF NOT EXISTS(SELECT * FROM [dbo].[ST_StopCard] WHERE ID=@ID)
		BEGIN
			INSERT INTO [dbo].[ST_StopCard]
			   ([DocNum]
			   ,CreatedBy
			   ,[DocDate]
			   ,[DocTime]
			   ,[AdjPerPPE]
			   ,[StopJob]
			   ,[ChangingPosition]
			   ,[RearrangingJob]
			   ,[AttachingGrouds]
			   ,[PerformingLocks]
			   ,[OtherReaction]
			   ,[Head]
			   ,[ArmsAndHands]
			   ,[EyesAndFace]
			   ,[Trunk]
			   ,[Ears]
			   ,[LegsAndFeet]
			   ,[RespiratorySystem]
			   ,[OtherPPE]
			   ,[HPES1]
			   ,[HPES2]
			   ,[HPES3]
			   ,[HPES4]
			   ,[HPES5]
			   ,[HPES6]
			   ,[StrikingAgaintObjects]
			   ,[ChokingHazardousAir]
			   ,[CaughtInOn]
			   ,[Inhaling]
			   ,[Falling]
			   ,[RepettiveMotions]
			   ,[Contacting]
			   ,[PositionsToWork]
			   ,[ContactSharpObject]
			   ,[FlyingObjects]
			   ,[ContactingElectric]
			   ,[EmployeeWork]
			   ,[NotRight]
			   ,[Unknow]
			   ,[UsedIncorrectly]
			   ,[NotUnderstood]
			   ,[InUnsafeCondition]
			   ,[NotFlowed]
			   ,[NotAequate]
			   ,[OtherProcedure]
			   ,[OtherTools]
			   ,[NotIsolate]
			   ,[ToolAndEquipment]
			   ,[NoCollectGarbage]
			   ,[EmergencyExit]
			   ,[Slip]
			   ,[OtherOderLine],[External]
			   ,[DUNG_CU_THIET_BI]
				,[KHU_VUC_LAM_VIEC]
				,[MOI_TRUONG_LAM_VIEC]
				,[SACH_SE_NGAN_NAP]
				,[LOAI_CONG_VIEC]
				,[HO_TEN_CA_NHAN_TO_NHOM]
				,[HANH_DONG_HANH_VI_AN_TOAN]
				,[HANH_VI_KHONG_AN_TOAN]
				,[KHAC_PHUC_TUC_THI],IS_APPROVED,APPROVAL_USER,[TOtherOrderliness]
				,[TOtherPPE]
				,[TOtherProcedure]
				,[TOtherReactions]
				,[TOtherToolAndEquipment],Description,[REPORT_PARENT],IMG_PATCH1,IMG_PATCH2,NGUOILIENQUAN1,NGUOILIENQUAN2,Image_1,Image_2)
			 VALUES
			   (@DOCNO
			   ,@CreatedBy
			   ,@DocDate
			   ,@DocTime
			   ,@AdjPerPPE
			   ,@StopJob
			   ,@ChangingPosition
			   ,@RearrangingJob
			   ,@AttachingGrouds
			   ,@PerformingLocks
			   ,@OtherReaction
			   ,@Head
			   ,@ArmsAndHands
			   ,@EyesAndFace
			   ,@Trunk
			   ,@Ears
			   ,@LegsAndFeet
			   ,@RespiratorySystem
			   ,@OtherPPE
			   ,@HPES1
			   ,@HPES2
			   ,@HPES3
			   ,@HPES4
			   ,@HPES5
			   ,@HPES6
			   ,@StrikingAgaintObjects
			   ,@ChokingHazardousAir
			   ,@CaughtInOn
			   ,@Inhaling
			   ,@Falling
			   ,@RepettiveMotions
			   ,@Contacting
			   ,@PositionsToWork
			   ,@ContactSharpObject
			   ,@FlyingObjects
			   ,@ContactingElectric
			   ,@EmployeeWork
			   ,@NotRight
			   ,@Unknow
			   ,@UsedIncorrectly
			   ,@NotUnderstood
			   ,@InUnsafeCondition
			   ,@NotFlowed
			   ,@NotAequate
			   ,@OtherProcedure
			   ,@OtherTools
			   ,@NotIsolate
			   ,@ToolAndEquipment
			   ,@NoCollectGarbage
			   ,@EmergencyExit
			   ,@Slip
			   ,@OtherOderLine,@External
			   ,@DUNG_CU_THIET_BI
				,@KHU_VUC_LAM_VIEC
				,@MOI_TRUONG_LAM_VIEC
				,@SACH_SE_NGAN_NAP
				,@LOAI_CONG_VIEC
				,@HO_TEN_CA_NHAN_TO_NHOM
				,@HANH_DONG_HANH_VI_AN_TOAN
				,@HANH_VI_KHONG_AN_TOAN
				,@KHAC_PHUC_TUC_THI,@IS_APPROVED,@APPROVAL_USER,@TOtherOrderliness
				,@TOtherPPE
				,@TOtherProcedure
				,@TOtherReactions
				,@TOtherToolAndEquipment,@Description,@REPORT_PARENT,@IMG_PATCH1,@IMG_PATCH2,@NGUOILIENQUAN1,@NGUOILIENQUAN2,@Image_1,@Image_2)

				SET @ID=ISNULL((SELECT MAX(ID) FROM [dbo].[ST_StopCard]),1)

				--UPDATE DBO.ST_StopCard SET APPROVAL_USER=CreatedBy , IS_APPROVED='True' 
				--WHERE ID=@ID AND EXISTS (SELECT * FROM DBO.ST_APPROVAL_USER WHERE FORM_ID='FrmStopCard' AND (ISNULL(APPROVAL_LEVEL1,'')=DBO.ST_StopCard.CreatedBy OR ISNULL(APPROVAL_LEVEL2,'')=DBO.ST_StopCard.CreatedBy))
				
				UPDATE DBO.ST_StopCard SET APPROVAL_USER=CreatedBy , IS_APPROVED='True' 
				IF LEN(@NGUOILIENQUAN2)>0
				BEGIN
					IF NOT EXISTS	(SELECT * FROM ST_DT_NGOAICANG WHERE  UPPER([TENDOITUONG]) = UPPER(@NGUOILIENQUAN2) AND @NGUOILIENQUAN2 <> '')
					INSERT INTO ST_DT_NGOAICANG([TENDOITUONG],[REF_TYPE])
					SELECT @NGUOILIENQUAN2,3
				END
		   END

		   INSERT INTO [dbo].[ST_StopCard2]
           ([StopCardID]
           ,[PermanentAction]
           ,[PersonIncharge]
           ,[Deadline]
		   ,[CompletedDate]
		   ,[Status]
		   ,[PersonReport]
		   ,[Description],MUCUUTIEN,[THIETBI],	[LOAIYEUCAU],	[NGUYENNHAN])
		   SELECT @ID,[PermanentAction],[PersonIncharge],[Deadline],[CompletedDate] ,[Status],[PersonReport],[Description],[MUCUUTIEN],[THIETBI],	[LOAIYEUCAU],	[NGUYENNHAN] FROM @TB2 WHERE ISNULL(ID,-1) NOT IN (SELECT ID FROM [dbo].[ST_StopCard2])

		   INSERT INTO [dbo].[ST_StopCard1]
           ([StopCardID]
           ,[PreventiveAction]
           ,[PersonIncharge]
           ,[Deadline]
		   ,[CompletedDate]
		   ,[Status]
		   ,[PersonReport]
		   ,[Description],MUCUUTIEN,[THIETBI],	[LOAIYEUCAU],	[NGUYENNHAN])
		   SELECT @ID,[PreventiveAction],[PersonIncharge],[Deadline],[CompletedDate] ,[Status],[PersonReport],[Description],[MUCUUTIEN],[THIETBI],	[LOAIYEUCAU],	[NGUYENNHAN] FROM @TB WHERE ISNULL(ID,-1) NOT IN (SELECT ID FROM [dbo].[ST_StopCard1])
			
		   ---######START######TAO PHIEU YEU CAU TU DONG NEU CHUNG TU DA DUOC DUYET######START######---
			IF EXISTS(SELECT * FROM DBO.ST_StopCard WHERE ID=@ID AND  IS_APPROVED='True')
			BEGIN
				SET @SLCT=0
				
				SELECT @SLCT=COUNT(*) FROM
				(
				SELECT T2.PreventiveAction,T2.ID,1 [Type]
				FROM DBO.ST_StopCard T1 INNER JOIN DBO.ST_StopCard1 T2 ON T1.ID=T2.StopCardID
				WHERE T1.ID=@ID AND ISNULL(T2.THIETBI,'') <> '' AND ISNULL(T2.LOAIYEUCAU,-1) <> -1 AND ISNULL(T2.NGUYENNHAN,-1) <> -1 AND ISNULL(T2.MUCUUTIEN,-1) <> -1
				union all
				
				SELECT T2.PermanentAction,T2.ID,2 [Type]
				FROM DBO.ST_StopCard T1 INNER JOIN DBO.ST_StopCard2 T2 ON T1.ID=T2.StopCardID
				WHERE T1.ID=@ID AND ISNULL(T2.THIETBI,'') <> '' AND ISNULL(T2.LOAIYEUCAU,-1) <> -1 AND ISNULL(T2.NGUYENNHAN,-1) <> -1 AND ISNULL(T2.MUCUUTIEN,-1) <> -1
				)T
				
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
					
					SELECT @MS_YEU_CAU,@MS_YEU_CAU,DocDate,DocTime,ISNULL( t.REPORT_PARENT,T.CreatedBy),'BARI',T.DocNum,T.ID,t.[Description],ISNULL( t.REPORT_PARENT,T.CreatedBy)
					FROM DBO.ST_StopCard T
					WHERE ID=@ID
					
					
					SET @STT=ISNULL((SELECT TOP 1 STT FROM YEU_CAU_NSD WHERE MS_YEU_CAU=@MS_YEU_CAU),0)
					
					INSERT INTO YEU_CAU_NSD_CHI_TIET(STT,MS_MAY,MO_TA_TINH_TRANG,MS_LOAI_YEU_CAU_BT,MS_NGUYEN_NHAN,REF_TYPE,REF_NO,REF_ID,DETAIL_TYPE,LINE_ID,MS_UU_TIEN,USERNAME_DSX,THOI_GIAN_DSX,Y_KIEN_DSX,THUC_HIEN_DSX,YEU_CAU)
					SELECT @STT,T2.THIETBI,T1.[Description],T2.LOAIYEUCAU,T2.NGUYENNHAN,'ST' ,T1.DocNum,T1.ID,'PreventiveAction1',T2.ID,T2.MUCUUTIEN,T1.APPROVAL_USER,GETDATE(),N'Duyệt tự động từ Safety (Chứng từ (Ecomaint AT): ' + t1.DocNum+')',1,T2.PreventiveAction
					FROM DBO.ST_StopCard T1 INNER JOIN DBO.ST_StopCard1 T2 ON T1.ID=T2.StopCardID
					WHERE T1.ID=@ID AND ISNULL(T2.THIETBI,'') <> '' AND ISNULL(T2.LOAIYEUCAU,-1) <> -1 AND ISNULL(T2.NGUYENNHAN,-1) <> -1 AND ISNULL(T2.MUCUUTIEN,-1) <> -1
					
					INSERT INTO YEU_CAU_NSD_CHI_TIET(STT,MS_MAY,MO_TA_TINH_TRANG,MS_LOAI_YEU_CAU_BT,MS_NGUYEN_NHAN,REF_TYPE,REF_NO,REF_ID,DETAIL_TYPE,LINE_ID,MS_UU_TIEN,USERNAME_DSX,THOI_GIAN_DSX,Y_KIEN_DSX,THUC_HIEN_DSX,YEU_CAU)
					SELECT @STT,T2.THIETBI,T1.[Description],T2.LOAIYEUCAU,T2.NGUYENNHAN,'ST' ,T1.DocNum,T1.ID,'PreventiveAction2',T2.ID,T2.MUCUUTIEN,T1.APPROVAL_USER,GETDATE(),N'Duyệt tự động từ Safety (Chứng từ (Ecomaint AT): ' + t1.DocNum+')',1,T2.PermanentAction
					FROM DBO.ST_StopCard T1 INNER JOIN DBO.ST_StopCard2 T2 ON T1.ID=T2.StopCardID
					WHERE T1.ID=@ID AND ISNULL(T2.THIETBI,'') <> '' AND ISNULL(T2.LOAIYEUCAU,-1) <> -1 AND ISNULL(T2.NGUYENNHAN,-1) <> -1 AND ISNULL(T2.MUCUUTIEN,-1) <> -1
				END
			END
			---######END######TAO PHIEU YEU CAU TU DONG NEU CHUNG TU DA DUOC DUYET######END######---
	
	END
	IF @ACTION='UPDATE'
	BEGIN
		IF EXISTS(SELECT * FROM [dbo].[ST_StopCard] WHERE ID=@ID)
		BEGIN
			UPDATE [dbo].[ST_StopCard]
			SET [DocNum] = @DocNum
			  ,CreatedBy=@CreatedBy
			  ,[DocDate] = @DocDate
			  ,[DocTime] = @DocTime
			  ,[AdjPerPPE] = @AdjPerPPE
			  ,[StopJob] = @StopJob
			  ,[ChangingPosition] = @ChangingPosition
			  ,[RearrangingJob] = @RearrangingJob
			  ,[AttachingGrouds] = @AttachingGrouds
			  ,[PerformingLocks] = @PerformingLocks
			  ,[OtherReaction] = @OtherReaction
			  ,[Head] = @Head
			  ,[ArmsAndHands] = @ArmsAndHands
			  ,[EyesAndFace] = @EyesAndFace
			  ,[Trunk] = @Trunk
			  ,[Ears] = @Ears
			  ,[LegsAndFeet] = @LegsAndFeet
			  ,[RespiratorySystem] = @RespiratorySystem
			  ,[OtherPPE] = @OtherPPE
			  ,[HPES1] = @HPES1
			  ,[HPES2] = @HPES2
			  ,[HPES3] = @HPES3
			  ,[HPES4] = @HPES4
			  ,[HPES5] = @HPES5
			  ,[HPES6] = @HPES6
			  ,[StrikingAgaintObjects] = @StrikingAgaintObjects
			  ,[ChokingHazardousAir] = @ChokingHazardousAir
			  ,[CaughtInOn] = @CaughtInOn
			  ,[Inhaling] = @Inhaling
			  ,[Falling] = @Falling
			  ,[RepettiveMotions] = @RepettiveMotions
			  ,[Contacting] = @Contacting
			  ,[PositionsToWork] = @PositionsToWork
			  ,[ContactSharpObject] = @ContactSharpObject
			  ,[FlyingObjects] = @FlyingObjects
			  ,[ContactingElectric] = @ContactingElectric
			  ,[EmployeeWork] = @EmployeeWork
			  ,[NotRight] = @NotRight
			  ,[Unknow] = @Unknow
			  ,[UsedIncorrectly] = @UsedIncorrectly
			  ,[NotUnderstood] = @NotUnderstood
			  ,[InUnsafeCondition] = @InUnsafeCondition
			  ,[NotFlowed] = @NotFlowed
			  ,[NotAequate] = @NotAequate
			  ,[OtherProcedure] = @OtherProcedure
			  ,[OtherTools] = @OtherTools
			  ,[NotIsolate] = @NotIsolate
			  ,[ToolAndEquipment] = @ToolAndEquipment
			  ,[NoCollectGarbage] = @NoCollectGarbage
			  ,[EmergencyExit] = @EmergencyExit
			  ,[Slip] = @Slip
			  ,[OtherOderLine] = @OtherOderLine
			  ,[External]=@External
			  ,[DUNG_CU_THIET_BI]=@DUNG_CU_THIET_BI
			  ,[KHU_VUC_LAM_VIEC]=@KHU_VUC_LAM_VIEC
			  ,[MOI_TRUONG_LAM_VIEC]=@MOI_TRUONG_LAM_VIEC
			  ,[SACH_SE_NGAN_NAP]=@SACH_SE_NGAN_NAP
			  ,[LOAI_CONG_VIEC]=@LOAI_CONG_VIEC
			  ,[HO_TEN_CA_NHAN_TO_NHOM]=@HO_TEN_CA_NHAN_TO_NHOM
			  ,[HANH_DONG_HANH_VI_AN_TOAN]=@HANH_DONG_HANH_VI_AN_TOAN
			  ,[HANH_VI_KHONG_AN_TOAN]=@HANH_VI_KHONG_AN_TOAN
			  ,[KHAC_PHUC_TUC_THI]=@KHAC_PHUC_TUC_THI
			  ,IS_APPROVED=@IS_APPROVED,
			   APPROVAL_USER=@APPROVAL_USER
			  ,[TOtherOrderliness] = @TOtherOrderliness
			  ,[TOtherPPE] = @TOtherPPE
			  ,[TOtherProcedure] = @TOtherProcedure
			  ,[TOtherReactions] = @TOtherReactions
			  ,[TOtherToolAndEquipment] = @TOtherToolAndEquipment,
			   Description=@Description
			   ,[REPORT_PARENT]=@REPORT_PARENT,
			   IMG_PATCH1=@IMG_PATCH1
			   ,IMG_PATCH2 =@IMG_PATCH2,
			   Image_1 = @Image_1,
			   Image_2 = @Image_2,
			   NGUOILIENQUAN1=@NGUOILIENQUAN1,
			   NGUOILIENQUAN2=@NGUOILIENQUAN2
			WHERE ID=@ID
			
		END
		
		DELETE [dbo].[ST_StopCard1] WHERE [StopCardID]=@ID
		INSERT INTO [dbo].[ST_StopCard1]
				   ([StopCardID]
				   ,[PreventiveAction]
				   ,[PersonIncharge]
				   ,[Deadline]
				   ,[CompletedDate]
				   ,[Status]
				   ,[PersonReport]
				   ,[Description],MUCUUTIEN,[THIETBI],	[LOAIYEUCAU],	[NGUYENNHAN])
		SELECT @ID,[PreventiveAction],[PersonIncharge],[Deadline],[CompletedDate] ,[Status],[PersonReport],[Description],MUCUUTIEN,[THIETBI],	[LOAIYEUCAU],	[NGUYENNHAN] FROM @TB WHERE ISNULL(ID,-1) NOT IN (SELECT ID FROM [dbo].[ST_StopCard1])

		DELETE [dbo].[ST_StopCard2] WHERE [StopCardID]=@ID
		INSERT INTO [dbo].[ST_StopCard2]
				   ([StopCardID]
				   ,[PermanentAction]
				   ,[PersonIncharge]
				   ,[Deadline]
				   ,[CompletedDate]
				   ,[Status]
				   ,[PersonReport]
				   ,[Description],MUCUUTIEN,[THIETBI],	[LOAIYEUCAU],	[NGUYENNHAN])
		SELECT @ID,[PermanentAction],[PersonIncharge],[Deadline],[CompletedDate] ,[Status],[PersonReport],[Description],MUCUUTIEN,[THIETBI],	[LOAIYEUCAU],	[NGUYENNHAN] FROM @TB2 WHERE ISNULL(ID,-1) NOT IN (SELECT ID FROM [dbo].[ST_StopCard2])

		IF LEN(@NGUOILIENQUAN2)>0
		BEGIN
			IF NOT EXISTS	(SELECT * FROM ST_DT_NGOAICANG WHERE  UPPER([TENDOITUONG]) = UPPER(@NGUOILIENQUAN2) AND @NGUOILIENQUAN2 <> '')
			INSERT INTO ST_DT_NGOAICANG([TENDOITUONG],[REF_TYPE])
			SELECT @NGUOILIENQUAN2,3
		END
		
		---######START######TAO PHIEU YEU CAU TU DONG NEU CHUNG TU DA DUOC DUYET######START######---
		IF ISNULL(@CLICK_APPROVAL,'False')='True' AND EXISTS(SELECT * FROM DBO.ST_StopCard WHERE ID=@ID AND  IS_APPROVED='True')
		BEGIN
			SET @SLCT=0
				
			SELECT @SLCT=COUNT(*) FROM
			(
			SELECT T2.PreventiveAction,T2.ID,1 [Type]
			FROM DBO.ST_StopCard T1 INNER JOIN DBO.ST_StopCard1 T2 ON T1.ID=T2.StopCardID
			WHERE T1.ID=@ID AND ISNULL(T2.THIETBI,'') <> '' AND ISNULL(T2.LOAIYEUCAU,-1) <> -1 AND ISNULL(T2.NGUYENNHAN,-1) <> -1 AND ISNULL(T2.MUCUUTIEN,-1) <> -1
			union all
			
			SELECT T2.PermanentAction,T2.ID,2 [Type]
			FROM DBO.ST_StopCard T1 INNER JOIN DBO.ST_StopCard2 T2 ON T1.ID=T2.StopCardID
			WHERE T1.ID=@ID AND ISNULL(T2.THIETBI,'') <> '' AND ISNULL(T2.LOAIYEUCAU,-1) <> -1 AND ISNULL(T2.NGUYENNHAN,-1) <> -1 AND ISNULL(T2.MUCUUTIEN,-1) <> -1
			)T
			
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
				
				SELECT @DOCNO= DocNum  FROM DBO.ST_StopCard WHERE ID=@ID 
				
				DELETE YEU_CAU_NSD_CHI_TIET WHERE REF_TYPE='ST' AND REF_ID=@ID
				DELETE YEU_CAU_NSD WHERE REF_NO=@DOCNO AND REF_ID=@ID
				
				
				INSERT INTO YEU_CAU_NSD(MS_YEU_CAU,SO_YEU_CAU,NGAY,GIO_YEU_CAU,NGUOI_YEU_CAU,MS_N_XUONG,REF_NO,REF_ID,USER_COMMENT,USER_LAP)
				
				SELECT @MS_YEU_CAU,@MS_YEU_CAU,DocDate,DocTime,ISNULL( t.REPORT_PARENT,T.CreatedBy),'BARI',T.DocNum,T.ID,t.[Description],ISNULL( t.REPORT_PARENT,T.CreatedBy)
				FROM DBO.ST_StopCard T
				WHERE ID=@ID
				
				
				SET @STT=ISNULL((SELECT TOP 1 STT FROM YEU_CAU_NSD WHERE MS_YEU_CAU=@MS_YEU_CAU),0)
				
				INSERT INTO YEU_CAU_NSD_CHI_TIET(STT,MS_MAY,MO_TA_TINH_TRANG,MS_LOAI_YEU_CAU_BT,MS_NGUYEN_NHAN,REF_TYPE,REF_NO,REF_ID,DETAIL_TYPE,LINE_ID,MS_UU_TIEN,USERNAME_DSX,THOI_GIAN_DSX,Y_KIEN_DSX,THUC_HIEN_DSX,YEU_CAU)
				SELECT @STT,T2.THIETBI,T1.[Description],T2.LOAIYEUCAU,T2.NGUYENNHAN,'ST' ,T1.DocNum,T1.ID,'PreventiveAction1',T2.ID,T2.MUCUUTIEN,T1.APPROVAL_USER,GETDATE(),N'Duyệt tự động từ Safety (Chứng từ (Ecomaint AT): ' + t1.DocNum+')',1,T2.PreventiveAction
				FROM DBO.ST_StopCard T1 INNER JOIN DBO.ST_StopCard1 T2 ON T1.ID=T2.StopCardID
				WHERE T1.ID=@ID AND ISNULL(T2.THIETBI,'') <> '' AND ISNULL(T2.LOAIYEUCAU,-1) <> -1 AND ISNULL(T2.NGUYENNHAN,-1) <> -1 AND ISNULL(T2.MUCUUTIEN,-1) <> -1
				
				INSERT INTO YEU_CAU_NSD_CHI_TIET(STT,MS_MAY,MO_TA_TINH_TRANG,MS_LOAI_YEU_CAU_BT,MS_NGUYEN_NHAN,REF_TYPE,REF_NO,REF_ID,DETAIL_TYPE,LINE_ID,MS_UU_TIEN,USERNAME_DSX,THOI_GIAN_DSX,Y_KIEN_DSX,THUC_HIEN_DSX,YEU_CAU)
				SELECT @STT,T2.THIETBI,T1.[Description],T2.LOAIYEUCAU,T2.NGUYENNHAN,'ST' ,T1.DocNum,T1.ID,'PreventiveAction2',T2.ID,T2.MUCUUTIEN,T1.APPROVAL_USER,GETDATE(),N'Duyệt tự động từ Safety (Chứng từ (Ecomaint AT): ' + t1.DocNum+')',1,T2.PermanentAction
				FROM DBO.ST_StopCard T1 INNER JOIN DBO.ST_StopCard2 T2 ON T1.ID=T2.StopCardID
				WHERE T1.ID=@ID AND ISNULL(T2.THIETBI,'') <> '' AND ISNULL(T2.LOAIYEUCAU,-1) <> -1 AND ISNULL(T2.NGUYENNHAN,-1) <> -1 AND ISNULL(T2.MUCUUTIEN,-1) <> -1
			END
		END
		---######END######TAO PHIEU YEU CAU TU DONG NEU CHUNG TU DA DUOC DUYET######END######---
		
	END
	IF @ACTION='DELETE'
	BEGIN
		UPDATE [dbo].[ST_StopCard] SET IS_DELETE='True' WHERE ID=@ID
		--IF EXISTS(SELECT * FROM [dbo].[ST_StopCard] WHERE ID=@ID)
		--BEGIN
		--	DELETE [dbo].[ST_StopCard] WHERE ID=@ID
		--END
		--IF EXISTS(SELECT * FROM [dbo].[ST_StopCard1] WHERE [StopCardID]=@ID)
		--BEGIN
		--	DELETE [dbo].[ST_StopCard1] WHERE [StopCardID]=@ID
		--END
		--IF EXISTS(SELECT * FROM [dbo].[ST_StopCard2] WHERE [StopCardID]=@ID)
		--BEGIN
		--	DELETE [dbo].[ST_StopCard2] WHERE [StopCardID]=@ID
		--END
		
	END

END






