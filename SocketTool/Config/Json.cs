using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;

namespace SocketTool
{
    public class Json
    {
        protected JsonDocumentOptions _options;
        protected JsonNode _json_root;

        public event ThreadExceptionEventHandler OnExceptionEvent;

        public Json()
        {
            _options = new JsonDocumentOptions
            {
                // コメントを許可
                CommentHandling = JsonCommentHandling.Skip,
                // 末尾のコンマを許可
                AllowTrailingCommas = true
            };
        }

        public virtual int ReadJson(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    _json_root = JsonNode.Parse(fs, null, _options);
                };
            }
            catch (Exception ex){
                OnException(new Exception($"json読み込み失敗{path}({ex.Message})"));;
                return -1;
            }
            return 0;
        }

        public void OnException(Exception e)
        {
            var args = new ThreadExceptionEventArgs(e);
            OnExceptionEvent?.Invoke(this, args);
        }

    }
}
