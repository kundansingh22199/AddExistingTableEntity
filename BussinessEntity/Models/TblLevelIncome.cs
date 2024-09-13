using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblLevelIncome
{
    public int Id { get; set; }

    public string? RegNo { get; set; }

    public string? Sid { get; set; }

    public decimal? TotalAmt { get; set; }

    public decimal? Income { get; set; }

    public int? LevelNo { get; set; }

    public decimal? Tds { get; set; }

    public decimal? Admin { get; set; }

    public decimal? NetAmt { get; set; }

    public string? Type { get; set; }

    public DateTime? CreateDate { get; set; }
}
