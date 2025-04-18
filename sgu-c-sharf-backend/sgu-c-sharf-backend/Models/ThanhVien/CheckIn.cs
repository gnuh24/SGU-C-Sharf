using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sgu_c_sharf_backend.Models.ThanhVien
{
    [Table("CheckIn")]
    public class CheckIn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        
        [Required]
        public DateTime ThoiGianCheckIn {get; set;}

        [Required]
        public int IdThanhVien {get; set;}
    }
}