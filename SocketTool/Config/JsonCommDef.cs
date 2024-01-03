using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SocketTool.Config
{
    public class JsonCommDef:Json
    {
        // シングルトン
        static private JsonCommDef _instance =null;
        static public JsonCommDef GetInstance()
        {
            if(_instance == null)
            {
                _instance = new JsonCommDef();
            }
            return _instance;
        }
        private JsonCommDef() : base()
        {

        }



        protected Dictionary<string, JsonNode> _remort_addr = new  Dictionary<string, JsonNode>();
        protected Dictionary<string, JsonNode> _self_addr = new Dictionary<string, JsonNode>();

        public override int ReadJson(string path)
        {
            try
            {
                base.ReadJson(path);
         
                _remort_addr.Clear();
                JsonNode remort = _json_root["remort"];
                foreach(JsonNode node in remort.AsArray())
                {
                    _remort_addr.Add(node["name"].ToString(), node);
                }

                _self_addr.Clear();
                JsonNode self = _json_root["self"];
                foreach (JsonNode node in self.AsArray())
                {
                    _self_addr.Add(node["name"].ToString(), node);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"JsonCommDef読み込み失敗{path}({ex.Message})"); ;
            }
            return 0;
        }

        public List<string>GetRemoteMachineList()
        {
            List<string> ret = new List<string>();
            foreach(string key in _remort_addr.Keys)
            {
                ret.Add(key.ToString());
            }
            return ret;
        }

        public List<string> GetSelfMachineList()
        {
            List<string> ret = new List<string>();
            foreach (string key in _self_addr.Keys)
            { 
                ret.Add(key.ToString());
            }
            return ret;
        }


        public string GetRemoteIp(string name, int no)
        {
            return _remort_addr[name]["addr"].AsArray()[no - 1]["ip"].ToString();
        }
        public string GetRemotePort(string name, int no)
        {
            return _remort_addr[name]["addr"].AsArray()[no - 1]["port"].ToString();
        }
        public string GetSelfIp(string name)
        {
            return _self_addr[name]["ip"].ToString();
        }
        public string GetSelfPort(string name, int no)
        {
            return _self_addr[name][$"port{no}"].ToString();
        }

    }
}
