using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ConferenceApp
{
    public partial class ReviewsForm : Form
    {
        private int currentReviewerId;
        private string currentUserRole;

        private bool isOrganizerMode;
        private bool isAdminMode;

        private int selectedReportId = 0;
        private int selectedReviewId = 0;

        public ReviewsForm(int reviewerId) : this(reviewerId, "Рецензент")
        {
        }

        public ReviewsForm(int reviewerId, string userRole)
        {
            InitializeComponent();

            currentReviewerId = reviewerId;
            currentUserRole = userRole;

            isOrganizerMode = currentUserRole == "Организатор";
            isAdminMode = currentUserRole == "Администратор";

            SetupGridStyle();
            LoadComboData();
            ConfigureAccessByRole();
            CenterTitle();
            LoadReports();

            dgvReports.MouseDown += dgvReports_MouseDown;
        }

        private void ConfigureAccessByRole()
        {
            if (isAdminMode)
            {
                lblTitle.Text = "Редактирование рецензий";
                Text = "Редактирование рецензий";

                btnSave.Visible = true;
                btnSave.Text = "Сохранить изменения";
                btnClear.Text = "Отменить рецензирование";

                txtTopic.ReadOnly = false;
                txtAnnotation.ReadOnly = false;
                txtKeywords.ReadOnly = false;
                txtFilePath.ReadOnly = false;
                txtComments.ReadOnly = false;

                txtAuthor.Visible = false;
                cmbAuthor.Visible = true;

                txtReportStatus.Visible = false;
                cmbReportStatus.Visible = true;

                lblReviewer.Visible = true;
                txtReviewer.Visible = false;
                cmbReviewer.Visible = true;

                numNovelty.Enabled = true;
                numRelevance.Enabled = true;
                numQuality.Enabled = true;
                cmbResult.Enabled = true;
            }
            else if (isOrganizerMode)
            {
                lblTitle.Text = "Рецензии докладов";
                Text = "Рецензии докладов";

                btnSave.Visible = false;
                btnClear.Text = "Отменить рецензирование";

                txtTopic.ReadOnly = true;
                txtAnnotation.ReadOnly = true;
                txtKeywords.ReadOnly = true;
                txtFilePath.ReadOnly = true;
                txtComments.ReadOnly = true;

                txtAuthor.Visible = true;
                cmbAuthor.Visible = false;

                txtReportStatus.Visible = true;
                cmbReportStatus.Visible = false;

                lblReviewer.Visible = true;
                txtReviewer.Visible = true;
                cmbReviewer.Visible = false;

                numNovelty.Enabled = false;
                numRelevance.Enabled = false;
                numQuality.Enabled = false;
                cmbResult.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Рецензирование докладов";
                Text = "Рецензирование докладов";

                btnSave.Visible = true;
                btnSave.Text = "Сохранить рецензию";
                btnClear.Text = "Очистить";

                txtTopic.ReadOnly = true;
                txtAnnotation.ReadOnly = true;
                txtKeywords.ReadOnly = true;
                txtFilePath.ReadOnly = true;
                txtComments.ReadOnly = false;

                txtAuthor.Visible = true;
                cmbAuthor.Visible = false;

                txtReportStatus.Visible = true;
                cmbReportStatus.Visible = false;

                lblReviewer.Visible = false;
                txtReviewer.Visible = false;
                cmbReviewer.Visible = false;

                numNovelty.Enabled = true;
                numRelevance.Enabled = true;
                numQuality.Enabled = true;
                cmbResult.Enabled = true;
            }
        }

        private void LoadComboData()
        {
            cmbReportStatus.Items.Clear();
            cmbReportStatus.Items.AddRange(new object[]
            {
                "На рассмотрении",
                "Принят",
                "Отклонен"
            });

            string authorsQuery = @"
                SELECT
                    id_participant,
                    LTRIM(RTRIM(last_name + N' ' + first_name + N' ' + ISNULL(middle_name, N''))) AS full_name
                FROM dbo.tb_participants
                WHERE participant_status = N'Докладчик'
                ORDER BY last_name, first_name;
            ";

            DataTable authors = Database.ExecuteSelect(authorsQuery, new SqlParameter[0]);

            cmbAuthor.DataSource = authors;
            cmbAuthor.DisplayMember = "full_name";
            cmbAuthor.ValueMember = "id_participant";

            string reviewersQuery = @"
                SELECT
                    id_participant,
                    LTRIM(RTRIM(last_name + N' ' + first_name + N' ' + ISNULL(middle_name, N''))) AS full_name
                FROM dbo.tb_participants
                WHERE user_role = N'Рецензент'
                ORDER BY last_name, first_name;
            ";

            DataTable reviewersFromDb = Database.ExecuteSelect(reviewersQuery, new SqlParameter[0]);

            DataTable reviewers = new DataTable();
            reviewers.Columns.Add("id_participant", typeof(int));
            reviewers.Columns.Add("full_name", typeof(string));

            reviewers.Rows.Add(0, "Не назначен");

            foreach (DataRow row in reviewersFromDb.Rows)
            {
                reviewers.Rows.Add(row["id_participant"], row["full_name"]);
            }

            cmbReviewer.DataSource = reviewers;
            cmbReviewer.DisplayMember = "full_name";
            cmbReviewer.ValueMember = "id_participant";
        }

        private void LoadReports()
        {
            try
            {
                string query;
                SqlParameter[] parameters;

                if (isOrganizerMode || isAdminMode)
                {
                    query = @"
                        SELECT
                            r.id_report AS [ID],
                            r.id_author AS [AuthorID],
                            rv.id_review AS [ReviewID],
                            rv.id_reviewer AS [ReviewerID],
                            r.topic AS [Тема],
                            a.last_name + N' ' + a.first_name + N' ' + ISNULL(a.middle_name, N'') AS [Автор],
                            r.review_status AS [Статус доклада],
                            ISNULL(rev.last_name + N' ' + rev.first_name + N' ' + ISNULL(rev.middle_name, N''), N'Не назначен') AS [Рецензент],
                            ISNULL(CONVERT(NVARCHAR(10), rv.novelty_score), N'') AS [Новизна],
                            ISNULL(CONVERT(NVARCHAR(10), rv.relevance_score), N'') AS [Актуальность],
                            ISNULL(CONVERT(NVARCHAR(10), rv.quality_score), N'') AS [Качество],
                            ISNULL(rv.review_result, N'Не рецензирован') AS [Результат],
                            r.annotation AS [Аннотация],
                            r.keywords AS [Ключевые слова],
                            r.file_path AS [Файл],
                            rv.comments AS [Комментарий]
                        FROM dbo.tb_reports AS r
                        INNER JOIN dbo.tb_participants AS a
                            ON r.id_author = a.id_participant
                        LEFT JOIN dbo.tb_reviews AS rv
                            ON r.id_report = rv.id_report
                        LEFT JOIN dbo.tb_participants AS rev
                            ON rv.id_reviewer = rev.id_participant
                        ORDER BY
                            CASE WHEN rv.id_review IS NULL THEN 0 ELSE 1 END,
                            r.id_report DESC;
                    ";

                    parameters = new SqlParameter[0];
                }
                else
                {
                    query = @"
                        SELECT
                            r.id_report AS [ID],
                            r.id_author AS [AuthorID],
                            rv.id_review AS [ReviewID],
                            rv.id_reviewer AS [ReviewerID],
                            r.topic AS [Тема],
                            a.last_name + N' ' + a.first_name + N' ' + ISNULL(a.middle_name, N'') AS [Автор],
                            r.review_status AS [Статус доклада],
                            N'' AS [Рецензент],
                            ISNULL(CONVERT(NVARCHAR(10), rv.novelty_score), N'') AS [Новизна],
                            ISNULL(CONVERT(NVARCHAR(10), rv.relevance_score), N'') AS [Актуальность],
                            ISNULL(CONVERT(NVARCHAR(10), rv.quality_score), N'') AS [Качество],
                            ISNULL(rv.review_result, N'Не рецензирован') AS [Результат],
                            r.annotation AS [Аннотация],
                            r.keywords AS [Ключевые слова],
                            r.file_path AS [Файл],
                            rv.comments AS [Комментарий]
                        FROM dbo.tb_reports AS r
                        INNER JOIN dbo.tb_participants AS a
                            ON r.id_author = a.id_participant
                        LEFT JOIN dbo.tb_reviews AS rv
                            ON r.id_report = rv.id_report
                           AND rv.id_reviewer = @ReviewerId
                        WHERE r.id_author <> @ReviewerId
                        ORDER BY
                            CASE WHEN rv.id_review IS NULL THEN 0 ELSE 1 END,
                            r.id_report DESC;
                    ";

                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@ReviewerId", currentReviewerId)
                    };
                }

                DataTable table = Database.ExecuteSelect(query, parameters);

                dgvReports.DataSource = null;
                dgvReports.AutoGenerateColumns = true;
                dgvReports.DataSource = table;

                HideColumn("ID");
                HideColumn("AuthorID");
                HideColumn("ReviewID");
                HideColumn("ReviewerID");
                HideColumn("Аннотация");
                HideColumn("Ключевые слова");
                HideColumn("Файл");
                HideColumn("Комментарий");

                foreach (DataGridViewColumn column in dgvReports.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки докладов: " + ex.Message);
            }
        }

        private void HideColumn(string columnName)
        {
            if (dgvReports.Columns.Contains(columnName))
            {
                dgvReports.Columns[columnName].Visible = false;
            }
        }

        private void SetupGridStyle()
        {
            dgvReports.BackgroundColor = Color.White;
            dgvReports.BorderStyle = BorderStyle.FixedSingle;
            dgvReports.RowHeadersVisible = false;
            dgvReports.AllowUserToAddRows = false;
            dgvReports.AllowUserToDeleteRows = false;
            dgvReports.AllowUserToResizeRows = false;
            dgvReports.ReadOnly = true;
            dgvReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReports.MultiSelect = false;
            dgvReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReports.GridColor = Color.LightGray;
            dgvReports.EnableHeadersVisualStyles = false;

            dgvReports.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 240, 250);
            dgvReports.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvReports.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvReports.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvReports.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);

            dgvReports.DefaultCellStyle.BackColor = Color.White;
            dgvReports.DefaultCellStyle.ForeColor = Color.Black;
            dgvReports.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 230, 250);
            dgvReports.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvReports.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F);

            dgvReports.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 252, 255);
        }

        private void CenterTitle()
        {
            lblTitle.Left = (ClientSize.Width - lblTitle.Width) / 2;
        }

        private void dgvReports_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvReports.Rows[e.RowIndex];

            selectedReportId = Convert.ToInt32(row.Cells["ID"].Value);
            selectedReviewId = GetIntCellValue(row, "ReviewID");

            int authorId = GetIntCellValue(row, "AuthorID");
            int reviewerId = GetIntCellValue(row, "ReviewerID");

            txtTopic.Text = GetCellValue(row, "Тема");
            txtAuthor.Text = GetCellValue(row, "Автор");
            txtReportStatus.Text = GetCellValue(row, "Статус доклада");
            txtReviewer.Text = GetCellValue(row, "Рецензент");
            txtAnnotation.Text = GetCellValue(row, "Аннотация");
            txtKeywords.Text = GetCellValue(row, "Ключевые слова");
            txtFilePath.Text = GetCellValue(row, "Файл");
            txtComments.Text = GetCellValue(row, "Комментарий");

            SelectComboValue(cmbAuthor, authorId);
            SelectComboValue(cmbReviewer, reviewerId);

            string reportStatus = GetCellValue(row, "Статус доклада");

            if (cmbReportStatus.Items.Contains(reportStatus))
            {
                cmbReportStatus.SelectedItem = reportStatus;
            }
            else
            {
                cmbReportStatus.SelectedIndex = 0;
            }

            SetNumericValue(numNovelty, GetCellValue(row, "Новизна"));
            SetNumericValue(numRelevance, GetCellValue(row, "Актуальность"));
            SetNumericValue(numQuality, GetCellValue(row, "Качество"));

            string result = GetCellValue(row, "Результат");

            if (result == "Принят" || result == "Отклонен" || result == "На доработку")
            {
                cmbResult.SelectedItem = result;
            }
            else
            {
                cmbResult.SelectedIndex = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isOrganizerMode)
            {
                MessageBox.Show("Организатор не может редактировать рецензирование.");
                return;
            }

            if (selectedReportId == 0)
            {
                MessageBox.Show("Выберите доклад.");
                return;
            }

            if (isAdminMode)
            {
                SaveAdminChanges();
            }
            else
            {
                SaveReviewerChanges();
            }
        }

        private void SaveReviewerChanges()
        {
            if (cmbResult.SelectedItem == null)
            {
                MessageBox.Show("Выберите результат рецензирования.");
                return;
            }

            try
            {
                if (selectedReviewId == 0)
                {
                    string query = @"
                        EXEC dbo.usp_add_review
                            @id_report = @ReportId,
                            @id_reviewer = @ReviewerId,
                            @novelty_score = @NoveltyScore,
                            @relevance_score = @RelevanceScore,
                            @quality_score = @QualityScore,
                            @review_result = @ReviewResult,
                            @comments = @Comments;
                    ";

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@ReportId", selectedReportId),
                        new SqlParameter("@ReviewerId", currentReviewerId),
                        new SqlParameter("@NoveltyScore", Convert.ToInt32(numNovelty.Value)),
                        new SqlParameter("@RelevanceScore", Convert.ToInt32(numRelevance.Value)),
                        new SqlParameter("@QualityScore", Convert.ToInt32(numQuality.Value)),
                        new SqlParameter("@ReviewResult", cmbResult.SelectedItem.ToString()),
                        new SqlParameter("@Comments", GetNullableText(txtComments.Text))
                    };

                    Database.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Рецензия добавлена.");
                }
                else
                {
                    string query = @"
                        UPDATE dbo.tb_reviews
                        SET
                            novelty_score = @NoveltyScore,
                            relevance_score = @RelevanceScore,
                            quality_score = @QualityScore,
                            review_result = @ReviewResult,
                            comments = @Comments
                        WHERE id_review = @ReviewId
                          AND id_reviewer = @ReviewerId;
                    ";

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@NoveltyScore", Convert.ToInt32(numNovelty.Value)),
                        new SqlParameter("@RelevanceScore", Convert.ToInt32(numRelevance.Value)),
                        new SqlParameter("@QualityScore", Convert.ToInt32(numQuality.Value)),
                        new SqlParameter("@ReviewResult", cmbResult.SelectedItem.ToString()),
                        new SqlParameter("@Comments", GetNullableText(txtComments.Text)),
                        new SqlParameter("@ReviewId", selectedReviewId),
                        new SqlParameter("@ReviewerId", currentReviewerId)
                    };

                    Database.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Рецензия изменена.");
                }

                LoadReports();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения рецензии: " + ex.Message);
            }
        }

        private void SaveAdminChanges()
        {
            if (string.IsNullOrWhiteSpace(txtTopic.Text))
            {
                MessageBox.Show("Введите тему доклада.");
                return;
            }

            int authorId = GetSelectedComboId(cmbAuthor);
            int reviewerId = GetSelectedComboId(cmbReviewer);

            if (authorId == 0)
            {
                MessageBox.Show("Выберите автора доклада.");
                return;
            }

            if (selectedReviewId > 0 && reviewerId == 0)
            {
                MessageBox.Show("Для существующей рецензии должен быть выбран рецензент.");
                return;
            }

            if (cmbReportStatus.SelectedItem == null)
            {
                MessageBox.Show("Выберите статус доклада.");
                return;
            }

            if (cmbResult.SelectedItem == null)
            {
                MessageBox.Show("Выберите результат рецензирования.");
                return;
            }

            try
            {
                string query = @"
                    BEGIN TRY
                        BEGIN TRANSACTION;

                        UPDATE dbo.tb_reports
                        SET
                            topic = @Topic,
                            annotation = @Annotation,
                            keywords = @Keywords,
                            review_status = @ReportStatus,
                            file_path = @FilePath,
                            id_author = @AuthorId
                        WHERE id_report = @ReportId;

                        IF @ReviewId > 0
                        BEGIN
                            UPDATE dbo.tb_reviews
                            SET
                                id_reviewer = @ReviewerId,
                                novelty_score = @NoveltyScore,
                                relevance_score = @RelevanceScore,
                                quality_score = @QualityScore,
                                review_result = @ReviewResult,
                                comments = @Comments
                            WHERE id_review = @ReviewId;
                        END
                        ELSE IF @ReviewerId > 0
                        BEGIN
                            INSERT INTO dbo.tb_reviews
                            (
                                comments,
                                novelty_score,
                                relevance_score,
                                quality_score,
                                review_result,
                                id_report,
                                id_reviewer
                            )
                            VALUES
                            (
                                @Comments,
                                @NoveltyScore,
                                @RelevanceScore,
                                @QualityScore,
                                @ReviewResult,
                                @ReportId,
                                @ReviewerId
                            );
                        END

                        UPDATE dbo.tb_reports
                        SET review_status = @ReportStatus
                        WHERE id_report = @ReportId;

                        COMMIT TRANSACTION;
                    END TRY
                    BEGIN CATCH
                        IF @@TRANCOUNT > 0
                            ROLLBACK TRANSACTION;

                        THROW;
                    END CATCH;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ReportId", selectedReportId),
                    new SqlParameter("@ReviewId", selectedReviewId),
                    new SqlParameter("@AuthorId", authorId),
                    new SqlParameter("@ReviewerId", reviewerId),
                    new SqlParameter("@Topic", txtTopic.Text.Trim()),
                    new SqlParameter("@Annotation", GetNullableText(txtAnnotation.Text)),
                    new SqlParameter("@Keywords", GetNullableText(txtKeywords.Text)),
                    new SqlParameter("@ReportStatus", cmbReportStatus.SelectedItem.ToString()),
                    new SqlParameter("@FilePath", GetNullableText(txtFilePath.Text)),
                    new SqlParameter("@NoveltyScore", Convert.ToInt32(numNovelty.Value)),
                    new SqlParameter("@RelevanceScore", Convert.ToInt32(numRelevance.Value)),
                    new SqlParameter("@QualityScore", Convert.ToInt32(numQuality.Value)),
                    new SqlParameter("@ReviewResult", cmbResult.SelectedItem.ToString()),
                    new SqlParameter("@Comments", GetNullableText(txtComments.Text))
                };

                Database.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Изменения сохранены.");
                LoadReports();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения изменений: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (isOrganizerMode || isAdminMode)
            {
                CancelReview();
            }
            else
            {
                ClearFields();
            }
        }

        private void CancelReview()
        {
            if (selectedReviewId == 0)
            {
                MessageBox.Show("Выберите рецензию для отмены.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Отменить выбранное рецензирование?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                string query = @"
                    DELETE FROM dbo.tb_reviews
                    WHERE id_review = @ReviewId;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ReviewId", selectedReviewId)
                };

                Database.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Рецензирование отменено.");
                LoadReports();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка отмены рецензирования: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvReports_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dgvReports.HitTest(e.X, e.Y);

            if (hit.Type == DataGridViewHitTestType.None)
            {
                ClearFields();
            }
        }

        private void SetNumericValue(NumericUpDown numeric, string value)
        {
            int number;

            if (int.TryParse(value, out number) && number >= 1 && number <= 10)
            {
                numeric.Value = number;
            }
            else
            {
                numeric.Value = 1;
            }
        }

        private object GetNullableText(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return DBNull.Value;
            }

            return value.Trim();
        }

        private string GetCellValue(DataGridViewRow row, string columnName)
        {
            if (!dgvReports.Columns.Contains(columnName))
            {
                return "";
            }

            object value = row.Cells[columnName].Value;

            if (value == null || value == DBNull.Value)
            {
                return "";
            }

            return value.ToString();
        }

        private int GetIntCellValue(DataGridViewRow row, string columnName)
        {
            if (!dgvReports.Columns.Contains(columnName))
            {
                return 0;
            }

            object value = row.Cells[columnName].Value;

            if (value == null || value == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToInt32(value);
        }

        private int GetSelectedComboId(ComboBox comboBox)
        {
            if (comboBox.SelectedValue == null || comboBox.SelectedValue == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToInt32(comboBox.SelectedValue);
        }

        private void SelectComboValue(ComboBox comboBox, int id)
        {
            if (comboBox.DataSource == null)
            {
                return;
            }

            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                DataRowView row = comboBox.Items[i] as DataRowView;

                if (row == null)
                {
                    continue;
                }

                if (Convert.ToInt32(row[comboBox.ValueMember]) == id)
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }

            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        private void ClearFields()
        {
            selectedReportId = 0;
            selectedReviewId = 0;

            txtTopic.Clear();
            txtAuthor.Clear();
            txtReviewer.Clear();
            txtReportStatus.Clear();
            txtAnnotation.Clear();
            txtKeywords.Clear();
            txtFilePath.Clear();
            txtComments.Clear();

            numNovelty.Value = 1;
            numRelevance.Value = 1;
            numQuality.Value = 1;

            if (cmbResult.Items.Count > 0)
            {
                cmbResult.SelectedIndex = 0;
            }

            if (cmbReportStatus.Items.Count > 0)
            {
                cmbReportStatus.SelectedIndex = 0;
            }

            SelectComboValue(cmbAuthor, 0);
            SelectComboValue(cmbReviewer, 0);

            if (dgvReports.Rows.Count > 0)
            {
                dgvReports.ClearSelection();
            }
        }
    }
}