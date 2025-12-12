using Domain.Department;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Department
{
    namespace NDepartment
    {
        internal sealed class DALClass : IDepartmentResponse
        {
            private readonly SampleContext context;

            public DALClass(SampleContext context)
            {
                this.context = context;
            } // constructor...

            public async Task<List<DepartmentResponse>> GetDepartments()
            {
                return await
                (from cs in context.Departments
                 select new DepartmentResponse
                 {
                     IdNo = cs.IdNo,
                     Name = cs.Name
                 }).ToListAsync();
            } // GetDepartments...

            public async Task<DepartmentResponse> GetDepartmentById(int id)
            {
                var department = await
                (from cs in context.Departments
                 where cs.IdNo == id
                 select new DepartmentResponse
                 {
                     IdNo = cs.IdNo,
                     Name = cs.Name
                 }).FirstOrDefaultAsync();

                return department!;
            } // GetDepartmentById...

            public async Task<string> Save(DepartmentResponse department)
            {
                string message = string.Empty;
                await using var trans = await context.Database.BeginTransactionAsync();

                try
                {
                    if(department.IdNo == 0)
                    {
                        Infrastructure.Models.Department d = new Infrastructure.Models.Department();
                        d.Name = department.Name;
                        await context.AddAsync(d);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        var existingDepartment = await context.Departments.FirstOrDefaultAsync(m => m.IdNo == department.IdNo);
                        existingDepartment!.Name = department.Name;
                        context.Update(existingDepartment);
                        await context.SaveChangesAsync();
                    } // end if...

                    await trans.CommitAsync();
                    message = "Success";
                }
                catch (Exception ex)
                {
                    await trans.RollbackAsync();
                    message = ex.ToString();
                }
                finally
                {
                    trans?.Dispose();
                }

                return message;
            } // Save...
        } // class...
    } // namespace NDepartment...
}
