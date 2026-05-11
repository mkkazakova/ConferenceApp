USE ConferenceDB;
GO

IF OBJECT_ID('dbo.trg_reports_author_must_be_speaker', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_reports_author_must_be_speaker;
IF OBJECT_ID('dbo.trg_reports_remove_from_program_if_not_accepted', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_reports_remove_from_program_if_not_accepted;
IF OBJECT_ID('dbo.trg_reviews_reviewer_check', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_reviews_reviewer_check;
IF OBJECT_ID('dbo.trg_reviews_update_report_status', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_reviews_update_report_status;
IF OBJECT_ID('dbo.trg_program_only_accepted_reports', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_program_only_accepted_reports;
IF OBJECT_ID('dbo.trg_section_visits_limit', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_section_visits_limit;
IF OBJECT_ID('dbo.trg_sections_max_participants_check', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_sections_max_participants_check;
GO

CREATE TRIGGER dbo.trg_reports_author_must_be_speaker
ON dbo.tb_reports
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM inserted AS i
        INNER JOIN dbo.tb_participants AS p
            ON i.id_author = p.id_participant
        WHERE p.participant_status <> N'Докладчик'
    )
    BEGIN
        ROLLBACK TRANSACTION;
        THROW 51001, N'Автором доклада может быть только участник со статусом "Докладчик".', 1;
    END;
END;
GO

CREATE TRIGGER dbo.trg_reports_remove_from_program_if_not_accepted
ON dbo.tb_reports
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DELETE cp
    FROM dbo.tb_conference_program AS cp
    INNER JOIN inserted AS i
        ON cp.id_report = i.id_report
    WHERE i.review_status <> N'Принят';
END;
GO

CREATE TRIGGER dbo.trg_reviews_reviewer_check
ON dbo.tb_reviews
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM inserted AS i
        INNER JOIN dbo.tb_participants AS p
            ON i.id_reviewer = p.id_participant
        WHERE p.user_role <> N'Рецензент'
    )
    BEGIN
        ROLLBACK TRANSACTION;
        THROW 51002, N'Рецензию может добавлять только пользователь с ролью "Рецензент".', 1;
    END;

    IF EXISTS
    (
        SELECT 1
        FROM inserted AS i
        INNER JOIN dbo.tb_reports AS r
            ON i.id_report = r.id_report
        WHERE i.id_reviewer = r.id_author
    )
    BEGIN
        ROLLBACK TRANSACTION;
        THROW 51003, N'Автор не может рецензировать собственный доклад.', 1;
    END;
END;
GO

CREATE TRIGGER dbo.trg_reviews_update_report_status
ON dbo.tb_reviews
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE r
    SET review_status =
        CASE
            WHEN EXISTS
            (
                SELECT 1
                FROM dbo.tb_reviews AS rv
                WHERE rv.id_report = r.id_report
                  AND rv.review_result = N'Отклонен'
            )
            THEN N'Отклонен'
            WHEN EXISTS
            (
                SELECT 1
                FROM dbo.tb_reviews AS rv
                WHERE rv.id_report = r.id_report
                  AND rv.review_result = N'На доработку'
            )
            THEN N'На рассмотрении'
            WHEN EXISTS
            (
                SELECT 1
                FROM dbo.tb_reviews AS rv
                WHERE rv.id_report = r.id_report
                  AND rv.review_result = N'Принят'
            )
            THEN N'Принят'
            ELSE N'На рассмотрении'
        END
    FROM dbo.tb_reports AS r
    WHERE r.id_report IN
    (
        SELECT id_report FROM inserted
        UNION
        SELECT id_report FROM deleted
    );

    DELETE cp
    FROM dbo.tb_conference_program AS cp
    INNER JOIN dbo.tb_reports AS r
        ON cp.id_report = r.id_report
    WHERE r.review_status <> N'Принят'
      AND r.id_report IN
      (
          SELECT id_report FROM inserted
          UNION
          SELECT id_report FROM deleted
      );
END;
GO

CREATE TRIGGER dbo.trg_program_only_accepted_reports
ON dbo.tb_conference_program
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM inserted AS i
        INNER JOIN dbo.tb_reports AS r
            ON i.id_report = r.id_report
        WHERE r.review_status <> N'Принят'
    )
    BEGIN
        ROLLBACK TRANSACTION;
        THROW 51004, N'В программу конференции можно добавить только принятый доклад.', 1;
    END;
END;
GO

CREATE TRIGGER dbo.trg_section_visits_limit
ON dbo.tb_section_visits
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM dbo.tb_sections AS s
        INNER JOIN
        (
            SELECT id_section, COUNT(*) AS visitors_count
            FROM dbo.tb_section_visits
            GROUP BY id_section
        ) AS vc
            ON s.id_section = vc.id_section
        WHERE vc.visitors_count > s.max_participants
    )
    BEGIN
        ROLLBACK TRANSACTION;
        THROW 51005, N'Количество участников секции превышает установленный лимит.', 1;
    END;
END;
GO

CREATE TRIGGER dbo.trg_sections_max_participants_check
ON dbo.tb_sections
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE(max_participants)
       AND EXISTS
       (
           SELECT 1
           FROM inserted AS i
           WHERE
           (
               SELECT COUNT(*)
               FROM dbo.tb_section_visits AS sv
               WHERE sv.id_section = i.id_section
           ) > i.max_participants
       )
    BEGIN
        ROLLBACK TRANSACTION;
        THROW 51006, N'Нельзя установить лимит меньше текущего количества записанных участников.', 1;
    END;
END;
GO
