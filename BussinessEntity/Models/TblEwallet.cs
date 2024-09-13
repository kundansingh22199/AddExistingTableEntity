using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblEwallet
{
    public int Id { get; set; }

    public string? RegNo { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? Active { get; set; }

    public DateTime? ActiveDate { get; set; }

    public decimal? Withdraw { get; set; }

    public int? Type { get; set; }

    public string? Wallet { get; set; }

    public string? CreditIn { get; set; }

    public string? BankName { get; set; }

    public string? AccountNo { get; set; }

    public string? Ifsc { get; set; }

    public string? Remarks { get; set; }

    public string? Usdtaddress { get; set; }
}
