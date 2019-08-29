using TestingService.DAL.Entities;

namespace TestingService.DAL.Source
{
    public class AnswerInfo
    {
        public int Id { get; set; }
        public int ResultId { get; set; }
        public bool isTrue { get; set; }
        public string Text_of_answer { get; set; }
        public int QuestionId { get; set; }       
        public virtual Result Result { get; set; }
    }
}