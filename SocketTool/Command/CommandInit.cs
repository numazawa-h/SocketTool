using SocketTool.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketTool
{
    public  class CommandInit: Command
    {
        protected Dictionary<string, JsonValue> _values = new Dictionary<string, JsonValue>();

        public CommandInit(string json) : base(json)
        {
            if (_cmd_def["cmd"].ToString() == "init")
            {
                if (_cmd_def.AsObject().ContainsKey("values"))
                {
                    foreach (KeyValuePair<string, JsonNode> pair in (JsonObject)_cmd_def["values"])
                    {
                        string key = pair.Key.ToString();
                        _values.Add(key, (JsonValue)pair.Value.AsValue());
                    }
                }
            }
        }

        public override void Exec(FormMain form)
        {
            foreach(string key in _values.Keys)
            {
                JsonValue val = _values[key];
                switch (key)
                {
                    case "carno":
                        form.SetCarNo(val.GetValue<int>());
                        break;
                    case "dump":
                        bool sw = val.GetValue<int>() == 1;
                        JsonDataDef.GetInstance().SetMessageDump(sw);
                        break;
                }
            }
            Application.DoEvents();
        }
    }
}
