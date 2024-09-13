using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessEntity.Models
{
    public class modFundReceive
    {
        public string? regno { get; set; }
        public string? from_id { get; set; }
        public decimal? amount { get; set; }
        public int? status { get; set; }
        public string? createdate { get; set; }
        public string? remarks { get; set; }
    }
}
