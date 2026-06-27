using ClosedXML.Excel;
using ExcelDataReader;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transfer_app.Cls;

namespace Transfer_app.Pages.Internal_Transfer
{
    public partial class Step_1_Select_Direction : Form
    {
        Cls.component component = new Cls.component();
        Cls.db conn = new Cls.db();
        
        public Step_1_Select_Direction()
        {
            InitializeComponent();
            component.SetRoundedForm(this, 20);
        }

        private void Step_1_Select_Direction_Load(object sender, EventArgs e)
        {
            string branch = Class.Session.Branch;


            var branchWhs = Class.WhsManager.WhsMap
                .Where(x => x.Key.StartsWith(branch));

            foreach (var item in branchWhs)
            {
                guna2ComboBox_from.Items.Add(item.Key);
                guna2ComboBox_to.Items.Add(item.Key);
            }

            int num = getfilesNum();
            if (getfilesNum() > 0)
            {
                guna2Button_uploadSrv.Visible = true;
                guna2Button_uploadSrv.Text = $"upload Srv ( { num.ToString() } )";
            }
            else
            {
                guna2Button_uploadSrv.Visible = false;
            }
            //MessageBox.Show(num.ToString());
        }

        private void guna2Button_back_Click(object sender, EventArgs e)
        {
            SelectFrm frm = new SelectFrm();
            frm.Show();
            this.Close();
        }

        private void guna2Button_Start_Scan_Click(object sender, EventArgs e)
        {
            if (!component.LockButton(guna2Button_Start_Scan))
                return;

            try
            {

            if (guna2ComboBox_from.SelectedItem == null || guna2ComboBox_to.SelectedItem == null)
            {
                MessageBox.Show("Please select both 'From' and 'To' locations.");
                return;
            }
            if (guna2ComboBox_from.SelectedItem.ToString() == guna2ComboBox_to.SelectedItem.ToString())
            {
                MessageBox.Show("'From' and 'To' locations cannot be the same.");
                return;
            }

            string from = guna2ComboBox_from.SelectedItem.ToString();
            string to = guna2ComboBox_to.SelectedItem.ToString();

            string fromCode = Class.WhsManager.WhsMap[from];
            string toCode = Class.WhsManager.WhsMap[to];

            Class.TransferSession.FromName = from;
            Class.TransferSession.ToName = to;
            Class.TransferSession.FromCode = fromCode;
            Class.TransferSession.ToCode = toCode;

            //MessageBox.Show($"Selected From: {from} ({fromCode}), To: {to} ({toCode})");

            // I WANT TO FETCH THE DATA FROM SQLSERVER IN EXCEL AND SAVE IN WH FOLDER 

            if (!SyncStockToExcel())
            {
                return;
            }
            
            Pages.Internal_Transfer.Step_2_Scan_Items frm = new Pages.Internal_Transfer.Step_2_Scan_Items();
            frm.Show();
            this.Hide();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                component.UnlockButton(guna2Button_Start_Scan);
            }
        }

        private bool SyncStockToExcel()
        {
            try
            {
                guna2ProgressBar1.Value = 0;
                Application.DoEvents();

                string fromWhs = Class.TransferSession.FromCode;
                string toWhs = Class.TransferSession.ToCode;

                string whFolder = Path.Combine(Application.StartupPath, "WH");

                if (!Directory.Exists(whFolder))
                    Directory.CreateDirectory(whFolder);

                foreach (string file in Directory.GetFiles(whFolder, "*.xlsx"))
                {
                    File.Delete(file);
                }

                string filePath = Path.Combine(
                    whFolder,
                    fromWhs + "___" +
                    toWhs + "___" +
                    DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") +
                    ".xlsx"
                );

                conn.openconn();

                guna2ProgressBar1.Value = 25;
                Application.DoEvents();

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

                SqlCommand cmd = new SqlCommand(sql, conn.getconn);

                cmd.Parameters.AddWithValue("@FromWhs", fromWhs);
                cmd.Parameters.AddWithValue("@ToWhs", toWhs);

                SqlDataReader dr = cmd.ExecuteReader();

                guna2ProgressBar1.Value = 60;
                Application.DoEvents();

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
                    guna2ProgressBar1.Value = 100;
                    Application.DoEvents();
                }

                dr.Close();
                conn.closeconn();

                //MessageBox.Show("✅ Stock file created successfully.");

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error:\n" + ex.Message);
                return false;
            }
        }

        private async void guna2Button_uploadSrv_Click(object sender, EventArgs e)
        {
            if (!component.LockButton(guna2Button_uploadSrv))
                return;

            try
            {
                string pendingFolder = Path.Combine(
                    Application.StartupPath,
                    "Pending",
                    Class.Session.Branch
                );

                if (!Directory.Exists(pendingFolder))
                {
                    MessageBox.Show("No Pending files.");
                    return;
                }

                string[] files = Directory.GetFiles(pendingFolder, "*.xlsx");

                if (files.Length == 0)
                {
                    MessageBox.Show("No files to upload.");
                    return;
                }

                int success = 0;
                int failed = 0;

                guna2ProgressBar1.Value = 0;

                foreach (string filePath in files)
                {
                    try
                    {
                        string type = "";
                        string fromWhs = "";
                        string toWhs = "";
                        string branch = "";

                        using (var wb = new XLWorkbook(filePath))
                        {
                            var info = wb.Worksheet("INFO");

                            type = GetInfoValue(info, "Transfer Type").ToLower();
                            fromWhs = GetInfoValue(info, "From");
                            toWhs = GetInfoValue(info, "To");
                            branch = GetInfoValue(info, "Branch");
                        }

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

                        IRestResponse response = await client.ExecuteTaskAsync(request);

                        if (!response.IsSuccessful)
                        {
                            failed++;
                            continue;
                        }

                        JObject json = JObject.Parse(response.Content);
                        string transferNo = json["transfer_no"]?.ToString();

                        if (string.IsNullOrEmpty(transferNo))
                        {
                            failed++;
                            continue;
                        }

                        MoveToArchive(filePath, transferNo, fromWhs, toWhs);

                        success++;
                    }
                    catch
                    {
                        failed++;
                    }

                    guna2ProgressBar1.Value =
                        (int)(((double)(success + failed) / files.Length) * 100);

                    Application.DoEvents();
                }

                MessageBox.Show(
                    "Upload Finished\n\n" +
                    "Success: " + success + "\n" +
                    "Failed: " + failed
                );

                SelectFrm frm = new SelectFrm();
                frm.Show();
                this.Close();
            }
            finally
            {
                component.UnlockButton(guna2Button_uploadSrv);
            }
        }
        string GetInfoValue(IXLWorksheet ws, string key)
        {
            foreach (var row in ws.RowsUsed())
            {
                string cellKey = row.Cell(1).GetString();

                if (cellKey.Trim().Equals(key, StringComparison.OrdinalIgnoreCase))
                    return row.Cell(2).GetString();
            }

            return "";
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

            string newFileName =
                transferNo + "___" +
                fromWhs + "___" +
                toWhs + "___" +
                DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") +
                ".xlsx";

            string newPath = Path.Combine(archiveFolder, newFileName);

            if (File.Exists(newPath))
                File.Delete(newPath);

            File.Move(oldFilePath, newPath);
        }

        public int getfilesNum()
        {
            string pendingFolder = Path.Combine(
                Application.StartupPath,
                "Pending",
                Class.Session.Branch
            );

            if (!Directory.Exists(pendingFolder))
                return 0;

            return Directory.GetFiles(pendingFolder, "*.xlsx").Length;
        }
    }
}
