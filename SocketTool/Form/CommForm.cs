using SocketTool.CommData;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

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

            // 初期表示(受信側)
            this.chk_Self_Ack.Checked = Config.JsonCommDef.GetInstance().GetRecvAckChk(rescop_no);
            int interval1 = Config.JsonCommDef.GetInstance().GetRecvHealthInterval(rescop_no);
            this.chk_Self_AutoConnect.Checked = Config.JsonCommDef.GetInstance().GetRecvConnectChk(rescop_no);

            // 初期表示(送信側)
            this.chk_Remort_Ack.Checked = Config.JsonCommDef.GetInstance().GetSendAckChk(rescop_no);
            int interval2 = Config.JsonCommDef.GetInstance().GetSendHealthInterval(rescop_no);
            this.chk_Remort_Health.Checked = interval2 > 0;
            this.txt_Remort_Health_Interval.Text = interval2.ToString();
            this.chk_Remort_AutoConnect.Checked = Config.JsonCommDef.GetInstance().GetSendConnectChk(rescop_no);


        }

        public int OnSelfMachineChange(string iaddress, string portno, string mashine_code)
        {
            if(accept_socket !=null || connect_socket != null){
                return -1;
            }
            this.txt_Self_IpAddress.Text = iaddress;
            this.txt_Self_PortNo.Text = portno;
            this.commHeader.SetSrcMachineCode(mashine_code);

            return 0;
        }

        public int OnRemortMachineChange(string iaddress, string portno, string mashine_code)
        {
            if (accept_socket != null || connect_socket!= null){
                return -1;
            }
            this.txt_Remort_IpAddress.Text = iaddress;
            this.txt_Remort_PortNo.Text = portno;
            this.commHeader.SetDstMachineCode(mashine_code);

            return 0;
        }


        public void SendData(string dtype, byte[] data)
        {
            if (connect_socket != null)
            {
                commHeader.SetOnSend(dtype);
                connect_socket.Send(commHeader.GetData(), data);
            }
        }
        public void SendData(CommData.CommData_Data comm_data)
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
            CommData_Header header = new CommData_Header(args.HeadBuff);
            CommData_Data data = new CommData_Data(header.DataType, args.DataBuff);

            DisplaySendRecvData(header, data, 1);
        }

        private void OnRecvDatahandler(object sender, CommDataEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CommDataHandler(OnRecvDatahandler), new object[] { sender, args });
                return;
            }
            CommData_Header header = new CommData_Header(args.HeadBuff);
            CommData_Data data = new CommData_Data(header.DataType, args.DataBuff);
            if(data.isActiveMessage())
            {
                Form1 form = (Form1)this.ParentForm;
                form.OnActiveReceived(_rescop_no);
            }
 
            DisplaySendRecvData(header, data, 0);
        }


        private void DisplaySendRecvData(CommData_Header header, CommData_Data data, int direction )
        {

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

            
            this.rtx_MsgList.AppendText(GetCMessageDiscription(header, data, direction));
 //           this.rtx_MsgList.AppendText(dump_message(header, data ));
        }

        private string GetCMessageDiscription(CommData_Header header, CommData_Data data, int direction)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(adjust($"{header.RecvDateTime:yyyy/MM/dd HH:mm:ss}", 20));
            sb.Append(data.Name);
            if(data.DataType == CommData_Data.DTYPE_ActiveChange)
            {
                sb.Append("(");
                sb.Append(data.GetDataDiscription("mode-active"));
                sb.Append(")");
            }

            return adjust(sb.ToString(), 80)+"\r\n";
        }

        private string adjust(string msg, int byte_size)
        {
            int str_size = Encoding.GetEncoding("shift_jis").GetByteCount(msg);
            if(byte_size > str_size) 
            {
                msg += new string(' ', byte_size- str_size);
            }

            return msg;
        }

        private string dump_message(CommData_Header header, CommData_Data data)
        {
            byte[] hed = header.GetData();
            byte[] dat = data.GetData();

            StringBuilder sb = new StringBuilder();
            int cnt = 0;
            sb.Append("[");
            foreach (byte b in hed)
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
            cnt = 0;
            sb.Append("[");
            foreach (byte b in dat)
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
            
            return sb.ToString();
        }

        private async void chk_Self_AutoConnect_CheckedChanged(object sender, EventArgs e)
        {
            this.statusStrip.Items.Clear();
            if (this.chk_Self_AutoConnect.Checked)
            {
                if (accept_socket == null)
                {
                    await Task.Delay(500);

                    lbl_Self_Status.Text = "接続待ち...";
                    lbl_Self_Status.ForeColor = Color.DeepPink;
                    Application.DoEvents();

                    recv_socket.Listen(txt_Self_IpAddress.Text, txt_Self_PortNo.Text);
                }
            }
            else
            {
                lbl_Self_Status.Text = "切断";
                lbl_Self_Status.ForeColor = Color.Black;
                Application.DoEvents();

                recv_socket.StopListen();
                accept_socket?.Stop();
                accept_socket = null;
            }
        }

        private async void chk_Remote_AutoConnect_CheckedChanged(object sender, EventArgs e)
        {
            this.statusStrip.Items.Clear();
            if (this.chk_Remort_AutoConnect.Checked)
            {
                await Task.Delay(1000);

                this.lbl_Remote_Status.Text = "接続中...";
                lbl_Remote_Status.ForeColor = Color.DeepPink;
                Application.DoEvents();

                send_socket.Connect(txt_Remort_IpAddress.Text, txt_Remort_PortNo.Text);
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
            this.statusStrip.Items.Clear();
            if (args.Exception.Message.Length > 30)
            {
                this.statusStrip.Items.Add("！");
                this.statusStrip.Items[0].ForeColor = Color.Red;
                this.statusStrip.Items.Add(args.Exception.Message.Substring(0, Math.Min(50, args.Exception.Message.Length))+"..");
                this.statusStrip.Items[1].ForeColor = Color.Red;
            }
            else
            {
                this.statusStrip.Items.Add(args.Exception.Message);
                this.statusStrip.Items[0].ForeColor = Color.Red;
            }
            Application.DoEvents();
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
            Application.DoEvents();
            await Task.Delay(5000);
            connect_socket = null;
            if (chk_Remort_AutoConnect.Checked)
            {
                lbl_Remote_Status.Text = "接続中...";
                lbl_Remote_Status.ForeColor = Color.DeepPink;
                Application.DoEvents();
                await Task.Delay(500);
                send_socket.Connect(txt_Remort_IpAddress.Text, txt_Remort_PortNo.Text);
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

        private async void OnDisConnectEventHandler(object sender, ConnectEventArgs args)
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
                    await Task.Delay(10);
                    recv_socket.Listen(txt_Self_IpAddress.Text, txt_Self_PortNo.Text);
                }
            }
            else
            {
                this.BackColor = _back_color;
                lbl_Remote_Status.Text = "切断";
                lbl_Remote_Status.ForeColor = Color.Black;
                connect_socket = null;
                if (chk_Remort_AutoConnect.Checked)
                {
                    lbl_Remote_Status.Text = "接続中...";
                    lbl_Remote_Status.ForeColor = Color.DeepPink;
                    await Task.Delay(10);
                    send_socket.Connect(txt_Remort_IpAddress.Text, txt_Remort_PortNo.Text);
                }
            }
        }
    }
}

