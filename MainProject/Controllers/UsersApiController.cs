using Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Users;

namespace MainProject.Controllers
{
    [ApiController]

    public class UsersApiController : ControllerBase
    {
        private readonly IUserResponse iuser;

        public UsersApiController(IUserResponse iuser)
        {
            this.iuser = iuser;
        } // constructor...

        [HttpPost("new-user")]
        public async Task<IActionResult> CreateNewUser(NewUserResponse user)
        {
            string str = await iuser.AddNewUser(user);
            return Ok(str);
        } // CreateNewUser...
    } // class...
}
