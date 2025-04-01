using System;
using System.Collections.Generic;

namespace GPLX.Models;

public partial class KhoaHoc
{
    public int MaKhoaHoc { get; set; }

    public string TenKhoaHoc { get; set; } = null!;

    public string? ThoiGianHoc { get; set; }

    public decimal HocPhi { get; set; }

    public string? MoTa { get; set; }

    public string MaLoai { get; set; } = null!;

    public virtual ICollection<LopHoc> LopHocs { get; set; } = new List<LopHoc>();
}
