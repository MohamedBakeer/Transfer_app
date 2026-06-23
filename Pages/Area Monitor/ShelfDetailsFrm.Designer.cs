namespace Transfer_app.Pages.Area_Monitor
{
    partial class ShelfDetailsFrm
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
            this.guna2Button_back = new Guna.UI2.WinForms.Guna2Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView_Items = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label_title = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label_total_items = new System.Windows.Forms.Label();
            this.label_total_qty = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Items)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
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
            this.guna2Button_back.Location = new System.Drawing.Point(16, 9);
            this.guna2Button_back.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Button_back.Name = "guna2Button_back";
            this.guna2Button_back.ShadowDecoration.Parent = this.guna2Button_back;
            this.guna2Button_back.Size = new System.Drawing.Size(124, 42);
            this.guna2Button_back.TabIndex = 31;
            this.guna2Button_back.Text = "Back";
            this.guna2Button_back.Click += new System.EventHandler(this.guna2Button_back_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.guna2Button_back);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 744);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1217, 60);
            this.panel3.TabIndex = 29;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView_Items);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1217, 804);
            this.panel1.TabIndex = 1;
            // 
            // dataGridView_Items
            // 
            this.dataGridView_Items.AllowUserToAddRows = false;
            this.dataGridView_Items.AllowUserToDeleteRows = false;
            this.dataGridView_Items.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_Items.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_Items.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Items.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Items.Location = new System.Drawing.Point(0, 78);
            this.dataGridView_Items.Name = "dataGridView_Items";
            this.dataGridView_Items.ReadOnly = true;
            this.dataGridView_Items.Size = new System.Drawing.Size(1217, 666);
            this.dataGridView_Items.TabIndex = 35;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel4);
            this.panel2.Controls.Add(this.tableLayoutPanel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1217, 78);
            this.panel2.TabIndex = 36;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.label_title, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1217, 42);
            this.tableLayoutPanel3.TabIndex = 32;
            // 
            // label_title
            // 
            this.label_title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_title.Font = new System.Drawing.Font("Cairo", 12.25F);
            this.label_title.Location = new System.Drawing.Point(3, 0);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(1211, 42);
            this.label_title.TabIndex = 32;
            this.label_title.Text = "M02 WH - AREA 3";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Controls.Add(this.label_total_qty, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label_total_items, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 42);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1217, 36);
            this.tableLayoutPanel4.TabIndex = 33;
            // 
            // label_total_items
            // 
            this.label_total_items.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_total_items.Font = new System.Drawing.Font("Cairo", 12.25F);
            this.label_total_items.Location = new System.Drawing.Point(611, 0);
            this.label_total_items.Name = "label_total_items";
            this.label_total_items.Size = new System.Drawing.Size(603, 36);
            this.label_total_items.TabIndex = 30;
            this.label_total_items.Text = "Total Items : 0";
            this.label_total_items.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_total_qty
            // 
            this.label_total_qty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_total_qty.Font = new System.Drawing.Font("Cairo", 12.25F);
            this.label_total_qty.Location = new System.Drawing.Point(3, 0);
            this.label_total_qty.Name = "label_total_qty";
            this.label_total_qty.Size = new System.Drawing.Size(602, 36);
            this.label_total_qty.TabIndex = 30;
            this.label_total_qty.Text = "Total Qty : 0";
            this.label_total_qty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ShelfDetailsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1217, 804);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Cairo", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "ShelfDetailsFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShelfDetailsFrm";
            this.Load += new System.EventHandler(this.ShelfDetailsFrm_Load);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Items)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button guna2Button_back;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView_Items;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label_total_items;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_total_qty;
    }
}