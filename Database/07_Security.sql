USE ConferenceDB;
GO

/* =========================================================
   Удаление пользователей и ролей при повторном создании
   ========================================================= */

IF USER_ID(N'user_participant') IS NOT NULL DROP USER user_participant;
IF USER_ID(N'user_reviewer') IS NOT NULL DROP USER user_reviewer;
IF USER_ID(N'user_organizer') IS NOT NULL DROP USER user_organizer;
IF USER_ID(N'user_admin') IS NOT NULL DROP USER user_admin;
GO

IF DATABASE_PRINCIPAL_ID(N'role_participant') IS NOT NULL DROP ROLE role_participant;
IF DATABASE_PRINCIPAL_ID(N'role_reviewer') IS NOT NULL DROP ROLE role_reviewer;
IF DATABASE_PRINCIPAL_ID(N'role_organizer') IS NOT NULL DROP ROLE role_organizer;
IF DATABASE_PRINCIPAL_ID(N'role_admin') IS NOT NULL DROP ROLE role_admin;
GO


/* =========================================================
   Создание ролей
   ========================================================= */

CREATE ROLE role_participant;
CREATE ROLE role_reviewer;
CREATE ROLE role_organizer;
CREATE ROLE role_admin;
GO


/* =========================================================
   Создание учебных пользователей без логинов
   ========================================================= */

CREATE USER user_participant WITHOUT LOGIN;
CREATE USER user_reviewer WITHOUT LOGIN;
CREATE USER user_organizer WITHOUT LOGIN;
CREATE USER user_admin WITHOUT LOGIN;
GO


/* =========================================================
   Назначение пользователей ролям
   ========================================================= */

ALTER ROLE role_participant ADD MEMBER user_participant;
ALTER ROLE role_reviewer ADD MEMBER user_reviewer;
ALTER ROLE role_organizer ADD MEMBER user_organizer;
ALTER ROLE role_admin ADD MEMBER user_admin;
GO


/* =========================================================
   Права участника
   ========================================================= */

GRANT SELECT ON dbo.vw_conference_program TO role_participant;
GRANT SELECT ON dbo.vw_accepted_reports TO role_participant;
GRANT SELECT ON dbo.vw_section_popularity TO role_participant;

GRANT EXECUTE ON dbo.usp_add_participant TO role_participant;
GRANT EXECUTE ON dbo.usp_add_report TO role_participant;
GRANT EXECUTE ON dbo.usp_register_section_visit TO role_participant;
GO


/* =========================================================
   Права рецензента
   ========================================================= */

GRANT SELECT ON dbo.vw_reports_with_authors TO role_reviewer;
GRANT SELECT ON dbo.vw_review_results TO role_reviewer;

GRANT EXECUTE ON dbo.usp_add_review TO role_reviewer;
GO


/* =========================================================
   Права организатора
   ========================================================= */

GRANT SELECT ON dbo.vw_participants_list TO role_organizer;
GRANT SELECT ON dbo.vw_reports_with_authors TO role_organizer;
GRANT SELECT ON dbo.vw_conference_program TO role_organizer;
GRANT SELECT ON dbo.vw_review_results TO role_organizer;
GRANT SELECT ON dbo.vw_section_popularity TO role_organizer;
GRANT SELECT ON dbo.vw_accepted_reports TO role_organizer;

GRANT EXECUTE ON dbo.usp_add_participant TO role_organizer;
GRANT EXECUTE ON dbo.usp_add_report TO role_organizer;
GRANT EXECUTE ON dbo.usp_add_review TO role_organizer;
GRANT EXECUTE ON dbo.usp_register_section_visit TO role_organizer;
GRANT EXECUTE ON dbo.usp_add_conference_program TO role_organizer;
GRANT EXECUTE ON dbo.usp_update_report_status TO role_organizer;
GO


/* =========================================================
   Права администратора
   ========================================================= */

GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.tb_participants TO role_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.tb_sections TO role_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.tb_reports TO role_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.tb_reviews TO role_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.tb_conference_program TO role_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.tb_section_visits TO role_admin;

GRANT SELECT ON dbo.vw_participants_list TO role_admin;
GRANT SELECT ON dbo.vw_reports_with_authors TO role_admin;
GRANT SELECT ON dbo.vw_conference_program TO role_admin;
GRANT SELECT ON dbo.vw_review_results TO role_admin;
GRANT SELECT ON dbo.vw_section_popularity TO role_admin;
GRANT SELECT ON dbo.vw_accepted_reports TO role_admin;

GRANT EXECUTE ON dbo.usp_add_participant TO role_admin;
GRANT EXECUTE ON dbo.usp_add_report TO role_admin;
GRANT EXECUTE ON dbo.usp_add_review TO role_admin;
GRANT EXECUTE ON dbo.usp_register_section_visit TO role_admin;
GRANT EXECUTE ON dbo.usp_add_conference_program TO role_admin;
GRANT EXECUTE ON dbo.usp_update_report_status TO role_admin;
GO