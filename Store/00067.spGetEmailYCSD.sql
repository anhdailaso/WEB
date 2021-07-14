IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spGetEmailYCSD')
   exec('CREATE PROCEDURE spGetEmailYCSD AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROC spGetEmailYCSD
	@sNXCha NVARCHAR(MAX) = 'VF01.01.01.01',
	@MsLoaiGoiMail INT = 1,
	@sUName NVARCHAR(100) = '8091117',
	@sMailThem NVARCHAR(1000) = 'mail@mail.com;mail@mail.com'
AS	
--@MsLoaiGoiMail = 1 là yeu cau
--@MsLoaiGoiMail = 2 là duyet yeu cau
DECLARE @YCSD_EMAIL TABLE (
	[EMAIL] [nvarchar](MAX)  NULL
)  
--REPLACE(ISNULL(RTRIM(LTRIM(EMAIL)),''''), CHAR(13) + CHAR(10), '')
DECLARE @sSql NVARCHAR(MAX) = '  SELECT ISNULL(RTRIM(LTRIM(EMAIL)),'''') AS EMAIL  FROM dbo.NHA_XUONG_EMAIL WHERE  (MS_LOAI_EMAIL = 1) AND (MS_N_XUONG = ' + char(39)  + @sNXCha + char(39) + ') '
SELECT @sNXCha = DBO.MGetNXUongCha(@sNXCha) 
--SELECT POS,[VALUE] FROM dbo.MGetSplit(@sNX,';')
Declare @Pos INT, @VALUE nvarchar (1000)


Declare  TTNhap CURSOR FOR
	SELECT * FROM dbo.MGetSplit(@sNXCha,';')
Open TTNhap
FETCH NEXT FROM TTNhap
INTO @Pos,@VALUE
While @@Fetch_Status = 0
BEGIN
	SET @sSql = @sSql  + 	' UNION SELECT ISNULL(RTRIM(LTRIM(EMAIL)),'''') AS EMAIL  FROM dbo.NHA_XUONG_EMAIL WHERE (MS_LOAI_EMAIL = 1) AND MS_N_XUONG = ' + char(39)  + @VALUE + char(39)  
    FETCH NEXT FROM TTNhap
    INTO  @Pos,@VALUE

End

Close TTNhap
Deallocate TTNhap

INSERT INTO @YCSD_EMAIL
exec (@sSql)
INSERT INTO @YCSD_EMAIL
SELECT ISNULL(USER_MAIL,'') FROM dbo.USERS WHERE USERNAME = @sUName
UNION	
SELECT @sMailThem
--SELECT * FROM @YCSD_EMAIL
declare @sName nvarchar(max)

	SELECT @sName =COALESCE(ISNULL(@sName,'') + CASE LEN(ISNULL(@sName,'')) WHEN 0 THEN '' ELSE ';' END  ,'' )  + EMAIL
	FROM
	(
		SELECT DISTINCT REPLACE(RTRIM(LTRIM(EMAIL)), CHAR(13) + CHAR(10), '') AS EMAIL FROM @YCSD_EMAIL
	) T1



SELECT DISTINCT [VALUE] INTO #TMP FROM dbo.MGetSplit(@sName,';')
declare @sName1 nvarchar(max)
	SELECT @sName1 =COALESCE(ISNULL(@sName1,'') + CASE LEN(ISNULL(@sName1,'')) WHEN 0 THEN '' ELSE ';' END  ,'' )  + [VALUE]
	FROM
	(
		SELECT DISTINCT REPLACE(LTRIM(RTRIM([VALUE])), CHAR(13) + CHAR(10), '')  AS [VALUE] FROM #TMP
	) T1

--SELECT DISTINCT REPLACE(LTRIM(RTRIM([VALUE])), CHAR(13) + CHAR(10), '')  AS	EMAIL_YCSD FROM #TMP
SELECT @sName1 AS EMAIL_YCSD
