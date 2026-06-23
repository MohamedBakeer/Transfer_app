using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transfer_app.Pages.Internal_Transfer
{
    public partial class Step_2_Scan_Items : Form
    {
        Cls.component component = new Cls.component();
        public Step_2_Scan_Items()
        {
            InitializeComponent();
            component.SetRoundedForm(this, 20);
            

            textBox_barcode.KeyDown += textBox_barcode_KeyDown;
            dataGridView1.Click += (s, e) => textBox_barcode.Focus();
            this.Shown += (s, e) => textBox_barcode.Focus();
        }

        private void guna2Button_back_Click(object sender, EventArgs e)
        {
            Pages.Internal_Transfer.Step_1_Select_Direction frm = new Pages.Internal_Transfer.Step_1_Select_Direction();
            frm.Show();
            this.Close();
        }
        void SetupGrid()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("Barcode", "Barcode");
            dataGridView1.Columns.Add("Zone", "Zone");
            dataGridView1.Columns.Add("QtyScan", "Qty Scanned");
            dataGridView1.Columns.Add("QtyWhs", "Qty In From");
            dataGridView1.Columns.Add("QtyTo", "Qty In To");

            dataGridView1.DefaultCellStyle.Font = new Font("Cairo", 15);
            dataGridView1.RowTemplate.Height = 40;

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

            Dictionary<string, int> stock = new Dictionary<string, int>();

            Dictionary<string, int> stockTo = new Dictionary<string, int>();

        void LoadExcelStock()
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string whFolder = Path.Combine(Application.StartupPath, "WH");

                string[] files = Directory.GetFiles(whFolder, "*.xlsx");

                if (files.Length == 0)
                {
                    MessageBox.Show("❌ لا يوجد ملف داخل WH");
                    return;
                }

                string path = files[0];

                stock.Clear();
                stockTo.Clear();

                using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataTable table = reader.AsDataSet().Tables[0];

                    int colBarcode = -1;
                    int colQty = -1;
                    int colQtyTo = -1;

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        string name = table.Rows[0][i].ToString().Trim().ToLower();

                        if (name == "barcode") colBarcode = i;
                        if (name == "qty") colQty = i;
                        if (name == "qty_to") colQtyTo = i;
                    }

                    for (int r = 1; r < table.Rows.Count; r++)
                    {
                        string barcode = NormalizeBarcode(table.Rows[r][colBarcode].ToString());

                        int qty = 0;
                        int.TryParse(table.Rows[r][colQty].ToString(), out qty);

                        int qtyTo = 0;
                        int.TryParse(table.Rows[r][colQtyTo].ToString(), out qtyTo);

                        if (barcode != "" && !stock.ContainsKey(barcode))
                        {
                            stock.Add(barcode, qty);
                            stockTo.Add(barcode, qtyTo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ خطأ في قراءة ملف WH:\n" + ex.Message);
            }
        }

        Dictionary<string, List<string>> zones = new Dictionary<string, List<string>>();

        void LoadZoneFile()
        {
            try
            {
                zones.Clear();

                string fromWhs = Class.TransferSession.FromCode;

                string zoneFileName = "";

                if (fromWhs == "BG-WH-01") zoneFileName = "M01WH.xlsx";
                if (fromWhs == "BG-BR-01") zoneFileName = "M01BR.xlsx";

                if (fromWhs == "TR-WH-01") zoneFileName = "M02WH.xlsx";
                if (fromWhs == "TR-BR-01") zoneFileName = "M02BR.xlsx";

                if (fromWhs == "TR-WH-03") zoneFileName = "M03WH.xlsx";
                if (fromWhs == "TR-BR-03") zoneFileName = "M03BR.xlsx";

                if (zoneFileName == "")
                    return;

                string zoneFolder =
                    Path.Combine(Application.StartupPath, "ZONE");

                string zonePath =
                    Path.Combine(zoneFolder, zoneFileName);

                if (!File.Exists(zonePath))
                    return;

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                using (var stream = File.Open(zonePath, FileMode.Open, FileAccess.Read))
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataTable table = reader.AsDataSet().Tables[0];

                    int colBarcode = -1;
                    int colZone = -1;

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        string name =
                            table.Rows[0][i].ToString().Trim().ToLower();

                        if (name == "barcode") colBarcode = i;
                        if (name == "zone") colZone = i;
                    }

                    for (int r = 1; r < table.Rows.Count; r++)
                    {
                        string barcode =
                            table.Rows[r][colBarcode].ToString().Trim();

                        string zone =
                            table.Rows[r][colZone].ToString().Trim();

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
            }
        }

        void HandleScan(string barcode)
        {
            if (barcode == "")
                return;

            if (!stock.ContainsKey(barcode))
            {
                AddMissing(barcode, "Not Found In Selected Location");
                ShowErrorAlert("❌ الصنف غير موجود في المخزن");
                return;
            }

            int whsQty = stock[barcode];

            if (whsQty <= 0)
            {
                AddMissing(barcode, "No Available Qty");
                ShowErrorAlert("❌ لا توجد كمية متاحة لهذا الصنف");
                return;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value.ToString() == barcode)
                {
                    int scannedQty = Convert.ToInt32(row.Cells[2].Value);

                    if (scannedQty + 1 > whsQty)
                    {
                        AddMissing(barcode, "Need Stock Count");
                        ShowErrorAlert("❌ تم الوصول للكمية المتاحة\nضع باقي الصنف في سلة الجرد");
                        return;
                    }

                    row.Cells[2].Value = scannedQty + 1;

                    HighlightRow(row);
                    ScrollToRow(row);

                    return;
                }
            }

            int toQty = stockTo.ContainsKey(barcode) ? stockTo[barcode] : 0;

            string zoneText = GetZonesText(barcode);

            int index = dataGridView1.Rows.Add(
                barcode,
                zoneText,
                1,
                whsQty,
                toQty
            );

            DataGridViewRow newRow = dataGridView1.Rows[index];

            HighlightRow(newRow);
            ScrollToRow(newRow);
        }

        void ScrollToRow(DataGridViewRow row)
        {
            dataGridView1.ClearSelection();

            row.Selected = true;

            dataGridView1.CurrentCell = row.Cells[0];

            dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
        }

        void ShowErrorAlert(string message)
        {
            ErrorBeep();

            Color oldColor = this.BackColor;

            this.BackColor = Color.Red;
            dataGridView1.BackgroundColor = Color.MistyRose;

            MessageBox.Show(message);

            this.BackColor = oldColor;
            dataGridView1.BackgroundColor = Color.White;

            textBox_barcode.Focus();
        }

        void ErrorBeep()
        {
            System.Media.SystemSounds.Hand.Play();
        }
        string GetZonesText(string barcode)
        {
            if (zones.ContainsKey(barcode))
            {
                return string.Join(" / ", zones[barcode]);
            }

            return "";
        }
        void HighlightRow(DataGridViewRow activeRow)
        {
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                r.DefaultCellStyle.BackColor = Color.White;
                r.DefaultCellStyle.ForeColor = Color.Black;
            }

            activeRow.DefaultCellStyle.BackColor = Color.LightGreen;
            activeRow.DefaultCellStyle.ForeColor = Color.Black;

            dataGridView1.Refresh();
        }
        private void textBox_barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barcode = NormalizeBarcode(textBox_barcode.Text);

                if (barcode == "")
                    return;

                HandleScan(barcode);

                textBox_barcode.Clear();

                textBox_barcode.Focus();

                e.SuppressKeyPress = true;
            }
        }

        private void Step_2_Scan_Items_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadExcelStock();
            LoadZoneFile();

            label_note.Text =
                $"{Class.TransferSession.FromName} ({Class.TransferSession.FromCode})  →  " +
                $"{Class.TransferSession.ToName} ({Class.TransferSession.ToCode})";
        }

        string NormalizeBarcode(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";

            StringBuilder sb = new StringBuilder();

            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

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

        private void button_Send_pending_Click(object sender, EventArgs e)
        {
            if (!component.LockButton(button_Send_pending))
                return;

            try
            {


                guna2ProgressBar1.Value = 0;
                Application.DoEvents();

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("❌ لا يوجد أصناف في القائمة");
                    textBox_barcode.Focus();
                    return;
                }

                guna2ProgressBar1.Value = 20;
                Application.DoEvents();

                int scannedTotal = 0;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        int qty = 0;
                        int.TryParse(row.Cells[2].Value?.ToString(), out qty);
                        scannedTotal += qty;
                    }
                }

                guna2ProgressBar1.Value = 40;
                Application.DoEvents();

                int manualTotal = 0;

                if (!int.TryParse(textBox_manully.Text, out manualTotal))
                {
                    MessageBox.Show("❌ الرجاء إدخال العدد اليدوي");
                    textBox_manully.Focus();
                    guna2ProgressBar1.Value = 0;
                    return;
                }

                if (manualTotal != scannedTotal)
                {
                    MessageBox.Show("❌ العدد غير مطابق");
                    textBox_barcode.Focus();
                    guna2ProgressBar1.Value = 0;
                    return;
                }

                guna2ProgressBar1.Value = 70;
                Application.DoEvents();

                Class.TransferSession.PrepareItemsTable();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        Class.TransferSession.ItemsTable.Rows.Add(
                            row.Cells[0].Value?.ToString(),
                            row.Cells[1].Value?.ToString(),
                            row.Cells[2].Value?.ToString(),
                            row.Cells[3].Value?.ToString(),
                            row.Cells[4].Value?.ToString()
                        );
                    }
                }

                Class.TransferSession.PrepareMissingTable();

                foreach (var item in missingItems.Values)
                {
                    Class.TransferSession.MissingTable.Rows.Add(
                        item.Barcode,
                        item.ErrorType,
                        item.Count,
                        item.LastTime.ToString("yyyy-MM-dd HH:mm:ss")
                    );
                }

                guna2ProgressBar1.Value = 100;
                Application.DoEvents();

                Pages.Internal_Transfer.Step_3_Review___Save frm =
                    new Pages.Internal_Transfer.Step_3_Review___Save();

                frm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                component.UnlockButton(button_Send_pending);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            if (dataGridView1.CurrentRow.IsNewRow)
                return;

            string barcode = dataGridView1.CurrentRow.Cells[0].Value?.ToString();
            string zone = dataGridView1.CurrentRow.Cells[1].Value?.ToString();
            string qtyScan = dataGridView1.CurrentRow.Cells[2].Value?.ToString();
            string qtyWhs = dataGridView1.CurrentRow.Cells[3].Value?.ToString();

            DialogResult result = MessageBox.Show(
                "هل تريد حذف هذا الصنف؟\n\n" +
                "Barcode: " + barcode + "\n" +
                "Zone: " + zone + "\n" +
                "Qty Scanned: " + qtyScan + "\n" +
                "Qty In From: " + qtyWhs,
                "تأكيد حذف صنف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }

            textBox_barcode.Focus();
        }
    }
}
