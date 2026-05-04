using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ConferenceApp
{
    public partial class ProfileForm : Form
    {
        private int currentUserId;
        private string currentUserRole;

        public ProfileForm(int userId, string role)
        {
            InitializeComponent();

            currentUserId = userId;
            currentUserRole = role;

            InitComboBoxes();
            LoadUserData();
            ConfigureAccessByRole();
        }

        private void InitComboBoxes()
        {
            cmbStatus.Items.Clear();
            cmbRole.Items.Clear();
            cmbAcademicDegree.Items.Clear();

            cmbStatus.Items.AddRange(new object[]
            {
                "Докладчик",
                "Слушатель"
            });

            cmbRole.Items.AddRange(new object[]
            {
                "Участник",
                "Рецензент",
                "Организатор",
                "Администратор"
            });

            cmbAcademicDegree.Items.AddRange(new object[]
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

            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAcademicDegree.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbRole.Enabled = false;
        }

        private void ConfigureAccessByRole()
        {
            if (currentUserRole == "Рецензент")
            {
                lblStatus.Visible = false;
                cmbStatus.Visible = false;
            }
        }

        private void LoadUserData()
        {
            string query = @"
                SELECT
                    last_name + N' ' + first_name + N' ' + ISNULL(middle_name, N'') AS full_name,
                    email,
                    phone,
                    participant_status,
                    user_role,
                    workplace,
                    academic_degree
                FROM dbo.tb_participants
                WHERE id_participant = @UserId;
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", currentUserId)
            };

            DataTable table = Database.ExecuteSelect(query, parameters);

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("Пользователь не найден.");
                Close();
                return;
            }

            DataRow row = table.Rows[0];

            txtFullName.Text = row["full_name"].ToString();
            txtEmail.Text = row["email"].ToString();
            txtPhone.Text = row["phone"].ToString();
            txtWorkplace.Text = row["workplace"].ToString();

            cmbStatus.SelectedItem = row["participant_status"].ToString();
            cmbRole.SelectedItem = row["user_role"].ToString();

            string academicDegree = row["academic_degree"].ToString();

            if (string.IsNullOrWhiteSpace(academicDegree))
            {
                cmbAcademicDegree.SelectedItem = "Нет";
            }
            else
            {
                cmbAcademicDegree.SelectedItem = academicDegree;
            }
        }

        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            if (currentUserRole != "Рецензент" && cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Выберите статус.");
                return;
            }

            if (cmbAcademicDegree.SelectedItem == null)
            {
                MessageBox.Show("Выберите ученую степень.");
                return;
            }

            object academicDegreeValue;

            if (cmbAcademicDegree.SelectedItem.ToString() == "Нет")
            {
                academicDegreeValue = DBNull.Value;
            }
            else
            {
                academicDegreeValue = cmbAcademicDegree.SelectedItem.ToString();
            }

            string query;
            SqlParameter[] parameters;

            if (currentUserRole == "Рецензент")
            {
                query = @"
                    UPDATE dbo.tb_participants
                    SET academic_degree = @AcademicDegree
                    WHERE id_participant = @UserId;
                ";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@AcademicDegree", academicDegreeValue),
                    new SqlParameter("@UserId", currentUserId)
                };
            }
            else
            {
                query = @"
                    UPDATE dbo.tb_participants
                    SET participant_status = @Status,
                        academic_degree = @AcademicDegree
                    WHERE id_participant = @UserId;
                ";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@Status", cmbStatus.SelectedItem.ToString()),
                    new SqlParameter("@AcademicDegree", academicDegreeValue),
                    new SqlParameter("@UserId", currentUserId)
                };
            }

            int rows = Database.ExecuteNonQuery(query, parameters);

            if (rows > 0)
            {
                MessageBox.Show("Данные успешно сохранены.");
                LoadUserData();
                ConfigureAccessByRole();
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOldPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string repeatPassword = txtRepeatPassword.Text.Trim();

            if (oldPassword == "" || newPassword == "" || repeatPassword == "")
            {
                MessageBox.Show("Заполните все поля для смены пароля.");
                return;
            }

            if (newPassword != repeatPassword)
            {
                MessageBox.Show("Новый пароль и повтор пароля не совпадают.");
                return;
            }

            if (newPassword.Length < 5)
            {
                MessageBox.Show("Новый пароль должен содержать минимум 5 символов.");
                return;
            }

            string oldHash = GetSha256Hash(oldPassword);
            string newHash = GetSha256Hash(newPassword);

            string checkQuery = @"
                SELECT COUNT(*)
                FROM dbo.tb_participants
                WHERE id_participant = @UserId
                  AND password_hash = @OldHash;
            ";

            SqlParameter[] checkParameters =
            {
                new SqlParameter("@UserId", currentUserId),
                new SqlParameter("@OldHash", oldHash)
            };

            object result = Database.ExecuteScalar(checkQuery, checkParameters);
            int count = Convert.ToInt32(result);

            if (count == 0)
            {
                MessageBox.Show("Старый пароль указан неверно.");
                return;
            }

            string updateQuery = @"
                UPDATE dbo.tb_participants
                SET password_hash = @NewHash
                WHERE id_participant = @UserId;
            ";

            SqlParameter[] updateParameters =
            {
                new SqlParameter("@NewHash", newHash),
                new SqlParameter("@UserId", currentUserId)
            };

            int rows = Database.ExecuteNonQuery(updateQuery, updateParameters);

            if (rows > 0)
            {
                MessageBox.Show("Пароль успешно изменен.");

                txtOldPassword.Clear();
                txtNewPassword.Clear();
                txtRepeatPassword.Clear();
            }
        }

        private string GetSha256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();

                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}