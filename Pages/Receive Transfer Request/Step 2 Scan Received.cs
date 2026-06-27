using ClosedXML.Excel;
using ExcelDataReader;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Transfer_app.Pages.Receive_Transfer_Request
{
    public partial class Step_2_Scan_Received : Form
    {
        Cls.component component = new Cls.component();

        Dictionary<string, int> receivedItems = new Dictionary<string, int>();

        public Step_2_Scan_Received()
        {
            InitializeComponent();
            component.SetRoundedForm(this, 20);

            SetupGrid();

            textBox_barcode.KeyDown += textBox_barcode_KeyDown;

            dataGridView1.Click += (s, e) => textBox_barcode.Focus();
            this.Shown += (s, e) => textBox_barcode.Focus();
        }

        private void Step_2_Scan_Received_Load(object sender, EventArgs e)
        {
            SetupGrid();

            label_note.Text =
                "BOX NO: " + Class.TransferSession.RequestNo +
                "     FROM: " + Class.TransferSession.FromCode +
                "     TO: " + Class.TransferSession.ToCode;

            guna2ProgressBar1.Value = 0;

            textBox_barcode.Focus();
        }

        void SetupGrid()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("Barcode", "Barcode");
            dataGridView1.Columns.Add("QtyReceived", "Qty Received");

            dataGridView1.DefaultCellStyle.Font = new Font("Cairo", 18);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font =
                new Font("Cairo", 16, FontStyle.Bold);

            dataGridView1.RowTemplate.Height = 45;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void textBox_barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;

            string barcode = NormalizeBarcode(textBox_barcode.Text);

            if (barcode == "")
            {
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
            if (receivedItems.ContainsKey(barcode))
            {
                receivedItems[barcode]++;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Barcode"].Value.ToString() == barcode)
                    {
                        row.Cells["QtyReceived"].Value = receivedItems[barcode];
                        HighlightRow(row);
                        ScrollToRow(row);
                        return;
                    }
                }
            }
            else
            {
                receivedItems.Add(barcode, 1);

                int index = dataGridView1.Rows.Add(barcode, 1);
                DataGridViewRow row = dataGridView1.Rows[index];

                HighlightRow(row);
                ScrollToRow(row);
            }
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

        void ScrollToRow(DataGridViewRow row)
        {
            dataGridView1.ClearSelection();
            row.Selected = true;
            dataGridView1.CurrentCell = row.Cells[0];
            dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
        }

        string NormalizeBarcode(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";

            StringBuilder sb = new StringBuilder();

            foreach (char c in input)
            {
                if (char.IsDigit(c))
                    sb.Append(c);
            }

            return sb.ToString();
        }

        private void button_Del_All_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

            DialogResult r = MessageBox.Show(
                "هل تريد حذف كل الأصناف المستلمة؟",
                "تأكيد",
                MessageBoxButtons.YesNo
            );

            if (r == DialogResult.Yes)
            {
                receivedItems.Clear();
                dataGridView1.Rows.Clear();
            }

            textBox_barcode.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Step_1_Receive frm = new Step_1_Receive();
            frm.Show();
            this.Hide();
        }

        private void button_compare_Click(object sender, EventArgs e)
        {
            if (receivedItems.Count == 0)
            {
                MessageBox.Show("❌ لا توجد أصناف مستلمة");
                textBox_barcode.Focus();
                return;
            }

            CompareAndSaveReport();
        }

        async void CompareAndSaveReport()
        {
            try
            {
                guna2ProgressBar1.Value = 10;
                Application.DoEvents();

                Dictionary<string, int> requestedItems = LoadRequestedItems();

                guna2ProgressBar1.Value = 40;
                Application.DoEvents();

                string requestNo = Class.TransferSession.RequestNo;

                int requestedTotal = 0;
                int receivedTotal = 0;
                int diffTotal = 0;
                bool isMatch = true;

                HashSet<string> allBarcodes = new HashSet<string>();

                foreach (var b in requestedItems.Keys)
                    allBarcodes.Add(b);

                foreach (var b in receivedItems.Keys)
                    allBarcodes.Add(b);

                DataTable reportTable = new DataTable();
                reportTable.Columns.Add("Barcode");
                reportTable.Columns.Add("RequestedQty", typeof(int));
                reportTable.Columns.Add("ReceivedQty", typeof(int));
                reportTable.Columns.Add("Difference", typeof(int));
                reportTable.Columns.Add("Status");

                foreach (string barcode in allBarcodes)
                {
                    int requestedQty = requestedItems.ContainsKey(barcode) ? requestedItems[barcode] : 0;
                    int receivedQty = receivedItems.ContainsKey(barcode) ? receivedItems[barcode] : 0;
                    int diff = receivedQty - requestedQty;

                    string status = "OK";

                    if (requestedQty == 0 && receivedQty > 0)
                    {
                        status = "EXTRA";
                        isMatch = false;
                    }
                    else if (diff < 0)
                    {
                        status = "SHORT";
                        isMatch = false;
                    }
                    else if (diff > 0)
                    {
                        status = "OVER";
                        isMatch = false;
                    }

                    requestedTotal += requestedQty;
                    receivedTotal += receivedQty;
                    diffTotal += diff;

                    reportTable.Rows.Add(
                        barcode,
                        requestedQty,
                        receivedQty,
                        diff,
                        status
                    );
                }

                guna2ProgressBar1.Value = 70;
                Application.DoEvents();

                if (isMatch)
                {
                    await UpdateTransferStatus(requestNo, "received_match");

                    guna2ProgressBar1.Value = 100;
                    Application.DoEvents();

                    MessageBox.Show(
                        "✅ MATCH\n\n" +
                        "تمت المطابقة بنجاح.\n" +
                        "تم تحديث حالة الطلب إلى received_match.\n\n" +
                        "سيتم استخدام ملف الطلب الأصلي للترحيل إلى SAP."
                    );

                    SelectFrm frm = new SelectFrm();
                    frm.Show();
                    this.Hide();

                    return;
                }

                string folder = Path.Combine(
                    Application.StartupPath,
                    "Receive",
                    "TransferRequest",
                    requestNo
                );

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string receivedPath = Path.Combine(folder, "received.xlsx");
                string reportPath = Path.Combine(folder, "report.xlsx");

                SaveReceivedFile(receivedPath);
                SaveReportFile(
                    reportPath,
                    requestNo,
                    requestedTotal,
                    receivedTotal,
                    diffTotal,
                    reportTable
                );

                await UploadReceiveReport(requestNo, receivedPath, reportPath);

                guna2ProgressBar1.Value = 100;
                Application.DoEvents();

                MessageBox.Show(
                    "❌ NOT MATCH\n\n" +
                    //"تم حفظ التقرير محلياً:\n" + reportPath + "\n\n" +
                    "تم تحديث حالة الطلب إلى received_not_match."
                );
            }
            catch (Exception ex)
            {
                guna2ProgressBar1.Value = 0;
                MessageBox.Show("❌ خطأ أثناء المقارنة:\n" + ex.Message);
            }
        }

        void SaveReceivedFile(string receivedPath)
        {
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("RECEIVED");

                ws.Cell(1, 1).Value = "Barcode";
                ws.Cell(1, 2).Value = "Qty Received";

                int row = 2;

                foreach (var item in receivedItems)
                {
                    ws.Cell(row, 1).Value = item.Key;
                    ws.Cell(row, 2).Value = item.Value;
                    row++;
                }

                ws.Columns().AdjustToContents();

                wb.SaveAs(receivedPath);
            }
        }

        void SaveReportFile(
    string reportPath,
    string requestNo,
    int requestedTotal,
    int receivedTotal,
    int diffTotal,
    DataTable reportTable
)
        {
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("REPORT");

                ws.Cell(1, 1).Value = "Request No";
                ws.Cell(1, 2).Value = requestNo;

                ws.Cell(2, 1).Value = "From";
                ws.Cell(2, 2).Value = Class.TransferSession.FromCode;

                ws.Cell(3, 1).Value = "To";
                ws.Cell(3, 2).Value = Class.TransferSession.ToCode;

                ws.Cell(4, 1).Value = "Created At";
                ws.Cell(4, 2).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                ws.Cell(5, 1).Value = "Result";
                ws.Cell(5, 2).Value = "NOT MATCH";

                ws.Cell(5, 4).Value = "Requested Total";
                ws.Cell(5, 5).Value = requestedTotal;

                ws.Cell(5, 6).Value = "Received Total";
                ws.Cell(5, 7).Value = receivedTotal;

                ws.Cell(5, 8).Value = "Diff Total";
                ws.Cell(5, 9).Value = diffTotal;

                ws.Cell(7, 1).Value = "Barcode";
                ws.Cell(7, 2).Value = "Requested Qty";
                ws.Cell(7, 3).Value = "Received Qty";
                ws.Cell(7, 4).Value = "Difference";
                ws.Cell(7, 5).Value = "Status";

                int row = 8;

                foreach (DataRow r in reportTable.Rows)
                {
                    ws.Cell(row, 1).Value = r["Barcode"].ToString();
                    ws.Cell(row, 2).Value = Convert.ToInt32(r["RequestedQty"]);
                    ws.Cell(row, 3).Value = Convert.ToInt32(r["ReceivedQty"]);
                    ws.Cell(row, 4).Value = Convert.ToInt32(r["Difference"]);
                    ws.Cell(row, 5).Value = r["Status"].ToString();

                    row++;
                }

                ws.Columns().AdjustToContents();

                wb.SaveAs(reportPath);
            }
        }

        async Task UpdateTransferStatus(string requestNo, string status)
        {
            var client = new RestClient("http://102.209.3.101:9500");

            var request = new RestRequest(
                "/WMSBKR/public/api/Transfer/status/" + requestNo,
                Method.PUT
            );

            request.AddParameter("status", status);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception(
                    "Failed to update transfer status:\n" +
                    response.Content
                );
            }
        }
        Dictionary<string, int> LoadRequestedItems()
        {
            Dictionary<string, int> requested = new Dictionary<string, int>();

            string path = Class.TransferSession.ReceiveRequestFilePath;

            if (!File.Exists(path))
                throw new Exception("Request file not found:\n" + path);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                DataTable table = reader.AsDataSet().Tables["Transfer"];

                if (table == null)
                    table = reader.AsDataSet().Tables[0];

                int colBarcode = -1;
                int colQty = -1;

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    string col = table.Rows[0][i].ToString().Trim().ToLower();

                    if (col == "barcode") colBarcode = i;
                    if (col == "qty") colQty = i;
                }

                if (colBarcode == -1 || colQty == -1)
                    throw new Exception("Invalid request file. Barcode or Qty column missing.");

                for (int r = 1; r < table.Rows.Count; r++)
                {
                    string barcode = NormalizeBarcode(table.Rows[r][colBarcode].ToString());

                    int qty = 0;
                    int.TryParse(table.Rows[r][colQty].ToString(), out qty);

                    if (barcode == "")
                        continue;

                    if (requested.ContainsKey(barcode))
                        requested[barcode] += qty;
                    else
                        requested.Add(barcode, qty);
                }
            }

            return requested;
        }

        async Task UploadReceiveReport(string requestNo, string receivedPath, string reportPath)
        {
            var client = new RestClient("http://102.209.3.101:9500");

            var request = new RestRequest(
                "/WMSBKR/public/api/Transfer/receive-report",
                Method.POST
            );

            request.AlwaysMultipartFormData = true;

            request.AddParameter("transfer_no", requestNo);
            request.AddFile("received_file", receivedPath);
            request.AddFile("report_file", reportPath);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception(
                    "Failed to upload receive report:\n" +
                    response.Content
                );
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            string barcode =
                dataGridView1.CurrentRow.Cells["Barcode"].Value?.ToString();

            string qty =
                dataGridView1.CurrentRow.Cells["QtyReceived"].Value?.ToString();

            DialogResult result = MessageBox.Show(
                $"Barcode : {barcode}\nQty : {qty}\n\nهل تريد حذف هذا الصنف؟",
                "تأكيد الحذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                receivedItems.Remove(barcode);

                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }

            textBox_barcode.Focus();
        }
    }
}