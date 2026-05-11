using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ConferenceApp
{
    public partial class SectionsForm : Form
    {
        private int currentUserId;
        private string currentUserRole;
        private int selectedSectionId = 0;
        private int selectedVisitId = 0;
        private int selectedFreePlaces = 0;

        private bool isLoadingSections = false;
        private bool isClearingFields = false;

        public SectionsForm(int userId, string role)
        {
            InitializeComponent();
            AppTheme.ApplyFormStyle(this);

            currentUserId = userId;
            currentUserRole = role == null ? "" : role.Trim();

            SetupGridStyle();
            ConfigureAccessByRole();
            CenterTitle();

            dgvSections.MouseDown += dgvSections_MouseDown;
            dgvSections.SelectionChanged += dgvSections_SelectionChanged;

            LoadSections();
        }

        private bool IsParticipant()
        {
            return currentUserRole == "Участник";
        }

        private bool IsOrganizerOrAdmin()
        {
            return currentUserRole == "Организатор" || currentUserRole == "Администратор";
        }

        private void LoadSections()
        {
            try
            {
                isLoadingSections = true;

                string query = @"
                    SELECT
                        s.id_section AS [ID],
                        s.section_name AS [Название секции],
                        s.description AS [Описание],
                        s.max_participants AS [Макс. участников],
                        COUNT(all_visits.id_visit) AS [Записано],
                        s.max_participants - COUNT(all_visits.id_visit) AS [Свободно],

                        user_visit.id_visit AS [ID посещения],
                        user_visit.organization_score AS [Оценка организации],
                        user_visit.content_score AS [Оценка содержания],
                        user_visit.usefulness_score AS [Оценка пользы],
                        user_visit.visit_comment AS [Комментарий],

                        CASE
                            WHEN @CanVisit = 0 THEN N''
                            WHEN user_visit.id_visit IS NULL THEN N'Не записан'
                            ELSE N'Записан'
                        END AS [Статус записи]
                    FROM dbo.tb_sections AS s
                    LEFT JOIN
                    (
                        SELECT
                            sv.id_visit,
                            sv.id_section
                        FROM dbo.tb_section_visits AS sv
                        INNER JOIN dbo.tb_participants AS p
                            ON sv.id_participant = p.id_participant
                        WHERE p.user_role = N'Участник'
                    ) AS all_visits
                        ON s.id_section = all_visits.id_section
                    LEFT JOIN dbo.tb_section_visits AS user_visit
                        ON s.id_section = user_visit.id_section
                       AND user_visit.id_participant = @UserId
                       AND @CanVisit = 1
                    GROUP BY
                        s.id_section,
                        s.section_name,
                        s.description,
                        s.max_participants,
                        user_visit.id_visit,
                        user_visit.organization_score,
                        user_visit.content_score,
                        user_visit.usefulness_score,
                        user_visit.visit_comment
                    ORDER BY s.section_name;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@UserId", currentUserId),
                    new SqlParameter("@CanVisit", IsParticipant() ? 1 : 0)
                };

                DataTable table = Database.ExecuteSelect(query, parameters);

                dgvSections.DataSource = null;
                dgvSections.AutoGenerateColumns = true;
                dgvSections.DataSource = table;

                HideColumn("ID");
                HideColumn("Описание");
                HideColumn("ID посещения");
                HideColumn("Оценка организации");
                HideColumn("Оценка содержания");
                HideColumn("Оценка пользы");
                HideColumn("Комментарий");

                if (!IsParticipant())
                    HideColumn("Статус записи");

                if (dgvSections.Columns.Contains("Название секции"))
                    dgvSections.Columns["Название секции"].HeaderText = "Название";

                foreach (DataGridViewColumn column in dgvSections.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки секций: " + ex.Message);
            }
            finally
            {
                isLoadingSections = false;
            }
        }

        private void HideColumn(string columnName)
        {
            if (dgvSections.Columns.Contains(columnName))
                dgvSections.Columns[columnName].Visible = false;
        }

        private void LoadSectionReports(int sectionId)
        {
            try
            {
                lstReports.Items.Clear();

                string query = @"
                    SELECT
                        CASE
                            WHEN p.middle_name IS NULL OR LTRIM(RTRIM(p.middle_name)) = N''
                                THEN LEFT(p.first_name, 1) + N'. ' + p.last_name + N' — ' + r.topic
                            ELSE LEFT(p.first_name, 1) + N'.' + LEFT(p.middle_name, 1) + N'. ' + p.last_name + N' — ' + r.topic
                        END AS report_info
                    FROM dbo.tb_conference_program cp
                    INNER JOIN dbo.tb_reports r
                        ON cp.id_report = r.id_report
                    INNER JOIN dbo.tb_participants p
                        ON r.id_author = p.id_participant
                    WHERE cp.id_section = @SectionId
                    ORDER BY p.last_name, r.topic;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@SectionId", sectionId)
                };

                DataTable table = Database.ExecuteSelect(query, parameters);

                if (table.Rows.Count == 0)
                {
                    lstReports.Items.Add("Доклады для этой секции отсутствуют.");
                    return;
                }

                foreach (DataRow row in table.Rows)
                {
                    lstReports.Items.Add(row["report_info"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки докладов секции: " + ex.Message);
            }
        }

        private void SetupGridStyle()
        {
            dgvSections.BackgroundColor = Color.White;
            dgvSections.BorderStyle = BorderStyle.FixedSingle;
            dgvSections.RowHeadersVisible = false;

            dgvSections.AllowUserToAddRows = false;
            dgvSections.AllowUserToDeleteRows = false;
            dgvSections.AllowUserToResizeRows = false;
            dgvSections.ReadOnly = true;

            dgvSections.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSections.MultiSelect = false;
            dgvSections.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvSections.GridColor = Color.LightGray;
            dgvSections.EnableHeadersVisualStyles = false;

            dgvSections.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 240, 250);
            dgvSections.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvSections.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvSections.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvSections.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);

            dgvSections.DefaultCellStyle.BackColor = Color.White;
            dgvSections.DefaultCellStyle.ForeColor = Color.Black;
            dgvSections.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 230, 250);
            dgvSections.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvSections.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F);

            dgvSections.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 252, 255);
        }

        private void ConfigureAccessByRole()
        {
            bool canManage = IsOrganizerOrAdmin();
            bool isParticipant = IsParticipant();

            btnRegister.Visible = isParticipant;
            btnCancelRegister.Visible = isParticipant;

            btnAdd.Visible = canManage;
            btnUpdate.Visible = canManage;
            btnDelete.Visible = canManage;

            lblFeedback.Visible = isParticipant;
            lblOrganizationScore.Visible = isParticipant;
            lblContentScore.Visible = isParticipant;
            lblUsefulnessScore.Visible = isParticipant;
            lblVisitComment.Visible = isParticipant;
            nudOrganizationScore.Visible = isParticipant;
            nudContentScore.Visible = isParticipant;
            nudUsefulnessScore.Visible = isParticipant;
            txtVisitComment.Visible = isParticipant;
            btnSaveFeedback.Visible = isParticipant;

            txtSectionName.ReadOnly = !canManage;
            txtDescription.ReadOnly = !canManage;

            AppTheme.ApplyButtonStyle(btnSaveFeedback);
            btnSaveFeedback.UseVisualStyleBackColor = false;
            btnSaveFeedback.ForeColor = Color.White;

            UpdateFeedbackControlsState(false);
        }

        private void CenterTitle()
        {
            lblTitle.Left = (ClientSize.Width - lblTitle.Width) / 2;
        }

        private void dgvSections_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            FillFieldsFromSelectedRow(dgvSections.Rows[e.RowIndex]);
        }

        private void dgvSections_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoadingSections || isClearingFields)
                return;

            if (dgvSections.CurrentRow == null)
                return;

            FillFieldsFromSelectedRow(dgvSections.CurrentRow);
        }

        private void FillFieldsFromSelectedRow(DataGridViewRow row)
        {
            if (row == null)
                return;

            if (isLoadingSections || isClearingFields)
                return;

            if (!dgvSections.Columns.Contains("ID"))
                return;

            selectedSectionId = Convert.ToInt32(row.Cells["ID"].Value);
            selectedVisitId = GetVisitIdFromRow(row);
            selectedFreePlaces = GetIntFromRow(row, "Свободно");

            txtSectionName.Text = GetCellValue(row, "Название секции");
            txtDescription.Text = GetCellValue(row, "Описание");

            LoadSectionReports(selectedSectionId);
            LoadFeedbackFromRow(row);
        }

        private void LoadFeedbackFromRow(DataGridViewRow row)
        {
            if (!IsParticipant())
                return;

            selectedVisitId = GetVisitIdFromRow(row);

            SetNumericValue(nudOrganizationScore, GetCellValue(row, "Оценка организации"));
            SetNumericValue(nudContentScore, GetCellValue(row, "Оценка содержания"));
            SetNumericValue(nudUsefulnessScore, GetCellValue(row, "Оценка пользы"));

            txtVisitComment.Text = GetCellValue(row, "Комментарий");

            UpdateFeedbackControlsState(selectedVisitId > 0);
        }

        private int GetVisitIdFromRow(DataGridViewRow row)
        {
            return GetIntFromRow(row, "ID посещения");
        }

        private int GetIntFromRow(DataGridViewRow row, string columnName)
        {
            if (row == null)
                return 0;

            if (!dgvSections.Columns.Contains(columnName))
                return 0;

            object value = row.Cells[columnName].Value;

            if (value == null || value == DBNull.Value)
                return 0;

            return Convert.ToInt32(value);
        }

        private void SetNumericValue(NumericUpDown control, string value)
        {
            if (int.TryParse(value, out int score) && score >= control.Minimum && score <= control.Maximum)
                control.Value = score;
            else
                control.Value = 5;
        }

        private string GetCellValue(DataGridViewRow row, string columnName)
        {
            if (row == null)
                return "";

            if (!dgvSections.Columns.Contains(columnName))
                return "";

            object value = row.Cells[columnName].Value;

            if (value == null || value == DBNull.Value)
                return "";

            return value.ToString();
        }

        private void UpdateFeedbackControlsState(bool enabled)
        {
            if (!IsParticipant())
                return;

            nudOrganizationScore.Enabled = enabled;
            nudContentScore.Enabled = enabled;
            nudUsefulnessScore.Enabled = enabled;
            txtVisitComment.Enabled = enabled;
            btnSaveFeedback.Enabled = enabled;

            nudOrganizationScore.ReadOnly = !enabled;
            nudContentScore.ReadOnly = !enabled;
            nudUsefulnessScore.ReadOnly = !enabled;
            txtVisitComment.ReadOnly = !enabled;

            AppTheme.ApplyButtonStyle(btnSaveFeedback);
            btnSaveFeedback.UseVisualStyleBackColor = false;
            btnSaveFeedback.ForeColor = Color.White;
        }

        private void dgvSections_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dgvSections.HitTest(e.X, e.Y);

            if (hit.Type == DataGridViewHitTestType.None)
            {
                ClearFields();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!IsParticipant())
            {
                MessageBox.Show("Запись на секции доступна только участникам.");
                return;
            }

            if (selectedSectionId == 0)
            {
                MessageBox.Show("Выберите секцию.");
                return;
            }

            if (selectedVisitId > 0)
            {
                MessageBox.Show("Вы уже записаны на эту секцию.");
                return;
            }

            if (selectedFreePlaces <= 0)
            {
                MessageBox.Show("В выбранной секции нет свободных мест.");
                return;
            }

            try
            {
                int sectionId = selectedSectionId;

                string query = @"
                    EXEC dbo.usp_register_section_visit
                        @id_participant = @UserId,
                        @id_section = @SectionId;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@UserId", currentUserId),
                    new SqlParameter("@SectionId", selectedSectionId)
                };

                Database.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Вы записаны на секцию.");
                LoadSections();
                SelectSectionInGrid(sectionId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка записи на секцию: " + ex.Message);
            }
        }

        private void btnCancelRegister_Click(object sender, EventArgs e)
        {
            if (!IsParticipant())
            {
                MessageBox.Show("Отмена записи доступна только участникам.");
                return;
            }

            if (selectedSectionId == 0)
            {
                MessageBox.Show("Выберите секцию.");
                return;
            }

            if (selectedVisitId == 0)
            {
                MessageBox.Show("Вы не записаны на эту секцию.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Отменить запись на выбранную секцию?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            try
            {
                string query = @"
                    DELETE FROM dbo.tb_section_visits
                    WHERE id_visit = @VisitId
                      AND id_participant = @UserId
                      AND id_section = @SectionId;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@VisitId", selectedVisitId),
                    new SqlParameter("@UserId", currentUserId),
                    new SqlParameter("@SectionId", selectedSectionId)
                };

                int rows = Database.ExecuteNonQuery(query, parameters);

                if (rows > 0)
                {
                    MessageBox.Show("Запись на секцию отменена.");
                    LoadSections();
                }
                else
                {
                    MessageBox.Show("Запись не найдена.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка отмены записи: " + ex.Message);
            }
        }

        private void btnSaveFeedback_Click(object sender, EventArgs e)
        {
            if (!IsParticipant())
            {
                MessageBox.Show("Оценивание доступно только участникам.");
                return;
            }

            if (selectedSectionId == 0)
            {
                MessageBox.Show("Выберите секцию.");
                return;
            }

            if (selectedVisitId == 0)
            {
                MessageBox.Show("Оценку можно оставить только после записи на секцию.");
                return;
            }

            try
            {
                int sectionId = selectedSectionId;

                string query = @"
                    EXEC dbo.usp_update_section_visit_feedback
                        @id_visit = @VisitId,
                        @organization_score = @OrganizationScore,
                        @content_score = @ContentScore,
                        @usefulness_score = @UsefulnessScore,
                        @visit_comment = @VisitComment;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@VisitId", selectedVisitId),
                    new SqlParameter("@OrganizationScore", Convert.ToInt32(nudOrganizationScore.Value)),
                    new SqlParameter("@ContentScore", Convert.ToInt32(nudContentScore.Value)),
                    new SqlParameter("@UsefulnessScore", Convert.ToInt32(nudUsefulnessScore.Value)),
                    new SqlParameter("@VisitComment", GetNullableText(txtVisitComment.Text))
                };

                Database.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Оценка и комментарий сохранены.");
                LoadSections();
                SelectSectionInGrid(sectionId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения оценки: " + ex.Message);
            }
        }

        private void SelectSectionInGrid(int sectionId)
        {
            if (sectionId == 0)
                return;

            foreach (DataGridViewRow row in dgvSections.Rows)
            {
                if (row.Cells["ID"].Value == null || row.Cells["ID"].Value == DBNull.Value)
                    continue;

                if (Convert.ToInt32(row.Cells["ID"].Value) == sectionId)
                {
                    row.Selected = true;
                    dgvSections.CurrentCell = row.Cells["Название секции"];

                    FillFieldsFromSelectedRow(row);
                    return;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!IsOrganizerOrAdmin())
                return;

            if (!ValidateFields())
                return;

            try
            {
                string query = @"
                    INSERT INTO dbo.tb_sections
                    (
                        section_name,
                        description,
                        max_participants
                    )
                    VALUES
                    (
                        @SectionName,
                        @Description,
                        30
                    );
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@SectionName", txtSectionName.Text.Trim()),
                    new SqlParameter("@Description", GetNullableText(txtDescription.Text))
                };

                Database.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Секция добавлена.");
                LoadSections();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления секции: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!IsOrganizerOrAdmin())
                return;

            if (selectedSectionId == 0)
            {
                MessageBox.Show("Выберите секцию.");
                return;
            }

            if (!ValidateFields())
                return;

            try
            {
                string query = @"
                    UPDATE dbo.tb_sections
                    SET
                        section_name = @SectionName,
                        description = @Description
                    WHERE id_section = @SectionId;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@SectionName", txtSectionName.Text.Trim()),
                    new SqlParameter("@Description", GetNullableText(txtDescription.Text)),
                    new SqlParameter("@SectionId", selectedSectionId)
                };

                Database.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Секция изменена.");
                LoadSections();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка изменения секции: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!IsOrganizerOrAdmin())
                return;

            if (selectedSectionId == 0)
            {
                MessageBox.Show("Выберите секцию.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Удалить выбранную секцию?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            try
            {
                string query = @"
                    DELETE FROM dbo.tb_sections
                    WHERE id_section = @SectionId
                      AND NOT EXISTS (
                          SELECT 1
                          FROM dbo.tb_section_visits
                          WHERE id_section = @SectionId
                      )
                      AND NOT EXISTS (
                          SELECT 1
                          FROM dbo.tb_conference_program
                          WHERE id_section = @SectionId
                      )
                      AND NOT EXISTS (
                          SELECT 1
                          FROM dbo.tb_materials
                          WHERE id_section = @SectionId
                      );
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@SectionId", selectedSectionId)
                };

                int rows = Database.ExecuteNonQuery(query, parameters);

                if (rows > 0)
                {
                    MessageBox.Show("Секция удалена.");
                    LoadSections();
                }
                else
                {
                    MessageBox.Show("Секцию нельзя удалить: на неё есть записи участников, доклады в программе или материалы.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления секции: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtSectionName.Text))
            {
                MessageBox.Show("Введите название секции.");
                return false;
            }

            return true;
        }

        private object GetNullableText(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return DBNull.Value;

            return value.Trim();
        }

        private void ClearFields()
        {
            try
            {
                isClearingFields = true;

                selectedSectionId = 0;
                selectedVisitId = 0;
                selectedFreePlaces = 0;

                txtSectionName.Clear();
                txtDescription.Clear();
                lstReports.Items.Clear();

                nudOrganizationScore.Value = 5;
                nudContentScore.Value = 5;
                nudUsefulnessScore.Value = 5;
                txtVisitComment.Clear();

                UpdateFeedbackControlsState(false);

                if (dgvSections.Rows.Count > 0)
                    dgvSections.ClearSelection();
            }
            finally
            {
                isClearingFields = false;
            }
        }
    }
}