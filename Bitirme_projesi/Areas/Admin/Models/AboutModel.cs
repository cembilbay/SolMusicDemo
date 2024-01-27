using System.ComponentModel.DataAnnotations;

namespace Bitirme_projesi.Areas.Admin.Models
{
    public class AboutModel
    {

        public int AboutID { get; set; }

        [Display(Name = "About Name 1")]
        public string AboutName1 { get; set; }

        [Display(Name = "About Name 2")]
        public string AboutName2 { get; set; }

        [Display(Name = "About Image")]
        public IFormFile AboutImageFile { get; set; } 

        [Display(Name = "About Status")]
        public string AboutStatus { get; set; }

        [Display(Name = "About Content 1")]
        public string AboutContent1 { get; set; }

        [Display(Name = "About Content 2")]
        public string AboutContent2 { get; set; }
    }
}



