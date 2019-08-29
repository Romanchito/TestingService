using System.Collections.Generic;

namespace TestingService.Models.EntityModels
{
    public class Question
    {
        public int Id { get; set; }
        public string Text_of_question { get; set; }
        public int QuestId { get; set; }
        public string EntryType { get; set; }
        public virtual Quest quest { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

        public Question()
        {
        }

        public Question(int questionId)
        {
            QuestId = questionId;
        }
    }
}