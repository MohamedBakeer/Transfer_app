using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer_app.Class
{
    public static class TransferSession
    {
        public static string FromName { get; set; }
        public static string ToName { get; set; }

        public static string FromCode { get; set; }
        public static string ToCode { get; set; }

        public static Dictionary<string, int> StockFrom { get; set; }
            = new Dictionary<string, int>();

        public static Dictionary<string, int> StockTo { get; set; }
            = new Dictionary<string, int>();

        public static DataTable ItemsTable { get; set; } = new DataTable();

        public static void PrepareItemsTable()
        {
            ItemsTable.Clear();
            ItemsTable.Columns.Clear();

            ItemsTable.Columns.Add("Barcode");
            ItemsTable.Columns.Add("Zone");
            ItemsTable.Columns.Add("QtyScan");
            ItemsTable.Columns.Add("QtyWhs");
            ItemsTable.Columns.Add("QtyTo");
        }

        public static DataTable MissingTable { get; set; } = new DataTable();

        public static void PrepareMissingTable()
        {
            MissingTable.Clear();
            MissingTable.Columns.Clear();

            MissingTable.Columns.Add("Barcode");
            MissingTable.Columns.Add("ErrorType");
            MissingTable.Columns.Add("Count");
            MissingTable.Columns.Add("LastTime");
        }


        public static string RequestNo = "";
        public static string BoxNo = "";
        public static string ReceiveRequestFilePath = "";
    }
}
