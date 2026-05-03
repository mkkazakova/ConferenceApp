using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ConferenceApp
{
    public partial class MaterialsForm : Form
    {
        public MaterialsForm()
        {
            InitializeComponent();

            CenterTitle();
            LoadMaterials();
        }

        private void LoadMaterials()
        {
            try
            {
                flowMaterials.Controls.Clear();

                string searchText = txtSearch.Text.Trim();

                string query = @"
                    SELECT
                        r.topic,
                        r.annotation,
                        r.keywords,
                        r.file_path,
                        p.last_name,
                        p.first_name,
                        p.middle_name,
                        p.workplace
                    FROM dbo.tb_reports AS r
                    INNER JOIN dbo.tb_participants AS p
                        ON r.id_author = p.id_participant
                    WHERE r.review_status = N'Принят'
                      AND (
                            @Search = N''
                            OR r.topic LIKE N'%' + @Search + N'%'
                            OR p.last_name LIKE N'%' + @Search + N'%'
                            OR p.first_name LIKE N'%' + @Search + N'%'
                            OR p.middle_name LIKE N'%' + @Search + N'%'
                            OR p.last_name + N' ' + p.first_name + N' ' + ISNULL(p.middle_name, N'') LIKE N'%' + @Search + N'%'
                          )
                    ORDER BY r.topic;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Search", searchText)
                };

                DataTable table = Database.ExecuteSelect(query, parameters);

                if (table.Rows.Count == 0)
                {
                    Label emptyLabel = new Label();
                    emptyLabel.Text = "Материалы не найдены.";
                    emptyLabel.AutoSize = true;
                    emptyLabel.Font = new Font("Microsoft Sans Serif", 11F);
                    emptyLabel.ForeColor = Color.FromArgb(32, 58, 95);
                    emptyLabel.Margin = new Padding(10);

                    flowMaterials.Controls.Add(emptyLabel);
                    return;
                }

                foreach (DataRow row in table.Rows)
                {
                    AddMaterialCard(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки материалов: " + ex.Message);
            }
        }

        private void AddMaterialCard(DataRow row)
        {
            string topic = row["topic"].ToString();
            string annotation = row["annotation"] == DBNull.Value ? "" : row["annotation"].ToString();
            string keywords = row["keywords"] == DBNull.Value ? "" : row["keywords"].ToString();
            string filePath = row["file_path"] == DBNull.Value ? "" : row["file_path"].ToString();
            string workplace = row["workplace"] == DBNull.Value ? "" : row["workplace"].ToString();

            string firstName = row["first_name"].ToString();
            string middleName = row["middle_name"] == DBNull.Value ? "" : row["middle_name"].ToString();
            string lastName = row["last_name"].ToString();

            string author = GetShortName(firstName, middleName, lastName);

            Panel card = new Panel();
            card.Width = flowMaterials.ClientSize.Width - 35;
            card.Height = 145;
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(5, 3, 5, 10);

            Label lblTopic = new Label();
            lblTopic.Text = topic;
            lblTopic.Location = new Point(15, 12);
            lblTopic.Size = new Size(760, 25);
            lblTopic.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblTopic.ForeColor = Color.FromArgb(32, 58, 95);
            lblTopic.AutoEllipsis = true;

            Label lblAuthor = new Label();
            lblAuthor.Text = "Автор: " + author + (workplace == "" ? "" : "    " + workplace);
            lblAuthor.Location = new Point(15, 40);
            lblAuthor.Size = new Size(760, 22);
            lblAuthor.Font = new Font("Microsoft Sans Serif", 9F);
            lblAuthor.ForeColor = Color.DimGray;
            lblAuthor.AutoEllipsis = true;

            Label lblAnnotation = new Label();
            lblAnnotation.Text = "Аннотация: " + annotation;
            lblAnnotation.Location = new Point(15, 65);
            lblAnnotation.Size = new Size(760, 22);
            lblAnnotation.Font = new Font("Microsoft Sans Serif", 9F);
            lblAnnotation.ForeColor = Color.Black;
            lblAnnotation.AutoEllipsis = true;

            Label lblKeywords = new Label();
            lblKeywords.Text = "Ключевые слова: " + keywords;
            lblKeywords.Location = new Point(15, 90);
            lblKeywords.Size = new Size(620, 22);
            lblKeywords.Font = new Font("Microsoft Sans Serif", 9F);
            lblKeywords.ForeColor = Color.Black;
            lblKeywords.AutoEllipsis = true;

            Label lblFile = new Label();
            lblFile.Text = filePath == "" ? "Файл: не указан" : "Файл: " + filePath;
            lblFile.Location = new Point(15, 115);
            lblFile.Size = new Size(620, 22);
            lblFile.Font = new Font("Microsoft Sans Serif", 9F);
            lblFile.ForeColor = Color.DimGray;
            lblFile.AutoEllipsis = true;

            Button btnOpen = new Button();
            btnOpen.Text = "Открыть файл";
            btnOpen.Size = new Size(140, 30);
            btnOpen.Location = new Point(card.Width - 160, 105);
            btnOpen.Tag = filePath;
            btnOpen.Enabled = filePath != "";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpenFile_Click;

            card.Controls.Add(lblTopic);
            card.Controls.Add(lblAuthor);
            card.Controls.Add(lblAnnotation);
            card.Controls.Add(lblKeywords);
            card.Controls.Add(lblFile);
            card.Controls.Add(btnOpen);

            flowMaterials.Controls.Add(card);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadMaterials();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button == null || button.Tag == null)
                return;

            string filePath = button.Tag.ToString();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Файл не указан.");
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл не найден:\n" + filePath);
                return;
            }

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = filePath;
                startInfo.UseShellExecute = true;

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка открытия файла: " + ex.Message);
            }
        }

        private string GetShortName(string firstName, string middleName, string lastName)
        {
            string result = "";

            if (!string.IsNullOrWhiteSpace(firstName))
                result += firstName.Substring(0, 1) + ".";

            if (!string.IsNullOrWhiteSpace(middleName))
                result += middleName.Substring(0, 1) + ".";

            result += " " + lastName;

            return result.Trim();
        }

        private void CenterTitle()
        {
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}