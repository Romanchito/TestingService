namespace TestingService.Models.EntityModels
{
    public class Answer
    {
        public int Id { get; set; }
        public bool isTrue { get; set; }
        public string Text_of_answer { get; set; }
        public int QuestionId { get; set; }
        public virtual Question question { get; set; }

        public Answer()
        {
        }

        public Answer(int questionId)
        {
            QuestionId = questionId;
        }
    }
}