using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SocketTool.Config.JsonDataDef;
using static System.Net.Mime.MediaTypeNames;

namespace SocketTool.CommData
{
    internal class CommData_Base
    {
        protected CommMessageDefine _define;
        public CommMessageDefine commMessageDefine { get { return _define; } }

        byte[] _data;


        public CommData_Base(CommMessageDefine def, byte[] data=null)
        {
            Init(def, data);
        }

        public CommData_Base()
        {
            _define = null;
            _data = null;
        }

        protected void Init(CommMessageDefine def, byte[] data = null)
        {
            _define = def;
            _data = data;
            if (data==null && def.Length > 0)
            {
                _data = new byte[def.Length];
            }
        }

        public byte[] GetData()
        {
            int ofs = 0;
            int len = _data.Length;

            byte[] val = new byte[len];
            Buffer.BlockCopy(_data, ofs, val, 0, len);

            return val;
        }

        public void SetData(byte[] val)
        {
            Buffer.BlockCopy(_data, 0, val, 0, Math.Min(val.Length, _data.Length));
        }


        public FldValue GetFldValue(string id)
        {
            int ofs = _define.GetFldOffset(id);
            int len = _define.GetFldLength(id);

            byte[] val = new byte[len];
            Buffer.BlockCopy(_data, ofs, val, 0, len);

            return new FldValue(val, id, this);
        }

        public void SetFldValue(string id, byte[] dat)
        {
            int ofs = _define.GetFldOffset(id);
            int len = _define.GetFldLength(id);
            Buffer.BlockCopy(dat, 0, _data, ofs, len);
        }

        public class FldValue
        {
            CommData_Base _owner;
            string _fld_id;
            byte[] _data;

            public FldValue(byte[] data, string fld_id="", CommData_Base owner = null)
            {
                _owner = owner;
                _fld_id = fld_id;
                _data = data;
            }

            public byte[] GetAsByte()
            {
                return _data.Clone() as byte[];
            }

            public int GetAsInt()
            {
                if (_data.Length > 4)
                {
                    return -1;
                }

                byte[] val = new byte[4];
                Buffer.BlockCopy(_data, 0, val, 4 - _data.Length, _data.Length);

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(val);
                }

                return BitConverter.ToInt32(val, 0);
            }

            public long GetAsLong()
            {
                if (_data.Length > 8)
                {
                    return -1;
                }

                byte[] val = new byte[8];
                Buffer.BlockCopy(_data, 0, val, 8 - _data.Length, _data.Length);

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(val);
                }

                return BitConverter.ToInt64(val, 0);
            }

            public string GetAsBcd()
            {
                StringBuilder sb = new StringBuilder();
                foreach (byte b in _data)
                {
                    int b1 = b >> 4;
                    int b2 = b & 0x0f;
                    sb.Append(b1);
                    sb.Append(b2);
                }

                return sb.ToString();
            }

            public string GetAsStringAsc()
            {
                return System.Text.Encoding.ASCII.GetString(_data); ;
            }

            public DateTime GetAsDateTimeBcd()
            {
                DateTime val;
                string dt = GetAsBcd();
                switch(dt.Length)
                {
                    case 8:
                        val = DateTime.ParseExact(dt, "yyyyMMdd", null);
                        break;
                    case 12:
                        val = DateTime.ParseExact(dt, "yyyyMMddHHmm", null);
                        break;
                    case 14:
                        val = DateTime.ParseExact(dt, "yyyyMMddHHmmss", null);
                        break;
                    default:
                        val = DateTime.MinValue;
                        break;
                }

                return val;
            }


            private void FillData(byte val)
            {
                for( int idx=0; idx< _data.Length; idx++)
                {
                    _data[idx] = val;
                }
            }

            public void SetAsByte(byte[] val)
            {
                FillData(0);
                Buffer.BlockCopy(val, 0, _data, 0, Math.Min(val.Length, _data.Length));
                _owner?.SetFldValue(_fld_id, _data);
                return;
            }

            public void SetAsInt(int val)
            {
                if (_data.Length > 4)
                {
                    return;
                }

                byte[] dat = BitConverter.GetBytes(val);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(dat);
                }

                FillData(0);
                Buffer.BlockCopy(dat, 4 - _data.Length, _data,0, _data.Length);
                _owner?.SetFldValue(_fld_id, _data);
                return;
            }

            public void SetAsLong(long val)
            {
                if (_data.Length > 8)
                {
                    return;
                }

                byte[] dat = BitConverter.GetBytes(val);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(dat);
                }

                FillData(0);
                Buffer.BlockCopy(dat, 8 - _data.Length, _data, 0, _data.Length);
                _owner?.SetFldValue(_fld_id, _data);
                return;
            }

            public void SetAsBcd(string val)
            {
                FillData(0);
                for(int idx=0; idx< _data.Length; idx++)
                {
                    int ofs = idx * 2;
                    if (ofs >= val.Length) break;

                    byte b = 0;
                    byte[] dat;
                    string d1 = val.Substring(idx*2, 1);
                    dat = System.Text.Encoding.ASCII.GetBytes(d1);
                    b = (byte)(dat[0] << 4);
                    if(ofs+1 < val.Length)
                    {
                        string d2 = val.Substring(idx * 2 + 1, 1);
                        dat = System.Text.Encoding.ASCII.GetBytes(d2);
                        b |= (byte)(dat[0] & 0x0f);
                    }

                    _data[idx] = b;
                }
                _owner?.SetFldValue(_fld_id, _data);
                return;
            }

            public void SetAsStringAsc(string val)
            {
                byte[] dat = System.Text.Encoding.ASCII.GetBytes(val);

                FillData(0);
                Buffer.BlockCopy(dat, 0, _data, 0, Math.Min(_data.Length, dat.Length));
                _owner?.SetFldValue(_fld_id, _data);
                return;
            }

            public void SetAsDateTimeBcd(DateTime val)
            {
                string dat = val.ToString("yyyyMMddHHmmss");

                SetAsBcd(dat);
                return;
            }

        }

    }
}
