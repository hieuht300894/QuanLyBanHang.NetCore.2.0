using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Server.Migrations
{
    public partial class db1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eCongNoNhaCungCap",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConLai = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    IDMaster = table.Column<int>(type: "int", nullable: false),
                    IDNhaCungCap = table.Column<int>(type: "int", nullable: false),
                    IsNhapHang = table.Column<bool>(type: "bit", nullable: false),
                    IsSoDuDauKy = table.Column<bool>(type: "bit", nullable: false),
                    IsThanhToan = table.Column<bool>(type: "bit", nullable: false),
                    IsTraHang = table.Column<bool>(type: "bit", nullable: false),
                    MaNhaCungCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoCu = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TenNhaCungCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThanhToan = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eCongNoNhaCungCap", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eDonViTinh",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KichHoat = table.Column<bool>(type: "bit", nullable: false),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eDonViTinh", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eHienThi",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eHienThi", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eKhachHang",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhAnh = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Ho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDGioiTinh = table.Column<int>(type: "int", nullable: false),
                    IDTinhThanh = table.Column<int>(type: "int", nullable: false),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TinhThanh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eKhachHang", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eKho",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KichHoat = table.Column<bool>(type: "bit", nullable: false),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eKho", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eNhaCungCap",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDTinhThanh = table.Column<int>(type: "int", nullable: false),
                    KichHoat = table.Column<bool>(type: "bit", nullable: false),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguoiLienHe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TinhThanh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eNhaCungCap", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eNhapHangNhaCungCap",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChietKhau = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNhaCungCap = table.Column<int>(type: "int", nullable: false),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaLoHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaNhaCungCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoLuong = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TenNhaCungCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThanhTien = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TienChietKhau = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TienVAT = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eNhapHangNhaCungCap", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eNhapHangNhaCungCapChiTiet",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChietKhau = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HanSuDung = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IDDonViTinh = table.Column<int>(type: "int", nullable: false),
                    IDKho = table.Column<int>(type: "int", nullable: false),
                    IDNhapHangNhaCungCap = table.Column<int>(type: "int", nullable: false),
                    IDSanPham = table.Column<int>(type: "int", nullable: false),
                    MaDonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaKho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLuong = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SoLuongLe = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SoLuongSi = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TenDonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenKho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThanhTien = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TienChietKhau = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TienVAT = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eNhapHangNhaCungCapChiTiet", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eNhomDonViTinh",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KichHoat = table.Column<bool>(type: "bit", nullable: false),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eNhomDonViTinh", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eNhomKhachHang",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KichHoat = table.Column<bool>(type: "bit", nullable: false),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eNhomKhachHang", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eNhomNhaCungCap",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KichHoat = table.Column<bool>(type: "bit", nullable: false),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eNhomNhaCungCap", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eNhomSanPham",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KichHoat = table.Column<bool>(type: "bit", nullable: false),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eNhomSanPham", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eQuyDoiDonVi",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonViTinhQuyDoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaTri = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    IDDonViTinh = table.Column<int>(type: "int", nullable: false),
                    IDDonViTinhQuyDoi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eQuyDoiDonVi", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eQuyDoiTienTe",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GiaTri = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    IDTienTe = table.Column<int>(type: "int", nullable: false),
                    IDTienTeQuyDoi = table.Column<int>(type: "int", nullable: false),
                    TienTe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TienTeQuyDoi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eQuyDoiTienTe", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eSanPham",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColorHex = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDDonViTinh = table.Column<int>(type: "int", nullable: false),
                    KichThuoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MauSac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eSanPham", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eSoDuDauKyKhachHang",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDKhachHang = table.Column<int>(type: "int", nullable: false),
                    MaKhachHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TenKhachHang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eSoDuDauKyKhachHang", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eSoDuDauKyNhaCungCap",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNhaCungCap = table.Column<int>(type: "int", nullable: false),
                    MaNhaCungCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TenNhaCungCap = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eSoDuDauKyNhaCungCap", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eTienTe",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KyHieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDienTu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eTienTe", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eTinhThanh",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DienGiai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDLoai = table.Column<int>(type: "int", nullable: false),
                    IDTinhThanh = table.Column<int>(type: "int", nullable: false),
                    Loai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TinhThanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eTinhThanh", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eTonKho",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HanSuDung = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IDDonViTinh = table.Column<int>(type: "int", nullable: false),
                    IDKho = table.Column<int>(type: "int", nullable: false),
                    IDNhomSanPham = table.Column<int>(type: "int", nullable: false),
                    IDSanPham = table.Column<int>(type: "int", nullable: false),
                    MaDonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaKho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaNhomSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoLuongKhachHangTra = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SoLuongNhap = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SoLuongTon = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SoLuongTraNhaCungCap = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SoLuongXuat = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TenDonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenKho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNhomSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eTonKho", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "eTonKhoDauKy",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDDonViTinh = table.Column<int>(type: "int", nullable: false),
                    IDKho = table.Column<int>(type: "int", nullable: false),
                    IDSanPham = table.Column<int>(type: "int", nullable: false),
                    MaDonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaKho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoLuong = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TenDonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenKho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eTonKhoDauKy", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "xAccount",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDPermission = table.Column<int>(type: "int", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PermissionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xAccount", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "xAgency",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Credentials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    HostAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HostPort = table.Column<int>(type: "int", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xAgency", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "xAppConfig",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    colBinary = table.Column<byte[]>(type: "varbinary(128)", maxLength: 128, nullable: true),
                    colBit = table.Column<bool>(type: "bit", nullable: true),
                    colDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    colDecimal = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    colFloat = table.Column<double>(type: "float", nullable: true),
                    colInt = table.Column<int>(type: "int", nullable: true),
                    colName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    colString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colXML = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xAppConfig", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "xDisplay",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EnableEdit = table.Column<bool>(type: "bit", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Group = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ParentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Showing = table.Column<bool>(type: "bit", nullable: false),
                    TextAlign = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    VisibleIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xDisplay", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "xFeature",
                columns: table => new
                {
                    KeyID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IDGroup = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsAdd = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsEdit = table.Column<bool>(type: "bit", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    IsExportExcel = table.Column<bool>(type: "bit", nullable: false),
                    IsPrintPreview = table.Column<bool>(type: "bit", nullable: false),
                    IsSave = table.Column<bool>(type: "bit", nullable: false),
                    ItemCount = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    VN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xFeature", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "xLayoutItemCaption",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ControlAlignment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FormName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LayoutControlItem = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LayoutControlName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TextLocation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TextVisible = table.Column<bool>(type: "bit", nullable: false),
                    VN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Visibility = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xLayoutItemCaption", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "xLog",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDPersonnel = table.Column<int>(type: "int", nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xLog", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "xMsgDictionary",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MsgName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    VN = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xMsgDictionary", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "xPermission",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xPermission", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "xPersonnel",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAccount = table.Column<bool>(type: "bit", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xPersonnel", x => x.KeyID);
                });

            migrationBuilder.CreateTable(
                name: "xUserFeature",
                columns: table => new
                {
                    KeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IDFeature = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IDPermission = table.Column<int>(type: "int", nullable: false),
                    IsAdd = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsEdit = table.Column<bool>(type: "bit", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    IsExportExcel = table.Column<bool>(type: "bit", nullable: false),
                    IsPrintPreview = table.Column<bool>(type: "bit", nullable: false),
                    IsSave = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xUserFeature", x => x.KeyID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eCongNoNhaCungCap");

            migrationBuilder.DropTable(
                name: "eDonViTinh");

            migrationBuilder.DropTable(
                name: "eHienThi");

            migrationBuilder.DropTable(
                name: "eKhachHang");

            migrationBuilder.DropTable(
                name: "eKho");

            migrationBuilder.DropTable(
                name: "eNhaCungCap");

            migrationBuilder.DropTable(
                name: "eNhapHangNhaCungCap");

            migrationBuilder.DropTable(
                name: "eNhapHangNhaCungCapChiTiet");

            migrationBuilder.DropTable(
                name: "eNhomDonViTinh");

            migrationBuilder.DropTable(
                name: "eNhomKhachHang");

            migrationBuilder.DropTable(
                name: "eNhomNhaCungCap");

            migrationBuilder.DropTable(
                name: "eNhomSanPham");

            migrationBuilder.DropTable(
                name: "eQuyDoiDonVi");

            migrationBuilder.DropTable(
                name: "eQuyDoiTienTe");

            migrationBuilder.DropTable(
                name: "eSanPham");

            migrationBuilder.DropTable(
                name: "eSoDuDauKyKhachHang");

            migrationBuilder.DropTable(
                name: "eSoDuDauKyNhaCungCap");

            migrationBuilder.DropTable(
                name: "eTienTe");

            migrationBuilder.DropTable(
                name: "eTinhThanh");

            migrationBuilder.DropTable(
                name: "eTonKho");

            migrationBuilder.DropTable(
                name: "eTonKhoDauKy");

            migrationBuilder.DropTable(
                name: "xAccount");

            migrationBuilder.DropTable(
                name: "xAgency");

            migrationBuilder.DropTable(
                name: "xAppConfig");

            migrationBuilder.DropTable(
                name: "xDisplay");

            migrationBuilder.DropTable(
                name: "xFeature");

            migrationBuilder.DropTable(
                name: "xLayoutItemCaption");

            migrationBuilder.DropTable(
                name: "xLog");

            migrationBuilder.DropTable(
                name: "xMsgDictionary");

            migrationBuilder.DropTable(
                name: "xPermission");

            migrationBuilder.DropTable(
                name: "xPersonnel");

            migrationBuilder.DropTable(
                name: "xUserFeature");
        }
    }
}
