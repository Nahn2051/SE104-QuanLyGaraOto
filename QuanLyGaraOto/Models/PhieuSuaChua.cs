using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGaraOto.Models
{
    [Table("PHIEUSUACHUA")]
    public class PhieuSuaChua
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaPhieuSuaChua { get; set; }

        [Required]
        public DateTime NgaySuaChua { get; set; }

        [Required]
        public int MaXe { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal TongTien { get; set; } = 0;

        // Navigation properties
        [ForeignKey("MaXe")]
        public virtual Xe? Xe { get; set; }

        public virtual ICollection<ChiTietPhieuSuaChua> DanhSachCTPhieuSuaChua { get; set; } = new List<ChiTietPhieuSuaChua>();
    }
}
