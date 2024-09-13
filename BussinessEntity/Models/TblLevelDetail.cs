using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblLevelDetail
{
    public int Id { get; set; }

    public string? RegNo { get; set; }

    public string? Pid { get; set; }

    public short? Position { get; set; }

    public int? LevelNo { get; set; }

    public DateTime? CreateDate { get; set; }
}
