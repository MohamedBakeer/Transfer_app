using ClosedXML.Excel;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transfer_app.Pages.Area_Location_Count
{
    public partial class AreaLocationCountPro : Form
    {
        Cls.component component = new Cls.component();

        public AreaLocationCountPro()
        {
            InitializeComponent();
            component.SetRoundedForm(this, 20);

            textBox_barcode.KeyDown += textBox_barcode_KeyDown;
            guna2ComboBox_location_to_count.SelectedIndexChanged += guna2ComboBox_location_to_count_SelectedIndexChanged;
            guna2ComboBox_Area.SelectedIndexChanged += guna2ComboBox_Area_SelectedIndexChanged;
        }

        private void AreaLocationCount_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadStoresFromClass();

            guna2ProgressBar1.Value = 0;
            textBox_barcode.Focus();
        }

        void SetupGrid()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("Barcode", "Barcode");
            dataGridView1.Columns.Add("Qty", "Qty");
            dataGridView1.Columns.Add("Description", "Description");
            dataGridView1.Columns.Add("Department", "Department");
            dataGridView1.Columns.Add("Area", "Area");
            dataGridView1.Columns.Add("Location", "Location");
            dataGridView1.Columns.Add("Store", "Store");

            dataGridView1.DefaultCellStyle.Font = new Font("Cairo", 13);
            dataGridView1.RowTemplate.Height = 40;

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        void LoadStoresFromClass()
        {
            guna2ComboBox_location_to_count.Items.Clear();

            foreach (var store in Class.StoreManager.StoresMap)
            {
                guna2ComboBox_location_to_count.Items.Add(store.Key);
            }

            if (guna2ComboBox_location_to_count.Items.Count > 0)
                guna2ComboBox_location_to_count.SelectedIndex = 0;
            else
                MessageBox.Show("لا توجد مخازن محملة. أعد تسجيل الدخول.");
        }

        bool IsShowroom()
        {
            string storeName = guna2ComboBox_location_to_count.Text;

            if (string.IsNullOrWhiteSpace(storeName))
                return false;

            return storeName.EndsWith(" BR");
        }

        private async void guna2ComboBox_location_to_count_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox_location_to_count.SelectedItem == null)
                return;

            string storeName = guna2ComboBox_location_to_count.Text;
            string storeCode = Class.StoreManager.GetCode(storeName);

            if (string.IsNullOrWhiteSpace(storeCode))
            {
                MessageBox.Show("Store code not found.");
                return;
            }

            if (IsShowroom())
            {
                guna2ComboBox_Area.Enabled = false;
                guna2ComboBox_location.Enabled = false;

                guna2ComboBox_Area.Items.Clear();
                guna2ComboBox_Area.Items.Add("AUTO BY DEPARTMENT");
                guna2ComboBox_Area.SelectedIndex = 0;

                guna2ComboBox_location.Items.Clear();
                guna2ComboBox_location.Items.Add("AUTO");
                guna2ComboBox_location.SelectedIndex = 0;
            }
            else
            {
                guna2ComboBox_Area.Enabled = true;
                guna2ComboBox_location.Enabled = true;

                await LoadAreasFromServer(storeCode);
            }

            textBox_barcode.Focus();
        }

        async Task LoadAreasFromServer(string storeCode)
        {
            try
            {
                guna2ComboBox_Area.Items.Clear();
                guna2ComboBox_location.Items.Clear();

                var client = new RestClient("http://102.209.3.101:9500");

                var request = new RestRequest(
                    "/WMSBKR/public/api/AreaLocation/areas/" + storeCode,
                    Method.GET
                );

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("فشل جلب Areas:\n" + response.Content);
                    return;
                }

                JObject json = JObject.Parse(response.Content);
                JArray areas = json["areas"] as JArray;

                if (areas == null || areas.Count == 0)
                {
                    MessageBox.Show("لا توجد Areas لهذا المخزن.");
                    return;
                }

                foreach (JObject area in areas)
                {
                    guna2ComboBox_Area.Items.Add(new AreaComboItem
                    {
                        Id = Convert.ToInt32(area["id"]),
                        Area = area["area"]?.ToString(),
                        LocationFrom = Convert.ToInt32(area["location_from"]),
                        LocationTo = Convert.ToInt32(area["location_to"])
                    });
                }

                if (guna2ComboBox_Area.Items.Count > 0)
                    guna2ComboBox_Area.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading areas:\n" + ex.Message);
            }
        }

        private void guna2ComboBox_Area_SelectedIndexChanged(object sender, EventArgs e)
        {
            guna2ComboBox_location.Items.Clear();

            if (IsShowroom())
                return;

            AreaComboItem area = guna2ComboBox_Area.SelectedItem as AreaComboItem;

            if (area == null)
                return;

            for (int i = area.LocationFrom; i <= area.LocationTo; i++)
            {
                guna2ComboBox_location.Items.Add(i.ToString());
            }

            if (guna2ComboBox_location.Items.Count > 0)
                guna2ComboBox_location.SelectedIndex = 0;

            textBox_barcode.Focus();
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

            e.SuppressKeyPress = true;

            string barcode = NormalizeBarcode(textBox_barcode.Text);

            if (barcode == "")
                return;

            if (!Class.ItemMasterManager.IsLoaded)
            {
                MessageBox.Show(
                    "Item Master غير محمل.\nلا يمكن التأكد من الباركودات الآن.\nأعد تسجيل الدخول والسيرفر شغال.",
                    "تنبيه",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                textBox_barcode.Clear();
                textBox_barcode.Focus();
                return;
            }

            if (!Class.ItemMasterManager.Exists(barcode))
            {
                MessageBox.Show(
                    "❌ هذا الباركود غير موجود في Item Master:\n\n" + barcode,
                    "Barcode Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                textBox_barcode.Clear();
                textBox_barcode.Focus();
                return;
            }

            AddScan(barcode);

            textBox_barcode.Clear();
            textBox_barcode.Focus();
        }

        void AddScan(string barcode)
        {
            if (guna2ComboBox_location_to_count.SelectedItem == null)
            {
                MessageBox.Show("اختر المخزن/الصالة.");
                return;
            }

            if (!IsShowroom())
            {
                if (guna2ComboBox_Area.SelectedItem == null)
                {
                    MessageBox.Show("اختر Area.");
                    return;
                }

                if (guna2ComboBox_location.SelectedItem == null)
                {
                    MessageBox.Show("اختر Location.");
                    return;
                }
            }

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

            var item = Class.ItemMasterManager.Get(barcode);

            string storeName = guna2ComboBox_location_to_count.Text;
            string areaText = "";
            string locationText = "";

            if (IsShowroom())
            {
                areaText = item.Department;
                locationText = "AUTO";
            }
            else
            {
                AreaComboItem area = guna2ComboBox_Area.SelectedItem as AreaComboItem;
                areaText = area.Area;
                locationText = guna2ComboBox_location.Text;
            }

            dataGridView1.Rows.Add(
                barcode,
                1,
                item.Description,
                item.Department,
                areaText,
                locationText,
                storeName
            );
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

            string filePath = SaveLocalExcel();

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                dataGridView1.Rows.Clear();
                textBox_manully.Clear();

                MessageBox.Show("✅ تم حفظ ملف الجرد محلياً فقط");
                guna2ProgressBar1.Value = 0;
                Application.DoEvents();
            }

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

        string SaveLocalExcel()
        {
            try
            {
                guna2ProgressBar1.Value = 10;
                Application.DoEvents();

                string storeName = guna2ComboBox_location_to_count.Text;
                string storeCode = Class.StoreManager.GetCode(storeName);

                string[] parts = storeName.Split(' ');

                if (parts.Length < 2)
                {
                    MessageBox.Show("Store name غير صحيح.");
                    return "";
                }

                string branch = parts[0];
                string storeType = parts[1];

                string area = IsShowroom() ? "AUTO" : guna2ComboBox_Area.Text;
                string location = IsShowroom() ? "AUTO" : guna2ComboBox_location.Text;

                string folder = Path.Combine(
                    Application.StartupPath,
                    "Pending",
                    "AreaLocationCountPro",
                    branch,
                    storeType
                );

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName =
                    storeCode.Replace("-", "") + "___" +
                    area.Replace("/", "-") + "___" +
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
                    ws.Cell(1, 9).Value = "Description";
                    ws.Cell(1, 10).Value = "Department";

                    int excelRow = 2;

                    foreach (DataGridViewRow dgRow in dataGridView1.Rows)
                    {
                        ws.Cell(excelRow, 1).Value = dgRow.Cells["Barcode"].Value?.ToString();
                        ws.Cell(excelRow, 2).Value = dgRow.Cells["Qty"].Value?.ToString();
                        ws.Cell(excelRow, 3).Value = dgRow.Cells["Area"].Value?.ToString();
                        ws.Cell(excelRow, 4).Value = dgRow.Cells["Location"].Value?.ToString();
                        ws.Cell(excelRow, 5).Value = dgRow.Cells["Store"].Value?.ToString();
                        ws.Cell(excelRow, 6).Value = storeCode;
                        ws.Cell(excelRow, 7).Value = Class.Session.Username;
                        ws.Cell(excelRow, 8).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        ws.Cell(excelRow, 9).Value = dgRow.Cells["Description"].Value?.ToString();
                        ws.Cell(excelRow, 10).Value = dgRow.Cells["Department"].Value?.ToString();

                        excelRow++;
                    }

                    var info = wb.Worksheets.Add("INFO");

                    info.Cell(1, 1).Value = "Type";
                    info.Cell(1, 2).Value = "AreaLocationCountPro";

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

                return filePath;
            }
            catch (Exception ex)
            {
                guna2ProgressBar1.Value = 0;
                MessageBox.Show("❌ خطأ أثناء الحفظ:\n" + ex.Message);
                return "";
            }
        }

        private async void button_upload_to_server_Click(object sender, EventArgs e)
        {
            await UploadPendingAreaLocationFiles(true);
        }

        async Task UploadPendingAreaLocationFiles(bool showMessage)
        {
            string rootFolder = Path.Combine(
                Application.StartupPath,
                "Pending",
                "AreaLocationCountPro"
            );

            if (!Directory.Exists(rootFolder))
            {
                if (showMessage)
                    MessageBox.Show("لا يوجد مجلد Pending");

                return;
            }

            string[] files = Directory.GetFiles(
                rootFolder,
                "*.xlsx",
                SearchOption.AllDirectories
            );

            if (files.Length == 0)
            {
                if (showMessage)
                    MessageBox.Show("لا يوجد ملفات للرفع");

                return;
            }

            int success = 0;
            int failed = 0;

            foreach (string file in files)
            {
                bool result = await UploadFileToServer(file);

                if (result)
                    success++;
                else
                    failed++;
            }

            if (showMessage)
            {
                MessageBox.Show(
                    "نتيجة الرفع:\n\n" +
                    "تم رفع: " + success + "\n" +
                    "فشل: " + failed + "\n\n" +
                    "أي ملف فشل سيبقى في Pending."
                );
            }
        }

        async Task<bool> UploadFileToServer(string filePath)
        {
            try
            {
                string fileName = Path.GetFileName(filePath);
                string storeCodeRaw = fileName.Split(new string[] { "___" }, StringSplitOptions.None)[0];

                string storeCode = "";

                foreach (var store in Class.StoreManager.StoresMap)
                {
                    if (store.Value.Replace("-", "") == storeCodeRaw)
                    {
                        storeCode = store.Value;
                        break;
                    }
                }

                if (string.IsNullOrWhiteSpace(storeCode))
                    return false;

                string storeType = new DirectoryInfo(Path.GetDirectoryName(filePath)).Name;
                string branch = new DirectoryInfo(Path.GetDirectoryName(Path.GetDirectoryName(filePath))).Name;

                var client = new RestClient("http://102.209.3.101:9500");

                var request = new RestRequest(
                    "/WMSBKR/public/api/AreaLocationCount/upload-pro",
                    Method.POST
                );

                request.AlwaysMultipartFormData = true;
                request.AddFile("file", filePath);
                request.AddParameter("created_by", Class.Session.Username);
                request.AddParameter("branch", branch);
                request.AddParameter("store_type", storeType);
                request.AddParameter("store_code", storeCode);

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (!response.IsSuccessful)
                    return false;

                string archive = Path.Combine(
                    Application.StartupPath,
                    "Excel",
                    "AreaLocationCountPro",
                    branch,
                    storeType
                );

                if (!Directory.Exists(archive))
                    Directory.CreateDirectory(archive);

                string newPath = Path.Combine(archive, Path.GetFileName(filePath));

                if (File.Exists(newPath))
                    File.Delete(newPath);

                File.Move(filePath, newPath);

                return true;
            }
            catch
            {
                return false;
            }
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
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);

            textBox_barcode.Focus();
        }
    }

    public class AreaComboItem
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public int LocationFrom { get; set; }
        public int LocationTo { get; set; }

        public override string ToString()
        {
            return Area;
        }
    }
}