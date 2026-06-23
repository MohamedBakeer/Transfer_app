using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transfer_app
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Dictionary<string, string> whsMap = new Dictionary<string, string>()
        {
            {"M1 WH", "BG-WH-01"},
            {"M1 BR", "BG-BR-01"},
            {"M2 WH", "TR-WH-01"},
            {"M2 BR", "TR-BR-01"},
            {"M3 WH", "TR-WH-03"},
            {"M3 BR", "TR-BR-03"},
        };


        private void button_set_Click(object sender, EventArgs e)
        {
            try
            {
                string from = whsMap[comboBox_from.Text];
                string to = whsMap[comboBox_to.Text];
                string this_Whs = whsMap[comboBox_this_Whs.Text];

                if (from == to)
                {
                    MessageBox.Show("FROM and TO cannot be the same location.");
                    return;
                }

                //MessageBox.Show($"FROM = {from}\nTO = {to}");

                Properties.Settings.Default.from = from;    
                Properties.Settings.Default.to = to;
                Properties.Settings.Default.thisWHs = this_Whs;
                Properties.Settings.Default.Save(); 
                this.Close();
            }
            catch
            {
                MessageBox.Show("Please select both FROM and TO locations.");
            }
        }

        private void Setting_Load(object sender, EventArgs e)
        {

            // تحميل القيم المحفوظة
            comboBox_from.Items.Clear();
            comboBox_to.Items.Clear();
            comboBox_this_Whs.Items.Clear();

            comboBox_from.Items.AddRange(whsMap.Keys.ToArray());
            comboBox_to.Items.AddRange(whsMap.Keys.ToArray());
            comboBox_this_Whs.Items.AddRange(whsMap.Keys.ToArray());

            comboBox_from.Text = whsMap.FirstOrDefault(x => x.Value == Properties.Settings.Default.from).Key;
            comboBox_to.Text = whsMap.FirstOrDefault(x => x.Value == Properties.Settings.Default.to).Key;
            comboBox_this_Whs.Text = whsMap.FirstOrDefault(x => x.Value == Properties.Settings.Default.thisWHs).Key;
            // Auto Update
            if (Properties.Settings.Default.AutoUpdate == "True")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Properties.Settings.Default.AutoUpdate = "True";
            }
            else
            {
                Properties.Settings.Default.AutoUpdate = "False";
            }

            Properties.Settings.Default.Save();
        }
    }
}