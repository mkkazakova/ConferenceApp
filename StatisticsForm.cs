using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ConferenceApp
{
    public partial class StatisticsForm : Form
    {
        public StatisticsForm()
        {
            InitializeComponent();

            SetupGridStyle();
            LoadSummary();
            LoadParticipantsByRole();
        }

        private void SetupGridStyle()
        {
            dgvStatistics.BackgroundColor = Color.White;
            dgvStatistics.BorderStyle = BorderStyle.FixedSingle;
            dgvStatistics.RowHeadersVisible = false;
            dgvStatistics.AllowUserToAddRows = false;
            dgvStatistics.AllowUserToDeleteRows = false;
            dgvStatistics.AllowUserToResizeRows = false;
            dgvStatistics.ReadOnly = true;
            dgvStatistics.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStatistics.MultiSelect = false;
            dgvStatistics.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStatistics.GridColor = Color.LightGray;
            dgvStatistics.EnableHeadersVisualStyles = false;

            dgvStatistics.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 240, 250);
            dgvStatistics.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvStatistics.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvStatistics.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvStatistics.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);

            dgvStatistics.DefaultCellStyle.BackColor = Color.White;
            dgvStatistics.DefaultCellStyle.ForeColor = Color.Black;
            dgvStatistics.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 230, 250);
            dgvStatistics.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvStatistics.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F);

            dgvStatistics.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 252, 255);
        }

        private void LoadSummary()
        {
            try
            {
                string query = @"
                    SELECT
                        (SELECT COUNT(*) FROM dbo.tb_participants) AS participants_count,
                        (SELECT COUNT(*) FROM dbo.tb_reports) AS reports_count,
                        (SELECT COUNT(*) FROM dbo.tb_sections) AS sections_count,
                        (SELECT COUNT(*) FROM dbo.tb_reviews) AS reviews_count,
                        (SELECT COUNT(*) FROM dbo.tb_conference_program) AS program_count,
                        (SELECT COUNT(*) FROM dbo.tb_section_visits) AS visits_count;
                ";

                DataTable table = Database.ExecuteSelect(query, new SqlParameter[0]);

                if (table.Rows.Count == 0)
                    return;

                DataRow row = table.Rows[0];

                lblParticipantsValue.Text = row["participants_count"].ToString();
                lblReportsValue.Text = row["reports_count"].ToString();
                lblSectionsValue.Text = row["sections_count"].ToString();
                lblReviewsValue.Text = row["reviews_count"].ToString();
                lblProgramValue.Text = row["program_count"].ToString();
                lblVisitsValue.Text = row["visits_count"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки общей статистики: " + ex.Message);
            }
        }

        private void LoadParticipantsByRole()
        {
            try
            {
                lblTableTitle.Text = "Количество участников по ролям";

                string query = @"
                    SELECT
                        user_role AS [Роль],
                        COUNT(*) AS [Количество]
                    FROM dbo.tb_participants
                    GROUP BY user_role
                    ORDER BY COUNT(*) DESC;
                ";

                LoadTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки статистики по ролям: " + ex.Message);
            }
        }

        private void LoadReportsByStatus()
        {
            try
            {
                lblTableTitle.Text = "Количество докладов по статусам";

                string query = @"
                    SELECT
                        review_status AS [Статус],
                        COUNT(*) AS [Количество]
                    FROM dbo.tb_reports
                    GROUP BY review_status
                    ORDER BY COUNT(*) DESC;
                ";

                LoadTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки статистики по докладам: " + ex.Message);
            }
        }

        private void LoadSectionPopularity()
        {
            try
            {
                lblTableTitle.Text = "Популярность секций";

                string query = @"
                    SELECT
                        s.section_name AS [Секция],
                        COUNT(sv.id_visit) AS [Количество записей]
                    FROM dbo.tb_sections AS s
                    LEFT JOIN dbo.tb_section_visits AS sv
                        ON s.id_section = sv.id_section
                    GROUP BY s.section_name
                    ORDER BY COUNT(sv.id_visit) DESC, s.section_name;
                ";

                LoadTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки популярности секций: " + ex.Message);
            }
        }

        private void LoadReviewsByReviewer()
        {
            try
            {
                lblTableTitle.Text = "Количество рецензий по рецензентам";

                string query = @"
                    SELECT
                        p.last_name + N' ' + p.first_name + N' ' + ISNULL(p.middle_name, N'') AS [Рецензент],
                        COUNT(rv.id_review) AS [Количество рецензий],
                        CAST(AVG((rv.novelty_score + rv.relevance_score + rv.quality_score) / 3.0) AS DECIMAL(5,2)) AS [Средняя оценка]
                    FROM dbo.tb_reviews AS rv
                    INNER JOIN dbo.tb_participants AS p
                        ON rv.id_reviewer = p.id_participant
                    GROUP BY
                        p.last_name,
                        p.first_name,
                        p.middle_name
                    ORDER BY COUNT(rv.id_review) DESC;
                ";

                LoadTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки статистики по рецензентам: " + ex.Message);
            }
        }

        private void LoadAverageScoresByReport()
        {
            try
            {
                lblTableTitle.Text = "Средние оценки докладов";

                string query = @"
                    SELECT
                        r.topic AS [Доклад],
                        COUNT(rv.id_review) AS [Количество рецензий],
                        CAST(AVG(CAST(rv.novelty_score AS FLOAT)) AS DECIMAL(5,2)) AS [Новизна],
                        CAST(AVG(CAST(rv.relevance_score AS FLOAT)) AS DECIMAL(5,2)) AS [Актуальность],
                        CAST(AVG(CAST(rv.quality_score AS FLOAT)) AS DECIMAL(5,2)) AS [Качество],
                        CAST(AVG((rv.novelty_score + rv.relevance_score + rv.quality_score) / 3.0) AS DECIMAL(5,2)) AS [Средняя оценка]
                    FROM dbo.tb_reports AS r
                    INNER JOIN dbo.tb_reviews AS rv
                        ON r.id_report = rv.id_report
                    GROUP BY r.topic
                    ORDER BY [Средняя оценка] DESC;
                ";

                LoadTable(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки средних оценок: " + ex.Message);
            }
        }

        private void LoadTable(string query)
        {
            DataTable table = Database.ExecuteSelect(query, new SqlParameter[0]);

            dgvStatistics.DataSource = null;
            dgvStatistics.AutoGenerateColumns = true;
            dgvStatistics.DataSource = table;

            foreach (DataGridViewColumn column in dgvStatistics.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgvStatistics.ClearSelection();
        }

        private void btnExportReport_Click(object sender, EventArgs e)
        {
            if (dgvStatistics.DataSource == null || dgvStatistics.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для формирования отчета.");
                return;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Сохранение отчета";
                dialog.Filter = "Текстовый отчет (*.txt)|*.txt|CSV файл (*.csv)|*.csv";
                dialog.FileName = "Отчет_конференции_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".txt";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    if (Path.GetExtension(dialog.FileName).ToLower() == ".csv")
                        ExportToCsv(dialog.FileName);
                    else
                        ExportToTxt(dialog.FileName);

                    MessageBox.Show("Отчет успешно сформирован.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка формирования отчета: " + ex.Message);
                }
            }
        }

        private void ExportToTxt(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, new UTF8Encoding(true)))
            {
                writer.WriteLine("ОТЧЕТ ПО ИНФОРМАЦИОННОЙ СИСТЕМЕ «НАУЧНАЯ КОНФЕРЕНЦИЯ»");
                writer.WriteLine("Дата формирования: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                writer.WriteLine();

                writer.WriteLine("Общая статистика:");
                writer.WriteLine("Участники: " + lblParticipantsValue.Text);
                writer.WriteLine("Доклады: " + lblReportsValue.Text);
                writer.WriteLine("Секции: " + lblSectionsValue.Text);
                writer.WriteLine("Рецензии: " + lblReviewsValue.Text);
                writer.WriteLine("Программа: " + lblProgramValue.Text);
                writer.WriteLine("Записи на секции: " + lblVisitsValue.Text);
                writer.WriteLine();

                writer.WriteLine(lblTableTitle.Text);
                writer.WriteLine(new string('-', 80));

                foreach (DataGridViewColumn column in dgvStatistics.Columns)
                    writer.Write(column.HeaderText + "\t");

                writer.WriteLine();

                foreach (DataGridViewRow row in dgvStatistics.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    foreach (DataGridViewCell cell in row.Cells)
                        writer.Write(Convert.ToString(cell.Value) + "\t");

                    writer.WriteLine();
                }
            }
        }

        private void ExportToCsv(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, new UTF8Encoding(true)))
            {
                foreach (DataGridViewColumn column in dgvStatistics.Columns)
                    writer.Write(EscapeCsv(column.HeaderText) + ";");

                writer.WriteLine();

                foreach (DataGridViewRow row in dgvStatistics.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    foreach (DataGridViewCell cell in row.Cells)
                        writer.Write(EscapeCsv(Convert.ToString(cell.Value)) + ";");

                    writer.WriteLine();
                }
            }
        }

        private string EscapeCsv(string value)
        {
            if (value == null)
                return "";

            value = value.Replace("\"", "\"\"");

            if (value.Contains(";") || value.Contains("\"") || value.Contains("\n"))
                return "\"" + value + "\"";

            return value;
        }

        private void btnParticipantsByRole_Click(object sender, EventArgs e)
        {
            LoadParticipantsByRole();
        }

        private void btnReportsByStatus_Click(object sender, EventArgs e)
        {
            LoadReportsByStatus();
        }

        private void btnSectionPopularity_Click(object sender, EventArgs e)
        {
            LoadSectionPopularity();
        }

        private void btnReviewsByReviewer_Click(object sender, EventArgs e)
        {
            LoadReviewsByReviewer();
        }

        private void btnAverageScores_Click(object sender, EventArgs e)
        {
            LoadAverageScoresByReport();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSummary();
            LoadParticipantsByRole();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}