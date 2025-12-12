using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public class UserResponse
    {
        public int IdNo {  get; set; }
        public string Username {  get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    } // class...

    public class NewUserResponse
    {
        public int IdNo { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ReTypePassword {  get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    } // class...
}
