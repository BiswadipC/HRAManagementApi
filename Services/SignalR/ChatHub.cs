using Domain.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SignalR
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatMessageClass message)
        {
            message.TimeStamp = DateTime.UtcNow;
            await Clients.All.SendAsync("ReceiveMessage", message);
        } // SendMessage...
    } // class...
}
