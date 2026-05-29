using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGaraOto.Models
{
    [Table("CT_PHIEUNHAP")]
    public class ChiTietPhieuNhap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaCTPN { get; set; }

        [Required]
        public int MaPhieuNhap { get; set; }

        [Required]
        public int MaVTPT { get; set; }

        public int SoLuong { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal DonGia { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal ThanhTien { get; set; }

        // Navigation properties
        [ForeignKey("MaPhieuNhap")]
        public virtual PhieuNhap? PhieuNhap { get; set; }

        [ForeignKey("MaVTPT")]
        public virtual VatTuPhuTung? VatTuPhuTung { get; set; }
    }
}
