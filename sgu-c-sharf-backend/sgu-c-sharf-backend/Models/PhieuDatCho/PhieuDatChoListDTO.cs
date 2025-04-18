﻿namespace sgu_c_sharf_backend.Models.PhieuDatCho
{
    public class PhieuDatChoListDTO
    {
        public int Id { get; set; }
        public int IdThanhVien { get; set; }
        public string HoTenThanhVien { get; set; } // Để hiển thị tên thành viên
        public TrangThaiPhieuDatChoEnum TrangThai { get; set; }
        public DateTime ThoiGianDat { get; set; }
        public DateTime ThoiGianLapPhieu { get; set; }
    }
}