using System;
using System.Collections.Generic;

namespace GPLX.Models;

public partial class CongDan
{
    public string Cccd { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public DateOnly NgaySinh { get; set; }

    public bool GioiTinh { get; set; }

    public string QuocTich { get; set; } = null!;

    public string? DiaChi { get; set; }

    public string? SoDienThoai { get; set; }

    public virtual ICollection<DkthiGplx> DkthiGplxes { get; set; } = new List<DkthiGplx>();
}
