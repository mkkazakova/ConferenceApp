namespace ConferenceApp
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblMiddleName;
        private System.Windows.Forms.TextBox txtMiddleName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblParticipantStatus;
        private System.Windows.Forms.ComboBox cmbParticipantStatus;
        private System.Windows.Forms.Label lblWorkplace;
        private System.Windows.Forms.TextBox txtWorkplace;
        private System.Windows.Forms.Label lblAcademicDegree;
        private System.Windows.Forms.ComboBox cmbAcademicDegree;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.GroupBox gbRole;
        private System.Windows.Forms.RadioButton rbParticipant;
        private System.Windows.Forms.RadioButton rbReviewer;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnCancel;

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
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblMiddleName = new System.Windows.Forms.Label();
            this.txtMiddleName = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblParticipantStatus = new System.Windows.Forms.Label();
            this.cmbParticipantStatus = new System.Windows.Forms.ComboBox();
            this.lblWorkplace = new System.Windows.Forms.Label();
            this.txtWorkplace = new System.Windows.Forms.TextBox();
            this.lblAcademicDegree = new System.Windows.Forms.Label();
            this.cmbAcademicDegree = new System.Windows.Forms.ComboBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.gbRole = new System.Windows.Forms.GroupBox();
            this.rbParticipant = new System.Windows.Forms.RadioButton();
            this.rbReviewer = new System.Windows.Forms.RadioButton();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbRole.SuspendLayout();
            this.SuspendLayout();

            // lblLastName
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(30, 25);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(59, 13);
            this.lblLastName.Text = "Фамилия:";

            // txtLastName
            this.txtLastName.Location = new System.Drawing.Point(170, 22);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(250, 20);

            // lblFirstName
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(30, 60);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(32, 13);
            this.lblFirstName.Text = "Имя:";

            // txtFirstName
            this.txtFirstName.Location = new System.Drawing.Point(170, 57);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(250, 20);

            // lblMiddleName
            this.lblMiddleName.AutoSize = true;
            this.lblMiddleName.Location = new System.Drawing.Point(30, 95);
            this.lblMiddleName.Name = "lblMiddleName";
            this.lblMiddleName.Size = new System.Drawing.Size(57, 13);
            this.lblMiddleName.Text = "Отчество:";

            // txtMiddleName
            this.txtMiddleName.Location = new System.Drawing.Point(170, 92);
            this.txtMiddleName.Name = "txtMiddleName";
            this.txtMiddleName.Size = new System.Drawing.Size(250, 20);

            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(30, 130);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(35, 13);
            this.lblEmail.Text = "Email:";

            // txtEmail
            this.txtEmail.Location = new System.Drawing.Point(170, 127);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(250, 20);

            // lblPhone
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(30, 165);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(55, 13);
            this.lblPhone.Text = "Телефон:";

            // txtPhone
            this.txtPhone.Location = new System.Drawing.Point(170, 162);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(250, 20);

            // lblParticipantStatus
            this.lblParticipantStatus.AutoSize = true;
            this.lblParticipantStatus.Location = new System.Drawing.Point(30, 200);
            this.lblParticipantStatus.Name = "lblParticipantStatus";
            this.lblParticipantStatus.Size = new System.Drawing.Size(88, 13);
            this.lblParticipantStatus.Text = "Статус участия:";

            // cmbParticipantStatus
            this.cmbParticipantStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParticipantStatus.Items.AddRange(new object[]
            {
                "Слушатель",
                "Докладчик"
            });
            this.cmbParticipantStatus.Location = new System.Drawing.Point(170, 197);
            this.cmbParticipantStatus.Name = "cmbParticipantStatus";
            this.cmbParticipantStatus.Size = new System.Drawing.Size(250, 21);

            // lblWorkplace
            this.lblWorkplace.AutoSize = true;
            this.lblWorkplace.Location = new System.Drawing.Point(30, 235);
            this.lblWorkplace.Name = "lblWorkplace";
            this.lblWorkplace.Size = new System.Drawing.Size(83, 13);
            this.lblWorkplace.Text = "Место работы:";

            // txtWorkplace
            this.txtWorkplace.Location = new System.Drawing.Point(170, 232);
            this.txtWorkplace.Name = "txtWorkplace";
            this.txtWorkplace.Size = new System.Drawing.Size(250, 20);

            // lblAcademicDegree
            this.lblAcademicDegree.AutoSize = true;
            this.lblAcademicDegree.Location = new System.Drawing.Point(30, 270);
            this.lblAcademicDegree.Name = "lblAcademicDegree";
            this.lblAcademicDegree.Size = new System.Drawing.Size(91, 13);
            this.lblAcademicDegree.Text = "Учёная степень:";

            // cmbAcademicDegree
            this.cmbAcademicDegree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAcademicDegree.Items.AddRange(new object[]
            {
                "Нет",
                "бакалавр",
                "магистр",
                "аспирант",
                "к.т.н.",
                "к.ф.-м.н.",
                "д.т.н.",
                "д.ф.-м.н."
            });
            this.cmbAcademicDegree.Location = new System.Drawing.Point(170, 267);
            this.cmbAcademicDegree.Name = "cmbAcademicDegree";
            this.cmbAcademicDegree.Size = new System.Drawing.Size(250, 21);

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(30, 305);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(48, 13);
            this.lblPassword.Text = "Пароль:";

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(170, 302);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(250, 20);

            // lblConfirmPassword
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Location = new System.Drawing.Point(30, 340);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(96, 13);
            this.lblConfirmPassword.Text = "Повтор пароля:";

            // txtConfirmPassword
            this.txtConfirmPassword.Location = new System.Drawing.Point(170, 337);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(250, 20);

            // gbRole
            this.gbRole.Controls.Add(this.rbParticipant);
            this.gbRole.Controls.Add(this.rbReviewer);
            this.gbRole.Location = new System.Drawing.Point(33, 375);
            this.gbRole.Name = "gbRole";
            this.gbRole.Size = new System.Drawing.Size(387, 65);
            this.gbRole.Text = "Тип регистрации";

            // rbParticipant
            this.rbParticipant.AutoSize = true;
            this.rbParticipant.Location = new System.Drawing.Point(20, 28);
            this.rbParticipant.Name = "rbParticipant";
            this.rbParticipant.Size = new System.Drawing.Size(75, 17);
            this.rbParticipant.Text = "Участник";
            this.rbParticipant.UseVisualStyleBackColor = true;

            // rbReviewer
            this.rbReviewer.AutoSize = true;
            this.rbReviewer.Location = new System.Drawing.Point(160, 28);
            this.rbReviewer.Name = "rbReviewer";
            this.rbReviewer.Size = new System.Drawing.Size(141, 17);
            this.rbReviewer.Text = "Заявка на рецензента";
            this.rbReviewer.UseVisualStyleBackColor = true;

            // btnRegister
            this.btnRegister.Location = new System.Drawing.Point(130, 465);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(160, 35);
            this.btnRegister.Text = "Зарегистрироваться";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(300, 465);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // RegisterForm
            this.ClientSize = new System.Drawing.Size(460, 530);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblMiddleName);
            this.Controls.Add(this.txtMiddleName);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblParticipantStatus);
            this.Controls.Add(this.cmbParticipantStatus);
            this.Controls.Add(this.lblWorkplace);
            this.Controls.Add(this.txtWorkplace);
            this.Controls.Add(this.lblAcademicDegree);
            this.Controls.Add(this.cmbAcademicDegree);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.gbRole);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnCancel);
            this.Name = "RegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Регистрация";

            this.gbRole.ResumeLayout(false);
            this.gbRole.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}