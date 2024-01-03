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
        JsonCommDef _comm_def = JsonCommDef.GetInstance();

        ServerSocket recv_socket = new ServerSocket(4, 0, 1);
        ClientSocket send_socket = new ClientSocket(4, 0, 1);

        SocketReadWrite accept_socket = null;
        SocketReadWrite connect_socket = null;


        int _RESCOP_NO;
        public int RESCOP_NO
        {
            get
            {
                return _RESCOP_NO;
            }
            set
            {
                _RESCOP_NO = value;
            }
        }


        public CommForm()
        {
            InitializeComponent();

            recv_socket.OnExceptionEvent += OnExceptionHandler;
            recv_socket.OnRecvData += OnRecvDatahandler;
            recv_socket.OnFailListenEvent += OnFailListenHandler;
            recv_socket.OnAcceptEvent += OnAcceptEventHandler;
            recv_socket.OnDisConnectEvent += OnDisConnectEventHandler;

            send_socket.OnExceptionEvent += OnExceptionHandler;
            send_socket.OnRecvData += OnRecvDatahandler;
            send_socket.OnFailConnectEvent += OnFaiConnectHandler;
            send_socket.OnConnectEvent += OnConnectEventHandler;
            send_socket.OnDisConnectEvent += OnDisConnectEventHandler;
        }


        private void CommForm_Load(object sender, EventArgs e)
        {
            switch (this.RESCOP_NO)
            {
                case 1: this.grp_Comm.Text = "１系"; break;
                case 2: this.grp_Comm.Text = "２系"; break;
                default:
                    this.grp_Comm.Text = "？？？系"; break;
            }
        }

        public void Init()
        {
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

        }

        public void SendData(byte[] head, byte[] data)
        {
            connect_socket?.Send(head, data);
        }

        private void OnRecvDatahandler(object sender, RecvDataEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new RecvDataHandler(OnRecvDatahandler), new object[] { sender, args });
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (byte b in args.HeadBuff)
            {
                sb.Append($"{b:X2}");
            }
            sb.Append(']'); ;
            sb.Append("[");
            foreach (byte b in args.DataBuff)
            {
                sb.Append($"{b:X2}");
            }
            sb.Append(']'); ;
            sb.Append("\n");

            this.rtx_MsgList.Text += sb.ToString();
        }


        private void cbx_Remorte_Machine_SelectedIndexChanged(object sender, EventArgs e)
        {
            string addr = _comm_def.GetRemoteIp(cbx_Remorte_Machine.Text, this.RESCOP_NO);
            string port = _comm_def.GetRemotePort(cbx_Remorte_Machine.Text, this.RESCOP_NO);

            this.txt_Remote_IpAddress.Text = addr;
            this.txt_Remote_PortNo.Text = port;
        }

        private void cbx_Self_Machine_SelectedIndexChanged(object sender, EventArgs e)
        {
            string addr = _comm_def.GetSelfIp(cbx_Self_Machine.Text);
            string port = _comm_def.GetSelfPort(cbx_Self_Machine.Text, this.RESCOP_NO);

            this.txt_Self_IpAddress.Text = addr;
            this.txt_Self_PortNo.Text = port;
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

