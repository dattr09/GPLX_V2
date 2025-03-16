using System;
using System.Collections.Generic;

namespace GPLX.Models;

public partial class DieuKienDatGplx
{
    public int MaDkdat { get; set; }

    public string MaLoai { get; set; } = null!;

    public int DiemLyThuyetDat { get; set; }

    public int DiemThucHanhDat { get; set; }

    public int? DiemMoPhongDat { get; set; }

    public int? DiemDuongTruongDat { get; set; }

    public string? GhiChu { get; set; }

    public virtual LoaiGplx MaLoaiNavigation { get; set; } = null!;
}
