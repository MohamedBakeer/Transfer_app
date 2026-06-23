namespace TransferApp
{
    partial class TransferOld
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferOld));
            this.textBox_barcode = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.iconPictureBox_Print = new FontAwesome.Sharp.IconPictureBox();
            this.textBox_manully = new System.Windows.Forms.TextBox();
            this.iconButton_update_xlx = new FontAwesome.Sharp.IconButton();
            this.button_Setting = new System.Windows.Forms.Button();
            this.button_Close = new System.Windows.Forms.Button();
            this.button_Export = new System.Windows.Forms.Button();
            this.button_Del_All = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2CheckBox_print = new Guna.UI2.WinForms.Guna2CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox_Print)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_barcode
            // 
            this.textBox_barcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.textBox_barcode.Location = new System.Drawing.Point(12, 12);
            this.textBox_barcode.Name = "textBox_barcode";
            this.textBox_barcode.Size = new System.Drawing.Size(435, 35);
            this.textBox_barcode.TabIndex = 0;
            this.textBox_barcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_barcode_KeyDown);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1256, 604);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1256, 604);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.guna2CheckBox_print);
            this.panel2.Controls.Add(this.iconPictureBox_Print);
            this.panel2.Controls.Add(this.textBox_manully);
            this.panel2.Controls.Add(this.iconButton_update_xlx);
            this.panel2.Controls.Add(this.button_Setting);
            this.panel2.Controls.Add(this.button_Close);
            this.panel2.Controls.Add(this.button_Export);
            this.panel2.Controls.Add(this.button_Del_All);
            this.panel2.Controls.Add(this.textBox_barcode);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1256, 56);
            this.panel2.TabIndex = 3;
            // 
            // iconPictureBox_Print
            // 
            this.iconPictureBox_Print.BackColor = System.Drawing.Color.White;
            this.iconPictureBox_Print.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.iconPictureBox_Print.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.iconPictureBox_Print.IconColor = System.Drawing.SystemColors.ControlDarkDark;
            this.iconPictureBox_Print.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox_Print.IconSize = 34;
            this.iconPictureBox_Print.Location = new System.Drawing.Point(984, 11);
            this.iconPictureBox_Print.Name = "iconPictureBox_Print";
            this.iconPictureBox_Print.Size = new System.Drawing.Size(36, 34);
            this.iconPictureBox_Print.TabIndex = 7;
            this.iconPictureBox_Print.TabStop = false;
            this.iconPictureBox_Print.Click += new System.EventHandler(this.iconPictureBox_Print_Click);
            // 
            // textBox_manully
            // 
            this.textBox_manully.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.textBox_manully.Location = new System.Drawing.Point(664, 11);
            this.textBox_manully.Name = "textBox_manully";
            this.textBox_manully.Size = new System.Drawing.Size(105, 35);
            this.textBox_manully.TabIndex = 6;
            this.textBox_manully.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // iconButton_update_xlx
            // 
            this.iconButton_update_xlx.IconChar = FontAwesome.Sharp.IconChar.Sync;
            this.iconButton_update_xlx.IconColor = System.Drawing.Color.Black;
            this.iconButton_update_xlx.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton_update_xlx.IconSize = 20;
            this.iconButton_update_xlx.Location = new System.Drawing.Point(1028, 11);
            this.iconButton_update_xlx.Name = "iconButton_update_xlx";
            this.iconButton_update_xlx.Size = new System.Drawing.Size(75, 34);
            this.iconButton_update_xlx.TabIndex = 5;
            this.iconButton_update_xlx.UseVisualStyleBackColor = true;
            this.iconButton_update_xlx.Click += new System.EventHandler(this.iconButton_update_xlx_Click);
            // 
            // button_Setting
            // 
            this.button_Setting.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.button_Setting.Location = new System.Drawing.Point(1109, 11);
            this.button_Setting.Name = "button_Setting";
            this.button_Setting.Size = new System.Drawing.Size(75, 34);
            this.button_Setting.TabIndex = 4;
            this.button_Setting.Text = "⚙️";
            this.button_Setting.UseVisualStyleBackColor = true;
            this.button_Setting.Click += new System.EventHandler(this.button_Setting_Click);
            // 
            // button_Close
            // 
            this.button_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button_Close.Location = new System.Drawing.Point(1190, 11);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(60, 34);
            this.button_Close.TabIndex = 3;
            this.button_Close.Text = "X";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // button_Export
            // 
            this.button_Export.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button_Export.Location = new System.Drawing.Point(559, 11);
            this.button_Export.Name = "button_Export";
            this.button_Export.Size = new System.Drawing.Size(99, 34);
            this.button_Export.TabIndex = 2;
            this.button_Export.Text = "Export";
            this.button_Export.UseVisualStyleBackColor = true;
            this.button_Export.Click += new System.EventHandler(this.button_Export_Click);
            // 
            // button_Del_All
            // 
            this.button_Del_All.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button_Del_All.Location = new System.Drawing.Point(454, 12);
            this.button_Del_All.Name = "button_Del_All";
            this.button_Del_All.Size = new System.Drawing.Size(99, 34);
            this.button_Del_All.TabIndex = 1;
            this.button_Del_All.Text = "Delete All";
            this.button_Del_All.UseVisualStyleBackColor = true;
            this.button_Del_All.Click += new System.EventHandler(this.button_Del_All_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(10, 10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1256, 694);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 660);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1256, 34);
            this.panel4.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(801, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(801, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(455, 34);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // guna2CheckBox_print
            // 
            this.guna2CheckBox_print.AutoSize = true;
            this.guna2CheckBox_print.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2CheckBox_print.CheckedState.BorderRadius = 2;
            this.guna2CheckBox_print.CheckedState.BorderThickness = 0;
            this.guna2CheckBox_print.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2CheckBox_print.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2CheckBox_print.Location = new System.Drawing.Point(963, 20);
            this.guna2CheckBox_print.Name = "guna2CheckBox_print";
            this.guna2CheckBox_print.Size = new System.Drawing.Size(15, 14);
            this.guna2CheckBox_print.TabIndex = 8;
            this.guna2CheckBox_print.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2CheckBox_print.UncheckedState.BorderRadius = 2;
            this.guna2CheckBox_print.UncheckedState.BorderThickness = 0;
            this.guna2CheckBox_print.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2CheckBox_print.UseVisualStyleBackColor = true;
            this.guna2CheckBox_print.CheckedChanged += new System.EventHandler(this.guna2CheckBox_print_CheckedChanged);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1276, 714);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainFrm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox_Print)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_barcode;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_Export;
        private System.Windows.Forms.Button button_Del_All;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button_Setting;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton iconButton_update_xlx;
        private System.Windows.Forms.TextBox textBox_manully;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox_Print;
        private Guna.UI2.WinForms.Guna2CheckBox guna2CheckBox_print;
    }
}

