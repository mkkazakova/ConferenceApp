using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ConferenceApp
{
    public partial class ReportsForm : Form
    {
        private int currentUserId;
        private int selectedReportId = 0;

        public ReportsForm(int userId)
        {
            InitializeComponent();

            currentUserId = userId;

            SetupGridStyle();
            CenterTitle();

            dgvReports.MouseDown += dgvReports_MouseDown;

            LoadReports();
        }

        private void LoadReports()
        {
            try
            {
                string query = @"
                    SELECT
                        id_report AS [ID],
                        topic AS [Тема],
                        annotation AS [Аннотация],
                        keywords AS [Ключевые слова],
                        review_status AS [Статус рецензирования],
                        file_path AS [Файл]
                    FROM dbo.tb_reports
                    WHERE id_author = @UserId
                    ORDER BY id_report DESC;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@UserId", currentUserId)
                };

                DataTable table = Database.ExecuteSelect(query, parameters);

                dgvReports.DataSource = null;
                dgvReports.AutoGenerateColumns = true;
                dgvReports.DataSource = table;

                if (dgvReports.Columns.Count > 0)
                {
                    dgvReports.Columns[0].Visible = false;
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
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
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
            if (!ValidateFields())
                return;

            try
            {
                string query = @"
                    EXEC dbo.usp_add_report
                        @topic = @Topic,
                        @id_author = @AuthorId,
                        @annotation = @Annotation,
                        @keywords = @Keywords,
                        @file_path = @FilePath;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Topic", txtTopic.Text.Trim()),
                    new SqlParameter("@AuthorId", currentUserId),
                    new SqlParameter("@Annotation", GetNullableText(txtAnnotation.Text)),
                    new SqlParameter("@Keywords", GetNullableText(txtKeywords.Text)),
                    new SqlParameter("@FilePath", GetNullableText(txtFilePath.Text))
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
            if (selectedReportId == 0)
            {
                MessageBox.Show("Выберите доклад.");
                return;
            }

            if (!ValidateFields())
                return;

            try
            {
                string query = @"
                    UPDATE dbo.tb_reports
                    SET
                        topic = @Topic,
                        annotation = @Annotation,
                        keywords = @Keywords,
                        file_path = @FilePath
                    WHERE id_report = @ReportId
                      AND id_author = @UserId
                      AND review_status = N'На рассмотрении';
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Topic", txtTopic.Text.Trim()),
                    new SqlParameter("@Annotation", GetNullableText(txtAnnotation.Text)),
                    new SqlParameter("@Keywords", GetNullableText(txtKeywords.Text)),
                    new SqlParameter("@FilePath", GetNullableText(txtFilePath.Text)),
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
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

        private void dgvReports_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvReports.Rows[e.RowIndex];

            selectedReportId = Convert.ToInt32(row.Cells[0].Value);

            txtTopic.Text = GetCellValue(row, 1);
            txtAnnotation.Text = GetCellValue(row, 2);
            txtKeywords.Text = GetCellValue(row, 3);
            txtReviewStatus.Text = GetCellValue(row, 4);
            txtFilePath.Text = GetCellValue(row, 5);
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = dialog.FileName;
            }
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

        private string GetCellValue(DataGridViewRow row, int index)
        {
            if (row.Cells[index].Value == null || row.Cells[index].Value == DBNull.Value)
                return "";

            return row.Cells[index].Value.ToString();
        }

        private void ClearFields()
        {
            selectedReportId = 0;

            txtTopic.Clear();
            txtAnnotation.Clear();
            txtKeywords.Clear();
            txtFilePath.Clear();

            txtReviewStatus.Text = "Статус изменяется после рецензирования";

            if (dgvReports.Rows.Count > 0)
            {
                dgvReports.ClearSelection();
            }
        }
    }
}