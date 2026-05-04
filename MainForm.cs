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
            btnReports.Click += btnReports_Click;
            btnSections.Click += btnSections_Click;
            btnProgram.Click += btnProgram_Click;
            btnMaterials.Click += btnMaterials_Click;
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

            btnReports.Text = "Доклады";

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
                btnReports.Text = "Доклады для рецензирования";

                btnReviews.Visible = false;
                btnSections.Visible = false;
                btnProgram.Visible = false;
                btnMaterials.Visible = false;
                btnParticipants.Visible = false;
                btnStatistics.Visible = false;
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
            ProfileForm profileForm = new ProfileForm(currentUserId, currentUserRole);
            profileForm.ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            if (currentUserRole == "Рецензент")
            {
                ReviewsForm reviewsForm = new ReviewsForm(currentUserId);
                reviewsForm.ShowDialog();
            }
            else
            {
                ReportsForm reportsForm = new ReportsForm(currentUserId);
                reportsForm.ShowDialog();
            }
        }

        private void btnSections_Click(object sender, EventArgs e)
        {
            SectionsForm sectionsForm = new SectionsForm(currentUserId, currentUserRole);
            sectionsForm.ShowDialog();
        }

        private void btnProgram_Click(object sender, EventArgs e)
        {
            ProgramForm programForm = new ProgramForm(currentUserRole);
            programForm.ShowDialog();
        }

        private void btnMaterials_Click(object sender, EventArgs e)
        {
            MaterialsForm materialsForm = new MaterialsForm();
            materialsForm.ShowDialog();
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