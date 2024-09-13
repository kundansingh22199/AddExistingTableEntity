using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessEntity.Models
{
    public class ModTeamDetails
    {
        public string? regno { get; set; }
        public string? sid { get; set; }
        public string? name { get; set; }
        public decimal? jointype { get; set; }
        public string? paidunpaid { get; set; }
        public DateTime? doj { get; set; }
        public int? levelno { get; set; }
        public string? activedate { get; set; }
    }
}
