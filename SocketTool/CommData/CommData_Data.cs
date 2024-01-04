using SocketTool.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTool.CommData
{
    public class CommData_Data: CommData_Base
    {
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

       

        public string GetDataDiscription(string fldid)
        {
            string val = this.GetFldValue(fldid).GetAsBcd();
            return Config.JsonDataDef.GetInstance().GetValueDescription(fldid, val);
        }


    }
}
