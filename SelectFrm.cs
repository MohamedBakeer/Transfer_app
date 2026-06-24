using System;
using System.Windows.Forms;

namespace Transfer_app
{
    public partial class SelectFrm : Form
    {
        Cls.component component = new Cls.component();

        public SelectFrm()
        {
            InitializeComponent();
            component.SetRoundedForm(this, 20);
        }

        private void SelectFrm_Load(object sender, EventArgs e)
        {
            LoadFormsCombo();
        }

        void LoadFormsCombo()
        {
            guna2ComboBox_Frms.Items.Clear();

            string role = Class.Session.Role;

            if (role == "admin")
            {
                AddAdminForms();
            }
            else if (role == "brand_manager")
            {
                AddBrandManagerForms();
            }
            else if (role == "warehouse_user")
            {
                AddWarehouseUserForms();
            }
            else if (role == "ceo")
            {
                AddCeoForms();
            }

            if (guna2ComboBox_Frms.Items.Count > 0)
                guna2ComboBox_Frms.SelectedIndex = 0;
        }

        void AddAdminForms()
        {
            //guna2ComboBox_Frms.Items.Add("Internal Transfer");
            //guna2ComboBox_Frms.Items.Add("Transfer Request");
            //guna2ComboBox_Frms.Items.Add("Receive Transfer Request");
            //guna2ComboBox_Frms.Items.Add("Shipment Manifest");

            //guna2ComboBox_Frms.Items.Add("Area Location Count");
            //guna2ComboBox_Frms.Items.Add("Transfer Monitor");
            guna2ComboBox_Frms.Items.Add("Area Monitor");
            guna2ComboBox_Frms.Items.Add("Inventory Analysis");

            guna2ComboBox_Frms.Items.Add("Pending Upload");

            //guna2ComboBox_Frms.Items.Add("Approve Receive Difference");
            //guna2ComboBox_Frms.Items.Add("Adjustment Stock Request");

            //guna2ComboBox_Frms.Items.Add("Inventory Analysis");
            //guna2ComboBox_Frms.Items.Add("Supplier Shipment Receive");
            //guna2ComboBox_Frms.Items.Add("Approve Requests");

            //guna2ComboBox_Frms.Items.Add("Reports");
            //guna2ComboBox_Frms.Items.Add("Settings");
        }

        void AddBrandManagerForms()
        {
            guna2ComboBox_Frms.Items.Add("Area Location Count");
            guna2ComboBox_Frms.Items.Add("Area Location Count Pro");

            // لاحقاً
            // guna2ComboBox_Frms.Items.Add("Adjustment Stock Request");
             //guna2ComboBox_Frms.Items.Add("Inventory Analysis");
            // guna2ComboBox_Frms.Items.Add("Reports");
        }

        void AddWarehouseUserForms()
        {
            guna2ComboBox_Frms.Items.Add("Internal Transfer");
            guna2ComboBox_Frms.Items.Add("Transfer Request");
            guna2ComboBox_Frms.Items.Add("Shipment Manifest");
            guna2ComboBox_Frms.Items.Add("Receive Transfer Request");
            //guna2ComboBox_Frms.Items.Add("Location Transfer");


            // لاحقاً
            // guna2ComboBox_Frms.Items.Add("Adjustment Stock Request");
        }

        void AddCeoForms()
        {
            //guna2ComboBox_Frms.Items.Add("Transfer Monitor");
            guna2ComboBox_Frms.Items.Add("Area Monitor");
            guna2ComboBox_Frms.Items.Add("Inventory Analysis");

            // لاحقاً
            // guna2ComboBox_Frms.Items.Add("Inventory Monitor");
        }

        private void iconButton_start_Click(object sender, EventArgs e)
        {
            if (!component.LockButton(iconButton_start))
                return;

            try
            {
                if (guna2ComboBox_Frms.SelectedItem == null)
                {
                    MessageBox.Show("Please select an option.");
                    return;
                }

                string selectedFrm = guna2ComboBox_Frms.SelectedItem.ToString();

                OpenSelectedForm(selectedFrm);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
            finally
            {
                component.UnlockButton(iconButton_start);
            }
        }

        void OpenSelectedForm(string selectedFrm)
        {
            Form frm = null;

            if (selectedFrm == "Internal Transfer")
            {
                frm = new Pages.Internal_Transfer.Step_1_Select_Direction();
            }
            else if (selectedFrm == "Transfer Request")
            {
                frm = new Pages.Transfer_Request.Step_1_Select_Direction();
            }
            else if (selectedFrm == "Receive Transfer Request")
            {
                frm = new Pages.Receive_Transfer_Request.Step_1_Receive();
            }
            else if (selectedFrm == "Shipment Manifest")
            {
                frm = new Pages.Shipment_Manifest.Step_1_Select_Transfers();
            }
            else if (selectedFrm == "Area Location Count")
            {
                frm = new Pages.Area_Location_Count.AreaLocationCount();
            }
            else if (selectedFrm == "Area Location Count Pro")
            {
                frm = new Pages.Area_Location_Count.AreaLocationCountPro();
            }
            else if (selectedFrm == "Transfer Monitor")
            {
                frm = new Pages.Transfer_Monitor.TransferMonitorFrm();
            }
            else if (selectedFrm == "Area Monitor")
            {
                frm = new Pages.Area_Monitor.AreaMonitorFrm();
            }
            else if (selectedFrm == "Location Transfer")
            {
                frm = new Pages.Location_Transfer.LocationTransferFrm();
            }
            else if (selectedFrm == "Inventory Analysis")
            {
                frm = new Pages.Inventory_Analysis.InventoryAnalysisFrm();
            }
            else if (selectedFrm == "Adjustment Stock Request")
            {
                //MessageBox.Show("Adjustment Stock Request is not ready yet.");
                return;
            }
            else if (selectedFrm == "Pending Upload")
            {
                //frm = new Pages.Pending_Upload.PendingUploadFrm();
            }
            else if (selectedFrm == "Approve Receive Difference")
            {
                //MessageBox.Show("Approve Receive Difference is not ready yet.");
                return;
            }
            else if (selectedFrm == "Inventory Analysis")
            {
                //MessageBox.Show("Inventory Analysis is not ready yet.");
                return;
            }
            else if (selectedFrm == "Supplier Shipment Receive")
            {
                //MessageBox.Show("Supplier Shipment Receive is not ready yet.");
                return;
            }
            else if (selectedFrm == "Approve Requests")
            {
                //MessageBox.Show("Approve Requests is not ready yet.");
                return;
            }
            else if (selectedFrm == "Reports")
            {
                //MessageBox.Show("Reports is not ready yet.");
                return;
            }
            else if (selectedFrm == "Settings")
            {
                //MessageBox.Show("Settings is not ready yet.");
                return;
            }
            else if (selectedFrm == "Inventory Monitor")
            {
                //MessageBox.Show("Inventory Monitor is not ready yet.");
                return;
            }

            if (frm != null)
            {
                frm.Show();
                this.Hide();
            }
        }

        private void iconButton_logout_Click(object sender, EventArgs e)
        {
            LoginFrm frm = new LoginFrm();
            frm.Show();
            this.Hide();
        }
    }
}