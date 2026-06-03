using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGaraOto.Models
{
    [Table("PHIEUTHUTIEN")]
    public class PhieuThuTien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaPhieuThuTien { get; set; }

        [Required]
        public int MaXe { get; set; }

        [Required]
        public DateTime NgayThuTien { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal SoTienThu { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal TienNoTruocThu { get; set; }

        [NotMapped]
        public decimal TienPhat => SoTienThu > TienNoTruocThu ? SoTienThu - TienNoTruocThu : 0;

        // Navigation properties
        [ForeignKey("MaXe")]
        public virtual Xe? Xe { get; set; }
    }
}
