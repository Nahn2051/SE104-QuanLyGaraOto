using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGaraOto.Models
{
    [Table("THAMSO")]
    public class ThamSo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(100)]
        public string TenThamSo { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string GiaTri { get; set; } = string.Empty;
    }
}
