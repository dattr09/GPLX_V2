using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GPLX.Models;

public partial class KetQuaThiGplx
{
    [Key]
    [Column(TypeName = "VARCHAR(20)")] // 🔥 Đảm bảo SQL Server hỗ trợ
    [StringLength(5, ErrorMessage = "Mã Kết Quả tối đa 5 ký tự.")]
    public string MaKetQua { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng chọn đăng ký thi GPLX")]
    public int MaDkthiGplx { get; set; }

    public int DiemLyThuyet { get; set; }

    public int DiemThucHanh { get; set; }

    public int? DiemMoPhong { get; set; }

    public int? DiemDuongTruong { get; set; }

    public string? GhiChu { get; set; }

    public string KetQua { get; set; } = null!;

    public virtual ICollection<Gplx> Gplxes { get; set; } = new List<Gplx>();
    [NotMapped] // 🔥 Tránh bind từ form
    [JsonIgnore] // 🔥 Tránh bind từ form
    public virtual DkthiGplx? MaDkthiGplxNavigation { get; set; } = null!;
}
