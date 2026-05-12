USE master;
GO

DECLARE @BackupPath NVARCHAR(4000);

SET @BackupPath = CONVERT(NVARCHAR(4000), SERVERPROPERTY('InstanceDefaultBackupPath'));

IF RIGHT(@BackupPath, 1) <> N'\'
    SET @BackupPath = @BackupPath + N'\';

SET @BackupPath = @BackupPath + N'ConferenceDB_full.bak';

PRINT @BackupPath;

BACKUP DATABASE ConferenceDB
TO DISK = @BackupPath
WITH
    FORMAT,
    INIT,
    NAME = N'Full Backup of ConferenceDB',
    DESCRIPTION = N'Полная резервная копия базы данных ConferenceDB',
    STATS = 10;
GO