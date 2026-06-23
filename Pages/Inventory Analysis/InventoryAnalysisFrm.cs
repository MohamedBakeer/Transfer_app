using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

using System.Threading.Tasks;

namespace Transfer_app.Pages.Inventory_Analysis
{
    public partial class InventoryAnalysisFrm : Form
    {
        Cls.component component = new Cls.component();

        JObject lastResult = null;
        string lastDownloadUrl = "";

        ToolTip tip = new ToolTip();
        public InventoryAnalysisFrm()
        {
            InitializeComponent();
            component.SetRoundedForm(this, 20);

        }

        private async void InventoryAnalysisFrm_Load(object sender, EventArgs e)
        {
            LoadStores();
            LoadModes();
            await LoadDepartments();

            SetupArabicTooltips();

            guna2Button_downloadPdf.Visible = false;
        }

        async Task LoadDepartments()
        {
            try
            {
                guna2ComboBox_department.Items.Clear();
                guna2ComboBox_department.Items.Add("all");

                var client = new RestClient("http://102.209.3.101:9500");

                var request = new RestRequest(
                    "/WMSBKR/public/api/InventoryAnalysis/departments",
                    Method.GET
                );

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("فشل جلب الأقسام:\n" + response.Content);
                    return;
                }

                JObject json = JObject.Parse(response.Content);

                JArray departments = json["departments"] as JArray;

                if (departments != null)
                {
                    foreach (var dep in departments)
                    {
                        guna2ComboBox_department.Items.Add(dep.ToString());
                    }
                }

                guna2ComboBox_department.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading departments:\n" + ex.Message);
            }
        }

        void SetupArabicTooltips()
        {
            tip.AutoPopDelay = 12000;
            tip.InitialDelay = 400;
            tip.ReshowDelay = 200;
            tip.ShowAlways = true;
            tip.IsBalloon = true;
            tip.ToolTipTitle = "توضيح";

            tip.SetToolTip(guna2ComboBox_fromStore,
                "المصدر الذي سيتم أخذ البضاعة منه.\nمثال: M02 WH يعني مخزن فرع M02.");

            tip.SetToolTip(guna2ComboBox_toStore,
                "الوجهة التي تحتاج البضاعة.\nمثال: M02 BR يعني صالة عرض فرع M02.");

            tip.SetToolTip(gunaNumeric_lowQty,
                "أقل كمية تعتبر الصنف قليل.\nمثلاً إذا وضعتها 3، أي صنف كميته 3 أو أقل يعتبر قليل.");

            tip.SetToolTip(guna2ComboBox_mode,
                "طريقة حساب الكمية المقترحة.\nFixed: يعطي كمية ثابتة.\nAuto: يحسب الكمية حسب الفرق بين المصدر والوجهة.");

            tip.SetToolTip(gunaNumeric_fixedQty,
                "الكمية الثابتة المقترحة لكل صنف.\nتُستخدم فقط عندما يكون Mode = fixed.");

            tip.SetToolTip(gunaNumeric_safetyQty,
                "كمية الأمان التي يجب أن تبقى في المصدر.\nمثلاً 5 يعني لا يقترح نقل آخر 5 قطع من المخزن.");

            tip.SetToolTip(gunaNumeric_maxQty,
                "أكبر كمية يمكن اقتراحها للصنف الواحد.\nتُستخدم فقط عندما يكون Mode = auto.");

            tip.SetToolTip(guna2Button_analysis,
                "ابدأ تحليل المخزون بين المصدر والوجهة.\nبعد اكتمال التحليل سيظهر زر تحميل PDF.");

            tip.SetToolTip(guna2Button_downloadPdf,
                "تحميل تقرير PDF يحتوي على النتائج والتوصيات وأماكن السحب.");

            tip.SetToolTip(guna2Button_back,
                "الرجوع إلى القائمة الرئيسية.");
        }


        void LoadStores()
        {
            guna2ComboBox_fromStore.Items.Clear();
            guna2ComboBox_toStore.Items.Clear();

            foreach (var store in Class.StoreManager.StoresMap)
            {
                guna2ComboBox_fromStore.Items.Add(store.Key);
                guna2ComboBox_toStore.Items.Add(store.Key);
            }

            if (guna2ComboBox_fromStore.Items.Count > 0)
                guna2ComboBox_fromStore.SelectedIndex = 0;

            if (guna2ComboBox_toStore.Items.Count > 1)
                guna2ComboBox_toStore.SelectedIndex = 1;
            else if (guna2ComboBox_toStore.Items.Count > 0)
                guna2ComboBox_toStore.SelectedIndex = 0;
        }

        void LoadModes()
        {
            guna2ComboBox_mode.Items.Clear();
            guna2ComboBox_mode.Items.Add("fixed");
            guna2ComboBox_mode.Items.Add("auto");
            guna2ComboBox_mode.SelectedIndex = 0;

            gunaNumeric_lowQty.Value = 3;
            gunaNumeric_fixedQty.Value = 10;
            gunaNumeric_safetyQty.Value = 5;
            gunaNumeric_maxQty.Value = 20;

            ApplyModeView();
        }

        void ApplyModeView()
        {
            string mode = guna2ComboBox_mode.Text;

            if (mode == "fixed")
            {
                gunaNumeric_fixedQty.Enabled = true;
                gunaNumeric_safetyQty.Enabled = true;
                gunaNumeric_maxQty.Enabled = false;
            }
            else
            {
                gunaNumeric_fixedQty.Enabled = false;
                gunaNumeric_safetyQty.Enabled = true;
                gunaNumeric_maxQty.Enabled = true;
            }
        }

        private void guna2ComboBox_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyModeView();
        }

        private async void guna2Button_analysis_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox_fromStore.SelectedItem == null || guna2ComboBox_toStore.SelectedItem == null)
            {
                MessageBox.Show("Please select From Store and To Store.");
                return;
            }

            if (guna2ComboBox_fromStore.Text == guna2ComboBox_toStore.Text)
            {
                MessageBox.Show("From Store and To Store cannot be same.");
                return;
            }

            string fromStore = Class.StoreManager.GetCode(guna2ComboBox_fromStore.Text);
            string toStore = Class.StoreManager.GetCode(guna2ComboBox_toStore.Text);

            int lowQty = Convert.ToInt32(gunaNumeric_lowQty.Value);
            int fixedQty = Convert.ToInt32(gunaNumeric_fixedQty.Value);
            int safetyQty = Convert.ToInt32(gunaNumeric_safetyQty.Value);
            int maxQty = Convert.ToInt32(gunaNumeric_maxQty.Value);
            string mode = guna2ComboBox_mode.Text;
            string department = guna2ComboBox_department.Text;

            try
            {
                guna2Button_analysis.Enabled = false;
                guna2Button_downloadPdf.Visible = false;
                lastResult = null;
                lastDownloadUrl = "";

                var client = new RestClient("http://102.209.3.101:9500");

                string url =
                    "/WMSBKR/public/api/InventoryAnalysis/internal" +
                    "?from_store=" + fromStore +
                    "&to_store=" + toStore +
                    "&low_qty=" + lowQty +
                    "&suggest_mode=" + mode +
                    "&suggest_qty=" + fixedQty +
                    "&safety_qty=" + safetyQty +
                    "&max_qty=" + maxQty +
                    "&department=" + Uri.EscapeDataString(department);

                var request = new RestRequest(url, Method.GET);
                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("Analysis failed:\n" + response.Content);
                    return;
                }

                JObject json = JObject.Parse(response.Content);

                if (json["success"]?.ToString().ToLower() != "true")
                {
                    MessageBox.Show(json["message"]?.ToString());
                    return;
                }

                lastResult = json;

                JObject summary = json["summary"] as JObject;

                MessageBox.Show(
                    "Analysis Completed.\n\n" +
                    "Missing Items: " + summary["missing_items"] + "\n" +
                    "Low Items: " + summary["low_items"] + "\n" +
                    "Both Low Items: " + summary["both_low_items"] + "\n" +
                    "Recommendations: " + summary["recommendations"],
                    "Inventory Analysis"
                );

                MessageBox.Show(
                    "تم إكمال التحليل بنجاح.\n\n" +
                    "الأصناف غير الموجودة: " + summary["missing_items"] + "\n" +
                    "الأصناف القليلة: " + summary["low_items"] + "\n" +
                    "الأصناف القليلة في الطرفين: " + summary["both_low_items"] + "\n" +
                    "عدد التوصيات: " + summary["recommendations"],
                    "Inventory Analysis"
                );

                guna2Button_downloadPdf.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
            finally
            {
                guna2Button_analysis.Enabled = true;
            }
        }

        private async void guna2Button_downloadPdf_Click(object sender, EventArgs e)
        {
            if (lastResult == null)
            {
                MessageBox.Show("Please run analysis first.");
                return;
            }

            try
            {
                guna2Button_downloadPdf.Enabled = false;

                string fromStore = Class.StoreManager.GetCode(guna2ComboBox_fromStore.Text);
                string toStore = Class.StoreManager.GetCode(guna2ComboBox_toStore.Text);

                int lowQty = Convert.ToInt32(gunaNumeric_lowQty.Value);
                int fixedQty = Convert.ToInt32(gunaNumeric_fixedQty.Value);
                int safetyQty = Convert.ToInt32(gunaNumeric_safetyQty.Value);
                int maxQty = Convert.ToInt32(gunaNumeric_maxQty.Value);
                string mode = guna2ComboBox_mode.Text;
                string department = guna2ComboBox_department.Text;

                var client = new RestClient("http://102.209.3.101:9500");

                string url =
                    "/WMSBKR/public/api/InventoryAnalysis/pdf" +
                    "?from_store=" + fromStore +
                    "&to_store=" + toStore +
                    "&low_qty=" + lowQty +
                    "&suggest_mode=" + mode +
                    "&suggest_qty=" + fixedQty +
                    "&safety_qty=" + safetyQty +
                    "&max_qty=" + maxQty +
                    "&department=" + Uri.EscapeDataString(department);

                var request = new RestRequest(url, Method.GET);
                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("PDF failed:\n" + response.Content);
                    return;
                }

                JObject json = JObject.Parse(response.Content);

                if (json["success"]?.ToString().ToLower() != "true")
                {
                    MessageBox.Show(json["message"]?.ToString());
                    return;
                }

                string downloadUrl = json["download_url"]?.ToString();

                if (string.IsNullOrWhiteSpace(downloadUrl))
                {
                    MessageBox.Show("Download URL not found.");
                    return;
                }

                System.Diagnostics.Process.Start(downloadUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
            finally
            {
                guna2Button_downloadPdf.Enabled = true;
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