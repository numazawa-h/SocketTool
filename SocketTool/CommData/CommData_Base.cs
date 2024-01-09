using SocketTool.Config;
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
    public class CommData_Base 
    {
        protected CommMessageDefine _define;
        public CommMessageDefine commMessageDefine { get { return _define; } }

        public string Name {  get { return _define.Name; } }
        public string DType { get { return _define.DType; } }

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

        protected void Init(CommMessageDefine def, byte[] dat = null)
        {
            _define = def;
            _data = dat;

            if (dat != null)
            {
                // データ生成済ならその長さでコピー
                _data = new byte[dat.Length];
                Buffer.BlockCopy(dat, 0, _data, 0, _data.Length);

            }
            else
            {
                if (def.Length == 0)
                {
                    // データ部なしメッセージ
                    _data = System.Array.Empty<byte>(); 
                }
                if (def.Length > 0)
                {
                    // 固定長メッセージ
                    _data = new byte[def.Length];
                }
                if (def.Length < 0)
                {
                    // 可変長メッセージ
                    if (def.MinLength > 0)
                    {
                        // 固定部分ありならその部分のみ生成
                        _data = new byte[def.MinLength];
                    }
                    else
                    {
                        // 固定部分がなければ空で生成
                        _data = System.Array.Empty<byte>();
                    }
                }
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
            Buffer.BlockCopy(val, 0, _data, 0, Math.Min(val.Length, _data.Length));
        }

        public void AddData(byte[] val)
        {
            byte[] _tmp = new byte[_data.Length + val.Length];
            Buffer.BlockCopy(_data, 0, _tmp, 0, _data.Length);
            Buffer.BlockCopy(val, 0, _tmp, _data.Length, val.Length);
            _data = _tmp;
        }

        public FldValue GetFldValue(string fldid)
        {
            byte[] val;
            try
            {
                int ofs = _define.GetFldOffset(fldid);
                int len = _define.GetFldLength(fldid);

                if(len > 0)
                {
                    val = new byte[len];
                    Buffer.BlockCopy(_data, ofs, val, 0, len);
                }
                else
                {
                    val =System.Array.Empty<byte>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{fldid}の取得で例外発生({ex.Message})");
            }

            return new FldValue(val, fldid, this);
        }

        public void SetFldValue(string fldid, byte[] dat)
        {
            try
            {
                int ofs = _define.GetFldOffset(fldid);
                int len = _define.GetFldLength(fldid);
                Buffer.BlockCopy(dat, 0, _data, ofs, len);
            }
            catch (Exception ex)
            {
                throw new Exception($"{fldid}の設定で例外発生({ex.Message})");
            }
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
            public FldValue(string bcd, string fld_id = "", CommData_Base owner = null)
            {
                _owner = owner;
                _fld_id = fld_id;
                _data = new byte[(int)Math.Ceiling((double)bcd.Length /2)];
                SetAsBcd(bcd);
            }

            public byte[] GetAsByte()
            {
                return _data.Clone() as byte[];
            }

            public int GetAsInt()
            {
                if (_data.Length > 4 || _data.Length < 1 )
                {
                    return 0;
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
                if (_data.Length > 8 || _data.Length < 1)
                {
                    return 0;
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
                    sb.Append($"{b1:X}");
                    sb.Append($"{b2:X}");
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
                if (_data.Length > 4 || _data.Length < 1)
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
                if (_data.Length > 8 || _data.Length < 1)
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

                    string d = val.Substring(ofs, 2);
                    _data[idx] = Convert.ToByte(d, 16);
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

            public void SetAsDateWTimeBcd(DateTime val)
            {
                string dat = string.Format("{0:yyyyMMdd}{1:D1}{2:HHmmss}", val, val.DayOfWeek, val);

                SetAsBcd(dat);
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
