namespace Transfer_app.Pages.Area_Monitor
{
    partial class ShelfsFrm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.guna2Button_back = new Guna.UI2.WinForms.Guna2Button();
            this.flowLayoutPanel_shelfs = new System.Windows.Forms.FlowLayoutPanel();
            this.label_title = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel_shelfs);
            this.panel1.Controls.Add(this.label_title);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1174, 796);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.guna2Button_back);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 740);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1174, 56);
            this.panel2.TabIndex = 0;
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
            this.guna2Button_back.Location = new System.Drawing.Point(12, 8);
            this.guna2Button_back.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Button_back.Name = "guna2Button_back";
            this.guna2Button_back.ShadowDecoration.Parent = this.guna2Button_back;
            this.guna2Button_back.Size = new System.Drawing.Size(124, 42);
            this.guna2Button_back.TabIndex = 32;
            this.guna2Button_back.Text = "Back";
            this.guna2Button_back.Click += new System.EventHandler(this.guna2Button_back_Click);
            // 
            // flowLayoutPanel_shelfs
            // 
            this.flowLayoutPanel_shelfs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_shelfs.Location = new System.Drawing.Point(0, 33);
            this.flowLayoutPanel_shelfs.Name = "flowLayoutPanel_shelfs";
            this.flowLayoutPanel_shelfs.Size = new System.Drawing.Size(1174, 707);
            this.flowLayoutPanel_shelfs.TabIndex = 1;
            // 
            // label_title
            // 
            this.label_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_title.Font = new System.Drawing.Font("Cairo", 12.25F);
            this.label_title.Location = new System.Drawing.Point(0, 0);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(1174, 33);
            this.label_title.TabIndex = 33;
            this.label_title.Text = "M02 WH - AREA 3";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ShelfsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 806);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Cairo", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "ShelfsFrm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShelfsFrm";
            this.Load += new System.EventHandler(this.ShelfsFrm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_shelfs;
        private Guna.UI2.WinForms.Guna2Button guna2Button_back;
        private System.Windows.Forms.Label label_title;
    }
}