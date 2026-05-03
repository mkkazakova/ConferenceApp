using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ConferenceApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (email == "" || password == "")
            {
                MessageBox.Show("Введите email и пароль.");
                return;
            }

            string passwordHash = GetSha256Hash(password);

            string query = @"
                SELECT TOP 1
                    id_participant,
                    last_name + N' ' + first_name + N' ' + ISNULL(middle_name, N'') AS full_name,
                    email,
                    user_role
                FROM dbo.tb_participants
                WHERE email = @Email
                  AND password_hash = @PasswordHash;
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@PasswordHash", passwordHash)
            };

            DataTable table = Database.ExecuteSelect(query, parameters);

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("Неверный email или пароль.");
                return;
            }

            int userId = Convert.ToInt32(table.Rows[0]["id_participant"]);
            string fullName = table.Rows[0]["full_name"].ToString();
            string role = table.Rows[0]["user_role"].ToString();

            MainForm mainForm = new MainForm(userId, fullName, role, this);

            mainForm.Show();
            this.Hide();
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
    }
}