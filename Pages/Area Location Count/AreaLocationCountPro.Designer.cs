namespace Transfer_app.Pages.Area_Location_Count
{
    partial class AreaLocationCountPro
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox_manully = new System.Windows.Forms.TextBox();
            this.button_Del_All = new System.Windows.Forms.Button();
            this.textBox_barcode = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.guna2ComboBox_location_to_count = new Guna.UI2.WinForms.Guna2ComboBox();
            this.button_upload_to_server = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2ComboBox_Area = new Guna.UI2.WinForms.Guna2ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.guna2ProgressBar1 = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.guna2ComboBox_location = new Guna.UI2.WinForms.Guna2ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1353, 719);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1353, 848);
            this.panel1.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dataGridView1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1353, 719);
            this.panel4.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBox_manully);
            this.panel3.Controls.Add(this.button_Del_All);
            this.panel3.Controls.Add(this.textBox_barcode);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 719);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1353, 62);
            this.panel3.TabIndex = 1;
            // 
            // textBox_manully
            // 
            this.textBox_manully.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_manully.Font = new System.Drawing.Font("Cairo", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_manully.Location = new System.Drawing.Point(1224, 6);
            this.textBox_manully.Name = "textBox_manully";
            this.textBox_manully.Size = new System.Drawing.Size(124, 52);
            this.textBox_manully.TabIndex = 9;
            this.textBox_manully.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_Del_All
            // 
            this.button_Del_All.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_Del_All.Font = new System.Drawing.Font("Cairo", 15.75F, System.Drawing.FontStyle.Bold);
            this.button_Del_All.Location = new System.Drawing.Point(553, 6);
            this.button_Del_All.Name = "button_Del_All";
            this.button_Del_All.Size = new System.Drawing.Size(143, 52);
            this.button_Del_All.TabIndex = 8;
            this.button_Del_All.Text = "Delete All";
            this.button_Del_All.UseVisualStyleBackColor = true;
            this.button_Del_All.Click += new System.EventHandler(this.button_Del_All_Click);
            // 
            // textBox_barcode
            // 
            this.textBox_barcode.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBox_barcode.Font = new System.Drawing.Font("Cairo", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_barcode.Location = new System.Drawing.Point(12, 6);
            this.textBox_barcode.Name = "textBox_barcode";
            this.textBox_barcode.Size = new System.Drawing.Size(535, 52);
            this.textBox_barcode.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.guna2ComboBox_location_to_count);
            this.panel2.Controls.Add(this.button_upload_to_server);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.guna2ComboBox_location);
            this.panel2.Controls.Add(this.guna2ComboBox_Area);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button_Save);
            this.panel2.Controls.Add(this.guna2ProgressBar1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 781);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1353, 67);
            this.panel2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cairo", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(465, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 36);
            this.label3.TabIndex = 26;
            this.label3.Text = "Store :";
            // 
            // guna2ComboBox_location_to_count
            // 
            this.guna2ComboBox_location_to_count.Animated = true;
            this.guna2ComboBox_location_to_count.BackColor = System.Drawing.Color.Transparent;
            this.guna2ComboBox_location_to_count.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2ComboBox_location_to_count.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.guna2ComboBox_location_to_count.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.guna2ComboBox_location_to_count.FocusedColor = System.Drawing.Color.Empty;
            this.guna2ComboBox_location_to_count.FocusedState.Parent = this.guna2ComboBox_location_to_count;
            this.guna2ComboBox_location_to_count.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2ComboBox_location_to_count.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.guna2ComboBox_location_to_count.FormattingEnabled = true;
            this.guna2ComboBox_location_to_count.HoverState.Parent = this.guna2ComboBox_location_to_count;
            this.guna2ComboBox_location_to_count.ItemHeight = 30;
            this.guna2ComboBox_location_to_count.Items.AddRange(new object[] {
            "A1",
            "A2",
            "A3",
            "A4",
            "A5",
            "A6",
            "A7",
            "A8",
            "A9",
            "A10",
            "A11",
            "A12",
            "A13",
            "A14",
            "A15",
            "A16",
            "A17",
            "A18",
            "A19",
            "A20"});
            this.guna2ComboBox_location_to_count.ItemsAppearance.Parent = this.guna2ComboBox_location_to_count;
            this.guna2ComboBox_location_to_count.Location = new System.Drawing.Point(538, 9);
            this.guna2ComboBox_location_to_count.Name = "guna2ComboBox_location_to_count";
            this.guna2ComboBox_location_to_count.ShadowDecoration.Parent = this.guna2ComboBox_location_to_count;
            this.guna2ComboBox_location_to_count.Size = new System.Drawing.Size(229, 36);
            this.guna2ComboBox_location_to_count.TabIndex = 25;
            // 
            // button_upload_to_server
            // 
            this.button_upload_to_server.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_upload_to_server.Font = new System.Drawing.Font("Cairo", 14F, System.Drawing.FontStyle.Bold);
            this.button_upload_to_server.Location = new System.Drawing.Point(142, 6);
            this.button_upload_to_server.Name = "button_upload_to_server";
            this.button_upload_to_server.Size = new System.Drawing.Size(124, 42);
            this.button_upload_to_server.TabIndex = 24;
            this.button_upload_to_server.Text = "upload Srv";
            this.button_upload_to_server.UseVisualStyleBackColor = true;
            this.button_upload_to_server.Click += new System.EventHandler(this.button_upload_to_server_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cairo", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1000, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 36);
            this.label1.TabIndex = 22;
            this.label1.Text = "Location :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cairo", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(773, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 36);
            this.label2.TabIndex = 20;
            this.label2.Text = "Area :";
            // 
            // guna2ComboBox_Area
            // 
            this.guna2ComboBox_Area.Animated = true;
            this.guna2ComboBox_Area.BackColor = System.Drawing.Color.Transparent;
            this.guna2ComboBox_Area.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2ComboBox_Area.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.guna2ComboBox_Area.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.guna2ComboBox_Area.FocusedColor = System.Drawing.Color.Empty;
            this.guna2ComboBox_Area.FocusedState.Parent = this.guna2ComboBox_Area;
            this.guna2ComboBox_Area.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2ComboBox_Area.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.guna2ComboBox_Area.FormattingEnabled = true;
            this.guna2ComboBox_Area.HoverState.Parent = this.guna2ComboBox_Area;
            this.guna2ComboBox_Area.ItemHeight = 30;
            this.guna2ComboBox_Area.Items.AddRange(new object[] {
            "A1",
            "A2",
            "A3",
            "A4",
            "A5",
            "A6",
            "A7",
            "A8",
            "A9",
            "A10",
            "A11",
            "A12",
            "A13",
            "A14",
            "A15",
            "A16",
            "A17",
            "A18",
            "A19",
            "A20"});
            this.guna2ComboBox_Area.ItemsAppearance.Parent = this.guna2ComboBox_Area;
            this.guna2ComboBox_Area.Location = new System.Drawing.Point(839, 9);
            this.guna2ComboBox_Area.Name = "guna2ComboBox_Area";
            this.guna2ComboBox_Area.ShadowDecoration.Parent = this.guna2ComboBox_Area;
            this.guna2ComboBox_Area.Size = new System.Drawing.Size(155, 36);
            this.guna2ComboBox_Area.TabIndex = 19;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.Font = new System.Drawing.Font("Cairo", 14F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(12, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 42);
            this.button2.TabIndex = 18;
            this.button2.Text = "Back";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button_Save
            // 
            this.button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Save.Font = new System.Drawing.Font("Cairo", 14F, System.Drawing.FontStyle.Bold);
            this.button_Save.Location = new System.Drawing.Point(1224, 6);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(124, 42);
            this.button_Save.TabIndex = 10;
            this.button_Save.Text = "Next";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // guna2ProgressBar1
            // 
            this.guna2ProgressBar1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.guna2ProgressBar1.BorderRadius = 2;
            this.guna2ProgressBar1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.guna2ProgressBar1.Location = new System.Drawing.Point(12, 51);
            this.guna2ProgressBar1.Name = "guna2ProgressBar1";
            this.guna2ProgressBar1.ShadowDecoration.Parent = this.guna2ProgressBar1;
            this.guna2ProgressBar1.Size = new System.Drawing.Size(1329, 13);
            this.guna2ProgressBar1.TabIndex = 17;
            this.guna2ProgressBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // guna2ComboBox_location
            // 
            this.guna2ComboBox_location.Animated = true;
            this.guna2ComboBox_location.BackColor = System.Drawing.Color.Transparent;
            this.guna2ComboBox_location.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2ComboBox_location.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.guna2ComboBox_location.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.guna2ComboBox_location.FocusedColor = System.Drawing.Color.Empty;
            this.guna2ComboBox_location.FocusedState.Parent = this.guna2ComboBox_location;
            this.guna2ComboBox_location.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2ComboBox_location.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.guna2ComboBox_location.FormattingEnabled = true;
            this.guna2ComboBox_location.HoverState.Parent = this.guna2ComboBox_location;
            this.guna2ComboBox_location.ItemHeight = 30;
            this.guna2ComboBox_location.Items.AddRange(new object[] {
            "A1",
            "A2",
            "A3",
            "A4",
            "A5",
            "A6",
            "A7",
            "A8",
            "A9",
            "A10",
            "A11",
            "A12",
            "A13",
            "A14",
            "A15",
            "A16",
            "A17",
            "A18",
            "A19",
            "A20"});
            this.guna2ComboBox_location.ItemsAppearance.Parent = this.guna2ComboBox_location;
            this.guna2ComboBox_location.Location = new System.Drawing.Point(1097, 9);
            this.guna2ComboBox_location.Name = "guna2ComboBox_location";
            this.guna2ComboBox_location.ShadowDecoration.Parent = this.guna2ComboBox_location;
            this.guna2ComboBox_location.Size = new System.Drawing.Size(121, 36);
            this.guna2ComboBox_location.TabIndex = 19;
            // 
            // AreaLocationCountPro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1353, 848);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Cairo", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "AreaLocationCountPro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AreaLocationCount";
            this.Load += new System.EventHandler(this.AreaLocationCount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox_manully;
        private System.Windows.Forms.Button button_Del_All;
        private System.Windows.Forms.TextBox textBox_barcode;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button_Save;
        private Guna.UI2.WinForms.Guna2ProgressBar guna2ProgressBar1;
        private Guna.UI2.WinForms.Guna2ComboBox guna2ComboBox_Area;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_upload_to_server;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2ComboBox guna2ComboBox_location_to_count;
        private Guna.UI2.WinForms.Guna2ComboBox guna2ComboBox_location;
    }
}