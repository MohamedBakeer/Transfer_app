using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;


namespace Transfer_app.Cls
{
    internal class component
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
(
    int nLeftRect,     // x-coordinate of upper-left corner
    int nTopRect,      // y-coordinate of upper-left corner
    int nRightRect,    // x-coordinate of lower-right corner
    int nBottomRect,   // y-coordinate of lower-right corner
    int nWidthEllipse, // height of ellipse
    int nHeightEllipse // width of ellipse
);

        public void SetRoundedForm(Form form, int cornerRadius)
        {
            form.FormBorderStyle = FormBorderStyle.None;
            form.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, form.Width, form.Height, cornerRadius, cornerRadius));

            FitFormToScreenIfNeeded(form);
        }

        public void FitFormToScreenIfNeeded(Form frm, int margin = 10)
        {
            Screen screen = Screen.FromControl(frm);
            Rectangle area = screen.WorkingArea;

            int maxWidth = area.Width - (margin * 2);
            int maxHeight = area.Height - (margin * 2);

            if (frm.Width > maxWidth || frm.Height > maxHeight)
            {
                frm.StartPosition = FormStartPosition.Manual;

                frm.Size = new Size(
                    Math.Min(frm.Width, maxWidth),
                    Math.Min(frm.Height, maxHeight)
                );

                frm.Location = new Point(
                    area.Left + (area.Width - frm.Width) / 2,
                    area.Top + (area.Height - frm.Height) / 2
                );
            }
        }

        public bool LockButton(Control btn)
        {
            if (!btn.Enabled)
                return false;

            btn.Enabled = false;
            return true;
        }

        public void UnlockButton(Control btn)
        {
            btn.Enabled = true;
        }


        public bool RunApp()
        {
            string p = Path.Combine(Application.StartupPath, "xls.dll");

            return File.Exists(p);
        }
    }
}
