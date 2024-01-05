using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SocketTool.Properties;
using System.Threading;
using System.Diagnostics;

namespace SocketTool
{
    internal class ClientSocket: SocketBase
    {
        public event EventHandler OnFailConnectEvent;


        public ClientSocket( int headsize, int datalen_ofs, int datalen_len) : base( headsize, datalen_ofs, datalen_len)
        {

        }

        public void Connect(string iaddr, string no)
        {
            try
            {
                int portno;
                try
                {
                    portno = int.Parse(no);
                }
                catch (Exception)
                {
                    throw new Exception("ポート番号が数値でありません");
                }
                _ipAddress = IPAddress.Parse(iaddr);
                _remoteEP = new IPEndPoint(_ipAddress, portno);
                checkParam();

                Socket socket = new Socket(this._ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.BeginConnect(_remoteEP,  new AsyncCallback(ConnectCallback), socket);


            }
            catch(Exception ex)
            {
                OnFailConnect();
                OnException(ex); 
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket socket = (Socket)ar.AsyncState;
                socket.EndConnect(ar);
                OnConnect(socket);
            }
            catch (Exception ex)
            {
                OnFailConnect();
                OnException(ex);
            }
        }

        public void StopConnect()
        {

        }

        public void OnFailConnect()
        {
            OnFailConnectEvent?.Invoke(this, EventArgs.Empty);
        }


    }
}
