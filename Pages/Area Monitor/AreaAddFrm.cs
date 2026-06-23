using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Windows.Forms;

namespace Transfer_app.Pages.Area_Monitor
{
    public partial class AreaAddFrm : Form
    {
        Cls.component component = new Cls.component();

        public AreaAddFrm()
        {
            InitializeComponent();
            component.SetRoundedForm(this, 20);

        }

        private void AreaAddFrm_Load(object sender, EventArgs e)
        {
            LoadStores();
        }

        void LoadStores()
        {
            guna2ComboBox_branch.Items.Clear();

            foreach (var store in Class.StoreManager.StoresMap)
            {
                guna2ComboBox_branch.Items.Add(store.Key);
            }

            if (guna2ComboBox_branch.Items.Count > 0)
                guna2ComboBox_branch.SelectedIndex = 0;
        }

        private async void guna2Button_add_arae_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox_branch.SelectedItem == null)
            {
                MessageBox.Show("Please select store.");
                return;
            }

            string storeName = guna2ComboBox_branch.Text;
            string storeCode = Class.StoreManager.GetCode(storeName);

            string area = textBox_username.Text.Trim();

            if (area == "")
            {
                MessageBox.Show("Please enter Area.");
                textBox_username.Focus();
                return;
            }

            int locationFrom = Convert.ToInt32(gunaNumeric_location.Value);
            int locationTo = Convert.ToInt32(gunaNumeric1.Value);

            if (locationFrom <= 0 || locationTo <= 0)
            {
                MessageBox.Show("Location must be greater than 0.");
                return;
            }

            if (locationFrom > locationTo)
            {
                MessageBox.Show("Location From must be less than Location To.");
                return;
            }

            try
            {
                var client = new RestClient("http://102.209.3.101:9500");

                var request = new RestRequest(
                    "/WMSBKR/public/api/AreaLocation/add",
                    Method.POST
                );

                request.AddParameter("store_code", storeCode);
                request.AddParameter("area", area);
                request.AddParameter("location_from", locationFrom);
                request.AddParameter("location_to", locationTo);

                IRestResponse response = await client.ExecuteTaskAsync(request);

                JObject json = JObject.Parse(response.Content);

                bool success = json["success"]?.ToObject<bool>() ?? false;
                string message = json["message"]?.ToString();

                if (success)
                {
                    MessageBox.Show("✅ " + message);
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

        private void guna2Button_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}