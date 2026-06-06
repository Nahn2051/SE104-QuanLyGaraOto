using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGaraOto.Models
{
    [Table("CT_PHIEUSUACHUA")]
    public class ChiTietPhieuSuaChua
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaCTSC { get; set; }

        [Required]
        public int MaPhieuSuaChua { get; set; }

        [Required]
        [MaxLength(200)]
        public string NoiDungSuaChua { get; set; } = string.Empty;

        public int? MaVTPT { get; set; }

        public int? MaTienCong { get; set; }

        public int SoLuong { get; set; } = 1;

        [Column("TienCong", TypeName = "decimal(18,0)")]
        public decimal ChiPhiTienCong { get; set; } = 0;

        [Column(TypeName = "decimal(18,0)")]
        public decimal DonGia { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal ThanhTien { get; set; }

        // Navigation properties
        [ForeignKey("MaPhieuSuaChua")]
        public virtual PhieuSuaChua? PhieuSuaChua { get; set; }

        [ForeignKey("MaVTPT")]
        public virtual VatTuPhuTung? VatTuPhuTung { get; set; }

        [ForeignKey("MaTienCong")]
        public virtual TienCong? TienCong { get; set; }
    }
}
