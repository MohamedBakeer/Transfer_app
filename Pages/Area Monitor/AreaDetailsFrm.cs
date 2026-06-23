using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transfer_app.Pages.Area_Monitor
{
    public partial class AreaDetailsFrm : Form
    {
        Cls.component component = new Cls.component();
        int areaLocationId;
        string storeName;
        string areaName;
        int locationFrom;
        int locationTo;

        public AreaDetailsFrm(
            int areaLocationId,
            string storeName,
            string areaName,
            int locationFrom,
            int locationTo
        )
        {
            InitializeComponent();
            component.SetRoundedForm(this, 20);
            this.areaLocationId = areaLocationId;
            this.storeName = storeName;
            this.areaName = areaName;
            this.locationFrom = locationFrom;
            this.locationTo = locationTo;

        }

        private async void AreaDetailsFrm_Load(object sender, EventArgs e)
        {
            if (Class.Session.Role == "admin")
            {
                guna2Button_delete_area.Visible = true;
                guna2Button_delete_area.Enabled = true;
            }
            else
            {
                guna2Button_delete_area.Visible = false;
                guna2Button_delete_area.Enabled = false;
            }

            SetupGrids();

            label_whs_area.Text = storeName + " - AREA " + areaName;
            label_range_zone.Text = locationFrom + " -> " + locationTo;

            await LoadDetails();
        }

        void SetupGrids()
        {
            dataGridView_Departments.ReadOnly = true;
            dataGridView_Departments.AllowUserToAddRows = false;
            dataGridView_Departments.AllowUserToDeleteRows = false;
            dataGridView_Departments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_Departments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_Departments.DefaultCellStyle.Font = new Font("Cairo", 11);
            dataGridView_Departments.ColumnHeadersDefaultCellStyle.Font = new Font("Cairo", 11, FontStyle.Bold);
            dataGridView_Departments.RowTemplate.Height = 35;

            dataGridView_Items.ReadOnly = true;
            dataGridView_Items.AllowUserToAddRows = false;
            dataGridView_Items.AllowUserToDeleteRows = false;
            dataGridView_Items.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_Items.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_Items.DefaultCellStyle.Font = new Font("Cairo", 10);
            dataGridView_Items.ColumnHeadersDefaultCellStyle.Font = new Font("Cairo", 10, FontStyle.Bold);
            dataGridView_Items.RowTemplate.Height = 35;
        }

        async Task LoadDetails()
        {
            try
            {
                var client = new RestClient("http://102.209.3.101:9500");

                var request = new RestRequest(
                    "/WMSBKR/public/api/AreaLocation/details/" + areaLocationId,
                    Method.GET
                );

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("فشل جلب تفاصيل الـ Area:\n" + response.Content);
                    return;
                }

                JObject json = JObject.Parse(response.Content);

                if (json["success"]?.ToString().ToLower() != "true")
                {
                    MessageBox.Show(json["message"]?.ToString());
                    return;
                }

                JObject summary = json["summary"] as JObject;

                label_Total_Qty.Text =
                    "Total Qty : " + (summary?["total_qty"]?.ToString() ?? "0");

                label_Total_Items.Text =
                    "Total Items : " + (summary?["total_items"]?.ToString() ?? "0");

                label_Departments.Text =
                    "Departments : " + (summary?["departments_count"]?.ToString() ?? "0");

                LoadDepartmentsGrid(json["departments"] as JArray);
                LoadItemsGrid(json["items"] as JArray);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
        }

        void LoadDepartmentsGrid(JArray departments)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Department");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Items");

            if (departments != null)
            {
                foreach (JObject d in departments)
                {
                    dt.Rows.Add(
                        d["department"]?.ToString(),
                        d["qty"]?.ToString(),
                        d["items"]?.ToString()
                    );
                }
            }

            dataGridView_Departments.DataSource = dt;
        }

        void LoadItemsGrid(JArray items)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Barcode");
            dt.Columns.Add("Description");
            dt.Columns.Add("Department");
            dt.Columns.Add("UOM");
            dt.Columns.Add("Location");
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
                        item["location"]?.ToString(),
                        item["qty"]?.ToString()
                    );
                }
            }

            dataGridView_Items.DataSource = dt;
        }

        private void guna2Button_Shelfs_Click(object sender, EventArgs e)
        {
            Pages.Area_Monitor.ShelfsFrm frm = new Pages.Area_Monitor.ShelfsFrm(
                areaLocationId,
                storeName,
                areaName,
                locationFrom,
                locationTo
            );

            frm.ShowDialog();
        }

        private void guna2Button_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void guna2Button_delete_area_Click(object sender, EventArgs e)
        {

            string msg =
                "تحذير خطير!\n\n" +
                "سيتم حذف هذه الـ Area بالكامل:\n\n" +
                storeName + " - AREA " + areaName + "\n" +
                "Locations: " + locationFrom + " -> " + locationTo + "\n\n" +
                "وسيتم حذف كل الـ Shelfs / Locations وكل المنتجات الموجودة داخلها.\n\n" +
                "هل أنت متأكد؟";

            DialogResult r1 = MessageBox.Show(
                msg,
                "Confirm Delete Area",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (r1 != DialogResult.Yes)
                return;

            DialogResult r2 = MessageBox.Show(
                "تأكيد أخير:\n\nهل تريد حذف هذه الـ Area نهائياً؟",
                "Final Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Stop
            );

            if (r2 != DialogResult.Yes)
                return;

            try
            {
                var client = new RestClient("http://102.209.3.101:9500");

                var request = new RestRequest(
                    "/WMSBKR/public/api/AreaLocation/delete/" + areaLocationId,
                    Method.DELETE
                );

                IRestResponse response = await client.ExecuteTaskAsync(request);

                JObject json = JObject.Parse(response.Content);

                bool success = json["success"]?.ToObject<bool>() ?? false;
                string message = json["message"]?.ToString();

                if (success)
                {
                    MessageBox.Show(
                        "✅ " + message + "\n\n" +
                        "Deleted Rows: " + json["deleted_rows"] + "\n" +
                        "Deleted Qty: " + json["deleted_qty"]
                    );

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("❌ " + message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }

        }
    }
}