using System;
using System.Collections.Generic;

namespace GPLX.Models;

public partial class Gplx
{
    public string MaGplx { get; set; } = null!;

    public string MaKetQua { get; set; } = null!;

    public DateOnly NgayCap { get; set; }

    public DateOnly NgayHetHan { get; set; }

    public virtual KetQuaThiGplx MaKetQuaNavigation { get; set; } = null!;

    public virtual ICollection<ViPhamGplx> ViPhamGplxes { get; set; } = new List<ViPhamGplx>();
}
