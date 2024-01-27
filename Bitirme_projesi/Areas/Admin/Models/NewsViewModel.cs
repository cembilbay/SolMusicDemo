using System.ComponentModel.DataAnnotations;

namespace Bitirme_projesi.Areas.Admin.Models
{
    public class NewsViewModel
    {
        public int NewsID { get; set; }

        [Required(ErrorMessage = "Haber Başlığı gereklidir.")]
        public string NewsTitle { get; set; }

        [Required(ErrorMessage = "Haber İçeriği gereklidir.")]
        public string NewsContent { get; set; }

        [Display(Name = "Haber Resmi")]
        public IFormFile NewsImage { get; set; }

        [Display(Name = "Haber Oluşturulma Zamanı")]
        [DataType(DataType.DateTime)]
        public DateTime? NewsCreateTime { get; set; }

        [Display(Name = "Haber Durumu")]
        public bool? NewsStatus { get; set; }
    }
}
