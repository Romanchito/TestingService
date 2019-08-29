using System.ComponentModel.DataAnnotations;

namespace TestingService.Models.AccountModels
{
    public class LoginModel
    {
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Display(Name = "Почта")]
        [Required]
        public string Email { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 20 символов")]
        [Display(Name = "Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}