using Domain.Authentication;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Authentication
{
    public interface IAuthenticationResponse
    {
        Task<bool> IsAuthenticated(UserResponse user);
        Task<string> GenerateJWT(DateTime expiresAt, string username);
        Task<RefreshTokenClass> GenerateRefreshToken(string username, DateTime expiresAt);
        Task<List<dynamic>> GetClaimsByUserName();
    } // interface...
}
