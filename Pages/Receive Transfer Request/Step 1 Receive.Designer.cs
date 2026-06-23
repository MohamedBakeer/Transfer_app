namespace Transfer_app.Pages.Receive_Transfer_Request
{
    partial class Step_1_Receive
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
            this.textBox_BOX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Button_Start_Scan = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button_back = new Guna.UI2.WinForms.Guna2Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.guna2Button_back);
            this.panel1.Controls.Add(this.guna2Button_Start_Scan);
            this.panel1.Controls.Add(this.textBox_BOX);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(884, 68);
            this.panel1.TabIndex = 0;
            // 
            // textBox_BOX
            // 
            this.textBox_BOX.Font = new System.Drawing.Font("Cairo", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_BOX.Location = new System.Drawing.Point(111, 7);
            this.textBox_BOX.Name = "textBox_BOX";
            this.textBox_BOX.Size = new System.Drawing.Size(553, 52);
            this.textBox_BOX.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cairo", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 36);
            this.label1.TabIndex = 28;
            this.label1.Text = "BOX NO :";
            // 
            // guna2Button_Start_Scan
            // 
            this.guna2Button_Start_Scan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Button_Start_Scan.BorderRadius = 10;
            this.guna2Button_Start_Scan.CheckedState.Parent = this.guna2Button_Start_Scan;
            this.guna2Button_Start_Scan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2Button_Start_Scan.CustomImages.Parent = this.guna2Button_Start_Scan;
            this.guna2Button_Start_Scan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.guna2Button_Start_Scan.Font = new System.Drawing.Font("Cairo", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button_Start_Scan.ForeColor = System.Drawing.Color.Black;
            this.guna2Button_Start_Scan.HoverState.Parent = this.guna2Button_Start_Scan;
            this.guna2Button_Start_Scan.Location = new System.Drawing.Point(672, 7);
            this.guna2Button_Start_Scan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Button_Start_Scan.Name = "guna2Button_Start_Scan";
            this.guna2Button_Start_Scan.ShadowDecoration.Parent = this.guna2Button_Start_Scan;
            this.guna2Button_Start_Scan.Size = new System.Drawing.Size(112, 52);
            this.guna2Button_Start_Scan.TabIndex = 29;
            this.guna2Button_Start_Scan.Text = "Start Scan";
            this.guna2Button_Start_Scan.Click += new System.EventHandler(this.guna2Button_Start_Scan_Click);
            // 
            // guna2Button_back
            // 
            this.guna2Button_back.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Button_back.BorderRadius = 10;
            this.guna2Button_back.CheckedState.Parent = this.guna2Button_back;
            this.guna2Button_back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2Button_back.CustomImages.Parent = this.guna2Button_back;
            this.guna2Button_back.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.guna2Button_back.Font = new System.Drawing.Font("Cairo", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button_back.ForeColor = System.Drawing.Color.Black;
            this.guna2Button_back.HoverState.Parent = this.guna2Button_back;
            this.guna2Button_back.Location = new System.Drawing.Point(790, 8);
            this.guna2Button_back.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Button_back.Name = "guna2Button_back";
            this.guna2Button_back.ShadowDecoration.Parent = this.guna2Button_back;
            this.guna2Button_back.Size = new System.Drawing.Size(91, 52);
            this.guna2Button_back.TabIndex = 30;
            this.guna2Button_back.Text = "Back";
            this.guna2Button_back.Click += new System.EventHandler(this.guna2Button_back_Click);
            // 
            // Step_1_Receive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(894, 78);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Cairo", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "Step_1_Receive";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Step_1_Receive";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_BOX;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button guna2Button_Start_Scan;
        private Guna.UI2.WinForms.Guna2Button guna2Button_back;
    }
}