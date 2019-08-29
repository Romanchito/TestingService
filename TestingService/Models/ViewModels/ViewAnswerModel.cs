using System.ComponentModel.DataAnnotations;
using TestingService.BLL.DTO;

namespace TestingService.Models.ViewModels
{
    public class ViewAnswerModel
    {
        public int Id { get; set; }
        public bool isTrue { get; set; }
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Длина имени должна быть от 1 до 60 символов")]        
        [Display(Name = "Текст ответа")]
        [Required]
        public string Text_of_answer { get; set; }
        public int QuestionId { get; set; }
        public QuestionDTO Question { get; set; }

        public ViewAnswerModel()
        {
        }

        public ViewAnswerModel(int questionId)
        {
            QuestionId = questionId;
        }
    }
}