using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGaraOto.Models
{
    [Table("NGUOIDUNG")]
    public class NguoiDung
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNguoiDung { get; set; }

        [Required]
        [MaxLength(50)]
        public string TenDangNhap { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string MatKhau { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string TenNguoiDung { get; set; } = string.Empty;

        [Required]
        public int MaVaiTro { get; set; }

        // Navigation properties
        [ForeignKey("MaVaiTro")]
        public virtual VaiTro? VaiTro { get; set; }
    }
}
