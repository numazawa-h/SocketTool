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

        string _segment_no=null;        // 局番号
        string _machine_no=null;        // 装置番号

        int _auto_send = 0;                     // 自動送信
        int _auto_send_start = 0;               // 初回インターバル
        int _auto_send_interval = 0;            // インターバル
        int _auto_response = 0;                 // 自動応答
        int _ack_not_display = 0;               // 肯定応答非表示
        int _scroll_on  = 0;                    // スクロール表示
        int[] _ack_chk = new int[4];            // 肯定応答自動送信
        int[] _health_interval = new int[4];    // ヘルスチェックインターバル
        int[] _connect_chk = new int[4];        // 自動接続

        public bool AutoSend_is_on => (_auto_send == 1);
        public int AutoSendStartInterval => _auto_send_start;
        public int AutoSendInterval => _auto_send_interval;
        public bool AutoResponse_is_on => (_auto_response == 1);
        public bool AckNotDisplay_is_on => (_ack_not_display == 1);
        public bool Scroll_is_on => (_scroll_on == 1);


        // 初期表示の送信先サーバ名
        string _InitRemortMachineName;
        public string InitRemortMachineName => _InitRemortMachineName;

        // 初期表示の自装置番号
        string _InitSelfMachineName;
        public string InitSelfMachineName => _InitSelfMachineName;


        protected int _max_datasize = 1024;
        public int Maxdatasize => _max_datasize;
        

        protected Dictionary<string, JsonNode> _remort_addr = new  Dictionary<string, JsonNode>();
        protected Dictionary<string, JsonNode> _self_addr = new Dictionary<string, JsonNode>();

        public override int ReadJson(string path)
        {
            string item_name = "初期化中";
            try
            {
                base.ReadJson(path);

                item_name = "max_datasize";
                _max_datasize = _json_root[item_name].GetValue<int>();

                item_name = "initdis";
                JsonNode initdis = _json_root["initdis"];

                item_name = "自動送信";
                _auto_send = initdis["自動送信"]["check"].GetValue<int>();
                _auto_send_start = initdis["自動送信"]["start"].GetValue<int>();
                _auto_send_interval = initdis["自動送信"]["interval"].GetValue<int>();
                item_name = "自動応答";
                _auto_response = initdis["自動応答"].GetValue<int>();
                item_name = "肯定応答非表示";
                _ack_not_display = initdis["肯定応答非表示"].GetValue<int>();
                item_name = "スクロール";
                _scroll_on = initdis["スクロール"].GetValue<int>();

                item_name = "自装置番号";
                if (_machine_no == null) _machine_no = initdis["自装置番号"].ToString();
                item_name = "監視制御局番号";
                if (_segment_no == null) _segment_no = initdis["監視制御局番号"].ToString();

                item_name = "肯定応答";
                _ack_chk[0] = initdis["１系"]["受信側"]["肯定応答"].GetValue<int>();
                _ack_chk[1] = 0;
                _ack_chk[2] = initdis["２系"]["受信側"]["肯定応答"].GetValue<int>();
                _ack_chk[3] = 0;

                item_name = "ヘルスチェック";
                _health_interval[0] = 0;
                _health_interval[1] = initdis["１系"]["送信側"]["ヘルスチェック"].GetValue<int>();
                _health_interval[2] = 0;
                _health_interval[3] = initdis["２系"]["送信側"]["ヘルスチェック"].GetValue<int>();

                item_name = "接続";
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
                throw new Exception($"JsonCommDef読み込み失敗'{item_name}' {path}({ex.Message})"); ;
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
