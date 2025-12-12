using Domain.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Department
{
    public interface IDepartmentResponse
    {
        Task<List<DepartmentResponse>> GetDepartments();
        Task<DepartmentResponse> GetDepartmentById(int id);
        Task<string> Save(DepartmentResponse department);
    } // interface...
}
