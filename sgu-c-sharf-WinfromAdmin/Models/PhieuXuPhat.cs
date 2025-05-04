using System;
using System.ComponentModel.DataAnnotations;

namespace sgu_c_sharf_WinfromAdmin.Models
{
    public class PhieuXuPhat
    {
        [Key]
        public uint Id { get; set; } // Sử dụng uint để phù hợp với INT UNSIGNED

        [Required]
        public TrangThaiPhieuXuPhatEnum TrangThai { get; set; } = TrangThaiPhieuXuPhatEnum.CHUAXULY;

        [Required]
        public DateTime NgayViPham { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(255)]
        public string MoTa { get; set; } = string.Empty;

        public uint? ThoiHanXuPhat { get; set; } // Sử dụng uint? để phù hợp với INT UNSIGNED NULL
        public uint? MucPhat { get; set; } // Sử dụng uint? để phù hợp với INT UNSIGNED NULL

        [Required]
        public uint IdThanhVien { get; set; } // Sử dụng uint để phù hợp với INT UNSIGNED

        public string TenThanhVien { get; set; } = string.Empty; // Tên thành viên để hiển thị trong giao diện
        public DateTime? NgayXuPhat { get; set; } // Thêm thuộc tính NgayXuPhat để khớp với form
    }

    public class PhieuXuPhatCreateDTO
    {
        [Required]
        public TrangThaiPhieuXuPhatEnum TrangThai { get; set; } = TrangThaiPhieuXuPhatEnum.CHUAXULY;

        [Required]
        public DateTime NgayViPham { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(255)]
        public string MoTa { get; set; } = string.Empty;

        public uint? ThoiHanXuPhat { get; set; } // Sử dụng uint?
        public uint? MucPhat { get; set; } // Sử dụng uint?

        [Required]
        public uint IdThanhVien { get; set; } // Sử dụng uint
    }

    public class PhieuXuPhatDetailDTO
    {
        public uint Id { get; set; } // Sử dụng uint
        public TrangThaiPhieuXuPhatEnum TrangThai { get; set; } = TrangThaiPhieuXuPhatEnum.CHUAXULY;
        public DateTime NgayViPham { get; set; } = DateTime.Now;
        public string MoTa { get; set; } = string.Empty;
        public uint? ThoiHanXuPhat { get; set; } // Sử dụng uint?
        public uint? MucPhat { get; set; } // Sử dụng uint?
        public uint IdThanhVien { get; set; } // Sử dụng uint
        public string TenThanhVien { get; set; } = string.Empty; // Tên thành viên để hiển thị
        public DateTime? NgayXuPhat { get; set; } // Thêm thuộc tính NgayXuPhat để khớp với form
    }

    public class PhieuXuPhatUpdateDTO
    {
        public TrangThaiPhieuXuPhatEnum? TrangThai { get; set; }

        public DateTime? NgayViPham { get; set; }

        [MaxLength(255)]
        public string? MoTa { get; set; }

        public uint? ThoiHanXuPhat { get; set; } // Sử dụng uint?
        public uint? MucPhat { get; set; } // Sử dụng uint?

        public uint? IdThanhVien { get; set; } // Sử dụng uint?
        public DateTime? NgayXuPhat { get; set; } // Thêm thuộc tính NgayXuPhat để khớp với form
    }

    public enum TrangThaiPhieuXuPhatEnum
    {
        DAXOA,
        DAXULY,
        CHUAXULY
    }
}