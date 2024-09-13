using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblMail
{
    public int Id { get; set; }

    public string? RegNo { get; set; }

    public string? Ticket { get; set; }

    public string? ToRegNo { get; set; }

    public string? SendTo { get; set; }

    public string? Subject { get; set; }

    public string? Message { get; set; }

    public string? Reply { get; set; }

    public int? Status { get; set; }

    public DateTime? CreateDate { get; set; }
}
