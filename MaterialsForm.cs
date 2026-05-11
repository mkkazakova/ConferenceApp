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
            AppTheme.ApplyFormStyle(this);

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
                        m.id_material,
                        m.material_title,
                        m.material_description,
                        COALESCE(r.keywords, s.section_name, N'') AS keywords,
                        m.file_name,
                        m.file_extension,

                        COALESCE(creator.last_name, author.last_name, N'') AS last_name,
                        COALESCE(creator.first_name, author.first_name, N'') AS first_name,
                        COALESCE(creator.middle_name, author.middle_name, N'') AS middle_name,
                        COALESCE(creator.workplace, author.workplace, N'') AS workplace
                    FROM dbo.tb_materials AS m
                    LEFT JOIN dbo.tb_reports AS r
                        ON m.id_report = r.id_report
                    LEFT JOIN dbo.tb_sections AS s
                        ON m.id_section = s.id_section
                    LEFT JOIN dbo.tb_participants AS creator
                        ON m.created_by = creator.id_participant
                    LEFT JOIN dbo.tb_participants AS author
                        ON r.id_author = author.id_participant
                    WHERE
                        @Search = N''
                        OR m.material_title LIKE N'%' + @Search + N'%'
                        OR m.material_description LIKE N'%' + @Search + N'%'
                        OR m.file_name LIKE N'%' + @Search + N'%'
                        OR r.topic LIKE N'%' + @Search + N'%'
                        OR r.keywords LIKE N'%' + @Search + N'%'
                        OR s.section_name LIKE N'%' + @Search + N'%'
                        OR creator.last_name LIKE N'%' + @Search + N'%'
                        OR creator.first_name LIKE N'%' + @Search + N'%'
                        OR creator.middle_name LIKE N'%' + @Search + N'%'
                        OR author.last_name LIKE N'%' + @Search + N'%'
                        OR author.first_name LIKE N'%' + @Search + N'%'
                        OR author.middle_name LIKE N'%' + @Search + N'%'
                    ORDER BY m.material_title;
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
                    emptyLabel.Font = AppTheme.DefaultFont;
                    emptyLabel.ForeColor = AppTheme.CardSecondaryText;
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
            int materialId = Convert.ToInt32(row["id_material"]);

            string title = row["material_title"] == DBNull.Value ? "" : row["material_title"].ToString();
            string description = row["material_description"] == DBNull.Value ? "" : row["material_description"].ToString();
            string keywords = row["keywords"] == DBNull.Value ? "" : row["keywords"].ToString();
            string fileName = row["file_name"] == DBNull.Value ? "" : row["file_name"].ToString();
            string workplace = row["workplace"] == DBNull.Value ? "" : row["workplace"].ToString();

            string firstName = row["first_name"] == DBNull.Value ? "" : row["first_name"].ToString();
            string middleName = row["middle_name"] == DBNull.Value ? "" : row["middle_name"].ToString();
            string lastName = row["last_name"] == DBNull.Value ? "" : row["last_name"].ToString();

            string author = GetShortName(firstName, middleName, lastName);

            if (string.IsNullOrWhiteSpace(author))
                author = "не указан";

            Panel card = new Panel();
            card.Width = flowMaterials.ClientSize.Width - 35;
            card.Height = 145;
            card.Margin = new Padding(5, 3, 5, 10);

            AppTheme.ApplyCardStyle(card);

            Label lblTopic = new Label();
            lblTopic.Text = title;
            lblTopic.Location = new Point(15, 12);
            lblTopic.Size = new Size(760, 25);
            lblTopic.AutoEllipsis = true;
            AppTheme.ApplyCardTitleLabelStyle(lblTopic);

            Label lblAuthor = new Label();
            lblAuthor.Text = "Автор: " + author + (workplace == "" ? "" : "    " + workplace);
            lblAuthor.Location = new Point(15, 40);
            lblAuthor.Size = new Size(760, 22);
            lblAuthor.AutoEllipsis = true;
            AppTheme.ApplyCardSecondaryLabelStyle(lblAuthor);

            Label lblAnnotation = new Label();
            lblAnnotation.Text = "Описание: " + description;
            lblAnnotation.Location = new Point(15, 65);
            lblAnnotation.Size = new Size(760, 22);
            lblAnnotation.AutoEllipsis = true;
            AppTheme.ApplyCardMainLabelStyle(lblAnnotation);

            Label lblKeywords = new Label();
            lblKeywords.Text = "Ключевые слова / секция: " + keywords;
            lblKeywords.Location = new Point(15, 90);
            lblKeywords.Size = new Size(620, 22);
            lblKeywords.AutoEllipsis = true;
            AppTheme.ApplyCardMainLabelStyle(lblKeywords);

            Label lblFile = new Label();
            lblFile.Text = fileName == "" ? "Файл: не указан" : "Файл: " + fileName;
            lblFile.Location = new Point(15, 115);
            lblFile.Size = new Size(620, 22);
            lblFile.AutoEllipsis = true;
            AppTheme.ApplyCardSecondaryLabelStyle(lblFile);

            Button btnOpen = new Button();
            btnOpen.Text = "Открыть файл";
            btnOpen.Size = new Size(140, 30);
            btnOpen.Location = new Point(card.Width - 160, 105);
            btnOpen.Tag = materialId;
            btnOpen.Enabled = fileName != "";
            btnOpen.Click += btnOpenFile_Click;

            AppTheme.ApplyButtonStyle(btnOpen);

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

            int materialId = Convert.ToInt32(button.Tag);

            try
            {
                string query = @"
                    SELECT
                        file_name,
                        file_extension,
                        file_content
                    FROM dbo.tb_materials
                    WHERE id_material = @MaterialId;
                ";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@MaterialId", materialId)
                };

                DataTable table = Database.ExecuteSelect(query, parameters);

                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("Файл не найден в базе данных.");
                    return;
                }

                DataRow row = table.Rows[0];

                if (row["file_content"] == DBNull.Value)
                {
                    MessageBox.Show("Файл не прикреплен.");
                    return;
                }

                string fileName = row["file_name"] == DBNull.Value ? "" : row["file_name"].ToString();
                string fileExtension = row["file_extension"] == DBNull.Value ? ".pdf" : row["file_extension"].ToString();
                byte[] fileContent = (byte[])row["file_content"];

                if (fileContent.Length == 0)
                {
                    MessageBox.Show("Файл пустой.");
                    return;
                }

                string tempFilePath = SaveFileToTemp(materialId, fileName, fileExtension, fileContent);

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = tempFilePath;
                startInfo.UseShellExecute = true;

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка открытия файла: " + ex.Message);
            }
        }

        private string SaveFileToTemp(int materialId, string fileName, string fileExtension, byte[] fileContent)
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), "ConferenceApp", "Materials");
            Directory.CreateDirectory(tempDirectory);

            if (string.IsNullOrWhiteSpace(fileExtension))
                fileExtension = ".pdf";

            if (!fileExtension.StartsWith("."))
                fileExtension = "." + fileExtension;

            if (string.IsNullOrWhiteSpace(fileName))
                fileName = "material_" + materialId + fileExtension;

            fileName = GetSafeFileName(fileName);

            if (string.IsNullOrWhiteSpace(Path.GetExtension(fileName)))
                fileName += fileExtension;

            string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);

            string tempFileName = nameWithoutExtension + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
            string tempFilePath = Path.Combine(tempDirectory, tempFileName);

            File.WriteAllBytes(tempFilePath, fileContent);

            return tempFilePath;
        }

        private string GetSafeFileName(string fileName)
        {
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(invalidChar, '_');
            }

            return fileName;
        }

        private string GetShortName(string firstName, string middleName, string lastName)
        {
            string result = "";

            if (!string.IsNullOrWhiteSpace(firstName))
                result += firstName.Substring(0, 1) + ".";

            if (!string.IsNullOrWhiteSpace(middleName))
                result += middleName.Substring(0, 1) + ".";

            if (!string.IsNullOrWhiteSpace(lastName))
                result += " " + lastName;

            return result.Trim();
        }

        private void CenterTitle()
        {
            lblTitle.Left = (ClientSize.Width - lblTitle.Width) / 2;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}