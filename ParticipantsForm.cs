using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ConferenceApp
{
    public partial class ParticipantsForm : Form
    {
        private string currentUserRole;

        public ParticipantsForm()
            : this("Организатор")
        {
        }

        public ParticipantsForm(string userRole)
        {
            currentUserRole = userRole;

            InitializeComponent();
            ConfigureFormByRole();

            cmbRoleFilter.SelectedIndex = 0;
            cmbStatusFilter.SelectedIndex = 0;

            LoadParticipants();
            LoadReviewerRequests();
        }

        private bool IsAdmin()
        {
            return currentUserRole == "Администратор";
        }

        private void ConfigureFormByRole()
        {
            cmbRoleFilter.Items.Clear();

            if (IsAdmin())
            {
                Text = "Пользователи системы";
                lblTitle.Text = "Пользователи системы";
                tabPeople.Text = "Пользователи";

                cmbRoleFilter.Items.AddRange(new object[]
                {
                    "Все",
                    "Участник",
                    "Рецензент",
                    "Организатор",
                    "Администратор"
                });

                btnAddParticipant.Visible = true;
                btnEditParticipant.Visible = true;
                btnChangePassword.Visible = true;
            }
            else
            {
                Text = "Участники и рецензенты";
                lblTitle.Text = "Участники и рецензенты";
                tabPeople.Text = "Участники и рецензенты";

                cmbRoleFilter.Items.AddRange(new object[]
                {
                    "Все",
                    "Участник",
                    "Рецензент"
                });

                btnAddParticipant.Visible = false;
                btnEditParticipant.Visible = false;
                btnChangePassword.Visible = false;
            }
        }

        private void LoadParticipants()
        {
            string search = txtSearch.Text.Trim();
            string roleFilter = cmbRoleFilter.Text;
            string statusFilter = cmbStatusFilter.Text;

            string query = @"
                SELECT
                    p.id_participant,
                    p.last_name,
                    p.first_name,
                    p.middle_name,
                    LTRIM(RTRIM(
                        p.last_name + N' ' + p.first_name + N' ' + ISNULL(p.middle_name, N'')
                    )) AS [ФИО],
                    p.email AS [Email],
                    p.phone AS [Телефон],
                    p.participant_status AS [Статус участия],
                    p.user_role AS [Роль],
                    p.workplace AS [Место работы],
                    p.academic_degree AS [Учёная степень]
                FROM dbo.tb_participants AS p
                WHERE 1 = 1
            ";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!IsAdmin())
            {
                query += " AND p.user_role IN (N'Участник', N'Рецензент') ";
            }

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
                ORDER BY
                    p.user_role,
                    p.last_name,
                    p.first_name;
            ";

            DataTable table = Database.ExecuteSelect(query, parameters.ToArray());

            dgvParticipants.DataSource = table;

            HideTechnicalColumns();

            dgvParticipants.ClearSelection();
            ClearDetails();
        }

        private void HideTechnicalColumns()
        {
            string[] hiddenColumns =
            {
                "id_participant",
                "last_name",
                "first_name",
                "middle_name"
            };

            foreach (string columnName in hiddenColumns)
            {
                if (dgvParticipants.Columns.Contains(columnName))
                {
                    dgvParticipants.Columns[columnName].Visible = false;
                }
            }
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
                    r.file_name AS [Файл]
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

        private void LoadSectionVisits(int participantId)
        {
            string query = @"
                SELECT
                    s.section_name AS [Секция],
                    s.description AS [Описание]
                FROM dbo.tb_section_visits AS sv
                JOIN dbo.tb_sections AS s
                    ON sv.id_section = s.id_section
                WHERE sv.id_participant = @ParticipantId
                ORDER BY s.section_name;
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@ParticipantId", participantId)
            };

            dgvUserReports.DataSource = Database.ExecuteSelect(query, parameters);
        }

        private void LoadEmptyRelatedData()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Информация");
            table.Rows.Add("Связанные данные отсутствуют.");

            dgvUserReports.DataSource = table;
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

            gbUserReports.Text = "Связанные данные выбранного пользователя";
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
            string participantStatus = GetCellText(row, "Статус участия");

            lblFullNameValue.Text = GetCellText(row, "ФИО");
            lblEmailValue.Text = GetCellText(row, "Email");
            lblPhoneValue.Text = GetCellText(row, "Телефон");
            lblParticipantStatusValue.Text = participantStatus;
            lblRoleValue.Text = role;
            lblWorkplaceValue.Text = GetCellText(row, "Место работы");
            lblAcademicDegreeValue.Text = GetCellText(row, "Учёная степень");

            if (role == "Рецензент")
            {
                gbUserReports.Text = "Проверенные доклады рецензента";
                LoadReviewedReports(participantId);
            }
            else if (participantStatus == "Докладчик")
            {
                gbUserReports.Text = "Доклады выбранного пользователя";
                LoadParticipantReports(participantId);
            }
            else if (participantStatus == "Слушатель" && role == "Участник")
            {
                gbUserReports.Text = "Выбранные секции слушателя";
                LoadSectionVisits(participantId);
            }
            else
            {
                gbUserReports.Text = "Связанные данные выбранного пользователя";
                LoadEmptyRelatedData();
            }
        }

        private void btnAddParticipant_Click(object sender, EventArgs e)
        {
            if (!IsAdmin())
            {
                MessageBox.Show("Добавление пользователей доступно только администратору.");
                return;
            }

            ParticipantEditDialog dialog = new ParticipantEditDialog(false, null);

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (EmailExistsInDatabase(dialog.Email))
            {
                MessageBox.Show("Пользователь с таким email уже существует.");
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
                    @ParticipantStatus,
                    @UserRole,
                    @Workplace,
                    @AcademicDegree,
                    @PasswordHash
                );
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@LastName", dialog.LastName),
                new SqlParameter("@FirstName", dialog.FirstName),
                new SqlParameter("@MiddleName", ToDbValue(dialog.MiddleName)),
                new SqlParameter("@Email", dialog.Email),
                new SqlParameter("@Phone", ToDbValue(dialog.Phone)),
                new SqlParameter("@ParticipantStatus", dialog.ParticipantStatus),
                new SqlParameter("@UserRole", dialog.UserRole),
                new SqlParameter("@Workplace", ToDbValue(dialog.Workplace)),
                new SqlParameter("@AcademicDegree", ToDbValue(dialog.AcademicDegree)),
                new SqlParameter("@PasswordHash", GetSha256Hash(dialog.Password))
            };

            int result = Database.ExecuteNonQuery(query, parameters);

            if (result > 0)
            {
                MessageBox.Show("Пользователь добавлен.");
                LoadParticipants();
            }
        }

        private void btnEditParticipant_Click(object sender, EventArgs e)
        {
            if (!IsAdmin())
            {
                MessageBox.Show("Редактирование пользователей доступно только администратору.");
                return;
            }

            if (dgvParticipants.CurrentRow == null)
            {
                MessageBox.Show("Выберите пользователя.");
                return;
            }

            DataGridViewRow row = dgvParticipants.CurrentRow;

            int participantId = Convert.ToInt32(row.Cells["id_participant"].Value);
            string oldEmail = GetCellText(row, "Email");

            ParticipantEditDialog dialog = new ParticipantEditDialog(true, row);

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (!string.Equals(oldEmail, dialog.Email, StringComparison.OrdinalIgnoreCase) &&
                EmailExistsInDatabase(dialog.Email))
            {
                MessageBox.Show("Пользователь с таким email уже существует.");
                return;
            }

            string query = @"
                UPDATE dbo.tb_participants
                SET
                    last_name = @LastName,
                    first_name = @FirstName,
                    middle_name = @MiddleName,
                    email = @Email,
                    phone = @Phone,
                    participant_status = @ParticipantStatus,
                    user_role = @UserRole,
                    workplace = @Workplace,
                    academic_degree = @AcademicDegree
                WHERE id_participant = @ParticipantId;
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@ParticipantId", participantId),
                new SqlParameter("@LastName", dialog.LastName),
                new SqlParameter("@FirstName", dialog.FirstName),
                new SqlParameter("@MiddleName", ToDbValue(dialog.MiddleName)),
                new SqlParameter("@Email", dialog.Email),
                new SqlParameter("@Phone", ToDbValue(dialog.Phone)),
                new SqlParameter("@ParticipantStatus", dialog.ParticipantStatus),
                new SqlParameter("@UserRole", dialog.UserRole),
                new SqlParameter("@Workplace", ToDbValue(dialog.Workplace)),
                new SqlParameter("@AcademicDegree", ToDbValue(dialog.AcademicDegree))
            };

            int result = Database.ExecuteNonQuery(query, parameters);

            if (result > 0)
            {
                MessageBox.Show("Данные пользователя обновлены.");
                LoadParticipants();
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (!IsAdmin())
            {
                MessageBox.Show("Смена пароля доступна только администратору.");
                return;
            }

            if (dgvParticipants.CurrentRow == null)
            {
                MessageBox.Show("Выберите пользователя.");
                return;
            }

            DataGridViewRow row = dgvParticipants.CurrentRow;

            int participantId = Convert.ToInt32(row.Cells["id_participant"].Value);
            string fullName = GetCellText(row, "ФИО");

            PasswordEditDialog dialog = new PasswordEditDialog(fullName);

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string query = @"
                UPDATE dbo.tb_participants
                SET password_hash = @PasswordHash
                WHERE id_participant = @ParticipantId;
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@ParticipantId", participantId),
                new SqlParameter("@PasswordHash", GetSha256Hash(dialog.Password))
            };

            int result = Database.ExecuteNonQuery(query, parameters);

            if (result > 0)
            {
                MessageBox.Show("Пароль изменён.");
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
            string role = GetCellText(row, "Роль");

            if (role == "Администратор" && CountAdmins() <= 1)
            {
                MessageBox.Show("Нельзя удалить последнего администратора.");
                return;
            }

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

            int deleteResult = Database.ExecuteNonQuery(query, parameters);

            if (deleteResult > 0)
            {
                MessageBox.Show("Пользователь удалён.");
                LoadParticipants();
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
                new SqlParameter("@MiddleName", ToDbValue(request.MiddleName)),
                new SqlParameter("@Email", request.Email),
                new SqlParameter("@Phone", ToDbValue(request.Phone)),
                new SqlParameter("@Workplace", ToDbValue(request.Workplace)),
                new SqlParameter("@AcademicDegree", ToDbValue(request.AcademicDegree)),
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

        private int CountAdmins()
        {
            string query = @"
                SELECT COUNT(*)
                FROM dbo.tb_participants
                WHERE user_role = N'Администратор';
            ";

            object result = Database.ExecuteScalar(query);

            if (result == null)
            {
                return 0;
            }

            return Convert.ToInt32(result);
        }

        private object ToDbValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return DBNull.Value;
            }

            return value.Trim();
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

        private string GetSha256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();

                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
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

        private class ParticipantEditDialog : Form
        {
            private TextBox txtLastName;
            private TextBox txtFirstName;
            private TextBox txtMiddleName;
            private TextBox txtEmail;
            private TextBox txtPhone;
            private TextBox txtWorkplace;
            private ComboBox cmbParticipantStatus;
            private ComboBox cmbUserRole;
            private ComboBox cmbAcademicDegree;
            private TextBox txtPassword;
            private Button btnSave;
            private Button btnCancel;
            private bool editMode;

            public string LastName { get; private set; }
            public string FirstName { get; private set; }
            public string MiddleName { get; private set; }
            public string Email { get; private set; }
            public string Phone { get; private set; }
            public string ParticipantStatus { get; private set; }
            public string UserRole { get; private set; }
            public string Workplace { get; private set; }
            public string AcademicDegree { get; private set; }
            public string Password { get; private set; }

            public ParticipantEditDialog(bool editMode, DataGridViewRow row)
            {
                this.editMode = editMode;

                InitializeDialog();

                if (editMode && row != null)
                {
                    Text = "Редактирование пользователя";
                    txtLastName.Text = GetValue(row, "last_name");
                    txtFirstName.Text = GetValue(row, "first_name");
                    txtMiddleName.Text = GetValue(row, "middle_name");
                    txtEmail.Text = GetValue(row, "Email");
                    txtPhone.Text = GetValue(row, "Телефон");
                    txtWorkplace.Text = GetValue(row, "Место работы");
                    cmbParticipantStatus.Text = GetValue(row, "Статус участия");
                    cmbUserRole.Text = GetValue(row, "Роль");

                    string degree = GetValue(row, "Учёная степень");
                    cmbAcademicDegree.Text = degree == "" ? "Нет" : degree;

                    txtPassword.Visible = false;
                    Controls["lblPassword"].Visible = false;
                }
                else
                {
                    Text = "Добавление пользователя";
                    cmbParticipantStatus.SelectedIndex = 0;
                    cmbUserRole.SelectedIndex = 0;
                    cmbAcademicDegree.SelectedIndex = 0;
                }
            }

            private void InitializeDialog()
            {
                Width = 430;
                Height = 455;
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                BackColor = Color.FromArgb(240, 247, 255);

                AddLabel("Фамилия:", 20, 25);
                txtLastName = AddTextBox(160, 22);

                AddLabel("Имя:", 20, 60);
                txtFirstName = AddTextBox(160, 57);

                AddLabel("Отчество:", 20, 95);
                txtMiddleName = AddTextBox(160, 92);

                AddLabel("Email:", 20, 130);
                txtEmail = AddTextBox(160, 127);

                AddLabel("Телефон:", 20, 165);
                txtPhone = AddTextBox(160, 162);

                AddLabel("Статус:", 20, 200);
                cmbParticipantStatus = AddComboBox(160, 197);
                cmbParticipantStatus.Items.AddRange(new object[]
                {
                    "Слушатель",
                    "Докладчик"
                });

                AddLabel("Роль:", 20, 235);
                cmbUserRole = AddComboBox(160, 232);
                cmbUserRole.Items.AddRange(new object[]
                {
                    "Участник",
                    "Рецензент",
                    "Организатор",
                    "Администратор"
                });

                AddLabel("Место работы:", 20, 270);
                txtWorkplace = AddTextBox(160, 267);

                AddLabel("Учёная степень:", 20, 305);
                cmbAcademicDegree = AddComboBox(160, 302);
                cmbAcademicDegree.DropDownStyle = ComboBoxStyle.DropDown;
                cmbAcademicDegree.Items.AddRange(new object[]
                {
                "Нет",
                "бакалавр",
                "магистр",
                "аспирант",
                "к.т.н.",
                "к.ф.-м.н.",
                "д.т.н.",
                "д.ф.-м.н."
            });

                Label lblPassword = AddLabel("Пароль:", 20, 340);
                lblPassword.Name = "lblPassword";

                txtPassword = AddTextBox(160, 337);
                txtPassword.UseSystemPasswordChar = true;

                btnSave = new Button();
                btnSave.Text = "Сохранить";
                btnSave.Location = new Point(165, 375);
                btnSave.Size = new Size(110, 30);
                btnSave.Click += btnSave_Click;
                Controls.Add(btnSave);

                btnCancel = new Button();
                btnCancel.Text = "Отмена";
                btnCancel.Location = new Point(285, 375);
                btnCancel.Size = new Size(100, 30);
                btnCancel.DialogResult = DialogResult.Cancel;
                Controls.Add(btnCancel);
            }

            private Label AddLabel(string text, int x, int y)
            {
                Label label = new Label();
                label.Text = text;
                label.Location = new Point(x, y);
                label.Size = new Size(130, 22);
                Controls.Add(label);

                return label;
            }

            private TextBox AddTextBox(int x, int y)
            {
                TextBox textBox = new TextBox();
                textBox.Location = new Point(x, y);
                textBox.Size = new Size(225, 22);
                Controls.Add(textBox);

                return textBox;
            }

            private ComboBox AddComboBox(int x, int y)
            {
                ComboBox comboBox = new ComboBox();
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox.Location = new Point(x, y);
                comboBox.Size = new Size(225, 24);
                Controls.Add(comboBox);

                return comboBox;
            }

            private void btnSave_Click(object sender, EventArgs e)
            {
                LastName = txtLastName.Text.Trim();
                FirstName = txtFirstName.Text.Trim();
                MiddleName = txtMiddleName.Text.Trim();
                Email = txtEmail.Text.Trim();
                Phone = txtPhone.Text.Trim();
                ParticipantStatus = cmbParticipantStatus.Text.Trim();
                UserRole = cmbUserRole.Text.Trim();
                Workplace = txtWorkplace.Text.Trim();
                AcademicDegree = cmbAcademicDegree.Text.Trim() == "Нет" ? "" : cmbAcademicDegree.Text.Trim();
                Password = txtPassword.Text.Trim();

                if (LastName == "" || FirstName == "" || Email == "")
                {
                    MessageBox.Show("Заполните фамилию, имя и email.");
                    return;
                }

                if (!Email.Contains("@") || !Email.Contains("."))
                {
                    MessageBox.Show("Некорректный email.");
                    return;
                }

                if (ParticipantStatus == "" || UserRole == "")
                {
                    MessageBox.Show("Выберите статус и роль.");
                    return;
                }

                if (!editMode && Password == "")
                {
                    MessageBox.Show("Введите пароль.");
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }

            private string GetValue(DataGridViewRow row, string columnName)
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
        }

        private class PasswordEditDialog : Form
        {
            private TextBox txtPassword;
            private TextBox txtConfirmPassword;
            private Button btnSave;
            private Button btnCancel;

            public string Password { get; private set; }

            public PasswordEditDialog(string fullName)
            {
                InitializeDialog(fullName);
            }

            private void InitializeDialog(string fullName)
            {
                Text = "Смена пароля";
                Width = 400;
                Height = 230;
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                BackColor = Color.FromArgb(240, 247, 255);

                Label lblUser = new Label();
                lblUser.Text = "Пользователь: " + fullName;
                lblUser.Location = new Point(20, 20);
                lblUser.Size = new Size(340, 22);
                Controls.Add(lblUser);

                Label lblPassword = new Label();
                lblPassword.Text = "Новый пароль:";
                lblPassword.Location = new Point(20, 60);
                lblPassword.Size = new Size(130, 22);
                Controls.Add(lblPassword);

                txtPassword = new TextBox();
                txtPassword.Location = new Point(160, 57);
                txtPassword.Size = new Size(200, 22);
                txtPassword.UseSystemPasswordChar = true;
                Controls.Add(txtPassword);

                Label lblConfirmPassword = new Label();
                lblConfirmPassword.Text = "Повтор пароля:";
                lblConfirmPassword.Location = new Point(20, 95);
                lblConfirmPassword.Size = new Size(130, 22);
                Controls.Add(lblConfirmPassword);

                txtConfirmPassword = new TextBox();
                txtConfirmPassword.Location = new Point(160, 92);
                txtConfirmPassword.Size = new Size(200, 22);
                txtConfirmPassword.UseSystemPasswordChar = true;
                Controls.Add(txtConfirmPassword);

                btnSave = new Button();
                btnSave.Text = "Сохранить";
                btnSave.Location = new Point(145, 135);
                btnSave.Size = new Size(110, 30);
                btnSave.Click += btnSave_Click;
                Controls.Add(btnSave);

                btnCancel = new Button();
                btnCancel.Text = "Отмена";
                btnCancel.Location = new Point(265, 135);
                btnCancel.Size = new Size(95, 30);
                btnCancel.DialogResult = DialogResult.Cancel;
                Controls.Add(btnCancel);
            }

            private void btnSave_Click(object sender, EventArgs e)
            {
                string password = txtPassword.Text.Trim();
                string confirmPassword = txtConfirmPassword.Text.Trim();

                if (password == "")
                {
                    MessageBox.Show("Введите пароль.");
                    return;
                }

                if (password != confirmPassword)
                {
                    MessageBox.Show("Пароли не совпадают.");
                    return;
                }

                Password = password;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}