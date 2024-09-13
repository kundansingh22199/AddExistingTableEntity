using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblBankDetail
{
    public int Id { get; set; }

    public string? Regno { get; set; }

    public decimal? Amount { get; set; }

    public string? Pmode { get; set; }

    public string? BankName { get; set; }

    public string? Remark { get; set; }

    public string? Receipt { get; set; }

    public DateTime? DepositeDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? Status { get; set; }

    public string? BranchName { get; set; }

    public string? AccountHname { get; set; }

    public string? Utrno { get; set; }

    public decimal? PaidAmt { get; set; }

    public DateTime? Approvedate { get; set; }

    public string? Mobileno { get; set; }

    public string? Upiid { get; set; }

    public string? AccountNo { get; set; }

    public decimal? Usd { get; set; }
}
