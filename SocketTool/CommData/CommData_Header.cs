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

        public CommData_Header() : base()
        {
            base.Init(JsonDataDef.GetInstance().GetDataDefine(myid));
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
            int dlen = this.commMessageDefine.Length;
            this.GetFldValue("dtype").SetAsBcd(dtype);
            this.GetFldValue("dlen").SetAsInt(dlen);
            this.GetFldValue("alen").SetAsInt(dlen);
            this.GetFldValue("bnum").SetAsInt(1);
            this.GetFldValue("bend").SetAsInt(1);
            this.GetFldValue("bcnt").SetAsInt(1);
        }

        public void SetOnSend(string dtype)
        {
            int dlen = JsonDataDef.GetInstance().GetDataDefine(dtype).Length;
            this.GetFldValue("dtype").SetAsBcd(dtype);
            this.GetFldValue("dlen").SetAsInt(dlen);
            this.GetFldValue("alen").SetAsInt(dlen);
            this.GetFldValue("bnum").SetAsInt(1);
            this.GetFldValue("bend").SetAsInt(1);
            this.GetFldValue("bcnt").SetAsInt(1);
            switch (dtype)
            {
                // ヘルスチェックはシーケンス番号０
                case "0101":
                    this.GetFldValue("seqno").SetAsInt(0);
                    break;
                default:
                    this.GetFldValue("seqno").SetAsInt(SeqNo++);
                    break;
            }
            this.GetFldValue("time").SetAsDateTimeBcd(DateTime.Now);
        }


    }
}
