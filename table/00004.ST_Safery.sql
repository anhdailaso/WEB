if not exists(select * from sys.columns 
           where Name = N'Image_1' and Object_ID = Object_ID(N'ST_HazardReport'))
begin
ALTER TABLE dbo.ST_HazardReport ADD Image_1 IMAGE END  

if not exists(select * from sys.columns 
           where Name = N'Image_1' and Object_ID = Object_ID(N'ST_StopCard'))
begin
ALTER TABLE dbo.ST_StopCard ADD Image_1 IMAGE END  
