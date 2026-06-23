namespace Transfer_app.Pages.Transfer_Request
{
    partial class Step_2_Scan_Items
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox_manully = new System.Windows.Forms.TextBox();
            this.button_Del_All = new System.Windows.Forms.Button();
            this.textBox_barcode = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button_Send_pending = new System.Windows.Forms.Button();
            this.label_note = new System.Windows.Forms.Label();
            this.guna2ProgressBar1 = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1266, 805);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dataGridView1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1266, 676);
            this.panel4.TabIndex = 2;
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
            this.dataGridView1.Size = new System.Drawing.Size(1266, 676);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBox_manully);
            this.panel3.Controls.Add(this.button_Del_All);
            this.panel3.Controls.Add(this.textBox_barcode);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 676);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1266, 62);
            this.panel3.TabIndex = 1;
            // 
            // textBox_manully
            // 
            this.textBox_manully.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBox_manully.Font = new System.Drawing.Font("Cairo", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_manully.Location = new System.Drawing.Point(1139, 6);
            this.textBox_manully.Name = "textBox_manully";
            this.textBox_manully.Size = new System.Drawing.Size(124, 52);
            this.textBox_manully.TabIndex = 9;
            this.textBox_manully.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_Del_All
            // 
            this.button_Del_All.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_Del_All.Font = new System.Drawing.Font("Cairo", 15.75F, System.Drawing.FontStyle.Bold);
            this.button_Del_All.Location = new System.Drawing.Point(544, 6);
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
            this.textBox_barcode.Location = new System.Drawing.Point(3, 6);
            this.textBox_barcode.Name = "textBox_barcode";
            this.textBox_barcode.Size = new System.Drawing.Size(535, 52);
            this.textBox_barcode.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button_Send_pending);
            this.panel2.Controls.Add(this.label_note);
            this.panel2.Controls.Add(this.guna2ProgressBar1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 738);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1266, 67);
            this.panel2.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.Font = new System.Drawing.Font("Cairo", 14F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(3, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 42);
            this.button2.TabIndex = 18;
            this.button2.Text = "Back";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.guna2Button_back_Click);
            // 
            // button_Send_pending
            // 
            this.button_Send_pending.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_Send_pending.Font = new System.Drawing.Font("Cairo", 14F, System.Drawing.FontStyle.Bold);
            this.button_Send_pending.Location = new System.Drawing.Point(1139, 6);
            this.button_Send_pending.Name = "button_Send_pending";
            this.button_Send_pending.Size = new System.Drawing.Size(124, 42);
            this.button_Send_pending.TabIndex = 10;
            this.button_Send_pending.Text = "Next";
            this.button_Send_pending.UseVisualStyleBackColor = true;
            this.button_Send_pending.Click += new System.EventHandler(this.button_Send_pending_Click);
            // 
            // label_note
            // 
            this.label_note.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_note.Location = new System.Drawing.Point(133, 5);
            this.label_note.Name = "label_note";
            this.label_note.Size = new System.Drawing.Size(1000, 42);
            this.label_note.TabIndex = 1;
            this.label_note.Text = "label1";
            this.label_note.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2ProgressBar1
            // 
            this.guna2ProgressBar1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.guna2ProgressBar1.BorderRadius = 2;
            this.guna2ProgressBar1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.guna2ProgressBar1.Location = new System.Drawing.Point(3, 51);
            this.guna2ProgressBar1.Name = "guna2ProgressBar1";
            this.guna2ProgressBar1.ShadowDecoration.Parent = this.guna2ProgressBar1;
            this.guna2ProgressBar1.Size = new System.Drawing.Size(1260, 13);
            this.guna2ProgressBar1.TabIndex = 17;
            this.guna2ProgressBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // Step_2_Scan_Items
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1276, 815);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Cairo", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "Step_2_Scan_Items";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Step_2_Scan_Items";
            this.Load += new System.EventHandler(this.Step_2_Scan_Items_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2ProgressBar guna2ProgressBar1;
        private System.Windows.Forms.Label label_note;
        private System.Windows.Forms.TextBox textBox_manully;
        private System.Windows.Forms.Button button_Del_All;
        private System.Windows.Forms.TextBox textBox_barcode;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button_Send_pending;
    }
}