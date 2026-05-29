using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGaraOto.Models
{
    [Table("XE")]
    public class Xe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaXe { get; set; }

        [Required]
        [MaxLength(15)]
        public string BienSo { get; set; } = string.Empty;

        [Required]
        public int MaHieuXe { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenChuXe { get; set; } = string.Empty;

        [MaxLength(15)]
        public string? DienThoai { get; set; }

        [MaxLength(200)]
        public string? DiaChi { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        public DateTime? NgayTiepNhan { get; set; }

        // Thuộc tính tính toán, giá trị mặc định là 0
        [Column(TypeName = "decimal(18,0)")]
        public decimal TienNo { get; set; } = 0;

        // Navigation properties
        [ForeignKey("MaHieuXe")]
        public virtual HieuXe? HieuXe { get; set; }

        public virtual ICollection<PhieuSuaChua> DanhSachPhieuSuaChua { get; set; } = new List<PhieuSuaChua>();
        public virtual ICollection<PhieuThuTien> DanhSachPhieuThuTien { get; set; } = new List<PhieuThuTien>();
    }
}
