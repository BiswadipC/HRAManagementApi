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

            public async Task<DesignationAction> Save(DesignationResponse designation)
            {
                string message = string.Empty;
                await using var trans = await context.Database.BeginTransactionAsync();
                DesignationResponse response = new DesignationResponse();
                DesignationAction da = new DesignationAction();

                try
                {
                    if(string.IsNullOrWhiteSpace(designation.Name))
                    {
                        message = "Designation Name cannot be blank.";
                        throw new Exception("Designation Name cannot be blank.");
                    } // end if...

                    if (designation.IdNo == 0)
                    {
                        Infrastructure.Models.Designation d = new Infrastructure.Models.Designation();
                        d.Name = designation.Name;
                        await context.AddAsync(d);
                        await context.SaveChangesAsync();

                        response.IdNo = d.IdNo;
                        response.Name = d.Name;
                        da.Action = "Insert";
                        da.Data = response;
                    }
                    else
                    {
                        var existingDesignation = await context.Designations.FirstOrDefaultAsync(m => m.IdNo == designation.IdNo);
                        existingDesignation!.Name = designation.Name;
                        context.Update(existingDesignation);
                        await context.SaveChangesAsync();

                        response.IdNo = designation.IdNo;
                        response.Name = designation.Name;
                        da.Action = "Update";
                        da.Data = response;
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

                return da;
            } // Save...
        } // class...
    } // namespace NDesignation...
}
