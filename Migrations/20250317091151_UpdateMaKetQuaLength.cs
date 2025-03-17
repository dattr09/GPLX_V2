using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPLX.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMaKetQuaLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "KetQuaThiGPLX",
                columns: table => new
                {
                    MaKetQua = table.Column<string>(type: "VARCHAR(20)", maxLength: 10, nullable: false),
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
                    MaKetQua = table.Column<string>(type: "VARCHAR(20)", maxLength: 10, nullable: false),
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
                name: "DieuKienDatGPLX");

            migrationBuilder.DropTable(
                name: "ViPhamGPLX");

            migrationBuilder.DropTable(
                name: "GPLX");

            migrationBuilder.DropTable(
                name: "LoaiViPham");

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
