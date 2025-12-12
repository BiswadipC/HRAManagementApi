using Domain.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Services.SignalR;
using System.Runtime.InteropServices;

namespace MainProject.Controllers
{
    [Route("chatapi")]
    [ApiController]

    public class ChatApiController : ControllerBase
    {
        private readonly IHubContext<ChatHub> hub;

        public ChatApiController(IHubContext<ChatHub> hub)
        {
            this.hub = hub;
        } // constructor...

        [HttpPost("send")]
        public async Task<IActionResult> Send(ChatMessageClass message)
        {
            if (string.IsNullOrWhiteSpace(message.Message))
            {
                ModelState.AddModelError("BadRequest", "No message found. Invalid request.");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                return new BadRequestObjectResult(problemDetails);
            } // end if...

            message.TimeStamp = DateTime.UtcNow;
            await hub.Clients.All.SendAsync("ReceiveMessage", message);

            return Ok(new {Status = "Message Broadcasted", message});
        } // Send...
    } // class...
}
