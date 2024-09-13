using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblControlMst
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }
}
