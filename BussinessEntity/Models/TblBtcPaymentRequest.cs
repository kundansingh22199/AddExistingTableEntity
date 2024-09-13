using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblBtcPaymentRequest
{
    public int Id { get; set; }

    public string? Regno { get; set; }

    public decimal? Usd { get; set; }

    public string? Address { get; set; }

    public decimal? Btc { get; set; }

    public int? St { get; set; }

    public string? TranNo { get; set; }

    public DateTime? Createdate { get; set; }

    public DateTime? Approvedate { get; set; }

    public string? Remark { get; set; }

    public string? AdminRemark { get; set; }
}
