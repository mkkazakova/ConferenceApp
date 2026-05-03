namespace ConferenceApp
{
    partial class ProgramForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flowProgram;

        private System.Windows.Forms.Label lblReport;
        private System.Windows.Forms.Label lblSection;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblLocation;

        private System.Windows.Forms.ComboBox cmbReport;
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.TextBox txtLocation;

        private System.Windows.Forms.Button btnAdd;
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
            this.flowProgram = new System.Windows.Forms.FlowLayoutPanel();

            this.lblReport = new System.Windows.Forms.Label();
            this.lblSection = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();

            this.cmbReport = new System.Windows.Forms.ComboBox();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.txtLocation = new System.Windows.Forms.TextBox();

            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 58, 95);
            this.lblTitle.Location = new System.Drawing.Point(330, 25);
            this.lblTitle.Text = "Программа конференции";

            this.flowProgram.Location = new System.Drawing.Point(30, 75);
            this.flowProgram.Size = new System.Drawing.Size(940, 450);
            this.flowProgram.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.flowProgram.AutoScroll = true;
            this.flowProgram.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowProgram.WrapContents = false;

            this.lblReport.Location = new System.Drawing.Point(30, 405);
            this.lblReport.Size = new System.Drawing.Size(130, 20);
            this.lblReport.Text = "Доклад:";

            this.cmbReport.Location = new System.Drawing.Point(170, 402);
            this.cmbReport.Size = new System.Drawing.Size(800, 24);
            this.cmbReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblSection.Location = new System.Drawing.Point(30, 440);
            this.lblSection.Size = new System.Drawing.Size(130, 20);
            this.lblSection.Text = "Секция:";

            this.cmbSection.Location = new System.Drawing.Point(170, 437);
            this.cmbSection.Size = new System.Drawing.Size(800, 24);
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblDate.Location = new System.Drawing.Point(30, 475);
            this.lblDate.Size = new System.Drawing.Size(130, 20);
            this.lblDate.Text = "Дата:";

            this.dtpDate.Location = new System.Drawing.Point(170, 472);
            this.dtpDate.Size = new System.Drawing.Size(250, 22);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblTime.Location = new System.Drawing.Point(450, 475);
            this.lblTime.Size = new System.Drawing.Size(70, 20);
            this.lblTime.Text = "Время:";

            this.dtpTime.Location = new System.Drawing.Point(525, 472);
            this.dtpTime.Size = new System.Drawing.Size(150, 22);
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.ShowUpDown = true;

            this.lblLocation.Location = new System.Drawing.Point(30, 510);
            this.lblLocation.Size = new System.Drawing.Size(130, 20);
            this.lblLocation.Text = "Место:";

            this.txtLocation.Location = new System.Drawing.Point(170, 507);
            this.txtLocation.Size = new System.Drawing.Size(800, 22);

            this.btnAdd.Location = new System.Drawing.Point(170, 560);
            this.btnAdd.Size = new System.Drawing.Size(180, 35);
            this.btnAdd.Text = "Добавить в программу";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnClose.Location = new System.Drawing.Point(830, 560);
            this.btnClose.Size = new System.Drawing.Size(140, 35);
            this.btnClose.Text = "Назад";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.ClientSize = new System.Drawing.Size(1000, 620);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.flowProgram);

            this.Controls.Add(this.lblReport);
            this.Controls.Add(this.cmbReport);
            this.Controls.Add(this.lblSection);
            this.Controls.Add(this.cmbSection);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.txtLocation);

            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnClose);

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Программа конференции";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}