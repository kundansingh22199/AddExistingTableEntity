using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblKycDetail
{
    public int Id { get; set; }

    public string? AppMstRegNo { get; set; }

    public string? AccountName { get; set; }

    public string? AccountNo { get; set; }

    public string? Ifsc { get; set; }

    public string? Bank { get; set; }

    public string? Branch { get; set; }

    public string? PancardNo { get; set; }

    public string? PancardDoc { get; set; }

    public string? CryptoAddress { get; set; }

    public int? Status { get; set; }

    public DateTime? Createdate { get; set; }

    public DateTime? Activedate { get; set; }
}
