using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblNews
{
    public int Id { get; set; }

    public string? Queary { get; set; }

    public int? Type { get; set; }
}
