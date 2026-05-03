using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ConferenceApp
{
    public partial class ProgramForm : Form
    {
        private string currentUserRole;

        public ProgramForm(string role)
        {
            InitializeComponent();

            currentUserRole = role;

            ConfigureAccessByRole();
            CenterTitle();

            LoadProgramCards();
            LoadAcceptedReports();
            LoadSections();
        }

        private void LoadProgramCards()
        {
            try
            {
                flowProgram.Controls.Clear();

                string query = @"
                    SELECT
                        cp.presentation_date,
                        LEFT(CONVERT(NVARCHAR(8), cp.presentation_time, 108), 5) AS presentation_time,
                        s.section_name,
                        r.topic,
                        p.last_name,
                        p.first_name,
                        p.middle_name,
                        cp.location
                    FROM dbo.tb_conference_program AS cp
                    INNER JOIN dbo.tb_reports AS r
                        ON cp.id_report = r.id_report
                    INNER JOIN dbo.tb_sections AS s
                        ON cp.id_section = s.id_section
                    INNER JOIN dbo.tb_participants AS p
                        ON r.id_author = p.id_participant
                    ORDER BY cp.presentation_date, cp.presentation_time;
                ";

                DataTable table = Database.ExecuteSelect(query);

                if (table.Rows.Count == 0)
                {
                    Label emptyLabel = new Label();
                    emptyLabel.Text = "Программа конференции пока не сформирована.";
                    emptyLabel.AutoSize = true;
                    emptyLabel.Font = new Font("Microsoft Sans Serif", 11F);
                    emptyLabel.ForeColor = Color.FromArgb(32, 58, 95);
                    emptyLabel.Margin = new Padding(10);
                    flowProgram.Controls.Add(emptyLabel);
                    return;
                }

                DateTime? currentDate = null;

                foreach (DataRow row in table.Rows)
                {
                    DateTime date = Convert.ToDateTime(row["presentation_date"]);

                    if (currentDate == null || currentDate.Value.Date != date.Date)
                    {
                        currentDate = date.Date;
                        AddDateHeader(date);
                    }

                    AddProgramCard(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки программы: " + ex.Message);
            }
        }

        private void AddDateHeader(DateTime date)
        {
            Label lblDate = new Label();
            lblDate.Text = date.ToString("dd.MM.yyyy");
            lblDate.AutoSize = false;
            lblDate.Width = flowProgram.ClientSize.Width - 30;
            lblDate.Height = 30;
            lblDate.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblDate.ForeColor = Color.FromArgb(32, 58, 95);
            lblDate.Margin = new Padding(5, 10, 5, 5);

            flowProgram.Controls.Add(lblDate);
        }

        private void AddProgramCard(DataRow row)
        {
            string time = row["presentation_time"].ToString();
            string section = row["section_name"].ToString();
            string topic = row["topic"].ToString();
            string location = row["location"].ToString();

            string firstName = row["first_name"].ToString();
            string middleName = row["middle_name"] == DBNull.Value ? "" : row["middle_name"].ToString();
            string lastName = row["last_name"].ToString();

            string author = GetShortName(firstName, middleName, lastName);

            Panel card = new Panel();
            card.Width = flowProgram.ClientSize.Width - 35;
            card.Height = 105;
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(5, 3, 5, 10);

            Label lblTime = new Label();
            lblTime.Text = time;
            lblTime.Location = new Point(15, 15);
            lblTime.Size = new Size(70, 25);
            lblTime.Font = new Font("Microsoft Sans Serif", 13F, FontStyle.Bold);
            lblTime.ForeColor = Color.FromArgb(32, 58, 95);

            Label lblSection = new Label();
            lblSection.Text = section;
            lblSection.Location = new Point(100, 12);
            lblSection.Size = new Size(760, 24);
            lblSection.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblSection.ForeColor = Color.FromArgb(32, 58, 95);

            Label lblTopic = new Label();
            lblTopic.Text = topic;
            lblTopic.Location = new Point(100, 40);
            lblTopic.Size = new Size(760, 22);
            lblTopic.Font = new Font("Microsoft Sans Serif", 9F);
            lblTopic.ForeColor = Color.Black;

            Label lblInfo = new Label();
            lblInfo.Text = "Автор: " + author + "    Место: " + location;
            lblInfo.Location = new Point(100, 68);
            lblInfo.Size = new Size(760, 22);
            lblInfo.Font = new Font("Microsoft Sans Serif", 9F);
            lblInfo.ForeColor = Color.DimGray;

            card.Controls.Add(lblTime);
            card.Controls.Add(lblSection);
            card.Controls.Add(lblTopic);
            card.Controls.Add(lblInfo);

            flowProgram.Controls.Add(card);
        }

        private string GetShortName(string firstName, string middleName, string lastName)
        {
            string result = "";

            if (!string.IsNullOrWhiteSpace(firstName))
                result += firstName.Substring(0, 1) + ".";

            if (!string.IsNullOrWhiteSpace(middleName))
                result += middleName.Substring(0, 1) + ".";

            result += " " + lastName;

            return result.Trim();
        }

        private void LoadAcceptedReports()
        {
            try
            {
                string query = @"
                    SELECT
                        r.id_report,
                        r.topic
                    FROM dbo.tb_reports AS r
                    WHERE r.review_status = N'Принят'
                      AND NOT EXISTS (
                          SELECT 1
                          FROM dbo.tb_conference_program AS cp
                          WHERE cp.id_report = r.id_report
                      )
                    ORDER BY r.topic;
                ";

                DataTable table = Database.ExecuteSelect(query);

                cmbReport.DataSource = table;
                cmbReport.DisplayMember = "topic";
                cmbReport.ValueMember = "id_report";
                cmbReport.SelectedIndex = -1;
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

        private void ConfigureAccessByRole()
        {
            bool canManage = currentUserRole == "Организатор" || currentUserRole == "Администратор";

            lblReport.Visible = canManage;
            lblSection.Visible = canManage;
            lblDate.Visible = canManage;
            lblTime.Visible = canManage;
            lblLocation.Visible = canManage;

            cmbReport.Visible = canManage;
            cmbSection.Visible = canManage;
            dtpDate.Visible = canManage;
            dtpTime.Visible = canManage;
            txtLocation.Visible = canManage;
            btnAdd.Visible = canManage;
        }

        private void CenterTitle()
        {
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
                return;

            try
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
                    new SqlParameter("@ReportId", Convert.ToInt32(cmbReport.SelectedValue)),
                    new SqlParameter("@PresentationDate", dtpDate.Value.Date),
                    new SqlParameter("@PresentationTime", dtpTime.Value.TimeOfDay),
                    new SqlParameter("@Location", txtLocation.Text.Trim()),
                    new SqlParameter("@SectionId", Convert.ToInt32(cmbSection.SelectedValue))
                };

                Database.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Доклад добавлен в программу.");

                LoadProgramCards();
                LoadAcceptedReports();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления в программу: " + ex.Message);
            }
        }

        private bool ValidateFields()
        {
            if (cmbReport.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите доклад.");
                return false;
            }

            if (cmbSection.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите секцию.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Введите место проведения.");
                return false;
            }

            return true;
        }

        private void ClearFields()
        {
            cmbReport.SelectedIndex = -1;
            cmbSection.SelectedIndex = -1;
            txtLocation.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}