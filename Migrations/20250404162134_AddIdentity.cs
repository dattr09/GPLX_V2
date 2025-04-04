using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPLX.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CongDan",
                columns: table => new
                {
                    CCCD = table.Column<string>(type: "char(12)", unicode: false, fixedLength: true, maxLength: 12, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgaySinh = table.Column<DateOnly>(type: "date", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    QuocTich = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Việt Nam"),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoDienThoai = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CongDan__A955A0ABA6F2BB53", x => x.CCCD);
                });

            migrationBuilder.CreateTable(
                name: "GiangVien",
                columns: table => new
                {
                    MaGV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgaySinh = table.Column<DateOnly>(type: "date", nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: true),
                    SDT = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GiangVie__2725AEF3283B4BDF", x => x.MaGV);
                });

            migrationBuilder.CreateTable(
                name: "KhoaHoc",
                columns: table => new
                {
                    MaKhoaHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhoaHoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThoiGianHoc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HocPhi = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaLoai = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KhoaHoc__48F0FF98C46C293E", x => x.MaKhoaHoc);
                });

            migrationBuilder.CreateTable(
                name: "LoaiGPLX",
                columns: table => new
                {
                    MaLoai = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    TenLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DoTuoiDuocCap = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LoaiGPLX__730A5759C368576A", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "LoaiViPham",
                columns: table => new
                {
                    MaLoaiViPham = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenViPham = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MucPhat = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LoaiViPh__760D02765ED60D6E", x => x.MaLoaiViPham);
                });

            migrationBuilder.CreateTable(
                name: "TrungTamSatHach",
                columns: table => new
                {
                    MaTTSH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTrungTam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SoDT = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TrungTam__4484222233F6EEA2", x => x.MaTTSH);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LopHoc",
                columns: table => new
                {
                    MaLop = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ThoiGianHocTrongTuan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoLuongToiDa = table.Column<int>(type: "int", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, defaultValue: "Đang mở"),
                    IsOnline = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    MaKhoaHoc = table.Column<int>(type: "int", nullable: false),
                    MaGV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LopHoc__3B98D2737111EEC7", x => x.MaLop);
                    table.ForeignKey(
                        name: "FK_Lop_GiangVien",
                        column: x => x.MaGV,
                        principalTable: "GiangVien",
                        principalColumn: "MaGV");
                    table.ForeignKey(
                        name: "FK_Lop_KhoaHoc",
                        column: x => x.MaKhoaHoc,
                        principalTable: "KhoaHoc",
                        principalColumn: "MaKhoaHoc");
                });

            migrationBuilder.CreateTable(
                name: "DieuKienDatGPLX",
                columns: table => new
                {
                    MaDKDat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLoai = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    DiemLyThuyetDat = table.Column<int>(type: "int", nullable: false),
                    DiemThucHanhDat = table.Column<int>(type: "int", nullable: false),
                    DiemMoPhongDat = table.Column<int>(type: "int", nullable: true),
                    DiemDuongTruongDat = table.Column<int>(type: "int", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DieuKien__DC4946A7478CDB2E", x => x.MaDKDat);
                    table.ForeignKey(
                        name: "FK_DieuKienDat_LoaiGPLX",
                        column: x => x.MaLoai,
                        principalTable: "LoaiGPLX",
                        principalColumn: "MaLoai");
                });

            migrationBuilder.CreateTable(
                name: "DKThiGPLX",
                columns: table => new
                {
                    MaDKThiGPLX = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CCCD = table.Column<string>(type: "char(12)", unicode: false, fixedLength: true, maxLength: 12, nullable: false),
                    MaLoai = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    NgayThi = table.Column<DateOnly>(type: "date", nullable: false),
                    MaTTSH = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DKThiGPL__FA64BE89BE9D1072", x => x.MaDKThiGPLX);
                    table.ForeignKey(
                        name: "FK_DKThi_CCCD",
                        column: x => x.CCCD,
                        principalTable: "CongDan",
                        principalColumn: "CCCD");
                    table.ForeignKey(
                        name: "FK_DKThi_LoaiGPLX",
                        column: x => x.MaLoai,
                        principalTable: "LoaiGPLX",
                        principalColumn: "MaLoai");
                    table.ForeignKey(
                        name: "FK_DKThi_TTSH",
                        column: x => x.MaTTSH,
                        principalTable: "TrungTamSatHach",
                        principalColumn: "MaTTSH");
                });

            migrationBuilder.CreateTable(
                name: "DangKyKhoaHoc",
                columns: table => new
                {
                    MaDKKH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CCCD = table.Column<string>(type: "char(12)", unicode: false, fixedLength: true, maxLength: 12, nullable: false),
                    MaLop = table.Column<int>(type: "int", nullable: false),
                    NgayDangKy = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    TrangThaiDangKy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, defaultValue: "Đã đăng ký"),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DangKyKh__3D7B8BC80A52B26B", x => x.MaDKKH);
                    table.ForeignKey(
                        name: "FK_DKKH_LopHoc",
                        column: x => x.MaLop,
                        principalTable: "LopHoc",
                        principalColumn: "MaLop");
                });

            migrationBuilder.CreateTable(
                name: "KetQuaThiGPLX",
                columns: table => new
                {
                    MaKetQua = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    MaDKThiGPLX = table.Column<int>(type: "int", nullable: false),
                    DiemLyThuyet = table.Column<int>(type: "int", nullable: false),
                    DiemThucHanh = table.Column<int>(type: "int", nullable: false),
                    DiemMoPhong = table.Column<int>(type: "int", nullable: true),
                    DiemDuongTruong = table.Column<int>(type: "int", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    KetQua = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KetQuaTh__D5B3102A1D520BF1", x => x.MaKetQua);
                    table.ForeignKey(
                        name: "FK_KetQua_DKThi",
                        column: x => x.MaDKThiGPLX,
                        principalTable: "DKThiGPLX",
                        principalColumn: "MaDKThiGPLX");
                });

            migrationBuilder.CreateTable(
                name: "GPLX",
                columns: table => new
                {
                    MaGPLX = table.Column<string>(type: "char(12)", unicode: false, fixedLength: true, maxLength: 12, nullable: false),
                    MaKetQua = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    NgayCap = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    NgayHetHan = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GPLX__674D12528C5CD04C", x => x.MaGPLX);
                    table.ForeignKey(
                        name: "FK_GPLX_KetQuaThiGPLX",
                        column: x => x.MaKetQua,
                        principalTable: "KetQuaThiGPLX",
                        principalColumn: "MaKetQua");
                });

            migrationBuilder.CreateTable(
                name: "ViPhamGPLX",
                columns: table => new
                {
                    MaViPham = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaGPLX = table.Column<string>(type: "char(12)", unicode: false, fixedLength: true, maxLength: 12, nullable: false),
                    MaLoaiViPham = table.Column<int>(type: "int", nullable: false),
                    NgayViPham = table.Column<DateOnly>(type: "date", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Chưa đóng phạt")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ViPhamGP__F1921D8999C9A02F", x => x.MaViPham);
                    table.ForeignKey(
                        name: "FK_ViPham_GPLX",
                        column: x => x.MaGPLX,
                        principalTable: "GPLX",
                        principalColumn: "MaGPLX");
                    table.ForeignKey(
                        name: "FK_ViPham_LoaiViPham",
                        column: x => x.MaLoaiViPham,
                        principalTable: "LoaiViPham",
                        principalColumn: "MaLoaiViPham");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DangKyKhoaHoc_MaLop",
                table: "DangKyKhoaHoc",
                column: "MaLop");

            migrationBuilder.CreateIndex(
                name: "UQ_DKKH",
                table: "DangKyKhoaHoc",
                columns: new[] { "CCCD", "MaLop" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DieuKienDatGPLX_MaLoai",
                table: "DieuKienDatGPLX",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_DKThiGPLX_CCCD",
                table: "DKThiGPLX",
                column: "CCCD");

            migrationBuilder.CreateIndex(
                name: "IX_DKThiGPLX_MaLoai",
                table: "DKThiGPLX",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_DKThiGPLX_MaTTSH",
                table: "DKThiGPLX",
                column: "MaTTSH");

            migrationBuilder.CreateIndex(
                name: "IX_GPLX_MaKetQua",
                table: "GPLX",
                column: "MaKetQua");

            migrationBuilder.CreateIndex(
                name: "UQ__KetQuaTh__FA64BE8871E65C42",
                table: "KetQuaThiGPLX",
                column: "MaDKThiGPLX",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_MaGV",
                table: "LopHoc",
                column: "MaGV");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_MaKhoaHoc",
                table: "LopHoc",
                column: "MaKhoaHoc");

            migrationBuilder.CreateIndex(
                name: "IX_ViPhamGPLX_MaGPLX",
                table: "ViPhamGPLX",
                column: "MaGPLX");

            migrationBuilder.CreateIndex(
                name: "IX_ViPhamGPLX_MaLoaiViPham",
                table: "ViPhamGPLX",
                column: "MaLoaiViPham");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DangKyKhoaHoc");

            migrationBuilder.DropTable(
                name: "DieuKienDatGPLX");

            migrationBuilder.DropTable(
                name: "ViPhamGPLX");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LopHoc");

            migrationBuilder.DropTable(
                name: "GPLX");

            migrationBuilder.DropTable(
                name: "LoaiViPham");

            migrationBuilder.DropTable(
                name: "GiangVien");

            migrationBuilder.DropTable(
                name: "KhoaHoc");

            migrationBuilder.DropTable(
                name: "KetQuaThiGPLX");

            migrationBuilder.DropTable(
                name: "DKThiGPLX");

            migrationBuilder.DropTable(
                name: "CongDan");

            migrationBuilder.DropTable(
                name: "LoaiGPLX");

            migrationBuilder.DropTable(
                name: "TrungTamSatHach");
        }
    }
}
