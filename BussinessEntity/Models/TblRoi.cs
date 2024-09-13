using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblRoi
{
    public int Id { get; set; }

    public string? RegNo { get; set; }

    public decimal? TotalAmt { get; set; }

    public decimal? Roi { get; set; }

    public decimal? Tds { get; set; }

    public decimal? Admin { get; set; }

    public decimal? NetAmt { get; set; }

    public int? Status { get; set; }

    public int? TranId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? Doj { get; set; }
}
