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

        public Form1()
        {
            InitializeComponent();
            this.commForm1.RESCOP_NO = 1;
            this.commForm2.RESCOP_NO = 2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string wcd = System.AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                JsonCommDef.GetInstance().ReadJson(wcd + "config\\CommDef.json");
            }catch(Exception ex)
            {
                MessageBox.Show($"CommDef.jsonの読み込み失敗({ex.Message})");
                this.Close();
            }
            try
            {
                JsonDataDef.GetInstance().ReadJson(wcd + "config\\CommDataDef.json");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CommDataDef.jsonの読み込み失敗({ex.Message})");
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] head = new byte[] { 10, 0, 0, 0 };
            byte[] data = new byte[] { 0x20, 0x23, 0x12, 0x31 };


            CommData.CommDataBase.FldValue val = new CommData.CommDataBase.FldValue(data);
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

            commForm2.SendData(head, data);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            this.commForm1.Init();
            this.commForm2.Init();
        }
    }

}
