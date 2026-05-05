USE ConferenceDB;
GO

IF OBJECT_ID('dbo.tb_section_visits', 'U') IS NOT NULL DROP TABLE dbo.tb_section_visits;
IF OBJECT_ID('dbo.tb_reviews', 'U') IS NOT NULL DROP TABLE dbo.tb_reviews;
IF OBJECT_ID('dbo.tb_conference_program', 'U') IS NOT NULL DROP TABLE dbo.tb_conference_program;
IF OBJECT_ID('dbo.tb_reports', 'U') IS NOT NULL DROP TABLE dbo.tb_reports;
IF OBJECT_ID('dbo.tb_sections', 'U') IS NOT NULL DROP TABLE dbo.tb_sections;
IF OBJECT_ID('dbo.tb_participants', 'U') IS NOT NULL DROP TABLE dbo.tb_participants;
GO

CREATE TABLE dbo.tb_participants
(
    id_participant INT IDENTITY(1,1) NOT NULL,
    last_name NVARCHAR(100) NOT NULL,
    first_name NVARCHAR(100) NOT NULL,
    middle_name NVARCHAR(100) NULL,
    email NVARCHAR(255) NOT NULL,
    phone NVARCHAR(30) NULL,
    participant_status NVARCHAR(50) NOT NULL,
    user_role NVARCHAR(50) NOT NULL,
    workplace NVARCHAR(255) NULL,
    academic_degree NVARCHAR(100) NULL,
    password_hash NVARCHAR(64) NOT NULL
        CONSTRAINT df_tb_participants_password_hash
        DEFAULT N'5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5',

    CONSTRAINT pk_tb_participants PRIMARY KEY (id_participant),
    CONSTRAINT uq_tb_participants_email UNIQUE (email),

    CONSTRAINT chk_tb_participants_status
        CHECK (participant_status IN (N'Докладчик', N'Слушатель')),

    CONSTRAINT chk_tb_participants_role
        CHECK (user_role IN (N'Участник', N'Рецензент', N'Организатор', N'Администратор')),

    CONSTRAINT chk_tb_participants_email
        CHECK (email LIKE N'%@%.%')
);
GO

CREATE TABLE dbo.tb_sections
(
    id_section INT IDENTITY(1,1) NOT NULL,
    section_name NVARCHAR(200) NOT NULL,
    description NVARCHAR(MAX) NULL,

    CONSTRAINT pk_tb_sections PRIMARY KEY (id_section),
    CONSTRAINT uq_tb_sections_section_name UNIQUE (section_name)
);
GO

CREATE TABLE dbo.tb_reports
(
    id_report INT IDENTITY(1,1) NOT NULL,
    topic NVARCHAR(300) NOT NULL,
    annotation NVARCHAR(MAX) NULL,
    keywords NVARCHAR(300) NULL,
    review_status NVARCHAR(30) NOT NULL DEFAULT N'На рассмотрении',

    file_name NVARCHAR(255) NULL,
    file_extension NVARCHAR(20) NULL,
    file_content VARBINARY(MAX) NULL,

    id_author INT NOT NULL,

    CONSTRAINT pk_tb_reports PRIMARY KEY (id_report),

    CONSTRAINT fk_tb_reports_id_author
        FOREIGN KEY (id_author)
        REFERENCES dbo.tb_participants(id_participant),

    CONSTRAINT chk_tb_reports_review_status
        CHECK (review_status IN (N'На рассмотрении', N'Принят', N'Отклонен')),

    CONSTRAINT chk_tb_reports_file_data
        CHECK (
            (file_name IS NULL AND file_extension IS NULL AND file_content IS NULL)
            OR
            (file_name IS NOT NULL AND file_extension IS NOT NULL AND file_content IS NOT NULL)
        )
);
GO

CREATE TABLE dbo.tb_conference_program
(
    id_presentation INT IDENTITY(1,1) NOT NULL,
    id_report INT NOT NULL,
    presentation_date DATE NOT NULL,
    presentation_time TIME NOT NULL,
    location NVARCHAR(200) NOT NULL,
    id_section INT NOT NULL,

    CONSTRAINT pk_tb_conference_program PRIMARY KEY (id_presentation),

    CONSTRAINT fk_tb_conference_program_id_report
        FOREIGN KEY (id_report)
        REFERENCES dbo.tb_reports(id_report),

    CONSTRAINT fk_tb_conference_program_id_section
        FOREIGN KEY (id_section)
        REFERENCES dbo.tb_sections(id_section),

    CONSTRAINT uq_tb_conference_program_section_time
        UNIQUE (id_section, presentation_date, presentation_time),

    CONSTRAINT uq_tb_conference_program_report
        UNIQUE (id_report)
);
GO

CREATE TABLE dbo.tb_reviews
(
    id_review INT IDENTITY(1,1) NOT NULL,
    comments NVARCHAR(MAX) NULL,
    novelty_score INT NOT NULL,
    relevance_score INT NOT NULL,
    quality_score INT NOT NULL,
    review_result NVARCHAR(30) NOT NULL,
    id_report INT NOT NULL,
    id_reviewer INT NOT NULL,

    CONSTRAINT pk_tb_reviews PRIMARY KEY (id_review),

    CONSTRAINT fk_tb_reviews_id_report
        FOREIGN KEY (id_report)
        REFERENCES dbo.tb_reports(id_report),

    CONSTRAINT fk_tb_reviews_id_reviewer
        FOREIGN KEY (id_reviewer)
        REFERENCES dbo.tb_participants(id_participant),

    CONSTRAINT chk_tb_reviews_novelty_score
        CHECK (novelty_score BETWEEN 1 AND 10),

    CONSTRAINT chk_tb_reviews_relevance_score
        CHECK (relevance_score BETWEEN 1 AND 10),

    CONSTRAINT chk_tb_reviews_quality_score
        CHECK (quality_score BETWEEN 1 AND 10),

    CONSTRAINT chk_tb_reviews_result
        CHECK (review_result IN (N'Принят', N'Отклонен', N'На доработку')),

    CONSTRAINT uq_tb_reviews_report_reviewer
        UNIQUE (id_report, id_reviewer)
);
GO

CREATE TABLE dbo.tb_section_visits
(
    id_visit INT IDENTITY(1,1) NOT NULL,
    id_participant INT NOT NULL,
    id_section INT NOT NULL,

    CONSTRAINT pk_tb_section_visits PRIMARY KEY (id_visit),

    CONSTRAINT fk_tb_section_visits_id_participant
        FOREIGN KEY (id_participant)
        REFERENCES dbo.tb_participants(id_participant),

    CONSTRAINT fk_tb_section_visits_id_section
        FOREIGN KEY (id_section)
        REFERENCES dbo.tb_sections(id_section),

    CONSTRAINT uq_tb_section_visits_participant_section
        UNIQUE (id_participant, id_section)
);
GO