using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transfer_app.Pages.Area_Monitor
{
    public partial class ShelfDetailsFrm : Form
    {
        Cls.component component = new Cls.component();
        int areaLocationId;
        string storeName;
        string areaName;
        int shelfNo;

        public ShelfDetailsFrm(
            int areaLocationId,
            string storeName,
            string areaName,
            int shelfNo
        )
        {
            InitializeComponent();
            component.SetRoundedForm(this , 20);
            this.areaLocationId = areaLocationId;
            this.storeName = storeName;
            this.areaName = areaName;
            this.shelfNo = shelfNo;

            guna2Button_back.Click += guna2Button_back_Click;
        }

        private async void ShelfDetailsFrm_Load(object sender, EventArgs e)
        {
            SetupGrid();

            label_title.Text = storeName + " - AREA " + areaName + " - Shelf " + shelfNo;

            await LoadShelfDetails();
        }

        void SetupGrid()
        {
            dataGridView_Items.ReadOnly = true;
            dataGridView_Items.AllowUserToAddRows = false;
            dataGridView_Items.AllowUserToDeleteRows = false;
            dataGridView_Items.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_Items.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_Items.DefaultCellStyle.Font = new Font("Cairo", 10);
            dataGridView_Items.ColumnHeadersDefaultCellStyle.Font = new Font("Cairo", 10, FontStyle.Bold);
            dataGridView_Items.RowTemplate.Height = 35;
        }

        async Task LoadShelfDetails()
        {
            try
            {
                var client = new RestClient("http://102.209.3.101:9500");

                var request = new RestRequest(
                    "/WMSBKR/public/api/AreaLocation/shelf/" +
                    areaLocationId + "/" + shelfNo,
                    Method.GET
                );

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("فشل جلب تفاصيل الرف:\n" + response.Content);
                    return;
                }

                JObject json = JObject.Parse(response.Content);

                if (json["success"]?.ToString().ToLower() != "true")
                {
                    MessageBox.Show(json["message"]?.ToString());
                    return;
                }

                JObject summary = json["summary"] as JObject;

                label_total_qty.Text =
                    "Total Qty : " + (summary?["total_qty"]?.ToString() ?? "0");

                label_total_items.Text =
                    "Total Items : " + (summary?["total_items"]?.ToString() ?? "0");

                LoadItemsGrid(json["items"] as JArray);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
        }

        void LoadItemsGrid(JArray items)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Barcode");
            dt.Columns.Add("Description");
            dt.Columns.Add("Department");
            dt.Columns.Add("UOM");
            dt.Columns.Add("Qty");

            if (items != null)
            {
                foreach (JObject item in items)
                {
                    dt.Rows.Add(
                        item["barcode"]?.ToString(),
                        item["description"]?.ToString(),
                        item["department"]?.ToString(),
                        item["uom"]?.ToString(),
                        item["qty"]?.ToString()
                    );
                }
            }

            dataGridView_Items.DataSource = dt;
        }

        private void guna2Button_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}