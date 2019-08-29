using System.ComponentModel.DataAnnotations;

namespace TestingService.Models.AccountModels
{
    public class RegisterModel
    {
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Required]
        public string Email { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 20 символов")]
        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public string Patronomic { get; set; }

        public string Role { get; set; }
    }
}