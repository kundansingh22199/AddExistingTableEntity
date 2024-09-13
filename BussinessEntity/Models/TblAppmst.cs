using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblAppmst
{
    public int Id { get; set; }

    public string? RegNo { get; set; }

    public string? Name { get; set; }

    public string? MobileNo { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? TranPassword { get; set; }

    public int? LeftRight { get; set; }

    public string? Pid { get; set; }

    public string? Sid { get; set; }

    public DateTime? Doj { get; set; }

    public decimal? JoinType { get; set; }

    public DateTime? ActiveDate { get; set; }

    public string? Country { get; set; }

    public string? Zipcode { get; set; }

    public DateTime? Lastlogin { get; set; }

    public int? Oprank { get; set; }
}
