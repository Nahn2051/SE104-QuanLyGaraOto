using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGaraOto.Models
{
    [Table("PHIEUNHAP")]
    public class PhieuNhap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaPhieuNhap { get; set; }

        [Required]
        public DateTime NgayNhap { get; set; }

        // Navigation properties
        public virtual ICollection<ChiTietPhieuNhap> DanhSachCTPhieuNhap { get; set; } = new List<ChiTietPhieuNhap>();
    }
}
