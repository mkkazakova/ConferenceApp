namespace ConferenceApp
{
    partial class MaterialsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.FlowLayoutPanel flowMaterials;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.flowMaterials = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();

            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 58, 95);
            this.lblTitle.Location = new System.Drawing.Point(330, 25);
            this.lblTitle.Text = "Материалы конференции";

            this.lblSearch.Location = new System.Drawing.Point(30, 78);
            this.lblSearch.Size = new System.Drawing.Size(60, 20);
            this.lblSearch.Text = "Поиск:";

            this.txtSearch.Location = new System.Drawing.Point(90, 75);
            this.txtSearch.Size = new System.Drawing.Size(880, 22);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            this.flowMaterials.Location = new System.Drawing.Point(30, 115);
            this.flowMaterials.Size = new System.Drawing.Size(940, 430);
            this.flowMaterials.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.flowMaterials.AutoScroll = true;
            this.flowMaterials.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowMaterials.WrapContents = false;

            this.btnClose.Location = new System.Drawing.Point(830, 560);
            this.btnClose.Size = new System.Drawing.Size(140, 35);
            this.btnClose.Text = "Назад";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            this.ClientSize = new System.Drawing.Size(1000, 620);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.flowMaterials);
            this.Controls.Add(this.btnClose);

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Материалы конференции";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}