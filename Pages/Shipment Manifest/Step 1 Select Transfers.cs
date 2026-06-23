using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace Transfer_app.Pages.Shipment_Manifest
{
    public partial class Step_1_Select_Transfers : Form
    {
        Cls.component component = new Cls.component();

        JArray transfersList = new JArray();

        public Step_1_Select_Transfers()
        {
            InitializeComponent();
            component.SetRoundedForm(this, 20);
        }

        private void Step_1_Select_Transfers_Load(object sender, EventArgs e)
        {
            LoadBranches();

            guna2DateTimePicker_from.Value = DateTime.Today;
            guna2DateTimePicker1.Value = DateTime.Today;
        }

        void LoadBranches()
        {
            guna2ComboBox_from.Items.Clear();
            guna2ComboBox_to.Items.Clear();

            string myBranch = Class.Session.Branch;

            guna2ComboBox_from.Items.Add(myBranch);
            guna2ComboBox_from.SelectedIndex = 0;
            guna2ComboBox_from.Enabled = false;

            var branches = Class.WhsManager.WhsMap.Keys
                .Select(x => GetBranchFromKey(x))
                .Distinct()
                .ToList();

            foreach (string branch in branches)
            {
                if (branch != myBranch)
                    guna2ComboBox_to.Items.Add(branch);
            }

            if (guna2ComboBox_to.Items.Count > 0)
                guna2ComboBox_to.SelectedIndex = 0;
        }

        string GetBranchFromKey(string key)
        {
            return key.Split(' ')[0];
        }

        string GetWhsCodesByBranch(string branch)
        {
            return string.Join(",",
                Class.WhsManager.WhsMap
                    .Where(x => x.Key.StartsWith(branch))
                    .Select(x => x.Value)
            );
        }

        private async void guna2Button_Print_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox_to.Text == "")
            {
                MessageBox.Show("اختر الفرع المستلم.");
                return;
            }

            try
            {
                if (!component.LockButton(guna2Button_Print))
                    return;

                string fromBranch = Class.Session.Branch;
                string toBranch = guna2ComboBox_to.Text;

                string fromWhsCodes = GetWhsCodesByBranch(fromBranch);
                string toWhsCodes = GetWhsCodesByBranch(toBranch);

                string fromDate = guna2DateTimePicker_from.Value.ToString("yyyy-MM-dd");
                string toDate = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd");

                var client = new RestClient("http://102.209.3.101:9500");

                var request = new RestRequest(
                    "/WMSBKR/public/api/Transfer/manifest",
                    Method.GET
                );

                request.AddParameter("from_whs_codes", fromWhsCodes);
                request.AddParameter("to_whs_codes", toWhsCodes);
                request.AddParameter("from_date", fromDate);
                request.AddParameter("to_date", toDate);

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("فشل جلب طلبات التحويل:\n" + response.Content);
                    return;
                }

                JObject json = JObject.Parse(response.Content);
                transfersList = json["transfers"] as JArray;

                if (transfersList == null || transfersList.Count == 0)
                {
                    MessageBox.Show("لا توجد طلبات تحويل سارية في هذه الفترة.");
                    return;
                }

                PrintManifest(fromBranch, toBranch, fromDate, toDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ:\n" + ex.Message);
            }
            finally
            {
                component.UnlockButton(guna2Button_Print);
            }
        }

        private void PrintManifest(string fromBranch, string toBranch, string fromDate, string toDate)
        {
            PrintDocument doc = new PrintDocument();
            doc.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
            doc.DefaultPageSettings.Landscape = false;

            int printIndex = 0;

            doc.PrintPage += (s, e) =>
            {
                Graphics g = e.Graphics;

                int pageWidth = e.PageBounds.Width;
                int y = 45;

                Font titleFont = new Font("Arial", 24, FontStyle.Bold);
                Font headerFont = new Font("Arial", 13, FontStyle.Bold);
                Font normalFont = new Font("Arial", 11, FontStyle.Regular);
                Font smallFont = new Font("Arial", 9, FontStyle.Regular);

                StringFormat center = new StringFormat();
                center.Alignment = StringAlignment.Center;

                g.DrawString(
                    "SHIPMENT MANIFEST",
                    titleFont,
                    Brushes.Black,
                    new RectangleF(0, y, pageWidth, 40),
                    center
                );

                y += 55;

                g.DrawString("FROM BRANCH : " + fromBranch, headerFont, Brushes.Black, 50, y);
                y += 28;

                g.DrawString("TO BRANCH   : " + toBranch, headerFont, Brushes.Black, 50, y);
                y += 28;

                g.DrawString("DATE RANGE  : " + fromDate + " TO " + toDate, normalFont, Brushes.Black, 50, y);
                y += 35;

                g.DrawLine(Pens.Black, 50, y, pageWidth - 50, y);
                y += 20;

                g.DrawString("No", headerFont, Brushes.Black, 50, y);
                g.DrawString("Request No", headerFont, Brushes.Black, 100, y);
                g.DrawString("From Whs", headerFont, Brushes.Black, 280, y);
                g.DrawString("To Whs", headerFont, Brushes.Black, 430, y);
                g.DrawString("Date", headerFont, Brushes.Black, 580, y);

                y += 22;

                g.DrawLine(Pens.Black, 50, y, pageWidth - 50, y);
                y += 18;

                while (printIndex < transfersList.Count)
                {
                    JObject t = transfersList[printIndex] as JObject;

                    if (t != null)
                    {
                        string transferNo = t["transfer_no"]?.ToString();
                        string fromWhs = t["from_whs"]?.ToString();
                        string toWhs = t["to_whs"]?.ToString();

                        string createdAt = "";
                        DateTime dt;

                        if (DateTime.TryParse(t["created_at"]?.ToString(), out dt))
                            createdAt = dt.ToString("yyyy-MM-dd");

                        g.DrawString((printIndex + 1).ToString(), normalFont, Brushes.Black, 50, y);
                        g.DrawString(transferNo, normalFont, Brushes.Black, 100, y);
                        g.DrawString(fromWhs, normalFont, Brushes.Black, 280, y);
                        g.DrawString(toWhs, normalFont, Brushes.Black, 430, y);
                        g.DrawString(createdAt, normalFont, Brushes.Black, 580, y);

                        y += 24;
                    }

                    printIndex++;

                    if (y > 1040)
                    {
                        e.HasMorePages = true;
                        return;
                    }
                }

                y += 20;
                g.DrawLine(Pens.Black, 50, y, pageWidth - 50, y);
                y += 25;

                g.DrawString("TOTAL REQUESTS : " + transfersList.Count, headerFont, Brushes.Black, 50, y);

                y += 35;

                g.DrawString(
                    "Printed At: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                    smallFont,
                    Brushes.Black,
                    50,
                    y
                );

                e.HasMorePages = false;
            };

            PrintDialog dialog = new PrintDialog();
            dialog.Document = doc;
            dialog.UseEXDialog = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                doc.PrinterSettings = dialog.PrinterSettings;
                doc.Print();
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