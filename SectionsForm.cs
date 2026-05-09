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

        public SectionsForm(int userId, string role)
        {
            InitializeComponent();
            AppTheme.ApplyFormStyle(this);

            currentUserId = userId;
            currentUserRole = role;

            SetupGridStyle();
            ConfigureAccessByRole();
            CenterTitle();

            dgvSections.MouseDown += dgvSections_MouseDown;

            LoadSections();
        }

        private void LoadSections()
        {
            try
            {
                string query = @"
                    SELECT
                        s.id_section AS [ID],
                        s.section_name AS [Название секции],
                        s.description AS [Описание],
                        CASE
                            WHEN sv.id_visit IS NULL THEN N'Не записан'
                            ELSE N'Записан'
                        END AS [Статус записи]
                    FROM dbo.tb_sections AS s
                    LEFT JOIN dbo.tb_section_visits AS sv
                        ON s.id_section = sv.id_section
                       AND sv.id_participant = @UserId
                    ORDER BY s.section_name;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@UserId", currentUserId)
                };

                DataTable table = Database.ExecuteSelect(query, parameters);

                dgvSections.DataSource = null;
                dgvSections.AutoGenerateColumns = true;
                dgvSections.DataSource = table;

                if (dgvSections.Columns.Contains("ID"))
                    dgvSections.Columns["ID"].Visible = false;

                if (dgvSections.Columns.Contains("Описание"))
                    dgvSections.Columns["Описание"].Visible = false;

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
            bool canManage = currentUserRole == "Организатор" || currentUserRole == "Администратор";
            bool isParticipant = currentUserRole == "Участник";

            btnRegister.Visible = isParticipant;
            btnCancelRegister.Visible = isParticipant;

            btnAdd.Visible = canManage;
            btnUpdate.Visible = canManage;
            btnDelete.Visible = canManage;

            txtSectionName.ReadOnly = !canManage;
            txtDescription.ReadOnly = !canManage;
        }

        private void CenterTitle()
        {
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
        }

        private void dgvSections_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvSections.Rows[e.RowIndex];

            selectedSectionId = Convert.ToInt32(row.Cells["ID"].Value);
            txtSectionName.Text = row.Cells["Название секции"].Value.ToString();
            txtDescription.Text = row.Cells["Описание"].Value == DBNull.Value ? "" : row.Cells["Описание"].Value.ToString();

            LoadSectionReports(selectedSectionId);
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
            if (selectedSectionId == 0)
            {
                MessageBox.Show("Выберите секцию.");
                return;
            }

            string visitStatus = GetSelectedVisitStatus();

            if (visitStatus == "Записан")
            {
                MessageBox.Show("Вы уже записаны на эту секцию.");
                return;
            }

            try
            {
                string query = @"
                    INSERT INTO dbo.tb_section_visits
                    (
                        id_participant,
                        id_section
                    )
                    VALUES
                    (
                        @UserId,
                        @SectionId
                    );
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@UserId", currentUserId),
                    new SqlParameter("@SectionId", selectedSectionId)
                };

                Database.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Вы записаны на секцию.");
                LoadSections();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка записи на секцию: " + ex.Message);
            }
        }

        private void btnCancelRegister_Click(object sender, EventArgs e)
        {
            if (selectedSectionId == 0)
            {
                MessageBox.Show("Выберите секцию.");
                return;
            }

            string visitStatus = GetSelectedVisitStatus();

            if (visitStatus != "Записан")
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
                    WHERE id_participant = @UserId
                      AND id_section = @SectionId;
                ";

                SqlParameter[] parameters =
                {
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

        private string GetSelectedVisitStatus()
        {
            if (dgvSections.CurrentRow == null)
                return "";

            if (!dgvSections.Columns.Contains("Статус записи"))
                return "";

            object value = dgvSections.CurrentRow.Cells["Статус записи"].Value;

            if (value == null || value == DBNull.Value)
                return "";

            return value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
                return;

            try
            {
                string query = @"
                    INSERT INTO dbo.tb_sections
                    (
                        section_name,
                        description
                    )
                    VALUES
                    (
                        @SectionName,
                        @Description
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
                    MessageBox.Show("Секцию нельзя удалить: на неё есть записи участников или доклады в программе.");
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
            selectedSectionId = 0;

            txtSectionName.Clear();
            txtDescription.Clear();
            lstReports.Items.Clear();

            if (dgvSections.Rows.Count > 0)
                dgvSections.ClearSelection();
        }
    }
}