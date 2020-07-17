USE MASTER
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[$(database)]') AND type in (N'U'))
  ALTER DATABASE $(database) SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
 
create table #backupInformation (LogicalName varchar(100),
PhysicalName varchar(100),
Type varchar(1),
FileGroupName varchar(50) ,
Size bigint ,
MaxSize bigint,
FileId int,
CreateLSN int,
DropLSN int,
UniqueId uniqueidentifier,
ReadOnlyLSN int,
ReadWriteLSN int,
BackupSizeInBytes int,
SourceBlockSize int,
FileGroupId int,
LogGroupGUID uniqueidentifier,
DifferentialBaseLSN bigint,
DifferentialBaseGUID uniqueidentifier,
IsReadOnly bit, IsPresent bit )
 
insert into #backupInformation exec('restore filelistonly from disk = ''$(root)\$(database).bak''')
 
DECLARE @logicalNameD varchar(255);
DECLARE @logicalNameL varchar(255);
 
select top 1 @logicalNameD = LogicalName from #backupInformation where Type = 'D';
select top 1 @logicalNameL = LogicalName from #backupInformation where Type = 'L';
 
DROP TABLE #backupInformation 
 
RESTORE DATABASE $(database)
FROM DISK = '$(root)\$(database).bak'
WITH REPLACE,
MOVE @logicalNameD TO '$(programfiles)\Microsoft SQL Server\MSSQL.3\MSSQL\Data\$(database).mdf',
MOVE @logicalNameL TO '$(programfiles)\Microsoft SQL Server\MSSQL.3\MSSQL\Data\$(database).ldf'
GO
