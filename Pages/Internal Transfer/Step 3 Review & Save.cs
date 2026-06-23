using ClosedXML.Excel;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Transfer_app.Pages.Internal_Transfer
{
    public partial class Step_3_Review___Save : Form
    {
        Cls.component component = new Cls.component();

        string fileName = "";

        public Step_3_Review___Save()
        {
            InitializeComponent();
            component.SetRoundedForm(this, 20);
        }

        int GetTotalQty()
        {
            int totalQty = 0;

            foreach (DataRow row in Class.TransferSession.ItemsTable.Rows)
            {
                int qty = 0;
                int.TryParse(row["QtyScan"].ToString(), out qty);

                totalQty += qty;
            }

            return totalQty;
        }

        private void Step_3_Review___Save_Load(object sender, EventArgs e)
        {
            int totalQty = GetTotalQty();

            fileName =
                Class.TransferSession.FromCode + "___" +
                Class.TransferSession.ToCode + "___" +
                DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") +
                ".xlsx";

            label_from.Text = "From: " + Class.TransferSession.FromCode;
            label_to.Text = "To: " + Class.TransferSession.ToCode;
            label_total.Text = "Total Qty: " + totalQty;
            label_file_name.Text = "File Name: " + fileName;

            guna2ProgressBar1.Value = 0;
            label_status.Text = "Ready";
        }

        private void button_save_pending_Click(object sender, EventArgs e)
        {
            if (!component.LockButton(button_save_pending))
                return;



            try
            {
                int totalQty = GetTotalQty();

                guna2ProgressBar1.Value = 0;
                label_status.Text = "Preparing...";
                Application.DoEvents();

                string pendingFolder = Path.Combine(
    Application.StartupPath,
    "Pending",
    Class.Session.Branch
);

                if (!Directory.Exists(pendingFolder))
                    Directory.CreateDirectory(pendingFolder);

                string filePath = Path.Combine(pendingFolder, fileName);

                guna2ProgressBar1.Value = 30;
                label_status.Text = "Writing transfer file...";
                Application.DoEvents();

                using (var wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Transfer");

                    ws.Cell(1, 1).Value = "Barcode";
                    ws.Cell(1, 2).Value = "From";
                    ws.Cell(1, 3).Value = "Qty";
                    ws.Cell(1, 4).Value = "UoM";
                    ws.Cell(1, 5).Value = "To";
                    ws.Cell(1, 6).Value = "Zone";

                    int row = 2;

                    var wsMissing = wb.Worksheets.Add("MISSING");

                    wsMissing.Cell(1, 1).Value = "Barcode";
                    wsMissing.Cell(1, 2).Value = "Error Type";
                    wsMissing.Cell(1, 3).Value = "Count";
                    wsMissing.Cell(1, 4).Value = "Last Time";

                    int missingRow = 2;

                    foreach (DataRow item in Class.TransferSession.MissingTable.Rows)
                    {
                        wsMissing.Cell(missingRow, 1).Value = item["Barcode"].ToString();
                        wsMissing.Cell(missingRow, 2).Value = item["ErrorType"].ToString();
                        wsMissing.Cell(missingRow, 3).Value = item["Count"].ToString();
                        wsMissing.Cell(missingRow, 4).Value = item["LastTime"].ToString();

                        missingRow++;
                    }

                    wsMissing.Columns().AdjustToContents();
                    foreach (DataRow item in Class.TransferSession.ItemsTable.Rows)
                    {
                        ws.Cell(row, 1).Value = item["Barcode"].ToString();
                        ws.Cell(row, 2).Value = Class.TransferSession.FromCode;
                        ws.Cell(row, 3).Value = item["QtyScan"].ToString();
                        ws.Cell(row, 4).Value = "Pcs";
                        ws.Cell(row, 5).Value = Class.TransferSession.ToCode;
                        ws.Cell(row, 6).Value = item["Zone"].ToString();

                        row++;
                    }

                    ws.Columns().AdjustToContents();

                    guna2ProgressBar1.Value = 70;
                    label_status.Text = "Writing information...";
                    Application.DoEvents();

                    var info = wb.Worksheets.Add("INFO");

                    info.Cell(1, 1).Value = "Transfer Type";
                    info.Cell(1, 2).Value = "Internal";

                    info.Cell(2, 1).Value = "From";
                    info.Cell(2, 2).Value = Class.TransferSession.FromCode;

                    info.Cell(3, 1).Value = "To";
                    info.Cell(3, 2).Value = Class.TransferSession.ToCode;

                    info.Cell(4, 1).Value = "Created By";
                    info.Cell(4, 2).Value = Class.Session.Username;

                    info.Cell(5, 1).Value = "Branch";
                    info.Cell(5, 2).Value = Class.Session.Branch;

                    info.Cell(6, 1).Value = "Created At";
                    info.Cell(6, 2).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    info.Cell(7, 1).Value = "Total Qty";
                    info.Cell(7, 2).Value = totalQty;

                    info.Columns().AdjustToContents();

                    guna2ProgressBar1.Value = 90;
                    label_status.Text = "Saving...";
                    Application.DoEvents();

                    wb.SaveAs(filePath);
                }

                guna2ProgressBar1.Value = 100;
                label_status.Text = "Saved to Pending";
                Application.DoEvents();

                MessageBox.Show("✅ The file has been successfully saved to Pending");

                SelectFrm frm = new SelectFrm();
                frm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                guna2ProgressBar1.Value = 0;
                label_status.Text = "Failed";
                MessageBox.Show("❌ خطأ أثناء الحفظ:\n" + ex.Message);
            }
            finally
            {
                component.UnlockButton(button_save_pending);
            }
        }
        

        private void button_back_Click(object sender, EventArgs e)
        {
            Step_2_Scan_Items frm = new Step_2_Scan_Items();
            frm.Show();
            this.Hide();
        }
    }
}