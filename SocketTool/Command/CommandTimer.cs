using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SocketTool
{
    public class CommandTimer:Command
    {

        protected List<CommandSend> _on_timer_list = new List<CommandSend>();

        public CommandTimer(string json) : base(json)
        {
            foreach(JsonNode node in _cmd_def.AsArray())
            {
                _on_timer_list.Add(new CommandSend(node));
            }

        }

        public override void Exec(FormMain form)
        {
            foreach(Command cmd in _on_timer_list)
            {
                cmd.Exec(form);
            }
        }


    }
}
