using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace ConferenceApp
{
    public partial class ParticipantsForm : Form
    {
        public ParticipantsForm()
        {
            InitializeComponent();

            cmbRoleFilter.SelectedIndex = 0;
            cmbStatusFilter.SelectedIndex = 0;

            LoadParticipants();
            LoadReviewerRequests();
        }

        private void LoadParticipants()
        {
            string search = txtSearch.Text.Trim();
            string roleFilter = cmbRoleFilter.Text;
            string statusFilter = cmbStatusFilter.Text;

            string query = @"
                SELECT
                    p.id_participant,
                    LTRIM(RTRIM(
                        p.last_name + N' ' + p.first_name + N' ' + ISNULL(p.middle_name, N'')
                    )) AS [ФИО],
                    p.email AS [Email],
                    p.phone AS [Телефон],
                    p.participant_status AS [Статус участия],
                    p.user_role AS [Роль],
                    p.workplace AS [Место работы],
                    p.academic_degree AS [Учёная степень],
                    COUNT(rv.id_review) AS [Проверено докладов]
                FROM dbo.tb_participants AS p
                LEFT JOIN dbo.tb_reviews AS rv
                    ON p.id_participant = rv.id_reviewer
                WHERE p.user_role IN (N'Участник', N'Рецензент')
            ";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (search != "")
            {
                query += @"
                    AND (
                        p.last_name LIKE @Search
                        OR p.first_name LIKE @Search
                        OR p.middle_name LIKE @Search
                        OR p.email LIKE @Search
                    )
                ";

                parameters.Add(new SqlParameter("@Search", "%" + search + "%"));
            }

            if (roleFilter != "Все")
            {
                query += " AND p.user_role = @Role ";
                parameters.Add(new SqlParameter("@Role", roleFilter));
            }

            if (statusFilter != "Все")
            {
                query += " AND p.participant_status = @Status ";
                parameters.Add(new SqlParameter("@Status", statusFilter));
            }

            query += @"
                GROUP BY
                    p.id_participant,
                    p.last_name,
                    p.first_name,
                    p.middle_name,
                    p.email,
                    p.phone,
                    p.participant_status,
                    p.user_role,
                    p.workplace,
                    p.academic_degree
                ORDER BY
                    p.user_role,
                    p.last_name,
                    p.first_name;
            ";

            DataTable table = Database.ExecuteSelect(query, parameters.ToArray());

            dgvParticipants.DataSource = table;

            if (dgvParticipants.Columns.Contains("id_participant"))
            {
                dgvParticipants.Columns["id_participant"].Visible = false;
            }

            dgvParticipants.ClearSelection();
            ClearDetails();
        }

        private void LoadReviewerRequests()
        {
            List<ReviewerRequest> requests = ReviewerRequestStorage.Load();

            DataTable table = new DataTable();

            table.Columns.Add("ФИО");
            table.Columns.Add("Email");
            table.Columns.Add("Телефон");
            table.Columns.Add("Место работы");
            table.Columns.Add("Учёная степень");
            table.Columns.Add("Статус");

            foreach (ReviewerRequest request in requests)
            {
                if (request.Status != "На рассмотрении")
                {
                    continue;
                }

                table.Rows.Add(
                    (request.LastName + " " + request.FirstName + " " + request.MiddleName).Trim(),
                    request.Email,
                    request.Phone,
                    request.Workplace,
                    request.AcademicDegree,
                    request.Status
                );
            }

            dgvReviewerRequests.DataSource = table;
            dgvReviewerRequests.ClearSelection();
        }

        private void LoadReviewedReports(int reviewerId)
        {
            string query = @"
                SELECT
                    r.topic AS [Тема доклада],
                    rv.novelty_score AS [Новизна],
                    rv.relevance_score AS [Актуальность],
                    rv.quality_score AS [Качество],
                    CAST((rv.novelty_score + rv.relevance_score + rv.quality_score) / 3.0 AS DECIMAL(5,2)) AS [Средняя оценка],
                    rv.review_result AS [Решение],
                    rv.comments AS [Комментарий]
                FROM dbo.tb_reviews AS rv
                JOIN dbo.tb_reports AS r
                    ON rv.id_report = r.id_report
                WHERE rv.id_reviewer = @ReviewerId
                ORDER BY rv.id_review DESC;
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@ReviewerId", reviewerId)
            };

            dgvUserReports.DataSource = Database.ExecuteSelect(query, parameters);
        }

        private void LoadParticipantReports(int participantId)
        {
            string query = @"
                SELECT
                    r.topic AS [Тема доклада],
                    r.annotation AS [Аннотация],
                    r.keywords AS [Ключевые слова],
                    r.review_status AS [Статус рецензирования],
                    r.file_path AS [Файл]
                FROM dbo.tb_reports AS r
                WHERE r.id_author = @ParticipantId
                ORDER BY r.id_report DESC;
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@ParticipantId", participantId)
            };

            dgvUserReports.DataSource = Database.ExecuteSelect(query, parameters);
        }

        private void ClearDetails()
        {
            lblFullNameValue.Text = "-";
            lblEmailValue.Text = "-";
            lblPhoneValue.Text = "-";
            lblParticipantStatusValue.Text = "-";
            lblRoleValue.Text = "-";
            lblWorkplaceValue.Text = "-";
            lblAcademicDegreeValue.Text = "-";

            gbUserReports.Text = "Доклады выбранного пользователя";
            dgvUserReports.DataSource = null;
        }

        private void dgvParticipants_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvParticipants.CurrentRow == null)
            {
                ClearDetails();
                return;
            }

            DataGridViewRow row = dgvParticipants.CurrentRow;

            if (!dgvParticipants.Columns.Contains("id_participant"))
            {
                ClearDetails();
                return;
            }

            if (row.Cells["id_participant"].Value == null ||
                row.Cells["id_participant"].Value == DBNull.Value)
            {
                ClearDetails();
                return;
            }

            int participantId = Convert.ToInt32(row.Cells["id_participant"].Value);
            string role = GetCellText(row, "Роль");

            lblFullNameValue.Text = GetCellText(row, "ФИО");
            lblEmailValue.Text = GetCellText(row, "Email");
            lblPhoneValue.Text = GetCellText(row, "Телефон");
            lblParticipantStatusValue.Text = GetCellText(row, "Статус участия");
            lblRoleValue.Text = role;
            lblWorkplaceValue.Text = GetCellText(row, "Место работы");
            lblAcademicDegreeValue.Text = GetCellText(row, "Учёная степень");

            if (role == "Рецензент")
            {
                gbUserReports.Text = "Проверенные доклады рецензента";
                LoadReviewedReports(participantId);
            }
            else
            {
                gbUserReports.Text = "Доклады выбранного участника";
                LoadParticipantReports(participantId);
            }
        }

        private void btnDeleteParticipant_Click(object sender, EventArgs e)
        {
            if (dgvParticipants.CurrentRow == null)
            {
                MessageBox.Show("Выберите пользователя.");
                return;
            }

            DataGridViewRow row = dgvParticipants.CurrentRow;

            if (row.Cells["id_participant"].Value == null ||
                row.Cells["id_participant"].Value == DBNull.Value)
            {
                MessageBox.Show("Выберите пользователя.");
                return;
            }

            int participantId = Convert.ToInt32(row.Cells["id_participant"].Value);
            string fullName = GetCellText(row, "ФИО");

            DialogResult result = MessageBox.Show(
                "Удалить пользователя \"" + fullName + "\"?\n\n" +
                "Также будут удалены связанные записи: доклады, рецензии, программа конференции и записи на секции.",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            string query = @"
                BEGIN TRY
                    BEGIN TRANSACTION;

                    DECLARE @Reports TABLE (id_report INT);

                    INSERT INTO @Reports (id_report)
                    SELECT id_report
                    FROM dbo.tb_reports
                    WHERE id_author = @ParticipantId;

                    DELETE cp
                    FROM dbo.tb_conference_program AS cp
                    WHERE cp.id_report IN (SELECT id_report FROM @Reports);

                    DELETE rv
                    FROM dbo.tb_reviews AS rv
                    WHERE rv.id_report IN (SELECT id_report FROM @Reports)
                       OR rv.id_reviewer = @ParticipantId;

                    DELETE sv
                    FROM dbo.tb_section_visits AS sv
                    WHERE sv.id_participant = @ParticipantId;

                    DELETE r
                    FROM dbo.tb_reports AS r
                    WHERE r.id_report IN (SELECT id_report FROM @Reports);

                    DELETE p
                    FROM dbo.tb_participants AS p
                    WHERE p.id_participant = @ParticipantId;

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
                new SqlParameter("@ParticipantId", participantId)
            };

            try
            {
                Database.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Пользователь удалён.");

                LoadParticipants();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления: " + ex.Message);
            }
        }

        private void btnApproveRequest_Click(object sender, EventArgs e)
        {
            ReviewerRequest request = GetSelectedReviewerRequest();

            if (request == null)
            {
                MessageBox.Show("Выберите заявку.");
                return;
            }

            if (request.Status != "На рассмотрении")
            {
                MessageBox.Show("Эта заявка уже обработана.");
                return;
            }

            if (EmailExistsInDatabase(request.Email))
            {
                MessageBox.Show("Пользователь с таким email уже есть в базе данных.");
                return;
            }

            string query = @"
                INSERT INTO dbo.tb_participants
                (
                    last_name,
                    first_name,
                    middle_name,
                    email,
                    phone,
                    participant_status,
                    user_role,
                    workplace,
                    academic_degree,
                    password_hash
                )
                VALUES
                (
                    @LastName,
                    @FirstName,
                    @MiddleName,
                    @Email,
                    @Phone,
                    N'Слушатель',
                    N'Рецензент',
                    @Workplace,
                    @AcademicDegree,
                    @PasswordHash
                );
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@LastName", request.LastName),
                new SqlParameter("@FirstName", request.FirstName),
                new SqlParameter("@MiddleName", string.IsNullOrWhiteSpace(request.MiddleName) ? (object)DBNull.Value : request.MiddleName),
                new SqlParameter("@Email", request.Email),
                new SqlParameter("@Phone", string.IsNullOrWhiteSpace(request.Phone) ? (object)DBNull.Value : request.Phone),
                new SqlParameter("@Workplace", string.IsNullOrWhiteSpace(request.Workplace) ? (object)DBNull.Value : request.Workplace),
                new SqlParameter("@AcademicDegree", string.IsNullOrWhiteSpace(request.AcademicDegree) ? (object)DBNull.Value : request.AcademicDegree),
                new SqlParameter("@PasswordHash", request.PasswordHash)
            };

            int insertResult = Database.ExecuteNonQuery(query, parameters);

            if (insertResult > 0)
            {
                UpdateReviewerRequestStatus(request.Email, "Одобрена");

                MessageBox.Show("Заявка одобрена. Рецензент добавлен в базу данных.");

                LoadParticipants();
                LoadReviewerRequests();
            }
        }

        private void btnRejectRequest_Click(object sender, EventArgs e)
        {
            ReviewerRequest request = GetSelectedReviewerRequest();

            if (request == null)
            {
                MessageBox.Show("Выберите заявку.");
                return;
            }

            if (request.Status != "На рассмотрении")
            {
                MessageBox.Show("Эта заявка уже обработана.");
                return;
            }

            UpdateReviewerRequestStatus(request.Email, "Отклонена");

            MessageBox.Show("Заявка отклонена.");

            LoadReviewerRequests();
        }

        private ReviewerRequest GetSelectedReviewerRequest()
        {
            if (dgvReviewerRequests.CurrentRow == null)
            {
                return null;
            }

            string email = GetCellText(dgvReviewerRequests.CurrentRow, "Email");

            if (email == "")
            {
                return null;
            }

            return ReviewerRequestStorage.Load()
                .FirstOrDefault(r => string.Equals(r.Email, email, StringComparison.OrdinalIgnoreCase));
        }

        private void UpdateReviewerRequestStatus(string email, string status)
        {
            List<ReviewerRequest> requests = ReviewerRequestStorage.Load();

            ReviewerRequest request = requests.FirstOrDefault(r =>
                string.Equals(r.Email, email, StringComparison.OrdinalIgnoreCase));

            if (request != null)
            {
                request.Status = status;
                ReviewerRequestStorage.Save(requests);
            }
        }

        private bool EmailExistsInDatabase(string email)
        {
            string query = @"
                SELECT COUNT(*)
                FROM dbo.tb_participants
                WHERE email = @Email;
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Email", email)
            };

            object result = Database.ExecuteScalar(query, parameters);

            return result != null && Convert.ToInt32(result) > 0;
        }

        private string GetCellText(DataGridViewRow row, string columnName)
        {
            if (row == null || row.DataGridView == null)
            {
                return "";
            }

            if (!row.DataGridView.Columns.Contains(columnName))
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadParticipants();
        }

        private void cmbRoleFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadParticipants();
        }

        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadParticipants();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}