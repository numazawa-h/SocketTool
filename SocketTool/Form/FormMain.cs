using SocketTool.CommData;
using SocketTool.CommForm;
using SocketTool.Config;
using SocketTool.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SocketTool.Properties.SocketBase;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace SocketTool
{
    public partial class FormMain : Form
    {
        Color back_color;       // アクティブ側の色を戻す為に保存しておく

        private int active_rescop_no = 0;   // アクティブ側の系番号


        public event EventHandler OnInitEvent;
        public event EventHandler OnActivChangeEvent;
        public event EventHandler OnAutoSendEvent;
        public delegate void RecvEventHandler(Object sender, RecvEventArgs args);
        public event RecvEventHandler OnRecvEvent;


        public FormMain()
        {
            InitializeComponent();
            back_color = this.BackColor;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            this.commForm1.Init(1, this);
            this.commForm2.Init(2, this);

            this.chk_auto_send.Checked = JsonCommDef.GetInstance().AutoSend_is_on;
            this.txt_auto_send_startInterval.Text = JsonCommDef.GetInstance().AutoSendStartInterval.ToString().Trim();
            this.txt_auto_send_interval.Text = JsonCommDef.GetInstance().AutoSendInterval.ToString().Trim();
            chk_auto_response.Checked = JsonCommDef.GetInstance().AutoResponse_is_on;
            chk_Ack_Not_Display.Checked = JsonCommDef.GetInstance().AckNotDisplay_is_on;
            chk_Scroll.Checked = JsonCommDef.GetInstance().Scroll_is_on;

            this.cbx_Remort_Machine.Items.Clear();
            foreach (string name in JsonCommDef.GetInstance().GetRemoteMachineList())
            {
                cbx_Remort_Machine.Items.Add(name);
            }
            cbx_Remort_Machine.Text = JsonCommDef.GetInstance().InitRemortMachineName;

            this.cbx_Self_Machine.Items.Clear();
            foreach (string name in JsonCommDef.GetInstance().GetSelfMachineList())
            {
                cbx_Self_Machine.Items.Add(name);
            }
            cbx_Self_Machine.Text = JsonCommDef.GetInstance().InitSelfMachineName;

            // シナリオセットアップ
            ScenarioDef.GetInstance().OnInit(this);
            OnInitEvent?.Invoke(this, EventArgs.Empty);



        }


        public void SetCarNo(int carno)
        {
            // TODO
        }

        private void OnException(Exception ex)
        {
            Log.Warn(ex.ToString());
            MessageBox.Show(ex.Message);
        }

        public void OnRecvConnect()
        {
            cbx_Remort_Machine.Enabled = false;

        }

        public void OnRecv(string dtype, int rescop_no)
        {
            RecvEventArgs args = new RecvEventArgs(dtype, rescop_no);
            OnRecvEvent?.Invoke(this, args);

        }

        /// <summary>
        /// 系切替通知(アクティブ)を受信した時の処理
        /// </summary>
        /// <param name="rescop_no">系( 1..１系アクティブ、2..２系アクティブ、0..両系スタンバイ)</param>
        public void OnActiveReceived(int rescop_no)
        {
            this.active_rescop_no = 0;

            if (rescop_no == 1)
            {
                commForm1.BackColor = Color.MistyRose;
                commForm2.BackColor = back_color;
                active_rescop_no = rescop_no;

                OnActivChangeEvent?.Invoke(this, EventArgs.Empty);

            }
            if (rescop_no == 2)
            {
                commForm1.BackColor = back_color;
                commForm2.BackColor = Color.MistyRose;
                active_rescop_no = rescop_no;

                OnActivChangeEvent?.Invoke(this, EventArgs.Empty);
            }
        }


        /// <summary>
        /// 系切替通知(アクティブ)を受信した時の処理
        /// </summary>
        /// <param name="rescop_no">系( 1..１系アクティブ、2..２系アクティブ、0..両系スタンバイ)</param>
        public void OnDisconnect(int rescop_no)
        {
            if(rescop_no == this.active_rescop_no)
            {
                active_rescop_no =0;
                if (rescop_no == 1)
                {
                    commForm1.BackColor = back_color;
                }
                if (rescop_no == 2)
                {
                    commForm2.BackColor = back_color;
                }
            }
        }


        public void Send(CommData_Data comm_data, int rescop_no = 0)
        {
            Send(comm_data.DType, comm_data.GetData(), rescop_no);
        }

        /// <summary>
        /// 通常の送信
        /// </summary>
        /// <param name="dtype">データ種別</param>
        /// <param name="dat">データ(バイト配列)</param>
        public void Send(string dtype, byte[] dat, int rescop_no = 0)
        {
            if (rescop_no == 0)
            {
                if (this.active_rescop_no == 1)
                {
                    commForm1.SendData(dtype, dat);
                }
                if (this.active_rescop_no == 2)
                {
                    commForm2.SendData(dtype, dat);
                }
            }
            if (rescop_no == 1)
            {
                commForm1.SendData(dtype, dat);
            }
            if (rescop_no == 2)
            {
                commForm2.SendData(dtype, dat);
            }
        }

        /// <summary>
        /// ヘッダ指定送信
        /// </summary>
        /// <remarks>異常ヘッダを送信したい時などに使用する</remarks>
        /// <param name="hed">ヘッダ(バイト配列)</param>
        /// <param name="dat">データ(バイト配列)</param>
        public void Send(byte[]hed, byte[] dat, int rescop_no =0)
        {
            if (rescop_no == 0)
            {
                if (this.active_rescop_no == 1)
                {
                    commForm1.SendData(hed, dat);
                }
                if (this.active_rescop_no == 2)
                {
                    commForm2.SendData(hed, dat);
                }
            }
            if (rescop_no == 1 )
            {
                commForm1.SendData(hed, dat);
            }
            if (rescop_no == 2)
            {
                commForm1.SendData(hed, dat);
            }
        }


        private void chk_AutoSend_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_auto_send.Checked)
                {
                    int interval = -1;
                    if (int.TryParse(txt_auto_send_startInterval.Text, out interval))
                    {
                        this.AutoSend_timer.Tag = "start";
                        this.AutoSend_timer.Interval = interval;
                        this.AutoSend_timer.Enabled = true;
                        this.txt_auto_send_startInterval.Enabled = false;
                        this.txt_auto_send_interval.Enabled = false;
                    }
                }
                else
                {
                    this.AutoSend_timer.Enabled = false;
                    this.txt_auto_send_startInterval.Enabled = true;
                    this.txt_auto_send_interval.Enabled = true;
                }
            }
            catch (Exception)
            {
                chk_auto_send.Checked = false;
            }
        }
        private void AutoSend_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (chk_auto_send.Checked)
                {
                    if (this.AutoSend_timer.Tag != null)
                    {
                        int interval = int.Parse(txt_auto_send_interval.Text);
                        this.AutoSend_timer.Tag = null;
                        this.AutoSend_timer.Interval = interval;
                    }
                    OnAutoSendEvent?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    this.AutoSend_timer.Enabled = false;
                    this.txt_auto_send_startInterval.Enabled = true;
                    this.txt_auto_send_interval.Enabled = true;
                }
            }
            catch (Exception)
            {
                chk_auto_send.Checked = false;
            }
        }
        private void chk_Ack_Not_Display_CheckedChanged(object sender, EventArgs e)
        {
            commForm1.SetAckNotDisplay(chk_Ack_Not_Display.Checked);
            commForm2.SetAckNotDisplay(chk_Ack_Not_Display.Checked);
        }

        private void chk_Scroll_CheckedChanged(object sender, EventArgs e)
        {
            commForm1.SetScrollOn(chk_Scroll.Checked);
            commForm2.SetScrollOn(chk_Scroll.Checked);
        }


        private void cbx_Remort_Machine_SelectedIndexChanged(object sender, EventArgs e)
        {
            string addr1 = JsonCommDef.GetInstance().GetRemoteIp(cbx_Remort_Machine.Text, 1);
            string port1 = JsonCommDef.GetInstance().GetRemotePort(cbx_Remort_Machine.Text, 1);
            string addr2 = JsonCommDef.GetInstance().GetRemoteIp(cbx_Remort_Machine.Text, 2);
            string port2 = JsonCommDef.GetInstance().GetRemotePort(cbx_Remort_Machine.Text, 2);
            string dst1 = JsonCommDef.GetInstance().GetRemoteMachineCode(cbx_Remort_Machine.Text, 1);
            string dst2 = JsonCommDef.GetInstance().GetRemoteMachineCode(cbx_Remort_Machine.Text, 2);

            int ret1 = this.commForm1.OnRemortMachineChange(addr1, port1, dst1);
            int ret2 = this.commForm2.OnRemortMachineChange(addr2, port2, dst2);
            if (ret1 == -1 || ret2 == -1)
            {
                OnException(new Exception("接続中に対象装置を変更することはできません"));
            }
        }

        private void cbx_Self_Machine_SelectedIndexChanged(object sender, EventArgs e)
        {
            string addr = JsonCommDef.GetInstance().GetSelfIp(cbx_Self_Machine.Text);
            string port1 = JsonCommDef.GetInstance().GetSelfPort(cbx_Self_Machine.Text, 1);
            string port2 = JsonCommDef.GetInstance().GetSelfPort(cbx_Self_Machine.Text, 2);
            string src = JsonCommDef.GetInstance().GetSelfMachineCode(cbx_Self_Machine.Text);

            int ret1 = this.commForm1.OnSelfMachineChange(addr, port1, src);
            int ret2 = this.commForm2.OnSelfMachineChange(addr, port2, src);
            if (ret1 == -1 || ret2 == -1)
            {
                OnException(new Exception("接続中に対象装置を変更することはできません"));
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            byte[] head = new byte[] { 10, 0, 0, 0 };
            byte[] data = new byte[] { 0x20, 0x23, 0x12, 0x31 };


            CommData.CommData_Base.FldValue val = new CommData.CommData_Base.FldValue(data);
            int i = val.GetAsInt();
            long lng = val.GetAsLong();
            string bcd = val.GetAsBcd();
            byte[] bytes = val.GetAsByte();
            DateTime dt = val.GetAsDateTimeBcd();

            val.SetAsByte(new byte[] { 0x31, 0x32, 041, 0x42 });
            string str = val.GetAsStringAsc();
            val.SetAsInt(539169329);
            str = val.GetAsBcd();
            val.SetAsLong(539169329);
            str = val.GetAsBcd();
            val.SetAsDateTimeBcd(DateTime.Now);
            str = val.GetAsBcd();
            val.SetAsStringAsc("12AB");
            str = val.GetAsBcd();
            tabControl.SelectedIndex = 0;
            Application.DoEvents();

            commForm1.SendData(CommData_Data.DTYPE_STA, new byte[48]);

            CommData_Data msg0202 = new CommData_Data(CommData_Data.DTYPE_ACT_CHANGE);
            msg0202.GetFldValue("active-change").SetAsInt(1);
            string fldname = JsonDataDef.GetInstance().GetMessageDefine(CommData_Data.DTYPE_ACT_CHANGE).GetFldName("active-change");
            commForm1.SendData(msg0202);
            Application.DoEvents();

            CommData_Data msg0501 = new CommData_Data(CommData_Data.DTYPE_NP);
            msg0501.GetFldValue("carno").SetAsInt(123);
            msg0501.LoadImage("image", "test");
            commForm1.SendData(msg0501);

            CommData_Data msg0301= new CommData_Data(CommData_Data.DTYPE_PASSCAR);
            msg0301.GetFldValue("carno").SetAsInt(1);
            Send(msg0301);

            CommData_Data msg0302 = new CommData_Data(CommData_Data.DTYPE_LANE_ST);
            msg0302.GetFldValue("carno_01").SetAsInt(1);
            msg0302.GetFldValue("carno_02").SetAsInt(2);
            msg0302.GetFldValue("carno_03").SetAsInt(3);
            Send(msg0302);
        }


        private void txt_Numric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }


        public void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // ドラッグ中のファイルやディレクトリの取得
                string[] drags = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string d in drags)
                {
                    if (!System.IO.File.Exists(d))
                    {
                        // ファイル以外であればイベント・ハンドラを抜ける
                        return;
                    }
                }
                e.Effect = DragDropEffects.Copy;
            }
        }

        public void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach( string path in files)
            {
                string dtype = null;
                byte[] hed = System.Array.Empty<byte>();
                byte[] dat = System.Array.Empty<byte>();
                string img_fname = null; 
                string fname = System.IO.Path.GetFileName(path);
                if (fname.Substring(4, 1) == "_")
                {
                    try
                    {
                        dtype = fname.Substring(0, 4);
                        string file_ext = Path.GetExtension(path);
                        if (file_ext == ".bin")
                        {
                            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                            {
                                dat = new byte[fs.Length];
                                fs.Read(dat, 0, dat.Length);
                            }
                        }
                        if (file_ext == ".txt")
                        {
                            foreach (string lin in File.ReadLines(path))
                            {
                                var matchs = Regex.Matches(lin, @"\[[0-9,a-f,A-F ]*\]");
                                if (matchs.Count >0)
                                {
                                    dat = parse_bcd(matchs[0].Value);
                                }
                                matchs = Regex.Matches(lin, @"\[[0-9,a-z,A-Z \-_]*\]");
                                if (matchs.Count > 1)
                                {
                                    switch (dtype)
                                    {
                                        case CommData_Data.DTYPE_NP:
                                            img_fname = matchs[1].Value.Trim();
                                            img_fname = img_fname.Replace("[", string.Empty);
                                            img_fname = img_fname.Replace(" ", string.Empty);
                                            img_fname = img_fname.Replace("]", string.Empty);
                                            CommData_Data msg0501 = new CommData_Data(CommData_Data.DTYPE_NP, dat);
                                            msg0501.LoadImage("image", img_fname);
                                            dat = msg0501.GetData();
                                            break;
                                    }

                                }
                                break;      // 最初の一行だけ処理する
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.OnException(new Exception($"ドラッグドロップ'{path}'読み込みで例外" + ex.Message));
                    }
                }
                else
                {
                    try
                    {
                        string file_ext = Path.GetExtension(path);
                        if (file_ext == ".txt")
                        {
                            foreach (string lin in File.ReadLines(path))
                            {
                                var matchs = Regex.Matches(lin, @"\[[0-9,a-f,A-F ]*\]");
                                if (matchs.Count > 1)
                                {
                                    hed = parse_bcd(matchs[0].Value);
                                    dat = parse_bcd(matchs[1].Value);
                                }
                            }
                        }
                        else
                        {
                            ScenarioDef.GetInstance().Run(this, path);
                        }
                    }
                    catch (Exception ex)
                    {
                        this.OnException(new Exception($"ドラッグドロップ'{path}'読み込みで例外" + ex.Message));
                    }
                }
                if (dtype != null)
                {
                     this.Send(dtype, dat);
                }
                if( hed.Length >0 && dat.Length > 0 )
                {
                    this.Send(hed, dat);
                }

            }

        }
        private byte[] parse_bcd(string bcd)
        {
            bcd = bcd.Replace("[", string.Empty);
            bcd = bcd.Replace(" ", string.Empty);
            bcd = bcd.Replace("]", string.Empty);

            if ((bcd.Length % 2) == 1)
            {
                return System.Array.Empty<byte>();
            }

            int byte_size = bcd.Length / 2;
            byte[] buf = new byte[byte_size];

            int buf_idx = 0;
            for (int idx = 0; idx < bcd.Length; idx += 2)
            {
                string w = bcd.Substring(idx, 2);
                buf[buf_idx] = Convert.ToByte(w, 16);
                buf_idx++;
            }

            return buf;
        }

    }


}
