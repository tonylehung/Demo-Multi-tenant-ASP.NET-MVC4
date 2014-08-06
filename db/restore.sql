
USE master;
GO

IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'ort')
DROP DATABASE [ort]
GO
RESTORE DATABASE [ort]
FROM DISK = 'D:\database\ort.bak'
WITH
MOVE 'ort' TO 'D:\database\ort.mdf',
MOVE 'ort_log' TO 'D:\database\ort_log.mdf'
GO