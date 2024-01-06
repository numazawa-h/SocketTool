using SocketTool.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SocketTool.Config.JsonDataDef;

namespace SocketTool.CommData
{
    public class CommData_Data : CommData_Base
    {
        public const string DTYPE_Ack = "0001";                 // 肯定応答
        public const string DTYPE_Nak = "0002";                 // 否定応答
        public const string DTYPE_HealthCheck = "0101";         // ヘルスチェック
        public const string DTYPE_Start = "0201";               // 開始要求
        public const string DTYPE_ActiveChange = "0202";        // 系切替通知
        public const string DTYPE_PASSCAR = "0301";             // 通過車両
        public const string DTYPE_LANE_ST = "0302";             // 車線管理
        public const string DTYPE_NP = "0501";                  // NP認識


        public CommData_Data(string dtype) : base()
        {
            base.Init(JsonDataDef.GetInstance().GetMessageDefine(dtype));
        }

        public CommData_Data(string dtype, byte[] data) : base()
        {
            base.Init(JsonDataDef.GetInstance().GetMessageDefine(dtype), data);
        }


        public bool isActiveMessage()
        {
            if(DType == DTYPE_ActiveChange)
            {
                return GetDataDiscription("active-change") == "アクティブ";
            }

            return false;
        }

        public bool isNeadAck()
        {
            if (DType == DTYPE_Ack) return false;
            if (DType == DTYPE_Nak) return false;
            return true;
        }
        public bool isNeadNak()
        {
            return false;
        }

        public string GetDataDiscription(string fldid)
        {
            string val = this.GetFldValue(fldid).GetAsBcd();
            return Config.JsonDataDef.GetInstance().GetValueDescription(fldid, val);
        }

        public string GetMsgDiscription()
        {
            StringBuilder sb = new StringBuilder();
            bool isFirst = true;
            foreach (string key in _define.Fld_List.Keys)
            {
                FieldDefine fld = _define.Fld_List[key];
                if (fld.isDispDesc)
                {
                    string desc = GetDataDiscription(fld.FldId);
                    if (desc != string.Empty) {
                        if (isFirst)
                        {
                            isFirst = false;
                            sb.Append("(");
                        }
                        else
                        {
                            sb.Append(",");
                        }
                        sb.Append(desc);
                    }
                }
            }
            if (isFirst == false)
            {
                sb.Append(")");
            }

            return sb.ToString();
        }

        public void LoadImage(string fldid, string fnmae)
        {
            byte[] dat =System.Array.Empty<byte>();

            string wcd = System.AppDomain.CurrentDomain.BaseDirectory;
            string path = wcd + Config.JsonDataDef.GetInstance().GetValueDescription(fldid, fnmae);
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    dat = new byte[fs.Length];
                    fs.Read(dat, 0, dat.Length);
                }
            }
            catch (Exception ex)
            {

            }

            AddData(dat);
        }

        public void SaveImage(string fldid, string fnmae)
        {
            byte[] dat = GetFldValue(fldid).GetAsByte();
            string wcd = System.AppDomain.CurrentDomain.BaseDirectory;
            string path = wcd + Config.JsonDataDef.GetInstance().GetValueDescription(fldid, fnmae);
            try
            {
                using (var fs = new FileStream(path, FileMode.Create))
                using (var sw = new BinaryWriter(fs))
                {
                    sw.Write(dat);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
