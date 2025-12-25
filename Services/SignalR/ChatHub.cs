using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Authenticated: " + Context.User?.Identity?.IsAuthenticated);
            Console.WriteLine("Name: " + Context.User?.Identity?.Name);
            Console.WriteLine("UserIdentifier: " + Context.UserIdentifier);
            return base.OnConnectedAsync();
        } // OnConnectedAsync...

        public async Task SendMessageToAll(string message)
        {
            var connectionId = Context.ConnectionId;
            await Clients.All.SendAsync("ReceiveMessage", message);
        } // SendMessageToAll...

        public async Task SendMessageToClient(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveMessage", message);
        } // SendMessageToClient...
    } // class...
}
