﻿using SocketTool.CommData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SocketTool
{
    public class CommandSend : Command
    {
        protected string _dtype = string.Empty;
        protected CommData_Data _data;
        protected Dictionary<string, JsonValue> _values = new Dictionary<string, JsonValue>();
        protected int _rescop_no = 0;

        public CommandSend(string json) : base(json)
        {
            Init(_cmd_def);
        }

        public CommandSend(JsonNode node)
        {
            Init(node);
        }

        private void Init(JsonNode node)
        {
            if (node["cmd"].ToString() == "send")
            {
                _dtype = node["dtype"].ToString();
                if (node.AsObject().ContainsKey("values"))
                {
                    foreach (KeyValuePair<string, JsonNode> pair in (JsonObject)node["values"])
                    {
                        string key = pair.Key.ToString();
                        _values.Add(key, (JsonValue)pair.Value.AsValue());
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
                    // imageタイプの時は、valにファイル名を設定しておく
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
                        // TODO: 追加したvalueタイプに未対応
                        data.GetFldValue(key).SetAsBcd(val.ToString());
                    }
                }
            }

            form.Send(_dtype, data.GetData(), _rescop_no);
        }

    }
}
