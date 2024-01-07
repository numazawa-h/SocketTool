using SocketTool.CommData;
using SocketTool.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using static SocketTool.Properties.SocketBase;

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


        protected List<CommandInit> _on_init_list = new List<CommandInit>();
        protected List<CommandSend> _on_active_list = new List<CommandSend>();
        protected List<OnRecvCmd> _on_recv_list = new List<OnRecvCmd>();
        protected CommandTimer _commandTimer= null;

        protected int _auto_send_start_interval = 1000;
        public int AutoSendStartInterval => _auto_send_start_interval;
        protected int _auto_send_interval = 1000;
        public int AutoSendInterval => _auto_send_interval;

        public void ReadCsvFile(string path)
        {
            _on_recv_list.Clear();

            foreach (string lin in File.ReadLines(path))
            {
                ScenarioRecord rec = new ScenarioRecord(lin);
                switch (rec.When)
                {
                    case "OnInit":
                        _on_init_list.Add(new CommandInit(rec.Cmd));
                        break;
                    case "OnActive":
                        _on_active_list.Add(new CommandSend(rec.Cmd));
                        break;
                    case "OnRecv":
                        _on_recv_list.Add(new OnRecvCmd(rec));
                        break;
                    case "OnTimer":
                        _auto_send_start_interval = rec.Skip;
                        _auto_send_interval = rec.Times;
                        _commandTimer = new CommandTimer(rec.Cmd);
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

        public void  OnInit(FormMain form)
        {
            form.OnInitEvent += OnInitHandler;
            form.OnActivChangeEvent += OnActiveHandler;
            form.OnRecvEvent += OnRecvHandler;
            form.OnAutoSendEvent += OnTimerHandler;
        }

        private void OnInitHandler(object sender, EventArgs args)
        {
            foreach( Command cmd in _on_init_list)
            {
                cmd.Exec((FormMain)sender);
            }
        }
        private void OnTimerHandler(object sender, EventArgs args)
        {
            _commandTimer.Exec((FormMain)sender);
        }

        private void OnActiveHandler(object sender, EventArgs args)
        {
            foreach (Command cmd in _on_active_list)
            {
                cmd.Exec((FormMain)sender);
            }
        }
        private void OnRecvHandler(object sender, RecvEventArgs args)
        {
            // Ack Nackの受信は無視する
            if (args.DType == CommData_Data.DTYPE_Nak || args.DType == CommData_Data.DTYPE_Ack)
            {
                return;
            }

            foreach (OnRecvCmd cmd in _on_recv_list)
            {
                if (cmd.isDtypeTarget(args.DType))
                {
                    // Ack Nackは受信した系に対して送信する
                    if(cmd.Dtype == CommData_Data.DTYPE_Nak || cmd.Dtype == CommData_Data.DTYPE_Ack)
                    {
                        cmd.SetRescopNo(args.RescopNo);
                    }
                    cmd.Exec((FormMain)sender);
                }
            }
        }


        protected class OnRecvCmd 
        {
            public string Dtype => _dtype;
            CommandSend _cmd;
            string _dtype;
            int _skip;
            int _times;

            public OnRecvCmd(ScenarioRecord snr)
            {
                _cmd = new CommandSend(snr.Cmd);
                _dtype = snr.Cond;
                _skip = snr.Skip;
                _times = snr.Times;
            }

            public void SetRescopNo(int rescop_no)
            {
                _cmd.SetRescopNo(rescop_no);
            }

            public void Exec(FormMain form)
            {
                if(_times == 0)
                {
                    return;
                }
                if(_skip > 0)
                {
                    --_skip;
                    return;
                }

                _cmd.Exec(form);
                --_times;
            }

            public  bool isDtypeTarget(string dtype) 
            {
                bool ret = true;
                for(int i=0; i< 4; i++)
                {
                    string d1 = _dtype.Substring(i,1);
                    string d2 = dtype.Substring(i, 1);
                    if(d1 !="*" && d1 != d2)
                    {
                        ret = false;
                        break;
                    }
                }

                return ret;
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
                When = flds[0].Trim();
                Cond = flds[1].Trim();
                try
                {
                    Skip = int.Parse(flds[2].Trim());
                    Times = int.Parse(flds[3].Trim());
                }
                catch (Exception ex)
                {
                    Skip = 0;
                    Times = 0;
                }
                Cmd = flds[4].Trim();
            }
        }


    }

    public class RecvEventArgs : EventArgs
    {
        private string _dtype;
        private int _rescop_no;

        public string DType => _dtype;
        public int RescopNo => _rescop_no;

        public RecvEventArgs(string dtype, int rescop_no)
        {
            _dtype = dtype;
            _rescop_no = rescop_no;
        }
    }

}
