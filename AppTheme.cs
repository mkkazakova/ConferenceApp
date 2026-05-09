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
        public static readonly Color White = Color.White;
        public static readonly Color Border = ColorTranslator.FromHtml("#D8C7E3");

        public static readonly Color CardBack = White;
        public static readonly Color CardSelectedBack = Light;
        public static readonly Color CardBorder = Border;
        public static readonly Color CardTitleText = Dark;
        public static readonly Color CardMainText = Dark;
        public static readonly Color CardSecondaryText = Primary;

        public static readonly Color TableAlternateRow = ColorTranslator.FromHtml("#F9F4FC");

        public static readonly Font DefaultFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font TitleFont = new Font("Segoe UI", 14F, FontStyle.Bold);
        public static readonly Font ButtonFont = new Font("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font HeaderFont = new Font("Segoe UI", 10F, FontStyle.Bold);

        public static void ApplyFormStyle(Form form)
        {
            form.BackColor = Background;
            form.Font = DefaultFont;
            form.ForeColor = Dark;
            form.StartPosition = FormStartPosition.CenterScreen;

            ApplyControlsStyle(form.Controls);
        }

        public static void ApplyControlsStyle(Control.ControlCollection controls)
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
                else if (control is RichTextBox richTextBox)
                {
                    ApplyRichTextBoxStyle(richTextBox);
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
                else if (control is GroupBox groupBox)
                {
                    ApplyGroupBoxStyle(groupBox);
                }
                else if (control is TabControl tabControl)
                {
                    ApplyTabControlStyle(tabControl);
                }
                else if (control is TabPage tabPage)
                {
                    ApplyTabPageStyle(tabPage);
                }
                else if (control is FlowLayoutPanel flowLayoutPanel)
                {
                    ApplyFlowLayoutPanelStyle(flowLayoutPanel);
                }
                else if (control is Panel panel)
                {
                    ApplyPanelStyle(panel);
                }
                else if (control is DataGridView dataGridView)
                {
                    ApplyDataGridViewStyle(dataGridView);
                }
                else if (control is ListBox listBox)
                {
                    ApplyListBoxStyle(listBox);
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    ApplyDateTimePickerStyle(dateTimePicker);
                }
                else if (control is NumericUpDown numericUpDown)
                {
                    ApplyNumericUpDownStyle(numericUpDown);
                }
                else if (control is CheckBox checkBox)
                {
                    ApplyCheckBoxStyle(checkBox);
                }
                else if (control is RadioButton radioButton)
                {
                    ApplyRadioButtonStyle(radioButton);
                }

                if (control.HasChildren)
                {
                    ApplyControlsStyle(control.Controls);
                }
            }
        }

        public static void ApplyButtonStyle(Button button)
        {
            button.BackColor = Primary;
            button.ForeColor = Background;
            button.Font = ButtonFont;

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Dark;
            button.FlatAppearance.MouseDownBackColor = Accent;

            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;

            if (button.Height < 32)
            {
                button.Height = 32;
            }
        }

        public static void ApplySecondaryButtonStyle(Button button)
        {
            button.BackColor = Light;
            button.ForeColor = Dark;
            button.Font = ButtonFont;

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Border;
            button.FlatAppearance.MouseOverBackColor = Accent;
            button.FlatAppearance.MouseDownBackColor = Primary;

            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;

            if (button.Height < 32)
            {
                button.Height = 32;
            }
        }

        public static void ApplyTextBoxStyle(TextBox textBox)
        {
            textBox.BackColor = White;
            textBox.ForeColor = Dark;
            textBox.Font = DefaultFont;
            textBox.BorderStyle = BorderStyle.FixedSingle;
        }

        public static void ApplyRichTextBoxStyle(RichTextBox richTextBox)
        {
            richTextBox.BackColor = White;
            richTextBox.ForeColor = Dark;
            richTextBox.Font = DefaultFont;
            richTextBox.BorderStyle = BorderStyle.FixedSingle;
        }

        public static void ApplyComboBoxStyle(ComboBox comboBox)
        {
            comboBox.BackColor = White;
            comboBox.ForeColor = Dark;
            comboBox.Font = DefaultFont;
            comboBox.FlatStyle = FlatStyle.Flat;
        }

        public static void ApplyLinkLabelStyle(LinkLabel linkLabel)
        {
            linkLabel.ForeColor = Primary;
            linkLabel.LinkColor = Primary;
            linkLabel.ActiveLinkColor = Dark;
            linkLabel.VisitedLinkColor = Accent;
            linkLabel.Font = DefaultFont;
        }

        public static void ApplyLabelStyle(Label label)
        {
            label.ForeColor = Dark;

            if (label.Text.Length > 0 && label.Text == label.FindForm()?.Text)
            {
                label.Font = TitleFont;
            }
            else
            {
                label.Font = DefaultFont;
            }
        }

        public static void ApplyCardTitleLabelStyle(Label label)
        {
            label.ForeColor = CardTitleText;
            label.Font = HeaderFont;
        }

        public static void ApplyCardMainLabelStyle(Label label)
        {
            label.ForeColor = CardMainText;
            label.Font = DefaultFont;
        }

        public static void ApplyCardSecondaryLabelStyle(Label label)
        {
            label.ForeColor = CardSecondaryText;
            label.Font = DefaultFont;
        }

        public static void ApplyGroupBoxStyle(GroupBox groupBox)
        {
            groupBox.BackColor = Background;
            groupBox.ForeColor = Dark;
            groupBox.Font = HeaderFont;
        }

        public static void ApplyPanelStyle(Panel panel)
        {
            panel.BackColor = Light;
        }

        public static void ApplyCardStyle(Panel card)
        {
            card.BackColor = CardBack;
            card.ForeColor = CardMainText;
            card.BorderStyle = BorderStyle.FixedSingle;
        }

        public static void ApplySelectedCardStyle(Panel card)
        {
            card.BackColor = CardSelectedBack;
            card.ForeColor = CardMainText;
            card.BorderStyle = BorderStyle.FixedSingle;
        }

        public static void ApplyFlowLayoutPanelStyle(FlowLayoutPanel panel)
        {
            panel.BackColor = Light;
        }

        public static void ApplyTabControlStyle(TabControl tabControl)
        {
            tabControl.BackColor = Background;
            tabControl.ForeColor = Dark;
            tabControl.Font = HeaderFont;
        }

        public static void ApplyTabPageStyle(TabPage tabPage)
        {
            tabPage.BackColor = Background;
            tabPage.ForeColor = Dark;
            tabPage.Font = DefaultFont;
        }

        public static void ApplyListBoxStyle(ListBox listBox)
        {
            listBox.BackColor = White;
            listBox.ForeColor = Dark;
            listBox.Font = DefaultFont;
            listBox.BorderStyle = BorderStyle.FixedSingle;
        }

        public static void ApplyDateTimePickerStyle(DateTimePicker dateTimePicker)
        {
            dateTimePicker.BackColor = White;
            dateTimePicker.ForeColor = Dark;
            dateTimePicker.Font = DefaultFont;
            dateTimePicker.CalendarForeColor = Dark;
            dateTimePicker.CalendarMonthBackground = White;
            dateTimePicker.CalendarTitleBackColor = Primary;
            dateTimePicker.CalendarTitleForeColor = White;
        }

        public static void ApplyNumericUpDownStyle(NumericUpDown numericUpDown)
        {
            numericUpDown.BackColor = White;
            numericUpDown.ForeColor = Dark;
            numericUpDown.Font = DefaultFont;
            numericUpDown.BorderStyle = BorderStyle.FixedSingle;
        }

        public static void ApplyCheckBoxStyle(CheckBox checkBox)
        {
            checkBox.BackColor = Background;
            checkBox.ForeColor = Dark;
            checkBox.Font = DefaultFont;
            checkBox.FlatStyle = FlatStyle.Flat;
        }

        public static void ApplyRadioButtonStyle(RadioButton radioButton)
        {
            radioButton.BackColor = Background;
            radioButton.ForeColor = Dark;
            radioButton.Font = DefaultFont;
            radioButton.FlatStyle = FlatStyle.Flat;
        }

        public static void ApplyDataGridViewStyle(DataGridView dataGridView)
        {
            dataGridView.EnableHeadersVisualStyles = false;

            dataGridView.BackgroundColor = Background;
            dataGridView.GridColor = Light;
            dataGridView.BorderStyle = BorderStyle.FixedSingle;

            dataGridView.RowHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = false;

            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;

            dataGridView.DefaultCellStyle.BackColor = White;
            dataGridView.DefaultCellStyle.ForeColor = Dark;
            dataGridView.DefaultCellStyle.SelectionBackColor = Accent;
            dataGridView.DefaultCellStyle.SelectionForeColor = White;
            dataGridView.DefaultCellStyle.Font = DefaultFont;

            dataGridView.RowsDefaultCellStyle.BackColor = White;
            dataGridView.RowsDefaultCellStyle.ForeColor = Dark;
            dataGridView.RowsDefaultCellStyle.SelectionBackColor = Accent;
            dataGridView.RowsDefaultCellStyle.SelectionForeColor = White;

            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = TableAlternateRow;
            dataGridView.AlternatingRowsDefaultCellStyle.ForeColor = Dark;
            dataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = Accent;
            dataGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor = White;

            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Primary;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = White;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Primary;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = White;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = ButtonFont;

            dataGridView.RowHeadersDefaultCellStyle.BackColor = Primary;
            dataGridView.RowHeadersDefaultCellStyle.ForeColor = White;
            dataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Primary;
            dataGridView.RowHeadersDefaultCellStyle.SelectionForeColor = White;

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.HeaderCell.Style.BackColor = Primary;
                column.HeaderCell.Style.ForeColor = White;
                column.HeaderCell.Style.SelectionBackColor = Primary;
                column.HeaderCell.Style.SelectionForeColor = White;
                column.HeaderCell.Style.Font = ButtonFont;
            }

            dataGridView.DataBindingComplete -= DataGridView_DataBindingComplete;
            dataGridView.DataBindingComplete += DataGridView_DataBindingComplete;
        }

        private static void DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;

            if (dataGridView == null)
                return;

            ApplyDataGridViewStyle(dataGridView);
        }
    }
}