using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static SocketTool.CommData.CommData_Base;

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

        // 画面のメッセージリストにダンプを表示するか否か
        private bool _message_dump = false;
        public bool isMessage_Dump { get { return _message_dump; } }

        // 通信メッセージ定義
        protected Dictionary<string, CommMessageDefine> _message_def = new Dictionary<string, CommMessageDefine>();

        // データの値の説明定義
        protected Dictionary<string, ValuesDefine> _values_def = new Dictionary<string, ValuesDefine>();


        public override int ReadJson(string path)
        {
            try
            {
                int ret = base.ReadJson(path);

                if (_json_root.AsObject().ContainsKey("message-dump"))
                {
                    _message_dump = (_json_root["message-dump"].GetValue<int>() == 1);
                }
                else
                {
                    _message_dump = false;
                }
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
            catch (Exception ex)            
            {
                throw new Exception($"JsonDataDef読み込み失敗{path}({ex.Message})");
            }
            return 0;
        }

        public CommMessageDefine GetMessageDefine(string dtype)
        {
            return _message_def[dtype];
        }

        public void SetMessageDump(bool sw) 
        {
            _message_dump = sw;
        }

        public string GetValueDescription(string fldid, string val)
        {
            string valid = fldid;
            if (valid.Contains("_"))
            {
                valid = valid.Substring(0, valid.IndexOf("_"));
            }
            if (_values_def.ContainsKey(valid))
            {
                return _values_def[valid][val];
            }

            return "？？？";
        }


        public class CommMessageDefine
        {
            string _dtype;
            string _name;
            int _data_len;
            int _data_minlen;

            public string DType { get { return _dtype; } }
            public string Name { get { return _name; } }
            public int Length {  get { return _data_len; } }
            public int MinLength { get { return _data_minlen; } }

            Dictionary<string, FieldDefine> _fld_def_list = new Dictionary<string, FieldDefine>();
            public Dictionary<string, FieldDefine> Fld_List { get {  return _fld_def_list; } }  

            public CommMessageDefine(JsonObject def)
            {
                _dtype = def["id"].ToString();
                _name = def["name"].ToString();
                _data_len = def["len"].GetValue<int>();
                if (def.ContainsKey("minlen"))
                {
                    _data_minlen = def["minlen"].GetValue<int>();
                }
                else
                {
                    _data_minlen = 0;
                }

                foreach (JsonObject obj in def["flds"].AsArray())
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
            bool _disp_desc;
            public string FldId { get { return _id; } }
            public string FldName { get { return _name; } }
            public int FldLength { get { return _bytelen; } }
            public int FldOffset { get { return _ofs; } }
            public bool isDispDesc {  get { return _disp_desc; } }

            public FieldDefine(JsonObject def)
            {
                _id = def["id"].ToString();
                _ofs = def["ofs"].GetValue<int>();
                _bytelen = def["len"].GetValue<int>();

                if (def.ContainsKey("disp"))
                {
                    _disp_desc = def["disp"].GetValue<int>() ==1;
                }

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

            /// <summary>
            /// データ長変更
            /// </summary>
            /// <remarks>可変長データの時、後から設定する</remarks>
            /// <param name="len"></param>
            public void SetFldLength(int len)
            {
                _bytelen = len;
            }
        }


        public class ValuesDefine
        {
            string _fldid;
            string _fldname;

            public string FldId { get { return _fldid; } }

            public string FldName { get { return _fldname; } }

            Dictionary<string, string> _values_def = new Dictionary<string, string>();
            JsonObject _format_def = new JsonObject();

            public ValuesDefine(JsonObject def)
            {
                _fldid = def["id"].ToString();
                _fldname = def["name"].ToString();

                if (def.ContainsKey("values"))
                {
                    foreach (KeyValuePair<string, JsonNode> pair in (JsonObject)def["values"])
                    {
                        _values_def.Add(pair.Key, pair.Value.ToString());
                    }
                }
                if (def.ContainsKey("format"))
                {
                    _format_def = def["format"].AsObject();
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
                        if (_format_def.ContainsKey("type"))
                        {
                            if (_format_def["type"].ToString() == "int")
                            {
                                FldValue fld = new FldValue(val);
                                int valint = fld.GetAsInt();
                                if (_format_def.ContainsKey("notdisp"))
                                {
                                    int notdisp = _format_def["notdisp"].GetValue<int>();
                                    if (valint ==notdisp)
                                    {
                                        return string.Empty;
                                    }
                                }
                                string fmt = _format_def["fmt"].ToString();
                                return string.Format(fmt, valint);
                            }
                            if (_format_def["type"].ToString() == "image")
                            {
                                string fmt = _format_def["fmt"].ToString();
                                return string.Format(fmt, val);
                            }
                        }
                    }
                    return "？？？";
                }
            }
            public string[] Values
            {
                get { return _values_def.Keys.ToArray<string>(); }
            }
        }
    }
}
