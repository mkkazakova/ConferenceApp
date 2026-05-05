namespace ConferenceApp
{
    partial class ParticipantsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPeople;
        private System.Windows.Forms.TabPage tabRequests;

        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblRoleFilter;
        private System.Windows.Forms.ComboBox cmbRoleFilter;
        private System.Windows.Forms.Label lblStatusFilter;
        private System.Windows.Forms.ComboBox cmbStatusFilter;

        private System.Windows.Forms.DataGridView dgvParticipants;

        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblFullNameValue;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblEmailValue;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblPhoneValue;
        private System.Windows.Forms.Label lblParticipantStatus;
        private System.Windows.Forms.Label lblParticipantStatusValue;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblRoleValue;
        private System.Windows.Forms.Label lblWorkplace;
        private System.Windows.Forms.Label lblWorkplaceValue;
        private System.Windows.Forms.Label lblAcademicDegree;
        private System.Windows.Forms.Label lblAcademicDegreeValue;

        private System.Windows.Forms.GroupBox gbUserReports;
        private System.Windows.Forms.DataGridView dgvUserReports;

        private System.Windows.Forms.Button btnDeleteParticipant;

        private System.Windows.Forms.DataGridView dgvReviewerRequests;
        private System.Windows.Forms.Button btnApproveRequest;
        private System.Windows.Forms.Button btnRejectRequest;

        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPeople = new System.Windows.Forms.TabPage();
            this.tabRequests = new System.Windows.Forms.TabPage();

            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblRoleFilter = new System.Windows.Forms.Label();
            this.cmbRoleFilter = new System.Windows.Forms.ComboBox();
            this.lblStatusFilter = new System.Windows.Forms.Label();
            this.cmbStatusFilter = new System.Windows.Forms.ComboBox();

            this.dgvParticipants = new System.Windows.Forms.DataGridView();

            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblFullNameValue = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblEmailValue = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblPhoneValue = new System.Windows.Forms.Label();
            this.lblParticipantStatus = new System.Windows.Forms.Label();
            this.lblParticipantStatusValue = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblRoleValue = new System.Windows.Forms.Label();
            this.lblWorkplace = new System.Windows.Forms.Label();
            this.lblWorkplaceValue = new System.Windows.Forms.Label();
            this.lblAcademicDegree = new System.Windows.Forms.Label();
            this.lblAcademicDegreeValue = new System.Windows.Forms.Label();

            this.gbUserReports = new System.Windows.Forms.GroupBox();
            this.dgvUserReports = new System.Windows.Forms.DataGridView();

            this.btnDeleteParticipant = new System.Windows.Forms.Button();

            this.dgvReviewerRequests = new System.Windows.Forms.DataGridView();
            this.btnApproveRequest = new System.Windows.Forms.Button();
            this.btnRejectRequest = new System.Windows.Forms.Button();

            this.btnClose = new System.Windows.Forms.Button();

            this.tabControl.SuspendLayout();
            this.tabPeople.SuspendLayout();
            this.tabRequests.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.dgvParticipants)).BeginInit();
            this.gbDetails.SuspendLayout();
            this.gbUserReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviewerRequests)).BeginInit();

            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 58, 95);
            this.lblTitle.Location = new System.Drawing.Point(320, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(360, 31);
            this.lblTitle.Text = "Участники и рецензенты";

            // tabControl
            this.tabControl.Controls.Add(this.tabPeople);
            this.tabControl.Controls.Add(this.tabRequests);
            this.tabControl.Location = new System.Drawing.Point(25, 65);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(950, 490);

            // tabPeople
            this.tabPeople.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.tabPeople.Controls.Add(this.lblSearch);
            this.tabPeople.Controls.Add(this.txtSearch);
            this.tabPeople.Controls.Add(this.lblRoleFilter);
            this.tabPeople.Controls.Add(this.cmbRoleFilter);
            this.tabPeople.Controls.Add(this.lblStatusFilter);
            this.tabPeople.Controls.Add(this.cmbStatusFilter);
            this.tabPeople.Controls.Add(this.dgvParticipants);
            this.tabPeople.Controls.Add(this.gbDetails);
            this.tabPeople.Controls.Add(this.gbUserReports);
            this.tabPeople.Controls.Add(this.btnDeleteParticipant);
            this.tabPeople.Location = new System.Drawing.Point(4, 25);
            this.tabPeople.Name = "tabPeople";
            this.tabPeople.Padding = new System.Windows.Forms.Padding(3);
            this.tabPeople.Size = new System.Drawing.Size(942, 461);
            this.tabPeople.Text = "Участники и рецензенты";

            // tabRequests
            this.tabRequests.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.tabRequests.Controls.Add(this.dgvReviewerRequests);
            this.tabRequests.Controls.Add(this.btnApproveRequest);
            this.tabRequests.Controls.Add(this.btnRejectRequest);
            this.tabRequests.Location = new System.Drawing.Point(4, 25);
            this.tabRequests.Name = "tabRequests";
            this.tabRequests.Padding = new System.Windows.Forms.Padding(3);
            this.tabRequests.Size = new System.Drawing.Size(942, 461);
            this.tabRequests.Text = "Заявки рецензентов";

            // lblSearch
            this.lblSearch.Location = new System.Drawing.Point(15, 20);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(70, 20);
            this.lblSearch.Text = "Поиск:";

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(75, 17);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(250, 22);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // lblRoleFilter
            this.lblRoleFilter.Location = new System.Drawing.Point(350, 20);
            this.lblRoleFilter.Name = "lblRoleFilter";
            this.lblRoleFilter.Size = new System.Drawing.Size(50, 20);
            this.lblRoleFilter.Text = "Роль:";

            // cmbRoleFilter
            this.cmbRoleFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoleFilter.Items.AddRange(new object[]
            {
                "Все",
                "Участник",
                "Рецензент"
            });
            this.cmbRoleFilter.Location = new System.Drawing.Point(405, 17);
            this.cmbRoleFilter.Name = "cmbRoleFilter";
            this.cmbRoleFilter.Size = new System.Drawing.Size(140, 24);
            this.cmbRoleFilter.SelectedIndexChanged += new System.EventHandler(this.cmbRoleFilter_SelectedIndexChanged);

            // lblStatusFilter
            this.lblStatusFilter.Location = new System.Drawing.Point(570, 20);
            this.lblStatusFilter.Name = "lblStatusFilter";
            this.lblStatusFilter.Size = new System.Drawing.Size(65, 20);
            this.lblStatusFilter.Text = "Статус:";

            // cmbStatusFilter
            this.cmbStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusFilter.Items.AddRange(new object[]
            {
                "Все",
                "Слушатель",
                "Докладчик"
            });
            this.cmbStatusFilter.Location = new System.Drawing.Point(640, 17);
            this.cmbStatusFilter.Name = "cmbStatusFilter";
            this.cmbStatusFilter.Size = new System.Drawing.Size(140, 24);
            this.cmbStatusFilter.SelectedIndexChanged += new System.EventHandler(this.cmbStatusFilter_SelectedIndexChanged);

            // btnDeleteParticipant
            this.btnDeleteParticipant.Location = new System.Drawing.Point(795, 14);
            this.btnDeleteParticipant.Name = "btnDeleteParticipant";
            this.btnDeleteParticipant.Size = new System.Drawing.Size(130, 30);
            this.btnDeleteParticipant.Text = "Удалить";
            this.btnDeleteParticipant.Click += new System.EventHandler(this.btnDeleteParticipant_Click);

            // dgvParticipants
            this.dgvParticipants.AllowUserToAddRows = false;
            this.dgvParticipants.AllowUserToDeleteRows = false;
            this.dgvParticipants.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvParticipants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParticipants.EnableHeadersVisualStyles = false;
            this.dgvParticipants.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.dgvParticipants.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvParticipants.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
            this.dgvParticipants.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvParticipants.Location = new System.Drawing.Point(15, 55);
            this.dgvParticipants.MultiSelect = false;
            this.dgvParticipants.Name = "dgvParticipants";
            this.dgvParticipants.ReadOnly = true;
            this.dgvParticipants.RowHeadersVisible = false;
            this.dgvParticipants.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvParticipants.Size = new System.Drawing.Size(910, 145);
            this.dgvParticipants.SelectionChanged += new System.EventHandler(this.dgvParticipants_SelectionChanged);

            // gbDetails
            this.gbDetails.Controls.Add(this.lblFullName);
            this.gbDetails.Controls.Add(this.lblFullNameValue);
            this.gbDetails.Controls.Add(this.lblEmail);
            this.gbDetails.Controls.Add(this.lblEmailValue);
            this.gbDetails.Controls.Add(this.lblPhone);
            this.gbDetails.Controls.Add(this.lblPhoneValue);
            this.gbDetails.Controls.Add(this.lblParticipantStatus);
            this.gbDetails.Controls.Add(this.lblParticipantStatusValue);
            this.gbDetails.Controls.Add(this.lblRole);
            this.gbDetails.Controls.Add(this.lblRoleValue);
            this.gbDetails.Controls.Add(this.lblWorkplace);
            this.gbDetails.Controls.Add(this.lblWorkplaceValue);
            this.gbDetails.Controls.Add(this.lblAcademicDegree);
            this.gbDetails.Controls.Add(this.lblAcademicDegreeValue);
            this.gbDetails.Location = new System.Drawing.Point(15, 210);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(910, 105);
            this.gbDetails.Text = "Информация о выбранном пользователе";

            // lblFullName
            this.lblFullName.Location = new System.Drawing.Point(20, 30);
            this.lblFullName.Size = new System.Drawing.Size(120, 20);
            this.lblFullName.Text = "ФИО:";

            // lblFullNameValue
            this.lblFullNameValue.Location = new System.Drawing.Point(150, 30);
            this.lblFullNameValue.Size = new System.Drawing.Size(250, 20);
            this.lblFullNameValue.Text = "-";

            // lblEmail
            this.lblEmail.Location = new System.Drawing.Point(20, 60);
            this.lblEmail.Size = new System.Drawing.Size(120, 20);
            this.lblEmail.Text = "Email:";

            // lblEmailValue
            this.lblEmailValue.Location = new System.Drawing.Point(150, 60);
            this.lblEmailValue.Size = new System.Drawing.Size(250, 20);
            this.lblEmailValue.Text = "-";

            // lblPhone
            this.lblPhone.Location = new System.Drawing.Point(405, 30);
            this.lblPhone.Size = new System.Drawing.Size(120, 20);
            this.lblPhone.Text = "Телефон:";

            // lblPhoneValue
            this.lblPhoneValue.Location = new System.Drawing.Point(535, 30);
            this.lblPhoneValue.Size = new System.Drawing.Size(170, 20);
            this.lblPhoneValue.Text = "-";

            // lblParticipantStatus
            this.lblParticipantStatus.Location = new System.Drawing.Point(405, 60);
            this.lblParticipantStatus.Size = new System.Drawing.Size(120, 20);
            this.lblParticipantStatus.Text = "Статус участия:";

            // lblParticipantStatusValue
            this.lblParticipantStatusValue.Location = new System.Drawing.Point(535, 60);
            this.lblParticipantStatusValue.Size = new System.Drawing.Size(170, 20);
            this.lblParticipantStatusValue.Text = "-";

            // lblRole
            this.lblRole.Location = new System.Drawing.Point(705, 30);
            this.lblRole.Size = new System.Drawing.Size(120, 20);
            this.lblRole.Text = "Роль:";

            // lblRoleValue
            this.lblRoleValue.Location = new System.Drawing.Point(825, 30);
            this.lblRoleValue.Size = new System.Drawing.Size(80, 20);
            this.lblRoleValue.Text = "-";

            // lblWorkplace
            this.lblWorkplace.Location = new System.Drawing.Point(705, 60);
            this.lblWorkplace.Size = new System.Drawing.Size(120, 20);
            this.lblWorkplace.Text = "Место работы:";

            // lblWorkplaceValue
            this.lblWorkplaceValue.Location = new System.Drawing.Point(825, 60);
            this.lblWorkplaceValue.Size = new System.Drawing.Size(80, 20);
            this.lblWorkplaceValue.Text = "-";

            // lblAcademicDegree
            this.lblAcademicDegree.Location = new System.Drawing.Point(20, 82);
            this.lblAcademicDegree.Size = new System.Drawing.Size(120, 20);
            this.lblAcademicDegree.Text = "Учёная степень:";

            // lblAcademicDegreeValue
            this.lblAcademicDegreeValue.Location = new System.Drawing.Point(150, 82);
            this.lblAcademicDegreeValue.Size = new System.Drawing.Size(250, 20);
            this.lblAcademicDegreeValue.Text = "-";

            // gbUserReports
            this.gbUserReports.Controls.Add(this.dgvUserReports);
            this.gbUserReports.Location = new System.Drawing.Point(15, 325);
            this.gbUserReports.Name = "gbUserReports";
            this.gbUserReports.Size = new System.Drawing.Size(910, 120);
            this.gbUserReports.Text = "Доклады выбранного пользователя";

            // dgvUserReports
            this.dgvUserReports.AllowUserToAddRows = false;
            this.dgvUserReports.AllowUserToDeleteRows = false;
            this.dgvUserReports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUserReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserReports.EnableHeadersVisualStyles = false;
            this.dgvUserReports.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.dgvUserReports.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvUserReports.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
            this.dgvUserReports.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvUserReports.Location = new System.Drawing.Point(15, 25);
            this.dgvUserReports.MultiSelect = false;
            this.dgvUserReports.Name = "dgvUserReports";
            this.dgvUserReports.ReadOnly = true;
            this.dgvUserReports.RowHeadersVisible = false;
            this.dgvUserReports.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUserReports.Size = new System.Drawing.Size(880, 80);

            // dgvReviewerRequests
            this.dgvReviewerRequests.AllowUserToAddRows = false;
            this.dgvReviewerRequests.AllowUserToDeleteRows = false;
            this.dgvReviewerRequests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReviewerRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReviewerRequests.EnableHeadersVisualStyles = false;
            this.dgvReviewerRequests.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.dgvReviewerRequests.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvReviewerRequests.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
            this.dgvReviewerRequests.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvReviewerRequests.Location = new System.Drawing.Point(20, 25);
            this.dgvReviewerRequests.MultiSelect = false;
            this.dgvReviewerRequests.Name = "dgvReviewerRequests";
            this.dgvReviewerRequests.ReadOnly = true;
            this.dgvReviewerRequests.RowHeadersVisible = false;
            this.dgvReviewerRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReviewerRequests.Size = new System.Drawing.Size(900, 345);

            // btnApproveRequest
            this.btnApproveRequest.Location = new System.Drawing.Point(620, 390);
            this.btnApproveRequest.Name = "btnApproveRequest";
            this.btnApproveRequest.Size = new System.Drawing.Size(140, 35);
            this.btnApproveRequest.Text = "Одобрить";
            this.btnApproveRequest.Click += new System.EventHandler(this.btnApproveRequest_Click);

            // btnRejectRequest
            this.btnRejectRequest.Location = new System.Drawing.Point(780, 390);
            this.btnRejectRequest.Name = "btnRejectRequest";
            this.btnRejectRequest.Size = new System.Drawing.Size(140, 35);
            this.btnRejectRequest.Text = "Отклонить";
            this.btnRejectRequest.Click += new System.EventHandler(this.btnRejectRequest_Click);

            // btnClose
            this.btnClose.Location = new System.Drawing.Point(830, 570);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 35);
            this.btnClose.Text = "Назад";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // ParticipantsForm
            this.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.ClientSize = new System.Drawing.Size(1000, 620);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnClose);
            this.Name = "ParticipantsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Участники и рецензенты";

            ((System.ComponentModel.ISupportInitialize)(this.dgvParticipants)).EndInit();
            this.gbDetails.ResumeLayout(false);
            this.gbUserReports.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviewerRequests)).EndInit();

            this.tabPeople.ResumeLayout(false);
            this.tabPeople.PerformLayout();
            this.tabRequests.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}