using System.ComponentModel.DataAnnotations;

namespace TestingService.Models.ViewModels
{
    public class ViewUserModel
    {
        public int Id { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Display(Name = "Почтовый ардес")]
        [Required]
        public string Email { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 20 символов")]
        [Display(Name = "Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина имени должна быть от 3 до 20 символов")]
        [Display(Name = "Имя")]
        [Required]
        public string Name { get; set; }

        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина фамилии должна быть от 3 до 25 символов")]
        [Display(Name = "Фамилия")]
        [Required]
        public string Surname { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина отчества должна быть от 3 до 20 символов")]
        [Display(Name = "Отчество")]
        public string Patronomic { get; set; }        
        public int? RoleId { get; set; }
        public int? GroupId { get; set; }
    }
}