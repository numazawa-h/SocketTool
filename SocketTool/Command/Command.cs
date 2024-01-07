using SocketTool.CommData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SocketTool
{
    public class Command
    {
        protected JsonDocumentOptions _options;
        protected JsonNode _cmd_def;

        public Command(string json) 
        {
            _options = new JsonDocumentOptions
            {
                // コメントを許可
                CommentHandling = JsonCommentHandling.Skip,
                // 末尾のコンマを許可
                AllowTrailingCommas = true
            };

            _cmd_def = JsonNode.Parse(json, null, _options);
        }

        public virtual void  Exec(FormMain form)
        {
            
        }
    }
}
