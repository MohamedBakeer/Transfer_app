namespace Transfer_app.Pages.Pending_Upload
{
    partial class PendingUploadFrm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panel_main = new System.Windows.Forms.Panel();
            this.panel_grid = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.label_status = new System.Windows.Forms.Label();
            this.guna2ProgressBar1 = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.button_back = new System.Windows.Forms.Button();
            this.button_refresh = new System.Windows.Forms.Button();
            this.button_upload = new System.Windows.Forms.Button();
            this.panel_top = new System.Windows.Forms.Panel();
            this.label_summary = new System.Windows.Forms.Label();
            this.label_title = new System.Windows.Forms.Label();
            this.panel_main.SuspendLayout();
            this.panel_grid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel_bottom.SuspendLayout();
            this.panel_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.panel_grid);
            this.panel_main.Controls.Add(this.panel_bottom);
            this.panel_main.Controls.Add(this.panel_top);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(5, 5);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(790, 682);
            this.panel_main.TabIndex = 0;
            // 
            // panel_grid
            // 
            this.panel_grid.Controls.Add(this.dataGridView1);
            this.panel_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_grid.Location = new System.Drawing.Point(0, 95);
            this.panel_grid.Name = "panel_grid";
            this.panel_grid.Padding = new System.Windows.Forms.Padding(8);
            this.panel_grid.Size = new System.Drawing.Size(790, 477);
            this.panel_grid.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(8, 8);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(774, 461);
            this.dataGridView1.TabIndex = 0;
            // 
            // panel_bottom
            // 
            this.panel_bottom.Controls.Add(this.label_status);
            this.panel_bottom.Controls.Add(this.guna2ProgressBar1);
            this.panel_bottom.Controls.Add(this.button_back);
            this.panel_bottom.Controls.Add(this.button_refresh);
            this.panel_bottom.Controls.Add(this.button_upload);
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_bottom.Location = new System.Drawing.Point(0, 572);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(790, 110);
            this.panel_bottom.TabIndex = 2;
            // 
            // label_status
            // 
            this.label_status.Font = new System.Drawing.Font("Cairo", 9F);
            this.label_status.Location = new System.Drawing.Point(8, 84);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(774, 23);
            this.label_status.TabIndex = 5;
            this.label_status.Text = "Ready";
            this.label_status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2ProgressBar1
            // 
            this.guna2ProgressBar1.BorderRadius = 2;
            this.guna2ProgressBar1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.guna2ProgressBar1.Location = new System.Drawing.Point(8, 68);
            this.guna2ProgressBar1.Name = "guna2ProgressBar1";
            this.guna2ProgressBar1.ShadowDecoration.Parent = this.guna2ProgressBar1;
            this.guna2ProgressBar1.Size = new System.Drawing.Size(774, 14);
            this.guna2ProgressBar1.TabIndex = 4;
            this.guna2ProgressBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // button_back
            // 
            this.button_back.Font = new System.Drawing.Font("Cairo", 11F, System.Drawing.FontStyle.Bold);
            this.button_back.Location = new System.Drawing.Point(8, 12);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(120, 42);
            this.button_back.TabIndex = 0;
            this.button_back.Text = "Back";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            // 
            // button_refresh
            // 
            this.button_refresh.Font = new System.Drawing.Font("Cairo", 11F, System.Drawing.FontStyle.Bold);
            this.button_refresh.Location = new System.Drawing.Point(134, 12);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(130, 42);
            this.button_refresh.TabIndex = 2;
            this.button_refresh.Text = "Refresh";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // button_upload
            // 
            this.button_upload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_upload.Font = new System.Drawing.Font("Cairo", 11F, System.Drawing.FontStyle.Bold);
            this.button_upload.Location = new System.Drawing.Point(632, 12);
            this.button_upload.Name = "button_upload";
            this.button_upload.Size = new System.Drawing.Size(150, 42);
            this.button_upload.TabIndex = 3;
            this.button_upload.Text = "Upload Selected";
            this.button_upload.UseVisualStyleBackColor = true;
            this.button_upload.Click += new System.EventHandler(this.button_upload_Click);
            // 
            // panel_top
            // 
            this.panel_top.Controls.Add(this.label_summary);
            this.panel_top.Controls.Add(this.label_title);
            this.panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top.Location = new System.Drawing.Point(0, 0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(790, 95);
            this.panel_top.TabIndex = 0;
            // 
            // label_summary
            // 
            this.label_summary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_summary.Font = new System.Drawing.Font("Cairo", 11F);
            this.label_summary.Location = new System.Drawing.Point(0, 55);
            this.label_summary.Name = "label_summary";
            this.label_summary.Size = new System.Drawing.Size(790, 40);
            this.label_summary.TabIndex = 1;
            this.label_summary.Text = "Pending Files: 0";
            this.label_summary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_title
            // 
            this.label_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_title.Font = new System.Drawing.Font("Cairo", 20.25F, System.Drawing.FontStyle.Bold);
            this.label_title.Location = new System.Drawing.Point(0, 0);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(790, 55);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "Pending Upload";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PendingUploadFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 692);
            this.Controls.Add(this.panel_main);
            this.Font = new System.Drawing.Font("Cairo", 8.249999F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "PendingUploadFrm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PendingUploadFrm";
            this.Load += new System.EventHandler(this.PendingUploadFrm_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_grid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel_bottom.ResumeLayout(false);
            this.panel_top.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.Panel panel_top;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_summary;
        private System.Windows.Forms.Panel panel_grid;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Button button_back;
        private System.Windows.Forms.Button button_refresh;
        private System.Windows.Forms.Button button_upload;
        private Guna.UI2.WinForms.Guna2ProgressBar guna2ProgressBar1;
        private System.Windows.Forms.Label label_status;
    }
}