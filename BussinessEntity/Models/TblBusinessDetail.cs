using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblBusinessDetail
{
    public int Id { get; set; }

    public string? RegNo { get; set; }

    public int? LeftTotal { get; set; }

    public int? RightTotal { get; set; }

    public int? CarryLeft { get; set; }

    public int? CarryRight { get; set; }

    public decimal? LeftId { get; set; }

    public decimal? RightId { get; set; }

    public decimal? TotalLeftId { get; set; }

    public decimal? TotalRightId { get; set; }

    public int? LeftActive { get; set; }

    public int? RightActive { get; set; }
}
