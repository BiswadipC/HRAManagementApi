using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PayScale
{
    public class PayScaleResponse
    {
        public int IdNo {  get; set; }
        public string PayScaleCode {  get; set; } = string.Empty;
        public decimal Basic { get; set; } = 0.00m;
        public decimal? HRAPerc {  get; set; } = decimal.Zero;
        public decimal? DAPerc { get; set; } = decimal.Zero;
        public decimal? ConveanceAllowance { get; set; } = decimal.Zero;
        public decimal? MedicalAllowance { get; set; } = decimal.Zero;
        public decimal? OtherFixedAllowances { get; set; } = decimal.Zero;
    } // PayScaleResponse...
}
