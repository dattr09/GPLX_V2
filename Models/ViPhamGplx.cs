using System;
using System.Collections.Generic;

namespace GPLX.Models;

public partial class ViPhamGplx
{
    public int MaViPham { get; set; }

    public string MaGplx { get; set; } = null!;

    public int MaLoaiViPham { get; set; }

    public DateOnly NgayViPham { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual Gplx? MaGplxNavigation { get; set; } = null!;

    public virtual LoaiViPham? MaLoaiViPhamNavigation { get; set; } = null!;
}
