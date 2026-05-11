USE ConferenceDB;
GO

IF OBJECT_ID('dbo.vw_section_visit_feedback', 'V') IS NOT NULL DROP VIEW dbo.vw_section_visit_feedback;
IF OBJECT_ID('dbo.vw_materials', 'V') IS NOT NULL DROP VIEW dbo.vw_materials;
IF OBJECT_ID('dbo.vw_accepted_reports', 'V') IS NOT NULL DROP VIEW dbo.vw_accepted_reports;
IF OBJECT_ID('dbo.vw_section_popularity', 'V') IS NOT NULL DROP VIEW dbo.vw_section_popularity;
IF OBJECT_ID('dbo.vw_review_results', 'V') IS NOT NULL DROP VIEW dbo.vw_review_results;
IF OBJECT_ID('dbo.vw_conference_program', 'V') IS NOT NULL DROP VIEW dbo.vw_conference_program;
IF OBJECT_ID('dbo.vw_reports_with_authors', 'V') IS NOT NULL DROP VIEW dbo.vw_reports_with_authors;
IF OBJECT_ID('dbo.vw_participants_list', 'V') IS NOT NULL DROP VIEW dbo.vw_participants_list;
GO

CREATE VIEW dbo.vw_participants_list AS
SELECT
    id_participant,
    LTRIM(RTRIM(last_name + N' ' + first_name + N' ' + ISNULL(middle_name, N''))) AS full_name,
    email, phone, participant_status, user_role, workplace, academic_degree
FROM dbo.tb_participants;
GO

CREATE VIEW dbo.vw_reports_with_authors AS
SELECT
    r.id_report, r.topic, r.annotation, r.keywords, r.review_status,
    r.file_name, r.file_extension,
    DATALENGTH(r.file_content) AS file_size_bytes,
    CASE WHEN r.file_content IS NOT NULL THEN N'Файл загружен' ELSE N'Файл отсутствует' END AS file_status,
    LTRIM(RTRIM(p.last_name + N' ' + p.first_name + N' ' + ISNULL(p.middle_name, N''))) AS author_name,
    p.email AS author_email,
    p.workplace
FROM dbo.tb_reports AS r
JOIN dbo.tb_participants AS p ON r.id_author = p.id_participant;
GO

CREATE VIEW dbo.vw_conference_program AS
SELECT
    cp.id_presentation, cp.presentation_date, cp.presentation_time, cp.location,
    s.id_section, s.section_name, s.max_participants,
    r.id_report, r.topic, r.review_status, r.file_name, r.file_extension,
    LTRIM(RTRIM(p.last_name + N' ' + p.first_name + N' ' + ISNULL(p.middle_name, N''))) AS author_name
FROM dbo.tb_conference_program AS cp
JOIN dbo.tb_reports AS r ON cp.id_report = r.id_report
JOIN dbo.tb_sections AS s ON cp.id_section = s.id_section
JOIN dbo.tb_participants AS p ON r.id_author = p.id_participant;
GO

CREATE VIEW dbo.vw_review_results AS
SELECT
    rv.id_review, r.id_report, r.topic,
    rv.novelty_score, rv.relevance_score, rv.quality_score,
    (rv.novelty_score + rv.relevance_score + rv.quality_score) / 3.0 AS average_score,
    rv.review_result, rv.comments,
    LTRIM(RTRIM(p.last_name + N' ' + p.first_name + N' ' + ISNULL(p.middle_name, N''))) AS reviewer_name
FROM dbo.tb_reviews AS rv
JOIN dbo.tb_reports AS r ON rv.id_report = r.id_report
JOIN dbo.tb_participants AS p ON rv.id_reviewer = p.id_participant;
GO

CREATE VIEW dbo.vw_section_popularity AS
SELECT
    s.id_section,
    s.section_name,
    s.description,
    s.max_participants,
    COUNT(sv.id_visit) AS visitors_count,
    s.max_participants - COUNT(sv.id_visit) AS free_places,
    CAST(COUNT(sv.id_visit) * 100.0 / NULLIF(s.max_participants, 0) AS DECIMAL(5,2)) AS fill_percent
FROM dbo.tb_sections AS s
LEFT JOIN dbo.tb_section_visits AS sv ON s.id_section = sv.id_section
GROUP BY s.id_section, s.section_name, s.description, s.max_participants;
GO

CREATE VIEW dbo.vw_accepted_reports AS
SELECT
    r.id_report, r.topic, r.annotation, r.keywords,
    r.file_name, r.file_extension,
    DATALENGTH(r.file_content) AS file_size_bytes,
    CASE WHEN r.file_content IS NOT NULL THEN N'Файл загружен' ELSE N'Файл отсутствует' END AS file_status,
    LTRIM(RTRIM(p.last_name + N' ' + p.first_name + N' ' + ISNULL(p.middle_name, N''))) AS author_name,
    p.workplace
FROM dbo.tb_reports AS r
JOIN dbo.tb_participants AS p ON r.id_author = p.id_participant
WHERE r.review_status = N'Принят';
GO

CREATE VIEW dbo.vw_materials AS
SELECT
    m.id_material,
    m.material_title,
    m.material_description,
    m.file_name,
    m.file_extension,
    DATALENGTH(m.file_content) AS file_size_bytes,
    m.upload_date,
    m.id_report,
    r.topic AS report_topic,
    m.id_section,
    s.section_name,
    m.created_by,
    LTRIM(RTRIM(p.last_name + N' ' + p.first_name + N' ' + ISNULL(p.middle_name, N''))) AS created_by_name
FROM dbo.tb_materials AS m
LEFT JOIN dbo.tb_reports AS r ON m.id_report = r.id_report
LEFT JOIN dbo.tb_sections AS s ON m.id_section = s.id_section
LEFT JOIN dbo.tb_participants AS p ON m.created_by = p.id_participant;
GO

CREATE VIEW dbo.vw_section_visit_feedback AS
SELECT
    sv.id_visit,
    sv.id_participant,
    LTRIM(RTRIM(p.last_name + N' ' + p.first_name + N' ' + ISNULL(p.middle_name, N''))) AS participant_name,
    sv.id_section,
    s.section_name,
    sv.organization_score,
    sv.content_score,
    sv.usefulness_score,
    CASE
        WHEN sv.organization_score IS NULL OR sv.content_score IS NULL OR sv.usefulness_score IS NULL
        THEN NULL
        ELSE (sv.organization_score + sv.content_score + sv.usefulness_score) / 3.0
    END AS average_visit_score,
    sv.visit_comment
FROM dbo.tb_section_visits AS sv
JOIN dbo.tb_participants AS p ON sv.id_participant = p.id_participant
JOIN dbo.tb_sections AS s ON sv.id_section = s.id_section;
GO
