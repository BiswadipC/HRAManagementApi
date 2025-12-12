using Domain.Department;
using Domain.Designation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Repository.Designation;
using Services.SignalR;
using System.Collections.Generic;

namespace MainProject.Controllers
{
    [Route("designations")]
    [ApiController]
    
    public class DesignationApiController : ControllerBase
    {
        private readonly IDesignationResponse idesignation;
        private readonly IHubContext<DesignationHub> hub;

        public DesignationApiController(IDesignationResponse idesignation, IHubContext<DesignationHub> hub)
        {
            this.idesignation = idesignation;
            this.hub = hub;
        } // constructor...

        [HttpGet("")]
        [Authorize(Policy = "VIEW_DESIGNATION")]
        public async Task<IActionResult> GetAllDesignations()
        {
            var designations = await idesignation.GetDesignations();
            return Ok(designations);
        } // GetAllDesignations...

        [HttpGet("{id}")]
        [Authorize(Policy = "EDIT_DESIGNATION")]
        public async Task<IActionResult> GetDesignationById(int id)
        {
            var designation = await idesignation.GetDesignationById(id);
            return Ok(designation);
        } // GetDesignationById...

        [HttpPost("")]
        [Authorize(Policy = "EDIT_DESIGNATION")]
        public async Task<IActionResult> Save(DesignationResponse designation)
        {
            string str = await idesignation.Save(designation);
            List<DesignationResponse> designations = await idesignation.GetDesignations();
            await hub.Clients.All.SendAsync("ReceiveMessage", designations);

            return Ok(str);
        } // Save...
    } // class...
}
