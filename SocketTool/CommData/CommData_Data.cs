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
        public const string DTYPE_STA = "0101";                 // 開始要求
        public const string DTYPE_HEALTH_CHK = "0103";          // ヘルスチェック
        public const string DTYPE_ACT_CHANGE = "0201";          // 系切替通知
        public const string DTYPE_LANE_ST = "0301";             // 車線管理
        public const string DTYPE_PASSCAR = "0303";             // 通過車両
        public const string DTYPE_PASSCAR_KEEP = "8303";        // 通過車両(蓄積)
        public const string DTYPE_NP = "0304";                  // NP認識
        public const string DTYPE_ENTER = "0306";               // 立ち入り画像
        public const string DTYPE_MOV_CUT = "0307";             // 動画切り出し


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
            if(DType == DTYPE_ACT_CHANGE)
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

        /// <summary>
        /// 画像データ読み込み
        /// </summary>
        /// <param name="fldid"></param>
        /// <param name="fpath">画像fileの実行ディレクトリからの相対パス</param>
        public void LoadImage(string fldid, string fpath)
        {
            byte[] dat =System.Array.Empty<byte>();

            string wcd = System.AppDomain.CurrentDomain.BaseDirectory;
            string path = wcd + Config.JsonDataDef.GetInstance().GetValueDescription(fldid, fpath);
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
                Log.Error("イメージファイル読み込みで例外発生", ex);
                dat = System.Array.Empty<byte>();
            }

            AddData(dat);
        }

        public void SaveImage(string fldid, string fpath)
        {
            byte[] dat = GetFldValue(fldid).GetAsByte();
            string wcd = System.AppDomain.CurrentDomain.BaseDirectory;
            string path = wcd + Config.JsonDataDef.GetInstance().GetValueDescription(fldid, fpath);
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
                Log.Error("イメージファイル書き込みで例外発生", ex);
            }
        }
    }
}
