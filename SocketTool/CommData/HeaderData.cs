using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

using SocketTool;
using static SocketTool.Config.JsonDataDef;

namespace SocketTool.CommData
{
    internal class HeaderData: CommDataBase
    {

        public int SeqNo = 0;
        public HeaderData(DataDefine def, byte[] data):base(def, data)
        {
        }


        public HeaderData(DataDefine def) : base(def)
        {
        }

        public void SetConnect(string src, string dst)
        {
            this.GetFldValue("dst").SetAsBcd(dst);
            this.GetFldValue("src").SetAsBcd(src);
        }
        public void SetNextData(string dtype)
        {
            this.GetFldValue("dtype").SetAsBcd(dtype);

        }


    }
}
