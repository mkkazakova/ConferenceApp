namespace ConferenceApp
{
    partial class SectionsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvSections;
        private System.Windows.Forms.Label lblSectionName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblReports;
        private System.Windows.Forms.TextBox txtSectionName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ListBox lstReports;

        private System.Windows.Forms.Label lblFeedback;
        private System.Windows.Forms.Label lblOrganizationScore;
        private System.Windows.Forms.Label lblContentScore;
        private System.Windows.Forms.Label lblUsefulnessScore;
        private System.Windows.Forms.Label lblVisitComment;
        private System.Windows.Forms.NumericUpDown nudOrganizationScore;
        private System.Windows.Forms.NumericUpDown nudContentScore;
        private System.Windows.Forms.NumericUpDown nudUsefulnessScore;
        private System.Windows.Forms.TextBox txtVisitComment;
        private System.Windows.Forms.Button btnSaveFeedback;

        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnCancelRegister;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvSections = new System.Windows.Forms.DataGridView();
            this.lblSectionName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblReports = new System.Windows.Forms.Label();
            this.txtSectionName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lstReports = new System.Windows.Forms.ListBox();

            this.lblFeedback = new System.Windows.Forms.Label();
            this.lblOrganizationScore = new System.Windows.Forms.Label();
            this.lblContentScore = new System.Windows.Forms.Label();
            this.lblUsefulnessScore = new System.Windows.Forms.Label();
            this.lblVisitComment = new System.Windows.Forms.Label();
            this.nudOrganizationScore = new System.Windows.Forms.NumericUpDown();
            this.nudContentScore = new System.Windows.Forms.NumericUpDown();
            this.nudUsefulnessScore = new System.Windows.Forms.NumericUpDown();
            this.txtVisitComment = new System.Windows.Forms.TextBox();
            this.btnSaveFeedback = new System.Windows.Forms.Button();

            this.btnRegister = new System.Windows.Forms.Button();
            this.btnCancelRegister = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvSections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOrganizationScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudContentScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUsefulnessScore)).BeginInit();

            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 58, 95);
            this.lblTitle.Location = new System.Drawing.Point(360, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(235, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Секции конференции";

            this.dgvSections.Location = new System.Drawing.Point(30, 75);
            this.dgvSections.Name = "dgvSections";
            this.dgvSections.Size = new System.Drawing.Size(940, 220);
            this.dgvSections.ReadOnly = true;
            this.dgvSections.AllowUserToAddRows = false;
            this.dgvSections.AllowUserToDeleteRows = false;
            this.dgvSections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSections.MultiSelect = false;
            this.dgvSections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSections.TabIndex = 1;
            this.dgvSections.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSections_CellClick);

            this.lblSectionName.Location = new System.Drawing.Point(30, 318);
            this.lblSectionName.Name = "lblSectionName";
            this.lblSectionName.Size = new System.Drawing.Size(130, 20);
            this.lblSectionName.TabIndex = 2;
            this.lblSectionName.Text = "Название:";

            this.txtSectionName.Location = new System.Drawing.Point(170, 315);
            this.txtSectionName.Name = "txtSectionName";
            this.txtSectionName.Size = new System.Drawing.Size(800, 22);
            this.txtSectionName.TabIndex = 3;

            this.lblDescription.Location = new System.Drawing.Point(30, 353);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(130, 20);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Описание:";

            this.txtDescription.Location = new System.Drawing.Point(170, 350);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(800, 50);
            this.txtDescription.Multiline = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.TabIndex = 5;

            this.lblReports.Location = new System.Drawing.Point(30, 417);
            this.lblReports.Name = "lblReports";
            this.lblReports.Size = new System.Drawing.Size(130, 20);
            this.lblReports.TabIndex = 6;
            this.lblReports.Text = "Доклады:";

            this.lstReports.Location = new System.Drawing.Point(170, 414);
            this.lstReports.Name = "lstReports";
            this.lstReports.Size = new System.Drawing.Size(800, 70);
            this.lstReports.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.lstReports.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstReports.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lstReports.ItemHeight = 18;
            this.lstReports.TabIndex = 7;

            this.lblFeedback.Location = new System.Drawing.Point(30, 500);
            this.lblFeedback.Name = "lblFeedback";
            this.lblFeedback.Size = new System.Drawing.Size(300, 20);
            this.lblFeedback.TabIndex = 8;
            this.lblFeedback.Text = "Оценка посещения:";

            this.lblOrganizationScore.Location = new System.Drawing.Point(170, 525);
            this.lblOrganizationScore.Name = "lblOrganizationScore";
            this.lblOrganizationScore.Size = new System.Drawing.Size(115, 20);
            this.lblOrganizationScore.TabIndex = 9;
            this.lblOrganizationScore.Text = "Организация:";

            this.nudOrganizationScore.Location = new System.Drawing.Point(290, 522);
            this.nudOrganizationScore.Name = "nudOrganizationScore";
            this.nudOrganizationScore.Minimum = 1;
            this.nudOrganizationScore.Maximum = 10;
            this.nudOrganizationScore.Value = 5;
            this.nudOrganizationScore.Size = new System.Drawing.Size(70, 22);
            this.nudOrganizationScore.TabIndex = 10;

            this.lblContentScore.Location = new System.Drawing.Point(390, 525);
            this.lblContentScore.Name = "lblContentScore";
            this.lblContentScore.Size = new System.Drawing.Size(140, 20);
            this.lblContentScore.TabIndex = 11;
            this.lblContentScore.Text = "Содержание:";

            this.nudContentScore.Location = new System.Drawing.Point(535, 522);
            this.nudContentScore.Name = "nudContentScore";
            this.nudContentScore.Minimum = 1;
            this.nudContentScore.Maximum = 10;
            this.nudContentScore.Value = 5;
            this.nudContentScore.Size = new System.Drawing.Size(70, 22);
            this.nudContentScore.TabIndex = 12;

            this.lblUsefulnessScore.Location = new System.Drawing.Point(635, 525);
            this.lblUsefulnessScore.Name = "lblUsefulnessScore";
            this.lblUsefulnessScore.Size = new System.Drawing.Size(130, 20);
            this.lblUsefulnessScore.TabIndex = 13;
            this.lblUsefulnessScore.Text = "Полезность:";

            this.nudUsefulnessScore.Location = new System.Drawing.Point(770, 522);
            this.nudUsefulnessScore.Name = "nudUsefulnessScore";
            this.nudUsefulnessScore.Minimum = 1;
            this.nudUsefulnessScore.Maximum = 10;
            this.nudUsefulnessScore.Value = 5;
            this.nudUsefulnessScore.Size = new System.Drawing.Size(70, 22);
            this.nudUsefulnessScore.TabIndex = 14;

            this.lblVisitComment.Location = new System.Drawing.Point(30, 562);
            this.lblVisitComment.Name = "lblVisitComment";
            this.lblVisitComment.Size = new System.Drawing.Size(130, 20);
            this.lblVisitComment.TabIndex = 15;
            this.lblVisitComment.Text = "Комментарий:";

            this.txtVisitComment.Location = new System.Drawing.Point(170, 557);
            this.txtVisitComment.Name = "txtVisitComment";
            this.txtVisitComment.Size = new System.Drawing.Size(650, 55);
            this.txtVisitComment.Multiline = true;
            this.txtVisitComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtVisitComment.TabIndex = 16;

            this.btnSaveFeedback.Location = new System.Drawing.Point(830, 570);
            this.btnSaveFeedback.Name = "btnSaveFeedback";
            this.btnSaveFeedback.Size = new System.Drawing.Size(140, 35);
            this.btnSaveFeedback.TabIndex = 17;
            this.btnSaveFeedback.Text = "Сохранить оценку";
            this.btnSaveFeedback.UseVisualStyleBackColor = false;
            this.btnSaveFeedback.ForeColor = System.Drawing.Color.White;
            this.btnSaveFeedback.Click += new System.EventHandler(this.btnSaveFeedback_Click);

            this.btnRegister.Location = new System.Drawing.Point(170, 640);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(140, 35);
            this.btnRegister.TabIndex = 18;
            this.btnRegister.Text = "Записаться";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            this.btnCancelRegister.Location = new System.Drawing.Point(325, 640);
            this.btnCancelRegister.Name = "btnCancelRegister";
            this.btnCancelRegister.Size = new System.Drawing.Size(140, 35);
            this.btnCancelRegister.TabIndex = 19;
            this.btnCancelRegister.Text = "Отменить";
            this.btnCancelRegister.UseVisualStyleBackColor = true;
            this.btnCancelRegister.Click += new System.EventHandler(this.btnCancelRegister_Click);

            this.btnAdd.Location = new System.Drawing.Point(170, 640);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(140, 35);
            this.btnAdd.TabIndex = 20;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnUpdate.Location = new System.Drawing.Point(325, 640);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(140, 35);
            this.btnUpdate.TabIndex = 21;
            this.btnUpdate.Text = "Изменить";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            this.btnDelete.Location = new System.Drawing.Point(480, 640);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 35);
            this.btnDelete.TabIndex = 22;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnClose.Location = new System.Drawing.Point(830, 640);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 35);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "Назад";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.ClientSize = new System.Drawing.Size(1000, 700);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvSections);
            this.Controls.Add(this.lblSectionName);
            this.Controls.Add(this.txtSectionName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblReports);
            this.Controls.Add(this.lstReports);

            this.Controls.Add(this.lblFeedback);
            this.Controls.Add(this.lblOrganizationScore);
            this.Controls.Add(this.nudOrganizationScore);
            this.Controls.Add(this.lblContentScore);
            this.Controls.Add(this.nudContentScore);
            this.Controls.Add(this.lblUsefulnessScore);
            this.Controls.Add(this.nudUsefulnessScore);
            this.Controls.Add(this.lblVisitComment);
            this.Controls.Add(this.txtVisitComment);
            this.Controls.Add(this.btnSaveFeedback);

            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnCancelRegister);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClose);

            this.Name = "SectionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Секции";

            ((System.ComponentModel.ISupportInitialize)(this.dgvSections)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOrganizationScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudContentScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUsefulnessScore)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}