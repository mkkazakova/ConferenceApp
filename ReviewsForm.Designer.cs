namespace ConferenceApp
{
    partial class ReviewsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvReports;

        private System.Windows.Forms.Label lblTopic;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblReportStatus;
        private System.Windows.Forms.Label lblAnnotation;
        private System.Windows.Forms.Label lblKeywords;
        private System.Windows.Forms.Label lblFilePath;

        private System.Windows.Forms.TextBox txtTopic;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtReportStatus;
        private System.Windows.Forms.TextBox txtAnnotation;
        private System.Windows.Forms.TextBox txtKeywords;
        private System.Windows.Forms.TextBox txtFilePath;

        private System.Windows.Forms.Label lblNovelty;
        private System.Windows.Forms.Label lblRelevance;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblComments;

        private System.Windows.Forms.NumericUpDown numNovelty;
        private System.Windows.Forms.NumericUpDown numRelevance;
        private System.Windows.Forms.NumericUpDown numQuality;
        private System.Windows.Forms.ComboBox cmbResult;
        private System.Windows.Forms.TextBox txtComments;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
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
            this.dgvReports = new System.Windows.Forms.DataGridView();

            this.lblTopic = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblReportStatus = new System.Windows.Forms.Label();
            this.lblAnnotation = new System.Windows.Forms.Label();
            this.lblKeywords = new System.Windows.Forms.Label();
            this.lblFilePath = new System.Windows.Forms.Label();

            this.txtTopic = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.txtReportStatus = new System.Windows.Forms.TextBox();
            this.txtAnnotation = new System.Windows.Forms.TextBox();
            this.txtKeywords = new System.Windows.Forms.TextBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();

            this.lblNovelty = new System.Windows.Forms.Label();
            this.lblRelevance = new System.Windows.Forms.Label();
            this.lblQuality = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblComments = new System.Windows.Forms.Label();

            this.numNovelty = new System.Windows.Forms.NumericUpDown();
            this.numRelevance = new System.Windows.Forms.NumericUpDown();
            this.numQuality = new System.Windows.Forms.NumericUpDown();
            this.cmbResult = new System.Windows.Forms.ComboBox();
            this.txtComments = new System.Windows.Forms.TextBox();

            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNovelty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRelevance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuality)).BeginInit();

            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 58, 95);
            this.lblTitle.Location = new System.Drawing.Point(370, 25);
            this.lblTitle.Text = "Рецензирование докладов";

            this.dgvReports.Location = new System.Drawing.Point(30, 75);
            this.dgvReports.Size = new System.Drawing.Size(940, 210);
            this.dgvReports.ReadOnly = true;
            this.dgvReports.AllowUserToAddRows = false;
            this.dgvReports.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReports.MultiSelect = false;
            this.dgvReports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReports.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReports_CellClick);

            this.lblTopic.Location = new System.Drawing.Point(30, 305);
            this.lblTopic.Size = new System.Drawing.Size(130, 20);
            this.lblTopic.Text = "Тема:";

            this.txtTopic.Location = new System.Drawing.Point(170, 302);
            this.txtTopic.Size = new System.Drawing.Size(800, 22);
            this.txtTopic.ReadOnly = true;

            this.lblAuthor.Location = new System.Drawing.Point(30, 335);
            this.lblAuthor.Size = new System.Drawing.Size(130, 20);
            this.lblAuthor.Text = "Автор:";

            this.txtAuthor.Location = new System.Drawing.Point(170, 332);
            this.txtAuthor.Size = new System.Drawing.Size(390, 22);
            this.txtAuthor.ReadOnly = true;

            this.lblReportStatus.Location = new System.Drawing.Point(580, 335);
            this.lblReportStatus.Size = new System.Drawing.Size(120, 20);
            this.lblReportStatus.Text = "Статус:";

            this.txtReportStatus.Location = new System.Drawing.Point(700, 332);
            this.txtReportStatus.Size = new System.Drawing.Size(270, 22);
            this.txtReportStatus.ReadOnly = true;

            this.lblAnnotation.Location = new System.Drawing.Point(30, 365);
            this.lblAnnotation.Size = new System.Drawing.Size(130, 20);
            this.lblAnnotation.Text = "Аннотация:";

            this.txtAnnotation.Location = new System.Drawing.Point(170, 362);
            this.txtAnnotation.Size = new System.Drawing.Size(800, 45);
            this.txtAnnotation.Multiline = true;
            this.txtAnnotation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAnnotation.ReadOnly = true;

            this.lblKeywords.Location = new System.Drawing.Point(30, 420);
            this.lblKeywords.Size = new System.Drawing.Size(130, 20);
            this.lblKeywords.Text = "Ключевые слова:";

            this.txtKeywords.Location = new System.Drawing.Point(170, 417);
            this.txtKeywords.Size = new System.Drawing.Size(390, 22);
            this.txtKeywords.ReadOnly = true;

            this.lblFilePath.Location = new System.Drawing.Point(580, 420);
            this.lblFilePath.Size = new System.Drawing.Size(120, 20);
            this.lblFilePath.Text = "Файл:";

            this.txtFilePath.Location = new System.Drawing.Point(700, 417);
            this.txtFilePath.Size = new System.Drawing.Size(270, 22);
            this.txtFilePath.ReadOnly = true;

            this.lblNovelty.Location = new System.Drawing.Point(30, 455);
            this.lblNovelty.Size = new System.Drawing.Size(130, 20);
            this.lblNovelty.Text = "Новизна:";

            this.numNovelty.Location = new System.Drawing.Point(170, 452);
            this.numNovelty.Size = new System.Drawing.Size(80, 22);
            this.numNovelty.Minimum = 1;
            this.numNovelty.Maximum = 10;
            this.numNovelty.Value = 1;

            this.lblRelevance.Location = new System.Drawing.Point(280, 455);
            this.lblRelevance.Size = new System.Drawing.Size(120, 20);
            this.lblRelevance.Text = "Актуальность:";

            this.numRelevance.Location = new System.Drawing.Point(405, 452);
            this.numRelevance.Size = new System.Drawing.Size(80, 22);
            this.numRelevance.Minimum = 1;
            this.numRelevance.Maximum = 10;
            this.numRelevance.Value = 1;

            this.lblQuality.Location = new System.Drawing.Point(515, 455);
            this.lblQuality.Size = new System.Drawing.Size(100, 20);
            this.lblQuality.Text = "Качество:";

            this.numQuality.Location = new System.Drawing.Point(620, 452);
            this.numQuality.Size = new System.Drawing.Size(80, 22);
            this.numQuality.Minimum = 1;
            this.numQuality.Maximum = 10;
            this.numQuality.Value = 1;

            this.lblResult.Location = new System.Drawing.Point(730, 455);
            this.lblResult.Size = new System.Drawing.Size(90, 20);
            this.lblResult.Text = "Решение:";

            this.cmbResult.Location = new System.Drawing.Point(820, 452);
            this.cmbResult.Size = new System.Drawing.Size(150, 24);
            this.cmbResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResult.Items.AddRange(new object[]
            {
                "Принят",
                "Отклонен",
                "На доработку"
            });

            this.lblComments.Location = new System.Drawing.Point(30, 490);
            this.lblComments.Size = new System.Drawing.Size(130, 20);
            this.lblComments.Text = "Комментарий:";

            this.txtComments.Location = new System.Drawing.Point(170, 487);
            this.txtComments.Size = new System.Drawing.Size(800, 55);
            this.txtComments.Multiline = true;
            this.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            this.btnSave.Location = new System.Drawing.Point(170, 560);
            this.btnSave.Size = new System.Drawing.Size(180, 35);
            this.btnSave.Text = "Сохранить рецензию";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnClear.Location = new System.Drawing.Point(365, 560);
            this.btnClear.Size = new System.Drawing.Size(140, 35);
            this.btnClear.Text = "Очистить";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            this.btnClose.Location = new System.Drawing.Point(830, 560);
            this.btnClose.Size = new System.Drawing.Size(140, 35);
            this.btnClose.Text = "Назад";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.ClientSize = new System.Drawing.Size(1000, 620);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvReports);

            this.Controls.Add(this.lblTopic);
            this.Controls.Add(this.txtTopic);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.lblReportStatus);
            this.Controls.Add(this.txtReportStatus);
            this.Controls.Add(this.lblAnnotation);
            this.Controls.Add(this.txtAnnotation);
            this.Controls.Add(this.lblKeywords);
            this.Controls.Add(this.txtKeywords);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.txtFilePath);

            this.Controls.Add(this.lblNovelty);
            this.Controls.Add(this.numNovelty);
            this.Controls.Add(this.lblRelevance);
            this.Controls.Add(this.numRelevance);
            this.Controls.Add(this.lblQuality);
            this.Controls.Add(this.numQuality);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.cmbResult);
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.txtComments);

            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnClose);

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Рецензирование докладов";

            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNovelty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRelevance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuality)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}