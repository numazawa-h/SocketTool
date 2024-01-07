using SocketTool.CommData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SocketTool
{
    public class CommandSend: Command
    {
        protected string _dtype =string.Empty;
        protected CommData_Data _data;
        protected Dictionary<string, JsonValue> _values = new Dictionary<string, JsonValue>();
        protected int _rescop_no = 0;

        public CommandSend(string json): base(json)
        {
            if (_cmd_def["cmd"].ToString() == "send")
            {
                _dtype = _cmd_def["dtype"].ToString();
                if (_cmd_def.AsObject().ContainsKey("values"))
                {
                    foreach (KeyValuePair<string, JsonNode> pair  in (JsonObject)_cmd_def["values"])
                    {
                        string key = pair.Key.ToString();
                        _values.Add(key, (JsonValue)pair.Value.AsValue() );
                    }
                }
            }
        }

        public void SetRescopNo(int rescop_no)
        {
            _rescop_no = rescop_no;
        }

        public  override void Exec(FormMain form)
        {
            if(_dtype == string.Empty)
            {
                // 送信コマンド以外なら実行しない
                return;
            }

            CommData_Data data = new CommData_Data(_dtype);
            foreach ( string key in _values.Keys )
            {
                JsonValue val = _values[key];

                if (key == "image")
                {
                    data.LoadImage("image", val.ToString());
                }
                else
                {
                    if(val.GetValueKind() == JsonValueKind.Number)
                    {
                        data.GetFldValue(key).SetAsInt(val.GetValue<int>());

                    }
                    else
                    {
                        data.GetFldValue(key).SetAsBcd(val.ToString());
                    }
                }
            }

            form.Send(_dtype, data.GetData(), _rescop_no);
        }

    }
}
