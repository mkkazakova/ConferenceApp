USE ConferenceDB;
GO

/* =========================================================
   1. Запросы на ввод данных
   ========================================================= */

/* 1.1. Добавление нового участника */
IF NOT EXISTS (
    SELECT 1 FROM dbo.tb_participants WHERE email = N'sokolov@mail.ru'
)
BEGIN
    INSERT INTO dbo.tb_participants
    (
        last_name, first_name, middle_name, email, phone,
        participant_status, user_role, workplace, academic_degree
    )
    VALUES
    (
        N'Соколов', N'Илья', N'Андреевич', N'sokolov@mail.ru', N'+79004040404',
        N'Слушатель', N'Участник', N'МГТУ им. Н.Э. Баумана', NULL
    );
END;
GO


/* 1.2. Добавление новой секции */
IF NOT EXISTS (
    SELECT 1 FROM dbo.tb_sections WHERE section_name = N'Киберфизические системы'
)
BEGIN
    INSERT INTO dbo.tb_sections
    (
        section_name, description
    )
    VALUES
    (
        N'Киберфизические системы',
        N'Секция, посвященная безопасности и проектированию киберфизических систем.'
    );
END;
GO


/* 1.3. Добавление нового доклада с файлом в БД */
IF NOT EXISTS (
    SELECT 1 FROM dbo.tb_reports WHERE topic = N'Безопасность данных в киберфизических системах'
)
BEGIN
    INSERT INTO dbo.tb_reports
    (
        topic,
        annotation,
        keywords,
        review_status,
        file_name,
        file_extension,
        file_content,
        id_author
    )
    VALUES
    (
        N'Безопасность данных в киберфизических системах',
        N'Доклад посвящен вопросам защиты данных при обработке информации в киберфизических системах.',
        N'безопасность, данные, киберфизические системы',
        N'На рассмотрении',
        N'report_new.pdf',
        N'.pdf',
        CONVERT(VARBINARY(MAX), N'Содержимое файла report_new.pdf'),
        1
    );
END;
GO


/* =========================================================
   2. Простые выборки данных
   ========================================================= */

/* 2.1. Список участников по алфавиту */
SELECT
    last_name,
    first_name,
    middle_name,
    email,
    participant_status,
    user_role
FROM dbo.tb_participants
ORDER BY last_name, first_name;
GO


/* 2.2. Список докладов, их статусов и файлов */
SELECT
    topic,
    review_status,
    file_name,
    file_extension,
    DATALENGTH(file_content) AS file_size_bytes
FROM dbo.tb_reports
ORDER BY review_status, topic;
GO


/* 2.3. Программа конференции */
SELECT
    cp.presentation_date,
    cp.presentation_time,
    s.section_name,
    r.topic,
    cp.location
FROM dbo.tb_conference_program AS cp
JOIN dbo.tb_reports AS r
    ON cp.id_report = r.id_report
JOIN dbo.tb_sections AS s
    ON cp.id_section = s.id_section
ORDER BY cp.presentation_date, cp.presentation_time;
GO


/* =========================================================
   3. Вывод результатов без дубликатов
   ========================================================= */

/* 3.1. Уникальные роли пользователей */
SELECT DISTINCT
    user_role
FROM dbo.tb_participants;
GO


/* 3.2. Уникальные места работы участников */
SELECT DISTINCT
    workplace
FROM dbo.tb_participants
WHERE workplace IS NOT NULL;
GO


/* =========================================================
   4. Запросы с константами и выражениями
   ========================================================= */

/* 4.1. Вывод ФИО участника одной строкой */
SELECT
    last_name + N' ' + first_name + N' ' + ISNULL(middle_name, N'') AS full_name,
    email
FROM dbo.tb_participants;
GO


/* 4.2. Расчет средней оценки рецензии */
SELECT
    id_review,
    id_report,
    (novelty_score + relevance_score + quality_score) / 3.0 AS average_score,
    review_result
FROM dbo.tb_reviews;
GO


/* 4.3. Добавление текстовой константы к результату */
SELECT
    topic,
    review_status,
    N'Научная конференция' AS event_name
FROM dbo.tb_reports;
GO


/* 4.4. Проверка статуса доклада через CASE */
SELECT
    topic,
    review_status,
    CASE
        WHEN review_status = N'Принят' THEN N'Можно включать в программу'
        WHEN review_status = N'Отклонен' THEN N'Нельзя включать в программу'
        ELSE N'Ожидает решения'
    END AS status_description
FROM dbo.tb_reports;
GO


/* 4.5. Проверка наличия файла у доклада */
SELECT
    topic,
    file_name,
    CASE
        WHEN file_content IS NOT NULL THEN N'Файл загружен в БД'
        ELSE N'Файл отсутствует'
    END AS file_status
FROM dbo.tb_reports;
GO


/* =========================================================
   5. Группировка и упорядочивание
   ========================================================= */

/* 5.1. Количество участников по ролям */
SELECT
    user_role,
    COUNT(*) AS participant_count
FROM dbo.tb_participants
GROUP BY user_role
ORDER BY participant_count DESC;
GO


/* 5.2. Количество докладов по статусам рецензирования */
SELECT
    review_status,
    COUNT(*) AS report_count
FROM dbo.tb_reports
GROUP BY review_status
ORDER BY report_count DESC;
GO


/* =========================================================
   6. Агрегатные, строковые и функции даты
   ========================================================= */

/* 6.1. Общее количество участников */
SELECT
    COUNT(*) AS total_participants
FROM dbo.tb_participants;
GO


/* 6.2. Средняя оценка актуальности докладов */
SELECT
    AVG(CAST(relevance_score AS FLOAT)) AS average_relevance_score
FROM dbo.tb_reviews;
GO


/* 6.3. Максимальная и минимальная оценка качества */
SELECT
    MAX(quality_score) AS max_quality_score,
    MIN(quality_score) AS min_quality_score
FROM dbo.tb_reviews;
GO


/* 6.4. Преобразование ФИО участников к верхнему регистру */
SELECT
    UPPER(last_name) AS last_name_upper,
    UPPER(first_name) AS first_name_upper,
    email
FROM dbo.tb_participants;
GO


/* 6.5. Определение дня проведения доклада */
SELECT
    r.topic,
    cp.presentation_date,
    DATENAME(WEEKDAY, cp.presentation_date) AS presentation_weekday
FROM dbo.tb_conference_program AS cp
JOIN dbo.tb_reports AS r
    ON cp.id_report = r.id_report;
GO


/* 6.6. Размеры файлов докладов */
SELECT
    topic,
    file_name,
    DATALENGTH(file_content) AS file_size_bytes
FROM dbo.tb_reports
WHERE file_content IS NOT NULL
ORDER BY file_size_bytes DESC;
GO