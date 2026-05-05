using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ConferenceApp
{
    public partial class ProgramForm : Form
    {
        private int currentUserId;
        private string currentUserRole;

        private int selectedPresentationId = 0;
        private int selectedReportId = 0;

        public ProgramForm(int userId, string role)
        {
            InitializeComponent();

            currentUserId = userId;
            currentUserRole = role;

            ConfigureAccessByRole();
            CenterTitle();

            LoadProgramCards();
        }

        private bool IsOrganizerOrAdmin()
        {
            return currentUserRole == "Организатор" || currentUserRole == "Администратор";
        }

        private void ConfigureAccessByRole()
        {
            bool canManage = IsOrganizerOrAdmin();

            btnAdd.Visible = canManage;
            btnUpdate.Visible = canManage;
            btnDelete.Visible = canManage;

            btnAdd.Text = "Добавить доклад";
            btnUpdate.Text = "Изменить доклад";
            btnDelete.Text = "Удалить из программы";
        }

        private void LoadProgramCards()
        {
            try
            {
                flowProgram.Controls.Clear();

                selectedPresentationId = 0;
                selectedReportId = 0;

                string query = @"
                    SELECT
                        cp.id_presentation,
                        r.id_report,
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
            int presentationId = Convert.ToInt32(row["id_presentation"]);
            int reportId = Convert.ToInt32(row["id_report"]);

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
            card.Cursor = Cursors.Hand;
            card.Tag = new ProgramCardData(presentationId, reportId);

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

            card.Click += ProgramCard_Click;
            lblTime.Click += ProgramCard_Click;
            lblSection.Click += ProgramCard_Click;
            lblTopic.Click += ProgramCard_Click;
            lblInfo.Click += ProgramCard_Click;

            flowProgram.Controls.Add(card);
        }

        private void ProgramCard_Click(object sender, EventArgs e)
        {
            Control clickedControl = sender as Control;

            Panel card = clickedControl as Panel;

            if (card == null)
            {
                card = clickedControl.Parent as Panel;
            }

            if (card == null || card.Tag == null)
                return;

            ProgramCardData data = card.Tag as ProgramCardData;

            selectedPresentationId = data.PresentationId;
            selectedReportId = data.ReportId;

            foreach (Control control in flowProgram.Controls)
            {
                if (control is Panel panel)
                {
                    panel.BackColor = Color.White;
                }
            }

            card.BackColor = Color.FromArgb(210, 230, 250);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ReportsForm reportsForm = new ReportsForm(currentUserId, currentUserRole);
            reportsForm.ShowDialog();

            LoadProgramCards();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedReportId == 0)
            {
                MessageBox.Show("Выберите доклад в программе.");
                return;
            }

            ReportsForm reportsForm = new ReportsForm(currentUserId, currentUserRole, selectedReportId);
            reportsForm.ShowDialog();

            LoadProgramCards();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedPresentationId == 0)
            {
                MessageBox.Show("Выберите доклад в программе.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Удалить выбранный доклад из программы конференции?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            try
            {
                string query = @"
                    DELETE FROM dbo.tb_conference_program
                    WHERE id_presentation = @PresentationId;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@PresentationId", selectedPresentationId)
                };

                Database.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Доклад удален из программы.");

                LoadProgramCards();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления из программы: " + ex.Message);
            }
        }

        private void CenterTitle()
        {
            lblTitle.Left = (ClientSize.Width - lblTitle.Width) / 2;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private class ProgramCardData
        {
            public int PresentationId { get; private set; }
            public int ReportId { get; private set; }

            public ProgramCardData(int presentationId, int reportId)
            {
                PresentationId = presentationId;
                ReportId = reportId;
            }
        }
    }
}