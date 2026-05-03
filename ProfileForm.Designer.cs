namespace ConferenceApp
{
    partial class ProfileForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;

        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;

        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;

        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cmbRole;

        private System.Windows.Forms.Label lblWorkplace;
        private System.Windows.Forms.TextBox txtWorkplace;

        private System.Windows.Forms.Label lblAcademicDegree;
        private System.Windows.Forms.ComboBox cmbAcademicDegree;

        private System.Windows.Forms.Button btnSaveProfile;

        private System.Windows.Forms.GroupBox groupPassword;
        private System.Windows.Forms.Label lblOldPassword;
        private System.Windows.Forms.TextBox txtOldPassword;

        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.TextBox txtNewPassword;

        private System.Windows.Forms.Label lblRepeatPassword;
        private System.Windows.Forms.TextBox txtRepeatPassword;

        private System.Windows.Forms.Button btnChangePassword;
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

            this.lblFullName = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();

            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();

            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();

            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();

            this.lblRole = new System.Windows.Forms.Label();
            this.cmbRole = new System.Windows.Forms.ComboBox();

            this.lblWorkplace = new System.Windows.Forms.Label();
            this.txtWorkplace = new System.Windows.Forms.TextBox();

            this.lblAcademicDegree = new System.Windows.Forms.Label();
            this.cmbAcademicDegree = new System.Windows.Forms.ComboBox();

            this.btnSaveProfile = new System.Windows.Forms.Button();

            this.groupPassword = new System.Windows.Forms.GroupBox();
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.txtOldPassword = new System.Windows.Forms.TextBox();

            this.lblNewPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();

            this.lblRepeatPassword = new System.Windows.Forms.Label();
            this.txtRepeatPassword = new System.Windows.Forms.TextBox();

            this.btnChangePassword = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.groupPassword.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 58, 95);
            this.lblTitle.Location = new System.Drawing.Point(35, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(236, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Личный кабинет";

            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(40, 85);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(46, 16);
            this.lblFullName.TabIndex = 1;
            this.lblFullName.Text = "ФИО:";

            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(210, 82);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.ReadOnly = true;
            this.txtFullName.Size = new System.Drawing.Size(360, 22);
            this.txtFullName.TabIndex = 2;

            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(40, 120);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(44, 16);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Email:";

            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(210, 117);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(360, 22);
            this.txtEmail.TabIndex = 4;

            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(40, 155);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(70, 16);
            this.lblPhone.TabIndex = 5;
            this.lblPhone.Text = "Телефон:";

            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(210, 152);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.ReadOnly = true;
            this.txtPhone.Size = new System.Drawing.Size(360, 22);
            this.txtPhone.TabIndex = 6;

            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(40, 190);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 16);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Статус:";

            // 
            // cmbStatus
            // 
            this.cmbStatus.Location = new System.Drawing.Point(210, 187);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(360, 24);
            this.cmbStatus.TabIndex = 8;

            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(40, 225);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(44, 16);
            this.lblRole.TabIndex = 9;
            this.lblRole.Text = "Роль:";

            // 
            // cmbRole
            // 
            this.cmbRole.Location = new System.Drawing.Point(210, 222);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(360, 24);
            this.cmbRole.TabIndex = 10;

            // 
            // lblWorkplace
            // 
            this.lblWorkplace.AutoSize = true;
            this.lblWorkplace.Location = new System.Drawing.Point(40, 260);
            this.lblWorkplace.Name = "lblWorkplace";
            this.lblWorkplace.Size = new System.Drawing.Size(103, 16);
            this.lblWorkplace.TabIndex = 11;
            this.lblWorkplace.Text = "Место работы:";

            // 
            // txtWorkplace
            // 
            this.txtWorkplace.Location = new System.Drawing.Point(210, 257);
            this.txtWorkplace.Name = "txtWorkplace";
            this.txtWorkplace.ReadOnly = true;
            this.txtWorkplace.Size = new System.Drawing.Size(360, 22);
            this.txtWorkplace.TabIndex = 12;

            // 
            // lblAcademicDegree
            // 
            this.lblAcademicDegree.AutoSize = true;
            this.lblAcademicDegree.Location = new System.Drawing.Point(40, 295);
            this.lblAcademicDegree.Name = "lblAcademicDegree";
            this.lblAcademicDegree.Size = new System.Drawing.Size(114, 16);
            this.lblAcademicDegree.TabIndex = 13;
            this.lblAcademicDegree.Text = "Ученая степень:";

            // 
            // cmbAcademicDegree
            // 
            this.cmbAcademicDegree.Location = new System.Drawing.Point(210, 292);
            this.cmbAcademicDegree.Name = "cmbAcademicDegree";
            this.cmbAcademicDegree.Size = new System.Drawing.Size(360, 24);
            this.cmbAcademicDegree.TabIndex = 14;

            // 
            // btnSaveProfile
            // 
            this.btnSaveProfile.BackColor = System.Drawing.Color.FromArgb(194, 224, 255);
            this.btnSaveProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveProfile.Location = new System.Drawing.Point(390, 330);
            this.btnSaveProfile.Name = "btnSaveProfile";
            this.btnSaveProfile.Size = new System.Drawing.Size(180, 32);
            this.btnSaveProfile.TabIndex = 15;
            this.btnSaveProfile.Text = "Сохранить данные";
            this.btnSaveProfile.UseVisualStyleBackColor = false;
            this.btnSaveProfile.Click += new System.EventHandler(this.btnSaveProfile_Click);

            // 
            // groupPassword
            // 
            this.groupPassword.Controls.Add(this.lblOldPassword);
            this.groupPassword.Controls.Add(this.txtOldPassword);
            this.groupPassword.Controls.Add(this.lblNewPassword);
            this.groupPassword.Controls.Add(this.txtNewPassword);
            this.groupPassword.Controls.Add(this.lblRepeatPassword);
            this.groupPassword.Controls.Add(this.txtRepeatPassword);
            this.groupPassword.Controls.Add(this.btnChangePassword);
            this.groupPassword.Location = new System.Drawing.Point(40, 380);
            this.groupPassword.Name = "groupPassword";
            this.groupPassword.Size = new System.Drawing.Size(530, 190);
            this.groupPassword.TabIndex = 16;
            this.groupPassword.TabStop = false;
            this.groupPassword.Text = "Изменение пароля";

            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Location = new System.Drawing.Point(20, 35);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(109, 16);
            this.lblOldPassword.TabIndex = 0;
            this.lblOldPassword.Text = "Старый пароль:";

            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Location = new System.Drawing.Point(170, 32);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '*';
            this.txtOldPassword.Size = new System.Drawing.Size(320, 22);
            this.txtOldPassword.TabIndex = 1;

            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Location = new System.Drawing.Point(20, 75);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(101, 16);
            this.lblNewPassword.TabIndex = 2;
            this.lblNewPassword.Text = "Новый пароль:";

            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(170, 72);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(320, 22);
            this.txtNewPassword.TabIndex = 3;

            // 
            // lblRepeatPassword
            // 
            this.lblRepeatPassword.AutoSize = true;
            this.lblRepeatPassword.Location = new System.Drawing.Point(20, 115);
            this.lblRepeatPassword.Name = "lblRepeatPassword";
            this.lblRepeatPassword.Size = new System.Drawing.Size(124, 16);
            this.lblRepeatPassword.TabIndex = 4;
            this.lblRepeatPassword.Text = "Повторите пароль:";

            // 
            // txtRepeatPassword
            // 
            this.txtRepeatPassword.Location = new System.Drawing.Point(170, 112);
            this.txtRepeatPassword.Name = "txtRepeatPassword";
            this.txtRepeatPassword.PasswordChar = '*';
            this.txtRepeatPassword.Size = new System.Drawing.Size(320, 22);
            this.txtRepeatPassword.TabIndex = 5;

            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(194, 224, 255);
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Location = new System.Drawing.Point(170, 145);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(180, 30);
            this.btnChangePassword.TabIndex = 6;
            this.btnChangePassword.Text = "Изменить пароль";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);

            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(450, 590);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 35);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // 
            // ProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.ClientSize = new System.Drawing.Size(620, 650);
            this.Controls.Add(this.lblTitle);

            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.txtFullName);

            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);

            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtPhone);

            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbStatus);

            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.cmbRole);

            this.Controls.Add(this.lblWorkplace);
            this.Controls.Add(this.txtWorkplace);

            this.Controls.Add(this.lblAcademicDegree);
            this.Controls.Add(this.cmbAcademicDegree);

            this.Controls.Add(this.btnSaveProfile);
            this.Controls.Add(this.groupPassword);
            this.Controls.Add(this.btnClose);

            this.Name = "ProfileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Личный кабинет";

            this.groupPassword.ResumeLayout(false);
            this.groupPassword.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}