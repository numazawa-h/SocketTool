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
        ServerSocket recv_socket = new ServerSocket(4, 0, 1);
        ClientSocket send_socket = new ClientSocket(4, 0, 1);




        public Form1()
        {
            InitializeComponent();
            recv_socket.OnExceptionEvent += OnExceptionHandler;
            recv_socket.OnRecvData += OnRecvDatahandler;
            recv_socket.OnConnectEvent += OnConnectEventHandler;

            send_socket.OnExceptionEvent += OnExceptionHandler;
            send_socket.OnRecvData += OnRecvDatahandler;
            send_socket.OnConnectEvent += OnConnectEventHandler;

        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            recv_socket.Listen(txtSelfAddress.Text, txtSelfPort.Text);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            send_socket.Connect(txtRemoteAddress.Text, txtRemotePort.Text);
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
        
        private void OnConnectEventHandler(object sender, ConnectEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ConnectEventHandler(OnConnectEventHandler), new object[] { sender, args });
                return;
            }

            byte[] head = new byte[] { 10, 0, 0, 0 };
            byte[] data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            args.Socket.Send(head, data);
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

    }

}
