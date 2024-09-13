using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblBinaryIncome
{
    public int Id { get; set; }

    public string? RegNo { get; set; }

    public decimal? LeftTotal { get; set; }

    public decimal? RightTotal { get; set; }

    public decimal? Income { get; set; }

    public decimal? Admin { get; set; }

    public decimal? Tds { get; set; }

    public decimal? NetBalance { get; set; }

    public decimal? Carryleft { get; set; }

    public decimal? Carryright { get; set; }

    public DateTime? CreateDate { get; set; }
}
