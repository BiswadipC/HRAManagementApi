using Domain.PayScale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PayScale
{
    public interface IPayScaleResponse
    {
        Task<List<PayScaleResponse>> GetPayScales();
        Task<PayScaleResponse> GetPayScaleById(int id);
        Task<string> Save(PayScaleResponse response);
    } // interface...
}
