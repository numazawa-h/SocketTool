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

        string _segment_no=null;
        string _machine_no=null;
        int[] _ack_chk = new int[4];
        int[] _health_interval = new int[4];
        int[] _connect_chk = new int[4];

        string _InitRemortMachineName;
        public string InitRemortMachineName => _InitRemortMachineName;

        string _InitSelfMachineName;
        public string InitSelfMachineName => _InitSelfMachineName;


        protected Dictionary<string, JsonNode> _remort_addr = new  Dictionary<string, JsonNode>();
        protected Dictionary<string, JsonNode> _self_addr = new Dictionary<string, JsonNode>();

        public override int ReadJson(string path)
        {
            try
            {
                base.ReadJson(path);

                JsonNode initdis = _json_root["initdis"];
                if(_machine_no == null) _machine_no = initdis["自装置番号"].ToString();
                if(_segment_no == null) _segment_no = initdis["監視制御局番号"].ToString();
                _ack_chk[0] = initdis["１系"]["受信側"]["肯定応答"].GetValue<int>();
                _ack_chk[1] = 0;
                _ack_chk[2] = initdis["２系"]["受信側"]["肯定応答"].GetValue<int>();
                _ack_chk[3] = 0;
                _health_interval[0] = 0;
                _health_interval[1] = initdis["１系"]["送信側"]["ヘルスチェック"].GetValue<int>();
                _health_interval[2] = 0;
                _health_interval[3] = initdis["２系"]["送信側"]["ヘルスチェック"].GetValue<int>();
                _connect_chk[0] = initdis["１系"]["受信側"]["接続"].GetValue<int>();
                _connect_chk[1] = initdis["１系"]["送信側"]["接続"].GetValue<int>();
                _connect_chk[2] = initdis["２系"]["受信側"]["接続"].GetValue<int>();
                _connect_chk[3] = initdis["２系"]["送信側"]["接続"].GetValue<int>();

                _remort_addr.Clear();
                JsonNode remort = _json_root["remort"];
                foreach(JsonNode node in remort.AsArray())
                {
                    string name = node["name"].ToString();
                    _remort_addr.Add(name, node);

                    string id1 = node["id"].AsArray()[0].ToString();
                    if (id1.Substring(0,2)== _segment_no)
                    {
                        _InitRemortMachineName = name;
                    }
                }

                _self_addr.Clear();
                JsonNode self = _json_root["self"];
                foreach (JsonNode node in self.AsArray())
                {
                    string name = node["name"].ToString();
                    _self_addr.Add(name, node);

                    string id = node["id"].ToString();
                    if (id == _machine_no)
                    {
                        _InitSelfMachineName = name;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"JsonCommDef読み込み失敗{path}({ex.Message})"); ;
            }
            return 0;
        }

        public void SetInit_segment_no(string segment_no)
        {
            _segment_no = segment_no;
        }
        public void SetInit_machine_no(string machine_no)
        {
            _machine_no = machine_no;
        }



        public string GetInitSelfMachineName()
        {
            return "料金所＃１L１レーン制御";
        }

        public bool GetRecvAckChk(int rescop_no)
        {
            if(rescop_no == 1) return this._ack_chk[0] == 1;
            if(rescop_no == 2) return this._ack_chk[2] == 1;
            return false;
        }
        public bool GetSendAckChk(int rescop_no)
        {
            if (rescop_no == 1) return this._ack_chk[1] == 1;
            if (rescop_no == 2) return this._ack_chk[3] == 1;
            return false;
        }

        public int GetRecvHealthInterval(int rescop_no)
        {
            if (rescop_no == 1) return this._health_interval[0];
            if (rescop_no == 2) return this._health_interval[2];
            return 0;
        }
        public int GetSendHealthInterval(int rescop_no)
        {
            if (rescop_no == 1) return this._health_interval[1];
            if (rescop_no == 2) return this._health_interval[3];
            return 0;
        }
        public bool GetRecvConnectChk(int rescop_no)
        {
            if (rescop_no == 1) return this._connect_chk[0] == 1;
            if (rescop_no == 2) return this._connect_chk[2] == 1;
            return false;
        }
        public bool GetSendConnectChk(int rescop_no)
        {
            if (rescop_no == 1) return this._connect_chk[1] == 1;
            if (rescop_no == 2) return this._connect_chk[3] == 1;
            return false;
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


        public string GetRemoteMachineCode(string name, int rescop_no)
        {
            return _remort_addr[name]["id"].AsArray()[rescop_no - 1].ToString();
        }
        public string GetRemoteIp(string name, int rescop_no)
        {
            return _remort_addr[name]["addr"].AsArray()[rescop_no - 1]["ip"].ToString();
        }
        public string GetRemotePort(string name, int rescop_no)
        {
            return _remort_addr[name]["addr"].AsArray()[rescop_no - 1]["port"].ToString();
        }

        public string GetSelfMachineCode(string name)
        {
            return _self_addr[name]["id"].ToString();
        }
        public string GetSelfIp(string name)
        {
            return _self_addr[name]["ip"].ToString();
        }
        public string GetSelfPort(string name, int rescop_no)
        {
            return _self_addr[name][$"port{rescop_no}"].ToString();
        }

    }
}
