using System;
using System.Collections.Generic;

namespace GPLX.Models;

public partial class LoaiViPham
{
    public int MaLoaiViPham { get; set; }

    public string TenViPham { get; set; } = null!;

    public decimal MucPhat { get; set; }

    public virtual ICollection<ViPhamGplx> ViPhamGplxes { get; set; } = new List<ViPhamGplx>();
}
