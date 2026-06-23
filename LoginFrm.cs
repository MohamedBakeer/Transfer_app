using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferApp;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Transfer_app
{
    public partial class LoginFrm : Form
    {
        Cls.component component = new Cls.component();
        public LoginFrm()
        {
            if (!component.RunApp())
            {
                Application.Exit();
                return;
            }
            else
            {
                InitializeComponent();
                component.SetRoundedForm(this, 20);
            }
        }

        async Task LoadStores()
        {
            Class.StoreManager.Clear();

            var client = new RestClient("http://102.209.3.101:9500");

            var request = new RestRequest(
                "/WMSBKR/public/api/Store/list",
                Method.GET
            );

            IRestResponse response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new Exception("Failed to load stores:\n" + response.Content);

            JObject json = JObject.Parse(response.Content);
            JArray stores = json["stores"] as JArray;

            foreach (JObject store in stores)
            {
                string storeName = store["store_name"]?.ToString();
                string storeCode = store["store_code"]?.ToString();

                if (storeName != "" && storeCode != "")
                    Class.StoreManager.StoresMap[storeName] = storeCode;
            }
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        bool loading = false;
        private async void button_login_Click(object sender, EventArgs e)
        {
            try
            {
                if (!component.LockButton(button_login))
                    return;

                if (textBox_username.Text == string.Empty || textBox_pass.Text == string.Empty)
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                var client = new RestClient("http://102.209.3.101:9500");

                var request = new RestRequest("/WMSBKR/public/api/User/login", Method.POST);
                request.AlwaysMultipartFormData = true;

                request.AddParameter("username", textBox_username.Text.Trim());
                request.AddParameter("password", textBox_pass.Text.Trim());

                IRestResponse response = await client.ExecuteAsync(request);

                string result = response.Content ?? "";

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("Login failed:\n" + result);
                    return;
                }

                var jsonResponse = JToken.Parse(result);

                Class.Session.Name = jsonResponse["user"]?["name"]?.ToString();
                Class.Session.Username = jsonResponse["user"]?["username"]?.ToString();
                Class.Session.Password = jsonResponse["user"]?["password"]?.ToString();
                Class.Session.Role = jsonResponse["user"]?["role"]?.ToString();
                Class.Session.Branch = jsonResponse["user"]?["branch"]?.ToString();

                await LoadStores();

                bool itemLoaded = await Class.ItemMasterManager.LoadFromServer();

                if (!itemLoaded)
                {
                    
                }

                SelectFrm selectFrm = new SelectFrm();
                selectFrm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                component.UnlockButton(button_login);
            }
        }
    }
}
