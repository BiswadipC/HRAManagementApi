using Domain.Designation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Designation
{
    public interface IDesignationResponse
    {
        Task<List<DesignationResponse>> GetDesignations();
        Task<DesignationResponse> GetDesignationById(int id);
        Task<string> Save(DesignationResponse designation);
    } // interface...
}
