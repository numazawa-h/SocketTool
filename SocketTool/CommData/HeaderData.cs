using System;
using System.Collections.Generic;
using System.Linq;
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

        public HeaderData(DataDefine def, byte[] data):base(def, data)
        {
        }


        public HeaderData(DataDefine def) : base(def)
        {
        }


    }
}
