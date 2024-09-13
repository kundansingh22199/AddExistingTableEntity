using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblTransactionDetail
{
    public int Id { get; set; }

    public string? RegNo { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? ActiveDate { get; set; }

    public string? Remarks { get; set; }

    public string? FromId { get; set; }

    public int? Status { get; set; }
}
