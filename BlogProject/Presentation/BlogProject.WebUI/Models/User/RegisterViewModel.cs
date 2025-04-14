using System.ComponentModel.DataAnnotations;

namespace BlogProject.WebUI.Models.User
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Adınızı ve soyadınızı giriniz.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı alanı boş bırakılamaz.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "E-posta adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bir şifre belirlemelisiniz.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Şifreniz en az 6 karakter olmalıdır.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$",
        ErrorMessage = "Şifreniz en az bir küçük harf, bir büyük harf ve bir rakam içermelidir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
