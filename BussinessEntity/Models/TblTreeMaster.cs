using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblTreeMaster
{
    public int Id { get; set; }

    public string? RegNo { get; set; }

    public string? LeftNode { get; set; }

    public string? RightNode { get; set; }

    public string? Pid { get; set; }

    public string? Sid { get; set; }
}
