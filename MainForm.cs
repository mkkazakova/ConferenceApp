using System;
using System.Drawing;
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

            AppTheme.ApplyFormStyle(this);
            ApplyMainFormStyle();

            ConfigureAccessByRole();
            ArrangeVisibleButtons();

            btnProfile.Click += btnProfile_Click;
            btnParticipants.Click += btnParticipants_Click;
            btnReports.Click += btnReports_Click;
            btnSections.Click += btnSections_Click;
            btnReviews.Click += btnReviews_Click;
            btnProgram.Click += btnProgram_Click;
            btnMaterials.Click += btnMaterials_Click;
            btnStatistics.Click += btnStatistics_Click;
            btnExit.Click += btnExit_Click;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            ConfigureAccessByRole();
            ArrangeVisibleButtons();
        }

        private void ApplyMainFormStyle()
        {
            lblTitle.Font = AppTheme.TitleFont;
            lblTitle.ForeColor = AppTheme.Dark;

            lblInfo.Font = AppTheme.HeaderFont;
            lblInfo.ForeColor = AppTheme.Dark;

            lblUser.Font = AppTheme.DefaultFont;
            lblUser.ForeColor = AppTheme.Dark;

            lblRole.Font = AppTheme.DefaultFont;
            lblRole.ForeColor = AppTheme.Dark;

            StyleExitButton();
        }

        private void StyleExitButton()
        {
            btnExit.BackColor = AppTheme.Primary;
            btnExit.ForeColor = AppTheme.Background;
            btnExit.Font = AppTheme.ButtonFont;

            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatAppearance.MouseOverBackColor = AppTheme.Dark;
            btnExit.FlatAppearance.MouseDownBackColor = AppTheme.Accent;

            btnExit.UseVisualStyleBackColor = false;
            btnExit.Cursor = Cursors.Hand;
        }

        private void ArrangeVisibleButtons()
        {
            Button[] buttons =
            {
                btnProfile,
                btnReports,
                btnReviews,
                btnParticipants,
                btnSections,
                btnProgram,
                btnMaterials,
                btnStatistics
            };

            int x = 185;
            int y = 175;
            int buttonWidth = 280;
            int buttonHeight = 44;
            int stepY = 60;

            foreach (Button button in buttons)
            {
                if (!button.Visible)
                    continue;

                button.Size = new Size(buttonWidth, buttonHeight);
                button.Location = new Point(x, y);
                button.BringToFront();

                y += stepY;
            }

            btnExit.Size = new Size(120, 40);
            btnExit.Location = new Point(490, 700);
            btnExit.BringToFront();
        }

        private void ConfigureAccessByRole()
        {
            btnProfile.Visible = true;

            btnReports.Visible = false;
            btnReviews.Visible = false;
            btnParticipants.Visible = false;
            btnSections.Visible = false;
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
            }
            else if (currentUserRole == "Организатор")
            {
                btnReports.Visible = true;
                btnReviews.Visible = true;
                btnParticipants.Visible = true;
                btnSections.Visible = true;
                btnProgram.Visible = true;
                btnMaterials.Visible = true;
                btnStatistics.Visible = true;
            }
            else if (currentUserRole == "Администратор")
            {
                btnReports.Visible = true;
                btnReviews.Visible = true;
                btnParticipants.Visible = true;
                btnSections.Visible = true;
                btnProgram.Visible = true;
                btnMaterials.Visible = true;
                btnStatistics.Visible = true;
            }
            else
            {
                MessageBox.Show("Некорректная роль пользователя.");
                returnToLogin = true;
                loginForm.Show();
                Close();
            }
        }

        private bool HasAccess(params string[] allowedRoles)
        {
            foreach (string role in allowedRoles)
            {
                if (currentUserRole == role)
                    return true;
            }

            MessageBox.Show("Недостаточно прав доступа.");
            return false;
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            ProfileForm profileForm = new ProfileForm(currentUserId, currentUserRole);
            profileForm.ShowDialog();
        }

        private void btnParticipants_Click(object sender, EventArgs e)
        {
            if (!HasAccess("Организатор", "Администратор"))
                return;

            ParticipantsForm participantsForm = new ParticipantsForm(currentUserRole);
            participantsForm.ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            if (!HasAccess("Участник", "Рецензент", "Организатор", "Администратор"))
                return;

            if (currentUserRole == "Рецензент")
            {
                ReviewsForm reviewsForm = new ReviewsForm(currentUserId, currentUserRole);
                reviewsForm.ShowDialog();
            }
            else
            {
                ReportsForm reportsForm = new ReportsForm(currentUserId, currentUserRole);
                reportsForm.ShowDialog();
            }
        }

        private void btnReviews_Click(object sender, EventArgs e)
        {
            if (!HasAccess("Организатор", "Администратор"))
                return;

            ReviewsForm reviewsForm = new ReviewsForm(currentUserId, currentUserRole);
            reviewsForm.ShowDialog();
        }

        private void btnSections_Click(object sender, EventArgs e)
        {
            if (!HasAccess("Участник", "Организатор", "Администратор"))
                return;

            SectionsForm sectionsForm = new SectionsForm(currentUserId, currentUserRole);
            sectionsForm.ShowDialog();
        }

        private void btnProgram_Click(object sender, EventArgs e)
        {
            if (!HasAccess("Участник", "Организатор", "Администратор"))
                return;

            ProgramForm programForm = new ProgramForm(currentUserId, currentUserRole);
            programForm.ShowDialog();
        }

        private void btnMaterials_Click(object sender, EventArgs e)
        {
            if (!HasAccess("Участник", "Организатор", "Администратор"))
                return;

            MaterialsForm materialsForm = new MaterialsForm();
            materialsForm.ShowDialog();
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            if (!HasAccess("Организатор", "Администратор"))
                return;

            StatisticsForm statisticsForm = new StatisticsForm(currentUserRole);
            statisticsForm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            returnToLogin = true;
            loginForm.Show();
            Close();
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