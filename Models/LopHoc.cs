using System;
using System.Collections.Generic;

namespace GPLX.Models;

public partial class LopHoc
{
    public int MaLop { get; set; }

    public DateOnly NgayBatDau { get; set; }

    public DateOnly NgayKetThuc { get; set; }

    public string? DiaDiem { get; set; }

    public string? ThoiGianHocTrongTuan { get; set; }

    public int? SoLuongToiDa { get; set; }

    public string? GhiChu { get; set; }

    public string? TrangThai { get; set; }

    public bool? IsOnline { get; set; }

    public int MaKhoaHoc { get; set; }

    public int MaGv { get; set; }

    public virtual ICollection<DangKyKhoaHoc> DangKyKhoaHocs { get; set; } = new List<DangKyKhoaHoc>();

    public virtual GiangVien MaGvNavigation { get; set; } = null!;

    public virtual KhoaHoc MaKhoaHocNavigation { get; set; } = null!;
}
