﻿using System.ComponentModel.DataAnnotations;

namespace sgu_c_sharf_backend.Models.PhieuMuon
{
    public class ChiTietPhieuMuonCreateDTO
    {
        [Required]
        public int IdPhieuMuon { get; set; }

        [Required]
        public int IdDauThietBi { get; set; }
    }
}