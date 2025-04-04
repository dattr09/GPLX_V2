using System;
using System.Collections.Generic;

namespace GPLX.Models;

public partial class DangKyKhoaHoc
{
    public int MaDkkh { get; set; }

    public string Cccd { get; set; } = null!;

    public int MaLop { get; set; }

    public DateOnly NgayDangKy { get; set; }

    public string? TrangThaiDangKy { get; set; }

    public string? GhiChu { get; set; }

    public virtual LopHoc? MaLopNavigation { get; set; } 
}
