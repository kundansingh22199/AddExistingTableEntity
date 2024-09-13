using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblAllIncome
{
    public int Id { get; set; }

    public string? RegNo { get; set; }

    public decimal? Roi { get; set; }

    public decimal? Direct { get; set; }

    public decimal? Binary { get; set; }

    public decimal? LevelRoi { get; set; }

    public decimal? Withdraw { get; set; }

    public decimal? TransFund { get; set; }

    public decimal? ReceiveFund { get; set; }

    public decimal? FundBalance { get; set; }

    public decimal? Balance { get; set; }
}
