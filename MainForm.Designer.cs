namespace ConferenceApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.FlowLayoutPanel menuPanel;

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
            this.menuPanel = new System.Windows.Forms.FlowLayoutPanel();

            this.btnProfile = new System.Windows.Forms.Button();
            this.btnParticipants = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnSections = new System.Windows.Forms.Button();
            this.btnReviews = new System.Windows.Forms.Button();
            this.btnProgram = new System.Windows.Forms.Button();
            this.btnMaterials = new System.Windows.Forms.Button();
            this.btnStatistics = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();

            this.menuPanel.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 58, 95);
            this.lblTitle.Location = new System.Drawing.Point(93, 37);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(604, 33);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Информационная система конференции";

            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(45, 45, 45);
            this.lblUser.Location = new System.Drawing.Point(93, 105);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(136, 20);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "Пользователь:";

            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(45, 45, 45);
            this.lblRole.Location = new System.Drawing.Point(93, 138);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(55, 20);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "Роль:";

            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(32, 58, 95);
            this.lblInfo.Location = new System.Drawing.Point(210, 185);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(379, 26);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "Доступные разделы приложения";

            // 
            // menuPanel
            // 
            this.menuPanel.AutoScroll = false;
            this.menuPanel.Controls.Add(this.btnProfile);
            this.menuPanel.Controls.Add(this.btnParticipants);
            this.menuPanel.Controls.Add(this.btnReports);
            this.menuPanel.Controls.Add(this.btnSections);
            this.menuPanel.Controls.Add(this.btnReviews);
            this.menuPanel.Controls.Add(this.btnProgram);
            this.menuPanel.Controls.Add(this.btnMaterials);
            this.menuPanel.Controls.Add(this.btnStatistics);
            this.menuPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.menuPanel.Location = new System.Drawing.Point(221, 228);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(400, 360);
            this.menuPanel.TabIndex = 4;
            this.menuPanel.WrapContents = false;

            // 
            // btnProfile
            // 
            this.btnProfile.BackColor = System.Drawing.Color.FromArgb(194, 224, 255);
            this.btnProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnProfile.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(400, 55);
            this.btnProfile.TabIndex = 0;
            this.btnProfile.TabStop = false;
            this.btnProfile.Text = "Личный кабинет";
            this.btnProfile.UseVisualStyleBackColor = false;

            // 
            // btnParticipants
            // 
            this.btnParticipants.BackColor = System.Drawing.Color.FromArgb(194, 224, 255);
            this.btnParticipants.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnParticipants.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnParticipants.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.btnParticipants.Name = "btnParticipants";
            this.btnParticipants.Size = new System.Drawing.Size(400, 55);
            this.btnParticipants.TabIndex = 1;
            this.btnParticipants.TabStop = false;
            this.btnParticipants.Text = "Участники";
            this.btnParticipants.UseVisualStyleBackColor = false;

            // 
            // btnReports
            // 
            this.btnReports.BackColor = System.Drawing.Color.FromArgb(194, 224, 255);
            this.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReports.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnReports.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(400, 55);
            this.btnReports.TabIndex = 2;
            this.btnReports.TabStop = false;
            this.btnReports.Text = "Доклады";
            this.btnReports.UseVisualStyleBackColor = false;

            // 
            // btnSections
            // 
            this.btnSections.BackColor = System.Drawing.Color.FromArgb(194, 224, 255);
            this.btnSections.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSections.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnSections.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.btnSections.Name = "btnSections";
            this.btnSections.Size = new System.Drawing.Size(400, 55);
            this.btnSections.TabIndex = 3;
            this.btnSections.TabStop = false;
            this.btnSections.Text = "Секции";
            this.btnSections.UseVisualStyleBackColor = false;

            // 
            // btnReviews
            // 
            this.btnReviews.BackColor = System.Drawing.Color.FromArgb(194, 224, 255);
            this.btnReviews.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReviews.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnReviews.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.btnReviews.Name = "btnReviews";
            this.btnReviews.Size = new System.Drawing.Size(400, 55);
            this.btnReviews.TabIndex = 4;
            this.btnReviews.TabStop = false;
            this.btnReviews.Text = "Рецензии";
            this.btnReviews.UseVisualStyleBackColor = false;

            // 
            // btnProgram
            // 
            this.btnProgram.BackColor = System.Drawing.Color.FromArgb(194, 224, 255);
            this.btnProgram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnProgram.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.btnProgram.Name = "btnProgram";
            this.btnProgram.Size = new System.Drawing.Size(400, 55);
            this.btnProgram.TabIndex = 5;
            this.btnProgram.TabStop = false;
            this.btnProgram.Text = "Программа конференции";
            this.btnProgram.UseVisualStyleBackColor = false;

            // 
            // btnMaterials
            // 
            this.btnMaterials.BackColor = System.Drawing.Color.FromArgb(194, 224, 255);
            this.btnMaterials.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaterials.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnMaterials.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.btnMaterials.Name = "btnMaterials";
            this.btnMaterials.Size = new System.Drawing.Size(400, 55);
            this.btnMaterials.TabIndex = 6;
            this.btnMaterials.TabStop = false;
            this.btnMaterials.Text = "Материалы";
            this.btnMaterials.UseVisualStyleBackColor = false;

            // 
            // btnStatistics
            // 
            this.btnStatistics.BackColor = System.Drawing.Color.FromArgb(194, 224, 255);
            this.btnStatistics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnStatistics.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.btnStatistics.Name = "btnStatistics";
            this.btnStatistics.Size = new System.Drawing.Size(400, 55);
            this.btnStatistics.TabIndex = 7;
            this.btnStatistics.TabStop = false;
            this.btnStatistics.Text = "Статистика";
            this.btnStatistics.UseVisualStyleBackColor = false;

            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnExit.Location = new System.Drawing.Point(651, 620);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 39);
            this.btnExit.TabIndex = 5;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.ClientSize = new System.Drawing.Size(842, 680);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.menuPanel);
            this.Controls.Add(this.btnExit);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главное меню";

            this.menuPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}