using System;
using System.Collections.Generic;

namespace BussinessEntity.Models;

public partial class TblCountryMaster
{
    public int CountryMasterId { get; set; }

    public string? CountryCode { get; set; }

    public string? CountryName { get; set; }

    public int? StatusId { get; set; }
}
