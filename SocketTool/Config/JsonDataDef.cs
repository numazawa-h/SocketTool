using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SocketTool.Config
{
    public class JsonDataDef: Json
    {
        // シングルトン
        static private JsonDataDef _instance = null;
        static public JsonDataDef GetInstance()
        {
            if (_instance == null)
            {
                _instance = new JsonDataDef();
            }
            return _instance;
        }
        private JsonDataDef():base()
        {
        }

        // 通信メッセージ定義
        protected Dictionary<string, CommMessageDefine> _message_def = new Dictionary<string, CommMessageDefine>();

        // データの値の説明定義
        protected Dictionary<string, ValuesDefine> _values_def = new Dictionary<string, ValuesDefine>();


        public override int ReadJson(string path)
        {
            try
            {
                int ret = base.ReadJson(path);

                _values_def.Clear();
                foreach (JsonObject def in _json_root["values-def"].AsArray())
                {
                    _values_def.Add(def["id"].ToString(), new ValuesDefine(def));
                }

                _message_def.Clear();
                foreach (JsonObject node in _json_root["message-def"].AsArray())
                {
                    _message_def.Add(node["id"].ToString(), new CommMessageDefine(node) );
                }
            }
            catch (Exception ex)            {
                throw new Exception($"JsonDataDef読み込み失敗{path}({ex.Message})"); ;
            }
            return 0;
        }

        public CommMessageDefine GetMessageDefine(string dtype)
        {
            return _message_def[dtype];
        }


        public string GetValueDescription(string fldid, string val)
        {
            if(_values_def.ContainsKey(fldid) && _values_def[fldid].Values.Contains<string>(val))
            {
                return _values_def[fldid][val];
            }

            //TODO:未完

            return "？？？";
        }



        public class CommMessageDefine
        {
            string _dtype;
            string _name;
            int _data_len;

            public string DType { get { return _dtype; } }
            public string Name { get { return _name; } }
            public int Length {  get { return _data_len; } }

            Dictionary<string, FieldDefine> _fld_def_list = new Dictionary<string, FieldDefine>();

            public CommMessageDefine(JsonObject def)
            {
                _dtype = def["id"].ToString();
                _name = def["name"].ToString();
                _data_len = def["len"].GetValue<int>();

                foreach(JsonObject obj in def["flds"].AsArray())
                {
                    _fld_def_list.Add(obj["id"].ToString(), new FieldDefine(obj));
                }
            }

            public string GetFldName(string fldid)
            {
                return _fld_def_list[fldid].FldName;
            }

            public int GetFldOffset(string fldid)
            {
                return _fld_def_list[fldid].FldOffset;
            }

            public int GetFldLength(string fldid)
            {
                return _fld_def_list[fldid].FldLength;
            }

        }

        public class FieldDefine
        {
            string _id;
            string _name;
            int _ofs;
            int _bytelen;
            public string FldId { get { return _id; } }
            public string FldName { get { return _name; } }
            public int FldLength { get { return _bytelen; } }
            public int FldOffset { get { return _ofs; } }

            public FieldDefine(JsonObject def)
            {
                _id = def["id"].ToString();
                _ofs = def["ofs"].GetValue<int>();
                _bytelen = def["len"].GetValue<int>();
                if (def.ContainsKey("name"))
                {
                    _name = def["name"].ToString();
                }
                else
                {
                    if (JsonDataDef.GetInstance()._values_def.ContainsKey(_id))
                    {
                        _name = JsonDataDef.GetInstance()._values_def[_id].FldName;
                    }
                    else
                    {
                        _name = _id;
                    }
                }
            }
        }


        public class ValuesDefine
        {
            string _id;
            string _name;

            public string FldId { get { return _id; } }

            public string FldName { get { return _name; } }

            Dictionary<string, string> _values_def = new Dictionary<string, string>();
            Dictionary<string, string> _format_def = new Dictionary<string, string>();

            public ValuesDefine(JsonObject def)
            {
                _id = def["id"].ToString();
                _name = def["name"].ToString();

                if (def.ContainsKey("values"))
                {
                    foreach (KeyValuePair<string, JsonNode> pair in (JsonObject)def["values"])
                    {
                        _values_def.Add(pair.Key, pair.Value.ToString());
                    }
                }
            }

            public string this[string val]
            {
                get
                {
                    if (_values_def.ContainsKey(val))
                    {
                        return _values_def[val];
                    }
                    else
                    {
                        //TODO:format対応
                        return "？？？";
                    }
                }
            }
            public string[] Values
            {
                get { return _values_def.Keys.ToArray<string>(); }
            }

        }
    }
}
