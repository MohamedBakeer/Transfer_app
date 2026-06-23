using ClosedXML.Excel;
using ExcelDataReader;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transfer_app;
namespace TransferApp
{
    public partial class TransferOld : Form
    {

        Transfer_app.Cls.component component = new Transfer_app.Cls.component();
        Dictionary<string, int> stock = new Dictionary<string, int>();
        Dictionary<string, int> stockTo = new Dictionary<string, int>();

        Dictionary<string, List<string>> zones = new Dictionary<string, List<string>>();


        public TransferOld()
        {
           

                InitializeComponent();

                component.SetRoundedForm(this, 20);
                textBox_barcode.KeyDown += textBox_barcode_KeyDown;
                button_Del_All.Click += button_Del_All_Click;
                dataGridView1.Click += dataGridView1_Click;
                this.Click += Form1_Click;
                
                SetupGrid();
                LoadExcelStock();
                LoadZoneFile();
                load_from_to();

                

                this.Shown += (s, e) => textBox_barcode.Focus();
            
        }
        

        void HighlightRow(DataGridViewRow activeRow)
        {
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                r.DefaultCellStyle.BackColor = Color.White;
            }

            activeRow.DefaultCellStyle.BackColor = Color.LightGreen;
            activeRow.DefaultCellStyle.ForeColor = Color.Black;

            dataGridView1.Refresh();   // 🔥 إجبار إعادة الرسم
        }



        void ScrollToLastRow()
        {
            if (dataGridView1.Rows.Count == 0) return;

            int lastRow = dataGridView1.Rows.Count - 1;

            dataGridView1.ClearSelection();
            dataGridView1.Rows[lastRow].Selected = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[lastRow].Cells[0];
            dataGridView1.FirstDisplayedScrollingRowIndex = lastRow;
        }


        public Dictionary<string, string> whsMap = new Dictionary<string, string>()
        {
            {"Whs Benghazi", "BG-WH-01"},
            {"Floor Benghazi", "BG-BR-01"},
            {"Whs Jraba", "TR-WH-01"},
            {"Floor Jraba", "TR-BR-01"},
            {"Whs Seyahia", "TR-WH-03"},
            {"Floor Seyahia", "TR-BR-03"},
        };

        private string GetLocationName(string code)
        {
            var item = whsMap.FirstOrDefault(x => x.Value == code);
            return item.Key ?? code; // if not found, show code
        }

        // =========================
        // Load From / To from Settings
        // =========================
        private void load_from_to()
        {
            string fromCode = Transfer_app.Properties.Settings.Default.from;
            string toCode = Transfer_app.Properties.Settings.Default.to;
            string app_for = Transfer_app.Properties.Settings.Default.thisWHs;

            string fromName = GetLocationName(fromCode);
            string toName = GetLocationName(toCode);
            string thisWhsName = GetLocationName(app_for);


            label1.Text = $"Transfer Item From: {fromName}   To: {toName}";
            label2.Text = $"Transfer App - Current WHS: {thisWhsName}";
        }


        string NormalizeBarcode(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "";

            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    int d = (int)char.GetNumericValue(c);
                    if (d >= 0 && d <= 9)
                        sb.Append(d.ToString());
                }
            }
            return sb.ToString();
        }

        void SetupGrid()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("Barcode", "Barcode");
            dataGridView1.Columns.Add("QtyScan", "Qty Scanned");
            dataGridView1.Columns.Add("QtyWhs", "Qty In From");
            dataGridView1.Columns.Add("QtyTo", "Qty In To");
            dataGridView1.Columns.Add("Zone", "Zone");

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.BackColor;
            dataGridView1.DefaultCellStyle.SelectionForeColor = dataGridView1.DefaultCellStyle.ForeColor;

            dataGridView1.MultiSelect = false;
        }


        void LoadExcelStock()
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string whFolder = Path.Combine(Application.StartupPath, "WH");

                if (!Directory.Exists(whFolder))
                {
                    MessageBox.Show("❌ مجلد WH غير موجود:\n" + whFolder);
                    Directory.CreateDirectory(whFolder);
                    return;
                }

                string[] files = Directory.GetFiles(whFolder, "*.xlsx");

                if (files.Length == 0)
                {
                    MessageBox.Show("❌ لا يوجد أي ملف Excel داخل مجلد WH");
                    return;
                }

                string path = files[0]; // أول ملف

                using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var ds = reader.AsDataSet();

                    if (ds.Tables.Count == 0)
                    {
                        MessageBox.Show("❌ ملف الإكسل لا يحتوي على Sheets");
                        return;
                    }

                    DataTable table = ds.Tables[0]; // أول Sheet

                    int colBarcode = -1, colQty = -1, colQtyTo = -1;

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        string name = table.Rows[0][i].ToString().Trim().ToLower();

                        if (name == "barcode") colBarcode = i;
                        if (name == "qty") colQty = i;
                        if (name == "qty_to") colQtyTo = i;
                    }

                    if (colBarcode == -1 || colQty == -1)
                    {
                        MessageBox.Show("❌ يجب أن يحتوي الملف على أعمدة باسم barcode و qty");
                        return;
                    }

                    stock.Clear();
                    stockTo.Clear();

                    stock.Clear();
                    stockTo.Clear();

                    for (int r = 1; r < table.Rows.Count; r++)
                    {
                        object cell = table.Rows[r][colBarcode];
                        string raw = (cell is double) ? ((double)cell).ToString("0") : cell.ToString();

                        string barcode = NormalizeBarcode(raw);

                        int qty = 0;
                        int.TryParse(table.Rows[r][colQty].ToString(), out qty);

                        int qtyTo = 0;
                        if (colQtyTo != -1)
                        {
                            int.TryParse(table.Rows[r][colQtyTo].ToString(), out qtyTo);
                        }

                        if (barcode != "" && !stock.ContainsKey(barcode))
                        {
                            stock.Add(barcode, qty);       // كمية From
                            stockTo.Add(barcode, qtyTo);   // كمية To
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ خطأ في قراءة الإكسل:\n" + ex.Message);
            }
        }

        private void textBox_barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string scanned = NormalizeBarcode(textBox_barcode.Text);
                HandleScan(scanned);
                textBox_barcode.Clear();
                textBox_barcode.Focus();
                e.SuppressKeyPress = true;
            }
        }

 
        void ErrorBeep()
        {
            SystemSounds.Hand.Play();
        }

        // =============================
        // Scan Logic
        // =============================
        void HandleScan(string barcode)
        {
            if (barcode == "") return;

            if (!stock.ContainsKey(barcode))
            {
                AddMissing(barcode, "Not Found In From WHS");

                this.BackColor = Color.Red;
                ErrorBeep();
                MessageBox.Show("❌ الصنف غير موجود في المخزن");
                this.BackColor = Color.White;
                return;
            }

            int whsQty = stock[barcode];

            //if (whsQty == 0)
            //{
            //    AddMissing(barcode, "Zero Qty In From WHS");

            //    ErrorBeep();
            //    this.BackColor = Color.Red;
            //    MessageBox.Show("❌ الكمية صفر — يحتاج جرد");
            //    this.BackColor = Color.White;
            //    return;
            //}

            if (whsQty <= 0)
            {
                AddMissing(barcode, "Invalid Qty In From WHS");

                ErrorBeep();
                this.BackColor = Color.Red;
                MessageBox.Show("❌ الكمية غير صالحة (صفر أو سالبة)");
                this.BackColor = Color.White;
                return;
            }

            // 🔁 لو الصنف موجود مسبقًا
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value.ToString() == barcode)
                {
                    int scanned = Convert.ToInt32(row.Cells[1].Value);

                    if (scanned + 1 > whsQty)
                    {
                        AddMissing(barcode, "Exceeded From WHS Qty");


                        ErrorBeep();
                        this.BackColor = Color.Red;
                        MessageBox.Show("❌ تم تجاوز كمية المخزن");
                        this.BackColor = Color.White;
                        return;
                    }

                    row.Cells[1].Value = scanned + 1;

                    // ✅ انتقل مباشرة إلى الصف الذي تم تحديثه
                    dataGridView1.ClearSelection();
                    row.Selected = false;   // مهم لإلغاء اللون الأزرق

                    dataGridView1.CurrentCell = row.Cells[0];
                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;

                    // ✅ تلوين الصف
                    HighlightRow(row);
                    return;


                }
            }

            // ➕ إضافة صف جديد
            int toQty = stockTo.ContainsKey(barcode) ? stockTo[barcode] : 0;
            string zoneText = GetZonesText(barcode);

            int idx = dataGridView1.Rows.Add(
                barcode,
                1,
                whsQty,
                toQty,
                zoneText
            );
            var newRow = dataGridView1.Rows[idx];

            // ✅ تلوين الصف الجديد
            HighlightRow(newRow);

            // ✅ سكرول بعد الإضافة مباشرة
            ScrollToLastRow();

        }



        // =============================
        // Delete All Button
        // =============================
        private void button_Del_All_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) return;

            var r = MessageBox.Show("هل تريد حذف كل الأصناف من القائمة؟", "تأكيد", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                dataGridView1.Rows.Clear();
            }

            textBox_barcode.Focus();
        }

        // =============================
        // Export to Excel + Delete All After
        // =============================

        Dictionary<string, MissingItem> missingItems = new Dictionary<string, MissingItem>();
        class MissingItem
        {
            public string Barcode { get; set; }
            public string ErrorType { get; set; }
            public int Count { get; set; }
            public DateTime LastTime { get; set; }
        }

        void AddMissing(string barcode, string errorType)
        {
            string key = barcode + "|" + errorType;

            if (missingItems.ContainsKey(key))
            {
                missingItems[key].Count++;
                missingItems[key].LastTime = DateTime.Now;
            }
            else
            {
                missingItems.Add(key, new MissingItem
                {
                    Barcode = barcode,
                    ErrorType = errorType,
                    Count = 1,
                    LastTime = DateTime.Now
                });
            }
        }


        private void button_Export_Click(object sender, EventArgs e)
        {
            // ===== تحقق من المجموع أولاً =====
            int totalManual;
            if (!int.TryParse(textBox_manully.Text, out totalManual))
            {
                MessageBox.Show("❌ الرجاء إدخال القيمة التي تم عدها يدوياً");
                textBox_barcode.Focus();
                return;
            }

            int totalScanned = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    int value;
                    if (int.TryParse(row.Cells[1].Value?.ToString(), out value))
                    {
                        totalScanned += value;
                    }
                }
            }

            if (totalScanned != totalManual)
            {
                MessageBox.Show("❌ المجموع غير مطابق، يرجى التحقق من البيانات");
                textBox_barcode.Focus();
                return;
            }

            string From_WHS = Transfer_app.Properties.Settings.Default.from;
            string To_Branch = Transfer_app.Properties.Settings.Default.to;
            string UoM = "Pcs";

            // المجلد الرئيسي
            string ResultFolder = Path.Combine(Application.StartupPath, "Excel");

            // إنشاء المجلد الرئيسي إذا غير موجود
            if (!Directory.Exists(ResultFolder))
            {
                Directory.CreateDirectory(ResultFolder);
            }

            // اسم مجلد التحويل حسب المخازن
            string TransferFolderName = From_WHS + "_TO_" + To_Branch;

            // المسار الكامل
            string TransferFolder = Path.Combine(ResultFolder, TransferFolderName);

            // إنشاء مجلد المخازن إذا غير موجود
            if (!Directory.Exists(TransferFolder))
            {
                Directory.CreateDirectory(TransferFolder);
            }

            // اسم الملف
            string filePath = Path.Combine(
                TransferFolder,
                From_WHS + "___" + To_Branch + "___" +
                DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx"
            );


            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("لا يوجد بيانات للتصدير");
                textBox_barcode.Focus();
                return;
            }

            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "Excel File (*.xlsx)|*.xlsx";
            //sfd.FileName = From_WHS + "___" + To_Branch + "___" +
            //               DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";

            //if (sfd.ShowDialog() != DialogResult.OK)
            //{
            //    textBox_barcode.Focus();
            //    return;
            //}

            try
            {
                using (var wb = new XLWorkbook())
                {
                    // ================= SHEET 1: Transfer =================
                    var ws = wb.Worksheets.Add("Transfer");

                    ws.Cell(1, 1).Value = "Barcode";
                    ws.Cell(1, 2).Value = "From";
                    ws.Cell(1, 3).Value = "Qty";
                    ws.Cell(1, 4).Value = "UoM";
                    ws.Cell(1, 5).Value = "To";

                    int row = 2;

                    foreach (DataGridViewRow dgRow in dataGridView1.Rows)
                    {
                        ws.Cell(row, 1).Value = dgRow.Cells[0].Value.ToString();
                        ws.Cell(row, 2).Value = From_WHS;
                        ws.Cell(row, 3).Value = dgRow.Cells[1].Value.ToString();
                        ws.Cell(row, 4).Value = UoM;
                        ws.Cell(row, 5).Value = To_Branch;

                        row++;
                    }

                    ws.Columns().AdjustToContents();

                    // ================= SHEET 2: MISSING =================
                    var wsMissing = wb.Worksheets.Add("MISSING");

                    wsMissing.Cell(1, 1).Value = "Barcode";
                    wsMissing.Cell(1, 2).Value = "Error Type";
                    wsMissing.Cell(1, 3).Value = "Count";
                    wsMissing.Cell(1, 4).Value = "Last Time";

                    int missingRow = 2;

                    foreach (var item in missingItems.Values)
                    {
                        wsMissing.Cell(missingRow, 1).Value = item.Barcode;
                        wsMissing.Cell(missingRow, 2).Value = item.ErrorType;
                        wsMissing.Cell(missingRow, 3).Value = item.Count;
                        wsMissing.Cell(missingRow, 4).Value = item.LastTime.ToString("yyyy-MM-dd HH:mm:ss");

                        missingRow++;
                    }

                    wsMissing.Columns().AdjustToContents();

                    ws.Protect("4480");
                    wsMissing.Protect("4480");
                    wb.Protect("4480");

                    // منع مشاكل الطباعة عند فتح/إغلاق الملف
                    ws.PageSetup.PrintAreas.Clear();
                    wsMissing.PageSetup.PrintAreas.Clear();

                    ws.SheetView.View = XLSheetViewOptions.Normal;
                    wsMissing.SheetView.View = XLSheetViewOptions.Normal;

                    wb.SaveAs(filePath);
                    //wb.SaveAs(sfd.FileName);

                }

                MessageBox.Show("✅ تم حفظ الملف بنجاح");

                dataGridView1.Rows.Clear();
                missingItems.Clear();
                textBox_manully.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ خطأ أثناء الحفظ:\n" + ex.Message);
            }

            textBox_barcode.Focus();
        }




        private void button_Close_Click(object sender, EventArgs e)
        {
            LoginFrm loginFrm = new LoginFrm();
            this.Close();
            loginFrm.ShowDialog();
        }

        // =============================
        // Keep Focus on TextBox
        // =============================
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBox_barcode.Focus();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            textBox_barcode.Focus();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.CurrentRow.Selected)
                {
                    var r = MessageBox.Show("هل تريد حذف هذا الصنف من القائمة؟", "تأكيد", MessageBoxButtons.YesNo);
                    if (r == DialogResult.Yes)
                    {
                        int index = dataGridView1.CurrentCell.RowIndex;
                        //MessageBox.Show("Row Index: " + index);
                        dataGridView1.Rows.RemoveAt(index);
                    }
                }
            }
        }



        private bool ShowPasswordDialog()
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 160,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Enter Password",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false,
                Icon = SystemIcons.Shield
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Text = "Password:" };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 240, PasswordChar = '*' };

            Button confirmation = new Button() { Text = "OK", Left = 100, Width = 80, Top = 85, DialogResult = DialogResult.OK };

            confirmation.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK && textBox.Text == "4480"; // <-- password here
        }


        private void button_Setting_Click(object sender, EventArgs e)
        {
            if (ShowPasswordDialog())
            {
                Setting setting = new Setting();
                setting.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wrong password!");
            }
            load_from_to();
            textBox_barcode.Focus();
        }

        Transfer_app.Cls.db db = new Transfer_app.Cls.db();

        void sync_data()
        {
            try
            {
                if (Transfer_app.Properties.Settings.Default.thisWHs != string.Empty)
                {



                    string fromWhs = Transfer_app.Properties.Settings.Default.from;
                    string toWhs = Transfer_app.Properties.Settings.Default.to;

                    string whFolder = Path.Combine(Application.StartupPath, "WH");

                    // 1️⃣ تأكد من وجود مجلد WH
                    if (!Directory.Exists(whFolder))
                        Directory.CreateDirectory(whFolder);

                    // 2️⃣ مسح كل الملفات القديمة داخل WH
                    foreach (string file in Directory.GetFiles(whFolder, "*.xlsx"))
                    {
                        File.Delete(file);
                    }

                    // 3️⃣ اسم الملف الجديد
                    string filePath = Path.Combine(
                        whFolder,
                        fromWhs + "___" +
                        toWhs + "___" +
                        DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") +
                        ".xlsx"
                    );

                    db.openconn();

                    fromWhs = Transfer_app.Properties.Settings.Default.from;
                    toWhs = Transfer_app.Properties.Settings.Default.to;

                    string sql = @"
                            SELECT
                                T0.CodeBars AS Barcode,
                                SUM(CASE WHEN T1.WhsCode = @FromWhs THEN T1.OnHand ELSE 0 END) AS QtyFrom,
                                SUM(CASE WHEN T1.WhsCode = @ToWhs THEN T1.OnHand ELSE 0 END) AS QtyTo
                            FROM OITM T0
                            INNER JOIN OITW T1 
                                ON T0.ItemCode = T1.ItemCode
                            WHERE T1.WhsCode IN (@FromWhs, @ToWhs)
                              AND ISNULL(T0.CodeBars, '') <> ''
                            GROUP BY T0.CodeBars
                            ORDER BY T0.CodeBars";

                    SqlCommand cmd = new SqlCommand(sql, db.getconn);
                    cmd.Parameters.AddWithValue("@FromWhs", fromWhs);
                    cmd.Parameters.AddWithValue("@ToWhs", toWhs);
                    SqlDataReader dr = cmd.ExecuteReader();

                    // 4️⃣ إنشاء ملف Excel جديد
                    using (var wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add("Stock");

                        ws.Cell(1, 1).Value = "barcode";
                        ws.Cell(1, 2).Value = "qty";
                        ws.Cell(1, 3).Value = "qty_to";

                        int row = 2;

                        while (dr.Read())
                        {
                            ws.Cell(row, 1).Value = dr["Barcode"].ToString();
                            ws.Cell(row, 2).Value = Convert.ToInt32(dr["QtyFrom"]);
                            ws.Cell(row, 3).Value = Convert.ToInt32(dr["QtyTo"]);
                            row++;
                        }

                        ws.Columns().AdjustToContents();
                        wb.SaveAs(filePath);
                    }

                    dr.Close();
                    db.closeconn();

                    // 5️⃣ إعادة تحميل الأصناف من الملف الجديد
                    stock.Clear();
                    LoadExcelStock();

                    MessageBox.Show("✅ The items file has been successfully updated and re-uploaded.");
                    textBox_barcode.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error:\n" + ex.Message);
            }
        }
        private void iconButton_update_xlx_Click(object sender, EventArgs e)
        {
            sync_data();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

            if (Transfer_app.Properties.Settings.Default.AutoUpdate == "True")
            {
                sync_data();
            }
        }

        bool printMode = false;

        private void UpdatePrintMode()
        {
            printMode = guna2CheckBox_print.Checked;

            MessageBox.Show(
                "Print mode is " + (printMode ? "ON" : "OFF") +
                "\n(Feature not implemented yet)"
            );
        }

        private void iconPictureBox_Print_Click(object sender, EventArgs e)
        {
            // تغيير حالة الـ CheckBox عند الضغط على الأيقونة
            guna2CheckBox_print.Checked = !guna2CheckBox_print.Checked;
        }

        private void guna2CheckBox_print_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrintMode();
        }

        string GetZoneFileNameFromWhsCode(string whsCode)
        {
            if (whsCode == "BG-WH-01") return "M01WH.xlsx";
            if (whsCode == "BG-BR-01") return "M01BR.xlsx";

            if (whsCode == "TR-WH-01") return "M02WH.xlsx";
            if (whsCode == "TR-BR-01") return "M02BR.xlsx";

            if (whsCode == "TR-WH-03") return "M03WH.xlsx";
            if (whsCode == "TR-BR-03") return "M03BR.xlsx";

            return "";
        }

        void LoadZoneFile()
        {
            try
            {
                zones.Clear();

                string fromWhs = Transfer_app.Properties.Settings.Default.from;
                string zoneFileName = GetZoneFileNameFromWhsCode(fromWhs);

                if (zoneFileName == "")
                    return;

                string zoneFolder = Path.Combine(Application.StartupPath, "ZONE");
                string zonePath = Path.Combine(zoneFolder, zoneFileName);

                if (!Directory.Exists(zoneFolder))
                {
                    Directory.CreateDirectory(zoneFolder);
                    return;
                }

                if (!File.Exists(zonePath))
                    return;

                using (var stream = File.Open(zonePath, FileMode.Open, FileAccess.Read))
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var ds = reader.AsDataSet();

                    if (ds.Tables.Count == 0)
                        return;

                    DataTable table = ds.Tables[0];

                    int colBarcode = -1;
                    int colZone = -1;

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        string name = table.Rows[0][i].ToString().Trim().ToLower();

                        if (name == "barcode") colBarcode = i;
                        if (name == "zone") colZone = i;
                    }

                    if (colBarcode == -1 || colZone == -1)
                        return;

                    for (int r = 1; r < table.Rows.Count; r++)
                    {
                        object cell = table.Rows[r][colBarcode];

                        string raw = (cell is double)
                            ? ((double)cell).ToString("0")
                            : cell.ToString();

                        string barcode = NormalizeBarcode(raw);
                        string zone = table.Rows[r][colZone].ToString().Trim();

                        if (barcode == "" || zone == "")
                            continue;

                        if (!zones.ContainsKey(barcode))
                            zones[barcode] = new List<string>();

                        if (!zones[barcode].Contains(zone))
                            zones[barcode].Add(zone);
                    }
                }
            }
            catch
            {
                // لا توقف البرنامج لو ملف ZONE فيه مشكلة
            }
        }

        string GetZonesText(string barcode)
        {
            if (zones.ContainsKey(barcode))
                return string.Join("\\", zones[barcode]);

            return "";
        }


    }
}


