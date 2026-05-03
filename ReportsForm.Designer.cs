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

            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 58, 95);
            this.lblTitle.Location = new System.Drawing.Point(330, 25);
            this.lblTitle.Text = "Мои доклады";

            this.dgvReports.Location = new System.Drawing.Point(30, 75);
            this.dgvReports.Size = new System.Drawing.Size(940, 230);
            this.dgvReports.ReadOnly = true;
            this.dgvReports.AllowUserToAddRows = false;
            this.dgvReports.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReports.MultiSelect = false;
            this.dgvReports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReports.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReports_CellClick);

            this.lblTopic.Location = new System.Drawing.Point(30, 330);
            this.lblTopic.Size = new System.Drawing.Size(130, 20);
            this.lblTopic.Text = "Тема:";

            this.txtTopic.Location = new System.Drawing.Point(170, 327);
            this.txtTopic.Size = new System.Drawing.Size(800, 22);

            this.lblAnnotation.Location = new System.Drawing.Point(30, 365);
            this.lblAnnotation.Size = new System.Drawing.Size(130, 20);
            this.lblAnnotation.Text = "Аннотация:";

            this.txtAnnotation.Location = new System.Drawing.Point(170, 362);
            this.txtAnnotation.Size = new System.Drawing.Size(800, 70);
            this.txtAnnotation.Multiline = true;
            this.txtAnnotation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            this.lblKeywords.Location = new System.Drawing.Point(30, 450);
            this.lblKeywords.Size = new System.Drawing.Size(130, 20);
            this.lblKeywords.Text = "Ключевые слова:";

            this.txtKeywords.Location = new System.Drawing.Point(170, 447);
            this.txtKeywords.Size = new System.Drawing.Size(800, 22);

            this.lblStatus.Location = new System.Drawing.Point(30, 485);
            this.lblStatus.Size = new System.Drawing.Size(130, 20);
            this.lblStatus.Text = "Статус:";

            this.txtReviewStatus.Location = new System.Drawing.Point(170, 482);
            this.txtReviewStatus.Size = new System.Drawing.Size(250, 22);
            this.txtReviewStatus.ReadOnly = true;

            this.lblFilePath.Location = new System.Drawing.Point(30, 520);
            this.lblFilePath.Size = new System.Drawing.Size(130, 20);
            this.lblFilePath.Text = "Файл:";

            this.txtFilePath.Location = new System.Drawing.Point(170, 517);
            this.txtFilePath.Size = new System.Drawing.Size(660, 22);

            this.btnChooseFile.Location = new System.Drawing.Point(840, 515);
            this.btnChooseFile.Size = new System.Drawing.Size(130, 28);
            this.btnChooseFile.Text = "Выбрать";
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);

            this.btnAdd.Location = new System.Drawing.Point(170, 560);
            this.btnAdd.Size = new System.Drawing.Size(140, 35);
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnUpdate.Location = new System.Drawing.Point(325, 560);
            this.btnUpdate.Size = new System.Drawing.Size(140, 35);
            this.btnUpdate.Text = "Изменить";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            this.btnDelete.Location = new System.Drawing.Point(480, 560);
            this.btnDelete.Size = new System.Drawing.Size(140, 35);
            this.btnDelete.Text = "Удалить";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnClear.Location = new System.Drawing.Point(635, 560);
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
            this.Controls.Add(this.lblAnnotation);
            this.Controls.Add(this.txtAnnotation);
            this.Controls.Add(this.lblKeywords);
            this.Controls.Add(this.txtKeywords);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtReviewStatus);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnChooseFile);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnClose);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Доклады";

            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}