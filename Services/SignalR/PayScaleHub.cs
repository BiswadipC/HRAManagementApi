using Domain.PayScale;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SignalR
{
    public class PayScaleHub : Hub
    {
        public async Task SendPayScales(PayScaleResponse response)
        {
            await Clients.All.SendAsync("PopulatePayScales", response);
        } // SendPayScales...
    } // class...
}
