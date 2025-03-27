using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GPLX.Models;

public partial class AppDBContext : DbContext
{
    public AppDBContext()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CongDan> CongDans { get; set; }

    public virtual DbSet<DieuKienDatGplx> DieuKienDatGplxes { get; set; }

    public virtual DbSet<DkthiGplx> DkthiGplxes { get; set; }

    public virtual DbSet<Gplx> Gplxes { get; set; }

    public virtual DbSet<KetQuaThiGplx> KetQuaThiGplxes { get; set; }

    public virtual DbSet<LoaiGplx> LoaiGplxes { get; set; }

    public virtual DbSet<LoaiViPham> LoaiViPhams { get; set; }

    public virtual DbSet<TrungTamSatHach> TrungTamSatHaches { get; set; }

    public virtual DbSet<ViPhamGplx> ViPhamGplxes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-7B4TMH6\\MSSQLSERVER1;Database=QL_GPLX;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CongDan>(entity =>
        {
            entity.HasKey(e => e.Cccd).HasName("PK__CongDan__A955A0ABA6F2BB53");

            entity.ToTable("CongDan");

            entity.Property(e => e.Cccd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CCCD");
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.QuocTich)
                .HasMaxLength(30)
                .HasDefaultValue("Việt Nam");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DieuKienDatGplx>(entity =>
        {
            entity.HasKey(e => e.MaDkdat).HasName("PK__DieuKien__DC4946A7478CDB2E");

            entity.ToTable("DieuKienDatGPLX");

            entity.Property(e => e.MaDkdat).HasColumnName("MaDKDat");
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.MaLoai)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.DieuKienDatGplxes)
                .HasForeignKey(d => d.MaLoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DieuKienDat_LoaiGPLX");
        });

        modelBuilder.Entity<DkthiGplx>(entity =>
        {
            entity.HasKey(e => e.MaDkthiGplx).HasName("PK__DKThiGPL__FA64BE89BE9D1072");

            entity.ToTable("DKThiGPLX", tb => tb.HasTrigger("trg_Check_DoTuoi_DKThi"));

            entity.Property(e => e.MaDkthiGplx).HasColumnName("MaDKThiGPLX");
            entity.Property(e => e.Cccd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CCCD");
            entity.Property(e => e.MaLoai)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaTtsh).HasColumnName("MaTTSH");

            entity.HasOne(d => d.CccdNavigation).WithMany(p => p.DkthiGplxes)
                .HasForeignKey(d => d.Cccd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DKThi_CCCD");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.DkthiGplxes)
                .HasForeignKey(d => d.MaLoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DKThi_LoaiGPLX");

            entity.HasOne(d => d.MaTtshNavigation).WithMany(p => p.DkthiGplxes)
                .HasForeignKey(d => d.MaTtsh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DKThi_TTSH");
        });

        modelBuilder.Entity<Gplx>(entity =>
        {
            entity.HasKey(e => e.MaGplx).HasName("PK__GPLX__674D12528C5CD04C");

            entity.ToTable("GPLX", tb =>
                {
                    tb.HasTrigger("trg_Check_KetQuaDat_GPLX");
                    tb.HasTrigger("trg_Check_NgayCap_GPLX");
                });

            entity.Property(e => e.MaGplx)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaGPLX");
            entity.Property(e => e.MaKetQua).HasMaxLength(10);
            entity.Property(e => e.NgayCap).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.MaKetQuaNavigation).WithMany(p => p.Gplxes)
                .HasForeignKey(d => d.MaKetQua)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GPLX_KetQuaThiGPLX");
        });

        modelBuilder.Entity<KetQuaThiGplx>(entity =>
        {
            entity.HasKey(e => e.MaKetQua).HasName("PK__KetQuaTh__D5B3102A1D520BF1");

            entity.ToTable("KetQuaThiGPLX");

            entity.HasIndex(e => e.MaDkthiGplx, "UQ__KetQuaTh__FA64BE8871E65C42").IsUnique();

            entity.Property(e => e.MaKetQua).HasMaxLength(10);
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.KetQua).HasMaxLength(10);
            entity.Property(e => e.MaDkthiGplx).HasColumnName("MaDKThiGPLX");

            entity.HasOne(d => d.MaDkthiGplxNavigation).WithOne(p => p.KetQuaThiGplx)
                .HasForeignKey<KetQuaThiGplx>(d => d.MaDkthiGplx)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KetQua_DKThi");
        });

        modelBuilder.Entity<LoaiGplx>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PK__LoaiGPLX__730A5759C368576A");

            entity.ToTable("LoaiGPLX");

            entity.Property(e => e.MaLoai)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenLoai).HasMaxLength(100);
        });

        modelBuilder.Entity<LoaiViPham>(entity =>
        {
            entity.HasKey(e => e.MaLoaiViPham).HasName("PK__LoaiViPh__760D02765ED60D6E");

            entity.ToTable("LoaiViPham");

            entity.Property(e => e.MucPhat).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TenViPham).HasMaxLength(255);
        });

        modelBuilder.Entity<TrungTamSatHach>(entity =>
        {
            entity.HasKey(e => e.MaTtsh).HasName("PK__TrungTam__4484222233F6EEA2");

            entity.ToTable("TrungTamSatHach");

            entity.Property(e => e.MaTtsh).HasColumnName("MaTTSH");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.SoDt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SoDT");
            entity.Property(e => e.TenTrungTam).HasMaxLength(100);
        });

        modelBuilder.Entity<ViPhamGplx>(entity =>
        {
            entity.HasKey(e => e.MaViPham).HasName("PK__ViPhamGP__F1921D8999C9A02F");

            entity.ToTable("ViPhamGPLX", tb => tb.HasTrigger("trg_Check_NgayViPham"));

            entity.Property(e => e.MaGplx)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaGPLX");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .HasDefaultValue("Chưa đóng phạt");

            entity.HasOne(d => d.MaGplxNavigation).WithMany(p => p.ViPhamGplxes)
                .HasForeignKey(d => d.MaGplx)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ViPham_GPLX");

            entity.HasOne(d => d.MaLoaiViPhamNavigation).WithMany(p => p.ViPhamGplxes)
                .HasForeignKey(d => d.MaLoaiViPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ViPham_LoaiViPham");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
