namespace ConferenceApp
{
    partial class StatisticsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel summaryPanel;

        private System.Windows.Forms.Label lblParticipantsTitle;
        private System.Windows.Forms.Label lblReportsTitle;
        private System.Windows.Forms.Label lblSectionsTitle;
        private System.Windows.Forms.Label lblReviewsTitle;
        private System.Windows.Forms.Label lblProgramTitle;
        private System.Windows.Forms.Label lblVisitsTitle;

        private System.Windows.Forms.Label lblParticipantsValue;
        private System.Windows.Forms.Label lblReportsValue;
        private System.Windows.Forms.Label lblSectionsValue;
        private System.Windows.Forms.Label lblReviewsValue;
        private System.Windows.Forms.Label lblProgramValue;
        private System.Windows.Forms.Label lblVisitsValue;

        private System.Windows.Forms.FlowLayoutPanel buttonsPanel;

        private System.Windows.Forms.Button btnParticipantsByRole;
        private System.Windows.Forms.Button btnReportsByStatus;
        private System.Windows.Forms.Button btnSectionPopularity;
        private System.Windows.Forms.Button btnReviewsByReviewer;
        private System.Windows.Forms.Button btnAverageScores;
        private System.Windows.Forms.Button btnSectionScores;
        private System.Windows.Forms.Button btnRefresh;

        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.DataGridView dgvStatistics;
        private System.Windows.Forms.Button btnExportReport;
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
            this.summaryPanel = new System.Windows.Forms.Panel();

            this.lblParticipantsTitle = new System.Windows.Forms.Label();
            this.lblReportsTitle = new System.Windows.Forms.Label();
            this.lblSectionsTitle = new System.Windows.Forms.Label();
            this.lblReviewsTitle = new System.Windows.Forms.Label();
            this.lblProgramTitle = new System.Windows.Forms.Label();
            this.lblVisitsTitle = new System.Windows.Forms.Label();

            this.lblParticipantsValue = new System.Windows.Forms.Label();
            this.lblReportsValue = new System.Windows.Forms.Label();
            this.lblSectionsValue = new System.Windows.Forms.Label();
            this.lblReviewsValue = new System.Windows.Forms.Label();
            this.lblProgramValue = new System.Windows.Forms.Label();
            this.lblVisitsValue = new System.Windows.Forms.Label();

            this.buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();

            this.btnParticipantsByRole = new System.Windows.Forms.Button();
            this.btnReportsByStatus = new System.Windows.Forms.Button();
            this.btnSectionPopularity = new System.Windows.Forms.Button();
            this.btnReviewsByReviewer = new System.Windows.Forms.Button();
            this.btnAverageScores = new System.Windows.Forms.Button();
            this.btnSectionScores = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();

            this.lblTableTitle = new System.Windows.Forms.Label();
            this.dgvStatistics = new System.Windows.Forms.DataGridView();
            this.btnExportReport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.summaryPanel.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatistics)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(331, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(121, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Статистика";

            // summaryPanel
            this.summaryPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.summaryPanel.Controls.Add(this.lblParticipantsTitle);
            this.summaryPanel.Controls.Add(this.lblReportsTitle);
            this.summaryPanel.Controls.Add(this.lblSectionsTitle);
            this.summaryPanel.Controls.Add(this.lblReviewsTitle);
            this.summaryPanel.Controls.Add(this.lblProgramTitle);
            this.summaryPanel.Controls.Add(this.lblVisitsTitle);
            this.summaryPanel.Controls.Add(this.lblParticipantsValue);
            this.summaryPanel.Controls.Add(this.lblReportsValue);
            this.summaryPanel.Controls.Add(this.lblSectionsValue);
            this.summaryPanel.Controls.Add(this.lblReviewsValue);
            this.summaryPanel.Controls.Add(this.lblProgramValue);
            this.summaryPanel.Controls.Add(this.lblVisitsValue);
            this.summaryPanel.Location = new System.Drawing.Point(35, 80);
            this.summaryPanel.Name = "summaryPanel";
            this.summaryPanel.Size = new System.Drawing.Size(820, 135);
            this.summaryPanel.TabIndex = 1;

            // lblParticipantsTitle
            this.lblParticipantsTitle.AutoSize = true;
            this.lblParticipantsTitle.Location = new System.Drawing.Point(25, 18);
            this.lblParticipantsTitle.Name = "lblParticipantsTitle";
            this.lblParticipantsTitle.Size = new System.Drawing.Size(92, 20);
            this.lblParticipantsTitle.TabIndex = 0;
            this.lblParticipantsTitle.Text = "Участники";

            // lblReportsTitle
            this.lblReportsTitle.AutoSize = true;
            this.lblReportsTitle.Location = new System.Drawing.Point(165, 18);
            this.lblReportsTitle.Name = "lblReportsTitle";
            this.lblReportsTitle.Size = new System.Drawing.Size(78, 20);
            this.lblReportsTitle.TabIndex = 1;
            this.lblReportsTitle.Text = "Доклады";

            // lblSectionsTitle
            this.lblSectionsTitle.AutoSize = true;
            this.lblSectionsTitle.Location = new System.Drawing.Point(305, 18);
            this.lblSectionsTitle.Name = "lblSectionsTitle";
            this.lblSectionsTitle.Size = new System.Drawing.Size(66, 20);
            this.lblSectionsTitle.TabIndex = 2;
            this.lblSectionsTitle.Text = "Секции";

            // lblReviewsTitle
            this.lblReviewsTitle.AutoSize = true;
            this.lblReviewsTitle.Location = new System.Drawing.Point(445, 18);
            this.lblReviewsTitle.Name = "lblReviewsTitle";
            this.lblReviewsTitle.Size = new System.Drawing.Size(84, 20);
            this.lblReviewsTitle.TabIndex = 3;
            this.lblReviewsTitle.Text = "Рецензии";

            // lblProgramTitle
            this.lblProgramTitle.AutoSize = true;
            this.lblProgramTitle.Location = new System.Drawing.Point(585, 18);
            this.lblProgramTitle.Name = "lblProgramTitle";
            this.lblProgramTitle.Size = new System.Drawing.Size(97, 20);
            this.lblProgramTitle.TabIndex = 4;
            this.lblProgramTitle.Text = "Программа";

            // lblVisitsTitle
            this.lblVisitsTitle.AutoSize = true;
            this.lblVisitsTitle.Location = new System.Drawing.Point(705, 18);
            this.lblVisitsTitle.Name = "lblVisitsTitle";
            this.lblVisitsTitle.Size = new System.Drawing.Size(67, 20);
            this.lblVisitsTitle.TabIndex = 5;
            this.lblVisitsTitle.Text = "Записи";

            // lblParticipantsValue
            this.lblParticipantsValue.Location = new System.Drawing.Point(25, 60);
            this.lblParticipantsValue.Name = "lblParticipantsValue";
            this.lblParticipantsValue.Size = new System.Drawing.Size(100, 40);
            this.lblParticipantsValue.TabIndex = 6;
            this.lblParticipantsValue.Text = "0";
            this.lblParticipantsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblReportsValue
            this.lblReportsValue.Location = new System.Drawing.Point(165, 60);
            this.lblReportsValue.Name = "lblReportsValue";
            this.lblReportsValue.Size = new System.Drawing.Size(100, 40);
            this.lblReportsValue.TabIndex = 7;
            this.lblReportsValue.Text = "0";
            this.lblReportsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblSectionsValue
            this.lblSectionsValue.Location = new System.Drawing.Point(305, 60);
            this.lblSectionsValue.Name = "lblSectionsValue";
            this.lblSectionsValue.Size = new System.Drawing.Size(100, 40);
            this.lblSectionsValue.TabIndex = 8;
            this.lblSectionsValue.Text = "0";
            this.lblSectionsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblReviewsValue
            this.lblReviewsValue.Location = new System.Drawing.Point(445, 60);
            this.lblReviewsValue.Name = "lblReviewsValue";
            this.lblReviewsValue.Size = new System.Drawing.Size(100, 40);
            this.lblReviewsValue.TabIndex = 9;
            this.lblReviewsValue.Text = "0";
            this.lblReviewsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblProgramValue
            this.lblProgramValue.Location = new System.Drawing.Point(585, 60);
            this.lblProgramValue.Name = "lblProgramValue";
            this.lblProgramValue.Size = new System.Drawing.Size(100, 40);
            this.lblProgramValue.TabIndex = 10;
            this.lblProgramValue.Text = "0";
            this.lblProgramValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblVisitsValue
            this.lblVisitsValue.Location = new System.Drawing.Point(705, 60);
            this.lblVisitsValue.Name = "lblVisitsValue";
            this.lblVisitsValue.Size = new System.Drawing.Size(80, 40);
            this.lblVisitsValue.TabIndex = 11;
            this.lblVisitsValue.Text = "0";
            this.lblVisitsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // buttonsPanel
            this.buttonsPanel.Controls.Add(this.btnParticipantsByRole);
            this.buttonsPanel.Controls.Add(this.btnReportsByStatus);
            this.buttonsPanel.Controls.Add(this.btnSectionPopularity);
            this.buttonsPanel.Controls.Add(this.btnReviewsByReviewer);
            this.buttonsPanel.Controls.Add(this.btnAverageScores);
            this.buttonsPanel.Controls.Add(this.btnSectionScores);
            this.buttonsPanel.Controls.Add(this.btnRefresh);
            this.buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.buttonsPanel.Location = new System.Drawing.Point(35, 245);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(250, 430);
            this.buttonsPanel.TabIndex = 2;
            this.buttonsPanel.WrapContents = false;

            // btnParticipantsByRole
            this.btnParticipantsByRole.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnParticipantsByRole.Name = "btnParticipantsByRole";
            this.btnParticipantsByRole.Size = new System.Drawing.Size(250, 50);
            this.btnParticipantsByRole.TabIndex = 0;
            this.btnParticipantsByRole.TabStop = false;
            this.btnParticipantsByRole.Text = "Участники по ролям";
            this.btnParticipantsByRole.UseVisualStyleBackColor = true;
            this.btnParticipantsByRole.Click += new System.EventHandler(this.btnParticipantsByRole_Click);

            // btnReportsByStatus
            this.btnReportsByStatus.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnReportsByStatus.Name = "btnReportsByStatus";
            this.btnReportsByStatus.Size = new System.Drawing.Size(250, 50);
            this.btnReportsByStatus.TabIndex = 1;
            this.btnReportsByStatus.TabStop = false;
            this.btnReportsByStatus.Text = "Доклады по статусам";
            this.btnReportsByStatus.UseVisualStyleBackColor = true;
            this.btnReportsByStatus.Click += new System.EventHandler(this.btnReportsByStatus_Click);

            // btnSectionPopularity
            this.btnSectionPopularity.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnSectionPopularity.Name = "btnSectionPopularity";
            this.btnSectionPopularity.Size = new System.Drawing.Size(250, 50);
            this.btnSectionPopularity.TabIndex = 2;
            this.btnSectionPopularity.TabStop = false;
            this.btnSectionPopularity.Text = "Популярность секций";
            this.btnSectionPopularity.UseVisualStyleBackColor = true;
            this.btnSectionPopularity.Click += new System.EventHandler(this.btnSectionPopularity_Click);

            // btnReviewsByReviewer
            this.btnReviewsByReviewer.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnReviewsByReviewer.Name = "btnReviewsByReviewer";
            this.btnReviewsByReviewer.Size = new System.Drawing.Size(250, 50);
            this.btnReviewsByReviewer.TabIndex = 3;
            this.btnReviewsByReviewer.TabStop = false;
            this.btnReviewsByReviewer.Text = "Рецензенты";
            this.btnReviewsByReviewer.UseVisualStyleBackColor = true;
            this.btnReviewsByReviewer.Click += new System.EventHandler(this.btnReviewsByReviewer_Click);

            // btnAverageScores
            this.btnAverageScores.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnAverageScores.Name = "btnAverageScores";
            this.btnAverageScores.Size = new System.Drawing.Size(250, 50);
            this.btnAverageScores.TabIndex = 4;
            this.btnAverageScores.TabStop = false;
            this.btnAverageScores.Text = "Средние оценки";
            this.btnAverageScores.UseVisualStyleBackColor = true;
            this.btnAverageScores.Click += new System.EventHandler(this.btnAverageScores_Click);

            // btnSectionScores
            this.btnSectionScores.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnSectionScores.Name = "btnSectionScores";
            this.btnSectionScores.Size = new System.Drawing.Size(250, 50);
            this.btnSectionScores.TabIndex = 5;
            this.btnSectionScores.TabStop = false;
            this.btnSectionScores.Text = "Оценка секций";
            this.btnSectionScores.UseVisualStyleBackColor = true;
            this.btnSectionScores.Click += new System.EventHandler(this.btnSectionScores_Click);

            // btnRefresh
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(250, 45);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // lblTableTitle
            this.lblTableTitle.AutoSize = true;
            this.lblTableTitle.Location = new System.Drawing.Point(310, 245);
            this.lblTableTitle.Name = "lblTableTitle";
            this.lblTableTitle.Size = new System.Drawing.Size(253, 20);
            this.lblTableTitle.TabIndex = 3;
            this.lblTableTitle.Text = "Количество участников по ролям";

            // dgvStatistics
            this.dgvStatistics.Location = new System.Drawing.Point(310, 285);
            this.dgvStatistics.Name = "dgvStatistics";
            this.dgvStatistics.RowHeadersWidth = 51;
            this.dgvStatistics.RowTemplate.Height = 24;
            this.dgvStatistics.Size = new System.Drawing.Size(545, 380);
            this.dgvStatistics.TabIndex = 4;
            this.dgvStatistics.TabStop = false;

            // btnExportReport
            this.btnExportReport.Location = new System.Drawing.Point(310, 690);
            this.btnExportReport.Name = "btnExportReport";
            this.btnExportReport.Size = new System.Drawing.Size(200, 39);
            this.btnExportReport.TabIndex = 5;
            this.btnExportReport.TabStop = false;
            this.btnExportReport.Text = "Сформировать отчет";
            this.btnExportReport.UseVisualStyleBackColor = true;
            this.btnExportReport.Click += new System.EventHandler(this.btnExportReport_Click);

            // btnClose
            this.btnClose.Location = new System.Drawing.Point(735, 690);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 39);
            this.btnClose.TabIndex = 6;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "Назад";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // StatisticsForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 750);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.summaryPanel);
            this.Controls.Add(this.buttonsPanel);
            this.Controls.Add(this.lblTableTitle);
            this.Controls.Add(this.dgvStatistics);
            this.Controls.Add(this.btnExportReport);
            this.Controls.Add(this.btnClose);
            this.Name = "StatisticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Статистика";

            this.summaryPanel.ResumeLayout(false);
            this.summaryPanel.PerformLayout();
            this.buttonsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatistics)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}