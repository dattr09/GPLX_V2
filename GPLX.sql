-- ===================================
-- 1️ TẠO CƠ SỞ DỮ LIỆU
-- ===================================
CREATE DATABASE QL_GPLX;
GO

USE QL_GPLX;
GO

-- ===================================
-- 2️ TẠO CÁC BẢNG CƠ SỞ
-- ===================================

-- 📌 Bảng LoaiGPLX: Chứa thông tin về các loại giấy phép lái xe
CREATE TABLE LoaiGPLX (
    MaLoai CHAR(3) PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255),
    DoTuoiDuocCap TINYINT NOT NULL,

    CONSTRAINT CK_LoaiGPLX_MaLoai CHECK (
        MaLoai IN ('A1', 'A', 'B1', 'B', 'C1', 'C', 'D1', 'D2', 'D', 'BE', 'C1E', 'CE', 'D1E', 'D2E', 'DE')
    )
);
GO

-- 📌 Bảng CongDan: Lưu thông tin công dân đăng ký thi GPLX
CREATE TABLE CongDan (
    CCCD CHAR(12) PRIMARY KEY,
    HoTen NVARCHAR(50) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh BIT NOT NULL,  
    QuocTich NVARCHAR(30) NOT NULL DEFAULT N'Việt Nam',
    DiaChi NVARCHAR(100),
    SoDienThoai VARCHAR(10),

    CONSTRAINT CK_CongDan_CCCD CHECK (LEN(CCCD) = 12 AND CCCD NOT LIKE '%[^0-9]%'),
    CONSTRAINT CK_CongDan_SoDienThoai CHECK (SoDienThoai LIKE '0[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
    CONSTRAINT CK_CongDan_HoTen CHECK (
         HoTen NOT LIKE '%[0-9]%' AND
    HoTen NOT LIKE '%[^a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠƯ'
                + 'àáâãèéêìíòóôõùúăđĩũơưẠẢẤẦẨẪẬẮẰẲẴẶ'
                + 'ẸẺẼỀỀỂỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢ'
                + 'ỤỦỨỪỬỮỰỲỴÝỶỸỳỵỷỹ '
                + '-''%'
    )
);
GO

-- 📌 Bảng TrungTamSatHach: Lưu thông tin về trung tâm sát hạch GPLX
CREATE TABLE TrungTamSatHach (
    MaTTSH INT IDENTITY(1,1) PRIMARY KEY,
    TenTrungTam NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255),
    SoDT VARCHAR(10),

    CONSTRAINT CK_TrungTamSatHach_SoDT CHECK (SoDT LIKE '0[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
);
GO

-- 📌 Bảng DKThiGPLX: Lưu thông tin công dân đăng ký thi GPLX
CREATE TABLE DKThiGPLX (
    MaDKThiGPLX INT IDENTITY(1,1) PRIMARY KEY,
    CCCD CHAR(12) NOT NULL,
    MaLoai CHAR(3) NOT NULL,
    NgayThi DATE NOT NULL,
    MaTTSH INT NOT NULL,

    CONSTRAINT FK_DKThi_CCCD FOREIGN KEY (CCCD) REFERENCES CongDan(CCCD),
    CONSTRAINT FK_DKThi_LoaiGPLX FOREIGN KEY (MaLoai) REFERENCES LoaiGPLX(MaLoai),
    CONSTRAINT FK_DKThi_TTSH FOREIGN KEY (MaTTSH) REFERENCES TrungTamSatHach(MaTTSH)
);
GO

-- 📌 Bảng DieuKienDatGPLX: Quy định điều kiện đạt GPLX
CREATE TABLE DieuKienDatGPLX (
    MaDKDat INT IDENTITY(1,1) PRIMARY KEY,  -- Mã điều kiện đạt
    MaLoai CHAR(3) NOT NULL,                -- Tham chiếu LoaiGPLX
    DiemLyThuyetDat INT NOT NULL,           -- Điểm tối thiểu lý thuyết
    DiemThucHanhDat INT NOT NULL,           -- Điểm tối thiểu thực hành (sa hình)
    DiemMoPhongDat INT NULL,                -- Điểm tối thiểu mô phỏng (nếu có)
    DiemDuongTruongDat INT NULL,            -- Điểm tối thiểu đường trường (nếu có)
    GhiChu NVARCHAR(255) NULL,              -- Ghi chú về lỗi liệt nếu có

    -- Khóa ngoại tham chiếu bảng LoaiGPLX
    CONSTRAINT FK_DieuKienDat_LoaiGPLX FOREIGN KEY (MaLoai) REFERENCES LoaiGPLX(MaLoai),

    -- Ràng buộc điểm hợp lệ (Điểm trong khoảng cho phép)
    CONSTRAINT CK_DiemLyThuyet CHECK (DiemLyThuyetDat BETWEEN 0 AND 35),
    CONSTRAINT CK_DiemThucHanh CHECK (DiemThucHanhDat BETWEEN 0 AND 100),
    CONSTRAINT CK_DiemMoPhong CHECK (DiemMoPhongDat BETWEEN 0 AND 100 OR DiemMoPhongDat IS NULL),
    CONSTRAINT CK_DiemDuongTruong CHECK (DiemDuongTruongDat BETWEEN 0 AND 100 OR DiemDuongTruongDat IS NULL)
);
GO
-- 📌 Bảng KetQuaThiGPLX: Lưu kết quả thi của công dân
CREATE TABLE KetQuaThiGPLX (
    MaKetQua NVARCHAR(10) PRIMARY KEY,      -- Mã kết quả thi (KQxxxx)
    MaDKThiGPLX INT UNIQUE NOT NULL,        -- Tham chiếu đến DKThiGPLX (mỗi lần thi có một kết quả)
    DiemLyThuyet INT NOT NULL,              -- Điểm lý thuyết (0 - 35)
    DiemThucHanh INT NOT NULL,              -- Điểm thực hành (0 - 100)
    DiemMoPhong INT NULL,                   -- Điểm mô phỏng (nếu có) (0 - 100)
    DiemDuongTruong INT NULL,               -- Điểm đường trường (nếu có) (0 - 100)
    GhiChu NVARCHAR(255),                   -- Các lỗi liệt: câu liệt, không đội nón, quá tốc độ...
    KetQua NVARCHAR(10) NOT NULL,           -- Kết quả "Đậu" hoặc "Rớt"

    -- Khóa ngoại tham chiếu bảng DKThiGPLX
    CONSTRAINT FK_KetQua_DKThi FOREIGN KEY (MaDKThiGPLX) REFERENCES DKThiGPLX (MaDKThiGPLX),

    -- Ràng buộc giá trị điểm hợp lệ
    CONSTRAINT CK_DiemLyThuyetThi CHECK (DiemLyThuyet BETWEEN 0 AND 35),
    CONSTRAINT CK_DiemThucHanhThi CHECK (DiemThucHanh BETWEEN 0 AND 100),
    CONSTRAINT CK_DiemMoPhongThi CHECK (DiemMoPhong BETWEEN 0 AND 100 OR DiemMoPhong IS NULL),
    CONSTRAINT CK_DiemDuongTruongThi CHECK (DiemDuongTruong BETWEEN 0 AND 100 OR DiemDuongTruong IS NULL),

    -- Ràng buộc kết quả chỉ nhận "Đậu" hoặc "Rớt"
    CONSTRAINT CK_KetQuaThi CHECK (KetQua IN (N'Đậu', N'Rớt'))
);
GO



-- 📌 Bảng GPLX: Lưu thông tin GPLX cấp cho công dân
CREATE TABLE GPLX (
    MaGPLX CHAR(12) PRIMARY KEY,
    MaKetQua NVARCHAR(10) NOT NULL,
    NgayCap DATE NOT NULL DEFAULT GETDATE(),
    NgayHetHan DATE NOT NULL,

    CONSTRAINT FK_GPLX_KetQuaThiGPLX FOREIGN KEY (MaKetQua) REFERENCES KetQuaThiGPLX(MaKetQua),
    CONSTRAINT CK_GPLX_MAGPLX CHECK (LEN(MaGPLX) = 12 AND MaGPLX NOT LIKE '%[^0-9]%')
);
GO

-- 📌 Bảng vi phạm: Lưu những loại vi phạm
CREATE TABLE LoaiViPham (
    MaLoaiViPham INT IDENTITY(1,1) PRIMARY KEY,  -- Mã loại vi phạm tự động tăng
    TenViPham NVARCHAR(255) NOT NULL,  -- Mô tả loại vi phạm
    MucPhat DECIMAL(10,2) NOT NULL CHECK (MucPhat >= 0)  -- Mức phạt tiền tiêu chuẩn
);
GO

-- 📌 Bảng vi phạm: Lưu lại lịch sử vi phạm giao thông của tài xế có GPLX.
CREATE TABLE ViPhamGPLX (
    MaViPham INT IDENTITY(1,1) PRIMARY KEY,  -- Mã vi phạm tự động tăng
    MaGPLX CHAR(12) NOT NULL,  -- Tham chiếu đến GPLX của người vi phạm
    MaLoaiViPham INT NOT NULL,  -- Tham chiếu đến bảng LoaiViPham
    NgayViPham DATE NOT NULL,  -- Ngày xảy ra vi phạm
	TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Chưa đóng phạt' 
        CHECK (TrangThai IN (N'Chưa đóng phạt', N'Đã đóng phạt', N'Khiếu nại')), 
    -- Khóa ngoại liên kết với bảng GPLX và LoaiViPham
    CONSTRAINT FK_ViPham_GPLX FOREIGN KEY (MaGPLX) REFERENCES GPLX(MaGPLX),
    CONSTRAINT FK_ViPham_LoaiViPham FOREIGN KEY (MaLoaiViPham) REFERENCES LoaiViPham(MaLoaiViPham)
);
GO

-- 📌 Bảng khóa học: Lưu lại thông tin những khóa học thi GPLX.
CREATE TABLE KhoaHoc (
    MaKhoaHoc INT IDENTITY(1,1) PRIMARY KEY,
    TenKhoaHoc NVARCHAR(100) NOT NULL,
    ThoiGianHoc NVARCHAR(50), -- Ví dụ: "2 tháng"
    HocPhi DECIMAL(10,2) NOT NULL,
    MoTa NVARCHAR(255), --Ví dụ: "Cho công dân trên 18 tuổi"
	MaLoai CHAR(3) NOT NULL, -- Tham chiếu LoaiGPLX

    CONSTRAINT FK_KhoaHoc_LoaiGPLX FOREIGN KEY (MaLoai) REFERENCES LoaiGPLX(MaLoai)
);
GO
-- 📌 Bảng giảng viên: Lưu lại thông tin giảng viên giảng dạy trong các khóa học.
CREATE TABLE GiangVien (
    MaGV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(50) NOT NULL,
    NgaySinh DATE,
    GioiTinh BIT,
    SDT VARCHAR(10),
    Email VARCHAR(100),

    CONSTRAINT CK_GV_SDT CHECK (SDT LIKE '0[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
);
GO
-- 📌 Bảng lớp học: Lưu lại thông tin các lớp học trong các khóa học.
CREATE TABLE LopHoc (
    MaLop INT IDENTITY(1,1) PRIMARY KEY,                -- Mã lớp tự động tăng
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    DiaDiem NVARCHAR(255),

    ThoiGianHocTrongTuan NVARCHAR(100),                 -- VD: "T2-T4-T6 (18h-20h)"
    SoLuongToiDa INT CHECK (SoLuongToiDa > 0),          -- Giới hạn số học viên
    GhiChu NVARCHAR(255),                               -- Ghi chú đặc biệt nếu có

    TrangThai NVARCHAR(30) DEFAULT N'Đang mở' CHECK (TrangThai IN (N'Đang mở', N'Đã đóng', N'Đã huỷ')),                                                  -- Trạng thái lớp học
    IsOnline BIT DEFAULT 0,                             -- 0: Offline | 1: Online
	MaKhoaHoc INT NOT NULL,                             -- Tham chiếu đến khoá học
    MaGV INT NOT NULL,                                  -- Giảng viên phụ trách
    -- Khóa ngoại
    CONSTRAINT FK_Lop_KhoaHoc FOREIGN KEY (MaKhoaHoc) REFERENCES KhoaHoc(MaKhoaHoc),
    CONSTRAINT FK_Lop_GiangVien FOREIGN KEY (MaGV) REFERENCES GiangVien(MaGV)
);
GO
-- 📌 Bảng đăng ký khóa học: Lưu lại thông tin các lớp học trong các khóa học.
CREATE TABLE DangKyKhoaHoc (
    MaDKKH INT IDENTITY(1,1) PRIMARY KEY,
    CCCD CHAR(12) NOT NULL,
    MaLop INT NOT NULL,
    NgayDangKy DATE NOT NULL DEFAULT GETDATE(),
    TrangThaiDangKy NVARCHAR(30) DEFAULT N'Đã đăng ký' CHECK (
        TrangThaiDangKy IN (N'Đã đăng ký', N'Đã huỷ', N'Chờ xác nhận')
    ),
    GhiChu NVARCHAR(255),

    CONSTRAINT FK_DKKH_CongDan FOREIGN KEY (CCCD) REFERENCES CongDan(CCCD),
    CONSTRAINT FK_DKKH_LopHoc FOREIGN KEY (MaLop) REFERENCES LopHoc(MaLop),
    CONSTRAINT UQ_DKKH UNIQUE (CCCD, MaLop) -- Một học viên không đăng ký 2 lần cùng lớp
);
GO





-- ===================================
-- 3️ TẠO CÁC TRIGGER RÀNG BUỘC
-- ===================================
-- 📌 TRIGGER KIỂM TRA ĐỘ TUỔI ĐĂNG KÝ THI
CREATE TRIGGER trg_Check_DoTuoi_DKThi
ON DKThiGPLX
FOR INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tuổi hợp lệ trước ngày thi
    IF EXISTS (
        SELECT 1
        FROM INSERTED i
        INNER JOIN CongDan cd ON i.CCCD = cd.CCCD
        INNER JOIN LoaiGPLX l ON i.MaLoai = l.MaLoai
        WHERE DATEADD(YEAR, l.DoTuoiDuocCap, cd.NgaySinh) > i.NgayThi
    )
    BEGIN
        RAISERROR (N'Công dân chưa đủ tuổi để đăng ký thi loại GPLX này!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- 📌 TRIGGER KIỂM TRA TÍNH HỢP LỆ CỦA KẾT QUẢ THI
CREATE TRIGGER trg_Validate_KetQuaThiGPLX
ON KetQuaThiGPLX
FOR INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- 1️ Ngăn không cho nhập "Đậu" nếu có ghi chú
    IF EXISTS (
        SELECT 1
        FROM INSERTED
        WHERE KetQua = N'Đậu' AND GhiChu IS NOT NULL
    )
    BEGIN
        RAISERROR (N'Khi "Đậu", không được có ghi chú vi phạm!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- 2️ Kiểm tra điểm có đạt đủ điều kiện không
    IF EXISTS (
        SELECT 1
        FROM INSERTED kq
        INNER JOIN DKThiGPLX dk ON kq.MaDKThiGPLX = dk.MaDKThiGPLX
        INNER JOIN DieuKienDatGPLX dkdat ON dk.MaLoai = dkdat.MaLoai
        WHERE kq.KetQua = N'Đậu'
        AND (
            kq.DiemLyThuyet < dkdat.DiemLyThuyetDat
            OR kq.DiemThucHanh < dkdat.DiemThucHanhDat
            OR (dkdat.DiemMoPhongDat IS NOT NULL AND kq.DiemMoPhong IS NOT NULL AND kq.DiemMoPhong < dkdat.DiemMoPhongDat)
            OR (dkdat.DiemDuongTruongDat IS NOT NULL AND kq.DiemDuongTruong IS NOT NULL AND kq.DiemDuongTruong < dkdat.DiemDuongTruongDat)
        )
    )
    BEGIN
        RAISERROR (N'Không thể nhập kết quả "Đậu" khi điểm không đủ điều kiện!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO

-- 📌 TRIGGER KIỂM TRA VIỆC CẤP GPLX
CREATE TRIGGER trg_Check_KetQuaDat_GPLX
ON GPLX
FOR INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM INSERTED i
        INNER JOIN KetQuaThiGPLX kq ON i.MaKetQua = kq.MaKetQua
        WHERE kq.KetQua <> N'Đậu'
    )
    BEGIN
        RAISERROR (N'Chỉ cấp GPLX cho thí sinh có kết quả "Đậu"!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- 📌 TRIGGER KIỂM TRA NGÀY CẤP GPLX PHẢI SAU NGÀY THI
CREATE TRIGGER trg_Check_NgayCap_GPLX
ON GPLX
FOR INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra ngày cấp GPLX phải sau ngày thi
    IF EXISTS (
        SELECT 1
        FROM INSERTED i
        INNER JOIN KetQuaThiGPLX kq ON i.MaKetQua = kq.MaKetQua
        INNER JOIN DKThiGPLX dk ON kq.MaDKThiGPLX = dk.MaDKThiGPLX
        WHERE i.NgayCap < dk.NgayThi
    )
    BEGIN
        RAISERROR (N'Ngày cấp GPLX phải sau ngày thi!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- 📌 TRIGGER KIỂM TRA NGÀY VI PHẠM PHẢI SAU NGÀY NHẬN GPLX
CREATE TRIGGER trg_Check_NgayViPham
ON ViPhamGPLX
FOR INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra nếu có bản ghi nào có NgayViPham trước NgayCap của GPLX
    IF EXISTS (
        SELECT 1
        FROM INSERTED i
        INNER JOIN GPLX g ON i.MaGPLX = g.MaGPLX
        WHERE i.NgayViPham < g.NgayCap
    )
    BEGIN
        RAISERROR (N'Ngày vi phạm phải sau ngày cấp GPLX!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- ===================================
-- 4 THÊM DỮ LIỆU CHO CÁC BẢNG
-- ===================================

-- 📌 Bảng Loại GPLX
INSERT INTO LoaiGPLX (MaLoai, TenLoai, MoTa, DoTuoiDuocCap)
VALUES
('A1',  N'Xe mô tô đến 125cc hoặc công suất 11kW',                        N'Xe mô tô hai bánh đến 125cc hoặc động cơ điện đến 11kW', 18),
('A',   N'Xe mô tô trên 125cc hoặc công suất trên 11kW',                 N'Xe mô tô hai bánh trên 125cc hoặc động cơ điện trên 11kW và các loại xe quy định cho hạng A1', 18),
('B1',  N'Xe mô tô ba bánh và loại xe hạng A1',                           N'Xe mô tô ba bánh và các loại xe quy định cho giấy phép lái xe hạng A1', 18),
('B',   N'Ô tô đến 8 chỗ ngồi và tải đến 3.5 tấn',                       N'Ô tô chở người đến 8 chỗ ngồi (không kể ghế lái), ô tô tải và ô tô chuyên dùng đến 3.5 tấn', 18),
('C1',  N'Ô tô tải từ 3.5 tấn đến dưới 7.5 tấn',                         N'Ô tô tải từ 3.5 tấn đến dưới 7.5 tấn', 18),
('C',   N'Ô tô tải từ 7.5 tấn trở lên',                                  N'Ô tô tải từ 7.5 tấn trở lên', 21),
('D1',  N'Ô tô chở người từ 8 đến 16 chỗ ngồi',                          N'Ô tô chở người từ 8 đến 16 chỗ ngồi (không kể ghế lái)', 24),
('D2',  N'Ô tô chở người từ 16 đến 29 chỗ ngồi',                         N'Ô tô chở người từ 16 đến 29 chỗ ngồi', 24),
('D',   N'Ô tô chở người từ 30 chỗ ngồi trở lên',                        N'Ô tô chở người từ 30 chỗ ngồi trở lên', 27),
('BE',  N'Ô tô hạng B kéo rơ moóc trên 750kg',                           N'Ô tô hạng B kéo rơ moóc có khối lượng toàn bộ theo thiết kế trên 750kg', 21),
('C1E', N'Ô tô hạng C1 kéo rơ moóc trên 750kg',                          N'Ô tô hạng C1 kéo rơ moóc có khối lượng toàn bộ theo thiết kế trên 750kg', 21),
('CE',  N'Ô tô hạng C kéo rơ moóc trên 750kg',                           N'Ô tô hạng C kéo rơ moóc có khối lượng toàn bộ theo thiết kế trên 750kg', 24),
('D1E', N'Ô tô hạng D1 kéo rơ moóc trên 750kg',                          N'Ô tô hạng D1 kéo rơ moóc có khối lượng toàn bộ theo thiết kế trên 750kg', 24),
('D2E', N'Ô tô hạng D2 kéo rơ moóc trên 750kg',                          N'Ô tô hạng D2 kéo rơ moóc có khối lượng toàn bộ theo thiết kế trên 750kg', 27),
('DE',  N'Ô tô hạng D kéo rơ moóc trên 750kg',                           N'Ô tô hạng D kéo rơ moóc có khối lượng toàn bộ theo thiết kế trên 750kg', 27);
GO
-- 📌 Bảng công dân 
INSERT INTO CongDan (CCCD, HoTen, NgaySinh, GioiTinh, QuocTich, DiaChi, SoDienThoai)
VALUES
('012345678901', N'Nguyễn Văn An', '2000-05-15', 1, N'Việt Nam', N'123 Đường Lê Lợi, Hà Nội', '0987654321'),
('012345678902', N'Trần Thị Bình', '1998-09-22', 0, N'Việt Nam', N'456 Đường Trần Hưng Đạo, TP.HCM', '0912345678'),
('012345678903', N'Lê Công Minh', '1995-12-10', 1, N'Việt Nam', N'789 Đường Nguyễn Huệ, Đà Nẵng', '0978456123'),
('012345678904', N'Phạm Thu Hằng', '2003-07-18', 0, N'Việt Nam', N'101 Đường Quang Trung, Hải Phòng', '0932123456'),
('012345678905', N'Hoàng Văn Đức', '1988-03-05', 1, N'Việt Nam', N'234 Đường Lý Thái Tổ, Cần Thơ', '0956789012'),
('012345678906', N'Bùi Thị Lan', '1990-11-30', 0, N'Việt Nam', N'567 Đường Hoàng Diệu, Huế', '0965432109'),
('012345678907', N'Đỗ Mạnh Cường', '1997-04-25', 1, N'Việt Nam', N'890 Đường Phan Chu Trinh, Quảng Ninh', '0945678932'),
('012345678908', N'Vũ Thị Ngọc', '2002-08-08', 0, N'Việt Nam', N'333 Đường Hai Bà Trưng, Vĩnh Long', '0923456789'),
('012345678909', N'Ngô Hoàng Nam', '1993-06-17', 1, N'Việt Nam', N'666 Đường Trường Chinh, Bình Dương', '0909876543'),
('012345678910', N'Trương Minh Hòa', '1985-01-20', 1, N'Việt Nam', N'999 Đường Võ Văn Kiệt, Nha Trang', '0987651234');
GO
-- 📌 Bảng trung tâm sát hạch 
INSERT INTO TrungTamSatHach (TenTrungTam, DiaChi, SoDT)
VALUES
(N'Trung tâm Sát hạch Lái xe Hà Nội', N'123 Đường Giải Phóng, Hà Nội', '0241234567'),
(N'Trung tâm Sát hạch Lái xe TP.HCM', N'456 Đường Điện Biên Phủ, TP.HCM', '0287654321'),
(N'Trung tâm Sát hạch Lái xe Đà Nẵng', N'789 Đường Nguyễn Văn Linh, Đà Nẵng', '0236372819'),
(N'Trung tâm Sát hạch Lái xe Cần Thơ', N'321 Đường 30/4, Cần Thơ', '0292387465'),
(N'Trung tâm Sát hạch Lái xe Hải Phòng', N'654 Đường Lạch Tray, Hải Phòng', '0225374912');

-- 📌 Bảng điều kiện đạt
INSERT INTO DieuKienDatGPLX (MaLoai, DiemLyThuyetDat, DiemThucHanhDat, DiemMoPhongDat, DiemDuongTruongDat, GhiChu)
VALUES
    ('A1', 21, 80, NULL, NULL, N'Rớt nếu có lỗi nghiêm trọng'),
    ('A', 23, 85, NULL, NULL, N'Rớt nếu có lỗi nghiêm trọng'),
    ('B1', 25, 80, NULL, NULL, N'Rớt nếu có lỗi nghiêm trọng'),
    ('B', 26, 80, NULL, 18, N'Rớt nếu có lỗi nghiêm trọng hoặc không hoàn thành bài thi'),
    ('C1', 26, 80, NULL, 18, N'Rớt nếu có lỗi nghiêm trọng hoặc không hoàn thành bài thi'),
    ('C', 28, 85, NULL, 18, N'Rớt nếu có lỗi nghiêm trọng hoặc không hoàn thành bài thi'),
    ('D1', 28, 85, NULL, 18, N'Rớt nếu có lỗi nghiêm trọng hoặc không hoàn thành bài thi'),
    ('D2', 28, 85, NULL, 18, N'Rớt nếu có lỗi nghiêm trọng hoặc không hoàn thành bài thi'),
    ('D', 30, 85, NULL, 18, N'Rớt nếu có lỗi nghiêm trọng hoặc không hoàn thành bài thi'),
    ('BE', 26, 85, 30, 18, N'Rớt nếu có lỗi nghiêm trọng hoặc không hoàn thành bài thi'),
    ('C1E', 28, 85, 32, 18, N'Rớt nếu có lỗi nghiêm trọng hoặc không hoàn thành bài thi'),
    ('CE', 28, 85, 34, 18, N'Rớt nếu có lỗi nghiêm trọng hoặc không hoàn thành bài thi'),
    ('D1E', 30, 85, 36, 18, N'Rớt nếu có lỗi nghiêm trọng hoặc không hoàn thành bài thi'),
    ('D2E', 30, 85, 36, 18, N'Rớt nếu có lỗi nghiêm trọng hoặc không hoàn thành bài thi'),
    ('DE', 30, 85, 38, 18, N'Rớt nếu có lỗi nghiêm trọng hoặc không hoàn thành bài thi');
GO
-- 📌 Bảng đăng kí thi
INSERT INTO DKThiGPLX (CCCD, MaLoai, NgayThi, MaTTSH)
VALUES
('012345678901', 'B1', '2024-04-10', 1),
('012345678902', 'A1', '2024-04-15', 2),
('012345678903', 'C',  '2024-04-20', 3),
('012345678904', 'B',  '2024-04-22', 4),
('012345678905', 'D',  '2024-05-01', 5);
GO
-- 📌 Bảng kết quả thi
INSERT INTO KetQuaThiGPLX (
    MaKetQua, MaDKThiGPLX, DiemLyThuyet, DiemThucHanh, DiemMoPhong, DiemDuongTruong, GhiChu, KetQua
)
VALUES
-- 1. Đậu bình thường, đầy đủ điểm, không vi phạm
('KQ0001', 1, 30, 90, 36, 36, NULL, N'Đậu'),

-- 2. Đậu, điểm cao, đạt xuất sắc
('KQ0002', 2, 32, 95, 38, 40, NULL, N'Đậu'),

-- 3. Rớt vì điểm thực hành không đạt (dưới 80 điểm yêu cầu)
('KQ0003', 3, 30, 75, 36, 36, N'Không đạt điểm thực hành yêu cầu', N'Rớt'),

-- 4. Rớt dù đủ điểm nhưng phạm lỗi nghiêm trọng: gây tai nạn
('KQ0004', 4, 30, 85, 36, 36, N'Đủ điểm nhưng gây tai nạn nghiêm trọng', N'Rớt'),

-- 5. Rớt vì ra khỏi sa hình khi thi thực hành
('KQ0005', 5, 28, 82, 36, 36, N'Đủ điểm nhưng đi sai quy trình và ra khỏi sa hình', N'Rớt');
GO
-- 📌 Bảng giấy phép lái xe
INSERT INTO GPLX (MaGPLX, MaKetQua, NgayCap, NgayHetHan)
VALUES
('790123456789', 'KQ0001', '2024-04-15', '2034-04-15'),
('791234567890', 'KQ0002', '2024-04-20', '2034-04-20');

-- 📌 Bảng loại vi phạm
INSERT INTO LoaiViPham (TenViPham, MucPhat)
VALUES
(N'Chạy quá tốc độ', 500000),
(N'Vượt đèn đỏ', 700000),
(N'Không đội mũ bảo hiểm', 250000),
(N'Lái xe khi có nồng độ cồn vượt mức cho phép', 3000000),
(N'Đi ngược chiều', 1000000),
(N'Không có GPLX khi điều khiển phương tiện', 5000000),
(N'Không chấp hành hiệu lệnh của CSGT', 2000000),
(N'Gây tai nạn nhưng không dừng lại xử lý', 5000000),
(N'Không bật đèn xe vào ban đêm', 300000),
(N'Đi vào đường cấm', 1500000);
GO


-- 📌 Bảng vi phạm
INSERT INTO ViPhamGPLX (MaGPLX, MaLoaiViPham, NgayViPham, TrangThai)
VALUES
('790123456789', 1, '2024-07-10', N'Chưa đóng phạt'),   -- Chạy quá tốc độ
('791234567890', 2, '2024-07-05', N'Đã đóng phạt'),      -- Vượt đèn đỏ
('790123456789', 3, '2024-07-01', N'Chưa đóng phạt'),    -- Không đội mũ bảo hiểm
('791234567890', 4, '2024-07-15', N'Chưa đóng phạt'),    -- Lái xe khi có nồng độ cồn vượt mức cho phép
('790123456789', 5, '2024-05-03', N'Đã đóng phạt'),      -- Đi ngược chiều
('791234567890', 6, '2024-05-20', N'Chưa đóng phạt'),    -- Không có GPLX khi điều khiển phương tiện
('790123456789', 7, '2024-06-10', N'Chưa đóng phạt'),    -- Không chấp hành hiệu lệnh của CSGT
('791234567890', 8, '2024-06-18', N'Đã đóng phạt'),      -- Gây tai nạn nhưng không dừng lại xử lý
('790123456789', 9, '2024-07-01', N'Chưa đóng phạt'),    -- Không bật đèn xe vào ban đêm
('791234567890', 10, '2024-07-15', N'Chưa đóng phạt');   -- Đi vào đường cấm
GO

-- 📌 Bảng khóa học
INSERT INTO KhoaHoc (TenKhoaHoc, ThoiGianHoc, HocPhi, MoTa, MaLoai)
VALUES
(N'Khóa học A1 cơ bản', N'1.5 tháng', 1200000, N'Dành cho người từ 18 tuổi trở lên thi bằng A1', 'A1'),
(N'Khóa học A nâng cao', N'2 tháng', 1800000, N'Dành cho người lái mô tô trên 125cc', 'A'),
(N'Khóa học B1 xe ba bánh', N'2 tháng', 3500000, N'Đào tạo điều khiển xe mô tô ba bánh và ôn luyện A1', 'B1'),
(N'Khóa học B lái ô tô cơ bản', N'3 tháng', 6000000, N'Đào tạo thực hành và lý thuyết lái ô tô dưới 3.5 tấn', 'B'),
(N'Khóa học C1 xe tải vừa', N'3.5 tháng', 6800000, N'Lái ô tô tải từ 3.5 đến dưới 7.5 tấn', 'C1'),
(N'Khóa học C xe tải lớn', N'4 tháng', 7500000, N'Dành cho người lái xe tải trên 7.5 tấn', 'C'),
(N'Khóa học D1 xe khách nhỏ', N'3 tháng', 8000000, N'Xe chở khách từ 8 đến 16 chỗ ngồi', 'D1'),
(N'Khóa học D2 xe khách vừa', N'3.5 tháng', 8500000, N'Xe chở khách từ 16 đến 29 chỗ ngồi', 'D2'),
(N'Khóa học D xe khách lớn', N'4 tháng', 9500000, N'Xe chở khách từ 30 chỗ ngồi trở lên', 'D'),
(N'Khóa học BE ô tô kéo rơ moóc nhỏ', N'2 tháng', 5000000, N'Lái ô tô hạng B kéo rơ moóc >750kg', 'BE'),
(N'Khóa học C1E xe tải rơ moóc', N'3 tháng', 7200000, N'Ô tô tải C1 kéo rơ moóc >750kg', 'C1E'),
(N'Khóa học CE xe tải nặng rơ moóc', N'3.5 tháng', 8200000, N'Ô tô tải hạng C kéo rơ moóc >750kg', 'CE'),
(N'Khóa học D1E xe khách nhỏ + rơ moóc', N'3.5 tháng', 8700000, N'Ô tô chở khách D1 kéo rơ moóc >750kg', 'D1E'),
(N'Khóa học D2E xe khách vừa + rơ moóc', N'4 tháng', 8900000, N'Ô tô chở khách D2 kéo rơ moóc >750kg', 'D2E'),
(N'Khóa học DE xe khách lớn + rơ moóc', N'4.5 tháng', 9500000, N'Ô tô chở khách D kéo rơ moóc >750kg', 'DE');

-- 📌 Bảng giảng viên 
INSERT INTO GiangVien (HoTen, NgaySinh, GioiTinh, SDT, Email)
VALUES
(N'Nguyễn Văn An',   '1980-05-12', 1, '0912345678', 'an.nguyen@gplx.vn'),
(N'Trần Thị Bích',   '1985-08-22', 0, '0934567890', 'bich.tran@gplx.vn'),
(N'Phạm Văn Cường',  '1978-11-03', 1, '0909123456', 'cuong.pham@gplx.vn'),
(N'Lê Thị Dung',     '1990-02-14', 0, '0945123789', 'dung.le@gplx.vn'),
(N'Hoàng Văn Duy',   '1982-07-29', 1, '0987654321', 'duy.hoang@gplx.vn'),
(N'Ngô Thị Hồng',    '1987-09-10', 0, '0923456789', 'hong.ngo@gplx.vn'),
(N'Vũ Minh Tuấn',    '1975-03-25', 1, '0976543210', 'tuan.vu@gplx.vn'),
(N'Đặng Thị Lan',    '1992-12-05', 0, '0961234567', 'lan.dang@gplx.vn'),
(N'Bùi Văn Hưng',    '1983-06-18', 1, '0956789123', 'hung.bui@gplx.vn'),
(N'Cao Thị Mai',     '1988-04-09', 0, '0937894561', 'mai.cao@gplx.vn');

-- 📌 Bảng lớp học
INSERT INTO LopHoc (NgayBatDau, NgayKetThuc, DiaDiem, ThoiGianHocTrongTuan, SoLuongToiDa, GhiChu, TrangThai, IsOnline, MaKhoaHoc, MaGV)
VALUES
('2025-05-05', '2025-08-28', N'241 Đường ABC, TP.Cần Thơ',         N'T3-T5 (8h-11h)',     20, NULL,							N'Đang mở',		0,  1, 6),
('2025-05-11', '2025-07-17', N'530 Đường ABC, TP.Hồ Chí Minh',     N'T2-T4-T6 (18h-20h)', 15, N'Lớp đặc biệt',				N'Đang mở',		0,  2, 1),
('2025-04-26', '2025-08-10', N'479 Đường ABC, TP.Hồ Chí Minh',     N'T7-CN (14h-17h)',    30, N'Học trực tuyến qua Zoom',	N'Đã huỷ',		1,  3, 2),
('2025-04-19', '2025-07-02', N'350 Đường ABC, TP.Cần Thơ',         N'T3-T5 (8h-11h)',     30, N'Lớp đặc biệt',				N'Đã huỷ',		0,  4, 1),
('2025-05-01', '2025-07-14', N'382 Đường ABC, TP.Hà Nội',          N'T7-CN (14h-17h)',    20, N'Ưu tiên nữ',				N'Đã đóng',		0,  5, 6),
('2025-05-06', '2025-08-30', N'526 Đường ABC, TP.Hồ Chí Minh',     N'T2-T4-T6 (18h-20h)', 15, NULL,							N'Đã huỷ',		0,  6, 2),
('2025-04-24', '2025-07-16', N'785 Đường ABC, TP.Cần Thơ',         N'T2-T4-T6 (18h-20h)', 30, N'Lớp đặc biệt',				N'Đang mở',		0,  7, 7),
('2025-05-13', '2025-07-24', N'643 Đường ABC, TP.Đà Nẵng',         N'T7-CN (14h-17h)',    15, N'Ưu tiên nữ',				N'Đang mở',		0,  8, 3),
('2025-05-01', '2025-07-28', N'618 Đường ABC, TP.Hồ Chí Minh',     N'T2-T4-T6 (18h-20h)', 25, NULL,							N'Đã đóng',		0,  9, 6),
('2025-04-27', '2025-08-15', N'457 Đường ABC, TP.Hồ Chí Minh',     N'T3-T5 (8h-11h)',     30, NULL,							N'Đã huỷ',		0, 10, 7),
('2025-05-10', '2025-07-25', N'289 Đường ABC, TP.Đà Nẵng',         N'T7-CN (14h-17h)',    25, NULL,							N'Đã huỷ',		0, 11, 9),
('2025-04-18', '2025-07-01', N'820 Đường ABC, TP.Hà Nội',          N'T3-T5 (8h-11h)',     20, NULL,							N'Đang mở',		0, 12, 9),
('2025-05-09', '2025-07-15', N'395 Đường ABC, TP.Cần Thơ',         N'T7-CN (14h-17h)',    20, NULL,							N'Đang mở',		0, 13, 8),
('2025-04-30', '2025-08-08', N'557 Đường ABC, TP.Hồ Chí Minh',     N'T3-T5 (8h-11h)',     25, N'Học trực tuyến qua Zoom',	N'Đã đóng',		1, 14, 6),
('2025-04-23', '2025-07-18', N'668 Đường ABC, TP.Cần Thơ',         N'T2-T4-T6 (18h-20h)', 30, NULL,							N'Đã huỷ',		0, 15, 2);

-- 📌 Bảng đăng ký khóa học
INSERT INTO DangKyKhoaHoc (CCCD, MaLop, NgayDangKy, TrangThaiDangKy, GhiChu)
VALUES
('012345678901', 1, '2025-04-14', N'Đã đăng ký',    NULL),
('012345678902', 2, '2025-04-15', N'Chờ xác nhận',  N'Đăng ký sớm'),
('012345678903', 3, '2025-04-16', N'Đã đăng ký',    NULL),
('012345678904', 4, '2025-04-17', N'Đã huỷ',        N'Chuyển từ lớp khác'),
('012345678905', 5, '2025-04-18', N'Đã đăng ký',    NULL),
('012345678906', 6, '2025-04-19', N'Đã đăng ký',    NULL),
('012345678907', 7, '2025-04-20', N'Chờ xác nhận',  NULL),
('012345678908', 8, '2025-04-21', N'Đã đăng ký',    N'Đăng ký sớm'),
('012345678909', 9, '2025-04-22', N'Đã huỷ',        NULL),
('012345678910',10, '2025-04-23', N'Đã đăng ký',    NULL);
