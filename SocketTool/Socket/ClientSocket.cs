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
        AutoResetEvent _wait_connect = new AutoResetEvent(false);
        Socket _connect;

        public event EventHandler OnFailConnectEvent;


        public ClientSocket( int headsize, int datalen_ofs, int datalen_len) : base( headsize, datalen_ofs, datalen_len)
        {

        }

        public async void Connect(string iaddr, string no)
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

                _wait_connect.Reset();
                socket.BeginConnect(_remoteEP,  new AsyncCallback(ConnectCallback), socket);
                await Task.Delay(1000);
                if (_wait_connect.WaitOne(500) == false)
                {
                    throw new Exception("接続待ちタイムアウト");
                }
                if (_connect == null)
                {
                    throw new Exception("コネクト失敗");
                }
                OnConnect(_connect);
            }
            catch (Exception ex)
            {
                OnFailConnect();
                OnException(ex); 
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                _connect = (Socket)ar.AsyncState;
                _connect.EndConnect(ar);
            }
            catch (Exception ex)
            {
                _connect = null;
            }
            finally {
                _wait_connect.Set();
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
