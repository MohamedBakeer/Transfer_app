using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Transfer_app.Pages.Transfer_Monitor
{
    public partial class TransferMonitorFrm : Form
    {
        Cls.component component = new Cls.component();

        public TransferMonitorFrm()
        {
            InitializeComponent();
            component.SetRoundedForm(this, 20);
        }

        private void TransferMonitorFrm_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadBranches();

            guna2DateTimePicker_from.Value = DateTime.Today.AddDays(-7);
            guna2DateTimePicker1.Value = DateTime.Today;

            gunaRadioButton_type_all.Checked = true;
            gunaRadioButton_status_all.Checked = true;
        }

        void LoadBranches()
        {
            guna2ComboBox_branch.Items.Clear();
            guna2ComboBox_branch.Items.Add("All");

            var branches = Class.WhsManager.WhsMap.Keys
                .Select(x => x.Split(' ')[0])
                .Distinct()
                .ToList();

            foreach (string branch in branches)
            {
                guna2ComboBox_branch.Items.Add(branch);
            }

            guna2ComboBox_branch.SelectedIndex = 0;
        }

        string GetWhsCodesBySelectedBranch()
        {
            string branch = guna2ComboBox_branch.Text;

            if (branch == "All" || branch == "")
                return "all";

            return string.Join(",",
                Class.WhsManager.WhsMap
                    .Where(x => x.Key.StartsWith(branch))
                    .Select(x => x.Value)
            );
        }

        string GetWhsNameByCode(string code)
        {
            var item = Class.WhsManager.WhsMap
                .FirstOrDefault(x => x.Value == code);

            if (string.IsNullOrEmpty(item.Key))
                return code;

            return item.Key;
        }

        void SetupGrid()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("TransferNo", "Transfer No");
            dataGridView1.Columns.Add("Type", "Type");
            dataGridView1.Columns.Add("FromName", "From");
            dataGridView1.Columns.Add("ToName", "To");
            dataGridView1.Columns.Add("Status", "Status");
            dataGridView1.Columns.Add("CreatedBy", "Created By");
            dataGridView1.Columns.Add("Date", "Date");

            dataGridView1.DefaultCellStyle.Font = new Font("Cairo", 12);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font =
                new Font("Cairo", 11, FontStyle.Bold);

            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        string GetSelectedType()
        {
            if (gunaRadioButton_type_internal.Checked)
                return "internal";

            if (gunaRadioButton_type_transfer_request.Checked)
                return "transfer_request";

            return "all";
        }

        string GetSelectedStatus()
        {
            if (gunaRadioButton_status_uploaded.Checked)
                return "uploaded";

            if (gunaRadioButton_status_received_match.Checked)
                return "received_match";

            if (gunaRadioButton_status_received_not_match.Checked)
                return "received_not_match";

            if (gunaRadioButton_status_posted.Checked)
                return "uploaded";
                //return "posted";

            if (gunaRadioButton_status_failed.Checked)
                return "failed";

            return "all";
        }

        private async void guna2Button_load_Click(object sender, EventArgs e)
        {
            try
            {
                if (!component.LockButton(guna2Button_load))
                    return;

                dataGridView1.Rows.Clear();

                string fromDate = guna2DateTimePicker_from.Value.ToString("yyyy-MM-dd");
                string toDate = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd");

                var client = new RestClient("http://102.209.3.101:9500");

                var request = new RestRequest(
                    "/WMSBKR/public/api/Transfer/monitor",
                    Method.GET
                );

                request.AddParameter("from_date", fromDate);
                request.AddParameter("to_date", toDate);
                request.AddParameter("type", GetSelectedType());
                request.AddParameter("status", GetSelectedStatus());
                request.AddParameter("whs_codes", GetWhsCodesBySelectedBranch());

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("فشل جلب البيانات:\n" + response.Content);
                    return;
                }

                JObject json = JObject.Parse(response.Content);
                JArray transfers = json["transfers"] as JArray;

                if (transfers == null || transfers.Count == 0)
                {
                    MessageBox.Show("No data matched the filter");
                    return;
                }

                foreach (JObject t in transfers)
                {
                    string createdAt = "";

                    DateTime dt;
                    if (DateTime.TryParse(t["created_at"]?.ToString(), out dt))
                        createdAt = dt.ToString("yyyy-MM-dd HH:mm");

                    string fromCode = t["from_whs"]?.ToString();
                    string toCode = t["to_whs"]?.ToString();

                    dataGridView1.Rows.Add(
                        t["transfer_no"]?.ToString(),
                        t["transfer_type"]?.ToString(),
                        GetWhsNameByCode(fromCode),
                        GetWhsNameByCode(toCode),
                        t["status"]?.ToString(),
                        t["created_by_username"]?.ToString(),
                        createdAt
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ:\n" + ex.Message);
            }
            finally
            {
                component.UnlockButton(guna2Button_load);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            string transferNo =
                dataGridView1.CurrentRow.Cells["TransferNo"].Value?.ToString();

            if (string.IsNullOrEmpty(transferNo))
                return;

            try
            {
                string folder = Path.Combine(
                    Application.StartupPath,
                    "Downloads",
                    "Transfers"
                );

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string savePath = Path.Combine(folder, transferNo + ".xlsx");

                string url =
                    "http://102.209.3.101:9500/WMSBKR/public/api/Transfer/download/" +
                    transferNo;

                using (WebClient web = new WebClient())
                {
                    web.DownloadFile(url, savePath);
                }

                Process.Start(savePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ فشل فتح الملف:\n" + ex.Message);
            }
        }

        private void guna2Button_back_Click(object sender, EventArgs e)
        {
            SelectFrm frm = new SelectFrm();
            frm.Show();
            this.Hide();
        }
    }
}