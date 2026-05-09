using System.Drawing;
using System.Windows.Forms;

namespace ConferenceApp
{
    public static class AppTheme
    {
        public static readonly Color Dark = ColorTranslator.FromHtml("#49225B");
        public static readonly Color Primary = ColorTranslator.FromHtml("#6E3482");
        public static readonly Color Accent = ColorTranslator.FromHtml("#A56ABD");
        public static readonly Color Light = ColorTranslator.FromHtml("#E7DBEF");
        public static readonly Color Background = ColorTranslator.FromHtml("#F5EBFA");

        public static readonly Font DefaultFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font TitleFont = new Font("Segoe UI", 14F, FontStyle.Bold);
        public static readonly Font ButtonFont = new Font("Segoe UI", 10F, FontStyle.Bold);

        public static void ApplyFormStyle(Form form)
        {
            form.BackColor = Background;
            form.Font = DefaultFont;
            form.ForeColor = Dark;

            ApplyControlsStyle(form.Controls);
        }

        private static void ApplyControlsStyle(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Button button)
                {
                    ApplyButtonStyle(button);
                }
                else if (control is TextBox textBox)
                {
                    ApplyTextBoxStyle(textBox);
                }
                else if (control is ComboBox comboBox)
                {
                    ApplyComboBoxStyle(comboBox);
                }
                else if (control is LinkLabel linkLabel)
                {
                    ApplyLinkLabelStyle(linkLabel);
                }
                else if (control is Label label)
                {
                    ApplyLabelStyle(label);
                }
                else if (control is Panel panel)
                {
                    ApplyPanelStyle(panel);
                }
                else if (control is DataGridView dataGridView)
                {
                    ApplyDataGridViewStyle(dataGridView);
                }

                if (control.HasChildren)
                {
                    ApplyControlsStyle(control.Controls);
                }
            }
        }

        private static void ApplyButtonStyle(Button button)
        {
            button.BackColor = Primary;
            button.ForeColor = Background;
            button.Font = ButtonFont;

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Dark;
            button.FlatAppearance.MouseDownBackColor = Accent;

            button.Cursor = Cursors.Hand;
            button.Height = 36;
            button.UseVisualStyleBackColor = false;
        }

        private static void ApplyTextBoxStyle(TextBox textBox)
        {
            textBox.BackColor = Color.White;
            textBox.ForeColor = Dark;
            textBox.Font = DefaultFont;
            textBox.BorderStyle = BorderStyle.FixedSingle;
        }

        private static void ApplyComboBoxStyle(ComboBox comboBox)
        {
            comboBox.BackColor = Color.White;
            comboBox.ForeColor = Dark;
            comboBox.Font = DefaultFont;
            comboBox.FlatStyle = FlatStyle.Flat;
        }

        private static void ApplyLinkLabelStyle(LinkLabel linkLabel)
        {
            linkLabel.ForeColor = Primary;
            linkLabel.LinkColor = Primary;
            linkLabel.ActiveLinkColor = Dark;
            linkLabel.VisitedLinkColor = Accent;
            linkLabel.Font = DefaultFont;
        }

        private static void ApplyLabelStyle(Label label)
        {
            label.ForeColor = Dark;
            label.Font = DefaultFont;
        }

        private static void ApplyPanelStyle(Panel panel)
        {
            panel.BackColor = Light;
        }

        private static void ApplyDataGridViewStyle(DataGridView dataGridView)
        {
            dataGridView.BackgroundColor = Background;
            dataGridView.GridColor = Light;
            dataGridView.BorderStyle = BorderStyle.None;

            dataGridView.DefaultCellStyle.BackColor = Color.White;
            dataGridView.DefaultCellStyle.ForeColor = Dark;
            dataGridView.DefaultCellStyle.SelectionBackColor = Accent;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.DefaultCellStyle.Font = DefaultFont;

            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Primary;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = ButtonFont;

            dataGridView.EnableHeadersVisualStyles = false;
        }
    }
}