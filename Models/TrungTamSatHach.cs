using System;
using System.Collections.Generic;

namespace GPLX.Models;

public partial class TrungTamSatHach
{
    public int MaTtsh { get; set; }

    public string TenTrungTam { get; set; } = null!;

    public string? DiaChi { get; set; }

    public string? SoDt { get; set; }

    public virtual ICollection<DkthiGplx> DkthiGplxes { get; set; } = new List<DkthiGplx>();
}
