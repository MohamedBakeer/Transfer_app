using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer_app.Class
{
    public static class WhsManager
    {
        public static Dictionary<string, string> WhsMap =
            new Dictionary<string, string>()
        {
            {"M01 WH", "BG-WH-01"},
            {"M01 BR", "BG-BR-01"},

            {"M02 WH", "TR-WH-01"},
            {"M02 BR", "TR-BR-01"},

            {"M03 WH", "TR-WH-03"},
            {"M03 BR", "TR-BR-03"},
        };
    }
}
