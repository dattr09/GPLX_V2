using System;
using System.Collections.Generic;

namespace GPLX.Models;

public partial class DkthiGplx
{
    public int MaDkthiGplx { get; set; }

    public string Cccd { get; set; } = null!;

    public string MaLoai { get; set; } = null!;

    public DateOnly NgayThi { get; set; }

    public int MaTtsh { get; set; }

    public virtual CongDan CccdNavigation { get; set; } = null!;

    public virtual KetQuaThiGplx? KetQuaThiGplx { get; set; }

    public virtual LoaiGplx MaLoaiNavigation { get; set; } = null!;

    public virtual TrungTamSatHach MaTtshNavigation { get; set; } = null!;
}
