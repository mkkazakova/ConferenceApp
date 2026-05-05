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
        private ProgramCardData selectedCardData = null;

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
                selectedCardData = null;

                string query = @"
                    SELECT
                        cp.id_presentation,
                        r.id_report,
                        s.id_section,
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
            int sectionId = Convert.ToInt32(row["id_section"]);

            DateTime date = Convert.ToDateTime(row["presentation_date"]);
            TimeSpan timeValue = TimeSpan.Parse(row["presentation_time"].ToString());

            string time = row["presentation_time"].ToString();
            string section = row["section_name"].ToString();
            string topic = row["topic"].ToString();
            string location = row["location"].ToString();

            string firstName = row["first_name"].ToString();
            string middleName = row["middle_name"] == DBNull.Value ? "" : row["middle_name"].ToString();
            string lastName = row["last_name"].ToString();

            string author = GetShortName(firstName, middleName, lastName);

            ProgramCardData data = new ProgramCardData(
                presentationId,
                reportId,
                sectionId,
                date,
                timeValue,
                location,
                topic
            );

            Panel card = new Panel();
            card.Width = flowProgram.ClientSize.Width - 35;
            card.Height = 105;
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(5, 3, 5, 10);
            card.Cursor = Cursors.Hand;
            card.Tag = data;

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
            lblSection.AutoEllipsis = true;

            Label lblTopic = new Label();
            lblTopic.Text = topic;
            lblTopic.Location = new Point(100, 40);
            lblTopic.Size = new Size(760, 22);
            lblTopic.Font = new Font("Microsoft Sans Serif", 9F);
            lblTopic.ForeColor = Color.Black;
            lblTopic.AutoEllipsis = true;

            Label lblInfo = new Label();
            lblInfo.Text = "Автор: " + author + "    Место: " + location;
            lblInfo.Location = new Point(100, 68);
            lblInfo.Size = new Size(760, 22);
            lblInfo.Font = new Font("Microsoft Sans Serif", 9F);
            lblInfo.ForeColor = Color.DimGray;
            lblInfo.AutoEllipsis = true;

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
                card = clickedControl.Parent as Panel;

            if (card == null || card.Tag == null)
                return;

            ProgramCardData data = card.Tag as ProgramCardData;

            selectedPresentationId = data.PresentationId;
            selectedReportId = data.ReportId;
            selectedCardData = data;

            foreach (Control control in flowProgram.Controls)
            {
                if (control is Panel panel)
                    panel.BackColor = Color.White;
            }

            card.BackColor = Color.FromArgb(210, 230, 250);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProgramEditDialog dialog = new ProgramEditDialog(false, null);

            if (dialog.ShowDialog() != DialogResult.OK)
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
                    new SqlParameter("@ReportId", dialog.ReportId),
                    new SqlParameter("@PresentationDate", dialog.PresentationDate),
                    new SqlParameter("@PresentationTime", dialog.PresentationTime),
                    new SqlParameter("@Location", dialog.LocationText),
                    new SqlParameter("@SectionId", dialog.SectionId)
                };

                Database.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Доклад добавлен в программу.");

                LoadProgramCards();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления в программу: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedCardData == null)
            {
                MessageBox.Show("Выберите доклад в программе.");
                return;
            }

            ProgramEditDialog dialog = new ProgramEditDialog(true, selectedCardData);

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                string query = @"
                    IF EXISTS (
                        SELECT 1
                        FROM dbo.tb_conference_program
                        WHERE id_section = @SectionId
                          AND presentation_date = @PresentationDate
                          AND presentation_time = @PresentationTime
                          AND id_presentation <> @PresentationId
                    )
                    BEGIN
                        THROW 52003, N'В выбранной секции уже есть доклад на это время.', 1;
                    END;

                    UPDATE dbo.tb_conference_program
                    SET
                        id_section = @SectionId,
                        presentation_date = @PresentationDate,
                        presentation_time = @PresentationTime,
                        location = @Location
                    WHERE id_presentation = @PresentationId;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@PresentationId", selectedPresentationId),
                    new SqlParameter("@SectionId", dialog.SectionId),
                    new SqlParameter("@PresentationDate", dialog.PresentationDate),
                    new SqlParameter("@PresentationTime", dialog.PresentationTime),
                    new SqlParameter("@Location", dialog.LocationText)
                };

                Database.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Запись программы изменена.");

                LoadProgramCards();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка изменения программы: " + ex.Message);
            }
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
            public int SectionId { get; private set; }
            public DateTime PresentationDate { get; private set; }
            public TimeSpan PresentationTime { get; private set; }
            public string LocationText { get; private set; }
            public string ReportTopic { get; private set; }

            public ProgramCardData(
                int presentationId,
                int reportId,
                int sectionId,
                DateTime presentationDate,
                TimeSpan presentationTime,
                string locationText,
                string reportTopic)
            {
                PresentationId = presentationId;
                ReportId = reportId;
                SectionId = sectionId;
                PresentationDate = presentationDate;
                PresentationTime = presentationTime;
                LocationText = locationText;
                ReportTopic = reportTopic;
            }
        }

        private class ProgramEditDialog : Form
        {
            private ComboBox cmbReport;
            private Label lblReportText;
            private ComboBox cmbSection;
            private DateTimePicker dtpDate;
            private ComboBox cmbTime;
            private TextBox txtLocation;
            private Button btnSave;
            private Button btnCancel;

            private bool editMode;
            private ProgramCardData cardData;

            public int ReportId { get; private set; }
            public int SectionId { get; private set; }
            public DateTime PresentationDate { get; private set; }
            public TimeSpan PresentationTime { get; private set; }
            public string LocationText { get; private set; }

            public ProgramEditDialog(bool editMode, ProgramCardData cardData)
            {
                this.editMode = editMode;
                this.cardData = cardData;

                InitializeDialog();

                LoadSections();
                LoadTimeValues();

                if (editMode && cardData != null)
                {
                    Text = "Редактирование доклада в программе";

                    ReportId = cardData.ReportId;
                    lblReportText.Text = cardData.ReportTopic;

                    cmbReport.Visible = false;
                    lblReportText.Visible = true;

                    cmbSection.SelectedValue = cardData.SectionId;
                    dtpDate.Value = cardData.PresentationDate;
                    cmbTime.SelectedItem = cardData.PresentationTime.ToString(@"hh\:mm");
                    txtLocation.Text = cardData.LocationText;
                }
                else
                {
                    Text = "Добавление доклада в программу";

                    cmbReport.Visible = true;
                    lblReportText.Visible = false;

                    LoadReportsForAdd();

                    dtpDate.Value = DateTime.Today;
                    cmbTime.SelectedItem = "10:00";
                }
            }

            private void InitializeDialog()
            {
                Width = 560;
                Height = 330;
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                BackColor = Color.FromArgb(240, 247, 255);

                AddLabel("Доклад:", 25, 35);

                cmbReport = new ComboBox();
                cmbReport.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbReport.Location = new Point(115, 32);
                cmbReport.Size = new Size(400, 24);
                Controls.Add(cmbReport);

                lblReportText = new Label();
                lblReportText.Location = new Point(115, 32);
                lblReportText.Size = new Size(400, 45);
                lblReportText.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
                lblReportText.ForeColor = Color.FromArgb(32, 58, 95);
                lblReportText.AutoEllipsis = true;
                Controls.Add(lblReportText);

                AddLabel("Секция:", 25, 85);

                cmbSection = new ComboBox();
                cmbSection.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbSection.Location = new Point(115, 82);
                cmbSection.Size = new Size(400, 24);
                Controls.Add(cmbSection);

                AddLabel("Дата:", 25, 135);

                dtpDate = new DateTimePicker();
                dtpDate.Format = DateTimePickerFormat.Short;
                dtpDate.Location = new Point(115, 132);
                dtpDate.Size = new Size(145, 22);
                Controls.Add(dtpDate);

                AddLabel("Время:", 285, 135);

                cmbTime = new ComboBox();
                cmbTime.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbTime.Location = new Point(370, 132);
                cmbTime.Size = new Size(145, 24);
                Controls.Add(cmbTime);

                AddLabel("Место:", 25, 185);

                txtLocation = new TextBox();
                txtLocation.Location = new Point(115, 182);
                txtLocation.Size = new Size(400, 22);
                Controls.Add(txtLocation);

                btnSave = new Button();
                btnSave.Text = "Сохранить";
                btnSave.Location = new Point(285, 245);
                btnSave.Size = new Size(110, 35);
                btnSave.Click += btnSave_Click;
                Controls.Add(btnSave);

                btnCancel = new Button();
                btnCancel.Text = "Отмена";
                btnCancel.Location = new Point(405, 245);
                btnCancel.Size = new Size(110, 35);
                btnCancel.DialogResult = DialogResult.Cancel;
                Controls.Add(btnCancel);
            }

            private void LoadReportsForAdd()
            {
                string query = @"
                    SELECT
                        r.id_report,
                        r.topic + N' — ' +
                        LTRIM(RTRIM(p.last_name + N' ' + p.first_name + N' ' + ISNULL(p.middle_name, N''))) AS report_text
                    FROM dbo.tb_reports AS r
                    INNER JOIN dbo.tb_participants AS p
                        ON r.id_author = p.id_participant
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
                cmbReport.DisplayMember = "report_text";
                cmbReport.ValueMember = "id_report";
                cmbReport.SelectedIndex = -1;
            }

            private void LoadSections()
            {
                string query = @"
                    SELECT id_section, section_name
                    FROM dbo.tb_sections
                    ORDER BY section_name;
                ";

                DataTable table = Database.ExecuteSelect(query);

                cmbSection.DataSource = table;
                cmbSection.DisplayMember = "section_name";
                cmbSection.ValueMember = "id_section";
                cmbSection.SelectedIndex = -1;
            }

            private void LoadTimeValues()
            {
                cmbTime.Items.Clear();

                for (int hour = 0; hour < 24; hour++)
                {
                    cmbTime.Items.Add(hour.ToString("00") + ":00");
                    cmbTime.Items.Add(hour.ToString("00") + ":30");
                }
            }

            private Label AddLabel(string text, int x, int y)
            {
                Label label = new Label();
                label.Text = text;
                label.Location = new Point(x, y);
                label.Size = new Size(85, 22);
                label.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
                label.ForeColor = Color.FromArgb(32, 58, 95);
                Controls.Add(label);

                return label;
            }

            private void btnSave_Click(object sender, EventArgs e)
            {
                if (!editMode)
                    ReportId = GetComboIntValue(cmbReport);

                SectionId = GetComboIntValue(cmbSection);
                PresentationDate = dtpDate.Value.Date;
                LocationText = txtLocation.Text.Trim();

                if (ReportId <= 0)
                {
                    MessageBox.Show("Выберите доклад.");
                    return;
                }

                if (SectionId <= 0)
                {
                    MessageBox.Show("Выберите секцию.");
                    return;
                }

                if (cmbTime.SelectedItem == null)
                {
                    MessageBox.Show("Выберите время.");
                    return;
                }

                if (!TimeSpan.TryParse(cmbTime.SelectedItem.ToString(), out TimeSpan parsedTime))
                {
                    MessageBox.Show("Некорректное время.");
                    return;
                }

                if (LocationText == "")
                {
                    MessageBox.Show("Введите место проведения.");
                    return;
                }

                PresentationTime = parsedTime;

                DialogResult = DialogResult.OK;
                Close();
            }

            private int GetComboIntValue(ComboBox comboBox)
            {
                if (comboBox.SelectedValue == null || comboBox.SelectedValue == DBNull.Value)
                    return 0;

                int value;

                if (int.TryParse(comboBox.SelectedValue.ToString(), out value))
                    return value;

                return 0;
            }
        }
    }
}