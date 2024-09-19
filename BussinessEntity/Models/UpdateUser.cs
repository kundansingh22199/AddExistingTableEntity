using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessEntity.Models
{
    public class UpdateUser
    {
        public string SId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfPassword { get; set; }
        public string TransPassword { get; set; }
        public string ConfTranPassword { get; set; }
        public string Status { get; set; }
        public string Doj { get; set; }
        public string Mobile { get; set; }
    }
}
