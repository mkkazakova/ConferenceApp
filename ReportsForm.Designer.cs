namespace ConferenceApp
{
    partial class ReportsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvReports;

        private System.Windows.Forms.Label lblTopic;
        private System.Windows.Forms.Label lblAnnotation;
        private System.Windows.Forms.Label lblKeywords;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblFilePath;

        private System.Windows.Forms.TextBox txtTopic;
        private System.Windows.Forms.TextBox txtAnnotation;
        private System.Windows.Forms.TextBox txtKeywords;
        private System.Windows.Forms.TextBox txtReviewStatus;
        private System.Windows.Forms.TextBox txtFilePath;

        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;

        private System.Windows.Forms.Label lblSection;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblLocation;

        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Button btnAddToSection;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvReports = new System.Windows.Forms.DataGridView();

            this.lblTopic = new System.Windows.Forms.Label();
            this.lblAnnotation = new System.Windows.Forms.Label();
            this.lblKeywords = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblFilePath = new System.Windows.Forms.Label();

            this.txtTopic = new System.Windows.Forms.TextBox();
            this.txtAnnotation = new System.Windows.Forms.TextBox();
            this.txtKeywords = new System.Windows.Forms.TextBox();
            this.txtReviewStatus = new System.Windows.Forms.TextBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();

            this.btnChooseFile = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.lblSection = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();

            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.btnAddToSection = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 58, 95);
            this.lblTitle.Location = new System.Drawing.Point(390, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(112, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Доклады";

            // dgvReports
            this.dgvReports.Location = new System.Drawing.Point(30, 75);
            this.dgvReports.Name = "dgvReports";
            this.dgvReports.Size = new System.Drawing.Size(940, 230);
            this.dgvReports.ReadOnly = true;
            this.dgvReports.AllowUserToAddRows = false;
            this.dgvReports.AllowUserToDeleteRows = false;
            this.dgvReports.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReports.MultiSelect = false;
            this.dgvReports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReports.TabIndex = 1;
            this.dgvReports.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReports_CellClick);

            // lblTopic
            this.lblTopic.Location = new System.Drawing.Point(30, 330);
            this.lblTopic.Name = "lblTopic";
            this.lblTopic.Size = new System.Drawing.Size(130, 20);
            this.lblTopic.TabIndex = 2;
            this.lblTopic.Text = "Тема:";

            // txtTopic
            this.txtTopic.Location = new System.Drawing.Point(170, 327);
            this.txtTopic.Name = "txtTopic";
            this.txtTopic.Size = new System.Drawing.Size(800, 22);
            this.txtTopic.TabIndex = 3;

            // lblAnnotation
            this.lblAnnotation.Location = new System.Drawing.Point(30, 365);
            this.lblAnnotation.Name = "lblAnnotation";
            this.lblAnnotation.Size = new System.Drawing.Size(130, 20);
            this.lblAnnotation.TabIndex = 4;
            this.lblAnnotation.Text = "Аннотация:";

            // txtAnnotation
            this.txtAnnotation.Location = new System.Drawing.Point(170, 362);
            this.txtAnnotation.Multiline = true;
            this.txtAnnotation.Name = "txtAnnotation";
            this.txtAnnotation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAnnotation.Size = new System.Drawing.Size(800, 70);
            this.txtAnnotation.TabIndex = 5;

            // lblKeywords
            this.lblKeywords.Location = new System.Drawing.Point(30, 450);
            this.lblKeywords.Name = "lblKeywords";
            this.lblKeywords.Size = new System.Drawing.Size(130, 20);
            this.lblKeywords.TabIndex = 6;
            this.lblKeywords.Text = "Ключевые слова:";

            // txtKeywords
            this.txtKeywords.Location = new System.Drawing.Point(170, 447);
            this.txtKeywords.Name = "txtKeywords";
            this.txtKeywords.Size = new System.Drawing.Size(800, 22);
            this.txtKeywords.TabIndex = 7;

            // lblStatus
            this.lblStatus.Location = new System.Drawing.Point(30, 485);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(130, 20);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Статус:";

            // txtReviewStatus
            this.txtReviewStatus.Location = new System.Drawing.Point(170, 482);
            this.txtReviewStatus.Name = "txtReviewStatus";
            this.txtReviewStatus.ReadOnly = true;
            this.txtReviewStatus.Size = new System.Drawing.Size(250, 22);
            this.txtReviewStatus.TabIndex = 9;

            // lblFilePath
            this.lblFilePath.Location = new System.Drawing.Point(30, 520);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(130, 20);
            this.lblFilePath.TabIndex = 10;
            this.lblFilePath.Text = "Файл:";

            // txtFilePath
            this.txtFilePath.Location = new System.Drawing.Point(170, 517);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(660, 22);
            this.txtFilePath.TabIndex = 11;

            // btnChooseFile
            this.btnChooseFile.Location = new System.Drawing.Point(840, 515);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(130, 28);
            this.btnChooseFile.TabIndex = 12;
            this.btnChooseFile.Text = "Выбрать";
            this.btnChooseFile.UseVisualStyleBackColor = true;
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);

            // lblSection
            this.lblSection.Location = new System.Drawing.Point(30, 560);
            this.lblSection.Name = "lblSection";
            this.lblSection.Size = new System.Drawing.Size(130, 20);
            this.lblSection.TabIndex = 13;
            this.lblSection.Text = "Секция:";

            // cmbSection
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Location = new System.Drawing.Point(170, 557);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(300, 24);
            this.cmbSection.TabIndex = 14;

            // lblDate
            this.lblDate.Location = new System.Drawing.Point(490, 560);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(50, 20);
            this.lblDate.TabIndex = 15;
            this.lblDate.Text = "Дата:";

            // dtpDate
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(545, 557);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(130, 22);
            this.dtpDate.TabIndex = 16;

            // lblTime
            this.lblTime.Location = new System.Drawing.Point(695, 560);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(55, 20);
            this.lblTime.TabIndex = 17;
            this.lblTime.Text = "Время:";

            // dtpTime
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(760, 557);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ShowUpDown = true;
            this.dtpTime.Size = new System.Drawing.Size(100, 22);
            this.dtpTime.TabIndex = 18;

            // lblLocation
            this.lblLocation.Location = new System.Drawing.Point(30, 595);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(130, 20);
            this.lblLocation.TabIndex = 19;
            this.lblLocation.Text = "Место:";

            // txtLocation
            this.txtLocation.Location = new System.Drawing.Point(170, 592);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(300, 22);
            this.txtLocation.TabIndex = 20;

            // btnAddToSection
            this.btnAddToSection.Location = new System.Drawing.Point(490, 588);
            this.btnAddToSection.Name = "btnAddToSection";
            this.btnAddToSection.Size = new System.Drawing.Size(180, 30);
            this.btnAddToSection.TabIndex = 21;
            this.btnAddToSection.Text = "Добавить в секцию";
            this.btnAddToSection.UseVisualStyleBackColor = true;
            this.btnAddToSection.Click += new System.EventHandler(this.btnAddToSection_Click);

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(170, 640);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(140, 35);
            this.btnAdd.TabIndex = 22;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnUpdate
            this.btnUpdate.Location = new System.Drawing.Point(325, 640);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(140, 35);
            this.btnUpdate.TabIndex = 23;
            this.btnUpdate.Text = "Изменить";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(480, 640);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 35);
            this.btnDelete.TabIndex = 24;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnClear
            this.btnClear.Location = new System.Drawing.Point(635, 640);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(140, 35);
            this.btnClear.TabIndex = 25;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // btnClose
            this.btnClose.Location = new System.Drawing.Point(830, 640);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 35);
            this.btnClose.TabIndex = 26;
            this.btnClose.Text = "Назад";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // ReportsForm
            this.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.ClientSize = new System.Drawing.Size(1000, 700);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvReports);

            this.Controls.Add(this.lblTopic);
            this.Controls.Add(this.txtTopic);
            this.Controls.Add(this.lblAnnotation);
            this.Controls.Add(this.txtAnnotation);
            this.Controls.Add(this.lblKeywords);
            this.Controls.Add(this.txtKeywords);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtReviewStatus);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnChooseFile);

            this.Controls.Add(this.lblSection);
            this.Controls.Add(this.cmbSection);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.btnAddToSection);

            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnClose);

            this.Name = "ReportsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Доклады";

            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}