﻿
namespace EntityModel.DataModel
{

    public class eKhachHang
    {

        public int KeyID { get; set; }
        public string Ma { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public int IDGioiTinh { get; set; }
        public string GioiTinh { get; set; }
        public System.DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public int IDTinhThanh { get; set; }
        public string TinhThanh { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string GhiChu { get; set; }
        public byte[] HinhAnh { get; set; }
    }
}
