﻿// <auto-generated />
using System;
using GPLX.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GPLX.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GPLX.Models.CongDan", b =>
                {
                    b.Property<string>("Cccd")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)")
                        .HasColumnName("CCCD")
                        .IsFixedLength();

                    b.Property<string>("DiaChi")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("GioiTinh")
                        .HasColumnType("bit");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateOnly>("NgaySinh")
                        .HasColumnType("date");

                    b.Property<string>("QuocTich")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasDefaultValue("Việt Nam");

                    b.Property<string>("SoDienThoai")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Cccd")
                        .HasName("PK__CongDan__A955A0ABA6F2BB53");

                    b.ToTable("CongDan", (string)null);
                });

            modelBuilder.Entity("GPLX.Models.DieuKienDatGplx", b =>
                {
                    b.Property<int>("MaDkdat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MaDKDat");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDkdat"));

                    b.Property<int?>("DiemDuongTruongDat")
                        .HasColumnType("int");

                    b.Property<int>("DiemLyThuyetDat")
                        .HasColumnType("int");

                    b.Property<int?>("DiemMoPhongDat")
                        .HasColumnType("int");

                    b.Property<int>("DiemThucHanhDat")
                        .HasColumnType("int");

                    b.Property<string>("GhiChu")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("MaLoai")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("char(3)")
                        .IsFixedLength();

                    b.HasKey("MaDkdat")
                        .HasName("PK__DieuKien__DC4946A7478CDB2E");

                    b.HasIndex("MaLoai");

                    b.ToTable("DieuKienDatGPLX", (string)null);
                });

            modelBuilder.Entity("GPLX.Models.DkthiGplx", b =>
                {
                    b.Property<int>("MaDkthiGplx")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MaDKThiGPLX");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDkthiGplx"));

                    b.Property<string>("Cccd")
                        .IsRequired()
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)")
                        .HasColumnName("CCCD")
                        .IsFixedLength();

                    b.Property<string>("MaLoai")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("char(3)")
                        .IsFixedLength();

                    b.Property<int>("MaTtsh")
                        .HasColumnType("int")
                        .HasColumnName("MaTTSH");

                    b.Property<DateOnly>("NgayThi")
                        .HasColumnType("date");

                    b.HasKey("MaDkthiGplx")
                        .HasName("PK__DKThiGPL__FA64BE89BE9D1072");

                    b.HasIndex("Cccd");

                    b.HasIndex("MaLoai");

                    b.HasIndex("MaTtsh");

                    b.ToTable("DKThiGPLX", null, t =>
                        {
                            t.HasTrigger("trg_Check_DoTuoi_DKThi");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("GPLX.Models.Gplx", b =>
                {
                    b.Property<string>("MaGplx")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)")
                        .HasColumnName("MaGPLX")
                        .IsFixedLength();

                    b.Property<string>("MaKetQua")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR(20)");

                    b.Property<DateOnly>("NgayCap")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateOnly>("NgayHetHan")
                        .HasColumnType("date");

                    b.HasKey("MaGplx")
                        .HasName("PK__GPLX__674D12528C5CD04C");

                    b.HasIndex("MaKetQua");

                    b.ToTable("GPLX", null, t =>
                        {
                            t.HasTrigger("trg_Check_KetQuaDat_GPLX");

                            t.HasTrigger("trg_Check_NgayCap_GPLX");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("GPLX.Models.KetQuaThiGplx", b =>
                {
                    b.Property<string>("MaKetQua")
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR(20)");

                    b.Property<int?>("DiemDuongTruong")
                        .HasColumnType("int");

                    b.Property<int>("DiemLyThuyet")
                        .HasColumnType("int");

                    b.Property<int?>("DiemMoPhong")
                        .HasColumnType("int");

                    b.Property<int>("DiemThucHanh")
                        .HasColumnType("int");

                    b.Property<string>("GhiChu")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("KetQua")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("MaDkthiGplx")
                        .HasColumnType("int")
                        .HasColumnName("MaDKThiGPLX");

                    b.HasKey("MaKetQua")
                        .HasName("PK__KetQuaTh__D5B3102A1D520BF1");

                    b.HasIndex(new[] { "MaDkthiGplx" }, "UQ__KetQuaTh__FA64BE8871E65C42")
                        .IsUnique();

                    b.ToTable("KetQuaThiGPLX", (string)null);
                });

            modelBuilder.Entity("GPLX.Models.LoaiGplx", b =>
                {
                    b.Property<string>("MaLoai")
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("char(3)")
                        .IsFixedLength();

                    b.Property<byte>("DoTuoiDuocCap")
                        .HasColumnType("tinyint");

                    b.Property<string>("MoTa")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TenLoai")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaLoai")
                        .HasName("PK__LoaiGPLX__730A5759C368576A");

                    b.ToTable("LoaiGPLX", (string)null);
                });

            modelBuilder.Entity("GPLX.Models.LoaiViPham", b =>
                {
                    b.Property<int>("MaLoaiViPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaLoaiViPham"));

                    b.Property<decimal>("MucPhat")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("TenViPham")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("MaLoaiViPham")
                        .HasName("PK__LoaiViPh__760D02765ED60D6E");

                    b.ToTable("LoaiViPham", (string)null);
                });

            modelBuilder.Entity("GPLX.Models.TrungTamSatHach", b =>
                {
                    b.Property<int>("MaTtsh")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MaTTSH");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaTtsh"));

                    b.Property<string>("DiaChi")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SoDt")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("SoDT");

                    b.Property<string>("TenTrungTam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaTtsh")
                        .HasName("PK__TrungTam__4484222233F6EEA2");

                    b.ToTable("TrungTamSatHach", (string)null);
                });

            modelBuilder.Entity("GPLX.Models.ViPhamGplx", b =>
                {
                    b.Property<int>("MaViPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaViPham"));

                    b.Property<string>("MaGplx")
                        .IsRequired()
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)")
                        .HasColumnName("MaGPLX")
                        .IsFixedLength();

                    b.Property<int>("MaLoaiViPham")
                        .HasColumnType("int");

                    b.Property<DateOnly>("NgayViPham")
                        .HasColumnType("date");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasDefaultValue("Chưa đóng phạt");

                    b.HasKey("MaViPham")
                        .HasName("PK__ViPhamGP__F1921D8999C9A02F");

                    b.HasIndex("MaGplx");

                    b.HasIndex("MaLoaiViPham");

                    b.ToTable("ViPhamGPLX", null, t =>
                        {
                            t.HasTrigger("trg_Check_NgayViPham");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("GPLX.Models.DieuKienDatGplx", b =>
                {
                    b.HasOne("GPLX.Models.LoaiGplx", "MaLoaiNavigation")
                        .WithMany("DieuKienDatGplxes")
                        .HasForeignKey("MaLoai")
                        .IsRequired()
                        .HasConstraintName("FK_DieuKienDat_LoaiGPLX");

                    b.Navigation("MaLoaiNavigation");
                });

            modelBuilder.Entity("GPLX.Models.DkthiGplx", b =>
                {
                    b.HasOne("GPLX.Models.CongDan", "CccdNavigation")
                        .WithMany("DkthiGplxes")
                        .HasForeignKey("Cccd")
                        .IsRequired()
                        .HasConstraintName("FK_DKThi_CCCD");

                    b.HasOne("GPLX.Models.LoaiGplx", "MaLoaiNavigation")
                        .WithMany("DkthiGplxes")
                        .HasForeignKey("MaLoai")
                        .IsRequired()
                        .HasConstraintName("FK_DKThi_LoaiGPLX");

                    b.HasOne("GPLX.Models.TrungTamSatHach", "MaTtshNavigation")
                        .WithMany("DkthiGplxes")
                        .HasForeignKey("MaTtsh")
                        .IsRequired()
                        .HasConstraintName("FK_DKThi_TTSH");

                    b.Navigation("CccdNavigation");

                    b.Navigation("MaLoaiNavigation");

                    b.Navigation("MaTtshNavigation");
                });

            modelBuilder.Entity("GPLX.Models.Gplx", b =>
                {
                    b.HasOne("GPLX.Models.KetQuaThiGplx", "MaKetQuaNavigation")
                        .WithMany("Gplxes")
                        .HasForeignKey("MaKetQua")
                        .IsRequired()
                        .HasConstraintName("FK_GPLX_KetQuaThiGPLX");

                    b.Navigation("MaKetQuaNavigation");
                });

            modelBuilder.Entity("GPLX.Models.KetQuaThiGplx", b =>
                {
                    b.HasOne("GPLX.Models.DkthiGplx", "MaDkthiGplxNavigation")
                        .WithOne("KetQuaThiGplx")
                        .HasForeignKey("GPLX.Models.KetQuaThiGplx", "MaDkthiGplx")
                        .IsRequired()
                        .HasConstraintName("FK_KetQua_DKThi");

                    b.Navigation("MaDkthiGplxNavigation");
                });

            modelBuilder.Entity("GPLX.Models.ViPhamGplx", b =>
                {
                    b.HasOne("GPLX.Models.Gplx", "MaGplxNavigation")
                        .WithMany("ViPhamGplxes")
                        .HasForeignKey("MaGplx")
                        .IsRequired()
                        .HasConstraintName("FK_ViPham_GPLX");

                    b.HasOne("GPLX.Models.LoaiViPham", "MaLoaiViPhamNavigation")
                        .WithMany("ViPhamGplxes")
                        .HasForeignKey("MaLoaiViPham")
                        .IsRequired()
                        .HasConstraintName("FK_ViPham_LoaiViPham");

                    b.Navigation("MaGplxNavigation");

                    b.Navigation("MaLoaiViPhamNavigation");
                });

            modelBuilder.Entity("GPLX.Models.CongDan", b =>
                {
                    b.Navigation("DkthiGplxes");
                });

            modelBuilder.Entity("GPLX.Models.DkthiGplx", b =>
                {
                    b.Navigation("KetQuaThiGplx");
                });

            modelBuilder.Entity("GPLX.Models.Gplx", b =>
                {
                    b.Navigation("ViPhamGplxes");
                });

            modelBuilder.Entity("GPLX.Models.KetQuaThiGplx", b =>
                {
                    b.Navigation("Gplxes");
                });

            modelBuilder.Entity("GPLX.Models.LoaiGplx", b =>
                {
                    b.Navigation("DieuKienDatGplxes");

                    b.Navigation("DkthiGplxes");
                });

            modelBuilder.Entity("GPLX.Models.LoaiViPham", b =>
                {
                    b.Navigation("ViPhamGplxes");
                });

            modelBuilder.Entity("GPLX.Models.TrungTamSatHach", b =>
                {
                    b.Navigation("DkthiGplxes");
                });
#pragma warning restore 612, 618
        }
    }
}
