using Domain.Department;
using Domain.Designation;
using Infrastructure.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Repository.Designation;
using Services.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Designation
{
    namespace NDesignation
    {
        internal sealed class DALClass : IDesignationResponse
        {
            private readonly SampleContext context;

            public DALClass(SampleContext context)
            {
                this.context = context;
            } // constructor...
            
            public async Task<DesignationResponse> GetDesignationById(int id)
            {
                var designation = await
                (from cs in context.Designations
                 where cs.IdNo == id
                 select new DesignationResponse
                 {
                     IdNo = cs.IdNo,
                     Name = cs.Name
                 }).FirstOrDefaultAsync();

                return designation!;
            } // GetDesignationById...

            public async Task<List<DesignationResponse>> GetDesignations()
            {
                return await
                (from cs in context.Designations
                 select new DesignationResponse
                 {
                     IdNo = cs.IdNo,
                     Name = cs.Name
                 }).ToListAsync();
            } // GetDesignations...

            public async Task<string> Save(DesignationResponse designation)
            {
                string message = string.Empty;
                await using var trans = await context.Database.BeginTransactionAsync();

                try
                {
                    if(string.IsNullOrWhiteSpace(designation.Name))
                    {
                        message = "Designation Name cannot be blank.";
                        return message;
                    } // end if...

                    if (designation.IdNo == 0)
                    {
                        Infrastructure.Models.Designation d = new Infrastructure.Models.Designation();
                        d.Name = designation.Name;
                        await context.AddAsync(d);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        var existingDesignation = await context.Designations.FirstOrDefaultAsync(m => m.IdNo == designation.IdNo);
                        existingDesignation!.Name = designation.Name;
                        context.Update(existingDesignation);
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
    } // namespace NDesignation...
}
