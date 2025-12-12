using Domain.Authentication;
using Domain.Users;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.Authentication;

namespace MainProject.Controllers
{
    [ApiController]

    public class AuthenticationApiController : ControllerBase
    {
        private readonly IAuthenticationResponse iauth;

        public AuthenticationApiController(IAuthenticationResponse iauth)
        {
            this.iauth = iauth;
        } // constructor...

        [HttpPost("validate-credentials")]
        public async Task<IActionResult> ValidateCredentials(UserResponse user)
        {
            try
            {
                bool b = await iauth.IsAuthenticated(user);
                if (!b)
                {
                    ModelState.AddModelError("Invalid User", "Invalid user. Login denied");
                    var problemDetails = new ValidationProblemDetails(ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    return new BadRequestObjectResult(problemDetails);
                } // if not a valid user...

                string jwt = await iauth.GenerateJWT(DateTime.UtcNow.AddSeconds(30), user.Username);
                RefreshTokenClass rtc = await iauth.GenerateRefreshToken(user.Username);
                string strJson = JsonConvert.SerializeObject(rtc);

                Response.Cookies.Append("AuthCookie", jwt, new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                });

                Response.Cookies.Append("RefreshCookie", strJson, new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                });

                return Ok("Success");
            } // end of try...
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            } // end of catch...            
        } // ValidateCredentials...

        [HttpPost("generate-token-rft")]
        public async Task<IActionResult> GenerateToken_RefreshToken()
        {
            string rftJson = Request.Cookies["RefreshCookie"]!.ToString();
            RefreshTokenClass rft = JsonConvert.DeserializeObject<RefreshTokenClass>(rftJson)!;
            string username = rft.Username;

            string jwt = await iauth.GenerateJWT(DateTime.UtcNow.AddSeconds(30), username);
            RefreshTokenClass rtc = await iauth.GenerateRefreshToken(username);
            string strJson = JsonConvert.SerializeObject(rtc);

            Response.Cookies.Append("AuthCookie", jwt, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            Response.Cookies.Append("RefreshCookie", strJson, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            return Ok();
        } // GenerateToken_RefreshToken...

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("AuthCookie", new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1)
            });

            Response.Cookies.Delete("RefreshCookie", new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1)
            });

            return await Task.Run(() => Ok());
        } // Logout...

        [HttpGet("WhoAmI")]
        public async Task<IActionResult> WhoAmI()
        {
            try
            {
                string token = Request.Cookies["AuthCookie"]!.ToString();
                string refreshToken = Request.Cookies["RefreshCookie"]!.ToString();

                return await Task.Run(() => Ok(true));
            }
            catch (Exception ex)
            {
                return await Task.Run(() => Ok(false));
            }
        } // WhoAmI...

        [HttpGet("getclaimsbyusername")]
        public async Task<IActionResult> GetClaimsByUserName()
        {
            List<dynamic> claims = await iauth.GetClaimsByUserName();
            return Ok(claims);
        } // GetClaimsByUserName...
    } // class...
}
