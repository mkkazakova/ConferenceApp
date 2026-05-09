namespace ConferenceApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblInfo;

        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnParticipants;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnSections;
        private System.Windows.Forms.Button btnReviews;
        private System.Windows.Forms.Button btnProgram;
        private System.Windows.Forms.Button btnMaterials;
        private System.Windows.Forms.Button btnStatistics;
        private System.Windows.Forms.Button btnExit;

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
            this.lblUser = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();

            this.btnProfile = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnReviews = new System.Windows.Forms.Button();
            this.btnParticipants = new System.Windows.Forms.Button();
            this.btnSections = new System.Windows.Forms.Button();
            this.btnProgram = new System.Windows.Forms.Button();
            this.btnMaterials = new System.Windows.Forms.Button();
            this.btnStatistics = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(145, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(430, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Информационная система конференции";

            // lblUser
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUser.Location = new System.Drawing.Point(80, 75);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(122, 23);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "Пользователь:";

            // lblRole
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRole.Location = new System.Drawing.Point(80, 105);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(51, 23);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "Роль:";

            // lblInfo
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblInfo.Location = new System.Drawing.Point(230, 135);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(263, 23);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "Доступные разделы приложения";

            // btnProfile
            this.btnProfile.Location = new System.Drawing.Point(230, 175);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(280, 44);
            this.btnProfile.TabIndex = 4;
            this.btnProfile.TabStop = false;
            this.btnProfile.Text = "Личный кабинет";
            this.btnProfile.UseVisualStyleBackColor = false;

            // btnReports
            this.btnReports.Location = new System.Drawing.Point(230, 235);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(280, 44);
            this.btnReports.TabIndex = 5;
            this.btnReports.TabStop = false;
            this.btnReports.Text = "Доклады";
            this.btnReports.UseVisualStyleBackColor = false;

            // btnReviews
            this.btnReviews.Location = new System.Drawing.Point(230, 295);
            this.btnReviews.Name = "btnReviews";
            this.btnReviews.Size = new System.Drawing.Size(280, 44);
            this.btnReviews.TabIndex = 6;
            this.btnReviews.TabStop = false;
            this.btnReviews.Text = "Рецензии";
            this.btnReviews.UseVisualStyleBackColor = false;

            // btnParticipants
            this.btnParticipants.Location = new System.Drawing.Point(230, 355);
            this.btnParticipants.Name = "btnParticipants";
            this.btnParticipants.Size = new System.Drawing.Size(280, 44);
            this.btnParticipants.TabIndex = 7;
            this.btnParticipants.TabStop = false;
            this.btnParticipants.Text = "Участники";
            this.btnParticipants.UseVisualStyleBackColor = false;

            // btnSections
            this.btnSections.Location = new System.Drawing.Point(230, 415);
            this.btnSections.Name = "btnSections";
            this.btnSections.Size = new System.Drawing.Size(280, 44);
            this.btnSections.TabIndex = 8;
            this.btnSections.TabStop = false;
            this.btnSections.Text = "Секции";
            this.btnSections.UseVisualStyleBackColor = false;

            // btnProgram
            this.btnProgram.Location = new System.Drawing.Point(230, 475);
            this.btnProgram.Name = "btnProgram";
            this.btnProgram.Size = new System.Drawing.Size(280, 44);
            this.btnProgram.TabIndex = 9;
            this.btnProgram.TabStop = false;
            this.btnProgram.Text = "Программа конференции";
            this.btnProgram.UseVisualStyleBackColor = false;

            // btnMaterials
            this.btnMaterials.Location = new System.Drawing.Point(230, 535);
            this.btnMaterials.Name = "btnMaterials";
            this.btnMaterials.Size = new System.Drawing.Size(280, 44);
            this.btnMaterials.TabIndex = 10;
            this.btnMaterials.TabStop = false;
            this.btnMaterials.Text = "Материалы";
            this.btnMaterials.UseVisualStyleBackColor = false;

            // btnStatistics
            this.btnStatistics.Location = new System.Drawing.Point(230, 595);
            this.btnStatistics.Name = "btnStatistics";
            this.btnStatistics.Size = new System.Drawing.Size(280, 44);
            this.btnStatistics.TabIndex = 11;
            this.btnStatistics.TabStop = false;
            this.btnStatistics.Text = "Статистика";
            this.btnStatistics.UseVisualStyleBackColor = false;

            // btnExit
            this.btnExit.Location = new System.Drawing.Point(490, 700);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 40);
            this.btnExit.TabIndex = 12;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = false;

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 750);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnProfile);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnReviews);
            this.Controls.Add(this.btnParticipants);
            this.Controls.Add(this.btnSections);
            this.Controls.Add(this.btnProgram);
            this.Controls.Add(this.btnMaterials);
            this.Controls.Add(this.btnStatistics);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главное меню";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}