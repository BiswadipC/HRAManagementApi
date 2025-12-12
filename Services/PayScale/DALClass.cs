using Domain.PayScale;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Repository.PayScale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PayScale
{
    internal class DALClass : IPayScaleResponse
    {
        private readonly SampleContext context;

        public DALClass(SampleContext context)
        {
            this.context = context;
        } // constructor...

        public async Task<List<PayScaleResponse>> GetPayScales()
        {
            var payScales = await (from cs in context.PayScales
                                   select new PayScaleResponse
                                   {
                                       IdNo = cs.IdNo,
                                       PayScaleCode = cs.PayScaleCode,
                                       Basic = cs.BasicSalary,
                                       HRAPerc = cs.HraPercentage,
                                       DAPerc = cs.DaPercentage,
                                       ConveanceAllowance = cs.ConveyanceAllowance,
                                       MedicalAllowance = cs.MedicalAllowance,
                                       OtherFixedAllowances = cs.OtherFixedAllowances
                                   }).ToListAsync();
            return payScales;
        } // GetPayScales...

        public async Task<PayScaleResponse> GetPayScaleById(int id)
        {
            var payScale = await (from cs in context.PayScales
                                  where cs.IdNo == id
                                   select new PayScaleResponse
                                   {
                                       IdNo = cs.IdNo,
                                       PayScaleCode = cs.PayScaleCode,
                                       Basic = cs.BasicSalary,
                                       HRAPerc = cs.HraPercentage,
                                       DAPerc = cs.DaPercentage,
                                       ConveanceAllowance = cs.ConveyanceAllowance,
                                       MedicalAllowance = cs.MedicalAllowance,
                                       OtherFixedAllowances = cs.OtherFixedAllowances
                                   }).FirstOrDefaultAsync();
            return payScale!;
        } // GetPayScaleById...

        public async Task<string> Save(PayScaleResponse response)
        {
            string message = string.Empty;
            await using var trans = await context.Database.BeginTransactionAsync();

            try
            {
                if(response.IdNo == 0)
                {
                    Infrastructure.Models.PayScale ps = new Infrastructure.Models.PayScale();
                    ps.IdNo = response.IdNo;
                    ps.PayScaleCode = response.PayScaleCode;
                    ps.BasicSalary = response.Basic;
                    ps.HraPercentage = response.HRAPerc;
                    ps.DaPercentage = response.DAPerc;
                    ps.ConveyanceAllowance = response.ConveanceAllowance;
                    ps.MedicalAllowance = response.MedicalAllowance;
                    ps.OtherFixedAllowances = response.OtherFixedAllowances;
                    await context.AddAsync(ps);
                    await context.SaveChangesAsync();
                }
                else
                {
                    var existingPayScale = await context.PayScales.FirstOrDefaultAsync(m => m.IdNo == response.IdNo);
                    existingPayScale!.BasicSalary = response.Basic;
                    existingPayScale.HraPercentage = response.HRAPerc;
                    existingPayScale.DaPercentage = response.DAPerc;
                    existingPayScale.ConveyanceAllowance = response.ConveanceAllowance;
                    existingPayScale.MedicalAllowance = response.MedicalAllowance;
                    existingPayScale.OtherFixedAllowances = response.OtherFixedAllowances;
                    context.Update(existingPayScale);
                    await context.SaveChangesAsync();
                } // end if...

                await trans.CommitAsync();
                message = "Success";
            }
            catch(Exception e)
            {
                await trans.RollbackAsync();
                message = e.ToString();
            }
            finally
            {
                trans.Dispose();
            }

            return message;
        } // Save...
    } // class...
}
