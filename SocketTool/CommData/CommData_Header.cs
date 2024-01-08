using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

using SocketTool;
using SocketTool.Config;
using static SocketTool.Config.JsonDataDef;

namespace SocketTool.CommData
{
    internal class CommData_Header: CommData_Base
    {
        const string myid = "head";
        private int SeqNo = 0;


        public string DataType => GetFldValue("dtype").GetAsBcd();
        public DateTime RecvDateTime => GetFldValue("sdatm").GetAsDateTimeBcd();

        public int Block_cnt => GetFldValue("bcnt").GetAsInt();
        public int Block_num => GetFldValue("bnum").GetAsInt();

        public CommData_Header() : base()
        {
            base.Init(JsonDataDef.GetInstance().GetMessageDefine(myid));
        }

        public CommData_Header(byte[] data) : base()
        {
            base.Init(JsonDataDef.GetInstance().GetMessageDefine(myid), data);
        }



        public void SetDstMachineCode(string dst)
        {
            this.GetFldValue("dst").SetAsBcd(dst);
        }
        public void SetSrcMachineCode(string src)
        {
            this.GetFldValue("src").SetAsBcd(src);
        }

        public void SetDataType(string dtype)
        {
            this.GetFldValue("dtype").SetAsBcd(dtype);
        }

        public void SetOnSend(string dtype, int dlen, int bnum =1, int bcnt =1)
        {
            this.GetFldValue("dtype").SetAsBcd(dtype);
            this.GetFldValue("dlen").SetAsInt(dlen);
            this.GetFldValue("alen").SetAsLong(dlen);       // TODO:全データ長はブロック分割前のサイズ
            this.GetFldValue("bnum").SetAsInt(bnum);
            this.GetFldValue("bend").SetAsInt((bnum ==bcnt)?1:0);
            this.GetFldValue("bcnt").SetAsInt(bcnt);
            switch (dtype)
            {
                // ヘルスチェックはシーケンス番号０
                case CommData_Data.DTYPE_HealthCheck:
                    this.GetFldValue("seqno").SetAsInt(0);
                    break;
                default:
                    this.GetFldValue("seqno").SetAsInt(SeqNo++);
                    break;
            }
            this.GetFldValue("sdatm").SetAsDateTimeBcd(DateTime.Now);
        }


    }
}
