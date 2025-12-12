using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SignalR
{
    public class ChatMessageClass
    {
        public string Username {  get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    } // class...
}
