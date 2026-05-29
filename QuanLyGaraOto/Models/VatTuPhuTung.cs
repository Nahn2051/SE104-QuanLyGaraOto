using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGaraOto.Models
{
    [Table("VATTUPHUTUNG")]
    public class VatTuPhuTung
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaVTPT { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenVTPT { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,0)")]
        public decimal DonGia { get; set; }

        public int SoLuongTon { get; set; } = 0;

        // Navigation properties
        public virtual ICollection<ChiTietPhieuSuaChua> DanhSachCTPhieuSuaChua { get; set; } = new List<ChiTietPhieuSuaChua>();
        public virtual ICollection<ChiTietPhieuNhap> DanhSachCTPhieuNhap { get; set; } = new List<ChiTietPhieuNhap>();
    }
}
