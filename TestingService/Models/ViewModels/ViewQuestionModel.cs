using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestingService.BLL.DTO;

namespace TestingService.Models.ViewModels
{
    public class ViewQuestionModel
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 4, ErrorMessage = "Длина должна быть от 4 до 60 символов")]
        [Display(Name = "Текст вопроса")]
        [Required]
        public string Text_of_question { get; set; }
        public int? ImageId { get; set; }
        public int QuestId { get; set; }
        public string EntryType { get; set; }
        public QuestDTO Quest { get; set; }
        public ICollection<AnswerDTO> Answers { get; set; }
        public ImageDTO Image { get; set; }
        public ViewQuestionModel()
        {
        }

        public ViewQuestionModel(int questionId)
        {
            QuestId = questionId;
        }
    }
}