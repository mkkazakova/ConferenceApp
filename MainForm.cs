using System;
using System.Windows.Forms;

namespace ConferenceApp
{
    public partial class MainForm : Form
    {
        private int currentUserId;
        private string currentUserName;
        private string currentUserRole;
        private LoginForm loginForm;
        private bool returnToLogin = false;

        public MainForm(int userId, string fullName, string role, LoginForm login)
        {
            InitializeComponent();

            currentUserId = userId;
            currentUserName = fullName;
            currentUserRole = role;
            loginForm = login;

            lblUser.Text = "Пользователь: " + currentUserName;
            lblRole.Text = "Роль: " + currentUserRole;

            ConfigureAccessByRole();

            btnProfile.Click += btnProfile_Click;
        }

        private void ConfigureAccessByRole()
        {
            btnProfile.Visible = true;

            btnParticipants.Visible = false;
            btnReports.Visible = false;
            btnSections.Visible = false;
            btnReviews.Visible = false;
            btnProgram.Visible = false;
            btnMaterials.Visible = false;
            btnStatistics.Visible = false;

            if (currentUserRole == "Участник")
            {
                btnReports.Visible = true;
                btnSections.Visible = true;
                btnProgram.Visible = true;
                btnMaterials.Visible = true;
            }
            else if (currentUserRole == "Рецензент")
            {
                btnReports.Visible = true;
                btnReviews.Visible = true;
                btnProgram.Visible = true;
            }
            else if (currentUserRole == "Организатор")
            {
                btnParticipants.Visible = true;
                btnReports.Visible = true;
                btnSections.Visible = true;
                btnReviews.Visible = true;
                btnProgram.Visible = true;
                btnMaterials.Visible = true;
                btnStatistics.Visible = true;
            }
            else if (currentUserRole == "Администратор")
            {
                btnParticipants.Visible = true;
                btnReports.Visible = true;
                btnSections.Visible = true;
                btnReviews.Visible = true;
                btnProgram.Visible = true;
                btnMaterials.Visible = true;
                btnStatistics.Visible = true;
            }
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            ProfileForm profileForm = new ProfileForm(currentUserId);
            profileForm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            returnToLogin = true;
            loginForm.Show();
            this.Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            if (!returnToLogin)
            {
                Application.Exit();
            }
        }
    }
}