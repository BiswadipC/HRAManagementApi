using Domain.Designation;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SignalR
{
    public class DesignationHub : Hub
    {
        public async Task SendMessage(DesignationResponse response)
        {
            await Clients.All.SendAsync("ReceiveMessage", response);
        } // SendMessage...
    } // class...
}
