using ClosedXML.Excel;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing;
using Zen.Barcode;

namespace Transfer_app.Pages.Transfer_Request
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

        private async void button_save_upload_srv_print_a4_Click(object sender, EventArgs e)
        {
            if (!component.LockButton(button_save_upload_srv_print_a4))
                return;

            
            string filePath = "";

            try
            {
                int totalQty = GetTotalQty();

                guna2ProgressBar1.Value = 0;
                label_status.Text = "Preparing...";
                Application.DoEvents();

                string pendingFolder = Path.Combine(
                    Application.StartupPath,
                    "Pending",
                    "TransferRequest"
                );

                if (!Directory.Exists(pendingFolder))
                    Directory.CreateDirectory(pendingFolder);

                filePath = Path.Combine(pendingFolder, fileName);

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

                    guna2ProgressBar1.Value = 60;
                    label_status.Text = "Writing information...";
                    Application.DoEvents();

                    var info = wb.Worksheets.Add("INFO");

                    info.Cell(1, 1).Value = "Transfer Type";
                    info.Cell(1, 2).Value = "transfer_request";

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

                    guna2ProgressBar1.Value = 75;
                    label_status.Text = "Saving...";
                    Application.DoEvents();

                    wb.SaveAs(filePath);
                }

                guna2ProgressBar1.Value = 85;
                label_status.Text = "Uploading to server...";
                Application.DoEvents();

                var client = new RestClient("http://102.209.3.101:9500");

                var request = new RestRequest("/WMSBKR/public/api/Transfer/upload", Method.POST);
                request.AlwaysMultipartFormData = true;

                request.AddFile("file", filePath);
                request.AddParameter("transfer_type", "transfer_request");
                request.AddParameter("from_whs", Class.TransferSession.FromCode);
                request.AddParameter("to_whs", Class.TransferSession.ToCode);
                request.AddParameter("branch", Class.Session.Branch);
                request.AddParameter("created_by_username", Class.Session.Username);
                request.AddParameter("created_by_name", Class.Session.Name);

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (!response.IsSuccessful)
                {
                    label_status.Text = "Saved Pending - Upload Failed";
                    guna2ProgressBar1.Value = 100;

                    MessageBox.Show(
                        "⚠️ تم حفظ الملف في Pending لكن فشل الرفع للسيرفر:\n\n" +
                        response.Content
                    );
                    Console.WriteLine(response.Content);

                    return;
                }

                JObject json = JObject.Parse(response.Content);

                string requestNo = json["request_no"]?.ToString();

                if (string.IsNullOrEmpty(requestNo))
                {
                    throw new Exception("Server did not return request_no");
                }

                Class.TransferSession.RequestNo = requestNo;
                Class.TransferSession.BoxNo = requestNo;

                guna2ProgressBar1.Value = 100;
                label_status.Text = "Uploaded Successfully";
                Application.DoEvents();

                MessageBox.Show(
                    "✅ تم حفظ ورفع طلب التحويل بنجاح\n\n" +
                    "Request / Box No: " + requestNo
                );

                // هنا بعدين نفتح شاشة الطباعة A4
                MoveTransferRequestToArchive(filePath, requestNo);
                PrintTransferRequestA4(requestNo, totalQty);
                SelectFrm frm = new SelectFrm();
                frm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                guna2ProgressBar1.Value = 0;
                label_status.Text = "Failed";

                MessageBox.Show("❌ خطأ:\n" + ex.Message);
            }
            finally
            {
                component.UnlockButton(button_save_upload_srv_print_a4);
            }
        }
        void MoveTransferRequestToArchive(string oldFilePath, string requestNo)
        {
            string archiveFolder = Path.Combine(
                Application.StartupPath,
                "Excel",
                Class.Session.Branch,
                "TR"
            );

            if (!Directory.Exists(archiveFolder))
                Directory.CreateDirectory(archiveFolder);

            string newFileName =
                requestNo + "___" +
                Class.TransferSession.FromCode + "___" +
                Class.TransferSession.ToCode + "___" +
                DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") +
                ".xlsx";

            string newPath = Path.Combine(archiveFolder, newFileName);

            File.Copy(oldFilePath, newPath, true);
            File.Delete(oldFilePath);
        }


        private void PrintTransferRequestA4(string requestNo, int totalQty)
        {
            PrintDocument doc = new PrintDocument();

            doc.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
            doc.DefaultPageSettings.Landscape = false;

            doc.PrintPage += (s, e) =>
            {
                Graphics g = e.Graphics;

                int pageWidth = e.PageBounds.Width;
                int y = 50;

                StringFormat center = new StringFormat();
                center.Alignment = StringAlignment.Center;

                Font titleFont = new Font("Arial", 32, FontStyle.Bold);
                Font dateFont = new Font("Arial", 14, FontStyle.Regular);
                Font infoFont = new Font("Arial", 20, FontStyle.Bold);
                Font numberFont = new Font("Arial", 30, FontStyle.Bold);

                g.DrawString(
                    "TRANSFER REQUEST",
                    titleFont,
                    Brushes.Black,
                    new RectangleF(0, y, pageWidth, 50),
                    center);

                y += 60;

                g.DrawString(
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                    dateFont,
                    Brushes.Black,
                    new RectangleF(0, y, pageWidth, 30),
                    center);

                y += 70;

                g.DrawLine(Pens.Black, 60, y, pageWidth - 60, y);

                y += 40;

                g.DrawString(
                    "FROM : " + Class.TransferSession.FromName +
                    " (" + Class.TransferSession.FromCode + ")",
                    infoFont,
                    Brushes.Black,
                    80,
                    y);

                y += 55;

                g.DrawString(
                    "TO   : " + Class.TransferSession.ToName +
                    " (" + Class.TransferSession.ToCode + ")",
                    infoFont,
                    Brushes.Black,
                    80,
                    y);

                y += 55;

                g.DrawString(
                    "TOTAL QTY : " + totalQty + " PCS",
                    infoFont,
                    Brushes.Black,
                    80,
                    y);

                y += 70;

                g.DrawLine(Pens.Black, 60, y, pageWidth - 60, y);

                y += 45;

                g.DrawString(
                    requestNo,
                    numberFont,
                    Brushes.Black,
                    new RectangleF(0, y, pageWidth, 50),
                    center);

                y += 80;

                BarcodeDraw barcode = BarcodeDrawFactory.Code128WithChecksum;

                Image barcodeImage = barcode.Draw(requestNo, 130);

                int barcodeX = (pageWidth - barcodeImage.Width) / 2;

                g.DrawImage(
                    barcodeImage,
                    barcodeX,
                    y,
                    barcodeImage.Width,
                    barcodeImage.Height);

                y += barcodeImage.Height + 30;

                g.DrawString(
                    requestNo,
                    numberFont,
                    Brushes.Black,
                    new RectangleF(0, y, pageWidth, 50),
                    center);

                y += 80;

                g.DrawLine(Pens.Black, 60, y, pageWidth - 60, y);
            };

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = doc;
            printDialog.UseEXDialog = true;
            printDialog.AllowSomePages = false;
            printDialog.AllowSelection = false;

            DialogResult result = printDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    doc.PrinterSettings = printDialog.PrinterSettings;
                    doc.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "⚠️ فشلت الطباعة، سيتم فتح المعاينة.\n\n" +
                        ex.Message
                    );

                    PrintPreviewDialog preview = new PrintPreviewDialog();
                    preview.Document = doc;
                    preview.Width = 1200;
                    preview.Height = 900;
                    preview.ShowDialog();
                }
            }
            else
            {
                PrintPreviewDialog preview = new PrintPreviewDialog();
                preview.Document = doc;
                preview.Width = 1200;
                preview.Height = 900;
                preview.ShowDialog();
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