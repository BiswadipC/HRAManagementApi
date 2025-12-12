using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Authentication
{
    public class RefreshTokenClass
    {
        public string Username {  get; set; } = string.Empty;
        public string TokenValue { get; set; } = string.Empty;
    } // class...
}
