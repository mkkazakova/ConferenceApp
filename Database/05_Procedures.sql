USE ConferenceDB;
GO

IF OBJECT_ID('dbo.usp_add_participant', 'P') IS NOT NULL DROP PROCEDURE dbo.usp_add_participant;
IF OBJECT_ID('dbo.usp_add_report', 'P') IS NOT NULL DROP PROCEDURE dbo.usp_add_report;
IF OBJECT_ID('dbo.usp_add_review', 'P') IS NOT NULL DROP PROCEDURE dbo.usp_add_review;
IF OBJECT_ID('dbo.usp_register_section_visit', 'P') IS NOT NULL DROP PROCEDURE dbo.usp_register_section_visit;
IF OBJECT_ID('dbo.usp_update_section_visit_feedback', 'P') IS NOT NULL DROP PROCEDURE dbo.usp_update_section_visit_feedback;
IF OBJECT_ID('dbo.usp_add_material', 'P') IS NOT NULL DROP PROCEDURE dbo.usp_add_material;
IF OBJECT_ID('dbo.usp_add_conference_program', 'P') IS NOT NULL DROP PROCEDURE dbo.usp_add_conference_program;
IF OBJECT_ID('dbo.usp_update_report_status', 'P') IS NOT NULL DROP PROCEDURE dbo.usp_update_report_status;
GO

CREATE PROCEDURE dbo.usp_add_participant
    @last_name NVARCHAR(100),
    @first_name NVARCHAR(100),
    @email NVARCHAR(255),
    @participant_status NVARCHAR(50),
    @user_role NVARCHAR(50),
    @middle_name NVARCHAR(100) = NULL,
    @phone NVARCHAR(30) = NULL,
    @workplace NVARCHAR(255) = NULL,
    @academic_degree NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.tb_participants WHERE email = @email)
    BEGIN
        THROW 50001, N'Участник с таким email уже существует.', 1;
    END;

    INSERT INTO dbo.tb_participants
    (
        last_name, first_name, middle_name, email, phone,
        participant_status, user_role, workplace, academic_degree
    )
    VALUES
    (
        @last_name, @first_name, @middle_name, @email, @phone,
        @participant_status, @user_role, @workplace, @academic_degree
    );
END;
GO

CREATE PROCEDURE dbo.usp_add_report
    @topic NVARCHAR(300),
    @id_author INT,
    @annotation NVARCHAR(MAX) = NULL,
    @keywords NVARCHAR(300) = NULL,
    @file_name NVARCHAR(255) = NULL,
    @file_extension NVARCHAR(20) = NULL,
    @file_content VARBINARY(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @file_name = NULLIF(LTRIM(RTRIM(@file_name)), N'');
    SET @file_extension = NULLIF(LTRIM(RTRIM(@file_extension)), N'');

    IF NOT EXISTS (SELECT 1 FROM dbo.tb_participants WHERE id_participant = @id_author)
    BEGIN
        THROW 50002, N'Автор доклада не найден.', 1;
    END;

    IF NOT EXISTS
    (
        SELECT 1
        FROM dbo.tb_participants
        WHERE id_participant = @id_author
          AND participant_status = N'Докладчик'
    )
    BEGIN
        THROW 50003, N'Доклад может добавить только участник со статусом "Докладчик".', 1;
    END;

    IF NOT
    (
        (@file_name IS NULL AND @file_extension IS NULL AND @file_content IS NULL)
        OR
        (@file_name IS NOT NULL AND @file_extension IS NOT NULL AND @file_content IS NOT NULL)
    )
    BEGIN
        THROW 50017, N'Файл должен быть заполнен полностью: имя, расширение и содержимое.', 1;
    END;

    INSERT INTO dbo.tb_reports
    (
        topic, annotation, keywords, review_status,
        file_name, file_extension, file_content, id_author
    )
    VALUES
    (
        @topic, @annotation, @keywords, N'На рассмотрении',
        @file_name, @file_extension, @file_content, @id_author
    );
END;
GO

CREATE PROCEDURE dbo.usp_add_review
    @id_report INT,
    @id_reviewer INT,
    @novelty_score INT,
    @relevance_score INT,
    @quality_score INT,
    @review_result NVARCHAR(30),
    @comments NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.tb_reports WHERE id_report = @id_report)
    BEGIN
        THROW 50004, N'Доклад не найден.', 1;
    END;

    IF NOT EXISTS
    (
        SELECT 1
        FROM dbo.tb_participants
        WHERE id_participant = @id_reviewer
          AND user_role = N'Рецензент'
    )
    BEGIN
        THROW 50005, N'Рецензент не найден или пользователь не имеет роли "Рецензент".', 1;
    END;

    IF EXISTS
    (
        SELECT 1
        FROM dbo.tb_reviews
        WHERE id_report = @id_report
          AND id_reviewer = @id_reviewer
    )
    BEGIN
        THROW 50006, N'Данный рецензент уже оставил рецензию на этот доклад.', 1;
    END;

    INSERT INTO dbo.tb_reviews
    (
        comments, novelty_score, relevance_score, quality_score,
        review_result, id_report, id_reviewer
    )
    VALUES
    (
        @comments, @novelty_score, @relevance_score, @quality_score,
        @review_result, @id_report, @id_reviewer
    );
END;
GO

CREATE PROCEDURE dbo.usp_register_section_visit
    @id_participant INT,
    @id_section INT,
    @organization_score INT = NULL,
    @content_score INT = NULL,
    @usefulness_score INT = NULL,
    @visit_comment NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.tb_participants WHERE id_participant = @id_participant)
    BEGIN
        THROW 50007, N'Участник не найден.', 1;
    END;

    IF NOT EXISTS (SELECT 1 FROM dbo.tb_sections WHERE id_section = @id_section)
    BEGIN
        THROW 50008, N'Секция не найдена.', 1;
    END;

    IF EXISTS
    (
        SELECT 1
        FROM dbo.tb_section_visits
        WHERE id_participant = @id_participant
          AND id_section = @id_section
    )
    BEGIN
        THROW 50009, N'Участник уже записан на эту секцию.', 1;
    END;

    IF EXISTS
    (
        SELECT 1
        FROM dbo.tb_sections AS s
        WHERE s.id_section = @id_section
          AND
          (
              SELECT COUNT(*)
              FROM dbo.tb_section_visits AS sv
              WHERE sv.id_section = @id_section
          ) >= s.max_participants
    )
    BEGIN
        THROW 50018, N'В выбранной секции нет свободных мест.', 1;
    END;

    INSERT INTO dbo.tb_section_visits
    (
        id_participant, id_section,
        organization_score, content_score, usefulness_score, visit_comment
    )
    VALUES
    (
        @id_participant, @id_section,
        @organization_score, @content_score, @usefulness_score, @visit_comment
    );
END;
GO

CREATE PROCEDURE dbo.usp_update_section_visit_feedback
    @id_visit INT,
    @organization_score INT,
    @content_score INT,
    @usefulness_score INT,
    @visit_comment NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.tb_section_visits WHERE id_visit = @id_visit)
    BEGIN
        THROW 50019, N'Запись о посещении секции не найдена.', 1;
    END;

    UPDATE dbo.tb_section_visits
    SET
        organization_score = @organization_score,
        content_score = @content_score,
        usefulness_score = @usefulness_score,
        visit_comment = @visit_comment
    WHERE id_visit = @id_visit;
END;
GO

CREATE PROCEDURE dbo.usp_add_material
    @material_title NVARCHAR(300),
    @file_name NVARCHAR(255),
    @file_extension NVARCHAR(20),
    @file_content VARBINARY(MAX),
    @material_description NVARCHAR(MAX) = NULL,
    @id_report INT = NULL,
    @id_section INT = NULL,
    @created_by INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @file_name = NULLIF(LTRIM(RTRIM(@file_name)), N'');
    SET @file_extension = NULLIF(LTRIM(RTRIM(@file_extension)), N'');

    IF @id_report IS NULL AND @id_section IS NULL
    BEGIN
        THROW 50020, N'Материал должен быть связан с докладом или секцией.', 1;
    END;

    IF @file_name IS NULL OR @file_extension IS NULL OR @file_content IS NULL
    BEGIN
        THROW 50021, N'Файл материала должен быть заполнен полностью.', 1;
    END;

    IF @id_report IS NOT NULL
       AND NOT EXISTS (SELECT 1 FROM dbo.tb_reports WHERE id_report = @id_report)
    BEGIN
        THROW 50022, N'Доклад для материала не найден.', 1;
    END;

    IF @id_section IS NOT NULL
       AND NOT EXISTS (SELECT 1 FROM dbo.tb_sections WHERE id_section = @id_section)
    BEGIN
        THROW 50023, N'Секция для материала не найдена.', 1;
    END;

    IF @created_by IS NOT NULL
       AND NOT EXISTS (SELECT 1 FROM dbo.tb_participants WHERE id_participant = @created_by)
    BEGIN
        THROW 50024, N'Пользователь, добавивший материал, не найден.', 1;
    END;

    INSERT INTO dbo.tb_materials
    (
        material_title, material_description, file_name, file_extension,
        file_content, id_report, id_section, created_by
    )
    VALUES
    (
        @material_title, @material_description, @file_name, @file_extension,
        @file_content, @id_report, @id_section, @created_by
    );
END;
GO

CREATE PROCEDURE dbo.usp_add_conference_program
    @id_report INT,
    @presentation_date DATE,
    @presentation_time TIME,
    @location NVARCHAR(200),
    @id_section INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.tb_reports WHERE id_report = @id_report)
    BEGIN
        THROW 50010, N'Доклад не найден.', 1;
    END;

    IF NOT EXISTS
    (
        SELECT 1
        FROM dbo.tb_reports
        WHERE id_report = @id_report
          AND review_status = N'Принят'
    )
    BEGIN
        THROW 50011, N'В программу можно добавить только принятый доклад.', 1;
    END;

    IF NOT EXISTS (SELECT 1 FROM dbo.tb_sections WHERE id_section = @id_section)
    BEGIN
        THROW 50012, N'Секция не найдена.', 1;
    END;

    IF EXISTS (SELECT 1 FROM dbo.tb_conference_program WHERE id_report = @id_report)
    BEGIN
        THROW 50013, N'Этот доклад уже добавлен в программу конференции.', 1;
    END;

    IF EXISTS
    (
        SELECT 1
        FROM dbo.tb_conference_program
        WHERE id_section = @id_section
          AND presentation_date = @presentation_date
          AND presentation_time = @presentation_time
    )
    BEGIN
        THROW 50014, N'В выбранной секции уже есть доклад на это время.', 1;
    END;

    INSERT INTO dbo.tb_conference_program
    (
        id_report, presentation_date, presentation_time, location, id_section
    )
    VALUES
    (
        @id_report, @presentation_date, @presentation_time, @location, @id_section
    );
END;
GO

CREATE PROCEDURE dbo.usp_update_report_status
    @id_report INT,
    @review_status NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.tb_reports WHERE id_report = @id_report)
    BEGIN
        THROW 50015, N'Доклад не найден.', 1;
    END;

    IF @review_status NOT IN (N'На рассмотрении', N'Принят', N'Отклонен')
    BEGIN
        THROW 50016, N'Недопустимый статус доклада.', 1;
    END;

    UPDATE dbo.tb_reports
    SET review_status = @review_status
    WHERE id_report = @id_report;

    IF @review_status <> N'Принят'
    BEGIN
        DELETE FROM dbo.tb_conference_program
        WHERE id_report = @id_report;
    END;
END;
GO
