using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessEntity.Models
{
    public class modUsdtRequest
    {
        public string Regno { get; set; }
        public string Name { get; set; }
        public decimal usd { get; set; }
        public string address { get; set; }
        public string TranNo { get; set; }
        public string? Remark { get; set; }
        public string status { get; set; }
        public string Createdate { get; set; }
    }
}
