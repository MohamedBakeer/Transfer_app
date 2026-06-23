using ClosedXML.Excel;
using RestSharp;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Transfer_app.Pages.Pending_Upload
{
    public partial class PendingUploadFrm : Form
    {
        Cls.component component = new Cls.component();

        public PendingUploadFrm()
        {
            InitializeComponent();
            component.SetRoundedForm(this, 20);
        }

        private void PendingUploadFrm_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadPendingFiles();
        }

        void SetupGrid()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("Type", "Type");
            dataGridView1.Columns.Add("Branch", "Branch");
            dataGridView1.Columns.Add("From", "From");
            dataGridView1.Columns.Add("To", "To");
            dataGridView1.Columns.Add("Qty", "Qty");
            dataGridView1.Columns.Add("Date", "Date");

            dataGridView1.Columns.Add("FileName", "File Name");
            dataGridView1.Columns.Add("FilePath", "File Path");

            dataGridView1.Columns["FileName"].Visible = false;
            dataGridView1.Columns["FilePath"].Visible = false;

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        void LoadPendingFiles()
        {
            dataGridView1.Rows.Clear();
            string pendingFolder = Path.Combine(
    Application.StartupPath,
    "Pending",
    Class.Session.Branch
);

            if (!Directory.Exists(pendingFolder))
                Directory.CreateDirectory(pendingFolder);

            string[] files = Directory.GetFiles(pendingFolder, "*.xlsx");

            foreach (string file in files)
            {
                string type = "";
                string from = "";
                string to = "";
                string branch = "";
                string createdAt = "";
                string totalQty = "";

                try
                {
                    using (var wb = new XLWorkbook(file))
                    {
                        var info = wb.Worksheet("INFO");

                        type = GetInfoValue(info, "Transfer Type");
                        from = GetInfoValue(info, "From");
                        to = GetInfoValue(info, "To");
                        branch = GetInfoValue(info, "Branch");
                        createdAt = GetInfoValue(info, "Created At");
                        totalQty = GetInfoValue(info, "Total Qty");


                        
                    }

                    if (Class.Session.Role == "warehouse_user")
                    {
                        if (branch != Class.Session.Branch)
                            continue;
                    }

                    dataGridView1.Rows.Add(
                        type,
                        branch,
                        from,
                        to,
                        totalQty,
                        createdAt,
                        Path.GetFileName(file),
                        file
                    );
                }
                catch
                {
                    dataGridView1.Rows.Add(
                        "Unknown",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        Path.GetFileName(file),
                        file
                    );
                }
            }

            label_summary.Text = "Pending Files: " + files.Length;
            label_status.Text = "Ready";
            guna2ProgressBar1.Value = 0;
        }

        string GetInfoValue(IXLWorksheet ws, string key)
        {
            foreach (var row in ws.RowsUsed())
            {
                string cellKey = row.Cell(1).GetString();

                if (cellKey.Trim().Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    return row.Cell(2).GetString();
                }
            }

            return "";
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            LoadPendingFiles();
        }


        private void button_back_Click(object sender, EventArgs e)
        {
            SelectFrm frm = new SelectFrm();
            frm.Show();
            this.Hide();
        }

        bool IsUploading = false;
        private async void button_upload_Click(object sender, EventArgs e)
        {
            
            //if()

            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a file.");
                return;
            }

            string filePath = dataGridView1.CurrentRow.Cells["FilePath"].Value.ToString();

            if (!File.Exists(filePath))
            {
                MessageBox.Show("❌ File not found:\n" + filePath);
                return;
            }

            string type = dataGridView1.CurrentRow.Cells["Type"].Value.ToString().ToLower();
            string branch = dataGridView1.CurrentRow.Cells["Branch"].Value.ToString();
            string fromWhs = dataGridView1.CurrentRow.Cells["From"].Value.ToString();
            string toWhs = dataGridView1.CurrentRow.Cells["To"].Value.ToString();

            if (type != "internal" && type != "external")
            {
                MessageBox.Show("❌ Invalid transfer type.");
                return;
            }

            try
            {
                guna2ProgressBar1.Value = 10;
                label_status.Text = "Uploading...";
                Application.DoEvents();

                var client = new RestClient("http://102.209.3.101:9500");

                var request = new RestRequest("/WMSBKR/public/api/Transfer/upload", Method.POST);
                request.AlwaysMultipartFormData = true;

                request.AddFile("file", filePath);

                request.AddParameter("transfer_type", type);
                request.AddParameter("from_whs", fromWhs);
                request.AddParameter("to_whs", toWhs);
                request.AddParameter("branch", branch);
                request.AddParameter("created_by_username", Class.Session.Username);
                request.AddParameter("created_by_name", Class.Session.Name);

                guna2ProgressBar1.Value = 50;
                Application.DoEvents();

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (!response.IsSuccessful)
                {
                    guna2ProgressBar1.Value = 0;
                    label_status.Text = "Upload failed";
                    MessageBox.Show("❌ Upload failed:\n" + response.Content);
                    Console.WriteLine(response.Content);
                    return;
                }

                Newtonsoft.Json.Linq.JObject json =
                    Newtonsoft.Json.Linq.JObject.Parse(response.Content);

                string transferNo = json["transfer_no"]?.ToString();

                if (string.IsNullOrEmpty(transferNo))
                {
                    guna2ProgressBar1.Value = 0;
                    label_status.Text = "Invalid server response";
                    MessageBox.Show("❌ Server did not return transfer number.");
                    return;
                }

                guna2ProgressBar1.Value = 80;
                label_status.Text = "Archiving...";
                Application.DoEvents();

                MoveToArchive(filePath, transferNo, fromWhs, toWhs);

                guna2ProgressBar1.Value = 100;
                label_status.Text = "Uploaded successfully";

                MessageBox.Show("✅ Uploaded Successfully\nTransfer No: " + transferNo);

                LoadPendingFiles();
            }
            catch (Exception ex)
            {
                guna2ProgressBar1.Value = 0;
                label_status.Text = "Error";
                MessageBox.Show("❌ Error:\n" + ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
        void MoveToArchive(string oldFilePath, string transferNo, string fromWhs, string toWhs)
        {
            string branch = Class.Session.Branch;

            string typeFolder = transferNo.Substring(2, 2) == "11" ? "IT" : "ET";

            string archiveFolder = Path.Combine(
                Application.StartupPath,
                "Excel",
                branch,
                typeFolder
            );

            if (!Directory.Exists(archiveFolder))
                Directory.CreateDirectory(archiveFolder);

            string oldName = Path.GetFileNameWithoutExtension(oldFilePath);

            string datePart = "";

            string[] parts = oldName.Split(new string[] { "___" }, StringSplitOptions.None);

            if (parts.Length >= 3)
                datePart = parts[2];

            string newFileName =
                transferNo + "___" +
                fromWhs + "___" +
                toWhs + "___" +
                datePart +
                ".xlsx";

            string newPath = Path.Combine(archiveFolder, newFileName);

            if (File.Exists(newPath))
                File.Delete(newPath);

            File.Move(oldFilePath, newPath);
        }
    }
}