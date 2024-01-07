using System;
using System.Collections.Generic;
using System.IO;

namespace SocketTool.Config
{
    /// <summary>
    /// シナリオ設定クラス
    /// </summary>
    public class ScenarioDef
    {
        // シングルトン
        static private ScenarioDef _instance = null;
        static public ScenarioDef GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ScenarioDef();
            }
            return _instance;
        }
        private ScenarioDef() 
        {
        }



        protected List<OnRecvCmd> _on_recv_list = new List<OnRecvCmd>();

        CommandSend cmd;



        public void ReadCsvFile(string path)
        {
            _on_recv_list.Clear();

            foreach (string lin in File.ReadLines(path))
            {
                ScenarioRecord rec = new ScenarioRecord(lin);
                switch (rec.When)
                {
                    case "OnRecv":
                        _on_recv_list.Add(new OnRecvCmd(rec));
                        break;
                    case "OnActive":
                        break;
                    case "OnTimer":
                        break;
                }
            }
        }

        /// <summary>
        /// すべてコマンドをすぐ実行する
        /// </summary>
        /// <param name="form"></param>
        /// <param name="path"></param>
        public void Run(FormMain form, string path)
        {
            foreach (string lin in File.ReadLines(path))
            {
                ScenarioRecord rec = new ScenarioRecord(lin);
                switch (rec.When)
                {
                    case "OnInit":
                        new CommandInit(rec.Cmd).Exec(form);
                        break;
                    default:
                        new CommandSend(rec.Cmd).Exec(form);
                        break;
                }
            }
        }


        protected class OnRecvCmd 
        { 
            CommandSend _cmd;
            string _cond;
            int _skip;
            int _times;

            public OnRecvCmd(ScenarioRecord snr)
            {
                _cmd = new CommandSend(snr.Cmd);
                _cond = snr.Cond;
                _skip = snr.Skip;
                _times = snr.Times;
            }
        }


        protected class ScenarioRecord
        {
            public string When { get; }
            public string Cond { get; }
            public int Skip { get; }
            public int Times { get; }
            public string Cmd { get;  }

            public ScenarioRecord(string lin)
            {
                string[] flds = lin.Split('\t');
                When = flds[0];
                Cond = flds[1];
                try
                {
                    Skip = int.Parse(flds[2]);
                    Times = int.Parse(flds[3]);
                }
                catch (Exception ex)
                {
                    Skip = 0;
                    Times = 0;
                }
                Cmd = flds[4];
            }
        }
    }
}
