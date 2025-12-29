using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext context)
        {
            string userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            return userId;
        } // GetUserId...
    } // class...
}
