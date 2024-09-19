using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessEntity.Models
{
    public class modCompose
    {
        public int? Id { get; set; }
        public string? Ticket { get; set; }
        public string? name { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public string? Reply { get; set; }
        public string? RegNo { get; set; }
        public string? ToRegNo { get; set; }
        public string? sendto { get; set; }
        public int? status { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
