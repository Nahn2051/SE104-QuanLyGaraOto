using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGaraOto.Models
{
    [Table("HIEUXE")]
    public class HieuXe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHieuXe { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenHieuXe { get; set; } = string.Empty;

        // Navigation properties
        public virtual ICollection<Xe> DanhSachXe { get; set; } = new List<Xe>();
    }
}
