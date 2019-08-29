using System.Collections.Generic;

namespace TestingService.BLL.DTO
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Text_of_question { get; set; }
        public int QuestId { get; set; }
        public int? ImageId { get; set; }
        public string EntryType { get; set; }
        public QuestDTO Quest { get; set; }
        public ICollection<AnswerDTO> Answers { get; set; }
        public ImageDTO Image { get; set; }
        public QuestionDTO()
        {
        }

        public QuestionDTO(int questionId)
        {
            QuestId = questionId;
        }
    }
}