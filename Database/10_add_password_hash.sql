USE ConferenceDB;
GO

/* =========================================================
   Добавление поля password_hash, если оно отсутствует
   ========================================================= */

IF COL_LENGTH('dbo.tb_participants', 'password_hash') IS NULL
BEGIN
    ALTER TABLE dbo.tb_participants
    ADD password_hash NVARCHAR(64) NOT NULL
        CONSTRAINT df_tb_participants_password_hash
        DEFAULT N'5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5'
        WITH VALUES;
END;
GO