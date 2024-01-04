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
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
        }

        public void SetCommActive(int rescop_no)
        {
            if(rescop_no == 1) {
                commForm1.BackColor = Color.MistyRose;
                commForm2.BackColor = back_color;
            }
            if (rescop_no ==2)
            {
                commForm1.BackColor = back_color;
                commForm2.BackColor = Color.MistyRose;
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
            commForm2.SendData("0101", System.Array.Empty<byte>());
            commForm1.SendData("0201", new byte[48]);

            CommData_Data msg0202 = new CommData_Data("0202");
            msg0202.GetFldValue("mode-active").SetAsInt(1);
            commForm1.SendData(msg0202);

        }

    }

}
