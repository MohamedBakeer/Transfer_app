namespace Transfer_app.Pages.Transfer_Monitor
{
    partial class TransferMonitorFrm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gunaRadioButton_type_transfer_request = new Guna.UI.WinForms.GunaRadioButton();
            this.gunaRadioButton_type_internal = new Guna.UI.WinForms.GunaRadioButton();
            this.gunaRadioButton_type_all = new Guna.UI.WinForms.GunaRadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.gunaRadioButton_status_failed = new Guna.UI.WinForms.GunaRadioButton();
            this.guna2Button_back = new Guna.UI2.WinForms.Guna2Button();
            this.gunaRadioButton_status_all = new Guna.UI.WinForms.GunaRadioButton();
            this.gunaRadioButton_status_posted = new Guna.UI.WinForms.GunaRadioButton();
            this.guna2Button_load = new Guna.UI2.WinForms.Guna2Button();
            this.gunaRadioButton_status_received_not_match = new Guna.UI.WinForms.GunaRadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.gunaRadioButton_status_received_match = new Guna.UI.WinForms.GunaRadioButton();
            this.gunaRadioButton_status_uploaded = new Guna.UI.WinForms.GunaRadioButton();
            this.guna2DateTimePicker1 = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.guna2DateTimePicker_from = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.guna2ComboBox_branch = new Guna.UI2.WinForms.Guna2ComboBox();
            this.panel1.SuspendLayout();
            this.guna2ShadowPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.guna2ShadowPanel1);
            this.panel1.Controls.Add(this.guna2Button_back);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(1018, 720);
            this.panel1.TabIndex = 0;
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.panel3);
            this.guna2ShadowPanel1.Controls.Add(this.panel2);
            this.guna2ShadowPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(5, 5);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.Radius = 5;
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.ShadowShift = 3;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(1008, 95);
            this.guna2ShadowPanel1.TabIndex = 26;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.gunaRadioButton_status_failed);
            this.panel3.Controls.Add(this.guna2Button_load);
            this.panel3.Controls.Add(this.gunaRadioButton_status_uploaded);
            this.panel3.Controls.Add(this.gunaRadioButton_status_received_match);
            this.panel3.Controls.Add(this.gunaRadioButton_status_all);
            this.panel3.Controls.Add(this.gunaRadioButton_status_received_not_match);
            this.panel3.Controls.Add(this.gunaRadioButton_status_posted);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 47);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1008, 48);
            this.panel3.TabIndex = 28;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.guna2ComboBox_branch);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.guna2DateTimePicker_from);
            this.panel2.Controls.Add(this.guna2DateTimePicker1);
            this.panel2.Controls.Add(this.gunaRadioButton_type_transfer_request);
            this.panel2.Controls.Add(this.gunaRadioButton_type_internal);
            this.panel2.Controls.Add(this.gunaRadioButton_type_all);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 47);
            this.panel2.TabIndex = 27;
            // 
            // gunaRadioButton_type_transfer_request
            // 
            this.gunaRadioButton_type_transfer_request.BaseColor = System.Drawing.SystemColors.Control;
            this.gunaRadioButton_type_transfer_request.CheckedOffColor = System.Drawing.Color.Gray;
            this.gunaRadioButton_type_transfer_request.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaRadioButton_type_transfer_request.FillColor = System.Drawing.Color.White;
            this.gunaRadioButton_type_transfer_request.Location = new System.Drawing.Point(212, 14);
            this.gunaRadioButton_type_transfer_request.Name = "gunaRadioButton_type_transfer_request";
            this.gunaRadioButton_type_transfer_request.Size = new System.Drawing.Size(112, 20);
            this.gunaRadioButton_type_transfer_request.TabIndex = 29;
            this.gunaRadioButton_type_transfer_request.Text = "transfer_request";
            // 
            // gunaRadioButton_type_internal
            // 
            this.gunaRadioButton_type_internal.BaseColor = System.Drawing.SystemColors.Control;
            this.gunaRadioButton_type_internal.CheckedOffColor = System.Drawing.Color.Gray;
            this.gunaRadioButton_type_internal.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaRadioButton_type_internal.FillColor = System.Drawing.Color.White;
            this.gunaRadioButton_type_internal.Location = new System.Drawing.Point(133, 14);
            this.gunaRadioButton_type_internal.Name = "gunaRadioButton_type_internal";
            this.gunaRadioButton_type_internal.Size = new System.Drawing.Size(67, 20);
            this.gunaRadioButton_type_internal.TabIndex = 30;
            this.gunaRadioButton_type_internal.Text = "internal";
            // 
            // gunaRadioButton_type_all
            // 
            this.gunaRadioButton_type_all.BaseColor = System.Drawing.SystemColors.Control;
            this.gunaRadioButton_type_all.Checked = true;
            this.gunaRadioButton_type_all.CheckedOffColor = System.Drawing.Color.Gray;
            this.gunaRadioButton_type_all.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaRadioButton_type_all.FillColor = System.Drawing.Color.White;
            this.gunaRadioButton_type_all.Location = new System.Drawing.Point(77, 14);
            this.gunaRadioButton_type_all.Name = "gunaRadioButton_type_all";
            this.gunaRadioButton_type_all.Size = new System.Drawing.Size(42, 20);
            this.gunaRadioButton_type_all.TabIndex = 31;
            this.gunaRadioButton_type_all.Text = "All";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cairo", 12.25F);
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 32);
            this.label1.TabIndex = 28;
            this.label1.Text = "Type : ";
            // 
            // gunaRadioButton_status_failed
            // 
            this.gunaRadioButton_status_failed.BaseColor = System.Drawing.SystemColors.Control;
            this.gunaRadioButton_status_failed.CheckedOffColor = System.Drawing.Color.Gray;
            this.gunaRadioButton_status_failed.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaRadioButton_status_failed.FillColor = System.Drawing.Color.White;
            this.gunaRadioButton_status_failed.Location = new System.Drawing.Point(594, 13);
            this.gunaRadioButton_status_failed.Name = "gunaRadioButton_status_failed";
            this.gunaRadioButton_status_failed.Size = new System.Drawing.Size(57, 20);
            this.gunaRadioButton_status_failed.TabIndex = 28;
            this.gunaRadioButton_status_failed.Text = "failed";
            // 
            // guna2Button_back
            // 
            this.guna2Button_back.BorderRadius = 10;
            this.guna2Button_back.CheckedState.Parent = this.guna2Button_back;
            this.guna2Button_back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2Button_back.CustomImages.Parent = this.guna2Button_back;
            this.guna2Button_back.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.guna2Button_back.Font = new System.Drawing.Font("Cairo", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button_back.ForeColor = System.Drawing.Color.Black;
            this.guna2Button_back.HoverState.Parent = this.guna2Button_back;
            this.guna2Button_back.Location = new System.Drawing.Point(8, 669);
            this.guna2Button_back.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Button_back.Name = "guna2Button_back";
            this.guna2Button_back.ShadowDecoration.Parent = this.guna2Button_back;
            this.guna2Button_back.Size = new System.Drawing.Size(124, 42);
            this.guna2Button_back.TabIndex = 25;
            this.guna2Button_back.Text = "Back";
            this.guna2Button_back.Click += new System.EventHandler(this.guna2Button_back_Click);
            // 
            // gunaRadioButton_status_all
            // 
            this.gunaRadioButton_status_all.BaseColor = System.Drawing.SystemColors.Control;
            this.gunaRadioButton_status_all.Checked = true;
            this.gunaRadioButton_status_all.CheckedOffColor = System.Drawing.Color.Gray;
            this.gunaRadioButton_status_all.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaRadioButton_status_all.FillColor = System.Drawing.Color.White;
            this.gunaRadioButton_status_all.Location = new System.Drawing.Point(90, 13);
            this.gunaRadioButton_status_all.Name = "gunaRadioButton_status_all";
            this.gunaRadioButton_status_all.Size = new System.Drawing.Size(42, 20);
            this.gunaRadioButton_status_all.TabIndex = 28;
            this.gunaRadioButton_status_all.Text = "All";
            // 
            // gunaRadioButton_status_posted
            // 
            this.gunaRadioButton_status_posted.BaseColor = System.Drawing.SystemColors.Control;
            this.gunaRadioButton_status_posted.CheckedOffColor = System.Drawing.Color.Gray;
            this.gunaRadioButton_status_posted.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaRadioButton_status_posted.FillColor = System.Drawing.Color.White;
            this.gunaRadioButton_status_posted.Location = new System.Drawing.Point(513, 13);
            this.gunaRadioButton_status_posted.Name = "gunaRadioButton_status_posted";
            this.gunaRadioButton_status_posted.Size = new System.Drawing.Size(64, 20);
            this.gunaRadioButton_status_posted.TabIndex = 28;
            this.gunaRadioButton_status_posted.Text = "posted";
            // 
            // guna2Button_load
            // 
            this.guna2Button_load.BorderRadius = 10;
            this.guna2Button_load.CheckedState.Parent = this.guna2Button_load;
            this.guna2Button_load.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2Button_load.CustomImages.Parent = this.guna2Button_load;
            this.guna2Button_load.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.guna2Button_load.Font = new System.Drawing.Font("Cairo", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button_load.ForeColor = System.Drawing.Color.Black;
            this.guna2Button_load.HoverState.Parent = this.guna2Button_load;
            this.guna2Button_load.Location = new System.Drawing.Point(884, 8);
            this.guna2Button_load.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Button_load.Name = "guna2Button_load";
            this.guna2Button_load.ShadowDecoration.Parent = this.guna2Button_load;
            this.guna2Button_load.Size = new System.Drawing.Size(112, 31);
            this.guna2Button_load.TabIndex = 24;
            this.guna2Button_load.Text = "Load";
            this.guna2Button_load.Click += new System.EventHandler(this.guna2Button_load_Click);
            // 
            // gunaRadioButton_status_received_not_match
            // 
            this.gunaRadioButton_status_received_not_match.BaseColor = System.Drawing.SystemColors.Control;
            this.gunaRadioButton_status_received_not_match.CheckedOffColor = System.Drawing.Color.Gray;
            this.gunaRadioButton_status_received_not_match.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaRadioButton_status_received_not_match.FillColor = System.Drawing.Color.White;
            this.gunaRadioButton_status_received_not_match.Location = new System.Drawing.Point(366, 13);
            this.gunaRadioButton_status_received_not_match.Name = "gunaRadioButton_status_received_not_match";
            this.gunaRadioButton_status_received_not_match.Size = new System.Drawing.Size(132, 20);
            this.gunaRadioButton_status_received_not_match.TabIndex = 28;
            this.gunaRadioButton_status_received_not_match.Text = "received_not_match";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cairo", 12.25F);
            this.label2.Location = new System.Drawing.Point(11, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 32);
            this.label2.TabIndex = 16;
            this.label2.Text = "Status : ";
            // 
            // gunaRadioButton_status_received_match
            // 
            this.gunaRadioButton_status_received_match.BaseColor = System.Drawing.SystemColors.Control;
            this.gunaRadioButton_status_received_match.CheckedOffColor = System.Drawing.Color.Gray;
            this.gunaRadioButton_status_received_match.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaRadioButton_status_received_match.FillColor = System.Drawing.Color.White;
            this.gunaRadioButton_status_received_match.Location = new System.Drawing.Point(239, 13);
            this.gunaRadioButton_status_received_match.Name = "gunaRadioButton_status_received_match";
            this.gunaRadioButton_status_received_match.Size = new System.Drawing.Size(110, 20);
            this.gunaRadioButton_status_received_match.TabIndex = 28;
            this.gunaRadioButton_status_received_match.Text = "received_match";
            // 
            // gunaRadioButton_status_uploaded
            // 
            this.gunaRadioButton_status_uploaded.BaseColor = System.Drawing.SystemColors.Control;
            this.gunaRadioButton_status_uploaded.CheckedOffColor = System.Drawing.Color.Gray;
            this.gunaRadioButton_status_uploaded.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaRadioButton_status_uploaded.FillColor = System.Drawing.Color.White;
            this.gunaRadioButton_status_uploaded.Location = new System.Drawing.Point(147, 13);
            this.gunaRadioButton_status_uploaded.Name = "gunaRadioButton_status_uploaded";
            this.gunaRadioButton_status_uploaded.Size = new System.Drawing.Size(76, 20);
            this.gunaRadioButton_status_uploaded.TabIndex = 28;
            this.gunaRadioButton_status_uploaded.Text = "uploaded";
            // 
            // guna2DateTimePicker1
            // 
            this.guna2DateTimePicker1.CheckedState.Parent = this.guna2DateTimePicker1;
            this.guna2DateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.guna2DateTimePicker1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.guna2DateTimePicker1.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.guna2DateTimePicker1.HoverState.Parent = this.guna2DateTimePicker1;
            this.guna2DateTimePicker1.Location = new System.Drawing.Point(828, 11);
            this.guna2DateTimePicker1.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.guna2DateTimePicker1.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.guna2DateTimePicker1.Name = "guna2DateTimePicker1";
            this.guna2DateTimePicker1.ShadowDecoration.Parent = this.guna2DateTimePicker1;
            this.guna2DateTimePicker1.Size = new System.Drawing.Size(168, 26);
            this.guna2DateTimePicker1.TabIndex = 28;
            this.guna2DateTimePicker1.Value = new System.DateTime(2026, 6, 15, 14, 5, 14, 741);
            // 
            // guna2DateTimePicker_from
            // 
            this.guna2DateTimePicker_from.CheckedState.Parent = this.guna2DateTimePicker_from;
            this.guna2DateTimePicker_from.CustomFormat = "yyyy-MM-dd";
            this.guna2DateTimePicker_from.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.guna2DateTimePicker_from.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2DateTimePicker_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.guna2DateTimePicker_from.HoverState.Parent = this.guna2DateTimePicker_from;
            this.guna2DateTimePicker_from.Location = new System.Drawing.Point(594, 11);
            this.guna2DateTimePicker_from.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.guna2DateTimePicker_from.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.guna2DateTimePicker_from.Name = "guna2DateTimePicker_from";
            this.guna2DateTimePicker_from.ShadowDecoration.Parent = this.guna2DateTimePicker_from;
            this.guna2DateTimePicker_from.Size = new System.Drawing.Size(168, 26);
            this.guna2DateTimePicker_from.TabIndex = 27;
            this.guna2DateTimePicker_from.Value = new System.DateTime(2026, 6, 16, 14, 12, 46, 885);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cairo", 12.25F);
            this.label3.Location = new System.Drawing.Point(768, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 32);
            this.label3.TabIndex = 29;
            this.label3.Text = ">>>>";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(18, 106);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(983, 556);
            this.dataGridView1.TabIndex = 27;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // guna2ComboBox_branch
            // 
            this.guna2ComboBox_branch.Animated = true;
            this.guna2ComboBox_branch.BackColor = System.Drawing.Color.Transparent;
            this.guna2ComboBox_branch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2ComboBox_branch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.guna2ComboBox_branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.guna2ComboBox_branch.FocusedColor = System.Drawing.Color.Empty;
            this.guna2ComboBox_branch.FocusedState.Parent = this.guna2ComboBox_branch;
            this.guna2ComboBox_branch.Font = new System.Drawing.Font("Cairo", 12F);
            this.guna2ComboBox_branch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.guna2ComboBox_branch.FormattingEnabled = true;
            this.guna2ComboBox_branch.HoverState.Parent = this.guna2ComboBox_branch;
            this.guna2ComboBox_branch.ItemHeight = 30;
            this.guna2ComboBox_branch.ItemsAppearance.Parent = this.guna2ComboBox_branch;
            this.guna2ComboBox_branch.Location = new System.Drawing.Point(366, 6);
            this.guna2ComboBox_branch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2ComboBox_branch.Name = "guna2ComboBox_branch";
            this.guna2ComboBox_branch.ShadowDecoration.Parent = this.guna2ComboBox_branch;
            this.guna2ComboBox_branch.Size = new System.Drawing.Size(168, 36);
            this.guna2ComboBox_branch.TabIndex = 32;
            // 
            // TransferMonitorFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1028, 730);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TransferMonitorFrm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TransferMonitorFrm";
            this.Load += new System.EventHandler(this.TransferMonitorFrm_Load);
            this.panel1.ResumeLayout(false);
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private Guna.UI2.WinForms.Guna2Button guna2Button_back;
        private Guna.UI2.WinForms.Guna2Button guna2Button_load;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI.WinForms.GunaRadioButton gunaRadioButton_type_transfer_request;
        private Guna.UI.WinForms.GunaRadioButton gunaRadioButton_type_internal;
        private Guna.UI.WinForms.GunaRadioButton gunaRadioButton_type_all;
        private System.Windows.Forms.Label label1;
        private Guna.UI.WinForms.GunaRadioButton gunaRadioButton_status_failed;
        private Guna.UI.WinForms.GunaRadioButton gunaRadioButton_status_all;
        private Guna.UI.WinForms.GunaRadioButton gunaRadioButton_status_posted;
        private Guna.UI.WinForms.GunaRadioButton gunaRadioButton_status_received_not_match;
        private System.Windows.Forms.Label label2;
        private Guna.UI.WinForms.GunaRadioButton gunaRadioButton_status_received_match;
        private Guna.UI.WinForms.GunaRadioButton gunaRadioButton_status_uploaded;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2DateTimePicker guna2DateTimePicker_from;
        private Guna.UI2.WinForms.Guna2DateTimePicker guna2DateTimePicker1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Guna.UI2.WinForms.Guna2ComboBox guna2ComboBox_branch;
    }
}