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


        private List<SocketSendRecv> _socket_list = new List<SocketSendRecv>();
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

        public delegate void CommDataHandler(Object sender, CommDataEventArgs args);
        public event CommDataHandler OnSendData;
        public event CommDataHandler OnRecvData;



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

        protected void AddSocketList(SocketSendRecv handler)
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
            SocketSendRecv socket = new SocketSendRecv(soc, this);
            socket.StartRecv();
            AddSocketList(socket);

            OnAcceptEvent?.Invoke(this, new ConnectEventArgs(socket));
        }


        protected void OnConnect(Socket soc)
        {
            SocketSendRecv socket = new SocketSendRecv(soc, this);
            socket.StartRecv();
            AddSocketList(socket);

            OnConnectEvent?.Invoke(this, new ConnectEventArgs(socket));
        }

        public void OnDisConnect(SocketSendRecv socket)
        {
            var args = new ConnectEventArgs(socket);
            OnDisConnectEvent?.Invoke(this, args);
        }

        public void OnException(Exception e)
        {
            var args = new ThreadExceptionEventArgs(e);
            OnExceptionEvent?.Invoke(this, args);
        }


        public void OnSend(SocketSendRecv socket, byte[] head, byte[]data)
        {
            var args = new CommDataEventArgs(socket, head, data);
            OnSendData?.Invoke(this, args);
        }
        public void OnRecv(SocketSendRecv socket, byte[] head, byte[] data)
        {
            var args = new CommDataEventArgs(socket, head, data);
            OnRecvData?.Invoke(this, args);
        }
    }
    public class CommDataEventArgs : EventArgs
    {
        private SocketSendRecv _socket;
        private byte[] _head;
        private byte[] _data;

        public SocketSendRecv Socket => _socket;
        public byte[] HeadBuff => _head;
        public byte[] DataBuff => _data;

        public CommDataEventArgs(SocketSendRecv socket, byte[] head, byte[] data)
        {
            _socket = socket;
            _head = head;
            _data = data;
        }
    }

    public class ConnectEventArgs: EventArgs
    {
        private SocketSendRecv _socket;

        public SocketSendRecv Socket => _socket;

        public ConnectEventArgs(SocketSendRecv socket)
        {
            _socket = socket;
        }
    }
}
