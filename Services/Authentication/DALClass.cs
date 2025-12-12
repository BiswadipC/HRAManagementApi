using Azure.Core;
using Domain.Authentication;
using Domain.Users;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Repository.Authentication;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Services.Authentication
{
    namespace NAuthentication
    {
        internal sealed class DALClass : IAuthenticationResponse
        {
            private readonly SampleContext context;
            private readonly IDapperService dapper;
            private readonly IConfiguration configuration;
            private readonly IHttpContextAccessor httpContextAccessor;

            public DALClass(SampleContext context, IDapperService dapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
            {
                this.context = context;
                this.dapper = dapper;
                this.configuration = configuration;
                this.httpContextAccessor = httpContextAccessor;
            } // constructor...

            public async Task<bool> IsAuthenticated(UserResponse user)
            {
                if(context.Users.Any(m => m.Username == user.Username && m.Password == user.Password))
                {
                    return await Task.Run(() => true);
                }

                return await Task.Run(() => false);
            } // IsAuthenticated...

            public async Task<string> GenerateJWT(DateTime expiresAt, string username)
            {
                string key = configuration.GetValue<string>("SecurityKey")!;
                SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), 
                    SecurityAlgorithms.HmacSha256);

                List<Claim> claims = new List<Claim>();
                foreach(var data in context.ModulePolicyMappings.Where(m => m.Username == username))
                {
                    var claim = new Claim(data.PolicyName, data.PermissionType);
                    claims.Add(claim);
                }
                ClaimsIdentity identity = new ClaimsIdentity(claims);

                SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
                {
                    SigningCredentials = credentials,
                    Subject = identity,
                    Expires = expiresAt,
                    NotBefore = DateTime.UtcNow
                };

                JsonWebTokenHandler handler = new JsonWebTokenHandler();
                string token = handler.CreateToken(descriptor);
                return await Task.Run(() => token);
            } // GenerateJWT...

            public async Task<RefreshTokenClass> GenerateRefreshToken(string username)
            {
                string token = Guid.NewGuid().ToString();

                RefreshTokenClass rft = new RefreshTokenClass();
                rft.Username = username;
                rft.TokenValue = token;

                var existingUser = context.Users.FirstOrDefault(x =>  x.Username == username);
                existingUser!.RefreshToken = token;
                context.Users.Update(existingUser);
                await context.SaveChangesAsync();

                return rft;
            } // GenerateRefreshToken...

            public async Task<List<dynamic>> GetClaimsByUserName()
            {
                var claims = new List<dynamic>();
                try
                {
                    string rftJson = httpContextAccessor.HttpContext!.Request.Cookies["RefreshCookie"]!.ToString();
                    RefreshTokenClass rtc = JsonConvert.DeserializeObject<RefreshTokenClass>(rftJson)!;
                    string username = rtc.Username;

                    
                    var lists = context.ModulePolicyMappings.Where(m => m.Username == username).ToList();
                    foreach(var data in lists)
                    {
                        dynamic obj = new ExpandoObject();
                        obj.TypeName = data.PolicyName;
                        obj.Value = data.PermissionType;

                        claims.Add(obj);
                    } // end of foreach loop...
                }
                catch (Exception ex)
                {

                }

                return await Task.Run(() => claims);
            } // GetClaimsByUserName...
        } // class...
    } // namespace NAuthentication...
}
