using Domain.Department;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Department;

namespace MainProject.Controllers
{
    [Route("departments")]
    [ApiController]

    public class DepartmentApiController : ControllerBase
    {
        private readonly IDepartmentResponse idept;

        public DepartmentApiController(IDepartmentResponse idept)
        {
            this.idept = idept;
        } // constructor...

        [HttpGet("")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await idept.GetDepartments();
            return Ok(departments);
        } // GetAllDepartments...

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await idept.GetDepartmentById(id);
            return Ok(department);
        } // GetDepartmentById...

        [HttpPost("")]
        public async Task<IActionResult> Save(DepartmentResponse department)
        {
            string str = await idept.Save(department);
            return Ok(str);
        } // Save...
    } // class...
}
