using SocketTool.Properties;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketTool
{
    internal class ServerSocket: SocketBase
    {
        Socket _listner;

        public ServerSocket( int headsize, int datalen_ofs, int datalen_len) : base(headsize, datalen_ofs, datalen_len) 
        {
            _listner = null;
        }

        public override int checkParam()
        {
            base.checkParam();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            if (!ipHostInfo.AddressList.Contains<IPAddress>(_ipAddress))
            {
                OnException( new Exception("指定されたIPアドレスがホストに存在しません"));
                return -1;
            }
            return 0;
        }

        public void Listen(string iaddr, string no)
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

                // ソケットを作成
                if (_listner == null)
                {
                    _listner = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    _listner.Bind(_remoteEP);
                }
                _listner.Listen(0);
                _ = _listner.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }


        public void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket socket = _listner.EndAccept(ar);
                OnAccept(socket);
            }
            catch (System.ObjectDisposedException ex)
            {
                OnException(ex);
            }

        }
    }
}
