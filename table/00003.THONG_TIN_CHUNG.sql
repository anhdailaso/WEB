if not exists(select * from sys.columns 
           where Name = N'LINK_WEB' and Object_ID = Object_ID(N'THONG_TIN_CHUNG'))
begin
ALTER TABLE dbo.THONG_TIN_CHUNG ADD LINK_WEB NVARCHAR(200)END    

if not exists(select * from sys.columns 
           where Name = N'ImageGS' and Object_ID = Object_ID(N'GIAM_SAT_TINH_TRANG_TS'))
begin
ALTER TABLE dbo.GIAM_SAT_TINH_TRANG_TS ADD ImageGS IMAGE END    


