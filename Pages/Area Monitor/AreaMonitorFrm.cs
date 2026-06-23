using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace Transfer_app.Pages.Area_Monitor
{
    public partial class AreaMonitorFrm : Form
    {
        Cls.component component = new Cls.component();
        ToolTip toolTip = new ToolTip();

        public AreaMonitorFrm()
        {
            InitializeComponent();
            component.SetRoundedForm(this, 20);

            guna2Button_load.Click += guna2Button_load_Click;
            guna2Button_back.Click += guna2Button_back_Click;
            guna2Button_add_arae.Click += guna2Button_add_arae_Click;
        }

        private void AreaMonitorFrm_Load(object sender, EventArgs e)
        {
            guna2Button_add_arae.Visible = Class.Session.Role == "admin";
            guna2Button_add_arae.Enabled = Class.Session.Role == "admin";

            flowLayoutPanel_areas.AutoScroll = true;
            flowLayoutPanel_areas.WrapContents = true;
            flowLayoutPanel_areas.Padding = new Padding(15);
            flowLayoutPanel_areas.BackColor = Color.WhiteSmoke;

            guna2ComboBox_Branches.SelectedIndexChanged += (s, ev) =>
            {
                FillStoresByBranch();
                flowLayoutPanel_areas.Controls.Clear();
                label_total_qty_item.Text = "Item : 0 - Qty : 0";
            };

            FillBranchesCombo();
        }

        void FillBranchesCombo()
        {
            guna2ComboBox_Branches.Items.Clear();

            var branches = Class.StoreManager.StoresMap.Keys
                .Select(x => x.Split(' ')[0])
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            foreach (string branch in branches)
                guna2ComboBox_Branches.Items.Add(branch);

            if (guna2ComboBox_Branches.Items.Count > 0)
                guna2ComboBox_Branches.SelectedIndex = 0;
        }
        void FillStoresByBranch()
        {
            guna2ComboBox_branch_location.Items.Clear();

            string branch = guna2ComboBox_Branches.Text;

            foreach (var store in Class.StoreManager.StoresMap)
            {
                if (store.Key.StartsWith(branch + " "))
                    guna2ComboBox_branch_location.Items.Add(store.Key);
            }

            if (guna2ComboBox_branch_location.Items.Count > 0)
                guna2ComboBox_branch_location.SelectedIndex = 0;
        }

        private async void guna2Button_load_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox_branch_location.SelectedItem == null)
            {
                MessageBox.Show("Please select store.");
                return;
            }

            string storeName = guna2ComboBox_branch_location.Text;
            string storeCode = Class.StoreManager.GetCode(storeName);

            if (string.IsNullOrWhiteSpace(storeCode))
            {
                MessageBox.Show("Store code not found.");
                return;
            }

            await LoadAreaCardsFromServer(storeName, storeCode);
        }

        async Task LoadAreaCardsFromServer(string storeName, string storeCode)
        {
            try
            {
                flowLayoutPanel_areas.Controls.Clear();

                var client = new RestClient("http://102.209.3.101:9500");

                var request = new RestRequest(
                    "/WMSBKR/public/api/AreaLocation/areas/" + storeCode,
                    Method.GET
                );

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("فشل جلب المناطق:\n" + response.Content);
                    return;
                }

                JObject json = JObject.Parse(response.Content);
                JArray areas = json["areas"] as JArray;

                if (areas == null || areas.Count == 0)
                {
                    MessageBox.Show("Not Found Area.");
                    return;
                }

                int grandQty = 0;
                int grandItems = 0;

                foreach (JObject area in areas)
                {
                    int totalQty = Convert.ToInt32(area["total_qty"]);
                    int totalItems = Convert.ToInt32(area["total_items"]);

                    grandQty += totalQty;
                    grandItems += totalItems;

                    AddAreaCard(
                        Convert.ToInt32(area["id"]),
                        storeName,
                        area["store_code"]?.ToString(),
                        area["area"]?.ToString(),
                        Convert.ToInt32(area["location_from"]),
                        Convert.ToInt32(area["location_to"]),
                        totalQty,
                        totalItems
                    );
                }

                label_total_qty_item.Text = "Item : " + grandItems + " - Qty : " + grandQty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading areas:\n" + ex.Message);
            }
        }

        void AddAreaCard(
                int areaLocationId,
                string storeName,
                string storeCode,
                string area,
                int locationFrom,
                int locationTo,
                int totalQty,
                int totalItems
            )
        {
            Panel card = new Panel();
            card.Width = 230;
            card.Height = 145;
            card.Margin = new Padding(12);
            card.BackColor = Color.White;
            card.Cursor = Cursors.Hand;

            card.Tag = new AreaCardInfo
            {
                AreaLocationId = areaLocationId,
                StoreName = storeName,
                StoreCode = storeCode,
                Area = area,
                LocationFrom = locationFrom,
                LocationTo = locationTo,
                TotalQty = totalQty,
                TotalItems = totalItems
            };

            card.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(
                    e.Graphics,
                    card.ClientRectangle,
                    Color.Gainsboro,
                    ButtonBorderStyle.Solid
                );
            };

            Label lblArea = new Label();
            lblArea.Text = "AREA " + area;
            lblArea.Font = new Font("Cairo", 18, FontStyle.Bold);
            lblArea.ForeColor = Color.Black;
            lblArea.AutoSize = false;
            lblArea.TextAlign = ContentAlignment.MiddleCenter;
            lblArea.Dock = DockStyle.Top;
            lblArea.Height = 45;

            Label lblRange = new Label();
            lblRange.Text = "Location: " + locationFrom + " - " + locationTo;
            lblRange.Font = new Font("Cairo", 11);
            lblRange.ForeColor = Color.DimGray;
            lblRange.AutoSize = false;
            lblRange.TextAlign = ContentAlignment.MiddleCenter;
            lblRange.Dock = DockStyle.Top;
            lblRange.Height = 30;

            Label lblQty = new Label();
            lblQty.Text = "Total Qty: " + totalQty;
            lblQty.Font = new Font("Cairo", 12, FontStyle.Bold);
            lblQty.ForeColor = Color.Black;
            lblQty.AutoSize = false;
            lblQty.TextAlign = ContentAlignment.MiddleCenter;
            lblQty.Dock = DockStyle.Top;
            lblQty.Height = 30;

            Label lblItems = new Label();
            lblItems.Text = "Items: " + totalItems;
            lblItems.Font = new Font("Cairo", 12);
            lblItems.ForeColor = Color.Black;
            lblItems.AutoSize = false;
            lblItems.TextAlign = ContentAlignment.MiddleCenter;
            lblItems.Dock = DockStyle.Top;
            lblItems.Height = 30;

            card.Controls.Add(lblItems);
            card.Controls.Add(lblQty);
            card.Controls.Add(lblRange);
            card.Controls.Add(lblArea);

            string tip =
                "Store: " + storeName + "\n" +
                "Store Code: " + storeCode + "\n" +
                "Area: " + area + "\n" +
                "Location Range: " + locationFrom + " - " + locationTo + "\n" +
                "Total Qty: " + totalQty + "\n" +
                "Items: " + totalItems;

            toolTip.SetToolTip(card, tip);
            toolTip.SetToolTip(lblArea, tip);
            toolTip.SetToolTip(lblRange, tip);
            toolTip.SetToolTip(lblQty, tip);
            toolTip.SetToolTip(lblItems, tip);

            card.Click += AreaCard_Click;
            lblArea.Click += AreaCard_Click;
            lblRange.Click += AreaCard_Click;
            lblQty.Click += AreaCard_Click;
            lblItems.Click += AreaCard_Click;

            flowLayoutPanel_areas.Controls.Add(card);
        }

        private void AreaCard_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;

            while (c != null && !(c.Tag is AreaCardInfo))
                c = c.Parent;

            if (c == null)
                return;

            AreaCardInfo info = c.Tag as AreaCardInfo;

            AreaDetailsFrm frm = new AreaDetailsFrm(
                    info.AreaLocationId,
                    info.StoreName,
                    info.Area,
                    info.LocationFrom,
                    info.LocationTo
                );

            DialogResult result = frm.ShowDialog();

            if (result == DialogResult.OK && guna2ComboBox_branch_location.SelectedItem != null)
            {
                guna2Button_load.PerformClick();
            }
        }

        private void guna2Button_back_Click(object sender, EventArgs e)
        {
            SelectFrm frm = new SelectFrm();
            frm.Show();
            this.Hide();
        }

        private void guna2Button_add_arae_Click(object sender, EventArgs e)
        {
            Pages.Area_Monitor.AreaAddFrm frm = new Pages.Area_Monitor.AreaAddFrm();
            frm.ShowDialog();

            // بعد الإضافة نعيد تحميل الكروت لو مخزن مختار
            if (guna2ComboBox_branch_location.SelectedItem != null)
            {
                guna2Button_load.PerformClick();
            }
        }

    }

    public class AreaCardInfo
    {
        public int AreaLocationId { get; set; }

        public string StoreName { get; set; }
        public string StoreCode { get; set; }
        public string Area { get; set; }

        public int LocationFrom { get; set; }
        public int LocationTo { get; set; }

        public int TotalQty { get; set; }
        public int TotalItems { get; set; }
    }
}