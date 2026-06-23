using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transfer_app.Pages.Area_Monitor
{
    public partial class ShelfsFrm : Form
    {
        Cls.component component = new Cls.component();

        int areaLocationId;
        string storeName;
        string areaName;
        int locationFrom;
        int locationTo;

        ToolTip toolTip = new ToolTip();

        public ShelfsFrm(
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

            guna2Button_back.Click += guna2Button_back_Click;
        }

        private async void ShelfsFrm_Load(object sender, EventArgs e)
        {
            label_title.Text = storeName + " - AREA " + areaName +
                               " | Shelfs " + locationFrom + " -> " + locationTo;

            flowLayoutPanel_shelfs.AutoScroll = true;
            flowLayoutPanel_shelfs.WrapContents = true;
            flowLayoutPanel_shelfs.Padding = new Padding(15);
            flowLayoutPanel_shelfs.BackColor = Color.WhiteSmoke;

            await LoadShelfsFromServer();
        }

        async Task LoadShelfsFromServer()
        {
            try
            {
                flowLayoutPanel_shelfs.Controls.Clear();

                var client = new RestClient("http://102.209.3.101:9500");

                var request = new RestRequest(
                    "/WMSBKR/public/api/AreaLocation/shelfs/" + areaLocationId,
                    Method.GET
                );

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("Failed to load shelfs:\n" + response.Content);
                    return;
                }

                JObject json = JObject.Parse(response.Content);

                if (json["success"]?.ToString().ToLower() != "true")
                {
                    MessageBox.Show(json["message"]?.ToString());
                    return;
                }

                JArray shelfs = json["shelfs"] as JArray;

                if (shelfs == null || shelfs.Count == 0)
                {
                    MessageBox.Show("No shelfs found.");
                    return;
                }

                foreach (JObject shelf in shelfs)
                {
                    AddShelfCard(
                        Convert.ToInt32(shelf["location"]),
                        Convert.ToInt32(shelf["total_qty"]),
                        Convert.ToInt32(shelf["total_items"])
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
        }

        void AddShelfCard(int shelfNo, int totalQty, int totalItems)
        {
            Panel card = new Panel();
            card.Width = 260;
            card.Height = 120;
            card.Margin = new Padding(10);
            card.Cursor = Cursors.Hand;
            
            if (totalQty > 0)
            {
                card.BackColor = Color.White;
            }
            else
            {
                card.BackColor = Color.FromArgb(255, 220, 220);
            }

            card.Tag = shelfNo;

            card.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(
                    e.Graphics,
                    card.ClientRectangle,
                    Color.Gainsboro,
                    ButtonBorderStyle.Solid
                );
            };

            Label lblShelf = new Label();
            lblShelf.Text = "Shelf " + shelfNo;
            lblShelf.Font = new Font("Cairo", 16, FontStyle.Bold);
            lblShelf.TextAlign = ContentAlignment.MiddleCenter;
            lblShelf.Dock = DockStyle.Top;
            lblShelf.Height = 45;

            Label lblQty = new Label();
            lblQty.Text = "Qty: " + totalQty;
            lblQty.Font = new Font("Cairo", 11, FontStyle.Bold);
            lblQty.TextAlign = ContentAlignment.MiddleCenter;
            lblQty.Dock = DockStyle.Top;
            lblQty.Height = 30;

            Label lblItems = new Label();
            lblItems.Text = "Items: " + totalItems;
            lblItems.Font = new Font("Cairo", 11);
            lblItems.TextAlign = ContentAlignment.MiddleCenter;
            lblItems.Dock = DockStyle.Top;
            lblItems.Height = 30;

            card.Controls.Add(lblItems);
            card.Controls.Add(lblQty);
            card.Controls.Add(lblShelf);

            string tip =
                "Area: " + areaName + "\n" +
                "Shelf: " + shelfNo + "\n" +
                "Qty: " + totalQty + "\n" +
                "Items: " + totalItems;

            toolTip.SetToolTip(card, tip);
            toolTip.SetToolTip(lblShelf, tip);
            toolTip.SetToolTip(lblQty, tip);
            toolTip.SetToolTip(lblItems, tip);

            card.Click += ShelfCard_Click;
            lblShelf.Click += ShelfCard_Click;
            lblQty.Click += ShelfCard_Click;
            lblItems.Click += ShelfCard_Click;

            flowLayoutPanel_shelfs.Controls.Add(card);
        }

        private void ShelfCard_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;

            while (c != null && !(c.Tag is int))
                c = c.Parent;

            if (c == null)
                return;

            int shelfNo = Convert.ToInt32(c.Tag);

            ShelfDetailsFrm frm = new ShelfDetailsFrm(
                areaLocationId,
                storeName,
                areaName,
                shelfNo
            );

            frm.ShowDialog();
        }

        private void guna2Button_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}