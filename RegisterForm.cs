using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ConferenceApp
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();

            cmbParticipantStatus.SelectedIndex = 0;
            cmbAcademicDegree.SelectedIndex = 0;
            rbParticipant.Checked = true;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string lastName = txtLastName.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string middleName = txtMiddleName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string workplace = txtWorkplace.Text.Trim();
            string academicDegree = cmbAcademicDegree.Text == "Нет" ? "" : cmbAcademicDegree.Text;
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string participantStatus = cmbParticipantStatus.Text;

            if (lastName == "" || firstName == "" || email == "" || password == "")
            {
                MessageBox.Show("Заполните фамилию, имя, email и пароль.");
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Некорректный email.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают.");
                return;
            }

            if (password.Length <= 4)
            {
                MessageBox.Show("Пароль должен содержать больше 4 символов.");
                return;
            }

            if (EmailExistsInDatabase(email))
            {
                MessageBox.Show("Пользователь с таким email уже существует.");
                return;
            }

            if (ReviewerRequestStorage.EmailExists(email))
            {
                MessageBox.Show("Заявка с таким email уже подана.");
                return;
            }

            string passwordHash = GetSha256Hash(password);

            if (rbParticipant.Checked)
            {
                RegisterParticipant(
                    lastName,
                    firstName,
                    middleName,
                    email,
                    phone,
                    participantStatus,
                    workplace,
                    academicDegree,
                    passwordHash
                );
            }
            else
            {
                RegisterReviewerRequest(
                    lastName,
                    firstName,
                    middleName,
                    email,
                    phone,
                    workplace,
                    academicDegree,
                    passwordHash
                );
            }
        }

        private bool EmailExistsInDatabase(string email)
        {
            string query = @"
                SELECT COUNT(*)
                FROM dbo.tb_participants
                WHERE email = @Email;
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Email", email)
            };

            object result = Database.ExecuteScalar(query, parameters);

            return result != null && Convert.ToInt32(result) > 0;
        }

        private void RegisterParticipant(
            string lastName,
            string firstName,
            string middleName,
            string email,
            string phone,
            string participantStatus,
            string workplace,
            string academicDegree,
            string passwordHash)
        {
            string query = @"
                INSERT INTO dbo.tb_participants
                (
                    last_name,
                    first_name,
                    middle_name,
                    email,
                    phone,
                    participant_status,
                    user_role,
                    workplace,
                    academic_degree,
                    password_hash
                )
                VALUES
                (
                    @LastName,
                    @FirstName,
                    @MiddleName,
                    @Email,
                    @Phone,
                    @ParticipantStatus,
                    N'Участник',
                    @Workplace,
                    @AcademicDegree,
                    @PasswordHash
                );
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@LastName", lastName),
                new SqlParameter("@FirstName", firstName),
                new SqlParameter("@MiddleName", string.IsNullOrWhiteSpace(middleName) ? (object)DBNull.Value : middleName),
                new SqlParameter("@Email", email),
                new SqlParameter("@Phone", string.IsNullOrWhiteSpace(phone) ? (object)DBNull.Value : phone),
                new SqlParameter("@ParticipantStatus", participantStatus),
                new SqlParameter("@Workplace", string.IsNullOrWhiteSpace(workplace) ? (object)DBNull.Value : workplace),
                new SqlParameter("@AcademicDegree", string.IsNullOrWhiteSpace(academicDegree) ? (object)DBNull.Value : academicDegree),
                new SqlParameter("@PasswordHash", passwordHash)
            };

            int result = Database.ExecuteNonQuery(query, parameters);

            if (result > 0)
            {
                MessageBox.Show("Регистрация участника выполнена.");
                Close();
            }
        }

        private void RegisterReviewerRequest(
            string lastName,
            string firstName,
            string middleName,
            string email,
            string phone,
            string workplace,
            string academicDegree,
            string passwordHash)
        {
            ReviewerRequest request = new ReviewerRequest
            {
                LastName = lastName,
                FirstName = firstName,
                MiddleName = middleName,
                Email = email,
                Phone = phone,
                Workplace = workplace,
                AcademicDegree = academicDegree,
                PasswordHash = passwordHash,
                Status = "На рассмотрении"
            };

            ReviewerRequestStorage.Add(request);

            MessageBox.Show("Заявка на роль рецензента отправлена администратору.");
            Close();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}