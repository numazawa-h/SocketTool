using System;
using System.Collections.Generic;
using System.Linq;
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
        protected Dictionary<string, CommMessageDefine> _data_def = new Dictionary<string, CommMessageDefine>();

        protected Dictionary<string, DataDiscriptionDefine> _data_desc_def = new Dictionary<string, DataDiscriptionDefine>();


        public override int ReadJson(string path)
        {
            try
            {
                int ret = base.ReadJson(path);

                _data_def.Clear();
                foreach (JsonNode node in _json_root["datadef"].AsArray())
                {
                    _data_def.Add(node["id"].ToString(), new CommMessageDefine(node) );
                }
                foreach (JsonNode node in _json_root["valdef"].AsArray())
                {
                    _data_desc_def.Add(node["id"].ToString(), new DataDiscriptionDefine(node));
                }
            }
            catch (Exception ex)            {
                throw new Exception($"JsonDataDef読み込み失敗{path}({ex.Message})"); ;
            }
            return 0;
        }

        public CommMessageDefine GetMessageDefine(string id)
        {
            return _data_def[id];
        }


        public string GetValueDescription(string fldid, string val)
        {
            string description = "？？？";
            DataDiscriptionDefine def = null;
            _data_desc_def.TryGetValue(fldid, out def);
            if (def != null)
            {
                description = def[val];
            }
            return description;
        }

        public class FieldDefine
        {
            string _id;
            string _name;
            int _ofs;
            int _bytelen;
            public string Id { get { return _id; } }
            public string Name { get { return _name; } }
            public int Length { get { return _bytelen; } }
            public int Offset { get { return _ofs; } }

            public FieldDefine(JsonNode def)
            {
                _id = def["id"].ToString();
  //              _name = def["name"].ToString();
                _ofs = def["ofs"].GetValue<int>();
                _bytelen = def["len"].GetValue<int>();
            }
        }

        public class CommMessageDefine
        {
            string _id;
            string _name;
            int _data_len;

            public string Id { get { return _id; } }
            public string Name { get { return _name; } }
            public int Length {  get { return _data_len; } }

            Dictionary<string, FieldDefine> _fld_def_list = new Dictionary<string, FieldDefine>();

            public CommMessageDefine(JsonNode def)
            {
                _id = def["id"].ToString();
                _name = def["name"].ToString();
                _data_len = def["len"].GetValue<int>();

                foreach(JsonNode node in def["flds"].AsArray())
                {
                    _fld_def_list.Add(node["id"].ToString(), new FieldDefine(node));
                }
            }

            public string GetFldName(string id)
            {
                return _fld_def_list[id].Name;
            }

            public int GetFldOffset(string id)
            {
                return _fld_def_list[id].Offset;
            }

            public int GetFldLength(string id)
            {
                return _fld_def_list[id].Length;
            }

        }

        public class DataDiscriptionDefine
        {
            string _id;
            string _name;

            public string FldId { get { return _id; } }

            public string FldName { get { return _name; } }    

            Dictionary<string, string> _desc_def = new Dictionary<string, string>();

            public DataDiscriptionDefine(JsonNode def)
            {
                _id = def["id"].ToString();
                _name = def["name"].ToString();

                JsonNode desc_list = def["desc"];

                foreach ( KeyValuePair<string, JsonNode> pair in (JsonObject)desc_list)
                {
                    _desc_def.Add(pair.Key, pair.Value.ToString());
                }
            }

            public string this[string val] { 
                get {
                    string desc = "？？？";
                    _desc_def.TryGetValue(val, out desc);
                    return desc; 
                } 
            }

            public string[] Values()
            {
                return _desc_def.Keys.ToArray<string>();
            }

        }

    }
}
