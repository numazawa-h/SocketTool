using SocketTool.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTool.CommData
{
    public class CommData_Data : CommData_Base
    {
        public const string DTYPE_Ack = "0001";                 // 肯定応答
        public const string DTYPE_Nak = "0002";                 // 否定応答
        public const string DTYPE_HealthCheck = "0101";         // ヘルスチェック
        public const string DTYPE_Start = "0201";               // 開始要求
        public const string DTYPE_ActiveChange = "0202";        // 系切替通知

        string _dtype;

        public string DataType { get { return _dtype; } }


        public CommData_Data(string dtype) : base()
        {
            _dtype = dtype;
            base.Init(JsonDataDef.GetInstance().GetMessageDefine(dtype));
        }

        public CommData_Data(string dtype, byte[] data) : base()
        {
            _dtype = dtype;
            base.Init(JsonDataDef.GetInstance().GetMessageDefine(dtype), data);
        }


        public bool isActiveMessage()
        {
            if(_dtype == DTYPE_ActiveChange)
            {
                return GetDataDiscription("active-change") == "アクティブ";
            }

            return false;
        }

        public bool isNeadAck()
        {
            if (_dtype == DTYPE_Ack) return false;
            if (_dtype == DTYPE_Nak) return false;
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


    }
}
