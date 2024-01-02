using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketTool.Properties
{
    public class SocketBase
    {
        private static ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();


        private List<SocketReadWrite> _socket_list = new List<SocketReadWrite>();
        protected IPAddress _ipAddress;
        protected IPEndPoint _remoteEP;
        public int _header_size;
        public int _datalen_ofs;
        public int _datalen_bytes;

        
        public event ThreadExceptionEventHandler OnExceptionEvent;

        public delegate void ConnectEventHandler(Object sender, ConnectEventArgs args);
        public event ConnectEventHandler OnAcceptEvent;
        public event ConnectEventHandler OnConnectEvent;
        public event ConnectEventHandler OnDisConnectEvent;

        public delegate void RecvDataHandler(Object sender, RecvDataEventArgs args);
        public event RecvDataHandler OnRecvData;
        


        public SocketBase( int headsize, int datalen_ofs, int datalen_bytes)
        {
            _header_size = headsize;
            _datalen_ofs = datalen_ofs;
            _datalen_bytes = datalen_bytes;
        }

        public virtual int  checkParam()
        {
            if (!(new int[] { 1, 2, 4 }).Contains(_datalen_bytes))
            {
                OnException(new Exception("datalen_bytes:データ長のバイト数は、1,2,4のいずれかで指定してください"));
                return -1;
            }

            return 0;
        }

        protected void AddSocketList(SocketReadWrite handler)
        {
            _locker.EnterWriteLock();
            try
            {
                _socket_list.Add(handler);
            }
            finally
            {
                _locker.ExitWriteLock();
            }        
        }


        protected void OnAccept(Socket soc)
        {
            SocketReadWrite socket = new SocketReadWrite(soc, this);
            socket.StartRecv();
            AddSocketList(socket);

            OnAcceptEvent?.Invoke(this, new ConnectEventArgs(socket));
        }


        protected void OnConnect(Socket soc)
        {
            SocketReadWrite socket = new SocketReadWrite(soc, this);
            socket.StartRecv();
            AddSocketList(socket);

            OnConnectEvent?.Invoke(this, new ConnectEventArgs(socket));
        }

        public void OnDisConnect(SocketReadWrite socket)
        {
            var args = new ConnectEventArgs(socket);
            OnDisConnectEvent?.Invoke(this, args);
        }

        public void OnException(Exception e)
        {
            var args = new ThreadExceptionEventArgs(e);
            OnExceptionEvent?.Invoke(this, args);
        }


        public void OnRecv(SocketReadWrite socket, byte[] head, byte[]data)
        {
            var args = new RecvDataEventArgs(socket, head, data);
            OnRecvData?.Invoke(this, args);
        }
    }
    public class RecvDataEventArgs : EventArgs
    {
        private SocketReadWrite _socket;
        private byte[] _head;
        private byte[] _data;

        public SocketReadWrite Socket => _socket;
        public byte[] HeadBuff => _head;
        public byte[] DataBuff => _data;

        public RecvDataEventArgs(SocketReadWrite socket, byte[] head, byte[] data)
        {
            _socket = socket;
            _head = head;
            _data = data;
        }
    }

    public class ConnectEventArgs: EventArgs
    {
        private SocketReadWrite _socket;

        public SocketReadWrite Socket => _socket;

        public ConnectEventArgs(SocketReadWrite socket)
        {
            _socket = socket;
        }
    }
}
