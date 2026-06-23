using ClosedXML.Excel;
using RestSharp;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;

using System.Drawing;

namespace Transfer_app.Pages.Area_Location_Count
{
    public partial class AreaLocationCount : Form
    {
        Cls.component component = new Cls.component();

        public AreaLocationCount()
        {
            InitializeComponent();
            component.SetRoundedForm(this, 20);

            textBox_barcode.KeyDown += textBox_barcode_KeyDown;
        }

        private void AreaLocationCount_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadLocations();

            guna2ProgressBar1.Value = 0;
            textBox_barcode.Focus();
        }

        void SetupGrid()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("Barcode", "Barcode");
            dataGridView1.Columns.Add("Qty", "Qty");
            dataGridView1.Columns.Add("Area", "Area");
            dataGridView1.Columns.Add("Location", "Location");
            dataGridView1.Columns.Add("Store", "Store");

            dataGridView1.DefaultCellStyle.Font = new Font("Cairo", 15);
            dataGridView1.RowTemplate.Height = 40;

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        void LoadLocations()
        {
            guna2ComboBox_location_to_count.Items.Clear();

            foreach (var item in Class.WhsManager.WhsMap)
            {
                guna2ComboBox_location_to_count.Items.Add(item.Key);
            }

            if (guna2ComboBox_location_to_count.Items.Count > 0)
                guna2ComboBox_location_to_count.SelectedIndex = 0;

            if (guna2ComboBox_Area.Items.Count > 0)
                guna2ComboBox_Area.SelectedIndex = 0;
        }

        string NormalizeBarcode(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";

            string result = "";

            foreach (char c in input)
            {
                if (char.IsDigit(c))
                    result += c;
            }

            return result;
        }

        private void textBox_barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            string barcode = NormalizeBarcode(textBox_barcode.Text);

            if (barcode == "")
                return;

            AddScan(barcode);

            textBox_barcode.Clear();
            textBox_barcode.Focus();

            e.SuppressKeyPress = true;
        }

        void AddScan(string barcode)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Barcode"].Value?.ToString() == barcode)
                {
                    int qty = Convert.ToInt32(row.Cells["Qty"].Value);
                    row.Cells["Qty"].Value = qty + 1;

                    dataGridView1.ClearSelection();
                    row.Selected = true;
                    dataGridView1.CurrentCell = row.Cells["Barcode"];

                    return;
                }
            }

            string area = guna2ComboBox_Area.Text;
            string location = gunaNumeric_location.Value.ToString();
            string store = guna2ComboBox_location_to_count.Text;

            dataGridView1.Rows.Add(
                barcode,
                1,
                area,
                location,
                store
            );
        }

        private void button_Del_All_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

            var r = MessageBox.Show("Delete all items?", "Confirm", MessageBoxButtons.YesNo);

            if (r == DialogResult.Yes)
                dataGridView1.Rows.Clear();

            textBox_barcode.Focus();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("❌ لا يوجد أصناف للحفظ");
                textBox_barcode.Focus();
                return;
            }

            int manualTotal = 0;

            if (!int.TryParse(textBox_manully.Text, out manualTotal))
            {
                MessageBox.Show("❌ أدخل العدد اليدوي");
                textBox_manully.Focus();
                return;
            }

            int scannedTotal = GetScannedTotal();

            if (manualTotal != scannedTotal)
            {
                MessageBox.Show("❌ العدد غير مطابق");
                textBox_barcode.Focus();
                return;
            }

            SaveLocalExcel();

            textBox_barcode.Focus();
        }

        int GetScannedTotal()
        {
            int total = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int qty = 0;
                int.TryParse(row.Cells["Qty"].Value?.ToString(), out qty);
                total += qty;
            }

            return total;
        }

        void SaveLocalExcel()
        {
            try
            {
                guna2ProgressBar1.Value = 10;
                Application.DoEvents();

                string storeName = guna2ComboBox_location_to_count.Text;
                string storeCode = Class.WhsManager.WhsMap[storeName];

                string area = guna2ComboBox_Area.Text;
                string location = gunaNumeric_location.Value.ToString();

                string folder = Path.Combine(
                    Application.StartupPath,
                    "Pending",
                    "AreaLocationCount"
                );

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName =
                    storeCode.Replace("-", "") + "___" +
                    area + "___" +
                    "LOC" + location + "___" +
                    DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") +
                    ".xlsx";

                string filePath = Path.Combine(folder, fileName);

                using (var wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("COUNT");

                    ws.Cell(1, 1).Value = "Barcode";
                    ws.Cell(1, 2).Value = "Qty";
                    ws.Cell(1, 3).Value = "Area";
                    ws.Cell(1, 4).Value = "Location";
                    ws.Cell(1, 5).Value = "Store";
                    ws.Cell(1, 6).Value = "StoreCode";
                    ws.Cell(1, 7).Value = "CountedBy";
                    ws.Cell(1, 8).Value = "CreatedAt";

                    int row = 2;

                    foreach (DataGridViewRow dgRow in dataGridView1.Rows)
                    {
                        ws.Cell(row, 1).Value = dgRow.Cells["Barcode"].Value?.ToString();
                        ws.Cell(row, 2).Value = dgRow.Cells["Qty"].Value?.ToString();
                        ws.Cell(row, 3).Value = area;
                        ws.Cell(row, 4).Value = location;
                        ws.Cell(row, 5).Value = storeName;
                        ws.Cell(row, 6).Value = storeCode;
                        ws.Cell(row, 7).Value = Class.Session.Username;
                        ws.Cell(row, 8).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        row++;
                    }

                    var info = wb.Worksheets.Add("INFO");

                    info.Cell(1, 1).Value = "Type";
                    info.Cell(1, 2).Value = "AreaLocationCount";

                    info.Cell(2, 1).Value = "Store";
                    info.Cell(2, 2).Value = storeName;

                    info.Cell(3, 1).Value = "StoreCode";
                    info.Cell(3, 2).Value = storeCode;

                    info.Cell(4, 1).Value = "Area";
                    info.Cell(4, 2).Value = area;

                    info.Cell(5, 1).Value = "Location";
                    info.Cell(5, 2).Value = location;

                    info.Cell(6, 1).Value = "CountedBy";
                    info.Cell(6, 2).Value = Class.Session.Username;

                    info.Cell(7, 1).Value = "CreatedAt";
                    info.Cell(7, 2).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    ws.Columns().AdjustToContents();
                    info.Columns().AdjustToContents();

                    wb.SaveAs(filePath);
                }

                guna2ProgressBar1.Value = 100;
                Application.DoEvents();

                MessageBox.Show("✅ تم حفظ ملف الجرد بنجاح");

                dataGridView1.Rows.Clear();
                textBox_manully.Clear();
            }
            catch (Exception ex)
            {
                guna2ProgressBar1.Value = 0;
                MessageBox.Show("❌ خطأ أثناء الحفظ:\n" + ex.Message);
            }
        }

        private async void button_upload_to_server_Click(object sender, EventArgs e)
        {
            string folder = Path.Combine(
                Application.StartupPath,
                "Pending",
                "AreaLocationCount"
            );

            if (!Directory.Exists(folder))
            {
                MessageBox.Show("لا يوجد مجلد AreaLocationCount");
                return;
            }

            string[] files = Directory.GetFiles(folder, "*.xlsx");

            if (files.Length == 0)
            {
                MessageBox.Show("لا يوجد ملفات للرفع");
                return;
            }

            foreach (string file in files)
            {
                await UploadFileToServer(file);
            }

            MessageBox.Show("✅ تم رفع كل ملفات الجرد للسيرفر");
        }

        //async Task UploadFileToServer(string filePath)
        //{
        //    var client = new RestClient("http://102.209.3.101:9500");

        //    var request = new RestRequest(
        //        "/WMSBKR/public/api/AreaLocationCount/upload",
        //        Method.POST
        //    );

        //    request.AlwaysMultipartFormData = true;
        //    request.AddFile("file", filePath);
        //    request.AddParameter("created_by", Class.Session.Username);

        //    IRestResponse response = await client.ExecuteTaskAsync(request);

        //    if (response.IsSuccessful)
        //    {
        //        string archive = Path.Combine(
        //            Application.StartupPath,
        //            "Excel",
        //            "AreaLocationCount"
        //        );

        //        if (!Directory.Exists(archive))
        //            Directory.CreateDirectory(archive);

        //        string newPath = Path.Combine(archive, Path.GetFileName(filePath));

        //        if (File.Exists(newPath))
        //            File.Delete(newPath);

        //        File.Move(filePath, newPath);
        //    }
        //    else
        //    {
        //        MessageBox.Show("❌ فشل رفع الملف:\n" + Path.GetFileName(filePath));
        //    }
        //}

        async Task UploadFileToServer(string filePath)
        {
            string fileName = Path.GetFileName(filePath);

            string storeCode = fileName.Split(new string[] { "___" }, StringSplitOptions.None)[0];

            string branch = "";
            string storeType = "";

            foreach (var item in Class.WhsManager.WhsMap)
            {
                string key = item.Key;      // M02 WH
                string value = item.Value;  // TR-WH-01

                if (value.Replace("-", "") == storeCode)
                {
                    branch = key.Split(' ')[0];     // M02
                    storeType = key.Split(' ')[1];  // WH أو BR
                    break;
                }
            }

            var client = new RestClient("http://102.209.3.101:9500");

            var request = new RestRequest(
                "/WMSBKR/public/api/AreaLocationCount/upload",
                Method.POST
            );

            request.AlwaysMultipartFormData = true;
            request.AddFile("file", filePath);
            request.AddParameter("created_by", Class.Session.Username);
            request.AddParameter("branch", branch);
            request.AddParameter("store_type", storeType);
            request.AddParameter("store_code", storeCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            if (response.IsSuccessful)
            {
                string archive = Path.Combine(
                    Application.StartupPath,
                    "Excel",
                    "AreaLocationCount",
                    branch,
                    storeType
                );

                if (!Directory.Exists(archive))
                    Directory.CreateDirectory(archive);

                string newPath = Path.Combine(archive, Path.GetFileName(filePath));

                if (File.Exists(newPath))
                    File.Delete(newPath);

                File.Move(filePath, newPath);
            }
            else
            {
                MessageBox.Show("❌ فشل رفع الملف:\n" + Path.GetFileName(filePath));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectFrm frm = new SelectFrm();
            frm.Show();
            this.Hide();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            string barcode = dataGridView1.CurrentRow.Cells["Barcode"].Value?.ToString();
            string qty = dataGridView1.CurrentRow.Cells["Qty"].Value?.ToString();

            DialogResult result = MessageBox.Show(
                $"Barcode : {barcode}\nQty : {qty}\n\nهل تريد حذف هذا الصنف؟",
                "تأكيد الحذف",
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