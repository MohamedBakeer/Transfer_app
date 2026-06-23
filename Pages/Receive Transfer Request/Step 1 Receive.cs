using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Transfer_app.Pages.Receive_Transfer_Request
{
    public partial class Step_1_Receive : Form
    {
        Cls.component component = new Cls.component();

        public Step_1_Receive()
        {
            InitializeComponent();
            component.SetRoundedForm(this, 20);

            textBox_BOX.Focus();
        }

        private void guna2Button_back_Click(object sender, EventArgs e)
        {
            SelectFrm frm = new SelectFrm();
            frm.Show();
            this.Hide();
        }

        private async void guna2Button_Start_Scan_Click(object sender, EventArgs e)
        {
            string boxNo = textBox_BOX.Text.Trim();

            if (boxNo == "")
            {
                MessageBox.Show("Please scan BOX NO.");
                textBox_BOX.Focus();
                return;
            }

            try
            {
                if (!component.LockButton(guna2Button_Start_Scan))
                    return;

                var client = new RestClient("http://102.209.3.101:9500");

                var request = new RestRequest(
                    "/WMSBKR/public/api/Transfer/TransferRequest/" + boxNo,
                    Method.GET
                );

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("❌ Transfer Request not found.");
                    return;
                }

                JObject json = JObject.Parse(response.Content);

                JObject transfer = json["transfer"] as JObject;

                if (transfer == null)
                {
                    MessageBox.Show("❌ Invalid server response.");
                    return;
                }

                string transferNo = transfer["transfer_no"]?.ToString();
                string transferType = transfer["transfer_type"]?.ToString();
                string fromWhs = transfer["from_whs"]?.ToString();
                string toWhs = transfer["to_whs"]?.ToString();
                string status = transfer["status"]?.ToString();

                if (transferType != "transfer_request")
                {
                    MessageBox.Show("❌ This number is not Transfer Request.");
                    return;
                }

                if (status == "received_match")
                {
                    MessageBox.Show(
                        "❌ لا يمكن استلام هذه الشحنة.\n\n" +
                        "تم استلام هذه الشحنة ومطابقتها مسبقاً."
                    );
                    return;
                }

                if (status == "received_not_match")
                {
                    MessageBox.Show(
                        "❌ لا يمكن استلام هذه الشحنة.\n\n" +
                        "تم استلام هذه الشحنة سابقاً ويوجد تقرير فروقات بانتظار مراجعة الإدارة."
                    );
                    return;
                }

                if (status == "posted")
                {
                    MessageBox.Show(
                        "❌ لا يمكن استلام هذه الشحنة.\n\n" +
                        "تم ترحيل هذه الشحنة إلى SAP مسبقاً."
                    );
                    return;
                }

                if (status == "archived")
                {
                    MessageBox.Show(
                        "❌ لا يمكن استلام هذه الشحنة.\n\n" +
                        "تم إغلاق هذه الشحنة نهائياً."
                    );
                    return;
                }

                if (status != "uploaded")
                {
                    MessageBox.Show(
                        "❌ لا يمكن استلام هذه الشحنة.\n\n" +
                        "الحالة الحالية: " + status
                    );
                    return;
                }

                if (!IsToWhsForMyBranch(toWhs))
                {
                    MessageBox.Show(
                        "❌ هذا الطلب ليس مرسل لهذا الفرع.\n\n" +
                        "Your Branch: " + Class.Session.Branch + "\n" +
                        "Request To: " + toWhs
                    );
                    return;
                }

                string folder = Path.Combine(
                    Application.StartupPath,
                    "Receive",
                    "TransferRequest",
                    transferNo
                );

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string savePath = Path.Combine(folder, "request.xlsx");

                string downloadUrl =
                    "http://102.209.3.101:9500/WMSBKR/public/api/Transfer/TransferRequest/" +
                    transferNo +
                    "/download";

                using (WebClient web = new WebClient())
                {
                    web.DownloadFile(downloadUrl, savePath);
                }

                Class.TransferSession.RequestNo = transferNo;
                Class.TransferSession.BoxNo = transferNo;

                Class.TransferSession.FromCode = fromWhs;
                Class.TransferSession.ToCode = toWhs;

                Class.TransferSession.FromName = GetWhsNameByCode(fromWhs);
                Class.TransferSession.ToName = GetWhsNameByCode(toWhs);

                Class.TransferSession.ReceiveRequestFilePath = savePath;

                Step_2_Scan_Received frm = new Step_2_Scan_Received();
                frm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error:\n" + ex.Message);
            }
            finally
            {
                component.UnlockButton(guna2Button_Start_Scan);
            }
        }

        private bool IsToWhsForMyBranch(string toWhs)
        {
            string branch = Class.Session.Branch;

            return Class.WhsManager.WhsMap.Any(x =>
                x.Key.StartsWith(branch) &&
                x.Value == toWhs
            );
        }

        private string GetWhsNameByCode(string whsCode)
        {
            var item = Class.WhsManager.WhsMap
                .FirstOrDefault(x => x.Value == whsCode);

            return item.Key ?? whsCode;
        }
    }
}