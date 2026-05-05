USE master;
GO

/* =========================================================
   Резервное копирование базы данных ConferenceDB
   ========================================================= */

BACKUP DATABASE ConferenceDB
TO DISK = N'C:\Backup\ConferenceDB_full.bak'
WITH
    FORMAT,
    INIT,
    COMPRESSION,
    NAME = N'Full Backup of ConferenceDB',
    DESCRIPTION = N'Полная резервная копия базы данных ConferenceDB',
    STATS = 10;
GO