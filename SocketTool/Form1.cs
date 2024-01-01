using SocketTool.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SocketTool.Properties.SocketBase;

namespace SocketTool
{
    public partial class Form1 : Form
    {

        JsonCommDef _comm_def = new JsonCommDef();

        ServerSocket recv_socket = new ServerSocket(4, 0, 1);
        ClientSocket send_socket = new ClientSocket(4, 0, 1);


        SocketReadWrite accept_socket = null;
        SocketReadWrite connect_socket = null;

        public Form1()
        {
            InitializeComponent();


            recv_socket.OnExceptionEvent += OnExceptionHandler;
            recv_socket.OnRecvData += OnRecvDatahandler;
            recv_socket.OnAcceptEvent += OnAcceptEventHandler;
            recv_socket.OnFailListenEvent += OnFailListenHandler;
            recv_socket.OnDisConnectEvent += OnDisConnectEventHandler;

            send_socket.OnExceptionEvent += OnExceptionHandler;
            send_socket.OnRecvData += OnRecvDatahandler;
            send_socket.OnConnectEvent += OnConnectEventHandler;
            send_socket.OnFailConnectEvent += OnFaiConnectHandler;
            send_socket.OnDisConnectEvent += OnDisConnectEventHandler;

            _comm_def.OnExceptionEvent += OnExceptionHandler;
            string wcd = System.AppDomain.CurrentDomain.BaseDirectory;
            _comm_def.ReadJson(wcd + "Config\\CommDef.json");

            cbxRemort1.Items.Clear();
            foreach ( string name in _comm_def.GetRemoteList())
            {
                cbxRemort1.Items.Add(name);
            }

            cbxSelf1.Items.Clear();
            foreach (string name in _comm_def.GetSelfList())
            {
                cbxSelf1.Items.Add(name);
            }
        }

        private void OnExceptionHandler(object sender, ThreadExceptionEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ThreadExceptionEventHandler(OnExceptionHandler), new object[] { sender,args });
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
            
            chkListen1.Checked = false;
        }

        private async void OnFaiConnectHandler(object sender, EventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler(OnFaiConnectHandler), new object[] { sender, args });
                return;
            }
            lbl_Connect.Text = "切断";
            lbl_Connect.ForeColor = Color.Black;
            connect_socket = null;
            if (chkConnect1.Checked)
            {
                await Task.Delay(10000);
                lbl_Connect.Text = "接続中...";
                lbl_Connect.ForeColor = Color.DeepPink;
                Application.DoEvents();
                send_socket.Connect(txtRemoteAddress1.Text, txtRemotePort1.Text);
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
            lbl_Accept.Text = "接続";
            lbl_Accept.ForeColor = Color.Red;
        }

        private void OnConnectEventHandler(object sender, ConnectEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ConnectEventHandler(OnConnectEventHandler), new object[] { sender, args });
                return;
            }

            connect_socket = args.Socket;
            lbl_Connect.Text = "接続";
            lbl_Connect.ForeColor = Color.Red;
        }

        private void OnDisConnectEventHandler(object sender, ConnectEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ConnectEventHandler(OnDisConnectEventHandler), new object[] { sender, args });
                return;
            }
            if(args.Socket.isServerSocket())
            {
                lbl_Accept.Text = "切断";
                lbl_Accept.ForeColor = Color.Black;
                accept_socket = null;
                if (chkListen1.Checked)
                {
                    lbl_Accept.Text = "接続待ち...";
                    lbl_Accept.ForeColor = Color.DeepPink;
                    recv_socket.Listen(txtSelfAddress1.Text, txtSelfPort1.Text);
                }
            }
            else
            {
                lbl_Connect.Text = "切断";
                lbl_Connect.ForeColor = Color.Black;
                connect_socket = null;
                if (chkConnect1.Checked)
                {
                    lbl_Connect.Text = "接続中...";
                    lbl_Connect.ForeColor = Color.DeepPink;
                    Application.DoEvents();
                    send_socket.Connect(txtRemoteAddress1.Text, txtRemotePort1.Text);
                }
            }
        }

        private void OnRecvDatahandler(object sender, RecvDataEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new RecvDataHandler(OnRecvDatahandler), new object[] { sender, args });
                return;
            }
            StringBuilder sb  = new StringBuilder();
            sb.Append("[");
            foreach(byte b in args.HeadBuff)
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

            this.rtxMsgList.Text += sb.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] head = new byte[] { 10, 0, 0, 0 };
            byte[] data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            connect_socket?.Send(head, data);
        }

        private void cbxRemort1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string addr = _comm_def.GetRemoteIp(cbxRemort1.Text, 1);
            string port = _comm_def.GetRemotePort(cbxRemort1.Text, 1);

            this.txtRemoteAddress1.Text = addr;
            this.txtRemotePort1.Text = port;
        }

        private void cbxSelf1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string addr = _comm_def.GetSelfIp(cbxSelf1.Text);
            string port = _comm_def.GetSelfPort(cbxSelf1.Text, 1);

            this.txtSelfAddress1.Text = addr;
            this.txtSelfPort1.Text = port;
        }

        private void chkListen1_CheckedChanged(object sender, EventArgs e)
        {
            if(chkListen1.Checked)
            {
                if (accept_socket == null)
                {
                    lbl_Accept.Text = "接続待ち...";
                    lbl_Accept.ForeColor = Color.DeepPink;
                    recv_socket.Listen(txtSelfAddress1.Text, txtSelfPort1.Text);
                }
            }
            else
            {

                if (accept_socket == null)
                {
                    lbl_Accept.Text = "切断";
                    lbl_Accept.ForeColor = Color.Black;
                    Application.DoEvents();
                }
                recv_socket.StopListen();
            }
        }

        private void chkConnect1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConnect1.Checked)
            {
                lbl_Connect.Text = "接続中...";
                lbl_Connect.ForeColor = Color.DeepPink;
                Application.DoEvents();
                send_socket.Connect(txtRemoteAddress1.Text, txtRemotePort1.Text);
            }
            else
            {
                lbl_Connect.Text = "切断";
                lbl_Connect.ForeColor = Color.Black;
                Application.DoEvents();
                connect_socket?.Stop();
                connect_socket = null;
            }
        }
    }

}
