using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Designation
{
    public class DesignationResponse
    {
        public int IdNo { get; set; }
        public string Name { get; set; } = string.Empty;
    } // class...

    public class DesignationAction
    {
        public string Action { get; set; } = string.Empty;
        public DesignationResponse Data {  get; set; } = new DesignationResponse();
    } // class...
}
