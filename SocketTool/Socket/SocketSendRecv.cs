using SocketTool.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketTool
{
    public class SocketSendRecv
    {
        protected SocketBase _soc_base;
        protected Socket _soc;
        protected int _head_size;
        protected byte[] _head;
        protected int _head_recv_cnt;
        protected int _data_size;
        protected byte[] _data;
        protected int _data_recv_cnt;



        public SocketSendRecv(Socket soc, SocketBase soc_base)
        {
            _soc_base = soc_base;
            _soc = soc;
            _head_size = soc_base._header_size;
            _head_recv_cnt = -1;
            _data_size = 0;
            _data_recv_cnt = -1;
        }

        public void Stop()
        {
            _soc.Close(); ;
            _head_recv_cnt = -1;
            _data_size = 0;
            _data_recv_cnt = -1;
        }

        public bool isServerSocket()
        {
            return _soc_base is ServerSocket;
        }

        public bool isClientSocket()
        {
            return _soc_base is ClientSocket;
        }

        public void Send(byte[] head, byte[] data)
        {
            try
            {
                _soc.Send(joinByteArray(head, data));
                onSend(head, data);
            }
            catch (Exception e)
            {
                Log.Error("送信中に例外発生", e);
                _soc_base.OnException(e);
            }
        }

        private byte[] joinByteArray(byte[] ary1, byte[] ary2)
        {
            byte[] mergedArray = new byte[ary1.Length + ary2.Length];
            int typeSize = System.Runtime.InteropServices.Marshal.SizeOf(
                ary1.GetType().GetElementType());
            Buffer.BlockCopy(ary1, 0, mergedArray, 0,  ary1.Length );
            Buffer.BlockCopy(ary2, 0, mergedArray, ary1.Length , ary2.Length);
        
            return mergedArray;
        }

        public void StartRecv()
        {
            _head_recv_cnt = -1;
            _data_recv_cnt = -1;
            _data_size = 0;
            Task.Run(() => recvProc(this));
        }

        static private void recvProc(SocketSendRecv _this)
        {
            try
            {
                while (true)
                {
                    if (_this._head_recv_cnt < _this._head_size)
                    {
                        _this.receiveHead();
                    }
                    else if (_this._data_recv_cnt < _this._data_size)
                    {
                        _this.receiveData();
                    }
                    else
                    {
                        _this.onRecv();
                        _this._head_recv_cnt = -1; 
                        _this._data_recv_cnt = -1;
                        _this._data_size = 0;
                    }
                }
            }
            catch (Exception ex) 
            { 
                _this._soc_base.OnDisConnect(_this);
                _this._soc_base.OnException(ex);
            }
        }

        private void receiveHead()
        {
            if (_head_recv_cnt < 0)
            {
                // ヘッダ受信の準備
                _head = new byte[_head_size];
                _head_recv_cnt = 0;
            }

            int rcnt = _soc.Receive(_head, _head_recv_cnt, _head_size - _head_recv_cnt, SocketFlags.None);
            if (rcnt == 0)
            {
                // 切断処理
                _soc_base.OnDisConnect(this);
                return;
            }
            _head_recv_cnt += rcnt;
        }
        private void receiveData()
        {
            if (_data_recv_cnt < 0)
            {
                // データ受信の準備
                _data_size = GetDataLength( _head);
                if (_data_size == 0)
                {
                    _data = System.Array.Empty<byte>();
                }
                else
                {
                    _data = new byte[_data_size];
                }
                _data_recv_cnt = 0;
                if (_data_size == 0)
                {
                    return;
                }
            }

            int rcnt = _soc.Receive(_data, _data_recv_cnt, _data_size - _data_recv_cnt, SocketFlags.None);
            if (rcnt == 0)
            {
                // 切断処理
                _soc_base.OnDisConnect(this);
                return;
            }
            _data_recv_cnt += rcnt;
        }

        private int GetDataLength(byte[] head)
        {
            byte[] data = new byte[4];
            switch (_soc_base._datalen_bytes)
            {
                case 1:
                    data[3] = head[_soc_base._datalen_ofs];
                    break;
                case 2:
                    data[2] = head[_soc_base._datalen_ofs];
                    data[3] = head[_soc_base._datalen_ofs + 1];
                    break;
                case 4:
                    data[0] = head[_soc_base._datalen_ofs];
                    data[1] = head[_soc_base._datalen_ofs + 1];
                    data[2] = head[_soc_base._datalen_ofs + 2];
                    data[3] = head[_soc_base._datalen_ofs + 3];
                    break;
            }
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }

            return BitConverter.ToInt32(data, 0);
        }

         private void onRecv()
        {
            _soc_base.OnRecv(this, _head, _data);
        }

        private void onSend(byte[] head, byte[] data)
        {
            _soc_base.OnSend(this, head, data);
        }
    }
}
