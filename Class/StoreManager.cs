using System.Collections.Generic;

namespace Transfer_app.Class
{
    public static class StoreManager
    {
        public static Dictionary<string, string> StoresMap =
            new Dictionary<string, string>();

        public static void Clear()
        {
            StoresMap.Clear();
        }

        public static string GetCode(string storeName)
        {
            if (StoresMap.ContainsKey(storeName))
                return StoresMap[storeName];

            return "";
        }
    }
}