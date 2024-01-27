using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CoreDemo.Models
{
    public class UserEditModel
    {

        public int UserID { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen Kullanıcı Adı giriniz")]
        public string UserName { get; set; }
        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "Lütfen ad soyad giriniz")]
        public string? NameSurname { get; set; }
        [Display(Name = "Şifre Tekrar")]
        [Compare("UserPassword", ErrorMessage = "Şifreler uyuşmuyor!")]
        public string? ConfirmPassword { get; set; }
        [Display(Name = "Resim")]
        [Required(ErrorMessage = "Lütfen resim dosyası seçiniz")]
        
        public string? UserMail { get; set; }
        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz")]
        public string? UserPassword { get; set; }
        public bool? UserStatus { get; set; }
    }
}
