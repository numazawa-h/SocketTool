﻿using SocketTool.CommData;
using SocketTool.Config;
using SocketTool.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SocketTool.Properties.SocketBase;

namespace SocketTool.CommForm
{
    public partial class CommForm : UserControl
    {
        CommData_Header commHeader = null;
        ServerSocket recv_socket = null;
        ClientSocket send_socket = null;

        SocketSendRecv accept_socket = null;
        SocketSendRecv connect_socket = null;

        Color _back_color;

        int _rescop_no = 0;
        public int RESCOP_NO
        {
            get
            {
                return _rescop_no;
            }
        }


        public CommForm()
        {
            InitializeComponent();
            _back_color = this.BackColor;
        }


        private void CommForm_Load(object sender, EventArgs e)
        {
        }

        public void Init(int rescop_no)
        {
            this._rescop_no = rescop_no;
            switch (this.RESCOP_NO)
            {
                case 1: this.grp_Comm.Text = "１系"; break;
                case 2: this.grp_Comm.Text = "２系"; break;
                default:
                    this.grp_Comm.Text = "？？？系"; break;
            }

            this.cbx_Remorte_Machine.Items.Clear();
            foreach (string name in JsonCommDef.GetInstance().GetRemoteMachineList())
            {
                cbx_Remorte_Machine.Items.Add(name);
            }

            this.cbx_Self_Machine.Items.Clear();
            foreach (string name in JsonCommDef.GetInstance().GetSelfMachineList())
            {
                cbx_Self_Machine.Items.Add(name);
            }

            // ヘッダ情報セットアップ
            commHeader = new CommData_Header();
            int head_len = commHeader.commMessageDefine.Length;
            int datalen_ofs = commHeader.commMessageDefine.GetFldOffset("dlen");
            int datalen_bytes = commHeader.commMessageDefine.GetFldLength("dlen");

            // 受信ソケットセットアップ
            recv_socket = new ServerSocket(head_len, datalen_ofs, datalen_bytes);
            recv_socket.OnExceptionEvent += OnExceptionHandler;
            recv_socket.OnSendData += OnSendDatahandler;
            recv_socket.OnRecvData += OnRecvDatahandler;
            recv_socket.OnFailListenEvent += OnFailListenHandler;
            recv_socket.OnAcceptEvent += OnAcceptEventHandler;
            recv_socket.OnDisConnectEvent += OnDisConnectEventHandler;

            // 送信ソケットセットアップ
            send_socket = new ClientSocket(head_len, datalen_ofs, datalen_bytes);
            send_socket.OnExceptionEvent += OnExceptionHandler;
            send_socket.OnSendData += OnSendDatahandler;
            send_socket.OnRecvData += OnRecvDatahandler;
            send_socket.OnFailConnectEvent += OnFaiConnectHandler;
            send_socket.OnConnectEvent += OnConnectEventHandler;
            send_socket.OnDisConnectEvent += OnDisConnectEventHandler;
        }

        public void SendData(string dtype, byte[] data)
        {
            if (connect_socket != null)
            {
                commHeader.SetOnSend(dtype);
                connect_socket.Send(commHeader.GetData(), data);
            }
        }
        public void SendData(CommData_Data comm_data)
        {
            if (connect_socket != null)
            {
                commHeader.SetOnSend(comm_data.DataType);
                connect_socket.Send(commHeader.GetData(), comm_data.GetData());
            }
        }


        private void OnSendDatahandler(object sender, CommDataEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CommDataHandler(OnSendDatahandler), new object[] { sender, args });
                return;
            }
            DisplaySendRecvData(args.HeadBuff, args.DataBuff, 1);
        }

        private void OnRecvDatahandler(object sender, CommDataEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CommDataHandler(OnRecvDatahandler), new object[] { sender, args });
                return;
            }
            CommData_Header header = new CommData_Header(args.HeadBuff);
            string dtype =header.GetFldValue("dtype").GetAsBcd();
            CommData_Data data = new CommData_Data(dtype, args.DataBuff);

            if (dtype == "0202")
            {
                if(data.GetDataDiscription("mode-active") == "アクティブ")
                {
                    Form1 form = (Form1)this.ParentForm;
                    form.SetCommActive(_rescop_no);
                }
            }

            DisplaySendRecvData(args.HeadBuff, args.DataBuff, 0);
        }


        private void DisplaySendRecvData(byte[] head, byte[] data, int direction )
        {
            StringBuilder sb = new StringBuilder();
            int cnt = 0;
            sb.Append("[");
            foreach (byte b in head)
            {
                if(cnt > 0 && (cnt % 8) == 0)
                {
                    sb.Append(" ");
                }
                if (cnt > 0 && (cnt % 4) == 0)
                {
                    sb.Append(" ");
                }
                if (cnt > 0 && (cnt % 16) == 0)
                {
                    sb.Append("\r\n ");
                }
                sb.Append($"{b:X2}");
                ++cnt;
            }
            sb.Append("]\r\n");
            cnt = 0;
            sb.Append("[");
            foreach (byte b in data)
            {
                if (cnt > 0 && (cnt % 8) == 0)
                {
                    sb.Append(" ");
                }
                if (cnt > 0 && (cnt % 4) == 0)
                {
                    sb.Append(" ");
                }
                if (cnt > 0 && (cnt % 16) == 0)
                {
                    sb.Append("\r\n ");
                }
                sb.Append($"{b:X2}");
                ++cnt;
            }
            sb.Append("]\r\n");

            rtx_MsgList.SelectionStart = rtx_MsgList.Text.Length;
            rtx_MsgList.SelectionLength = 0;
            if(direction == 0)
            {
                rtx_MsgList.SelectionBackColor = Color.White;
            }
            else
            {
                rtx_MsgList.SelectionBackColor = Color.Cyan;
            }
            rtx_MsgList.Focus();
            this.rtx_MsgList.AppendText( sb.ToString());
        }


        private void cbx_Remorte_Machine_SelectedIndexChanged(object sender, EventArgs e)
        {
            string addr = JsonCommDef.GetInstance().GetRemoteIp(cbx_Remorte_Machine.Text, this.RESCOP_NO);
            string port = JsonCommDef.GetInstance().GetRemotePort(cbx_Remorte_Machine.Text, this.RESCOP_NO);

            this.txt_Remote_IpAddress.Text = addr;
            this.txt_Remote_PortNo.Text = port;

            string dst = JsonCommDef.GetInstance().GetRemoteMachineCode(cbx_Remorte_Machine.Text, this.RESCOP_NO);
            commHeader.SetDstMachineCode(dst);
        }

        private void cbx_Self_Machine_SelectedIndexChanged(object sender, EventArgs e)
        {
            string addr = JsonCommDef.GetInstance().GetSelfIp(cbx_Self_Machine.Text);
            string port = JsonCommDef.GetInstance().GetSelfPort(cbx_Self_Machine.Text, this.RESCOP_NO);

            this.txt_Self_IpAddress.Text = addr;
            this.txt_Self_PortNo.Text = port;

            string src = JsonCommDef.GetInstance().GetSelfMachineCode(cbx_Self_Machine.Text);
            commHeader.SetSrcMachineCode(src);
        }

        private void chk_Self_AutoConnect_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_Self_AutoConnect.Checked)
            {
                if (accept_socket == null)
                {
                    lbl_Self_Status.Text = "接続待ち...";
                    lbl_Self_Status.ForeColor = Color.DeepPink;
                    recv_socket.Listen(txt_Self_IpAddress.Text, txt_Self_PortNo.Text);
                }
            }
            else
            {
                accept_socket?.Stop();
                accept_socket = null;
                    lbl_Self_Status.Text = "切断";
                    lbl_Self_Status.ForeColor = Color.Black;
                    Application.DoEvents();
                recv_socket.StopListen();
            }
        }

        private void chk_Remote_AutoConnect_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_Remote_AutoConnect.Checked)
            {
                this.lbl_Remote_Status.Text = "接続中...";
                lbl_Remote_Status.ForeColor = Color.DeepPink;
                Application.DoEvents();
                send_socket.Connect(txt_Remote_IpAddress.Text, txt_Remote_PortNo.Text);
            }
            else
            {
                lbl_Remote_Status.Text = "切断";
                lbl_Remote_Status.ForeColor = Color.Black;
                Application.DoEvents();
                connect_socket?.Stop();
                connect_socket = null;
            }
        }


        private void OnExceptionHandler(object sender, ThreadExceptionEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ThreadExceptionEventHandler(OnExceptionHandler), new object[] { sender, args });
                return;
            }
            MessageBox.Show(args.Exception.Message);
        }

        private void OnFailListenHandler(object sender, EventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler(OnFailListenHandler), new object[] { sender, args });
                return;
            }

            this.chk_Self_AutoConnect.Checked = false;
        }

        private async void OnFaiConnectHandler(object sender, EventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler(OnFaiConnectHandler), new object[] { sender, args });
                return;
            }
            this.lbl_Remote_Status.Text = "切断";
            this.lbl_Remote_Status.ForeColor = Color.Black;
            connect_socket = null;
            if (chk_Remote_AutoConnect.Checked)
            {
                await Task.Delay(10000);
                lbl_Remote_Status.Text = "接続中...";
                lbl_Remote_Status.ForeColor = Color.DeepPink;
                Application.DoEvents();
                send_socket.Connect(txt_Remote_IpAddress.Text, txt_Remote_PortNo.Text);
            }
        }

        private void OnAcceptEventHandler(object sender, ConnectEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ConnectEventHandler(OnAcceptEventHandler), new object[] { sender, args });
                return;
            }

            accept_socket = args.Socket;
            this.lbl_Self_Status.Text = "接続";
            this.lbl_Self_Status.ForeColor = Color.Red;
        }

        private void OnConnectEventHandler(object sender, ConnectEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ConnectEventHandler(OnConnectEventHandler), new object[] { sender, args });
                return;
            }

            connect_socket = args.Socket;
            lbl_Remote_Status.Text = "接続";
            lbl_Remote_Status.ForeColor = Color.Red;
        }

        private void OnDisConnectEventHandler(object sender, ConnectEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ConnectEventHandler(OnDisConnectEventHandler), new object[] { sender, args });
                return;
            }
            if (args.Socket.isServerSocket())
            {
                lbl_Self_Status.Text = "切断";
                lbl_Self_Status.ForeColor = Color.Black;
                accept_socket = null;
                if (chk_Self_AutoConnect.Checked)
                {
                    lbl_Self_Status.Text = "接続待ち...";
                    lbl_Self_Status.ForeColor = Color.DeepPink;
                    Application.DoEvents();
                    recv_socket.Listen(txt_Self_IpAddress.Text, txt_Self_PortNo.Text);
                }
            }
            else
            {
                this.BackColor = _back_color;
                lbl_Remote_Status.Text = "切断";
                lbl_Remote_Status.ForeColor = Color.Black;
                connect_socket = null;
                if (chk_Remote_AutoConnect.Checked)
                {
                    lbl_Remote_Status.Text = "接続中...";
                    lbl_Remote_Status.ForeColor = Color.DeepPink;
                    Application.DoEvents();
                    send_socket.Connect(txt_Remote_IpAddress.Text, txt_Remote_PortNo.Text);
                }
            }
        }
    }
}

