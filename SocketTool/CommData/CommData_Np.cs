using SocketTool.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTool.CommData
{
    internal class CommData_Np: CommData_Data
    {
        const string myid = DTYPE_NP;


        public CommData_Np() : base(myid)
        {
        }

        public CommData_Np(byte[] data) : base(myid, data)
        {
        }

    }
}
