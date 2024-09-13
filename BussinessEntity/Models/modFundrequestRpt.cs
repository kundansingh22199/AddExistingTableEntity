using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessEntity.Models
{
    public class modFundrequestRpt
    {
        public string? Regno { get; set; }
        public decimal? amount { get; set; }
        public decimal? usd { get; set; }
        public string? Utrno { get; set; }
        public string? Remarks { get; set; }
        public string? pmode { get; set; }
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public string? Name { get; set; }
        public string? AccountNo { get; set; }
        public string? UpiId { get; set; }
        public string? Status { get; set; }
        public string? CreateDate { get; set; }
    }
}
