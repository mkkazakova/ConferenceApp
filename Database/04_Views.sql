USE ConferenceDB;
GO

/* =========================================================
   1. Представление: список участников
   ========================================================= */

IF OBJECT_ID('dbo.vw_participants_list', 'V') IS NOT NULL
    DROP VIEW dbo.vw_participants_list;
GO

CREATE VIEW dbo.vw_participants_list
AS
SELECT
    id_participant,
    LTRIM(RTRIM(last_name + N' ' + first_name + N' ' + ISNULL(middle_name, N''))) AS full_name,
    email,
    phone,
    participant_status,
    user_role,
    workplace,
    academic_degree
FROM dbo.tb_participants;
GO


/* =========================================================
   2. Представление: доклады с авторами
   ========================================================= */

IF OBJECT_ID('dbo.vw_reports_with_authors', 'V') IS NOT NULL
    DROP VIEW dbo.vw_reports_with_authors;
GO

CREATE VIEW dbo.vw_reports_with_authors
AS
SELECT
    r.id_report,
    r.topic,
    r.annotation,
    r.keywords,
    r.review_status,
    r.file_name,
    r.file_extension,
    DATALENGTH(r.file_content) AS file_size_bytes,
    CASE
        WHEN r.file_content IS NOT NULL THEN N'Файл загружен'
        ELSE N'Файл отсутствует'
    END AS file_status,
    LTRIM(RTRIM(p.last_name + N' ' + p.first_name + N' ' + ISNULL(p.middle_name, N''))) AS author_name,
    p.email AS author_email,
    p.workplace
FROM dbo.tb_reports AS r
JOIN dbo.tb_participants AS p
    ON r.id_author = p.id_participant;
GO


/* =========================================================
   3. Представление: программа конференции
   ========================================================= */

IF OBJECT_ID('dbo.vw_conference_program', 'V') IS NOT NULL
    DROP VIEW dbo.vw_conference_program;
GO

CREATE VIEW dbo.vw_conference_program
AS
SELECT
    cp.id_presentation,
    cp.presentation_date,
    cp.presentation_time,
    cp.location,
    s.section_name,
    r.topic,
    r.review_status,
    r.file_name,
    r.file_extension,
    LTRIM(RTRIM(p.last_name + N' ' + p.first_name + N' ' + ISNULL(p.middle_name, N''))) AS author_name
FROM dbo.tb_conference_program AS cp
JOIN dbo.tb_reports AS r
    ON cp.id_report = r.id_report
JOIN dbo.tb_sections AS s
    ON cp.id_section = s.id_section
JOIN dbo.tb_participants AS p
    ON r.id_author = p.id_participant;
GO


/* =========================================================
   4. Представление: результаты рецензирования
   ========================================================= */

IF OBJECT_ID('dbo.vw_review_results', 'V') IS NOT NULL
    DROP VIEW dbo.vw_review_results;
GO

CREATE VIEW dbo.vw_review_results
AS
SELECT
    rv.id_review,
    r.id_report,
    r.topic,
    rv.novelty_score,
    rv.relevance_score,
    rv.quality_score,
    (rv.novelty_score + rv.relevance_score + rv.quality_score) / 3.0 AS average_score,
    rv.review_result,
    rv.comments,
    LTRIM(RTRIM(p.last_name + N' ' + p.first_name + N' ' + ISNULL(p.middle_name, N''))) AS reviewer_name
FROM dbo.tb_reviews AS rv
JOIN dbo.tb_reports AS r
    ON rv.id_report = r.id_report
JOIN dbo.tb_participants AS p
    ON rv.id_reviewer = p.id_participant;
GO


/* =========================================================
   5. Представление: популярность секций
   ========================================================= */

IF OBJECT_ID('dbo.vw_section_popularity', 'V') IS NOT NULL
    DROP VIEW dbo.vw_section_popularity;
GO

CREATE VIEW dbo.vw_section_popularity
AS
SELECT
    s.id_section,
    s.section_name,
    COUNT(sv.id_visit) AS visitors_count
FROM dbo.tb_sections AS s
LEFT JOIN dbo.tb_section_visits AS sv
    ON s.id_section = sv.id_section
GROUP BY
    s.id_section,
    s.section_name;
GO


/* =========================================================
   6. Представление: принятые доклады
   ========================================================= */

IF OBJECT_ID('dbo.vw_accepted_reports', 'V') IS NOT NULL
    DROP VIEW dbo.vw_accepted_reports;
GO

CREATE VIEW dbo.vw_accepted_reports
AS
SELECT
    r.id_report,
    r.topic,
    r.annotation,
    r.keywords,
    r.file_name,
    r.file_extension,
    DATALENGTH(r.file_content) AS file_size_bytes,
    CASE
        WHEN r.file_content IS NOT NULL THEN N'Файл загружен'
        ELSE N'Файл отсутствует'
    END AS file_status,
    LTRIM(RTRIM(p.last_name + N' ' + p.first_name + N' ' + ISNULL(p.middle_name, N''))) AS author_name,
    p.workplace
FROM dbo.tb_reports AS r
JOIN dbo.tb_participants AS p
    ON r.id_author = p.id_participant
WHERE r.review_status = N'Принят';
GO