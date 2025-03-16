using System;
using System.Collections.Generic;

namespace GPLX.Models;

public partial class KetQuaThiGplx
{
    public string MaKetQua { get; set; } = null!;

    public int MaDkthiGplx { get; set; }

    public int DiemLyThuyet { get; set; }

    public int DiemThucHanh { get; set; }

    public int? DiemMoPhong { get; set; }

    public int? DiemDuongTruong { get; set; }

    public string? GhiChu { get; set; }

    public string KetQua { get; set; } = null!;

    public virtual ICollection<Gplx> Gplxes { get; set; } = new List<Gplx>();

    public virtual DkthiGplx MaDkthiGplxNavigation { get; set; } = null!;
}
