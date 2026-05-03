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
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnCancelRegister = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvSections)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 58, 95);
            this.lblTitle.Location = new System.Drawing.Point(360, 25);
            this.lblTitle.Text = "Секции конференции";

            this.dgvSections.Location = new System.Drawing.Point(30, 75);
            this.dgvSections.Size = new System.Drawing.Size(940, 230);
            this.dgvSections.ReadOnly = true;
            this.dgvSections.AllowUserToAddRows = false;
            this.dgvSections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSections.MultiSelect = false;
            this.dgvSections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSections.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSections_CellClick);

            this.lblSectionName.Location = new System.Drawing.Point(30, 330);
            this.lblSectionName.Size = new System.Drawing.Size(130, 20);
            this.lblSectionName.Text = "Название:";

            this.txtSectionName.Location = new System.Drawing.Point(170, 327);
            this.txtSectionName.Size = new System.Drawing.Size(800, 22);

            this.lblDescription.Location = new System.Drawing.Point(30, 365);
            this.lblDescription.Size = new System.Drawing.Size(130, 20);
            this.lblDescription.Text = "Описание:";

            this.txtDescription.Location = new System.Drawing.Point(170, 362);
            this.txtDescription.Size = new System.Drawing.Size(800, 55);
            this.txtDescription.Multiline = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            this.lblReports.Location = new System.Drawing.Point(30, 435);
            this.lblReports.Size = new System.Drawing.Size(130, 20);
            this.lblReports.Text = "Доклады:";

            this.lstReports.Location = new System.Drawing.Point(170, 432);
            this.lstReports.Size = new System.Drawing.Size(800, 95);
            this.lstReports.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.lstReports.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstReports.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lstReports.ItemHeight = 18;

            this.btnRegister.Location = new System.Drawing.Point(170, 560);
            this.btnRegister.Size = new System.Drawing.Size(140, 35);
            this.btnRegister.Text = "Записаться";
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            this.btnCancelRegister.Location = new System.Drawing.Point(325, 560);
            this.btnCancelRegister.Size = new System.Drawing.Size(140, 35);
            this.btnCancelRegister.Text = "Отменить";
            this.btnCancelRegister.Click += new System.EventHandler(this.btnCancelRegister_Click);

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

            this.btnClose.Location = new System.Drawing.Point(830, 560);
            this.btnClose.Size = new System.Drawing.Size(140, 35);
            this.btnClose.Text = "Назад";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.ClientSize = new System.Drawing.Size(1000, 620);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvSections);
            this.Controls.Add(this.lblSectionName);
            this.Controls.Add(this.txtSectionName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblReports);
            this.Controls.Add(this.lstReports);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnCancelRegister);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClose);

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Секции";

            ((System.ComponentModel.ISupportInitialize)(this.dgvSections)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}