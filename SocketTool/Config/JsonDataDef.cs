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
        protected Dictionary<string, DataDefine> _data_def = new Dictionary<string, DataDefine>();

        public override int ReadJson(string path)
        {
            try
            {
                int ret = base.ReadJson(path);
                if (ret < 0)
                {
                    return ret;
                }

                _data_def.Clear();
                foreach (JsonNode node in _json_root["datadef"].AsArray())
                {
                    _data_def.Add(node["id"].ToString(), new DataDefine(node) );
                }
            }
            catch (Exception ex)
            {
                OnException(new Exception($"json読み込み失敗{path}({ex.Message})")); ;
                return -3;
            }
            return 0;
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
                _name = def["name"].ToString();
                _ofs = def["ofs"].GetValue<int>();
                _bytelen = def["len"].GetValue<int>();
            }
        }

        public class DataDefine
        {
            string _id;
            string _name;
            int _data_len;

            public string Id { get { return _id; } }
            public string Name { get { return _name; } }
            public int Length {  get { return _data_len; } }

            Dictionary<string, FieldDefine> _fld_def_list = new Dictionary<string, FieldDefine>();

            public DataDefine(JsonNode def)
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
                return _fld_def_list["id"].Name;
            }

            public int GetFldOffset(string id)
            {
                return _fld_def_list["id"].Offset;
            }

            public int GetFldLength(string id)
            {
                return _fld_def_list["id"].Length;
            }

        }
    }
}
