using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblManageFund
{
    public int Id { get; set; }

    public string? RegNo { get; set; }

    public decimal? Amount { get; set; }

    public string? FromId { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? Status { get; set; }

    public string? Remarks { get; set; }
}
