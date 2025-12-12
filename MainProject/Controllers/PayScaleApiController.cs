using Domain.PayScale;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Repository.PayScale;
using Services.SignalR;

namespace MainProject.Controllers
{
    [Route("pay-scales")]
    [ApiController]

    public class PayScaleApiController : ControllerBase
    {
        private readonly IPayScaleResponse ipay;
        private readonly IHubContext<PayScaleHub> hub;

        public PayScaleApiController(IPayScaleResponse ipay, IHubContext<PayScaleHub> hub)
        {
            this.ipay = ipay;
            this.hub = hub;
        } // constructor...

        [HttpGet("")]
        [Authorize(Policy = "VIEW_PAY-SCALE")]
        public async Task<IActionResult> GetPayScales()
        {
            var payScales = await ipay.GetPayScales();
            return Ok(payScales);
        } // GetPayScales...

        [HttpGet("{id}")]
        [Authorize(Policy = "EDIT_PAY-SCALE")]
        public async Task<IActionResult> GetPayScaleById(int id)
        {
            var payScale = await ipay.GetPayScaleById(id);
            return Ok(payScale);
        } // GetPayScaleById...

        [HttpPost("")]
        [Authorize(Policy = "EDIT_PAY-SCALE")]
        public async Task<IActionResult> Save(PayScaleResponse response)
        {
            string str = await ipay.Save(response);
            if(str != "Success")
            {
                ModelState.AddModelError("Save Error", str);
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                return new BadRequestObjectResult(problemDetails);
            }

            List<PayScaleResponse> payScales = await ipay.GetPayScales();
            await hub.Clients.All.SendAsync("PopulatePayScales", payScales);
            return Ok(str);
        } // Save...
    } // class...
}
