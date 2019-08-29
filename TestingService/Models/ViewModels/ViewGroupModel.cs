using System.ComponentModel.DataAnnotations;

namespace TestingService.Models.ViewModels
{
    public class ViewGroupModel
    {
        public int Id { get; set; }

        [StringLength(15, MinimumLength = 3, ErrorMessage = "Длина должна быть от 3 до 15 символов")]
        [Required]
        public string Name { get; set; }
    }
}