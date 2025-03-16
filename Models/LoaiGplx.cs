using System;
using System.Collections.Generic;

namespace GPLX.Models;

public partial class LoaiGplx
{
    public string MaLoai { get; set; } = null!;

    public string TenLoai { get; set; } = null!;

    public string? MoTa { get; set; }

    public byte DoTuoiDuocCap { get; set; }

    public virtual ICollection<DieuKienDatGplx> DieuKienDatGplxes { get; set; } = new List<DieuKienDatGplx>();

    public virtual ICollection<DkthiGplx> DkthiGplxes { get; set; } = new List<DkthiGplx>();
}
