namespace ConferenceApp
{
    partial class ProgramForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flowProgram;

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
            this.flowProgram = new System.Windows.Forms.FlowLayoutPanel();

            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 58, 95);
            this.lblTitle.Location = new System.Drawing.Point(330, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(305, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Программа конференции";

            // flowProgram
            this.flowProgram.Location = new System.Drawing.Point(30, 75);
            this.flowProgram.Name = "flowProgram";
            this.flowProgram.Size = new System.Drawing.Size(940, 450);
            this.flowProgram.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.flowProgram.AutoScroll = true;
            this.flowProgram.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowProgram.WrapContents = false;
            this.flowProgram.TabIndex = 1;

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(30, 555);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(190, 35);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Добавить доклад";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnUpdate
            this.btnUpdate.Location = new System.Drawing.Point(240, 555);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(190, 35);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Изменить доклад";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(450, 555);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(210, 35);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Удалить из программы";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnClose
            this.btnClose.Location = new System.Drawing.Point(830, 555);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 35);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Назад";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // ProgramForm
            this.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.ClientSize = new System.Drawing.Size(1000, 620);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.flowProgram);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClose);

            this.Name = "ProgramForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Программа конференции";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}