using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ConferenceApp
{
    public partial class ReportsForm : Form
    {
        private int currentUserId;
        private string currentUserRole;
        private int selectedReportId = 0;
        private int reportIdToSelect = 0;
        private string selectedSourceFilePath = "";

        private class ReportFileData
        {
            public string FileName { get; set; }
            public string FileExtension { get; set; }
            public byte[] FileContent { get; set; }
        }

        public ReportsForm(int userId, string role)
        {
            InitializeComponent();

            currentUserId = userId;
            currentUserRole = role;

            InitializeForm();
        }

        public ReportsForm(int userId, string role, int reportId)
        {
            InitializeComponent();

            currentUserId = userId;
            currentUserRole = role;
            reportIdToSelect = reportId;

            InitializeForm();
        }

        private void InitializeForm()
        {
            SetupGridStyle();
            ConfigureAccessByRole();
            CenterTitle();

            dgvReports.MouseDown += dgvReports_MouseDown;

            if (IsOrganizerOrAdmin())
            {
                LoadSections();
            }

            LoadReports();

            if (reportIdToSelect > 0)
            {
                SelectReportInGrid(reportIdToSelect);
            }
        }

        private bool IsOrganizer()
        {
            return currentUserRole == "Организатор";
        }

        private bool IsAdmin()
        {
            return currentUserRole == "Администратор";
        }

        private bool IsOrganizerOrAdmin()
        {
            return IsOrganizer() || IsAdmin();
        }

        private void ConfigureAccessByRole()
        {
            bool isOrganizer = IsOrganizer();
            bool isAdmin = IsAdmin();
            bool isParticipant = !isOrganizer && !isAdmin;

            lblTitle.Text = IsOrganizerOrAdmin() ? "Доклады участников" : "Мои доклады";

            btnAdd.Visible = isParticipant;
            btnUpdate.Visible = isParticipant || isAdmin;
            btnDelete.Visible = isParticipant;
            btnChooseFile.Visible = isParticipant || isAdmin;
            btnClear.Visible = isParticipant || isAdmin;

            lblSection.Visible = IsOrganizerOrAdmin();
            cmbSection.Visible = IsOrganizerOrAdmin();
            lblDate.Visible = IsOrganizerOrAdmin();
            dtpDate.Visible = IsOrganizerOrAdmin();
            lblTime.Visible = IsOrganizerOrAdmin();
            dtpTime.Visible = IsOrganizerOrAdmin();
            lblLocation.Visible = IsOrganizerOrAdmin();
            txtLocation.Visible = IsOrganizerOrAdmin();
            btnAddToSection.Visible = IsOrganizerOrAdmin();

            if (IsOrganizerOrAdmin())
            {
                btnAddToSection.Text = "Сохранить в программе";
            }

            txtTopic.ReadOnly = isOrganizer;
            txtAnnotation.ReadOnly = isOrganizer;
            txtKeywords.ReadOnly = isOrganizer;
            txtFilePath.ReadOnly = true;
            txtReviewStatus.ReadOnly = true;
        }

        private void LoadReports()
        {
            try
            {
                DataTable table;

                if (IsOrganizerOrAdmin())
                {
                    string query = @"
                        SELECT
                            r.id_report AS [ID],
                            r.topic AS [Тема],
                            p.last_name + N' ' + p.first_name + N' ' + ISNULL(p.middle_name, N'') AS [Автор],
                            r.annotation AS [Аннотация],
                            r.keywords AS [Ключевые слова],
                            r.review_status AS [Статус],
                            r.file_name AS [Файл],
                            cp.id_section AS [ID секции],
                            ISNULL(s.section_name, N'Не добавлен') AS [Секция],
                            cp.presentation_date AS [Дата],
                            CONVERT(VARCHAR(5), cp.presentation_time, 108) AS [Время],
                            cp.location AS [Место]
                        FROM dbo.tb_reports r
                        JOIN dbo.tb_participants p
                            ON r.id_author = p.id_participant
                        LEFT JOIN dbo.tb_conference_program cp
                            ON r.id_report = cp.id_report
                        LEFT JOIN dbo.tb_sections s
                            ON cp.id_section = s.id_section
                        ORDER BY r.id_report DESC;
                    ";

                    table = Database.ExecuteSelect(query);
                }
                else
                {
                    string query = @"
                        SELECT
                            id_report AS [ID],
                            topic AS [Тема],
                            annotation AS [Аннотация],
                            keywords AS [Ключевые слова],
                            review_status AS [Статус рецензирования],
                            file_name AS [Файл]
                        FROM dbo.tb_reports
                        WHERE id_author = @UserId
                        ORDER BY id_report DESC;
                    ";

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@UserId", currentUserId)
                    };

                    table = Database.ExecuteSelect(query, parameters);
                }

                dgvReports.DataSource = null;
                dgvReports.AutoGenerateColumns = true;
                dgvReports.DataSource = table;

                if (dgvReports.Columns.Count > 0)
                {
                    dgvReports.Columns[0].Visible = false;
                }

                if (dgvReports.Columns.Contains("ID секции"))
                {
                    dgvReports.Columns["ID секции"].Visible = false;
                }

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

        private void LoadSections()
        {
            try
            {
                string query = @"
                    SELECT
                        id_section,
                        section_name
                    FROM dbo.tb_sections
                    ORDER BY section_name;
                ";

                DataTable table = Database.ExecuteSelect(query);

                cmbSection.DataSource = table;
                cmbSection.DisplayMember = "section_name";
                cmbSection.ValueMember = "id_section";
                cmbSection.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки секций: " + ex.Message);
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

        private void dgvReports_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dgvReports.HitTest(e.X, e.Y);

            if (hit.Type == DataGridViewHitTestType.None)
            {
                ClearFields();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!CanParticipantEdit())
                return;

            if (!ValidateFields())
                return;

            try
            {
                ReportFileData fileData = GetSelectedFileData();

                string query = @"
                    EXEC dbo.usp_add_report
                        @topic = @Topic,
                        @id_author = @AuthorId,
                        @annotation = @Annotation,
                        @keywords = @Keywords,
                        @file_name = @FileName,
                        @file_extension = @FileExtension,
                        @file_content = @FileContent;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Topic", txtTopic.Text.Trim()),
                    new SqlParameter("@AuthorId", currentUserId),
                    new SqlParameter("@Annotation", GetNullableText(txtAnnotation.Text)),
                    new SqlParameter("@Keywords", GetNullableText(txtKeywords.Text)),
                    CreateNullableStringParameter("@FileName", fileData == null ? null : fileData.FileName, 255),
                    CreateNullableStringParameter("@FileExtension", fileData == null ? null : fileData.FileExtension, 20),
                    CreateFileContentParameter("@FileContent", fileData)
                };

                Database.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Доклад добавлен. Статус будет изменен после рецензирования.");
                LoadReports();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления доклада: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsOrganizer())
                return;

            if (selectedReportId == 0)
            {
                MessageBox.Show("Выберите доклад.");
                return;
            }

            if (!ValidateFields())
                return;

            try
            {
                ReportFileData fileData = GetSelectedFileData();

                string query;

                if (IsAdmin())
                {
                    query = @"
                        UPDATE dbo.tb_reports
                        SET
                            topic = @Topic,
                            annotation = @Annotation,
                            keywords = @Keywords,
                            file_name = CASE WHEN @HasNewFile = 1 THEN @FileName ELSE file_name END,
                            file_extension = CASE WHEN @HasNewFile = 1 THEN @FileExtension ELSE file_extension END,
                            file_content = CASE WHEN @HasNewFile = 1 THEN @FileContent ELSE file_content END
                        WHERE id_report = @ReportId;
                    ";
                }
                else
                {
                    query = @"
                        UPDATE dbo.tb_reports
                        SET
                            topic = @Topic,
                            annotation = @Annotation,
                            keywords = @Keywords,
                            file_name = CASE WHEN @HasNewFile = 1 THEN @FileName ELSE file_name END,
                            file_extension = CASE WHEN @HasNewFile = 1 THEN @FileExtension ELSE file_extension END,
                            file_content = CASE WHEN @HasNewFile = 1 THEN @FileContent ELSE file_content END
                        WHERE id_report = @ReportId
                          AND id_author = @UserId
                          AND review_status = N'На рассмотрении';
                    ";
                }

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Topic", txtTopic.Text.Trim()),
                    new SqlParameter("@Annotation", GetNullableText(txtAnnotation.Text)),
                    new SqlParameter("@Keywords", GetNullableText(txtKeywords.Text)),
                    new SqlParameter("@HasNewFile", SqlDbType.Bit) { Value = fileData != null },
                    CreateNullableStringParameter("@FileName", fileData == null ? null : fileData.FileName, 255),
                    CreateNullableStringParameter("@FileExtension", fileData == null ? null : fileData.FileExtension, 20),
                    CreateFileContentParameter("@FileContent", fileData),
                    new SqlParameter("@ReportId", selectedReportId),
                    new SqlParameter("@UserId", currentUserId)
                };

                int rows = Database.ExecuteNonQuery(query, parameters);

                if (rows > 0)
                {
                    MessageBox.Show("Доклад изменен.");
                    LoadReports();
                }
                else
                {
                    MessageBox.Show("Редактировать можно только свой доклад со статусом «На рассмотрении».");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка изменения доклада: " + ex.Message);
            }
        }

        private bool CanParticipantEdit()
        {
            return !IsOrganizerOrAdmin();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!CanParticipantEdit())
                return;

            if (selectedReportId == 0)
            {
                MessageBox.Show("Выберите доклад.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Удалить выбранный доклад?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            try
            {
                string query = @"
                    DELETE FROM dbo.tb_reports
                    WHERE id_report = @ReportId
                      AND id_author = @UserId
                      AND review_status = N'На рассмотрении'
                      AND NOT EXISTS (
                          SELECT 1
                          FROM dbo.tb_reviews
                          WHERE id_report = @ReportId
                      )
                      AND NOT EXISTS (
                          SELECT 1
                          FROM dbo.tb_conference_program
                          WHERE id_report = @ReportId
                      );
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ReportId", selectedReportId),
                    new SqlParameter("@UserId", currentUserId)
                };

                int rows = Database.ExecuteNonQuery(query, parameters);

                if (rows > 0)
                {
                    MessageBox.Show("Доклад удален.");
                    LoadReports();
                }
                else
                {
                    MessageBox.Show("Удалить можно только свой доклад со статусом «На рассмотрении».");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления доклада: " + ex.Message);
            }
        }

        private void btnAddToSection_Click(object sender, EventArgs e)
        {
            if (!IsOrganizerOrAdmin())
                return;

            if (selectedReportId == 0)
            {
                MessageBox.Show("Выберите доклад.");
                return;
            }

            if (cmbSection.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите секцию.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Введите место проведения.");
                return;
            }

            try
            {
                if (ReportAlreadyInProgram(selectedReportId))
                {
                    UpdateReportInProgram();
                    MessageBox.Show("Расписание доклада изменено.");
                }
                else
                {
                    AddReportToProgram();
                    MessageBox.Show("Доклад добавлен в программу.");
                }

                int reportId = selectedReportId;

                LoadReports();
                SelectReportInGrid(reportId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения программы: " + ex.Message);
            }
        }

        private bool ReportAlreadyInProgram(int reportId)
        {
            string query = @"
                SELECT COUNT(*)
                FROM dbo.tb_conference_program
                WHERE id_report = @ReportId;
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@ReportId", reportId)
            };

            object result = Database.ExecuteScalar(query, parameters);

            return Convert.ToInt32(result) > 0;
        }

        private void AddReportToProgram()
        {
            string query = @"
                EXEC dbo.usp_add_conference_program
                    @id_report = @ReportId,
                    @presentation_date = @PresentationDate,
                    @presentation_time = @PresentationTime,
                    @location = @Location,
                    @id_section = @SectionId;
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@ReportId", selectedReportId),
                new SqlParameter("@PresentationDate", dtpDate.Value.Date),
                new SqlParameter("@PresentationTime", dtpTime.Value.TimeOfDay),
                new SqlParameter("@Location", txtLocation.Text.Trim()),
                new SqlParameter("@SectionId", Convert.ToInt32(cmbSection.SelectedValue))
            };

            Database.ExecuteNonQuery(query, parameters);
        }

        private void UpdateReportInProgram()
        {
            string query = @"
                UPDATE dbo.tb_conference_program
                SET
                    id_section = @SectionId,
                    presentation_date = @PresentationDate,
                    presentation_time = @PresentationTime,
                    location = @Location
                WHERE id_report = @ReportId;
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@SectionId", Convert.ToInt32(cmbSection.SelectedValue)),
                new SqlParameter("@PresentationDate", dtpDate.Value.Date),
                new SqlParameter("@PresentationTime", dtpTime.Value.TimeOfDay),
                new SqlParameter("@Location", txtLocation.Text.Trim()),
                new SqlParameter("@ReportId", selectedReportId)
            };

            Database.ExecuteNonQuery(query, parameters);
        }

        private void dgvReports_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            FillFieldsFromRow(dgvReports.Rows[e.RowIndex]);
        }

        private void FillFieldsFromRow(DataGridViewRow row)
        {
            selectedReportId = Convert.ToInt32(row.Cells[0].Value);

            txtTopic.Text = GetCellValue(row, "Тема");
            txtAnnotation.Text = GetCellValue(row, "Аннотация");
            txtKeywords.Text = GetCellValue(row, "Ключевые слова");
            txtFilePath.Text = GetCellValue(row, "Файл");

            string status = GetCellValue(row, "Статус");

            if (string.IsNullOrWhiteSpace(status))
                status = GetCellValue(row, "Статус рецензирования");

            txtReviewStatus.Text = status;

            if (IsOrganizerOrAdmin())
            {
                FillProgramFields(row);
            }

            selectedSourceFilePath = "";
        }

        private void FillProgramFields(DataGridViewRow row)
        {
            string sectionId = GetCellValue(row, "ID секции");

            if (!string.IsNullOrWhiteSpace(sectionId))
            {
                cmbSection.SelectedValue = Convert.ToInt32(sectionId);
            }
            else
            {
                cmbSection.SelectedIndex = -1;
            }

            string date = GetCellValue(row, "Дата");

            if (DateTime.TryParse(date, out DateTime parsedDate))
            {
                dtpDate.Value = parsedDate;
            }
            else
            {
                dtpDate.Value = DateTime.Today;
            }

            string time = GetCellValue(row, "Время");

            if (TimeSpan.TryParse(time, out TimeSpan parsedTime))
            {
                dtpTime.Value = DateTime.Today.Add(parsedTime);
            }
            else
            {
                dtpTime.Value = DateTime.Now;
            }

            txtLocation.Text = GetCellValue(row, "Место");
        }

        private void SelectReportInGrid(int reportId)
        {
            foreach (DataGridViewRow row in dgvReports.Rows)
            {
                if (Convert.ToInt32(row.Cells[0].Value) == reportId)
                {
                    row.Selected = true;
                    dgvReports.CurrentCell = row.Cells[1];

                    if (row.Index >= 0)
                    {
                        dgvReports.FirstDisplayedScrollingRowIndex = row.Index;
                    }

                    FillFieldsFromRow(row);
                    return;
                }
            }
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            if (IsOrganizer())
                return;

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Выберите PDF-файл доклада";
                dialog.Filter = "PDF files (*.pdf)|*.pdf";
                dialog.Multiselect = false;
                dialog.CheckFileExists = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    selectedSourceFilePath = dialog.FileName;
                    txtFilePath.Text = Path.GetFileName(dialog.FileName);
                }
            }
        }

        private ReportFileData GetSelectedFileData()
        {
            if (string.IsNullOrWhiteSpace(selectedSourceFilePath))
                return null;

            if (!File.Exists(selectedSourceFilePath))
            {
                throw new FileNotFoundException("Выбранный файл не найден.");
            }

            string extension = Path.GetExtension(selectedSourceFilePath);

            if (!string.Equals(extension, ".pdf", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Можно выбрать только PDF-файл.");
            }

            return new ReportFileData
            {
                FileName = Path.GetFileName(selectedSourceFilePath),
                FileExtension = extension,
                FileContent = File.ReadAllBytes(selectedSourceFilePath)
            };
        }

        private SqlParameter CreateNullableStringParameter(string name, string value, int size)
        {
            SqlParameter parameter = new SqlParameter(name, SqlDbType.NVarChar, size);

            if (string.IsNullOrWhiteSpace(value))
                parameter.Value = DBNull.Value;
            else
                parameter.Value = value.Trim();

            return parameter;
        }

        private SqlParameter CreateFileContentParameter(string name, ReportFileData fileData)
        {
            SqlParameter parameter = new SqlParameter(name, SqlDbType.VarBinary, -1);

            if (fileData == null || fileData.FileContent == null || fileData.FileContent.Length == 0)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = fileData.FileContent;

            return parameter;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtTopic.Text))
            {
                MessageBox.Show("Введите тему доклада.");
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

        private string GetCellValue(DataGridViewRow row, string columnName)
        {
            int columnIndex = -1;

            foreach (DataGridViewColumn column in dgvReports.Columns)
            {
                if (column.Name == columnName ||
                    column.HeaderText == columnName ||
                    column.DataPropertyName == columnName)
                {
                    columnIndex = column.Index;
                    break;
                }
            }

            if (columnIndex == -1)
                return "";

            object value = row.Cells[columnIndex].Value;

            if (value == null || value == DBNull.Value)
                return "";

            return value.ToString();
        }

        private void ClearFields()
        {
            selectedReportId = 0;
            selectedSourceFilePath = "";

            txtTopic.Clear();
            txtAnnotation.Clear();
            txtKeywords.Clear();
            txtFilePath.Clear();

            txtReviewStatus.Text = "Статус изменяется после рецензирования";

            if (cmbSection.Visible && cmbSection.Items.Count > 0)
            {
                cmbSection.SelectedIndex = -1;
            }

            dtpDate.Value = DateTime.Today;
            dtpTime.Value = DateTime.Now;
            txtLocation.Clear();

            if (dgvReports.Rows.Count > 0)
            {
                dgvReports.ClearSelection();
            }
        }
    }
}