using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGaraOto.Models
{
    [Table("TIENCONG")]
    public class TienCong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTienCong { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenTienCong { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,0)")]
        public decimal DonGia { get; set; }

        // Navigation properties
        public virtual ICollection<ChiTietPhieuSuaChua> DanhSachCTPhieuSuaChua { get; set; } = new List<ChiTietPhieuSuaChua>();
    }
}
