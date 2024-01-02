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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] head = new byte[] { 10, 0, 0, 0 };
            byte[] data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            commForm2.SendData(head, data);
        }


    }

}
