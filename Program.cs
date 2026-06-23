using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferApp;

namespace Transfer_app
{
    internal static class Program
    {
        static Cls.component component = new Cls.component();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!component.RunApp())
            {
                Application.Exit();
                return;
            }
            else
            {
                Application.Run(new LoginFrm());
            }
        }
    }
}
