using SocketTool.CommData;
using SocketTool.CommForm;
using SocketTool.Config;
using SocketTool.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SocketTool.Properties.SocketBase;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace SocketTool
{
    public partial class Form1 : Form
    {
        Color back_color;

        public Form1()
        {
            InitializeComponent();
            back_color = this.BackColor;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.commForm1.Init(1);
            this.commForm2.Init(2);

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
        }


        public void OnRecvConnect()
        {
            cbx_Remort_Machine.Enabled = false;

        }

        /// <summary>
        /// 系切替通知(アクティブ)を受信した時の処理
        /// </summary>
        /// <param name="rescop_no">受信した系( 1..１系、2..２系)</param>
        public void OnActiveReceived(int rescop_no)
        {
            if (rescop_no == 1) {
                commForm1.BackColor = Color.MistyRose;
                commForm2.BackColor = back_color;
            }
            if (rescop_no ==2)
            {
                commForm1.BackColor = back_color;
                commForm2.BackColor = Color.MistyRose;
            }
        }



        private void cbx_Remort_Machine_SelectedIndexChanged(object sender, EventArgs e)
        {
            string addr1 = JsonCommDef.GetInstance().GetRemoteIp(cbx_Remort_Machine.Text, 1);
            string port1 = JsonCommDef.GetInstance().GetRemotePort(cbx_Remort_Machine.Text, 1);
            string addr2 = JsonCommDef.GetInstance().GetRemoteIp(cbx_Remort_Machine.Text, 2);
            string port2 = JsonCommDef.GetInstance().GetRemotePort(cbx_Remort_Machine.Text, 2);
            string dst1 = JsonCommDef.GetInstance().GetRemoteMachineCode(cbx_Remort_Machine.Text, 1);
            string dst2 = JsonCommDef.GetInstance().GetRemoteMachineCode(cbx_Remort_Machine.Text, 2);

            this.commForm1.OnRemortMachineChange(addr1, port1, dst1);
            this.commForm2.OnRemortMachineChange(addr2, port2, dst2);
        }

        private void cbx_Self_Machine_SelectedIndexChanged(object sender, EventArgs e)
        {
            string addr = JsonCommDef.GetInstance().GetSelfIp(cbx_Self_Machine.Text);
            string port1 = JsonCommDef.GetInstance().GetSelfPort(cbx_Self_Machine.Text, 1);
            string port2 = JsonCommDef.GetInstance().GetSelfPort(cbx_Self_Machine.Text, 2);
            string src = JsonCommDef.GetInstance().GetSelfMachineCode(cbx_Self_Machine.Text);

            int ret1 = this.commForm1.OnSelfMachineChange(addr, port1, src);
            int ret2 = this.commForm2.OnSelfMachineChange(addr, port2, src);
            if(ret1 == -1 || ret2 == -1){
                MessageBox.Show("接続中に対象装置を変更することはできません");
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

            val.SetAsByte(new byte[] {0x31,0x32, 041, 0x42});
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

            commForm2.SendData(CommData_Data.DTYPE_HealthCheck, System.Array.Empty<byte>());
            commForm1.SendData(CommData_Data.DTYPE_Start, new byte[48]);

            CommData.CommData_Data msg0202 = new CommData_Data(CommData_Data.DTYPE_ActiveChange);
            msg0202.GetFldValue("mode-active").SetAsInt(1);
            commForm1.SendData(msg0202);

        }
    }

}
