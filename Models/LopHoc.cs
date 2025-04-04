using System;
using System.Collections.Generic;

namespace GPLX.Models
{
    public partial class LopHoc
    {
        public int MaLop { get; set; } // Khóa chính

        public DateTime NgayBatDau { get; set; } // Ngày bắt đầu lớp học

        public DateTime NgayKetThuc { get; set; } // Ngày kết thúc lớp học

        public string? DiaDiem { get; set; } // Địa điểm lớp học

        public string? ThoiGianHocTrongTuan { get; set; } // Thời gian học trong tuần

        public int? SoLuongToiDa { get; set; } // Số lượng học viên tối đa

        public string? GhiChu { get; set; } // Ghi chú

        public string? TrangThai { get; set; } // Trạng thái lớp học (Đang mở, Đã đóng, Đã huỷ)

        public bool? IsOnline { get; set; } // Hình thức học (Online/Offline)

        public int MaKhoaHoc { get; set; } // Khóa ngoại tham chiếu đến bảng KhoaHoc

        public int MaGv { get; set; } // Khóa ngoại tham chiếu đến bảng GiangVien

        // Navigation properties - Chúng ta không cần gán trực tiếp từ form
        public virtual GiangVien? MaGvNavigation { get; set; } = null!; // Dùng để ánh xạ thông tin giảng viên từ MaGv
        public virtual KhoaHoc? MaKhoaHocNavigation { get; set; } = null!; // Dùng để ánh xạ thông tin khóa học từ MaKhoaHoc

        public virtual ICollection<DangKyKhoaHoc> DangKyKhoaHocs { get; set; } = new List<DangKyKhoaHoc>(); // Mối quan hệ với bảng DangKyKhoaHoc
    }
}
