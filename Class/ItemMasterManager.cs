using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Transfer_app.Class
{
    public static class ItemMasterManager
    {
        public static Dictionary<string, ItemInfo> Items =
            new Dictionary<string, ItemInfo>();

        public static bool IsLoaded
        {
            get { return Items.Count > 0; }
        }

        public static void Clear()
        {
            Items.Clear();
        }

        public static bool Exists(string barcode)
        {
            return Items.ContainsKey(barcode);
        }

        public static ItemInfo Get(string barcode)
        {
            if (Items.ContainsKey(barcode))
                return Items[barcode];

            return null;
        }

        public static async Task<bool> LoadFromServer()
        {
            try
            {
                Clear();

                var client = new RestClient("http://102.209.3.101:9500");

                var request = new RestRequest(
                    "/WMSBKR/public/api/AreaLocationCount/item-master-cache",
                    Method.GET
                );

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (!response.IsSuccessful)
                    return false;

                JObject json = JObject.Parse(response.Content);
                JArray items = json["items"] as JArray;

                if (items == null)
                    return false;

                foreach (JObject item in items)
                {
                    string barcode = item["barcode"]?.ToString();

                    if (string.IsNullOrWhiteSpace(barcode))
                        continue;

                    Items[barcode] = new ItemInfo
                    {
                        Barcode = barcode,
                        Description = item["description"]?.ToString(),
                        Department = item["department"]?.ToString(),
                        Uom = item["uom"]?.ToString()
                    };
                }

                return Items.Count > 0;
            }
            catch
            {
                return false;
            }
        }
    }

    public class ItemInfo
    {
        public string Barcode { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public string Uom { get; set; }
    }
}