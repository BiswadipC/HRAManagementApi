using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Authentication
{
    public class TokenClass
    {
        public string TokenValue { get; set; } = string.Empty;
        public DateTime ExpiresAt {  get; set; }
    } // class...
}
